' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB080G : 登録済み画像照会
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
''' LMB080Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB080G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB080F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB080F)

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
            .SetFKeysType(LMImFunctionKey.FkeyTypes.POP_L)

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

    ''' <summary>
    ''' ステータスバーの位置調整
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Sub SizesetStatusStrip(ByVal frm As LMB080F)
        'デザイナ上ではステータスバー内項目の幅設定ができないため、画面表示時に位置調整を行う
        '（参考機能：LMI530G.vb）

        'デザイン時のステータスバー幅(878)より若干縮めた幅
        Dim statusWidth As Integer = 873

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

    ''' <summary>
    ''' 明細部の動的生成コントロール削除
    ''' </summary>
    Friend Sub RemoveDetailInCtl()

        Me._Frm.SuspendLayout()

        Dim arr As ArrayList = New ArrayList

        For Each ctl As Control In Me._Frm.pnlDetailIn.Controls
            arr.Add(ctl.Name)
        Next

        For i As Integer = 0 To arr.Count - 1
            Dim ctrlPic() As Control = Me._Frm.pnlDetailIn.Controls.Find(arr(i).ToString(), True)
            If ctrlPic.Length > 0 Then
                Me._Frm.pnlDetailIn.Controls.Remove(ctrlPic(0))
            End If
        Next

        '縦サイズを小さくしてスクロールバーを非表示にする
        Dim sizeX As Integer = Me._Frm.pnlDetailIn.Size.Width
        Me._Frm.pnlDetailIn.Size = New System.Drawing.Size(sizeX, 10)

        Me._Frm.ResumeLayout(True)

    End Sub

    ''' <summary>
    ''' 明細部の動的生成コントロール作成
    ''' </summary>
    Friend Sub AddDetailInCtl(ByVal ds As DataSet)

        '入庫品(NO)毎に枠を作成するため、NOでグループ化とソートを行う
        Dim workDt As DataTable = ds.Tables(LMB080C.TABLE_NM_IN).Copy
        Dim view As DataView = New DataView(workDt)
        workDt = view.ToTable(True, {"NO", "SHOHIN_NM", "SATSUEI_DATE", "USER_LNM"})
        Dim sortDr As DataRow() = workDt.Select(Nothing, "NO ASC")
        Dim max As Integer = sortDr.Count
        Dim ridx As Integer = 0
        Dim intPnlLocationY As Integer = 3
        Dim intPnlSizeY As Integer = 0
        Dim panelMarginY As Integer = 0

        Me._Frm.SuspendLayout()

        For Each row As DataRow In sortDr
            Dim photoNo As String = row("NO").ToString()
            ridx += 1

            '上下コンテナの間隔
            If ridx = 2 Then
                panelMarginY = 15
            End If

            '------------------------------
            '以下で作成するコントロールのコンテナ
            '------------------------------
            intPnlLocationY = intPnlLocationY + intPnlSizeY + panelMarginY
            Dim objPnl As System.Windows.Forms.Panel = New System.Windows.Forms.Panel()
            objPnl.Name = String.Concat(LMB080C.CTLNM_PANELB, ridx.ToString())
            objPnl.Location = New System.Drawing.Point(3, intPnlLocationY)
            objPnl.BorderStyle = BorderStyle.FixedSingle
            objPnl.Tag = photoNo

            '------------------------------
            'タイトルラベル
            '------------------------------
            Dim objLbl1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
            objLbl1.Name = String.Concat("lblTitle1_", ridx.ToString())
            objLbl1.Location = New System.Drawing.Point(3, 5)
            objLbl1.Size = New System.Drawing.Size(90, 20)
            objLbl1.AutoSize = True
            objLbl1.Text = "入庫品名"
            objLbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            objPnl.Controls.Add(objLbl1)

            Dim objLbl2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
            objLbl2.Name = String.Concat("lblTitle2_", ridx.ToString())
            objLbl2.Location = New System.Drawing.Point(15, 30)
            objLbl2.Size = New System.Drawing.Size(70, 20)
            objLbl2.AutoSize = True
            objLbl2.Text = "撮影日"
            objLbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            objPnl.Controls.Add(objLbl2)

            Dim objLbl3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
            objLbl3.Name = String.Concat("lblTitle3_", ridx.ToString())
            objLbl3.Location = New System.Drawing.Point(185, 30)
            objLbl3.Size = New System.Drawing.Size(70, 20)
            objLbl3.AutoSize = True
            objLbl3.Text = "撮影者"
            objLbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            objPnl.Controls.Add(objLbl3)

            '------------------------------
            'タイトルテキスト＆日付
            '------------------------------
            Dim objTxt1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
            objTxt1.Name = String.Concat(LMB080C.CTLNM_SHOHIN_NM, ridx.ToString())
            objTxt1.Location = New System.Drawing.Point(70, 4)
            objTxt1.Size = New System.Drawing.Size(450, 18)
            objTxt1.HissuLabelVisible = False
            objTxt1.TabStop = False
            objTxt1.ReadOnly = True
            objTxt1.TextValue = row("SHOHIN_NM").ToString()
            objPnl.Controls.Add(objTxt1)

            Dim objDate1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
            objDate1.Name = String.Concat(LMB080C.CTLNM_SATSUEI_DATE, ridx.ToString())
            objDate1.Location = New System.Drawing.Point(70, 27)
            objDate1.Size = New System.Drawing.Size(118, 18)
            objDate1.HissuLabelVisible = False
            objDate1.TabStop = False
            objDate1.ReadOnly = True
            objDate1.TextValue = row("SATSUEI_DATE").ToString()
            objPnl.Controls.Add(objDate1)

            Dim objTxt2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
            objTxt2.Name = String.Concat(LMB080C.CTLNM_USER_LNM, ridx.ToString())
            objTxt2.Location = New System.Drawing.Point(240, 27)
            objTxt2.Size = New System.Drawing.Size(157, 18)
            objTxt2.HissuLabelVisible = False
            objTxt2.TabStop = False
            objTxt2.ReadOnly = True
            objTxt2.TextValue = row("USER_LNM").ToString()
            objPnl.Controls.Add(objTxt2)

            '------------------------------
            '画像
            '------------------------------
            'NOに該当するFILE_PATHを抽出
            Dim photoDr As DataRow() = ds.Tables(LMB080C.TABLE_NM_IN).Select(String.Concat("NO = '", photoNo, "'"), "FILE_PATH ASC")
            Dim maxPhotoCnt As Integer = photoDr.Count
            Dim imgXLine As Integer = 0
            Dim imgYLine As Integer = 0
            Dim locationX As Integer = 0
            Dim locationY As Integer = 0
            Dim phidx As Integer = 0
            For Each photorow As DataRow In photoDr
                phidx += 1
                If imgXLine >= 5 Then
                    imgXLine = 0
                    imgYLine += 1
                End If

                locationX = 40 + (LMB080C.SIZE_PANEL_ADD_X * imgXLine)
                locationY = 70 + (LMB080C.SIZE_PANEL_ADD_Y * imgYLine)

                Dim objPic As System.Windows.Forms.PictureBox = New System.Windows.Forms.PictureBox()
                objPic.Name = String.Concat(LMB080C.CTLNM_PHOTO, ridx.ToString(), "_", phidx.ToString())
                objPic.Location = New System.Drawing.Point(locationX, locationY)
                objPic.Size = New System.Drawing.Size(120, 120)
                'UPD START 2023/08/18 037916
                'objPic.ImageLocation = photorow("FILE_PATH").ToString()
                objPic.Image = Me.GetImage(photorow("FILE_PATH").ToString())
                'UPD END 2023/08/18 037916
                objPic.SizeMode = PictureBoxSizeMode.StretchImage
                objPic.BorderStyle = BorderStyle.FixedSingle

                'イベント追加
                AddHandler objPic.DoubleClick, AddressOf Me._Frm.PictureBox_DoubleClick

                objPnl.Controls.Add(objPic)

                imgXLine += 1
            Next

            intPnlSizeY = locationY + LMB080C.SIZE_PANEL_ADD_Y + 20
            objPnl.Size = New System.Drawing.Size(823, intPnlSizeY)
            Me._Frm.pnlDetailIn.Controls.Add(objPnl)
        Next

        Dim sizeX As Integer = Me._Frm.pnlDetailIn.Size.Width
        Me._Frm.pnlDetailIn.Size = New System.Drawing.Size(sizeX, (intPnlLocationY + intPnlSizeY + 20))
        Me._Frm.ResumeLayout(True)
    End Sub

#End Region

#Region "部品"

    'ADD START 2023/08/18 037916
    ''' <summary>
    ''' ファイルパスよりImageを作成する
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    Private Function GetImage(ByVal filePath As String) As Image
        Dim img As Image = Nothing

        Try
            Using myClient As System.Net.WebClient = New System.Net.WebClient
                'メモリ上に画像ファイル保持
                Dim mstream As System.IO.MemoryStream = New System.IO.MemoryStream(myClient.DownloadData(filePath))
                img = Image.FromStream(mstream)

                'EXIFの情報に基づいて画像を回転する
                Dim rotation As RotateFlipType = RotateFlipType.RotateNoneFlipNone
                For Each item As Imaging.PropertyItem In img.PropertyItems
                    'IDが &H112 のタグ(Orientationタグ)に画像の方向が設定されている
                    If item.Id = &H112 Then
                        Select Case item.Value(0)
                            Case 1  '通常（回転・反転なし）
                            Case 2  '左右反転
                                rotation = RotateFlipType.RotateNoneFlipX
                            Case 3  '時計回りに180度回転
                                rotation = RotateFlipType.Rotate180FlipNone
                            Case 4  '時計回りに180度回転＆左右反転
                                rotation = RotateFlipType.Rotate180FlipX
                            Case 5  '時計回りに90度回転＆左右反転
                                rotation = RotateFlipType.Rotate90FlipX
                            Case 6  '時計回りに90度回転
                                rotation = RotateFlipType.Rotate90FlipNone
                            Case 7  '時計回りに270度回転＆左右反転
                                rotation = RotateFlipType.Rotate270FlipX
                            Case 8  '時計回りに270度回転
                                rotation = RotateFlipType.Rotate270FlipNone
                        End Select

                        Exit For
                    End If
                Next

                '画像回転
                img.RotateFlip(rotation)
            End Using

        Catch ex As Exception
            img = Nothing
        End Try

        Return img
    End Function
    'ADD END 2023/08/18 037916

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

#End Region

#End Region

End Class
