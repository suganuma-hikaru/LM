' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI540V : オフライン出荷検索(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI540Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI540V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI540F

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI540G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI540F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm
        Me._Frm = frm
        Me._Gcon = New LMIControlG(frm)

        'Validate共通クラスの設定
        Me._Vcon = New LMIControlV(handlerClass, DirectCast(frm, Form), Me._Gcon)

        Me._G = New LMI540G(handlerClass, frm)

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "権限チェック"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI540C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu
            Case LMI540C.EventShubetsu.TORIKOMI     '取込

                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI540C.EventShubetsu.SEARCH,
                 LMI540C.EventShubetsu.PRINT
                ' 検索
                ' 印刷
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

        End Select

        If kengenFlg Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

#End Region

#Region "共通処理"

    ''' <summary>
    ''' チェックされた行番号のリストを取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        ' チェックボックスセルの列番号取得
        Dim defNo As Integer = LMI540C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprDetail)

    End Function

    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>チェックリストの取得が前提</remarks>
    Friend Function IsSelectDataChk(ByVal chkList As ArrayList) As Boolean

        If Not Me._Vcon.IsSelectChk(chkList.Count()) Then
            Me.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

#End Region ' "共通処理"

#Region "取込処理"

#Region "単項目チェック"

    ''' <summary>
    ''' 取込処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsTorikomiSingleCheck() As Boolean

        With Me._Frm

            ' 営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.cmbEigyo) Then
                Return False
            End If

            ' 倉庫
            .cmbWare.ItemName() = "倉庫"
            .cmbWare.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbWare) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region ' "単項目チェック"

#Region "関連チェック"

    ''' <summary>
    ''' 取込処理：入力チェック（関連チェック）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsTorikomiKanrenCheck(ByVal rtDs As DataSet, ByVal fileArr As ArrayList,
                                          ByVal SafeFileNames() As String, ByVal rcvDir As String) As Boolean

        Dim rcvFileCount As Integer = 0

        ' 受信可否チェック(ファイルをオープンされていないかチェック)
        For Each stFilePath As String In fileArr
            rcvFileCount += 1

            ' 受信ファイル可否チェック
            On Error Resume Next
            FileOpen(rcvFileCount, String.Concat(rcvDir, stFilePath), OpenMode.Binary, OpenAccess.ReadWrite)
            FileClose(rcvFileCount)
            If Err.Number > 0 Then
                Me.ShowMessage("E160", New String() {"選択されたファイルは、他", "開いているプログラム"})
                Return False
            End If
        Next

        Return True

    End Function

#End Region ' "関連チェック"

#End Region ' "取込処理"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="g"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSearchSingleCheck(ByVal g As LMI540G) As Boolean

        Dim sprDetailDef As LMI540G.sprDetailDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMI540G.sprDetailDefault)

        With Me._Frm

            ' 営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.cmbEigyo) Then
                Return False
            End If

            ' 倉庫
            .cmbWare.ItemName() = "倉庫"
            .cmbWare.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbWare) = False Then
                Return False
            End If

            ' 荷主コード(大)
            .txtCustCD_L.ItemName() = "荷主コード(大)"
            .txtCustCD_L.IsHissuCheck() = True
            .txtCustCD_L.IsForbiddenWordsCheck() = True
            .txtCustCD_L.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            ' 荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsHissuCheck() = True
            .txtCustCD_M.IsForbiddenWordsCheck() = True
            .txtCustCD_M.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            ' 出荷日FROM
            If Not .imdOutkaDateFrom.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {"出荷日From", "8"})
                Return False
            End If

            ' 出荷日TO
            If Not .imdOutkaDateTo.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {"出荷日To", "8"})
                Return False
            End If

            ' スプレッドの値をTrim
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)

            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)
            ' KEY NO.
            vCell.SetValidateCell(0, sprDetailDef.KEY_NO.ColNo)
            vCell.ItemName() = "KEY_NO"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 6
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' オフラインNo.
            vCell.SetValidateCell(0, sprDetailDef.OFFLINE_NO.ColNo)
            vCell.ItemName() = "OFFLINE_NO"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 依頼日
            vCell.SetValidateCell(0, sprDetailDef.IRAI_DATE.ColNo)
            vCell.ItemName() = "IRAI_DATE"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 8
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 依頼者
            vCell.SetValidateCell(0, sprDetailDef.IRAI_SYA.ColNo)
            vCell.ItemName() = "IRAI_SYA"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 種別
            vCell.SetValidateCell(0, sprDetailDef.SHUBETSU.ColNo)
            vCell.ItemName() = "SHUBETSU"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 納品日
            vCell.SetValidateCell(0, sprDetailDef.ARR_DATE.ColNo)
            vCell.ItemName() = "ARR_DATE"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 8
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 郵便番号
            vCell.SetValidateCell(0, sprDetailDef.ZIP.ColNo)
            vCell.ItemName() = "ZIP"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 9
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 住所
            vCell.SetValidateCell(0, sprDetailDef.DEST_AD.ColNo)
            vCell.ItemName() = "DEST_AD"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 200
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 会社名
            vCell.SetValidateCell(0, sprDetailDef.COMP_NM.ColNo)
            vCell.ItemName() = "COMP_NM"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 200
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 部署名
            vCell.SetValidateCell(0, sprDetailDef.BUSYO_NM.ColNo)
            vCell.ItemName() = "BUSYO_NM"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 200
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 担当者名
            vCell.SetValidateCell(0, sprDetailDef.TANTO_NM.ColNo)
            vCell.ItemName() = "TANTO_NM"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 200
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 電話番号
            vCell.SetValidateCell(0, sprDetailDef.TEL.ColNo)
            vCell.ItemName() = "TEL"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 品名
            vCell.SetValidateCell(0, sprDetailDef.GOODS_NM.ColNo)
            vCell.ItemName() = "GOODS_NM"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 200
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 製造ロット
            vCell.SetValidateCell(0, sprDetailDef.LOT_NO.ColNo)
            vCell.ItemName() = "LOT_NO"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 200
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 温度条件
            vCell.SetValidateCell(0, sprDetailDef.ONDO.ColNo)
            vCell.ItemName() = "ONDO"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 10
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 毒劇物
            vCell.SetValidateCell(0, sprDetailDef.DOKUGEKI.ColNo)
            vCell.ItemName() = "DOKUGEKI"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 10
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 配送便
            vCell.SetValidateCell(0, sprDetailDef.HAISO.ColNo)
            vCell.ItemName() = "HAISO"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 200
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' SAP受注登録番号
            vCell.SetValidateCell(0, sprDetailDef.SAP_NO.ColNo)
            vCell.ItemName() = "SAP_NO"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            ' 備考欄
            vCell.SetValidateCell(0, sprDetailDef.REMARK.ColNo)
            vCell.ItemName() = "REMARK"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 200
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索処理：入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSearchKanrenCheck() As Boolean

        With Me._Frm

            Dim custCdL As String = ""
            Dim custCdM As String = ""
            If String.IsNullOrEmpty(.txtCustCD_L.TextValue) = False Then
                custCdL = .txtCustCD_L.TextValue
            End If
            If String.IsNullOrEmpty(.txtCustCD_M.TextValue) = False Then
                custCdM = .txtCustCD_M.TextValue
            End If

            ' 荷主コード/荷主名 (大/中) 初期値設定(兼存在チェック)
            If Not Me._G.SetInitControlCust(Me._Frm, custCdL, custCdM) Then
                .txtCustCD_L.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                .txtCustCD_M.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                .txtCustCD_L.Focus()
                Me.ShowMessage("E773")
                Return False
            End If

            ' 出荷日FROM/出荷日TO
            If (Not String.IsNullOrEmpty(.imdOutkaDateFrom.TextValue)) AndAlso (Not String.IsNullOrEmpty(.imdOutkaDateTo.TextValue)) Then
                If Convert.ToInt32(.imdOutkaDateTo.TextValue) < Convert.ToInt32(.imdOutkaDateFrom.TextValue) Then
                    '大小チェック
                    Me.ShowMessage("E039", New String() {"出荷日To", "出荷日From"})
                    .imdOutkaDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdOutkaDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdOutkaDateFrom.Focus()
                    Return False
                End If
            End If
        End With

        Return True

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintSingleCheck(ByVal chkList As ArrayList) As Boolean

        With Me._Frm

            ' 営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.cmbEigyo) Then
                Return False
            End If

        End With

        ' 未選択チェック
        If Not IsSelectDataChk(chkList) Then
            Return False
        End If

        Return True

    End Function

#End Region

#End Region 'Method

End Class
