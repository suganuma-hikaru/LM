' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM100V : 商品マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMM100Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMM100V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM100F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV


    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが設定される前にアクセスして例外が発生する問題に対応 20151106 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM100F, ByVal v As LMMControlV)

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM100C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM100C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM100C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM100C.EventShubetsu.FUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM100C.EventShubetsu.SAKUJO_HUKKATU          '削除・復活
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM100C.EventShubetsu.KENSAKU         '検索
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
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

            Case LMM100C.EventShubetsu.TANKA_IKKATU_HENKO    '単価一括処理
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

                '2015.10.02 他荷主対応START
            Case LMM100C.EventShubetsu.TANINUSI    '他荷主
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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
                '2015.10.02 他荷主対応END

                'START YANAI 要望番号372
            Case LMM100C.EventShubetsu.NINUSHI_IKKATU_HENKO    '荷主一括処理
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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
                'END YANAI 要望番号372

            Case LMM100C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM100C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM100C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM100C.EventShubetsu.DOUBLE_CLICK         'ダブルクリック
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
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

            Case LMM100C.EventShubetsu.ENTER          'Enter
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
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

            Case LMM100C.EventShubetsu.PRINT   '印刷
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

            Case LMM100C.EventShubetsu.KIKEN_KAKUNIN
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
            Case LMM100C.EventShubetsu.VOLUME_IKKATU
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
        Return False


    End Function

    ''' <summary>
    ''' 編集押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsHenshuChk() As Boolean

        '2015.11.02 tsunehira add Start
        '英語化対応
        Dim msg As String = _Frm.FunctionKey.F2ButtonName
        '2015.11.02 tsunehira add End
        'Dim msg As String = "編集"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.lblGoodsKey.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        'データ存在チェックを行う
        If Me._ControlV.IsRecordStatusChk(Me._Frm.lblSituation.RecordStatus) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 複写押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsFukushaChk() As Boolean

        '2015.11.02 tsunehira add Start
        '英語化対応
        Dim msg As String = _Frm.FunctionKey.F3ButtonName
        '2015.11.02 tsunehira add End
        'Dim msg As String = "複写"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.lblGoodsKey.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 削除/復活押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSakujoHukkatuChk() As Boolean

        '2015.11.02 tsunehira add Start
        '英語化対応
        Dim msg As String = _Frm.FunctionKey.F4ButtonName
        '2015.11.02 tsunehira add End
        'Dim msg As String = "削除・復活"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.lblGoodsKey.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 単価一括変更押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsTankaIkkatuChk(ByVal list As ArrayList) As Boolean

        With Me._Frm.sprGoods.ActiveSheet

            '2015.11.02 tsunehira add Start
            '英語化対応
            Dim msg As String = _Frm.FunctionKey.F5ButtonName
            '2015.11.02 tsunehira add End
            'Dim msg As String = "単価一括変更"

            '必須選択チェック
            If list.Count = 0 Then
                MyBase.ShowMessage("E009")
                Return False
            End If

            '選択件数チェック
            Dim filter As String = String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I004, "'")
            filter = String.Concat(filter, " AND KBN_CD = '00'")
            filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")
            Dim limitData As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
            Dim limitCount As Decimal = Convert.ToDecimal(limitData(0).Item("VALUE1").ToString)

            If limitCount < list.Count Then
                MyBase.ShowMessage("E168", New String() {list.Count.ToString(), Math.Round(limitCount, 0).ToString()})
                Return False
            End If

            '他営業所チェック
            Dim row As Integer = Convert.ToInt32(list(0).ToString)
            Dim brCd As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
            If Me._ControlV.IsUserNrsBrCdChk(brCd, msg) = False Then
                Return False
            End If

            '同一荷主チェック
            Dim custL As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
            Dim custM As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))

            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max
                row = Convert.ToInt32(list(i).ToString)

                If Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo)).Equals(custL) = False _
                OrElse Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo)).Equals(custM) = False Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.ShowMessage("E824")
                    '20151029 tsunehira add End
                    'MyBase.ShowMessage("E264", New String() {msg, "荷主コード(大)・(中)の組合せ"})
                    Return False
                End If
            Next

            'データ存在チェック
            For i As Integer = 0 To max
                row = Convert.ToInt32(list(i).ToString)

                If Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SYS_DEL_FLG.ColNo)).Equals(LMConst.FLG.ON) Then
                    MyBase.ShowMessage("E035")
                    Return False
                End If
            Next

        End With

        Return True

    End Function

    '2015.10.02 他荷主対応START
    ''' <summary>
    ''' 他荷主押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsTaninusiChk(ByVal list As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm.sprGoods.ActiveSheet

            Dim msg As String = String.Empty

            '指定運送会社コード
            '2017/09/25 修正 李↓
            msg = lgm.Selector({"他荷主", "Other Custmer", "다른 하주", "中国語"})
            '2017/09/25 修正 李↑

            '必須選択チェック
            If list.Count = 0 Then
                MyBase.ShowMessage("E009")
                Return False
            End If

            '選択件数チェック
            Dim filter As String = String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I004, "'")
            filter = String.Concat(filter, " AND KBN_CD = '00'")
            filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")
            Dim limitData As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
            Dim limitCount As Decimal = Convert.ToDecimal(limitData(0).Item("VALUE1").ToString)

            If limitCount < list.Count Then
                MyBase.ShowMessage("E168", New String() {list.Count.ToString(), Math.Round(limitCount, 0).ToString()})
                Return False
            End If

            '他営業所チェック
            Dim row As Integer = Convert.ToInt32(list(0).ToString)
            Dim brCd As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
            If Me._ControlV.IsUserNrsBrCdChk(brCd, msg) = False Then
                Return False
            End If

            '同一荷主チェック
            Dim custL As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
            Dim custM As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))

            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max
                row = Convert.ToInt32(list(i).ToString)

                If Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo)).Equals(custL) = False _
                OrElse Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo)).Equals(custM) = False Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.ShowMessage("E825")
                    '20151029 tsunehira add End
                    'MyBase.ShowMessage("E264", New String() {msg, "荷主コード(大)・(中)の組合せ"})
                    Return False
                End If
            Next

            'データ存在チェック
            For i As Integer = 0 To max
                row = Convert.ToInt32(list(i).ToString)

                If Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SYS_DEL_FLG.ColNo)).Equals(LMConst.FLG.ON) Then
                    MyBase.ShowMessage("E035")
                    Return False
                End If
            Next

        End With

        Return True

    End Function
    '2015.10.02 他荷主対応END

    'START YANAI 要望番号372
    ''' <summary>
    ''' 荷主一括変更押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsNinushiIkkatuChk(ByVal list As ArrayList) As Boolean

        With Me._Frm.sprGoods.ActiveSheet


            '2015.11.02 tsunehira add Start
            '英語化対応
            Dim msg As String = _Frm.FunctionKey.F6ButtonName
            '2015.11.02 tsunehira add End
            'Dim msg As String = "荷主一括変更"

            '必須選択チェック
            If list.Count = 0 Then
                MyBase.ShowMessage("E009")
                Return False
            End If

            '選択件数チェック
            Dim filter As String = String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_I004, "'")
            filter = String.Concat(filter, " AND KBN_CD = '00'")
            filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")
            Dim limitData As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
            Dim limitCount As Decimal = Convert.ToDecimal(limitData(0).Item("VALUE1").ToString)

            If limitCount < list.Count Then
                MyBase.ShowMessage("E168", New String() {list.Count.ToString(), Math.Round(limitCount, 0).ToString()})
                Return False
            End If

            '他営業所チェック
            Dim row As Integer = Convert.ToInt32(list(0).ToString)
            Dim brCd As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
            If Me._ControlV.IsUserNrsBrCdChk(brCd, msg) = False Then
                Return False
            End If

            '同一荷主チェック
            Dim custL As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
            Dim custM As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))

            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max
                row = Convert.ToInt32(list(i).ToString)

                If Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo)).Equals(custL) = False _
                OrElse Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo)).Equals(custM) = False Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.ShowMessage("E817")
                    '20151029 tsunehira add End
                    'MyBase.ShowMessage("E264", New String() {msg, "荷主コード(大)・(中)の組合せ"})
                    Return False
                End If
            Next

            'データ存在チェック
            For i As Integer = 0 To max
                row = Convert.ToInt32(list(i).ToString)

                If Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SYS_DEL_FLG.ColNo)).Equals(LMConst.FLG.ON) Then
                    MyBase.ShowMessage("E035")
                    Return False
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主ポップ選択時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsNinushiPopSelectChk(ByVal list As ArrayList _
                                        , ByVal prm As LMFormData) As Boolean

        With Me._Frm.sprGoods.ActiveSheet

            '2015.11.02 tsunehira add Start
            '英語化対応
            Dim msg As String = _Frm.FunctionKey.F6ButtonName
            '2015.11.02 tsunehira add End
            'Dim msg As String = "荷主一括変更"

            '同一荷主チェック
            Dim row As Integer = Convert.ToInt32(list(0).ToString)
            Dim brCd As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
            Dim custL As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
            Dim custM As String = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            ' ポップアップウィンドウの「大中設定」押下による戻りの場合、CUST_CD_S と CUST_CD_SS が空で返るが、
            ' 編集モードで保存時入力必須の項目を空で更新することは妥当ではないため、チェックエラーとする。
            Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
            If dr.Item("CUST_CD_S").ToString() = "" Then
                MyBase.ShowMessage("E001", New String() {lgm.Selector({"荷主(小)", "Custmer(S)", "하주(小)", "中国語"})})
                Return False
            End If

            If brCd.Equals(dr.Item("NRS_BR_CD").ToString()) = False OrElse _
               custL.Equals(dr.Item("CUST_CD_L").ToString()) = False OrElse _
               custM.Equals(dr.Item("CUST_CD_M").ToString()) = False Then
                MyBase.ShowMessage("E422")
                Return False
            End If

        End With

        Return True

    End Function
    'END YANAI 要望番号372

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'スペース除去
        Call Me.TrinmFindRow()

        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As String) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM100C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._ControlV.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim msg As String() = Nothing
        Dim msg_1 As String = "Stock during operation division"

        '2017/09/25 修正 李↓
        'Dim msg_2 As String = "Stock during operation division"
        Dim msg_2 As String = "Factory processing work classification"
        '2017/09/25 修正 李↑


        With Me._Frm

            Select Case objNm
                Case .txtCustCdL.Name _
                   , .txtCustCdM.Name _
                   , .txtCustCdS.Name _
                   , .txtCustCdSS.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM, .txtCustCdS, .txtCustCdSS}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL, .lblCustNmM, .lblCustNmS, .lblCustNmSS}

                    '指定運送会社コード
                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"荷主コード(大)", "Custmer Code(L)", "하주코드(大)", "中国語"}),
                                        lgm.Selector({"荷主コード(中)", "Custmer Code(M)", "하주코드(中)", "中国語"}),
                                        lgm.Selector({"荷主コード(小)", "Custmer Code(S)", "하주코드(小)", "中国語"}),
                                        lgm.Selector({"荷主コード(極小)", "Custmer Code(SS)", "하주코드(極小)", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtNisonin.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtNisonin}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblNisonin}
                    msg = New String() {.lblTitleNisonin.Text}
                Case .txtNyukaSagyoKbn1.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtNyukaSagyoKbn1}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"入荷時加工作業区分1", String.Concat(msg_1, "1"), "입하시 가공작업구분1", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtNyukaSagyoKbn2.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtNyukaSagyoKbn2}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"入荷時加工作業区分2", String.Concat(msg_1, "2"), "입하시 가공작업구분2", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtNyukaSagyoKbn3.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtNyukaSagyoKbn3}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"入荷時加工作業区分3", String.Concat(msg_1, "3"), "입하시 가공작업구분3", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtNyukaSagyoKbn4.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtNyukaSagyoKbn4}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"入荷時加工作業区分4", String.Concat(msg_1, "4"), "입하시 가공작업구분4", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtNyukaSagyoKbn5.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtNyukaSagyoKbn5}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"入荷時加工作業区分5", String.Concat(msg_1, "5"), "입하시 가공작업구분5", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtShukkaSagyoKbn1.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtShukkaSagyoKbn1}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"出荷時加工作業区分1", String.Concat(msg_2, "1"), "출하시 가공작업구분1", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtShukkaSagyoKbn2.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtShukkaSagyoKbn2}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"出荷時加工作業区分2", String.Concat(msg_2, "2"), "출하시 가공작업구분2", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtShukkaSagyoKbn3.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtShukkaSagyoKbn3}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"出荷時加工作業区分3", String.Concat(msg_2, "3"), "출하시 가공작업구분3", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtShukkaSagyoKbn4.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtShukkaSagyoKbn4}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"出荷時加工作業区分4", String.Concat(msg_2, "4"), "출하시 가공작업구분4", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtShukkaSagyoKbn5.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtShukkaSagyoKbn5}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"出荷時加工作業区分5", String.Concat(msg_2, "5"), "출하시 가공작업구분5", "中国語"})}
                    '2017/09/25 修正 李↑

                Case .txtKonpoSagyoCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtKonpoSagyoCd}
                    clearCtl = New Win.InputMan.LMImTextBox() {.lblKonpoSagyoCd}
                    msg = New String() {.lblTitleKonpoSagyoCd.Text}
                Case .txtTankaGroup.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtTankaGroup}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblTekiyoStartDate _
                                                                                 , .numHokanTujo _
                                                                                 , .cmbHokanTujo _
                                                                                 , .numHokanTeion _
                                                                                 , .cmbHokanTeion _
                                                                                 , .numNiyakuNyuko _
                                                                                 , .cmbNiyakuNyuko _
                                                                                 , .numNiyakuShukko _
                                                                                 , .cmbNiyakuShukko _
                                                                                 , .numNiyakuMinHosho}
                    msg = New String() {.lblTitleTankaGroup.Text}
                Case .txtShobo.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtShobo}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblShobo}
                    msg = New String() {.lblTitleShobo.Text}

                Case .txtUn.Name
                    ctl = New Win.InputMan.LMImTextBox() { .txtUn}

                    msg = New String() { .lblTitleUn.Text}

                Case .txtPg.Name
                    ctl = New Win.InputMan.LMImTextBox() { .txtPg}

                    msg = New String() { .lblTitlePg.Text}

                Case .txtHasuSagyoKbn1.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtHasuSagyoKbn1}

                    msg = New String() {lgm.Selector({"端数出荷時作業区分1", String.Concat(msg_2, "1"), "출하시 잔수 작업 구분1", "中国語"})}

                Case .txtHasuSagyoKbn2.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtHasuSagyoKbn2}

                    msg = New String() {lgm.Selector({"端数出荷時作業区分2", String.Concat(msg_2, "2"), "출하시 잔수 작업 구분2", "中国語"})}

                Case .txtHasuSagyoKbn3.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtHasuSagyoKbn3}

                    msg = New String() {lgm.Selector({"端数出荷時作業区分3", String.Concat(msg_2, "3"), "출하시 잔수 작업 구분3", "中国語"})}
            End Select

            Dim focusCtl As Control = Me._Frm.ActiveControl
            Return Me._ControlV.IsFocusChk(actionType, ctl, msg, focusCtl, clearCtl)

        End With

    End Function

    ''' <summary>
    ''' 保存押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSaveChk(ByVal dateStr As String, ByVal zaikoFlg As Boolean, ByVal sumPoraZaiNb As Integer) As Boolean  'Add 2019/07/31 要望管理006855
        ''Friend Function IsSaveChk(ByVal dateStr As String, ByVal zaikoFlg As Boolean) As Boolean                           'Del 2019/07/31 要望管理006855

        With Me._Frm

            'スペース除去
            Call Me._ControlV.TrimSpaceTextvalue(.tabGoodsMst)
            Call Me._ControlV.TrimSpaceSprTextvalue(.sprGoodsDetail _
                                                    , .sprGoodsDetail.ActiveSheet.Rows.Count - 1 _
                                                    , LMM100G.sprGoodsDtlDef.BIKO.ColNo)

            '単項目チェック
            If Me.IsSaveSingleChk() = False Then
                Return False
            End If

            'マスタ存在チェック
            If Me.IsSaveExistMst(dateStr) = False Then
                Return False
            End If

            '関連チェック
            ''If Me.IsSaveRelationChk(zaikoFlg) = False Then              'Del 2019/07/31 要望管理006855
            If Me.IsSaveRelationChk(zaikoFlg, sumPoraZaiNb) = False Then  'Add 2019/07/31 要望管理006855
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 行追加時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsAddRowChk(ByVal maxSeq As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '空行チェック
        If Me.IsKuranChk() = False Then
            Return False
        End If

        '上限チェック
        Dim msg As String = String.Empty

        '2017/09/25 修正 李↓
        msg = lgm.Selector({"商品KEY枝番", "Goods KEY Branch Number", "상품KEY 가지번호", "中国語"})
        '2017/09/25 修正 李↑

        maxSeq = maxSeq + 1
        If Me._ControlV.IsMaxChk(maxSeq, 99, msg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 行削除時チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDelRowChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 印刷ボタン押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsPrintChk() As Boolean

        'スペース除去
        Call Me.TrinmFindRow()

        '単項目チェック
        If Me.IsPrintSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBaseMsg()

        Select Case Me._Frm.lblSituation.DispMode
            Case DispMode.INIT

                MyBase.ShowMessage("G007")

            Case DispMode.VIEW

                MyBase.ShowMessage("G013")

            Case DispMode.EDIT

                MyBase.ShowMessage("G003")

        End Select

    End Sub

    ''' <summary>
    ''' 危険品情報確認時チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsConfirmKikenChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 容積一括更新時チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsConfirmVolumeChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

#Region "内部メソッド"

    ''' <summary>
    ''' 検索行のTrim処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrinmFindRow()

        With Me._Frm.sprGoods
            Call Me._ControlV.TrimSpaceSprTextvalue(Me._Frm.sprGoods, 0)
            'Trimをかける
            Dim custCd As String = Me._ControlV.GetCellValue(.ActiveSheet.Cells(0, LMM100G.sprGoodsDef.CUST_CD.ColNo)).Replace("-", "")
            '荷主コードの"-"を削除する
            .SetCellValue(0, LMM100G.sprGoodsDef.CUST_CD.ColNo, custCd)
        End With

    End Sub

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprGoods)

            '【営業所コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.BR_NM.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.BR_NM.ColName
            vCell.IsHissuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.CUST_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.CUST_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 11
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主名(大)】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.CUST_NM_L.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.CUST_NM_L.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【商品コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.GOODS_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.GOODS_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【商品名】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.GOODS_NM_1.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.GOODS_NM_1.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【単価グループコード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.TANKA_GROUP_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.TANKA_GROUP_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【消防コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.SHOBO_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.SHOBO_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【請求先コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.SEIQT_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.SEIQT_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【請求先会社名】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.SEIQT_COMP_NM.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.SEIQT_COMP_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【請求先部署名】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.SEIQT_BUSHO_NM.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.SEIQT_BUSHO_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        '荷主コード、名称の必須チェック
        Return Me.IsKanrenHissuChk(0)

    End Function

    ''' <summary>
    ''' 荷主コード、荷主名の必須チェック
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKanrenHissuChk(ByVal rowNo As Integer) As Boolean

        Dim spr As Win.Spread.LMSpread = Me._Frm.sprGoods
        With spr.ActiveSheet

            '荷主コード、荷主名両方に値が無い場合、エラー
            If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(rowNo, LMM100G.sprGoodsDef.CUST_CD.ColNo))) = True _
                AndAlso String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(rowNo, LMM100G.sprGoodsDef.CUST_NM_L.ColNo))) = True Then

                .Cells(rowNo, LMM100G.sprGoodsDef.CUST_NM_L.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._ControlV.SetErrorControl(spr, rowNo, LMM100G.sprGoodsDef.CUST_CD.ColNo)
                Return Me._ControlV.SetErrMessage("E270", New String() {Me._ControlV.SetRepMsgData(LMM100G.sprGoodsDef.CUST_CD.ColName), Me._ControlV.SetRepMsgData(LMM100G.sprGoodsDef.CUST_NM_L.ColName)})

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '******************** 商品タブ ********************

            '【営業所】
            .cmbBr.ItemName = .lblTitleBr.Text
            .cmbBr.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBr) = False Then
                Call Me._ControlV.SetErrorControl(.cmbBr, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷主コード(大)】

            '2017/09/25 修正 李↓
            .txtCustCdL.ItemName = lgm.Selector({"荷主(大)", "Custmer(L)", "하주(大)", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            .txtCustCdL.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCdL, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷主コード(中)】

            '2017/09/25 修正 李↓
            .txtCustCdM.ItemName = lgm.Selector({"荷主(中)", "Custmer(M)", "하주(中)", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            .txtCustCdM.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCdM, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷主コード(小)】

            '2017/09/25 修正 李↓
            .txtCustCdS.ItemName = lgm.Selector({"荷主(小)", "Custmer(S)", "하주(小)", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCdS.IsHissuCheck = True
            .txtCustCdS.IsForbiddenWordsCheck = True
            .txtCustCdS.IsFullByteCheck = 2
            .txtCustCdS.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdS) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCdS, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷主コード(極小)】

            '2017/09/25 修正 李↓
            .txtCustCdSS.ItemName = lgm.Selector({"荷主(極小)", "Custmer(SS)", "하주(極小)", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCdSS.IsHissuCheck = True
            .txtCustCdSS.IsForbiddenWordsCheck = True
            .txtCustCdSS.IsFullByteCheck = 2
            .txtCustCdSS.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdSS) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCdSS, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷送人コード】
            .txtNisonin.ItemName = .lblTitleNisonin.Text
            .txtNisonin.IsForbiddenWordsCheck = True
            .txtNisonin.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtNisonin) = False Then
                Call Me._ControlV.SetErrorControl(.txtNisonin, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【商品名1】
            .txtGoodsNm1.ItemName = .lblTitleGoodsNm1.Text
            .txtGoodsNm1.IsHissuCheck = True
            .txtGoodsNm1.IsForbiddenWordsCheck = True
            .txtGoodsNm1.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtGoodsNm1) = False Then
                Call Me._ControlV.SetErrorControl(.txtGoodsNm1, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【商品名2】
            .txtGoodsNm2.ItemName = .lblTitleGoodsNm2.Text
            .txtGoodsNm2.IsForbiddenWordsCheck = True
            .txtGoodsNm2.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtGoodsNm2) = False Then
                Call Me._ControlV.SetErrorControl(.txtGoodsNm2, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【商品名3】
            .txtGoodsNm3.ItemName = .lblTitleGoodsNm3.Text
            .txtGoodsNm3.IsForbiddenWordsCheck = True
            .txtGoodsNm3.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtGoodsNm3) = False Then
                Call Me._ControlV.SetErrorControl(.txtGoodsNm3, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【商品コード】
            .txtGoodsCd.ItemName = .lblTitleGoodsCd.Text
            .txtGoodsCd.IsHissuCheck = True
            .txtGoodsCd.IsForbiddenWordsCheck = True
            .txtGoodsCd.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtGoodsCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtGoodsCd, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【入荷時加工作業コード1】

            '2017/09/25 修正 李↓
            .txtNyukaSagyoKbn1.ItemName = lgm.Selector({"入荷時加工作業区分1", "Stock during operation division 1", "입하시 가공작업구분1", "中国語"})
            '2017/09/25 修正 李↑

            .txtNyukaSagyoKbn1.IsForbiddenWordsCheck = True
            .txtNyukaSagyoKbn1.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtNyukaSagyoKbn1) = False Then
                Call Me._ControlV.SetErrorControl(.txtNyukaSagyoKbn1, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【入荷時加工作業コード2】

            '2017/09/25 修正 李↓
            .txtNyukaSagyoKbn2.ItemName = lgm.Selector({"入荷時加工作業区分2", "Stock during operation division 2", "입하시 가공작업구분2", "中国語"})
            '2017/09/25 修正 李↑

            .txtNyukaSagyoKbn2.IsForbiddenWordsCheck = True
            .txtNyukaSagyoKbn2.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtNyukaSagyoKbn2) = False Then
                Call Me._ControlV.SetErrorControl(.txtNyukaSagyoKbn2, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【入荷時加工作業コード3】

            '2017/09/25 修正 李↓
            .txtNyukaSagyoKbn3.ItemName = lgm.Selector({"入荷時加工作業区分3", "Stock during operation division 3", "입하시 가공작업구분3", "中国語"})
            '2017/09/25 修正 李↑

            .txtNyukaSagyoKbn3.IsForbiddenWordsCheck = True
            .txtNyukaSagyoKbn3.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtNyukaSagyoKbn3) = False Then
                Call Me._ControlV.SetErrorControl(.txtNyukaSagyoKbn3, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【入荷時加工作業コード4】

            '2017/09/25 修正 李↓
            .txtNyukaSagyoKbn4.ItemName = lgm.Selector({"入荷時加工作業区分4", "Stock during operation division 4", "입하시 가공작업구분4", "中国語"})
            '2017/09/25 修正 李↑

            .txtNyukaSagyoKbn4.IsForbiddenWordsCheck = True
            .txtNyukaSagyoKbn4.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtNyukaSagyoKbn4) = False Then
                Call Me._ControlV.SetErrorControl(.txtNyukaSagyoKbn4, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【入荷時加工作業コード5】

            '2017/09/25 修正 李↓
            .txtNyukaSagyoKbn5.ItemName = lgm.Selector({"入荷時加工作業区分5", "Stock during operation division 5", "입하시 가공작업구분5", "中国語"})
            '2017/09/25 修正 李↑

            .txtNyukaSagyoKbn5.IsForbiddenWordsCheck = True
            .txtNyukaSagyoKbn5.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtNyukaSagyoKbn5) = False Then
                Call Me._ControlV.SetErrorControl(.txtNyukaSagyoKbn5, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【出荷時加工作業コード1】

            '2017/09/25 修正 李↓
            .txtShukkaSagyoKbn1.ItemName = lgm.Selector({"出荷時加工作業区分5", "Factory processing work classification 1", "출하시 가공작업구분1", "中国語"})
            '2017/09/25 修正 李풀

            .txtShukkaSagyoKbn1.IsForbiddenWordsCheck = True
            .txtShukkaSagyoKbn1.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtShukkaSagyoKbn1) = False Then
                Call Me._ControlV.SetErrorControl(.txtShukkaSagyoKbn1, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【出荷時加工作業コード2】

            '2017/09/25 修正 李↓
            .txtShukkaSagyoKbn2.ItemName = lgm.Selector({"出荷時加工作業区分2", "Factory processing work classification 2", "출하시 가공작업구분2", "中国語"})
            '2017/09/25 修正 李풀

            .txtShukkaSagyoKbn2.IsForbiddenWordsCheck = True
            .txtShukkaSagyoKbn2.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtShukkaSagyoKbn2) = False Then
                Call Me._ControlV.SetErrorControl(.txtShukkaSagyoKbn2, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【出荷時加工作業コード3】

            '2017/09/25 修正 李↓
            .txtShukkaSagyoKbn3.ItemName = lgm.Selector({"出荷時加工作業区分3", "Factory processing work classification 3", "출하시 가공작업구분3", "中国語"})
            '2017/09/25 修正 李풀

            .txtShukkaSagyoKbn3.IsForbiddenWordsCheck = True
            .txtShukkaSagyoKbn3.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtShukkaSagyoKbn3) = False Then
                Call Me._ControlV.SetErrorControl(.txtShukkaSagyoKbn3, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【出荷時加工作業コード4】

            '2017/09/25 修正 李↓
            .txtShukkaSagyoKbn4.ItemName = lgm.Selector({"出荷時加工作業区分4", "Factory processing work classification 4", "출하시 가공작업구분4", "中国語"})
            '2017/09/25 修正 李풀

            .txtShukkaSagyoKbn4.IsForbiddenWordsCheck = True
            .txtShukkaSagyoKbn4.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtShukkaSagyoKbn4) = False Then
                Call Me._ControlV.SetErrorControl(.txtShukkaSagyoKbn4, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【出荷時加工作業コード5】

            '2017/09/25 修正 李↓
            .txtShukkaSagyoKbn5.ItemName = lgm.Selector({"出荷時加工作業区分5", "Factory processing work classification 5", "출하시 가공작업구분5", "中国語"})
            '2017/09/25 修正 李풀

            .txtShukkaSagyoKbn5.IsForbiddenWordsCheck = True
            .txtShukkaSagyoKbn5.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtShukkaSagyoKbn5) = False Then
                Call Me._ControlV.SetErrorControl(.txtShukkaSagyoKbn5, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【端数出荷時作業コード1】

            .txtHasuSagyoKbn1.ItemName = lgm.Selector({"端数出荷時作業1", "Fraction processing work classification 1", "분수의 가공 작업 구분1", "中国語"})

            .txtHasuSagyoKbn1.IsForbiddenWordsCheck = True
            .txtHasuSagyoKbn1.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtHasuSagyoKbn1) = False Then
                Call Me._ControlV.SetErrorControl(.txtHasuSagyoKbn1, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【端数出荷時作業コード2】

            .txtHasuSagyoKbn2.ItemName = lgm.Selector({"端数出荷時作業2", "Fraction processing work classification 2", "분수의 가공 작업 구분1", "中国語"})

            .txtHasuSagyoKbn2.IsForbiddenWordsCheck = True
            .txtHasuSagyoKbn2.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtHasuSagyoKbn2) = False Then
                Call Me._ControlV.SetErrorControl(.txtHasuSagyoKbn2, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【端数出荷時作業コード3】

            .txtHasuSagyoKbn3.ItemName = lgm.Selector({"端数出荷時作業3", "Fraction processing work classification 3", "분수의 가공 작업 구분1", "中国語"})

            .txtHasuSagyoKbn3.IsForbiddenWordsCheck = True
            .txtHasuSagyoKbn3.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtHasuSagyoKbn3) = False Then
                Call Me._ControlV.SetErrorControl(.txtHasuSagyoKbn3, .tabGoodsMst, .tpgGoods)
                Return False
            End If


            '【個数単位】
            .cmbKosuTani.ItemName = .lblTitleKosuTani.Text
            .cmbKosuTani.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKosuTani) = False Then
                Call Me._ControlV.SetErrorControl(.cmbKosuTani, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【風袋重量加算】
            .cmbHutaiJyuryo.ItemName = .lblTitleHutaiJyuryoKasan.Text
            .cmbHutaiJyuryo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHutaiJyuryo) = False Then
                Call Me._ControlV.SetErrorControl(.cmbHutaiJyuryo, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【入数】
            If Convert.ToDecimal(.numIrisu.Value) = 0 Then

                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E182", New String() {_Frm.lblTitleIrisu.TextValue, "1"})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E182", New String() {"入数", "1"})
                Call Me._ControlV.SetErrorControl(.numIrisu, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【包装単位】

            '2017/09/25 修正 李↓
            .cmbHosotani.ItemName = lgm.Selector({"包装単位", "Packing unit", "포장단위", "中国語"})
            '2017/09/25 修正 李풀

            .cmbHosotani.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHosotani) = False Then
                Call Me._ControlV.SetErrorControl(.cmbHosotani, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【梱包作業コード】
            .txtKonpoSagyoCd.ItemName = .lblTitleKonpoSagyoCd.Text
            .txtKonpoSagyoCd.IsForbiddenWordsCheck = True
            .txtKonpoSagyoCd.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtKonpoSagyoCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtKonpoSagyoCd, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【出荷時注意事項】
            .txtShukkaChuiJiko.ItemName = .lblTitleShukkaChuiJiko.Text
            .txtShukkaChuiJiko.IsForbiddenWordsCheck = True
            .txtShukkaChuiJiko.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtShukkaChuiJiko) = False Then
                Call Me._ControlV.SetErrorControl(.txtShukkaChuiJiko, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【温度管理区分】

            '2017/09/25 修正 李↓
            .cmbHokanKbnHokan.ItemName = lgm.Selector({"温度管理区分", "Temperature control classification", "온도관리구분", "中国語"})
            '2017/09/25 修正 李풀

            .cmbHokanKbnHokan.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHokanKbnHokan) = False Then
                Call Me._ControlV.SetErrorControl(.cmbHokanKbnHokan, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【運送温度管理区分】

            '2017/09/25 修正 李↓
            .cmbHokanKbnUnso.ItemName = lgm.Selector({"運送温度管理区分", "Transport temperature control classification", "운송온도관리구분", "中国語"})
            '2017/09/25 修正 李풀

            .cmbHokanKbnUnso.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHokanKbnUnso) = False Then
                Call Me._ControlV.SetErrorControl(.cmbHokanKbnUnso, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【温度管理開始月日(保管)】
            If .imdOndoKanriStartHokan.IsDateFullByteCheck = False Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E038", New String() {.lblTitleOndoKanriStart.TextValue + "(" + .lblTitleHokan.TextValue + ")", "4"})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E038", New String() {"温度管理開始月日(保管)", "4"})
                Call Me._ControlV.SetErrorControl(.imdOndoKanriStartHokan, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【温度管理開始月日(運送)】
            '2015.11.02 tsunehira add Start
            '英語化対応
            .imdOndoKanriStartUnso.ItemName = String.Concat(.lblTitleOndoKanriStart.TextValue, "(", .lblTitleUnso.TextValue, ")")
            '2015.11.02 tsunehira add End
            '.imdOndoKanriStartUnso.ItemName = "温度管理開始月日(運送)"
            .imdOndoKanriStartUnso.IsHissuCheck = Not LMM100C.UNSO_KANRI_NASHI.Equals(.cmbHokanKbnUnso.SelectedValue.ToString())
            If MyBase.IsValidateCheck(.imdOndoKanriStartUnso) = False Then
                Call Me._ControlV.SetErrorControl(.imdOndoKanriStartUnso, .tabGoodsMst, .tpgGoods)
                Return False
            End If
            If .imdOndoKanriStartUnso.IsDateFullByteCheck = False Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E038", New String() {.lblTitleOndoKanriStart.TextValue + "(" + .lblTitleUnso.TextValue + ")", "4"})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E038", New String() {"温度管理開始月日(運送)", "4"})
                Call Me._ControlV.SetErrorControl(.imdOndoKanriStartUnso, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【温度管理終了月日(保管)】
            If .imdOndoKanriEndHokan.IsDateFullByteCheck = False Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E038", New String() {.lblTitleOndoKanriEnd.TextValue + "(" + .lblTitleHokan.TextValue + ")", "4"})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E038", New String() {"温度管理終了月日(保管)", "4"})
                Call Me._ControlV.SetErrorControl(.imdOndoKanriEndHokan, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【温度管理終了月日(運送)】
            '2015.11.02 tsunehira add Start
            '英語化対応
            .imdOndoKanriEndUnso.ItemName = String.Concat(.lblTitleOndoKanriEnd.TextValue, "(", .lblTitleUnso.TextValue, ")")
            '2015.11.02 tsunehira add End
            '.imdOndoKanriEndUnso.ItemName = "温度管理終了月日(運送)"
            .imdOndoKanriEndUnso.IsHissuCheck = Not LMM100C.UNSO_KANRI_NASHI.Equals(.cmbHokanKbnUnso.SelectedValue.ToString())
            If MyBase.IsValidateCheck(.imdOndoKanriEndUnso) = False Then
                Call Me._ControlV.SetErrorControl(.imdOndoKanriEndUnso, .tabGoodsMst, .tpgGoods)
                Return False
            End If
            If .imdOndoKanriEndUnso.IsDateFullByteCheck = False Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E038", New String() {.lblTitleOndoKanriEnd.TextValue + "(" + .lblTitleUnso.TextValue + ")", "4"})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E038", New String() {"温度管終了始月日(運送)", "4"})
                Call Me._ControlV.SetErrorControl(.imdOndoKanriEndUnso, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【標準入目】
            If Convert.ToDecimal(.numHyojyunIrime.Value) = 0 Then
                MyBase.ShowMessage("E182", New String() {.lblTitleHyojyunIrime.Text, "0.001"})
                Call Me._ControlV.SetErrorControl(.numHyojyunIrime, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【標準入目単位】
            .cmbHyojyunIrimeTani.ItemName = .lblTitleHyojyunIrimeTani.Text
            .cmbHyojyunIrimeTani.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHyojyunIrimeTani) = False Then
                Call Me._ControlV.SetErrorControl(.cmbHyojyunIrimeTani, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【標準重量KGS】
            If Convert.ToDecimal(.numHyojyunJyuryo.Value) = 0 Then
                MyBase.ShowMessage("E182", New String() { .lblTitleHyojyunJyuryo.Text, "0.001"})
                Call Me._ControlV.SetErrorControl(.numHyojyunJyuryo, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【比重】
            If Convert.ToDecimal(.numHizyu.Value) = 0 Then
                MyBase.ShowMessage("E182", New String() { .lblTitleHizyu.Text, "0.001"})
                Call Me._ControlV.SetErrorControl(.numHizyu, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷主勘定科目コード1】
            .txtCustKanjokamoku1.ItemName = .lblTitleCustKanjokamoku1.Text
            .txtCustKanjokamoku1.IsForbiddenWordsCheck = True
            .txtCustKanjokamoku1.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtCustKanjokamoku1) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustKanjokamoku1, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷主勘定科目コード2】
            .txtCustKanjokamoku2.ItemName = .lblTitleCustKanjokamoku2.Text
            .txtCustKanjokamoku2.IsForbiddenWordsCheck = True
            .txtCustKanjokamoku2.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtCustKanjokamoku2) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustKanjokamoku2, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷主カテゴリ1】
            .txtCustCategory1.ItemName = .lblTitleCustCategory1.Text
            .txtCustCategory1.IsForbiddenWordsCheck = True
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            '.txtCustCategory1.IsByteCheck = 20
            .txtCustCategory1.IsByteCheck = 25
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            If MyBase.IsValidateCheck(.txtCustCategory1) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCategory1, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【荷主カテゴリ2】
            .txtCustCategory2.ItemName = .lblTitleCustCategory2.Text
            .txtCustCategory2.IsForbiddenWordsCheck = True
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            '.txtCustCategory2.IsByteCheck = 20
            .txtCustCategory2.IsByteCheck = 25
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            If MyBase.IsValidateCheck(.txtCustCategory2) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCategory2, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【ロット管理レベル】
            .cmbLotKanriLevel.ItemName = .lblTitleLotKanriLevel.Text
            .cmbLotKanriLevel.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbLotKanriLevel) = False Then
                Call Me._ControlV.SetErrorControl(.cmbLotKanriLevel, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【単価グループコード1】
            .txtTankaGroup.ItemName = .lblTitleTankaGroup.Text
            .txtTankaGroup.IsHissuCheck = True
            .txtTankaGroup.IsForbiddenWordsCheck = True
            .txtTankaGroup.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtTankaGroup) = False Then
                Call Me._ControlV.SetErrorControl(.txtTankaGroup, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【指定納品書区分】
            .cmbSiteiNohinSho.ItemName = .lblTitleSiteiNohinSho.Text
            .cmbSiteiNohinSho.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSiteiNohinSho) = False Then
                Call Me._ControlV.SetErrorControl(.cmbSiteiNohinSho, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【分析表区分】
            .cmbBunsekiHyo.ItemName = .lblTitleBunsekiHyo.Text
            .cmbBunsekiHyo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBunsekiHyo) = False Then
                Call Me._ControlV.SetErrorControl(.cmbBunsekiHyo, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【賞味期限管理】
            .cmbShomiKigenKanri.ItemName = .lblTitleShomiKigenKanri.Text
            .cmbShomiKigenKanri.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbShomiKigenKanri) = False Then
                Call Me._ControlV.SetErrorControl(.cmbShomiKigenKanri, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【製造日管理】
            .cmbSeizobiKanri.ItemName = .lblTitleSeizobiKanri.Text
            .cmbSeizobiKanri.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSeizobiKanri) = False Then
                Call Me._ControlV.SetErrorControl(.cmbSeizobiKanri, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【引き当て注意品】
            .cmbHikiateChuiHin.ItemName = .lblTitleHikiateChuiHin.Text
            .cmbHikiateChuiHin.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHikiateChuiHin) = False Then
                Call Me._ControlV.SetErrorControl(.cmbHikiateChuiHin, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【請求明細書出力】
            .cmbSeikyuMeisaisho.ItemName = .lblTitleSeikyuMeisaisho.Text
            .cmbSeikyuMeisaisho.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSeikyuMeisaisho) = False Then
                Call Me._ControlV.SetErrorControl(.cmbSeikyuMeisaisho, .tabGoodsMst, .tpgGoods)
                Return False
            End If

#If True Then           'ADD 2018/07/17 依頼番号 001540 
            '【運送保険】
            .cmbUnsoHoken.ItemName = .lblTitleUnsoHoken.Text
            .cmbUnsoHoken.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbUnsoHoken) = False Then
                Call Me._ControlV.SetErrorControl(.cmbUnsoHoken, .tabGoodsMst, .tpgGoods)
                Return False
            End If
#End If
#If True Then       'ADD 2018/08/01 依頼番号 2130
            '【幅(m)】
            If Convert.ToDecimal(.numWidth.Value) = 0 Then

                '英語化対応
                MyBase.ShowMessage("E182", New String() {_Frm.lblWidth.TextValue, "0.01"})

                Call Me._ControlV.SetErrorControl(.numWidth, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【奥行(m)】
            If Convert.ToDecimal(.numDepth.Value) = 0 Then

                '英語化対応
                MyBase.ShowMessage("E182", New String() {_Frm.lblDepth.TextValue, "0.01"})

                Call Me._ControlV.SetErrorControl(.numDepth, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【高さ(m)】
            If Convert.ToDecimal(.numHeight.Value) = 0 Then

                '英語化対応
                MyBase.ShowMessage("E182", New String() {_Frm.lblHeight.TextValue, "0.01"})

                Call Me._ControlV.SetErrorControl(.numHeight, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【実容積】
            If Convert.ToDecimal(.numActualVolume.Value) = 0 Then

                '英語化対応
                MyBase.ShowMessage("E182", New String() {_Frm.lblVolume.TextValue, "0.000001"})

                Call Me._ControlV.SetErrorControl(.numActualVolume, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【占有容積】
            If Convert.ToDecimal(.numOccupyVolume.Value) = 0 Then

                '英語化対応
                MyBase.ShowMessage("E182", New String() {_Frm.lblVolumeActual.TextValue, "0.000001"})

                Call Me._ControlV.SetErrorControl(.numOccupyVolume, .tabGoodsMst, .tpgGoods)
                Return False
            End If

#End If

            '******************** 商品明細タブ ********************

            '【バーコード】
            .txtBarkodo.ItemName = .lblTitleBarkodo.Text
            .txtBarkodo.IsForbiddenWordsCheck = True
            .txtBarkodo.IsByteCheck = 16
            If MyBase.IsValidateCheck(.txtBarkodo) = False Then
                Call Me._ControlV.SetErrorControl(.txtBarkodo, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【毒劇区分】
            .cmbDokugeki.ItemName = .lblTitleDokugeki.Text
            .cmbDokugeki.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbDokugeki) = False Then
                Call Me._ControlV.SetErrorControl(.cmbDokugeki, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【高圧ガス区分】
            .cmbKouathugas.ItemName = .lblTitleKouathugas.Text
            .cmbKouathugas.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKouathugas) = False Then
                Call Me._ControlV.SetErrorControl(.cmbKouathugas, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【薬事法区分】
            .cmbYakuziho.ItemName = .lblTitleYakuziho.Text
            .cmbYakuziho.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbYakuziho) = False Then
                Call Me._ControlV.SetErrorControl(.cmbYakuziho, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【消防危険品区分】
            .cmbShobokiken.ItemName = .lblTitleShobokiken.Text
            .cmbShobokiken.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbShobokiken) = False Then
                Call Me._ControlV.SetErrorControl(.cmbShobokiken, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【消防コード】
            .txtShobo.ItemName = .lblTitleShobo.Text
            .txtShobo.IsForbiddenWordsCheck = True
            .txtShobo.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtShobo) = False Then
                Call Me._ControlV.SetErrorControl(.txtShobo, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【UN】
            .txtUn.ItemName = .lblTitleUn.Text
            .txtUn.IsForbiddenWordsCheck = True
            .txtUn.IsByteCheck = 4
            .txtUn.IsMiddleSpace = True
            .txtUn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.txtUn) = False Then
                Call Me._ControlV.SetErrorControl(.txtUn, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【PG】
            .txtPg.ItemName = .lblTitlePg.Text
            .txtPg.IsForbiddenWordsCheck = True
            .txtPg.IsByteCheck = 5
            .txtPg.IsMiddleSpace = True
            .txtPg.IsHissuCheck = True
            If MyBase.IsValidateCheck(.txtPg) = False Then
                Call Me._ControlV.SetErrorControl(.txtPg, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【Class1】
            .lblClass1.ItemName = .lblTitleClass1.Text
            .lblClass1.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.lblClass1) = False Then
                Call Me._ControlV.SetErrorControl(.lblClass1, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【Class2】
            .lblClass2.ItemName = .lblTitleClass2.Text
            .lblClass2.IsForbiddenWordsCheck = True
            .lblClass2.IsByteCheck = 10
            If MyBase.IsValidateCheck(.lblClass2) = False Then
                Call Me._ControlV.SetErrorControl(.lblClass2, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '【Class3】
            .lblClass3.ItemName = .lblTitleClass3.Text
            .lblClass3.IsForbiddenWordsCheck = True
            .lblClass3.IsByteCheck = 3
            If MyBase.IsValidateCheck(.lblClass3) = False Then
                Call Me._ControlV.SetErrorControl(.lblClass3, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '----'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
            '【使用状態
            .cmbAVAL_YN.ItemName = .blTitleAVAL_YN.Text
            .cmbAVAL_YN.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbAVAL_YN) = False Then
                Call Me._ControlV.SetErrorControl(.cmbAVAL_YN, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

            '----

            '******************** フォーマット登録のチェック ********************
            '20150731 常平add
            '【商品コード
            .TxtBoxGoodsCd.ItemName = .lblFormatGoodsCd.Text
            .TxtBoxGoodsCd.IsHankakuCheck = True
            '.TxtBoxGoodsCd.IsSujiCheck = True
            .TxtBoxGoodsCd.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtGoodsCd) = False Then
                Call Me._ControlV.SetErrorControl(.TxtBoxGoodsCd, .tabGoodsMst, .tpgGoods)
                Return False
            End If
            If String.IsNullOrEmpty(.TxtBoxGoodsCd.TextValue) = False AndAlso Me.IsFormatCheck(.TxtBoxGoodsCd.TextValue) = False Then
                Call Me._ControlV.SetErrorControl(.TxtBoxGoodsCd, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【商品名１】
            .TxtBoxGoodsNM1.ItemName = .lblGoodsName1.Text
            .TxtBoxGoodsNM1.IsHankakuCheck = True
            '.TxtBoxGoodsNM1.IsSujiCheck = True
            .TxtBoxGoodsNM1.IsByteCheck = 30
            If MyBase.IsValidateCheck(.TxtBoxGoodsNM1) = False Then
                Call Me._ControlV.SetErrorControl(.TxtBoxGoodsNM1, .tabGoodsMst, .tpgGoods)
                Return False
            End If
            If String.IsNullOrEmpty(.TxtBoxGoodsNM1.TextValue) = False AndAlso Me.IsFormatCheck(.TxtBoxGoodsNM1.TextValue) = False Then
                Call Me._ControlV.SetErrorControl(.TxtBoxGoodsNM1, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【商品名２】
            .TxtBoxGoodsNM2.ItemName = .lblGoodsName2.Text
            .TxtBoxGoodsNM2.IsHankakuCheck = True
            '.TxtBoxGoodsNM2.IsSujiCheck = True
            .TxtBoxGoodsNM2.IsByteCheck = 30
            If MyBase.IsValidateCheck(.TxtBoxGoodsNM2) = False Then
                Call Me._ControlV.SetErrorControl(.TxtBoxGoodsNM2, .tabGoodsMst, .tpgGoods)
                Return False
            End If
            If String.IsNullOrEmpty(.TxtBoxGoodsNM2.TextValue) = False AndAlso Me.IsFormatCheck(.TxtBoxGoodsNM2.TextValue) = False Then
                Call Me._ControlV.SetErrorControl(.TxtBoxGoodsNM2, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【入目】
            .TxtBoxIrime.ItemName = .lblIrime.Text
            .TxtBoxIrime.IsHankakuCheck = True
            '.TxtBoxIrime.IsSujiCheck = True
            .TxtBoxIrime.IsByteCheck = 30
            If MyBase.IsValidateCheck(.TxtBoxIrime) = False Then
                Call Me._ControlV.SetErrorControl(.TxtBoxIrime, .tabGoodsMst, .tpgGoods)
                Return False
            End If
            If String.IsNullOrEmpty(.TxtBoxIrime.TextValue) = False AndAlso Me.IsFormatCheck(.TxtBoxIrime.TextValue) = False Then
                Call Me._ControlV.SetErrorControl(.TxtBoxIrime, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprGoodsDetail)

            '2017/09/25 修正 李↓ -- Jp.Co.Nrs.LM.Utility参照追加による修正
            Dim spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread = .sprGoodsDetail
            '2017/09/25 修正 李↑

            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max


                '【用途区分】
                vCell.SetValidateCell(i, LMM100G.sprGoodsDtlDef.YOTO_KBN.ColNo)
                vCell.ItemName = LMM100G.sprGoodsDtlDef.YOTO_KBN.ColName
                vCell.IsHissuCheck = True

                If MyBase.IsValidateCheck(vCell) = False Then
                    Call Me._ControlV.SetErrorControl(spr _
                                                      , i _
                                                      , LMM100G.sprGoodsDtlDef.YOTO_KBN.ColNo _
                                                      , .tabGoodsMst _
                                                      , .tpgGoodsDetail)
                    Return False
                End If

                '【設定値】
                vCell.SetValidateCell(i, LMM100G.sprGoodsDtlDef.SETTEI_VALUE.ColNo)
                vCell.ItemName = LMM100G.sprGoodsDtlDef.SETTEI_VALUE.ColName
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 100
                If MyBase.IsValidateCheck(vCell) = False Then
                    Call Me._ControlV.SetErrorControl(spr _
                                                      , i _
                                                      , LMM100G.sprGoodsDtlDef.SETTEI_VALUE.ColNo _
                                                      , .tabGoodsMst _
                                                      , .tpgGoodsDetail)
                    Return False
                End If

                '【備考】
                vCell.SetValidateCell(i, LMM100G.sprGoodsDtlDef.BIKO.ColNo)
                vCell.ItemName = LMM100G.sprGoodsDtlDef.BIKO.ColName
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 100
                If MyBase.IsValidateCheck(vCell) = False Then
                    Call Me._ControlV.SetErrorControl(spr _
                                  , i _
                                  , LMM100G.sprGoodsDtlDef.BIKO.ColNo _
                                  , .tabGoodsMst _
                                  , .tpgGoodsDetail)

                    Return False
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時単項目チェック(フォーマット用)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFormatCheck(Value As String) As Boolean
        Dim i As Integer

        If Len(Value) = 0 Then Exit Function

        For i = 1 To Len(Value)

            Select Case Mid(Value, i, 1)
                '記号
                Case "-", "/", "(", ")", "_", "&", "!", "$", "@", "?", ".", "#", "*", "\"
                    '半角大文字
                Case "A" To "Z"
                    '半角数字
                Case "0" To "9"

                Case Else

                    Exit Function
            End Select
        Next i

        IsFormatCheck = True
    End Function




    ''' <summary>
    ''' 保存押下時マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveExistMst(ByVal dateStr As String) As Boolean

        Dim ExistChkDr As DataRow() = Nothing
        Dim ctl As Control() = Nothing
        Dim focus As Control = Nothing

        With Me._Frm

            '******************** 商品タブ ********************

            '【荷主コードマスタ存在チェック】
            .lblCustNmL.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .lblCustNmS.TextValue = String.Empty
            .lblCustNmSS.TextValue = String.Empty
            If Me._ControlV.SelectCustListDataRow(ExistChkDr _
                                              , .txtCustCdL.TextValue _
                                              , .txtCustCdM.TextValue _
                                              , .txtCustCdS.TextValue _
                                              , .txtCustCdSS.TextValue _
                                              , LMMControlC.CustMsgType.CUST_SS) = False Then
                If .lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
                    ctl = New Control() {.txtCustCdS, .txtCustCdSS}
                    focus = .txtCustCdS
                Else
                    ctl = New Control() {.txtCustCdL, .txtCustCdM, .txtCustCdS, .txtCustCdSS}
                    focus = .txtCustCdL
                End If

                Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                Return False
            Else
                .txtCustCdL.TextValue = ExistChkDr(0).Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = ExistChkDr(0).Item("CUST_CD_M").ToString()
                .txtCustCdS.TextValue = ExistChkDr(0).Item("CUST_CD_S").ToString()
                .txtCustCdSS.TextValue = ExistChkDr(0).Item("CUST_CD_SS").ToString()
                .lblCustNmL.TextValue = ExistChkDr(0).Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = ExistChkDr(0).Item("CUST_NM_M").ToString()
                .lblCustNmS.TextValue = ExistChkDr(0).Item("CUST_NM_S").ToString()
                .lblCustNmSS.TextValue = ExistChkDr(0).Item("CUST_NM_SS").ToString()
            End If

            '【荷送人コード】
            .lblNisonin.TextValue = String.Empty
            If String.IsNullOrEmpty(.txtNisonin.TextValue) = False Then
                If Me._ControlV.SelectDestListDataRow(ExistChkDr _
                                  , .cmbBr.SelectedValue.ToString() _
                                  , .txtCustCdL.TextValue _
                                  , .txtNisonin.TextValue) = False Then
                    Call Me._ControlV.SetErrorControl(.txtNisonin, .tabGoodsMst, .tpgGoods)
                    Return False
                Else
                    .txtNisonin.TextValue = ExistChkDr(0).Item("DEST_CD").ToString()
                    .lblNisonin.TextValue = ExistChkDr(0).Item("DEST_NM").ToString()
                End If
            End If

            '【入荷時加工作業コード1】
            If Me.IsExistSagyoKmkM(.txtNyukaSagyoKbn1) = False Then
                Return False
            End If

            '【入荷時加工作業コード2】
            If Me.IsExistSagyoKmkM(.txtNyukaSagyoKbn2) = False Then
                Return False
            End If

            '【入荷時加工作業コード3】
            If Me.IsExistSagyoKmkM(.txtNyukaSagyoKbn3) = False Then
                Return False
            End If

            '【入荷時加工作業コード4】
            If Me.IsExistSagyoKmkM(.txtNyukaSagyoKbn4) = False Then
                Return False
            End If

            '【入荷時加工作業コード5】
            If Me.IsExistSagyoKmkM(.txtNyukaSagyoKbn5) = False Then
                Return False
            End If

            '【出荷時加工作業コード1】
            If Me.IsExistSagyoKmkM(.txtShukkaSagyoKbn1) = False Then
                Return False
            End If

            '【出荷時加工作業コード2】
            If Me.IsExistSagyoKmkM(.txtShukkaSagyoKbn2) = False Then
                Return False
            End If

            '【出荷時加工作業コード3】
            If Me.IsExistSagyoKmkM(.txtShukkaSagyoKbn3) = False Then
                Return False
            End If

            '【出荷時加工作業コード4】
            If Me.IsExistSagyoKmkM(.txtShukkaSagyoKbn4) = False Then
                Return False
            End If

            '【出荷時加工作業コード5】
            If Me.IsExistSagyoKmkM(.txtShukkaSagyoKbn5) = False Then
                Return False
            End If

            '【端数出荷時加工作業コード1】
            If Me.IsExistSagyoKmkM(.txtHasuSagyoKbn1) = False Then
                Return False
            End If

            '【端数出荷時加工作業コード2】
            If Me.IsExistSagyoKmkM(.txtHasuSagyoKbn2) = False Then
                Return False
            End If

            '【端数出荷時加工作業コード3】
            If Me.IsExistSagyoKmkM(.txtHasuSagyoKbn3) = False Then
                Return False
            End If

            '【梱包作業コード】
            .lblKonpoSagyoCd.TextValue = String.Empty
            If Me.IsExistSagyoKmkM(.txtKonpoSagyoCd, ExistChkDr) = False Then
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    .lblKonpoSagyoCd.TextValue = ExistChkDr(0).Item("SAGYO_NM").ToString()
                End If
            End If


            '【単価グループコード名称取得】
            Call Me.GetTankaMData(dateStr)

            '******************** 商品明細タブ ********************

            '【消防コード】
            .lblShobo.TextValue = String.Empty
            If String.IsNullOrEmpty(.txtShobo.TextValue) = False Then
                If Me._ControlV.SelectShoboListDataRow(ExistChkDr, .txtShobo.TextValue) = False Then
                    Call Me._ControlV.SetErrorControl(.txtShobo, .tabGoodsMst, .tpgGoodsDetail)
                    Return False
                Else
                    Dim shoboInfo As String = String.Empty
                    shoboInfo = String.Concat(shoboInfo, ExistChkDr(0).Item("RUI_NM").ToString())
                    shoboInfo = String.Concat(shoboInfo, " ", ExistChkDr(0).Item("HINMEI").ToString())
                    shoboInfo = String.Concat(shoboInfo, " ", ExistChkDr(0).Item("SEISITSU").ToString())
                    shoboInfo = String.Concat(shoboInfo, " ", ExistChkDr(0).Item("SYU_NM").ToString())

                    .txtShobo.TextValue = ExistChkDr(0).Item("SHOBO_CD").ToString()
                    .lblShobo.TextValue = shoboInfo
                End If
            End If

            '【UN、PG】
            .lblClass1.TextValue = String.Empty
            .lblClass2.TextValue = String.Empty
            .lblClass3.TextValue = String.Empty
            .lblKaiyouosen.TextValue = String.Empty
            .txtKaiyouosen.TextValue = String.Empty

            'UNマスタ存在チェック
            Dim CntChkOnlyFlg As String = "1"
            Dim CntChkResult As Integer = 0

            Call DirectCast(Me.MyHandler, LMM100H).SetReturnUNPop(CntChkOnlyFlg, LMM100C.EventShubetsu.ENTER, CntChkResult)

            'マスタチェックで存在しない場合、エラー
            If CntChkResult <> 1 Then
                ctl = New Control() { .txtUn, .txtPg}
                focus = .txtUn
                Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoodsDetail)
                Call Me._ControlV.SetMstErrMessage("UNマスタ", String.Concat(.txtUn.TextValue, " - ", .txtPg.TextValue))
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 作業項目マスタの存在チェックを行う
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExistSagyoKmkM(ByVal ctl As Win.InputMan.LMImTextBox _
                                      , Optional ByRef ExistChkDr As DataRow() = Nothing) As Boolean

        ExistChkDr = Nothing

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim custCdL As String = .txtCustCdL.TextValue.ToString()
            Dim chkValue As String = ctl.TextValue

            If String.IsNullOrEmpty(chkValue) = False Then
                'START YANAI 要望番号376
                'If Me._ControlV.SelectSagyoListDataRow(ExistChkDr, chkValue, custCdL, brCd) = False Then
                Dim SelectSagyoString As String = String.Empty
                '削除フラグ
                SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
                '作業コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", chkValue, "' ")
                '営業所コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", brCd, "' ")
                '荷主コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custCdL, "' OR CUST_CD_L = 'ZZZZZ')")

                Dim sagyoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
                If sagyoDr.Length = 0 Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.ShowMessage("E826", New String() {chkValue})
                    '20151029 tsunehira add End
                    'MyBase.ShowMessage("E079", New String() {"作業項目マスタ", chkValue})
                    'END YANAI 要望番号376
                    Call Me._ControlV.SetErrorControl(ctl, .tabGoodsMst, .tpgGoods)
                    Return False
                Else
                    'START YANAI 要望番号376
                    'ctl.TextValue = ExistChkDr(0).Item("SAGYO_CD").ToString()
                    ctl.TextValue = sagyoDr(0).Item("SAGYO_CD").ToString()
                    'END YANAI 要望番号376
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    '''  単価マスタの名称取得を行う(サーバーで存在チェックを行うので、キャッシュでは行わない)
    ''' </summary>
    ''' <param name="dateStr">システム日付</param>
    ''' <remarks></remarks>
    Private Sub GetTankaMData(ByVal dateStr As String)

        With Me._Frm

            .lblTekiyoStartDate.TextValue = String.Empty
            .numHokanTujo.TextValue = String.Empty
            .cmbHokanTujo.SelectedValue = String.Empty
            .numHokanTeion.TextValue = String.Empty
            .cmbHokanTeion.SelectedValue = String.Empty
            .numNiyakuNyuko.TextValue = String.Empty
            .cmbNiyakuNyuko.SelectedValue = String.Empty
            .numNiyakuShukko.TextValue = String.Empty
            .cmbNiyakuShukko.SelectedValue = String.Empty
            .numNiyakuMinHosho.TextValue = String.Empty

            Dim ExistChkDr As DataRow() = Nothing

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim custCdL As String = .txtCustCdL.TextValue.ToString()
            Dim custCdM As String = .txtCustCdM.TextValue.ToString()
            Dim chkValue As String = .txtTankaGroup.TextValue

            If String.IsNullOrEmpty(chkValue) = False Then
                ExistChkDr = Me._ControlV.SelectTankaListDataRow(brCd, custCdL, custCdM, chkValue, dateStr)
                If ExistChkDr IsNot Nothing _
                AndAlso ExistChkDr.Length > 0 Then
                    .txtTankaGroup.TextValue = ExistChkDr(0).Item("UP_GP_CD_1").ToString()
                    .lblTekiyoStartDate.TextValue = ExistChkDr(0).Item("STR_DATE").ToString()
                    .numHokanTujo.Value = ExistChkDr(0).Item("STORAGE_1").ToString()
                    .cmbHokanTujo.SelectedValue = ExistChkDr(0).Item("STORAGE_KB1").ToString()
                    .numHokanTeion.Value = ExistChkDr(0).Item("STORAGE_2").ToString()
                    .cmbHokanTeion.SelectedValue = ExistChkDr(0).Item("STORAGE_KB2").ToString()
                    .numNiyakuNyuko.Value = ExistChkDr(0).Item("HANDLING_IN").ToString()
                    .cmbNiyakuNyuko.SelectedValue = ExistChkDr(0).Item("HANDLING_IN_KB").ToString()
                    .numNiyakuShukko.Value = ExistChkDr(0).Item("HANDLING_OUT").ToString()
                    .cmbNiyakuShukko.SelectedValue = ExistChkDr(0).Item("HANDLING_OUT_KB").ToString()
                    .numNiyakuMinHosho.Value = ExistChkDr(0).Item("MINI_TEKI_IN_AMO").ToString()
                End If
            End If

        End With

    End Sub

    ''' <summary>
    ''' 保存押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveRelationChk(ByVal zaikoFlg As Boolean, ByVal sumPoraZaiNb As Integer) As Boolean  'Add 2019/07/31 要望管理006855
        ''Private Function IsSaveRelationChk(ByVal zaikoFlg As Boolean) As Boolean                           'Del 2019/07/31 要望管理006855

        Dim ctl As Control() = Nothing
        Dim focus As Control = Nothing

        With Me._Frm

            '******************** エラーチェック ********************

            '【温度管理区分(整合性チェック】
            focus = .cmbHokanKbnHokan
            If .cmbHokanKbnHokan.SelectedValue.Equals(LMM100C.ONDO_KANRI_JOON) = False Then
                If String.IsNullOrEmpty(.imdOndoKanriStartHokan.TextValue) _
                OrElse String.IsNullOrEmpty(.imdOndoKanriEndHokan.TextValue) Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.ShowMessage("E818")
                    '20151029 tsunehira add End
                    'MyBase.ShowMessage("E187", New String() {"温度管理区分が定温", "温度管理開始年月日と温度管理終了日"})
                    ctl = New Control() {.cmbHokanKbnHokan, .imdOndoKanriStartHokan, .imdOndoKanriEndHokan}
                    Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                    Return False
                End If
            End If

            '【温度管理上限・下限(大小チェック】
            If Convert.ToInt32(.numOndoKanriMax.Value) < Convert.ToInt32(.numOndoKanriMin.Value) Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E182", New String() {.lblTitleOndoKanriMax.TextValue, .lblTitleOndoKanriMin.TextValue})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E182", New String() {"管理温度上限", "管理温度下限"})
                ctl = New Control() {.numOndoKanriMax, .numOndoKanriMin}
                focus = .numOndoKanriMax
                Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                Return False
            End If

            '【分析表、ロット管理レベル(整合性チェック】
            If .cmbBunsekiHyo.SelectedValue.Equals(LMMControlC.FLG_ON) _
            AndAlso .cmbLotKanriLevel.SelectedValue.Equals(LMMControlC.FLG_OFF) Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E819")
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E187", New String() {"分析票が有", "ロット管理レベルは無以外"})
                ctl = New Control() {.cmbLotKanriLevel, .cmbBunsekiHyo}
                focus = .cmbLotKanriLevel
                Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)

                Return False

            End If

            'START YANAI 要望番号713
            '【デュポン専用チェック】
            Dim custDetailsDr() As DataRow = Nothing
            Dim kbnDr() As DataRow = Nothing
            '【ＦＲＢコードチェック】
            '要望番号:1253 terakawa 2012.07.13 Start
            'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
            '                                                                                                "CUST_CD = '", .txtCustCdL.TextValue, "' AND ", _
            '                                                                                                "SUB_KB = '15'" _
            '                                                                                                ) _
            '                                                                                 )
            custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbBr.SelectedValue, "' AND ", _
                                                                                                "CUST_CD = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                "SUB_KB = '15'" _
                                                                                                ) _
                                                                                 )
            '要望番号:1253 terakawa 2012.07.13 End
            If 0 < custDetailsDr.Length Then
                If ("01").Equals(custDetailsDr(0).Item("SET_NAIYO").ToString) = True Then
                    '荷主勘定科目コード１文字数チェック
                    If Len(.txtCustKanjokamoku1.TextValue) > 7 Then
                        '20151029 tsunehira add Start
                        '英語化対応
                        MyBase.ShowMessage("E023", New String() {.lblTitleCustKanjokamoku1.TextValue, "7"})
                        '20151029 tsunehira add End
                        'MyBase.ShowMessage("E023", New String() {"荷主勘定科目コード１", "7"})
                        ctl = New Control() {.txtCustKanjokamoku1}
                        focus = .txtCustKanjokamoku1
                        Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                        Return False
                    End If

                    '荷主カテゴリ１文字数チェック
                    If Len(.txtCustCategory1.TextValue) > 7 Then
                        '20151029 tsunehira add Start
                        '英語化対応
                        MyBase.ShowMessage("E023", New String() {.lblTitleCustCategory1.TextValue, "7"})
                        '20151029 tsunehira add End
                        'MyBase.ShowMessage("E023", New String() {"荷主カテゴリ１", "7"})
                        ctl = New Control() {.txtCustCategory1}
                        focus = .txtCustCategory1
                        Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                        Return False
                    End If

                    '荷主勘定科目コード１、荷主カテゴリ１相違チェック
                    If (.txtCustKanjokamoku1.TextValue).Equals(.txtCustCategory1.TextValue) = False Then
                        '20151029 tsunehira add Start
                        '英語化対応
                        MyBase.ShowMessage("E216", New String() {.lblTitleCustKanjokamoku1.TextValue, .lblTitleCustCategory1.TextValue})
                        '20151029 tsunehira add End
                        'MyBase.ShowMessage("E216", New String() {"荷主勘定科目コード１", "荷主カテゴリ１"})
                        ctl = New Control() {.txtCustKanjokamoku1, .txtCustCategory1}
                        focus = .txtCustKanjokamoku1
                        Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                        Return False
                    End If
                End If
            End If

            '【ＳＲＣコードチェック】
            '要望番号:1253 terakawa 2012.07.13 Start
            'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
            '                                                                                                "CUST_CD = '", .txtCustCdL.TextValue, "' AND ", _
            '                                                                                                "SUB_KB = '16'" _
            '                                                                                                ) _
            '                                                                                 )
            custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbBr.SelectedValue, "' AND ", _
                                                                                                            "CUST_CD = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                            "SUB_KB = '16'" _
                                                                                                            ) _
                                                                                             )
            '要望番号:1253 terakawa 2012.07.13 End
            If 0 < custDetailsDr.Length AndAlso _
                String.IsNullOrEmpty(.txtCustKanjokamoku2.TextValue) = False Then
                If ("01").Equals(custDetailsDr(0).Item("SET_NAIYO").ToString) = True Then
                    kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat( _
                                                                                                   "KBN_GROUP_CD = 'S028' AND ", _
                                                                                                   "KBN_CD = '", .txtCustKanjokamoku2.TextValue, "'" _
                                                                                                  ) _
                                                                                    )

                    '区分マスタチェック
                    If kbnDr.Length = 0 Then
                        '20151029 tsunehira add Start
                        '英語化対応
                        MyBase.ShowMessage("E820")
                        '20151029 tsunehira add End
                        'MyBase.ShowMessage("E079", New String() {"区分マスタ", "荷主勘定科目コード２"})
                        ctl = New Control() {.txtCustKanjokamoku2}
                        focus = .txtCustKanjokamoku2
                        Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                        Return False
                    End If

                    '荷主カテゴリ２設定値チェック
                    If (kbnDr(0).Item("KBN_NM2").ToString).Equals(.txtCustCategory2.TextValue) = False Then
                        '20151029 tsunehira add Start
                        '英語化対応
                        MyBase.ShowMessage("E821")
                        '20151029 tsunehira add End
                        'MyBase.ShowMessage("E217", New String() {"荷主カテゴリ２", "区分マスタ"})
                        ctl = New Control() {.txtCustCategory2}
                        focus = .txtCustCategory2
                        Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                        Return False
                    End If

                End If
            End If
            'END YANAI 要望番号713

            'START KIM 要望番号1518 2012/10/18
            If Me.ExistGoodsDetail(Me._Frm.sprGoodsDetail, Me._Frm.tpgGoodsDetail) = False Then
                Return False
            End If
            'END KIM 要望番号1518 2012/10/18

            '要望対応1995 端数出荷時作業区分対応

            '入数が1で端数出荷時作業区分が設定されているか
            If .numIrisu.TextValue = "1" Then
                Dim sagyoHasuTxtArray As Control() = New Control() {.txtHasuSagyoKbn1, .txtHasuSagyoKbn2, .txtHasuSagyoKbn3}
                For Each sagyoHasuTxt As Control In sagyoHasuTxtArray
                    If Not String.IsNullOrEmpty(DirectCast(sagyoHasuTxt, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue) Then
                        MyBase.ShowMessage("E983")
                        ctl = sagyoHasuTxtArray
                        focus = sagyoHasuTxt
                        Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                        Return False
                    End If
                Next
            End If

            Dim sagyoCnt As Integer = 0
            Dim sagyoCtl(7) As Control
            '出荷時加工作業区分、端数出荷時作業区分が5件を超えて設定されているか
            If Not String.IsNullOrEmpty(.txtShukkaSagyoKbn1.TextValue) Then
                sagyoCtl(sagyoCnt) = .txtShukkaSagyoKbn1
                sagyoCnt = sagyoCnt + 1
            End If
            If Not String.IsNullOrEmpty(.txtShukkaSagyoKbn2.TextValue) Then
                sagyoCtl(sagyoCnt) = .txtShukkaSagyoKbn2
                sagyoCnt = sagyoCnt + 1
            End If
            If Not String.IsNullOrEmpty(.txtShukkaSagyoKbn3.TextValue) Then
                sagyoCtl(sagyoCnt) = .txtShukkaSagyoKbn3
                sagyoCnt = sagyoCnt + 1
            End If
            If Not String.IsNullOrEmpty(.txtShukkaSagyoKbn4.TextValue) Then
                sagyoCtl(sagyoCnt) = .txtShukkaSagyoKbn4
                sagyoCnt = sagyoCnt + 1
            End If
            If Not String.IsNullOrEmpty(.txtShukkaSagyoKbn5.TextValue) Then
                sagyoCtl(sagyoCnt) = .txtShukkaSagyoKbn5
                sagyoCnt = sagyoCnt + 1
            End If
            If Not String.IsNullOrEmpty(.txtHasuSagyoKbn1.TextValue) Then
                sagyoCtl(sagyoCnt) = .txtHasuSagyoKbn1
                sagyoCnt = sagyoCnt + 1
            End If
            If Not String.IsNullOrEmpty(.txtHasuSagyoKbn2.TextValue) Then
                sagyoCtl(sagyoCnt) = .txtHasuSagyoKbn2
                sagyoCnt = sagyoCnt + 1
            End If
            If Not String.IsNullOrEmpty(.txtHasuSagyoKbn3.TextValue) Then
                sagyoCtl(sagyoCnt) = .txtHasuSagyoKbn3
                sagyoCnt = sagyoCnt + 1

            End If

            If sagyoCnt > 5 Then
                MyBase.ShowMessage("E982")
                ctl = sagyoCtl
                focus = .txtHasuSagyoKbn1
                Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                Return False
            End If


            'START YANAI 要望番号1025
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat( _
                                                                                           "KBN_GROUP_CD = 'I001' AND ", _
                                                                                           "KBN_CD = '", .cmbHyojyunIrimeTani.SelectedValue, "'" _
                                                                                          ) _
                                                                            )
            '******************** ワーニングチェック ********************

            '要望番号:1109 yamanaka 2012.09.28 Start
            If 0 < kbnDr.Length Then
                '標準入目 + 標準入目単位 + 標準重量KGS
                If ("1").Equals(kbnDr(0).Item("KBN_NM2").ToString) = True AndAlso _
                    (.numHyojyunIrime.Value).Equals(.numHyojyunJyuryo.Value) = False Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    If MyBase.ShowMessage("W255", New String() {kbnDr(0).Item("KBN_NM1").ToString()}) = MsgBoxResult.Ok Then
                        '20151029 tsunehira add End
                        'If MyBase.ShowMessage("W219", New String() {String.Concat("標準入目単位が", kbnDr(0).Item("KBN_NM1").ToString()), "標準入目と標準重量KGSは異なる値"}) = MsgBoxResult.Ok Then
                        '処理なし
                    Else
                        .numHyojyunJyuryo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        ctl = New Control() {.numHyojyunIrime}
                        focus = .numHyojyunIrime
                        Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                        Me.SetBaseMsg() '基本メッセージの設定
                        Return False
                    End If
                ElseIf ("2").Equals(kbnDr(0).Item("KBN_NM2").ToString) = True AndAlso _
                        (.numHyojyunIrime.Value).Equals(.numHyojyunJyuryo.Value) = True Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    If MyBase.ShowMessage("W256", New String() {kbnDr(0).Item("KBN_NM1").ToString()}) = MsgBoxResult.Ok Then
                        '20151029 tsunehira add End
                        'If MyBase.ShowMessage("W219", New String() {String.Concat("標準入目単位が", kbnDr(0).Item("KBN_NM1").ToString()), "標準入目と標準重量KGSは同一の値"}) = MsgBoxResult.Ok Then
                        '処理なし
                    Else
                        .numHyojyunJyuryo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        ctl = New Control() {.numHyojyunIrime}
                        focus = .numHyojyunIrime
                        Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                        Me.SetBaseMsg() '基本メッセージの設定
                        Return False
                    End If
                End If
            End If
            '要望番号:1109 yamanaka 2012.09.28 End
            'END YANAI 要望番号1025


            '【入数、荷札印刷枚数(同一チェック】
            If Convert.ToInt32(.numIrisu.Value) <> Convert.ToInt32(.numNihudaInsatu.Value) Then
                '20151029 tsunehira add Start
                '英語化対応
                If MyBase.ShowMessage("W121", New String() {.lblTitleIrisu.TextValue, .lblTitleNihudaInsatu.TextValue}) = MsgBoxResult.Ok Then
                    '20151029 tsunehira add End
                    'If MyBase.ShowMessage("W121", New String() {"入数", "荷札印刷枚数"}) = MsgBoxResult.Ok Then
                    '(2012.10.24)要望番号1500 コメント
                    'Return True
                Else
                    If .lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) _
                    AndAlso zaikoFlg = True Then
                        ctl = New Control() {.numNihudaInsatu}
                        focus = .numNihudaInsatu
                    Else
                        ctl = New Control() {.numIrisu, .numNihudaInsatu}
                        focus = .numIrisu
                    End If
                    Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                    Me.SetBaseMsg() '基本メッセージの設定
                    Return False
                End If
            End If

            'ShinodaStart 要望番号:2103
            Dim kbnDr2() As DataRow = Nothing
            kbnDr2 = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                                           "NRS_BR_CD = '", .cmbBr.SelectedValue.ToString, "' AND ", _
                                                                                           "CUST_CD = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                           "SUB_KB = '60' AND SET_NAIYO = '1' " _
                                                                                          ) _
                                                                            )


            If kbnDr2.Length > 0 Then
                Dim yotoKbn As String = String.Empty
                Dim setteiValue As String = String.Empty
                Dim rtnFlg As Boolean = False

                '営業所コード＝"20"（大阪）で、荷主コード（大）='00856'（浮間合成）の場合チェック
                For i As Integer = 0 To .sprGoodsDetail.Sheets(0).Rows.Count - 1
                    yotoKbn = Me._ControlV.GetCellValue(.sprGoodsDetail.Sheets(0).Cells(i, LMM100C.SprGoodsDetailColumnIndex.YOTO_KBN))
                    setteiValue = Me._ControlV.GetCellValue(.sprGoodsDetail.Sheets(0).Cells(i, LMM100C.SprGoodsDetailColumnIndex.SETTEI_VALUE))
                    If yotoKbn.Equals("10") = True AndAlso String.IsNullOrEmpty(setteiValue) = False Then
                        rtnFlg = True
                    End If
                Next

                If rtnFlg = False Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    If MyBase.ShowMessage("E822") <> MsgBoxResult.Ok Then
                        '20151029 tsunehira add End
                        'If MyBase.ShowMessage("E019", New String() {"在庫残数集計区分"}) <> MsgBoxResult.Ok Then
                        Return False
                    End If
                End If
            End If

            'ShinodaEnd　要望番号:2103

#If True Then   'ADD 2020/08/07 014005   【LMS】商品マスタ_入荷仮置場機能の追加
            '荷主詳細 A1(入荷商品詳細置場情報設定)ある場合だけ
            Dim TOU As String = String.Empty
            Dim SITU As String = String.Empty
            Dim ZONE As String = String.Empty
            Dim LOCA As String = String.Empty
            ''DEL 2020/08/12 荷主詳細チェックをやめる
            ''Dim kbnDr3() As DataRow = Nothing
            ''kbnDr3 = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat(
            ''                                                                               "NRS_BR_CD = '", .cmbBr.SelectedValue.ToString, "' AND ",
            ''                                                                               "CUST_CD = '", .txtCustCdL.TextValue & .txtCustCdM.TextValue, "' AND ",
            ''                                                                               "SUB_KB = 'A1' AND SET_NAIYO = '1' "
            ''                                                                              )
            ''                                                                )
            ''If kbnDr3.Length > 0 Then
            Dim yotoKbn2 As String = String.Empty
            Dim setteiValue2 As String = String.Empty
            Dim rtnFlg2 As Boolean = False

            '商品詳細　02 在庫情報設定の場合チェック
            For i As Integer = 0 To .sprGoodsDetail.Sheets(0).Rows.Count - 1
                yotoKbn2 = Me._ControlV.GetCellValue(.sprGoodsDetail.Sheets(0).Cells(i, LMM100C.SprGoodsDetailColumnIndex.YOTO_KBN))
                setteiValue2 = Me._ControlV.GetCellValue(.sprGoodsDetail.Sheets(0).Cells(i, LMM100C.SprGoodsDetailColumnIndex.SETTEI_VALUE))
                If yotoKbn2.Equals("02") = True AndAlso String.IsNullOrEmpty(setteiValue2) = False Then
                    Dim getSET_NAINOleng As Integer = setteiValue2.Length

                    setteiValue2 = String.Concat(setteiValue2, Space(17))
                    TOU = setteiValue2.Substring(0, 2)
                    SITU = setteiValue2.Substring(2, 2)
                    ZONE = setteiValue2.Substring(4, 2)
                    LOCA = setteiValue2.Substring(6, 10)

                    TOU = RTrim(TOU)
                    SITU = RTrim(SITU)
                    ZONE = RTrim(ZONE)
                    LOCA = RTrim(LOCA)

                    If getSET_NAINOleng >= 1 Then
                        '棟チェック
                        Dim ToDr() As DataRow = Nothing
                        ToDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU).Select(String.Concat(
                                                                                           "NRS_BR_CD = '", .cmbBr.SelectedValue.ToString, "' AND ",
                                                                                           "TOU_NO = '", TOU.ToString, "'"
                                                                                          )
                                                                            )

                        If ToDr.Length = 0 Then
                            MyBase.ShowMessage("E078", New String() {"置場情報の設定値が棟室マスタ"})

                            '.cmbUnsoHoken.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            ''ctl = New Control() { .sprGoodsDetail}
                            ''focus = .sprGoodsDetail
                            ''Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoodsDetail)
                            Call Me._ControlV.SetErrorControl(.sprGoodsDetail, New Integer() {i, i} _
                                                              , New Integer() {3, 3} _
                                                              , Me._Frm.tabGoodsMst, .tpgGoodsDetail)
                            Return False
                        End If

                        If getSET_NAINOleng >= 3 Then

                            '棟室チェック
                            Dim ToSituDr() As DataRow = Nothing
                            ToSituDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU).Select(String.Concat(
                                                                                               "NRS_BR_CD = '", .cmbBr.SelectedValue.ToString, "' AND ",
                                                                                               "TOU_NO = '", TOU.ToString, "' AND ",
                                                                                               "SITU_NO = '", SITU.ToString, "'"
                                                                                              )
                                                                                )

                            If ToSituDr.Length = 0 Then
                                MyBase.ShowMessage("E078", New String() {"置場情報の設定値が棟室マスタ"})

                                '.cmbUnsoHoken.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                                ''ctl = New Control() { .sprGoodsDetail}
                                ''focus = .sprGoodsDetail
                                ''Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoodsDetail)
                                Call Me._ControlV.SetErrorControl(.sprGoodsDetail, New Integer() {i, i} _
                                                                  , New Integer() {3, 3} _
                                                                  , Me._Frm.tabGoodsMst, .tpgGoodsDetail)
                                Return False
                            End If
                        End If

                        If getSET_NAINOleng >= 5 Then
                            'ゾーンチェック
                            Dim tszoneDr() As DataRow = Nothing
                            tszoneDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU_ZONE).Select(String.Concat(
                                                                                               "NRS_BR_CD = '", .cmbBr.SelectedValue.ToString, "' AND ",
                                                                                               "TOU_NO = '", TOU.ToString, "' AND ",
                                                                                               "SITU_NO = '", SITU.ToString, "' AND ",
                                                                                               "ZONE_CD = '", ZONE.ToString, "'"
                                                                                              )
                                                                                )

                            If tszoneDr.Length = 0 Then
                                MyBase.ShowMessage("E078", New String() {"置場情報の設定値がZONEマスタ"})

                                '.cmbUnsoHoken.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                                ''ctl = New Control() { .sprGoodsDetail}
                                ''focus = .sprGoodsDetail
                                ''Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoodsDetail)
                                Call Me._ControlV.SetErrorControl(.sprGoodsDetail, New Integer() {i, i} _
                                                              , New Integer() {3, 3} _
                                                              , Me._Frm.tabGoodsMst, .tpgGoodsDetail)
                                Return False

                            End If
                        End If
                    End If

                End If

                rtnFlg2 = True
            Next

            ''DEL 2020/08/12 荷主詳細チェックをやめる            End If

#End If

            '要望番号:1500 KIM 2012.10.09 START
            '【在庫残数集計区分】
            'If .cmbBr.SelectedValue.Equals("20") = True AndAlso .txtCustCdL.TextValue.Equals("00856") = True Then

            '    Dim yotoKbn As String = String.Empty
            '    Dim setteiValue As String = String.Empty
            '    Dim rtnFlg As Boolean = False

            '    '営業所コード＝"20"（大阪）で、荷主コード（大）='00856'（浮間合成）の場合チェック
            '    For i As Integer = 0 To .sprGoodsDetail.Sheets(0).Rows.Count - 1
            '        yotoKbn = Me._ControlV.GetCellValue(.sprGoodsDetail.Sheets(0).Cells(i, LMM100C.SprGoodsDetailColumnIndex.YOTO_KBN))
            '        setteiValue = Me._ControlV.GetCellValue(.sprGoodsDetail.Sheets(0).Cells(i, LMM100C.SprGoodsDetailColumnIndex.SETTEI_VALUE))
            '        If yotoKbn.Equals("10") = True AndAlso String.IsNullOrEmpty(setteiValue) = False Then
            '            rtnFlg = True
            '        End If
            '    Next

            '    If rtnFlg = False Then
            '        If MyBase.ShowMessage("W139", New String() {"在庫残数集計区分"}) <> MsgBoxResult.Ok Then
            '            Return False
            '        End If

            '    End If

            'End If
            '要望番号:1500 KIM 2012.10.09 END

#If True Then           'ADD 2018/07/17 依頼番号 001540 
            '運送保険有り時、寄託商品単価0は、エラー
            If ("01").Equals(.cmbUnsoHoken.SelectedValue.ToString) = True Then
                If Convert.ToDecimal(.numKitakuShohinTanka.Value) = 0 Then

                    MyBase.ShowMessage("E223", New String() {String.Concat("運送保険有り時、", .lblTitleKitakuShohinTanka.Text, "が入っていないため保存")})

                    .cmbUnsoHoken.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    ctl = New Control() {.numKitakuShohinTanka}
                    focus = .numKitakuShohinTanka
                    Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoods)
                    Return False

                End If

            End If
#End If

            '消防危険区分関連チェック
            Dim ExistChkDr As DataRow() = Nothing
            Dim ShobokikenChkFlg As Boolean = True

            If String.IsNullOrEmpty(.txtShobo.TextValue) = False Then
                If Me._ControlV.SelectShoboListDataRow(ExistChkDr, .txtShobo.TextValue) = True Then

                    Dim RuiKbn As String = ExistChkDr(0).Item("RUI").ToString()

                    Select Case Me._Frm.cmbShobokiken.SelectedValue.ToString()
                        Case LMM100C.SHOBO_KIKEN_KIKEN
                            If Not String.IsNullOrEmpty(RuiKbn) AndAlso Not (RuiKbn <> "09") Then
                                ShobokikenChkFlg = False
                            End If
                        Case LMM100C.SHOBO_KIKEN_SHITEIKANEN
                            If Not String.IsNullOrEmpty(RuiKbn) AndAlso Not (RuiKbn = "09") Then
                                ShobokikenChkFlg = False
                            End If
                        Case LMM100C.SHOBO_KIKEN_HIGAITO
                            If Not String.IsNullOrEmpty(RuiKbn) Then
                                ShobokikenChkFlg = False
                            End If

                    End Select

                End If
            Else
                If Me._Frm.cmbShobokiken.SelectedValue.ToString() <> LMM100C.SHOBO_KIKEN_HIGAITO Then
                    ShobokikenChkFlg = False
                End If
            End If

            If ShobokikenChkFlg = False Then
                MyBase.ShowMessage("E456", New String() { .lblTitleShobokiken.TextValue, .lblTitleShobo.TextValue})

                Call Me._ControlV.SetErrorControl(.txtShobo, .tabGoodsMst, .tpgGoodsDetail)
                Return False
            End If

#If True Then 'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
            '在庫ありで使用状態不可はエラー
            If .cmbAVAL_YN.SelectedValue.Equals("00") _
                AndAlso zaikoFlg = True Then

                If sumPoraZaiNb <> 0 Then  'Add 2019/07/31 要望管理006855

                    MyBase.ShowMessage("E223", New String() {String.Concat("在庫有り時、使用可能''不可''では保存")})

                        .cmbAVAL_YN.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()

                    ctl = New Control() {.cmbAVAL_YN}
                    focus = .cmbAVAL_YN
                    Call Me._ControlV.SetErrorControl(ctl, focus, .tabGoodsMst, .tpgGoodsDetail)
                    Return False

                End If  'Add 2019/07/31 要望管理006855

            End If

#End If
            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprGoodsDetail)
            Dim spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread = .sprGoodsDetail
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max

                If spr.ActiveSheet.Cells(i, LMM100G.sprGoodsDtlDef.YOTO_KBN.ColNo).Value.Equals(LMM100C.YOTO_KBN.YOTO_SAFETY_STOCK) OrElse _
                   spr.ActiveSheet.Cells(i, LMM100G.sprGoodsDtlDef.YOTO_KBN.ColNo).Value.Equals(LMM100C.YOTO_KBN.YOTO_PALETTE_LIMIT) Then
                    vCell.SetValidateCell(i, LMM100G.sprGoodsDtlDef.SETTEI_VALUE.ColNo)
                    vCell.ItemName = LMM100G.sprGoodsDtlDef.SETTEI_VALUE.ColName
                    vCell.IsNumericCheck = True
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Call Me._ControlV.SetErrorControl(spr _
                                                          , i _
                                                          , LMM100G.sprGoodsDtlDef.SETTEI_VALUE.ColNo _
                                                          , .tabGoodsMst _
                                                          , .tpgGoodsDetail)
                        Return False
                    End If
                End If
            Next


        End With

        Return True

    End Function


    'START KIM 要望番号1518 2012/10/18

    ''' <summary>
    '''重複チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ExistGoodsDetail(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread, ByVal tpg As System.Windows.Forms.TabPage) As Boolean
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim id As String = String.Empty
        Dim chckId As String = String.Empty
        Dim yotoKbnColmNo As Integer = LMM100C.SprGoodsDetailColumnIndex.YOTO_KBN

        '重複チェック(同一の用途区分が使用されている場合エラー)
        For i As Integer = 0 To max
            With spr.ActiveSheet

                id = Me._ControlV.GetCellValue(.Cells(i, yotoKbnColmNo))
                If i <> max Then
                    For j As Integer = i + 1 To max
                        chckId = Me._ControlV.GetCellValue(.Cells(j, yotoKbnColmNo))
                        If id.Equals(chckId) = True Then
                            '20151029 tsunehira add Start
                            '英語化対応
                            MyBase.ShowMessage("E823", New String() {(i + 1).ToString(), (j + 1).ToString()})
                            '20151029 tsunehira add End

                            'MyBase.ShowMessage("E496", New String() {"用途区分", String.Concat("該当行:" _
                            '                                                               , (i + 1).ToString(), "行目," _
                            '                                                               , (j + 1).ToString(), "行目")})
                            Call Me._ControlV.SetErrorControl(spr, New Integer() {i, j} _
                                                              , New Integer() {yotoKbnColmNo, yotoKbnColmNo} _
                                                              , Me._Frm.tabGoodsMst, tpg)
                            Return False
                        End If
                    Next
                End If
            End With
        Next

        Return True

    End Function

    'END KIM 要望番号1518 2012/10/18

    ''' <summary>
    '''行追加時、Spreadに何も入力されていない場合、エラー
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKuranChk() As Boolean

        With Me._Frm.sprGoodsDetail.ActiveSheet

            Dim rowMax As Integer = .Rows.Count - 1
            Dim colMax As Integer = .Columns.Count - 1
            Dim chkFlg As Boolean = False
            For i As Integer = 0 To rowMax
                For j As Integer = LMM100G.sprGoodsDtlDef.DEF.ColNo + 1 To colMax
                    If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, j))) = False Then
                        chkFlg = True
                        Exit For
                    End If
                Next
                If chkFlg = False Then
                    MyBase.ShowMessage("E219")
                    Return False
                End If

                '初期値設定
                chkFlg = False
            Next
        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷ボタン押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSingleChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '【営業所】

            '2017/09/25 修正 李↓
            .cmbPrint.ItemName = lgm.Selector({"印刷種別", "Print Type", "인쇄종별", "中国語"})
            '2017/09/25 修正 李↑

            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprGoods)


            '【営業所】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.BR_NM.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.BR_NM.ColName
            vCell.IsHissuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.CUST_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.CUST_CD.ColName
            vCell.IsHissuCheck = True
            vCell.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            If Me._ControlV.GetCellValue(.sprGoods.ActiveSheet.Cells(0, LMM100G.sprGoodsDef.CUST_CD.ColNo)).Length < 5 Then
                Me.ShowMessage("E182", New String() {LMM100G.sprGoodsDef.CUST_CD.ColName, "5Byte"})
                Me._ControlV.SetErrorControl(.sprGoods, 0, LMM100G.sprGoodsDef.CUST_CD.ColNo)
                Return False
            End If

            '【荷主名(大)】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.CUST_NM_L.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.CUST_NM_L.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【商品コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.GOODS_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.GOODS_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【商品名】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.GOODS_NM_1.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.GOODS_NM_1.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【単価グループコード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.TANKA_GROUP_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.TANKA_GROUP_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【消防コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.SHOBO_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.SHOBO_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【請求先コード】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.SEIQT_CD.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.SEIQT_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【請求先会社名】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.SEIQT_COMP_NM.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.SEIQT_COMP_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【請求先部署名】
            vCell.SetValidateCell(0, LMM100G.sprGoodsDef.SEIQT_BUSHO_NM.ColNo)
            vCell.ItemName = LMM100G.sprGoodsDef.SEIQT_BUSHO_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

#End Region

End Class
