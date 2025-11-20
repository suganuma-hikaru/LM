' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB050V : 入荷検品取込
'  作  成  者       :  菊池
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMB050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB050F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMBControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB050F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMBControlV(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

    Private Property sprDetail As Object

    ''' <summary>
    ''' 単項目
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsInputChk() As Boolean

        'Trim
        For i As Integer = 1 To Me._Frm.sprDetail.ActiveSheet.RowCount - 1
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, i)
        Next

        '単項目チェック
        If Me.IsSpreadInputChk() = False Then
            Return False
        End If

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

            '商品名
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.GOODS_NM.ColNo)
            vCell.ItemName = "商品名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品コード
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.GOODS_CD_CUST.ColNo)
            vCell.ItemName = "商品コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ロットNO
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.LOT_NO.ColNo)
            vCell.ItemName = "ロットNO"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考大(社外)
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.REMARK_L.ColNo)
            vCell.ItemName = "備考大(社外)"
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品別コメント
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.REMARK_M.ColNo)
            vCell.ItemName = "商品別コメント"
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '棟
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.TOU_NO.ColNo)
            vCell.ItemName = "棟"
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '室
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.SITU_NO.ColNo)
            vCell.ItemName = "室"
            vCell.IsByteCheck = 1
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ZONE
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.ZONE_CD.ColNo)
            vCell.ItemName = "ZONE"
            vCell.IsByteCheck = 1
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'LOCA
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.LOCA.ColNo)
            vCell.ItemName = "LOCA"
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'オーダー番号
            vCell.SetValidateCell(0, LMB050G.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo)
            vCell.ItemName = "オーダー番号"
            vCell.IsByteCheck = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


        End With

        Return True


    End Function

    ''' <summary>
    ''' チェックリストの選択チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSelectedCheck() As Boolean

        'チェックリスト格納変数
        Dim list As ArrayList = New ArrayList()

        '未選択チェック
        'チェックリスト取得
        list = Me.getCheckList()

        '未選択チェック
        If _Vcon.IsSelectChk(list.Count()) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        '商品なしデータを含んでいた場合、エラー
        If Me.IsIncludeNoGoods() = False Then
            MyBase.ShowMessage("E549")
            Return False
        End If

        '商品複数データを含んでいた場合、エラー
        If Me.IsIncludeAnyGoods() = False Then
            MyBase.ShowMessage("E550")
            Return False
        End If

        'ZONEなしデータを含んでいた場合、エラー
        If Me.IsIncludeNoZone() = False Then
            MyBase.ShowMessage("E726")
            Return False
        End If

        '注文番号違いの商品データを選択した場合、エラー
        If Me.IsIncludeDiffOrderNoL() = False Then
            MyBase.ShowMessage("E727")
            Return False
        End If

        'キャンセルの商品データを選択した場合、エラー
        If Me.IsIncludeCancel() = False Then
            MyBase.ShowMessage("E728")
            Return False
        End If

        '2015/2/20 BUYER_ORDER_NO 追加 adachi

        Return True

    End Function

    ''' <summary>
    ''' 商品なしデータを含んでいた場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIncludeNoGoods() As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1
        Dim intGoodsCount As Integer = 0

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet

                intGoodsCount = Integer.Parse(_Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), LMB050G.sprDetailDef.M_GOODS_COUNT.ColNo)))
                If intGoodsCount = 0 Then
                    '商品が存在しない場合
                    Return False
                End If
            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' 商品複数データを含んでいた場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIncludeAnyGoods() As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1
        Dim intGoodsUTCount As Integer = 0

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet

                intGoodsUTCount = Integer.Parse(_Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), LMB050G.sprDetailDef.M_GOODS_UT_NB_COUNT.ColNo)))
                If intGoodsUTCount > 1 Then
                    '該当商品が複数存在する場合
                    Return False
                End If
            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' ZONEなしデータを含んでいた場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIncludeNoZone() As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1
        Dim intZoneCount As Integer = 0

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet

                intZoneCount = Integer.Parse(_Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), LMB050G.sprDetailDef.M_ZONE_COUNT.ColNo)))
                If intZoneCount = 0 Then
                    'ZONEが存在しない場合
                    Return False
                End If
            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' オーダー番号違いの商品データを含んでいた場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIncludeDiffOrderNoL() As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1

        '0行目データ取得
        Dim strSerchWord As String = _Vcon.GetCellValue(_Frm.sprDetail.ActiveSheet.Cells(Integer.Parse(chkList(0).ToString()), LMB050G.sprDetailDef.BUYER_ORD_NO_L.ColNo))

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet

                If strSerchWord.Equals(_Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), LMB050G.sprDetailDef.BUYER_ORD_NO_L.ColNo))) = False Then
                    Return False
                End If
                strSerchWord = _Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), LMB050G.sprDetailDef.BUYER_ORD_NO_L.ColNo))
            End With

        Next

        Return True

    End Function

    ''' <summary>
    ''' キャンセルデータを含んでいた場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIncludeCancel() As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1

        'キャンセル名取得
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'F009' AND KBN_CD = '2'")(0)
        Dim strCancelNM As String = dr.Item("KBN_NM1").ToString()

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet
                If strCancelNM = _Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), LMB050G.sprDetailDef.STATE.ColNo)) Then
                    'STATEがキャンセル状態の場合
                    Return False
                End If
            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMB050C.SprColumnIndex.DEF

        '選択された行の行番号を取得
        Return Me.SprSelectList(defNo, _Frm.sprDetail)

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="defNo">チェックボックスセルのカラム№</param>
    ''' <param name="sprDetail">対象スプレッド</param>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Overloads Function SprSelectList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpreadSearch) As ArrayList

        With sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If _Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' 商品ポップを呼び出せるステータスかの判定
    ''' </summary>
    ''' <param name="targetRowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCallGoodsPop(ByVal targetRowIndex As Integer) As Boolean

        Dim strCount As String = _Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(targetRowIndex, LMB050G.sprDetailDef.M_GOODS_UT_NB_COUNT.ColNo))
        Dim intCount As Integer = 0

        If Integer.TryParse(strCount, intCount) Then
            If 1 < intCount Then
                '対象に入り目違いの商品マスタが複数ある場合
                Return True
            End If
        End If

        Return False

    End Function

    ''' <summary>
    ''' STATEが"変更"でかつ既に登録されている場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsStateHenko(ByVal ds As DataSet) As Boolean

        '未選択チェック
        If Not ds.Tables("LMB050IN").Rows.Count = 0 Then
            MyBase.ShowMessage("E729")
            Return False
        End If

        Return True

    End Function

#End Region 'Method

End Class
