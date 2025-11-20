' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF210V : 運行未登録一覧
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMF210Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF210V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF210F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFConV As LMFControlV


    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFConG As LMFControlG


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF210F, ByVal v As LMFControlV, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMFConV = v

        Me._LMFConG = g

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック()
        If Me.IsSingleCheck() = False Then
            Return False
        End If


        '単項目チェック
        If Me.IsSpreadInputChk() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsSaveCheck() = False Then
            Return False
        End If


        Return True



    End Function


    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'スプレッドのスペース除去
        Call Me._LMFConV.TrimSpaceSprTextvalue(Me._Frm.sprDetail)

    End Sub
    
    ''' <summary>
    ''' 画面ヘッダー部入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSingleCheck() As Boolean

        With Me._Frm
            Dim errorFlg As Boolean = False

            '営業所
            .cmbEigyo.ItemName = "営業所"
            .cmbEigyo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '運行予定日From
            .imdFrom.ItemName = "運行予定日From"
            If Me._LMFConV.IsInputDateFullByteChk(.imdFrom, .imdFrom.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '運行予定日To
            .imdTo.ItemName = "運行予定日To"
            If Me._LMFConV.IsInputDateFullByteChk(.imdTo, .imdTo.ItemName) = errorFlg Then
                Return errorFlg
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

            '運送番号
            vCell.SetValidateCell(0, LMF210G.sprDetailDef.TRIP_NO.ColNo)
            vCell.ItemName = "運送番号"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


            '運送会社名
            vCell.SetValidateCell(0, LMF210G.sprDetailDef.UNSO_NM.ColNo)
            vCell.ItemName = "運送会社名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '車番
            vCell.SetValidateCell(0, LMF210G.sprDetailDef.CAR_NO.ColNo)
            vCell.ItemName = "車番"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '乗務員
            vCell.SetValidateCell(0, LMF210G.sprDetailDef.DRIVER_NM.ColNo)
            vCell.ItemName = "乗務員"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考
            vCell.SetValidateCell(0, LMF210G.sprDetailDef.REMARK.ColNo)
            vCell.ItemName = "備考"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            Return True

        End With

    End Function


    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck() As Boolean

        With Me._Frm
            'エラーフラグ
            Dim errorFlg As Boolean = False
            '積載重量Fromの値
            Dim juryouFrom As String = .numLoadWtZanFrom.TextValue
            '積載重量Toの値
            Dim juryouTo As String = .numLoadWtZanTo.TextValue
            '管理温度Fromの値
            Dim kannriFrom As String = .numOnkanFrom.TextValue
            '管理温度Toの値
            Dim kannriTo As String = .numOnkanTo.TextValue

            '値'(積載重量From、積載重量To)がない場合、スルー
            If String.IsNullOrEmpty(juryouFrom) = False _
                 AndAlso String.IsNullOrEmpty(juryouTo) = False Then
                '積載重量の大小チェック
                ' 積載重量To<積載重量From時、エラー
                If Me.IsLargeSmallChk(juryouTo, juryouFrom, False) = False Then
                    .numLoadWtZanFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._LMFConV.SetErrorControl(.numLoadWtZanTo)
                    Return Me._LMFConV.SetErrMessage("E240", New String() {"積載重量残(From)", "積載重量(To)"})
                End If

            End If

            '運行予定日From
            .imdFrom.ItemName = "運行予定日(From)"
            '運行予定日To
            .imdTo.ItemName = "運行予定日(To)"
            '運行予定日の大小チェック
            If Me._LMFConV.IsDateFromToChk(.imdFrom, .imdTo, .imdTo.ItemName, .imdFrom.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '値(管理温度From、管理温度To)がない場合、スルー
            If String.IsNullOrEmpty(kannriFrom) = False _
              AndAlso String.IsNullOrEmpty(kannriTo) = False Then

                '管理温度の大小チェック
                ' 管理温度To<管理温度From時、エラー
                If Me.IsLargeSmallChk(kannriTo, kannriFrom, False) = False Then
                    .numOnkanFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._LMFConV.SetErrorControl(.numOnkanTo)
                    Return Me._LMFConV.SetErrMessage("E240", New String() {"管理温度(From)", "管理温度(To)"})
                End If

            End If

        End With

        Return True
    End Function
#Region "大小チェック"

    ''' <summary>
    ''' 大小チェック
    ''' </summary>
    ''' <param name="large">大きい方の値</param>
    ''' <param name="small">小さい方の値</param>
    ''' <param name="equalFlg">イコールがエラーの場合：True　イコールがエラーではないの場合：False</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsLargeSmallChk(ByVal large As String, ByVal small As String, ByVal equalFlg As Boolean) As Boolean

        '値がない場合、スルー
        If String.IsNullOrEmpty(large) = True OrElse _
           String.IsNullOrEmpty(small) = True _
           Then
            Return True
        End If

        '大小比較
        Return Me.IsLargeSmallChk(Convert.ToDecimal(Me._LMFConG.FormatNumValue(large)), Convert.ToDecimal(Me._LMFConG.FormatNumValue(small)), equalFlg)

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

#End Region 'Method

End Class
