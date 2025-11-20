' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ040 : 入目単位
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMZ040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ040F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ040F, ByVal v As LMZControlV)

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
        Call Me._LMZConV.TrimSpaceSprTextvalue(Me._Frm.sprTanka, 0, Me._Frm.sprTanka.ActiveSheet.Columns.Count - 1)


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

            '荷主コード(大)
            '2016.01.06 UMANO 英語化対応START
            '.lblCustCdL.ItemName = "荷主コード(大)"
            .lblCustCdL.ItemName = .LmTitleLabel6.Text()
            '2016.01.06 UMANO 英語化対応END
            .lblCustCdL.IsHissuCheck = True
            If MyBase.IsValidateCheck(.lblCustCdL) = False Then
                Return False
            End If

            '荷主コード(中)
            '2016.01.06 UMANO 英語化対応START
            '.lblCustCdM.ItemName = "荷主コード(中)"
            .lblCustCdM.ItemName = .LmTitleLabel6.Text()
            '2016.01.06 UMANO 英語化対応END
            .lblCustCdM.IsHissuCheck = True
            If MyBase.IsValidateCheck(.lblCustCdM) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' ＯＫボタン押下時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsOKCheck(ByVal arr As ArrayList _
                                  , ByVal miraiStrFlg As String _
                                  , ByVal sysDate As String) As Boolean

        '単一選択チェック
        If Me._LMZConV.IsSelectOneChk(arr.Count) = False Then
            Return False
        End If

        '未選択チェック
        If Me._LMZConV.IsSelectChk(arr.Count) = False Then
            Return False
        End If

        '選択時チェック
        If Me.IsSelectCheck(arr, miraiStrFlg, sysDate) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 選択時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSelectCheck(ByVal arr As ArrayList _
                                  , ByVal miraiStrFlg As String _
                                  , ByVal sysDate As String) As Boolean

        With Me._Frm.sprTanka.ActiveSheet

            '未来適用日選択チェック
            If miraiStrFlg.Equals(LMConst.FLG.OFF) Then
                Dim strDate As String = DateFormatUtility.DeleteSlash(Me._LMZConV.GetCellValue(.Cells(Convert.ToInt32(arr(0).ToString), LMZ040G.sprDetailDef.STR_DATE.ColNo)))

                If Convert.ToInt32(sysDate) < Convert.ToInt32(strDate) Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E320", New String() {"適用開始日が未来日", "選択"})
                    MyBase.ShowMessage("E898")
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If

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
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprTanka)

            '正式名
            vCell.SetValidateCell(0, LMZ040G.sprDetailDef.UP_GP_CD_1.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "マスタコード"
            vCell.ItemName = LMZ040G.sprDetailDef.UP_GP_CD_1.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品コード
            vCell.SetValidateCell(0, LMZ040G.sprDetailDef.REMARK.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "摘要"
            vCell.ItemName = LMZ040G.sprDetailDef.REMARK.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主カテゴリ1
            vCell.SetValidateCell(0, LMZ040G.sprDetailDef.REC_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "レコード№"
            vCell.ItemName = LMZ040G.sprDetailDef.REC_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region 'Method

End Class
