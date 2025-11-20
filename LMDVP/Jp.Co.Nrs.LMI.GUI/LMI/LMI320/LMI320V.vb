' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI320V : 請求明細・鑑作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI320Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI320V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI320F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI320F, ByVal v As LMIControlV, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

        'Gamen共通クラスの設定
        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI320C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI320C.EventShubetsu.TOJIRU       '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMI320C.EventShubetsu.SAKUSEI, _
                LMI320C.EventShubetsu.PRINT         '作成・印刷
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
    ''' 単項目/関連チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsInputChk(ByVal eventShubetsu As LMI320C.EventShubetsu) As Boolean

        '単項目/関連チェック
        If Me.IsSingleChk(eventShubetsu) = False _
            OrElse Me.IsSaveCheck(eventShubetsu) = False Then
            Return False
        End If

        Return True

    End Function

#Region "内部メソッド"

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI320C.SprDetailColumnIndex.DEF

        Return Me._ControlV.SprSelectList(defNo, Me._Frm.sprDetail)

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSingleChk(ByVal eventShubetsu As LMI320C.EventShubetsu) As Boolean

        With Me._Frm

            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)
            Dim arr As ArrayList = Me.GetCheckList

            Select Case eventShubetsu

                Case LMI320C.EventShubetsu.KENSAKU '検索処理

                    '【請求先コード】
                    vCell.SetValidateCell(0, LMI320G.sprDetailDef.SEIQTO_CD.ColNo)
                    vCell.ItemName = LMI320G.sprDetailDef.SEIQTO_CD.ColName
                    vCell.IsByteCheck = 7
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    '【請求先名】
                    vCell.SetValidateCell(0, LMI320G.sprDetailDef.SEIQTO_NM.ColNo)
                    vCell.ItemName = LMI320G.sprDetailDef.SEIQTO_NM.ColName
                    vCell.IsForbiddenWordsCheck = True
                    vCell.IsByteCheck = 60
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    '【鑑種別】
                    vCell.SetValidateCell(0, LMI320G.sprDetailDef.KAGAMI_SHUBETU.ColNo)
                    vCell.ItemName = LMI320G.sprDetailDef.KAGAMI_SHUBETU.ColName
                    vCell.IsByteCheck = 1
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    '【得意先コード】
                    vCell.SetValidateCell(0, LMI320G.sprDetailDef.TOKUISAKI_CD.ColNo)
                    vCell.ItemName = LMI320G.sprDetailDef.TOKUISAKI_CD.ColName
                    vCell.IsByteCheck = 8
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    '【負担課】
                    vCell.SetValidateCell(0, LMI320G.sprDetailDef.HUTANKA.ColNo)
                    vCell.ItemName = LMI320G.sprDetailDef.HUTANKA.ColName
                    vCell.IsByteCheck = 7
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                Case LMI320C.EventShubetsu.PRINT '印刷処理

                    '要望対応:1813 yamanaka 2013.02.21 Start
                    '選択チェック
                    'If Me._ControlV.IsSelectChk(arr.Count) = False Then
                    '    MyBase.ShowMessage("E009")
                    '    Return False
                    'End If
                    '要望対応:1813 yamanaka 2013.02.21 End

                    '【営業所】
                    .cmbNrsBr.ItemName = "営業所"
                    .cmbNrsBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbNrsBr) = False Then
                        Me._ControlV.SetErrorControl(.cmbNrsBr)
                        Return False
                    End If

                    '【請求日】
                    .imdSeiqDate.ItemName = "請求日"
                    .imdSeiqDate.IsHissuCheck = True
                    If IsValidateCheck(.imdSeiqDate) = False Then
                        Me._ControlV.SetErrorControl(.imdSeiqDate)
                        Return False
                    End If

                    '【印刷種別】
                    .cmbPrintShubetu.ItemName = "印刷種別"
                    .cmbPrintShubetu.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbPrintShubetu) = False Then
                        Me._ControlV.SetErrorControl(.cmbPrintShubetu)
                        Return False
                    End If

                Case LMI320C.EventShubetsu.SAKUSEI

                    '要望対応:1813 yamanaka 2013.02.21 Start
                    '選択チェック
                    'If Me._ControlV.IsSelectChk(arr.Count) = False Then
                    '    MyBase.ShowMessage("E009")
                    '    Return False
                    'End If
                    '要望対応:1813 yamanaka 2013.02.21 End

                    '【営業所】
                    .cmbNrsBr.ItemName = "営業所"
                    .cmbNrsBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbNrsBr) = False Then
                        Me._ControlV.SetErrorControl(.cmbNrsBr)
                        Return False
                    End If

                    '【請求日】
                    .imdSeiqDate.ItemName = "請求日"
                    .imdSeiqDate.IsHissuCheck = True
                    If Me.IsValidateCheck(.imdSeiqDate) = False Then
                        Me._ControlV.SetErrorControl(.imdSeiqDate)
                        Return False
                    End If

                    '【作成種別】
                    .cmbMake.ItemName = "作成種別"
                    .cmbMake.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbMake) = False Then
                        Me._ControlV.SetErrorControl(.cmbMake)
                        Return False
                    End If

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck(ByVal eventShubetsu As LMI320C.EventShubetsu) As Boolean

        With Me._Frm

            Dim arr As ArrayList = Me.GetCheckList
            Dim seiqtoCd As String = String.Empty

            Select Case eventShubetsu

                Case LMI320C.EventShubetsu.SAKUSEI

                    For i As Integer = 0 To arr.Count - 1

                        seiqtoCd = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI320G.sprDetailDef.SEIQTO_CD.ColNo))

                        '対象チェック
                        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'D020'" _
                                                                                                                      , "AND KBN_NM1 = '", seiqtoCd, "'"))
                        If kbnDr.Length = 0 AndAlso seiqtoCd.Equals("0001099") = False Then
                            MyBase.ShowMessage("E336", New String() {String.Concat(seiqtoCd, "は対象の請求先"), "処理"})
                            Return False
                        End If

                    Next

            End Select

        End With

        Return True

    End Function

#End Region

#End Region

End Class
