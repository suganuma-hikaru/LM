' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI250V : シリンダ番号チェック
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMI250Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI250V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI250F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConV As LMIControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI250F, ByVal v As LMIControlV, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ConV = v

        Me._ConG = g

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal EVENTSHUBETSU As LMI250C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case EVENTSHUBETSU

            Case LMI250C.EventShubetsu.PRINT        '印刷
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

            Case LMI250C.EventShubetsu.TOJIRU           '閉じる
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
    ''' 入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal EVENTSHUBETSU As LMI250C.EventShubetsu) As Boolean

        '単項目チェック
        If Me.IsPrintCheck(EVENTSHUBETSU) = False Then
            Return False
        End If

        '関連チェック
        If Me.IsPrintSaveCheck(EVENTSHUBETSU) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintCheck(ByVal EVENTSHUBETSU As LMI250C.EventShubetsu) As Boolean

        With Me._Frm

            ''荷主コード(大)
            '.txtCustCd.ItemName() = "荷主コード(大)"
            '.txtCustCd.IsHissuCheck() = True
            '.txtCustCd.IsForbiddenWordsCheck() = True
            '.txtCustCd.IsFullByteCheck() = 5
            'If MyBase.IsValidateCheck(.txtCustCd) = False Then
            '    Return False
            'End If


            '日付From
            .imdPrintDate_From.ItemName = "入庫予定日From"
            .imdPrintDate_From.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdPrintDate_From) = False Then
                Return False
            End If

            'フルバイトチェック
            If Me.IsInputDateFullByteChk(.imdPrintDate_From, .imdPrintDate_From.ItemName) = False Then
                Return False
            End If

            '日付To
            .imdPrintDate_To.ItemName = "入庫予定日To"
            .imdPrintDate_To.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdPrintDate_To) = False Then
                Return False
            End If

            'フルバイトチェック
            If Me.IsInputDateFullByteChk(.imdPrintDate_To, .imdPrintDate_To.ItemName) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintSaveCheck(ByVal EVENTSHUBETSU As LMI250C.EventShubetsu) As Boolean

        With Me._Frm

            Dim dateFrom As String = .imdPrintDate_From.TextValue
            Dim dateTo As String = .imdPrintDate_To.TextValue

            '日付の大小チェック
            If Me.IsLargeSmallChk(Convert.ToDecimal(dateTo), Convert.ToDecimal(dateFrom), False) = False Then
                .imdPrintDate_From.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._ConV.SetErrorControl(.imdPrintDate_To)
                MyBase.ShowMessage("E039", New String() {"日付To", "日付From"})
                Return False
            End If

            '上限チェック
            Dim limitDate As String = Convert.ToDateTime(DateFormatUtility.EditSlash(dateFrom)).AddDays(30).ToString("yyyyMMdd")

            If limitDate < dateTo Then
                .imdPrintDate_From.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._ConV.SetErrorControl(.imdPrintDate_To)
                MyBase.ShowMessage("E014", New String() {"日付To" _
                                                        , DateFormatUtility.EditSlash(dateFrom) _
                                                        , DateFormatUtility.EditSlash(limitDate) _
                                                        })
                Return False
            End If

            '荷主明細チェック
            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue _
                                                                                                                           , "' AND SUB_KB = '46'"))
            If custDetailsDr.Length = 0 Then
                'No1942 by s.kobayashi
                'MyBase.ShowMessage("E336", New String() {"対象荷主", "印刷"})
                MyBase.ShowMessage("E375", New String() {"対象データが存在しない", "印刷"})
                Return False
            End If

        End With

        Return True

    End Function

#Region "日付関連"

    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then
            MyBase.ShowMessage("E038", New String() {str, "8"})
            Return False
        End If

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

#End Region

#End Region

End Class
