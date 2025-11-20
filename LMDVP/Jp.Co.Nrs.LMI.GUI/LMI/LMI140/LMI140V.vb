' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI140V : 物産アニマルヘルス倉庫内処理検索
'  作  成  者       :  [HORI]
' ==========================================================================

Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI140Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI140V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI140F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI140F, ByVal v As LMIControlV, ByVal g As LMI140G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI140C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            If LMI140C.EventShubetsu.JISSEKI.Equals(eventShubetsu) = True Then
                '実績作成時チェック

                '選択チェック
                If Me.IsSelectDataChk() = False Then
                    Return False
                End If

            ElseIf LMI140C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '検索時チェック

                '営業所
                .cmbEigyo.ItemName() = .lblEigyo.Text
                .cmbEigyo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            MyBase.ShowMessage("E009")
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
        Dim defNo As Integer = LMI140C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprDetail)

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI140C.EventShubetsu, ByRef errDs As DataSet, ByVal g As LMI140G) As Hashtable

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        errDs = New LMI140DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprDetail.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                Dim jissekiFuyo As String = .Cells(selectRow, LMI140G.sprDetailDef.JISSEKI_FUYO.ColNo).Value().ToString()
                Dim jissekiShoriFlg As String = .Cells(selectRow, LMI140G.sprDetailDef.JISSEKI_SHORI_FLG.ColNo).Value().ToString()
                Dim procKbn As String = .Cells(selectRow, LMI140G.sprDetailDef.PROC_KBN.ColNo).Value().ToString()
                Dim nrsProcNo As String = .Cells(selectRow, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo).Value().ToString()

                '実績要否
                If "1".Equals(jissekiFuyo) Then
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMI140C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = "00"
                    dr("MESSAGE_ID") = "E320"
                    dr("PARA1") = "実績不要"
                    dr("PARA2") = "実績作成"
                    dr("KEY_NM") = "NRS処理番号"
                    dr("KEY_VALUE") = nrsProcNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMI140C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                'ステータス
                If Not "1".Equals(jissekiShoriFlg) Then
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMI140C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = "00"
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未送信"
                    dr("PARA2") = "実績作成"
                    dr("KEY_NM") = "NRS処理番号"
                    dr("KEY_VALUE") = nrsProcNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMI140C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '処理区分
                If Not "1".Equals(jissekiShoriFlg) Then
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMI140C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = "00"
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "ステータス変更"
                    dr("PARA2") = "実績作成"
                    dr("KEY_NM") = "NRS処理番号"
                    dr("KEY_VALUE") = nrsProcNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMI140C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

            End With

        Next

        Return errHt

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI140C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI140C.EventShubetsu.SINKI
                '新規
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

            Case LMI140C.EventShubetsu.JISSEKI
                '実績作成
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

            Case LMI140C.EventShubetsu.KENSAKU
                '検索
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

            Case LMI140C.EventShubetsu.DOUBLE_CLICK
                'スプレッドシート上でのダブルクリック
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

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

#End Region 'Method

End Class
