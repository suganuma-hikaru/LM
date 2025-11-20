' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB090G : 画像照会
'  作  成  者       :  matsumoto
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMB090Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB090G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB090F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConG As LMBControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB090F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.POP_M)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = True

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

    Friend Sub SetInitForm(ByVal frm As LMB090F, ByVal prmDs As DataSet)
        Dim inTbl As DataTable = prmDs.Tables(LMB090C.TABLE_NM_IN)
        If inTbl.Rows.Count > 0 Then
            'UPD START 2023/08/18 037916
            'If Not String.IsNullOrEmpty(inTbl.Rows(0).Item("FILE_PATH").ToString()) Then
            '    frm.picPhoto.ImageLocation = inTbl.Rows(0).Item("FILE_PATH").ToString()
            'End If
            If inTbl.Rows(0).Item("IMAGE") IsNot Nothing AndAlso inTbl.Rows(0).Item("IMAGE") IsNot DBNull.Value Then
                frm.picPhoto.Image = CType(inTbl.Rows(0).Item("IMAGE"), Image)
            End If
            'UPD END 2023/08/18 037916
        End If
    End Sub

    ''' <summary>
    ''' ステータスバーの位置調整
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Sub SizesetStatusStrip(ByVal frm As LMB090F)
        'デザイナ上ではステータスバー内項目の幅設定ができないため、画面表示時に位置調整を行う
        '（参考機能：LMI530G.vb）

        'デザイン時のステータスバー幅(1274)より若干縮めた幅
        Dim statusWidth As Integer = 1269

        'ステータスバーの位置調整
        Dim ctlSAria() As Control = frm.Controls.Find("pnlStatusAria", True)
        If ctlSAria.Length > 0 Then
            Dim ctlSts() As Control = ctlSAria(0).Controls.Find("StatusStrip", True)
            If ctlSts.Length > 0 Then
                'ステータスバー内表示項目幅集計
                Dim shrinkCount As Integer
                Dim totalWidth As Integer = 0
                shrinkCount = 0
                For i = 0 To DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items.Count
                    If _
                        DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusDbNm" OrElse
                        DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusUserNm" OrElse
                        DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusDateTime" Then
                        shrinkCount += 1
                        totalWidth += DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Width
                    End If
                    If shrinkCount >= 3 Then
                        Exit For
                    End If
                Next
                'ステータスバー内表示項目幅の調整
                Dim shrinkWidth As Integer = statusWidth - totalWidth
                shrinkCount = 0
                For i = 0 To DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items.Count
                    If _
                        DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusDbNm" OrElse
                        DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusUserNm" OrElse
                        DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusDateTime" Then
                        shrinkCount += 1
                        DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Width +=
                            CType(Math.Truncate(shrinkWidth * DirectCast(ctlSts(0), System.Windows.Forms.ToolStrip).Items(i).Width / totalWidth), Integer)
                    End If
                    If shrinkCount >= 3 Then
                        Exit For
                    End If
                Next
            End If
        End If

    End Sub

#End Region

#End Region

End Class
