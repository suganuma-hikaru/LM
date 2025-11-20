' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ       : データ抽出
'  プログラムID     :  LMQ010    : データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMQ010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMQ010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMQ010F

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMQ010G

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMQconG As LMQControlG

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Hcon As LMQControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMQControlV

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
    Friend Sub New(ByVal handlerClass As LMBaseGUIHandler, ByVal frm As LMQ010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyForm = frm

        _Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMQControlV(handlerClass, DirectCast(frm, Form))

        Me._ChkList = New ArrayList()

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(Optional ByVal eventShubetsu As LMQ010C.EventShubetsu = LMQ010C.EventShubetsu.SINKI) As Boolean

        With Me._Frm

            '検索項目のTrim
            Call Me.TrimSpaceTextValue()

            '【単項目チェック】
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprPattern)

            If LMQ010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '「パターン一覧」のパターンID
                vCell.SetValidateCell(0, LMQ010G.sprPattern.PATTERN_ID.ColNo)
                '2016.01.06 UMANO 英語化対応START
                'vCell.ItemName() = "パターン一覧"
                vCell.ItemName() = LMQ010G.sprPattern.PATTERN_ID.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 10
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMQ010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '「パターン一覧」のExcel名称
                vCell.SetValidateCell(0, LMQ010G.sprPattern.FILE_NM.ColNo)
                '2016.01.06 UMANO 英語化対応START
                'vCell.ItemName() = "Excel名称"
                vCell.ItemName() = LMQ010G.sprPattern.FILE_NM.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 20
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMQ010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '「パターン一覧」のExcelﾌｧｲﾙ名
                vCell.SetValidateCell(0, LMQ010G.sprPattern.FILE_TITLE_NM.ColNo)
                '2016.01.06 UMANO 英語化対応START
                'vCell.ItemName() = "Excelﾌｧｲﾙ名"
                vCell.ItemName() = LMQ010G.sprPattern.FILE_TITLE_NM.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 100
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMQ010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '「パターン一覧」の抽出内容
                vCell.SetValidateCell(0, LMQ010G.sprPattern.EX_CONTENTS.ColNo)
                '2016.01.06 UMANO 英語化対応START
                'vCell.ItemName() = "抽出内容"
                vCell.ItemName() = LMQ010G.sprPattern.EX_CONTENTS.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 100
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMQ010C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '状態の削除チェック
                If "1".Equals(.lblSysDelFlg.TextValue) Then
                    MyBase.ShowMessage("E212")
                    Return False
                End If
            End If

            If LMQ010C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                'SQLが選択されているかのチェックを行う（パターンIDが空の時、選択されていないと判断する）
                If System.String.IsNullOrEmpty(.txtPatternID.TextValue) = True Then
                    MyBase.ShowMessage("E199", New String() {"SQL"})
                    Return False
                End If
            End If

            If LMQ010C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                'パターンID「LMQ841 Tra-Net&LMSデータ抽出」の場合入力チェックを追加で行う
                If "LMQ841".Equals(.txtPatternID.TextValue) Then
                    With .sprParam.Sheets(0)
                        Dim maxCol As Integer = .ColumnCount - 1
                        Dim dayFrom As String = ""
                        Dim dayTo As String = ""
                        For i As Integer = 0 To maxCol

                            If System.String.IsNullOrEmpty(.Cells(0, i).Text) = True Then
                                'パラーメタ必須チェック
                                MyBase.ShowMessage("E001", New String() {"パラメータ"})
                                Return False
                            End If

                            If "出荷日FROM".Equals(.ColumnHeader.Columns(i).Label) Then
                                dayFrom = .Cells(0, i).Text
                                If dayFrom.Length <> 8 Then
                                    MyBase.ShowMessage("E012", New String() {"出荷日FROM", "8"})
                                    Return False
                                End If
                            ElseIf "出荷日TO".Equals(.ColumnHeader.Columns(i).Label) Then
                                dayTo = .Cells(0, i).Text
                                If dayTo.Length <> 8 Then
                                    MyBase.ShowMessage("E012", New String() {"出荷日TO", "8"})
                                    Return False
                                End If
                            End If
                        Next

                        dayFrom = dayFrom.Substring(0, 4) + "/" + dayFrom.Substring(4, 2) + "/" + dayFrom.Substring(6, 2)
                        dayTo = dayTo.Substring(0, 4) + "/" + dayTo.Substring(4, 2) + "/" + dayTo.Substring(6, 2)

                        Dim dayFromTime As DateTime = DateTime.ParseExact(dayFrom, "yyyy/M/d", Nothing)
                        Dim dayToTime As DateTime = DateTime.ParseExact(dayTo, "yyyy/M/d", Nothing)

                        Dim day As Double = (dayToTime - dayFromTime).TotalDays
                        If day < 0 Then
                            '出荷日大小チェック
                            MyBase.ShowMessage("E039", New String() {"出荷日TO", "出荷日FROM"})
                            Return False
                        ElseIf day >= 31 Then
                            '出荷日期間チェック
                            MyBase.ShowMessage("E117", New String() {"出荷日期間", "一か月間"})
                            Return False
                        End If

                    End With
                End If
            End If

            If LMQ010C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                'パターンID
                .txtPatternID.TextValue = Trim(.txtPatternID.TextValue)
                .txtPatternID.ItemName() = .lblPatternID.Text
                .txtPatternID.IsHissuCheck() = True
                .txtPatternID.IsForbiddenWordsCheck() = True
                .txtPatternID.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtPatternID) = False Then
                    Return False
                End If

                If IsExistSQL(.txtPatternID.TextValue) = False Then
                    Return False
                End If

            End If

            If LMQ010C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '種別
                .cmbSyubetu.ItemName() = .lblSyubetu.Text
                .cmbSyubetu.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbSyubetu) = False Then
                    Return False
                End If
            End If

            '★★Excel名称のVisible=Falseにしたため
            'If LMQ010C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
            '    'Excel名称
            '    .txtExcelName.TextValue = Trim(.txtExcelName.TextValue)
            '    .txtExcelName.ItemName() = .lblExcelName.Text
            '    .txtExcelName.IsHissuCheck() = True
            '    .txtExcelName.IsForbiddenWordsCheck() = True
            '    .txtExcelName.IsByteCheck() = 20
            '    If MyBase.IsValidateCheck(.txtExcelName) = False Then
            '        Return False
            '    End If
            'End If

            If LMQ010C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '抽出内容
                .txtTyusyutu.TextValue = Trim(.txtTyusyutu.TextValue)
                .txtTyusyutu.ItemName() = .lblTyusyutu.Text
                .txtTyusyutu.IsHissuCheck() = True
                .txtTyusyutu.IsForbiddenWordsCheck() = True
                .txtTyusyutu.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtTyusyutu) = False Then
                    Return False
                End If
            End If

            If LMQ010C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                'SQL
                .txtSql.TextValue = Trim(.txtSql.TextValue)
                .txtSql.ItemName() = .lblSql.Text
                .txtSql.IsHissuCheck() = True
                '.txtSql.IsByteCheck() = 1000
                If MyBase.IsValidateCheck(.txtSql) = False Then
                    Return False
                End If

                'SQL独自の禁止文字チェック
                Dim sqlVal As String = .txtSql.TextValue.ToUpper
                Dim sqlNGword As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S055'")
                Dim sqlMax As Integer = sqlNGword.Count - 1
                Dim wordPoint As Integer = 0
                For i As Integer = 0 To sqlMax
                    wordPoint = sqlVal.IndexOf(sqlNGword(i).Item("KBN_NM1").ToString)
                    If 0 <= wordPoint Then
                        MyBase.ShowMessage("E213", New String() {sqlNGword(i).Item("KBN_NM1").ToString})
                        Return False
                    End If
                Next
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 編集ボタン押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsHenshuCheck(Optional ByVal eventShubetsu As LMQ010C.EventShubetsu = LMQ010C.EventShubetsu.SINKI) As Boolean

        With Me._Frm

            '削除レコードは編集不可
            If .lblSituation.RecordStatus.Equals(RecordStatus.DELETE_REC) Then
                MyBase.ShowMessage("E035")
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' データ抽出SQLマスタの存在チェック
    ''' </summary>
    ''' <param name="text">チェック対象文字列</param>
    ''' <returns>ユーザー名</returns>
    ''' <remarks></remarks>
    Private Function IsExistSQL(ByVal text As String) As Boolean

        IsExistSQL = True
        '★★★ キャッシュがないため保留。コメントアウトしている。
        ' ''存在チェック
        'Dim mstSQL As String = String.Empty
        ''Dim strSqlUser As String = String.Empty
        'mstSQL = string.concat("PATTERN_ID = '",text,"'")
        'Dim sqlRows As DataRow() = Me.GetCachedMasterDataSet().Tables(LMConst.CacheTBL.SQL).Select(mstSQL)
        'If sqlRows.Length = 0 Then
        '    '存在エラー時
        '    Me.ShowMessage("E079", New String() {"データ抽出SQLマスタ", text})    'エラーメッセージ
        '    IsExistSQL = False
        'Else
        '    '正常時               
        '    IsExistSQL = True
        'End If

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMQ010C.SprPatternColumnIndex.DEF

        '選択された行の行番号を取得
        Return _Vcon.SprSelectList(defNo, _Frm.sprPattern)

    End Function

    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMQ010C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMQ010C.EventShubetsu.SINKI           '新規
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、30:管理職、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = False
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMQ010C.EventShubetsu.HENSYU          '編集
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、30:管理職、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = False
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMQ010C.EventShubetsu.DELREV          '削除・復活
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、30:管理職、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = False
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMQ010C.EventShubetsu.KENSAKU         '検索
                'すべての権限許可
                kengenFlg = True

            Case LMQ010C.EventShubetsu.EXCEL           'EXCEL作成
                'すべての権限許可
                kengenFlg = True

            Case LMQ010C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMQ010C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

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
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtPatternID.TextValue = .txtPatternID.TextValue.Trim()
            .cmbSyubetu.TextValue = .cmbSyubetu.TextValue.Trim()
            .txtExcelName.TextValue = .txtExcelName.TextValue.Trim()
            .txtExcelTitleName.TextValue = .txtExcelTitleName.TextValue.Trim()
            .txtTyusyutu.TextValue = .txtTyusyutu.TextValue.Trim()
            .txtSql.TextValue = .txtSql.TextValue.Trim()

            'スプレッドのスペース除去
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprPattern, 0)

        End With

    End Sub
#End Region 'Method

End Class
