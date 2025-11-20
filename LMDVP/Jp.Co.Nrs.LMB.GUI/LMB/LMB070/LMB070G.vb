' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB070G : 写真選択
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
''' LMB070Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB070G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB070F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB070F)

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

        Dim sel As Boolean = False

        For Each ctl As Control In Me._Frm.pnlDetailIn.Controls
            If ctl.Name.IndexOf(LMB070C.CTLNM_PANELB) >= 0 Then
                sel = True
                Exit For
            End If
        Next

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
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = "登　録"
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
            .F9ButtonEnabled = True
            .F10ButtonEnabled = False
            .F11ButtonEnabled = sel
            .F12ButtonEnabled = True

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

    ''' <summary>
    ''' DownLoadボタンロック処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockDownLoadControl()

        Dim lock As Boolean

        With Me._Frm

            lock = Not (.FunctionKey.F11ButtonEnabled)

            Me.SetLockControl(.btnDownLoad, lock)

        End With

    End Sub

    ''' <summary>
    ''' 検索条件項目の初期化
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="prmDs"></param>
    Friend Sub SetInitForm(ByVal frm As LMB070F, ByVal prmDs As DataSet)

        frm.imdSatsueiDateFrom.TextValue = String.Empty
        frm.imdSatsueiDateTo.TextValue = String.Empty
        frm.txtKeyword.TextValue = String.Empty
        frm.txtNo.TextValue = String.Empty

    End Sub

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            'Main
            .imdSatsueiDateFrom.TabIndex = LMB070C.CtlTabIndex_MAIN.SATSUEI_DATE_FROM
            .imdSatsueiDateTo.TabIndex = LMB070C.CtlTabIndex_MAIN.SATSUEI_DATE_TO
            .txtKeyword.TabIndex = LMB070C.CtlTabIndex_MAIN.KEYWORD
            .pnlDetail.TabIndex = LMB070C.CtlTabIndex_MAIN.PNL_DETAIL
        End With

    End Sub

    ''' <summary>
    ''' ステータスバーの位置調整
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Sub SizesetStatusStrip(ByVal frm As LMB070F)
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
    ''' 画像選択時の背景色
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property ColorPhotoSel As System.Drawing.Color
        Get
            Return System.Drawing.Color.Red
        End Get
    End Property

    ''' <summary>
    ''' 画像非選択時の背景色
    ''' </summary>
    ''' <returns></returns>
    Friend ReadOnly Property ColorPhotoNotSel As System.Drawing.Color
        Get
            Return System.Drawing.Color.Transparent
        End Get
    End Property

    ''' <summary>
    ''' 明細部の動的生成コントロール作成
    ''' </summary>
    Friend Sub AddDetailInCtl(ByVal ds As DataSet, ByRef photoDict As Dictionary(Of String, Integer))

        '入庫品(NO)毎に枠を作成するため、NOでグループ化とソートを行う
        Dim workDt As DataTable = ds.Tables(LMB070C.TABLE_NM_OUT).Copy
        Dim view As DataView = New DataView(workDt)
        workDt = view.ToTable(True, {"NO", "SHOHIN_NM", "SATSUEI_DATE", "USER_LNM", "SYS_UPD_DATE", "SYS_UPD_TIME"})
        Dim sortDr As DataRow() = workDt.Select(Nothing, "NO ASC")
        Dim max As Integer = sortDr.Count
        Dim ridx As Integer = 0
        Dim pnlLocationYBase As Integer = LMB070C.SIZE_PANEL_Y + 10 'パネルYサイズ+パネル同士のY軸スペース

        photoDict.Clear()

        Me._Frm.SuspendLayout()

        For Each row As DataRow In sortDr

            Dim photoNo As String = row("NO").ToString()
            ridx += 1

            '------------------------------
            '以下で作成するコントロールのコンテナ（入庫品(NO)毎の枠）
            '------------------------------
            Dim intPnlLocationY As Integer = 3 + ((ridx - 1) * pnlLocationYBase)
            Dim objPnl As System.Windows.Forms.Panel = New System.Windows.Forms.Panel()
            objPnl.Name = String.Concat(LMB070C.CTLNM_PANELB, ridx.ToString())
            objPnl.Location = New System.Drawing.Point(3, intPnlLocationY)
            objPnl.Size = New System.Drawing.Size(823, LMB070C.SIZE_PANEL_Y)
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
            objTxt1.Name = String.Concat(LMB070C.CTLNM_SHOHIN_NM, ridx.ToString())
            objTxt1.Location = New System.Drawing.Point(70, 4)
            objTxt1.Size = New System.Drawing.Size(450, 18)
            objTxt1.HissuLabelVisible = False
            objTxt1.TabStop = False
            objTxt1.ReadOnly = True
            objTxt1.IsByteCheck = 50
            objTxt1.TextValue = row("SHOHIN_NM").ToString()
            objPnl.Controls.Add(objTxt1)

            Dim objDate1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
            objDate1.Name = String.Concat(LMB070C.CTLNM_SATSUEI_DATE, ridx.ToString())
            objDate1.Location = New System.Drawing.Point(70, 27)
            objDate1.Size = New System.Drawing.Size(118, 18)
            objDate1.HissuLabelVisible = False
            objDate1.TabStop = False
            objDate1.ReadOnly = True
            objDate1.TextValue = row("SATSUEI_DATE").ToString()
            objPnl.Controls.Add(objDate1)

            Dim objTxt2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
            objTxt2.Name = String.Concat(LMB070C.CTLNM_USER_LNM, ridx.ToString())
            objTxt2.Location = New System.Drawing.Point(240, 27)
            objTxt2.Size = New System.Drawing.Size(157, 18)
            objTxt2.HissuLabelVisible = False
            objTxt2.TabStop = False
            objTxt2.ReadOnly = True
            objTxt2.TextValue = row("USER_LNM").ToString()
            objPnl.Controls.Add(objTxt2)

            Dim objTxt3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
            objTxt3.Name = String.Concat(LMB070C.CTLNM_SYS_UPD_DATE, ridx.ToString())
            objTxt3.Location = New System.Drawing.Point(240, 27)
            objTxt3.Size = New System.Drawing.Size(1, 1)
            objTxt3.HissuLabelVisible = False
            objTxt3.TabStop = False
            objTxt3.ReadOnly = True
            objTxt3.Visible = False
            objTxt3.TextValue = row("SYS_UPD_DATE").ToString()
            objPnl.Controls.Add(objTxt3)

            Dim objTxt4 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
            objTxt4.Name = String.Concat(LMB070C.CTLNM_SYS_UPD_TIME, ridx.ToString())
            objTxt4.Location = New System.Drawing.Point(240, 27)
            objTxt4.Size = New System.Drawing.Size(1, 1)
            objTxt4.HissuLabelVisible = False
            objTxt4.TabStop = False
            objTxt4.ReadOnly = True
            objTxt4.Visible = False
            objTxt4.TextValue = row("SYS_UPD_TIME").ToString()
            objPnl.Controls.Add(objTxt4)

            '------------------------------
            '編集/保存ボタン
            '------------------------------
            Dim objButton3 As Jp.Co.Nrs.LM.GUI.Win.LMButton = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
            objButton3.Name = String.Concat("btnEdit_", ridx.ToString())
            objButton3.Location = New System.Drawing.Point(520, 3)
            objButton3.Size = New System.Drawing.Size(90, 22)
            objButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
            objButton3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
            objButton3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
            objButton3.EnableStatus = True
            objButton3.UseVisualStyleBackColor = True
            objButton3.Visible = True
            objButton3.Text = "編集"
            'イベント追加
            AddHandler objButton3.Click, AddressOf Me._Frm.btnEdit_Click
            objPnl.Controls.Add(objButton3)

            Dim objButton4 As Jp.Co.Nrs.LM.GUI.Win.LMButton = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
            objButton4.Name = String.Concat("btnSave_", ridx.ToString())
            objButton4.Location = New System.Drawing.Point(520, 3)
            objButton4.Size = New System.Drawing.Size(90, 22)
            objButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
            objButton4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
            objButton4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
            objButton4.EnableStatus = True
            objButton4.UseVisualStyleBackColor = True
            objButton4.Visible = False
            objButton4.Text = "保存"
            'イベント追加
            AddHandler objButton4.Click, AddressOf Me._Frm.btnSave_Click
            objPnl.Controls.Add(objButton4)

            '------------------------------
            '全て選択ボタン
            '------------------------------
            Dim objButton1 As Jp.Co.Nrs.LM.GUI.Win.LMButton = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
            objButton1.Name = String.Concat("btnAllSel_", ridx.ToString())
            objButton1.Location = New System.Drawing.Point(720, 3)
            objButton1.Size = New System.Drawing.Size(90, 22)
            objButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
            objButton1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
            objButton1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
            objButton1.EnableStatus = True
            objButton1.UseVisualStyleBackColor = True
            objButton1.Text = "全て選択"
            'イベント追加
            AddHandler objButton1.Click, AddressOf Me._Frm.btnAllSel_Click
            objPnl.Controls.Add(objButton1)

            '------------------------------
            '画像
            '------------------------------
            'NOに該当するFILE_PATHを抽出
            Dim photoDr As DataRow() = ds.Tables(LMB070C.TABLE_NM_OUT).Select(String.Concat("NO = '", photoNo, "'"), "FILE_PATH ASC")
            Dim maxPhotoCnt As Integer = photoDr.Count
            Dim imgXLine As Integer = 0
            Dim imgYLine As Integer = 0
            Dim phidx As Integer = 0
            For Each photorow As DataRow In photoDr
                phidx += 1
                If imgXLine >= 5 Then
                    imgXLine = 0
                    imgYLine += 1
                End If

                '前回登録済み画像の場合、選択済みとして表示
                Dim borderBackColor As Color = Me.ColorPhotoNotSel
                Dim picTag As String = String.Empty
                If ds.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows.Count > 0 Then
                    If ds.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Select(String.Concat("NO = '", photoNo, "' AND FILE_PATH = '", photorow("FILE_PATH").ToString(), "'")).Length > 0 Then
                        borderBackColor = Me.ColorPhotoSel
                        picTag = LMB070C.PHOTO_SEL
                    End If
                End If

                Dim locationX As Integer = 40 + (LMB070C.SIZE_PANEL_ADD_X * imgXLine)
                Dim locationY As Integer = 70 + (LMB070C.SIZE_PANEL_ADD_Y * imgYLine)

                Dim objLblBorder As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
                objLblBorder.Name = String.Concat(LMB070C.CTLNM_PHOTOLABEL, ridx.ToString(), "_", phidx.ToString())
                objLblBorder.Location = New System.Drawing.Point(locationX - 3, locationY - 3)
                objLblBorder.Size = New System.Drawing.Size(126, 126)
                objLblBorder.AutoSize = False
                objLblBorder.Text = ""
                objLblBorder.BackColor = borderBackColor
                objLblBorder.Visible = (imgYLine < 2)   '2行(10個)までは表示、以外は初回非表示
                objLblBorder.Tag = picTag   'ADD 2023/08/18 037916
                objPnl.Controls.Add(objLblBorder)

                Dim objPic As System.Windows.Forms.PictureBox = New System.Windows.Forms.PictureBox()
                objPic.Name = String.Concat(LMB070C.CTLNM_PHOTO, ridx.ToString(), "_", phidx.ToString())
                objPic.Location = New System.Drawing.Point(locationX, locationY)
                objPic.Size = New System.Drawing.Size(120, 120)
                'UPD START 2023/08/18 037916
                'objPic.ImageLocation = photorow("FILE_PATH").ToString()
                objPic.Image = Me.GetImage(photorow("FILE_PATH").ToString())
                'UPD END 2023/08/18 037916
                objPic.SizeMode = PictureBoxSizeMode.StretchImage
                objPic.BorderStyle = BorderStyle.FixedSingle
                objPic.Visible = (imgYLine < 2) '2行(10個)までは表示、以外は初回非表示
                'UPD START 2023/08/18 037916
                'objPic.Tag = picTag
                objPic.Tag = photorow("FILE_PATH").ToString()
                'UPD END 2023/08/18 037916

                'イベント追加
                AddHandler objPic.Click, AddressOf Me._Frm.PictureBox_Click
                AddHandler objPic.DoubleClick, AddressOf Me._Frm.PictureBox_DoubleClick

                objPnl.Controls.Add(objPic)
                objPic.BringToFront()   '最前面へ移動（ラベルの前面へ移動）

                imgXLine += 1
            Next

            '------------------------------
            '全てを表示タン
            '------------------------------
            Dim objButton2 As Jp.Co.Nrs.LM.GUI.Win.LMButton = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
            objButton2.Name = String.Concat("btnAllDisp_", ridx.ToString())
            objButton2.Location = New System.Drawing.Point(330, 350)
            objButton2.Size = New System.Drawing.Size(100, 22)
            objButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
            objButton2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
            objButton2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
            objButton2.EnableStatus = (maxPhotoCnt > 10)    '写真枚数が10以下なら使用不可
            objButton2.UseVisualStyleBackColor = True
            objButton2.Text = LMB070C.BTNTITLE_ALLDISP

            'イベント追加
            AddHandler objButton2.Click, AddressOf Me._Frm.btnAllDisp_Click
            objPnl.Controls.Add(objButton2)

            Me._Frm.pnlDetailIn.Controls.Add(objPnl)

            '入庫品枠ごとの写真枚数を保持
            photoDict(photoNo) = maxPhotoCnt
        Next

        Dim sizeX As Integer = Me._Frm.pnlDetailIn.Size.Width
        Me._Frm.pnlDetailIn.Size = New System.Drawing.Size(sizeX, (pnlLocationYBase * max))
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
