' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI963V : 荷主自動振分画面(手動)（ハネウェル）
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI963Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI963V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI963F

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConV As LMIControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI963F, ByVal v As LMIControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConV = v

    End Sub

#End Region 'Constructor

#Region "Method"

    '''' <summary>
    '''' 単項目
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Friend Function IsInputChk() As Boolean

    '    'Trim
    '    Call Me._LMIConV.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)


    '    '単項目チェック
    '    If Me.IsSingleCheck() = False Then
    '        Return False
    '    End If


    '    '単項目チェック
    '    If Me.IsSpreadInputChk() = False Then
    '        Return False
    '    End If

    '    Return True

    'End Function

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
            '.cmbNrsBrCd.ItemName = .LmTitleLabel2.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
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

            '荷主名(大)
            vCell.SetValidateCell(0, LMI963G.sprDetailDef.CUST_NM_L.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(大)"
            vCell.ItemName = LMI963G.sprDetailDef.CUST_NM_L.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(中)
            vCell.SetValidateCell(0, LMI963G.sprDetailDef.CUST_NM_M.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(中)"
            vCell.ItemName = LMI963G.sprDetailDef.CUST_NM_M.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(小)
            vCell.SetValidateCell(0, LMI963G.sprDetailDef.CUST_NM_S.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(小)"
            vCell.ItemName = LMI963G.sprDetailDef.CUST_NM_S.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(極小)
            vCell.SetValidateCell(0, LMI963G.sprDetailDef.CUST_NM_SS.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(極小)"
            vCell.ItemName = LMI963G.sprDetailDef.CUST_NM_SS.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(大)
            vCell.SetValidateCell(0, LMI963G.sprDetailDef.CUST_CD_L.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(大)"
            vCell.ItemName = LMI963G.sprDetailDef.CUST_CD_L.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(中)
            vCell.SetValidateCell(0, LMI963G.sprDetailDef.CUST_CD_M.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(中)"
            vCell.ItemName = LMI963G.sprDetailDef.CUST_CD_M.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(小)
            vCell.SetValidateCell(0, LMI963G.sprDetailDef.CUST_CD_S.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(小)"
            vCell.ItemName = LMI963G.sprDetailDef.CUST_CD_S.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(極小)
            vCell.SetValidateCell(0, LMI963G.sprDetailDef.CUST_CD_SS.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(極小)"
            vCell.ItemName = LMI963G.sprDetailDef.CUST_CD_SS.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True


    End Function

    ''' <summary>
    ''' タイトルテキスト・フォント設定の切り替え
    ''' </summary>
    ''' <param name="fKey"></param>
    ''' <remarks></remarks>
    Friend Sub TitleSwitch(ByVal fKey As Win.InputMan.LMImFunctionKey)
        fKey.TitleSwitching(Me.MyForm)
    End Sub


#End Region 'Method

End Class
