' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH080V : EDI出荷データ検索
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMH080Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMH080V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH080F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMHControlV

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList


    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMHControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH080F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        '2011.06.16 ADD 馬野 NEWする必要があるのか今後検討!!!
        Me._Gcon = New LMHControlG(frm)

        '2011.06.15 EDIT 馬野
        'Validate共通クラスの設定
        Me._Vcon = New LMHControlV(handlerClass, DirectCast(frm, Form), Me._Gcon)

        Me._ChkList = New ArrayList()

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMH080C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMH080C.EventShubetsu.KENSAKU         '検索
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

            Case LMH080C.EventShubetsu.Delete         '変更
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

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMH080C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprEdiList)

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChk(ByVal arr As ArrayList) As Boolean

        '選択チェック
        If Me._Vcon.IsSelectChk(arr.Count) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

#Region "入力チェック（検索）"

    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            Call Me._Vcon.TrimSpaceSprTextvalue(.sprEdiList)

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '荷主コード
            .txtCustCD_L.ItemName() = "荷主コード大"
            .txtCustCD_L.IsForbiddenWordsCheck() = True
            .txtCustCD_L.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード中
            .txtCustCD_M.ItemName() = "荷主コード中"
            .txtCustCD_M.IsForbiddenWordsCheck() = True
            .txtCustCD_M.IsByteCheck() = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            'データ取込日From
            If .imdEdiDateFrom.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"データ取込日From", "8"})
                Return False
            End If

            'データ取込日To
            If .imdEdiDateTo.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"データ取込日To", "8"})
                Return False
            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprEdiList)

            'DeliverryNo
            vCell.SetValidateCell(0, LMH080G.sprEdiListDef.DELIV_NO.ColNo)
            vCell.ItemName() = "Deliverry No."
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品コード
            vCell.SetValidateCell(0, LMH080G.sprEdiListDef.GOODS_CD.ColNo)
            vCell.ItemName() = "商品コード"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品名
            vCell.SetValidateCell(0, LMH080G.sprEdiListDef.GOODS_NM.ColNo)
            vCell.ItemName() = "商品名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 35
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ロットNo.
            vCell.SetValidateCell(0, LMH080G.sprEdiListDef.LOT_NO.ColNo)
            vCell.ItemName() = "ロットNo."
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先名
            vCell.SetValidateCell(0, LMH080G.sprEdiListDef.DEST_NM.ColNo)
            vCell.ItemName() = "届先名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 125
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '入荷管理番号（大）
            vCell.SetValidateCell(0, LMH080G.sprEdiListDef.INKA_CTL_NO_L.ColNo)
            vCell.ItemName() = "入荷管理番号（大）"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ファイル名
            vCell.SetValidateCell(0, LMH080G.sprEdiListDef.FILE_NAME.ColNo)
            vCell.ItemName() = "ファイル名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 300
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ファイル行数
            vCell.SetValidateCell(0, LMH080G.sprEdiListDef.DATA_SEQ.ColNo)
            vCell.ItemName() = "ファイル行数"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuKanrenCheck() As Boolean

        With Me._Frm

            '出荷検索日Fromより出荷検索日Toが過去日の場合エラー
            'いずれも設定済である場合のみチェック
            If Me._Vcon.IsDateFromToChk(.imdEdiDateFrom, .imdEdiDateTo, "データ取込日To", "データ取込日From") = False Then

                Return False

            End If

        End With

        Return True

    End Function

#End Region '入力チェック（検索）

#Region "入力チェック（削除）"

    ''' <summary>
    ''' 削除時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsDeleteSingleCheck() As Boolean

        Dim selectArray As ArrayList = Me.GetCheckList
        '選択チェック
        If Me.IsSelectDataChk(selectArray) = False Then
            Return False
        End If

        Dim ContinueNo As Integer = 0
        With Me._Frm.sprEdiList.Sheets(0)

            For Each i As String In selectArray

                If "1".Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(i), LMH080C.SprColumnIndex.DEL_KB))) Then
                    MyBase.ShowMessage("E320", New String() {"すでに削除されているデータ", "削除処理は"})
                    Return False

                End If
            Next

        End With

        Return True

    End Function


#End Region

#End Region

End Class
