' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ050V : 棟室ゾーン照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMZ050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ050F
    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConV As LMZControlV
#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ050F, ByVal v As LMZControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMZConV = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 単項目
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsInputChk() As Boolean


        'Trim
        Call Me._LMZConV.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)


        '単項目チェック
        If Me.IsSingleCheck() = False Then
            Return False
        End If


        '単項目チェック
        If Me.IsSpreadInputChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 画面ヘッダー部入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSingleCheck() As Boolean

        With Me._Frm

            '営業所
            '2016.01.06 UMANO 英語化対応START
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.ItemName = .LmTitleLabel2.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '倉庫
            '2016.01.06 UMANO 英語化対応START
            '.cmbSoko.ItemName = "倉庫"
            .cmbSoko.ItemName = .LmTitleLabel1.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbSoko.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSoko) = False Then
                Return False
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm
            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '棟室名
            vCell.SetValidateCell(0, LMZ050G.sprDetailDef.TOU_SITU_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "棟室名"
            vCell.ItemName = LMZ050G.sprDetailDef.TOU_SITU_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '消防コード
            vCell.SetValidateCell(0, LMZ050G.sprDetailDef.SHOBO_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "消防コード"
            vCell.ItemName = LMZ050G.sprDetailDef.SHOBO_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


            '棟番号
            vCell.SetValidateCell(0, LMZ050G.sprDetailDef.TOU_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "棟番号"
            vCell.ItemName = LMZ050G.sprDetailDef.TOU_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '室番号
            vCell.SetValidateCell(0, LMZ050G.sprDetailDef.SITU_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "室番号"
            vCell.ItemName = LMZ050G.sprDetailDef.SITU_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            'vCell.IsByteCheck = 1
            vCell.IsByteCheck = 2
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If




        End With

        Return True


    End Function


#End Region 'Method

End Class
