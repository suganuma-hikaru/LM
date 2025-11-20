' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH070V : 先行手入力入出荷とEDIの紐付け
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMH070Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMH070V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH070F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMHControlV

    ''' <summary>
    ''' Gamen共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMHControlG

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH070F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Gcon = New LMHControlG(frm)

        'Validate共通クラスの設定
        Me._Vcon = New LMHControlV(handlerClass, DirectCast(frm, Form), _Gcon)

        Me._ChkList = New ArrayList()

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMH070C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMH070C.EventShubetsu.KENSAKU          '検索

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

            Case LMH070C.EventShubetsu.HOZON          '保存

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

#End Region '権限チェック

#Region "検索時チェック"
    ''' <summary>
    ''' 検索時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuCheck() As Boolean

        With Me._Frm
            '空白削除
            .txtInOutkaMngNo.TextValue = .txtInOutkaMngNo.TextValue.Trim().ToUpper()

            .txtInOutkaMngNo.ItemName() = "入荷管理番号(大)"
            .txtInOutkaMngNo.IsHissuCheck() = True
            .txtInOutkaMngNo.IsForbiddenWordsCheck() = True
            .txtInOutkaMngNo.IsFullByteCheck() = 9

            If MyBase.IsValidateCheck(.txtInOutkaMngNo) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索結果チェック処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuResultCheck() As Boolean

        With Me._Frm

            'EDIと紐付いていたらエラー
            If String.IsNullOrEmpty(Me._Vcon.GetCellValue(.sprGoodsInfoInOutka.ActiveSheet.Cells(0, LMH070G.sprGoodsInfoInOutkaDef.EDI_NO.ColNo))) = False Then
                Me.ShowMessage("E364")
                Return False
            End If

            With Me._Frm.sprGoodsInfoInOutka.ActiveSheet
                Dim inoutMax As Integer = .RowCount - 1
                Dim htDupl As Hashtable = New Hashtable
                Dim ediNo As String = String.Empty
                Dim lotNo As String = String.Empty

                For i As Integer = 0 To inoutMax
                    ediNo = Me._Vcon.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoInOutkaDef.INOUTKA_CTL_NO_M.ColNo))
                    lotNo = Me._Vcon.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoInOutkaDef.LOT_NO.ColNo))

                    If htDupl.ContainsKey(ediNo) = True Then
                        'メッセージエリアの設定
                        MyBase.ShowMessage("E379", New String() {ediNo.ToString()})
                        Return False
                    Else
                        htDupl.Add(ediNo, String.Empty)
                    End If

                Next

            End With

            '中件数が一致しなければエラー
            If .sprGoodsInfoInOutka.ActiveSheet.RowCount <> .sprGoodsInfoEdi.ActiveSheet.RowCount Then
                Me.ShowMessage("E217", New String() {"EDIデータの中件数", "紐付け対象の中件数"})
                Return False
            End If
            '荷主コードが一致しない場合エラー
            If .txtCustCD_L.TextValue <> .txtEdiCustCD_L.TextValue OrElse .txtCustCD_M.TextValue <> .txtEdiCustCD_M.TextValue Then
                Me.ShowMessage("E217", New String() {"EDIデータの荷主", "紐付け対象の荷主"})
                Return False
            End If
            '届先コードが一致しない場合エラー
            If .txtDestCd.TextValue <> .txtDestCd_Edi.TextValue Then
                Me.ShowMessage("E217", New String() {"EDIデータの届先", "紐付け対象の届先"})
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsHozonCheck() As Boolean

        With Me._Frm.sprGoodsInfoEdi.ActiveSheet

            Dim ediMax As Integer = .RowCount - 1
            Dim inoutMax As Integer = Me._Frm.sprGoodsInfoInOutka.ActiveSheet.RowCount - 1
            Dim htDupl As Hashtable = New Hashtable
            Dim htLink As Hashtable = New Hashtable
            Dim linkNo As String = String.Empty
            Dim ediNoM As String = String.Empty
            Dim msg As String = String.Empty
            Dim flgComp As Boolean = False

            For i As Integer = 0 To ediMax

                linkNo = Me._Vcon.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.LINK_NO.ColNo))

                '紐付け番号が未入力の場合エラー
                If String.IsNullOrEmpty(linkNo) = True Then
                    Me.ShowMessage("E340", New String() {"紐付け番号"})
                    Return False
                End If

                '紐付け番号が重複している場合エラー
                If htDupl.ContainsKey(linkNo) = True Then
                    msg = String.Concat("紐付け番号:", linkNo)
                    'メッセージエリアの設定
                    MyBase.ShowMessage("E022", New String() {msg})
                    Return False
                Else
                    htDupl.Add(linkNo, String.Empty)
                End If

                '紐付け番号と管理番号が一致しない場合エラー
                flgComp = False
                For ii As Integer = 0 To inoutMax
                    ediNoM = Me._Vcon.GetCellValue(_Frm.sprGoodsInfoInOutka.ActiveSheet.Cells(ii, _
                                                       LMH070G.sprGoodsInfoInOutkaDef.INOUTKA_CTL_NO_M.ColNo))

                    If linkNo.Equals(ediNoM) Then
                        '一致した場合
                        flgComp = True
                        htLink.Add(i, ii)
                        Exit For
                    Else
                    End If

                Next

                If flgComp = False Then
                    Me.ShowMessage("E217", New String() {"紐付け番号", linkNo})
                    Me._Vcon.SetErrorControl(Me._Frm.sprGoodsInfoEdi, i, LMH070G.sprGoodsInfoEdiDef.LINK_NO.ColNo)
                    Return False
                End If

                Dim inoutRowNo As Integer = Convert.ToInt32(htLink(i))
                Dim inoutGoodsCdNrs As String = Me._Vcon.GetCellValue _
                                    (_Frm.sprGoodsInfoInOutka.ActiveSheet.Cells(inoutRowNo _
                                    , LMH070G.sprGoodsInfoInOutkaDef.GOODS_CD_NRS.ColNo))

                Dim inoutGoodsCdCust As String = Me._Vcon.GetCellValue _
                                    (_Frm.sprGoodsInfoInOutka.ActiveSheet.Cells(inoutRowNo _
                                    , LMH070G.sprGoodsInfoInOutkaDef.GOODS_CD_CUST.ColNo))

                Dim ediGoodsCdNrs As String = Me._Vcon.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.GOODS_CD_NRS.ColNo))
                Dim ediGoodsCdCust As String = Me._Vcon.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.GOODS_CD_CUST.ColNo))

                '商品が一致しない場合エラー
                'Nrs商品コードで比較
                If String.IsNullOrEmpty(ediGoodsCdNrs) = False Then
                    If ediGoodsCdNrs.Equals(inoutGoodsCdNrs) = False Then
                        Me.ShowMessage("E367", New String() {"商品キー", linkNo})
                        Return False
                    End If
                    '荷主商品コードで比較
                Else
                    If ediGoodsCdCust.Equals(inoutGoodsCdCust) = False Then
                        Me.ShowMessage("E367", New String() {"荷主商品コード", linkNo})
                        Return False
                    End If
                End If

                Dim inoutLotNo As String = Me._Vcon.GetCellValue _
                                    (_Frm.sprGoodsInfoInOutka.ActiveSheet.Cells(inoutRowNo _
                                    , LMH070G.sprGoodsInfoInOutkaDef.LOT_NO.ColNo))

                Dim ediLotNo As String = Me._Vcon.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.LOT_NO.ColNo))

                'ロット№が一致しない場合エラー
                If String.IsNullOrEmpty(inoutLotNo) = False AndAlso String.IsNullOrEmpty(ediLotNo) = False Then
                    If ediLotNo.Equals(inoutLotNo) = False Then
                        Me.ShowMessage("E367", New String() {"ロット№", linkNo})
                        Return False
                    End If
                End If

                Dim inoutIrime As String = Me._Vcon.GetCellValue _
                    (_Frm.sprGoodsInfoInOutka.ActiveSheet.Cells(inoutRowNo _
                              , LMH070G.sprGoodsInfoInOutkaDef.IRIME.ColNo))

                Dim ediIrime As String = Me._Vcon.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.IRIME.ColNo))

                '入目が一致しない場合エラー
                If ediIrime.Equals(inoutIrime) = False Then
                    Me.ShowMessage("E367", New String() {"入目", linkNo})
                    Return False
                End If

                Dim inoutNb As String = Me._Vcon.GetCellValue _
                    (_Frm.sprGoodsInfoInOutka.ActiveSheet.Cells(inoutRowNo _
                            , LMH070G.sprGoodsInfoInOutkaDef.NB.ColNo))

                Dim ediNb As String = Me._Vcon.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.NB.ColNo))

                '個数が一致しない場合エラー
                If ediNb.Equals(inoutNb) = False Then
                    Me.ShowMessage("E367", New String() {"個数", linkNo})
                    Return False
                End If

            Next

            '入出荷日が一致しない場合ワーニング
            If Me._Frm.imdInOutkaDate.TextValue.Equals(Me._Frm.imdInOutkaDate_Edi.TextValue) = False Then
                'メッセージの表示
                If Me.ShowMessage("W164") = MsgBoxResult.Cancel Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

#End Region

#End Region 'Method

End Class
