' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB040V : 入荷検品選択
'  作  成  者       :  小林
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.LMGUIUtility
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMB040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB040F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB040F)

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
    Friend Function IsInputChk(ByVal g As LMB040G) As Boolean


        'Trim
        For i As Integer = 1 To Me._Frm.sprDetail.ActiveSheet.RowCount - 1
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, i)
        Next


        '単項目チェック
        If Me.IsSingleCheck() = False Then
            Return False
        End If


        '単項目チェック
        If Me.IsSpreadInputChk(g) = False Then
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


            '作業者コード
            .txtSAGYO_USER_CD.ItemName = "作業者コード"
            .txtSAGYO_USER_CD.IsForbiddenWordsCheck = True
            .txtSAGYO_USER_CD.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSAGYO_USER_CD) = False Then
                Return False
            End If

            '追加開始 2015.05.15 要望番号:2292
            '入荷日From + 入荷日To
            '  入荷日Fromより入荷日Toが過去日の場合エラー()
            '  いずれも設定済 である場合のみチェック
            If .imdSysEntDate.TextValue.Equals(String.Empty) = False _
                And .imdSysEntDateTo.TextValue.Equals(String.Empty) = False Then

                If .imdSysEntDateTo.TextValue < .imdSysEntDate.TextValue Then

                    '入荷日Fromより入荷日Toが過去日の場合エラー
                    '2015.10.26 tusnehira add
                    '英語化対応
                    Me.ShowMessage("E615")
                    'Me.ShowMessage("E039", New String() {"入荷日To", "入荷日From"})
                    .imdSysEntDate.BackColorDef() = GetAttentionBackColor()
                    .imdSysEntDateTo.BackColorDef() = GetAttentionBackColor()
                    .imdSysEntDate.Focus()
                    Return False

                End If

            End If
            '追加開始 2015.05.15 要望番号:2292

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk(ByVal g As LMB040G) As Boolean

        With Me._Frm
            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '正式名
            vCell.SetValidateCell(0, g.sprDetailDef.GOODS_NM.ColNo)
            vCell.ItemName = "商品名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品コード
            vCell.SetValidateCell(0, g.sprDetailDef.GOODS_CD_CUST.ColNo)
            vCell.ItemName = "商品コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ロットNO
            vCell.SetValidateCell(0, g.sprDetailDef.LOT_NO.ColNo)
            vCell.ItemName = "ロットNO"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '2014.02.17 WIT対応START
            'シリアル№
            vCell.SetValidateCell(0, g.sprDetailDef.SERIAL_NO.ColNo)
            vCell.ItemName = "シリアル№"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '2014.02.17 WIT対応END

#If True Then ' JT物流入荷検品対応 20160726 added inoue
            ' 製造日
            vCell.SetValidateCell(0, g.sprDetailDef.GOODS_CRT_DATE.ColNo)
            vCell.ItemName = "製造日"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 8
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
            ' 賞味有効期限
            vCell.SetValidateCell(0, g.sprDetailDef.LT_DATE.ColNo)
            vCell.ItemName = "賞味有効期限"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 8
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
#End If
            '置場
            vCell.SetValidateCell(0, g.sprDetailDef.OKIBA.ColNo)
            vCell.ItemName = "置場"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 19
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
    Friend Function IsSelectedCheck(ByVal g As LMB040G) As Boolean

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
        If Me.IsIncludeNoGoods(g) = False Then
            MyBase.ShowMessage("E549")
            Return False
        End If

        '商品なしデータを含んでいた場合、エラー
        If Me.IsIncludeAnyGoods(g) = False Then
            MyBase.ShowMessage("E550")
            Return False
        End If

        '削除商品データを含んでいた場合、エラー
        If Me.IsIncludeDelGoods(g) = False Then
            MyBase.ShowMessage("E551")
            Return False
        End If

        '荷主コード(中)違い商品データを含んでいた場合、エラー
        If Me.IsInclideDiffCustMGoods(g) = False Then
            MyBase.ShowMessage("E601")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 商品なしデータを含んでいた場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIncludeNoGoods(ByVal g As LMB040G) As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet

                If LMB040C.GATCH_KBN_NON.Equals(_Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), g.sprDetailDef.MST_EXISTS_KBN.ColNo))) = True Then
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
    Private Function IsIncludeAnyGoods(ByVal g As LMB040G) As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet

                If LMB040C.GATCH_KBN_ANY.Equals(_Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), g.sprDetailDef.MST_EXISTS_KBN.ColNo))) = True Then
                    Return False
                End If
            End With

        Next

        Return True

    End Function

    ''' <summary>
    ''' 削除商品データを含んでいた場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsIncludeDelGoods(ByVal g As LMB040G) As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet

                If LMB040C.GATCH_KBN_DEL.Equals(_Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), g.sprDetailDef.MST_EXISTS_KBN.ColNo))) = True Then
                    Return False
                End If
            End With

        Next

        Return True

    End Function

    ''' <summary>
    ''' 荷主コード(中)違い商品データを含んでいた場合、エラー
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsInclideDiffCustMGoods(ByVal g As LMB040G) As Boolean

        Dim chkList As ArrayList
        'チェックされた行番号取得
        chkList = New ArrayList()
        chkList = Me.getCheckList()
        Dim lngcnt As Integer = chkList.Count() - 1

        '値設定
        For i As Integer = 0 To lngcnt
            With _Frm.sprDetail.ActiveSheet

                If LMB040C.GATCH_KBN_DIFF_CUST_M.Equals(_Vcon.GetCellValue(.Cells(Integer.Parse(chkList(i).ToString()), g.sprDetailDef.MST_EXISTS_KBN.ColNo))) = True Then
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
        Dim defNo As Integer = LMB040C.SprColumnIndex.DEF

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
    Friend Function IsCallGoodsPop(ByVal targetRowIndex As Integer, ByVal g As LMB040G) As Boolean

        If LMB040C.GATCH_KBN_ANY.Equals(_Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(targetRowIndex, g.sprDetailDef.MST_EXISTS_KBN.ColNo))) = True Then
            '選択されたRowIndexを設定
            Return True
        End If
#If False Then ' フィルメニッヒ入荷検品対応 20170310 changed by inoue  
        Return False
#Else
        Return Me.IsCallGoodsPopUnconditionally(_Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(targetRowIndex, g.sprDetailDef.NRS_BR_CD.ColNo)) _
                                              , _Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(targetRowIndex, g.sprDetailDef.CUST_CD_L.ColNo)))
#End If
    End Function

#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 


    ''' <summary>
    ''' 同じ商品マスターの値を反映して良いか判定する(メッセージ付)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' 
    ''' </remarks>
    Friend Function IsSetSameGoodsDataWithMessage(ByVal masterRowIndex As Integer _
                                                , ByVal slaveRowIndex As Integer, ByVal g As LMB040G) As Boolean


        If (Me.IsSetSameGoodsData(masterRowIndex, slaveRowIndex, g) = False) Then

            Me.ShowMessage("E936", New String() {slaveRowIndex.ToString()})

            Return False
        End If


        Return True


    End Function

    ''' <summary>
    ''' 同じ商品マスターの値を反映して良いか判定する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' 
    ''' </remarks>
    Friend Function IsSetSameGoodsData(ByVal masterRowIndex As Integer _
                                     , ByVal slaveRowIndex As Integer, ByVal g As LMB040G) As Boolean

        If (masterRowIndex = slaveRowIndex) Then Return True

        'ロット, 商品名, 入目が一致している場合のみ許可
        Return Me.IsCellValuEequal(g.sprDetailDef.GOODS_NM.ColNo, masterRowIndex, slaveRowIndex) AndAlso _
               Me.IsCellValuEequal(g.sprDetailDef.LOT_NO.ColNo, masterRowIndex, slaveRowIndex) AndAlso _
               Me.IsCellValuEequal(g.sprDetailDef.IRIME.ColNo, masterRowIndex, slaveRowIndex) AndAlso _
               Me.IsCellValuEequal(g.sprDetailDef.IRIME_UT.ColNo, masterRowIndex, slaveRowIndex)

    End Function


    ''' <summary>
    ''' 二つの行のセルの値が一致するか判定する
    ''' </summary>
    ''' <param name="columnNo"></param>
    ''' <param name="rowIndex1"></param>
    ''' <param name="rowIndex2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCellValuEequal(ByVal columnNo As Integer _
                                   , ByVal rowIndex1 As Integer _
                                   , ByVal rowIndex2 As Integer) As Boolean

        With Me._Frm.sprDetail.ActiveSheet
            Return _Vcon.GetCellValue(.Cells(rowIndex1, columnNo)) _
                        .Equals(_Vcon.GetCellValue(.Cells(rowIndex2, columnNo)))
        End With
    End Function



    ''' <summary>
    ''' チェックされた商品を同期して変更するか判定する
    ''' </summary>
    ''' <param name="targetRowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DoChangeCheckedGoods(ByVal targetRowIndex As Integer, ByVal g As LMB040G) As Boolean

        Return Me.IsCallGoodsPopUnconditionally(_Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(targetRowIndex, g.sprDetailDef.NRS_BR_CD.ColNo)) _
                                              , _Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(targetRowIndex, g.sprDetailDef.CUST_CD_L.ColNo)))

    End Function


    ''' <summary>
    ''' ステータスに依存せず、商品ポップを呼び出す荷主か判定する
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="custCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCallGoodsPopUnconditionally(ByVal nrsBrCd As String _
                                                , ByVal custCd As String) As Boolean

        If (String.IsNullOrEmpty(nrsBrCd) OrElse _
            String.IsNullOrEmpty(custCd)) Then

            Return False
        End If

        Return LMB040C.M_GOODS_REF_KBN.ALL_STATUS _
            .Equals(Me.GetGoodsRefKbn(nrsBrCd, custCd))

    End Function

    ''' <summary>
    ''' 荷主詳細マスタより商品M参照区分を取得する
    ''' </summary>
    ''' <param name="nrsBrCd">営業所コード</param>
    ''' <param name="custCd">荷主コード</param>
    ''' <returns>
    ''' 商品M参照区分
    ''' </returns>
    ''' <remarks>
    ''' 0:標準(△のみ参照可)
    ''' 1:全許可(状態[×、△、●等]を問わず参照可)
    ''' </remarks>
    Private Function GetGoodsRefKbn(ByVal nrsBrCd As String, ByVal custCd As String) As String

        Const COLUMN_NM_SUB_KB As String = "SUB_KB"

        Dim goodsRefKbn As String = ""
        Dim row As DataRow _
            = Me._Vcon.SelectCustDetailsListDataRow(nrsBrCd, custCd) _
              .Where(Function(r) LMB040C.M_GOODS_REF_KBN.DEFINITION.SUB_KBN.Equals(r.Item(COLUMN_NM_SUB_KB))).FirstOrDefault()

        If (row IsNot Nothing AndAlso _
            row.Item(LMB040C.M_GOODS_REF_KBN.DEFINITION.VALUE_COLUMN_NM) IsNot Nothing) Then

            goodsRefKbn = row.Item(LMB040C.M_GOODS_REF_KBN.DEFINITION.VALUE_COLUMN_NM).ToString()

        End If

        Return goodsRefKbn

    End Function

#End If


#End Region 'Method

End Class
