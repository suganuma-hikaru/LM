' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG030V : 保管料荷役料明細編集
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMG030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG030F


    Private _Vcon As LMGControlV

    'START YANAI 20111014 一括変更追加
    Private _Hcon As LMGControlH
    'END YANAI 20111014 一括変更追加

#End Region 'Field

#Region "Constructor"

    'START YANAI 20111014 一括変更追加
    '''' <summary>
    '''' コンストラクタ
    '''' </summary>
    '''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    '''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    '''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    'Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG030F, ByVal v As LMGControlV)
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG030F, ByVal v As LMGControlV, ByVal h As LMGControlH)
        'END YANAI 20111014 一括変更追加

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        'START YANAI 20111014 一括変更追加
        Me._Hcon = h
        'END YANAI 20111014 一括変更追加

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal SHUBETSU As LMG030C.EventShubetsu) As Boolean

        '権限の設定
        Dim _Kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case _Kengen
            Case LMConst.AuthoKBN.VIEW                 '閲覧者
                Select Case SHUBETSU
                    '印刷時
                    'START YANAI 20111014 一括変更追加
                    'Case LMG030C.EventShubetsu.HENSHU _
                    ', LMG030C.EventShubetsu.TORIKOMI _
                    ', LMG030C.EventShubetsu.SAVE _
                    ', LMG030C.EventShubetsu.PRINT
                    Case LMG030C.EventShubetsu.HENSHU _
                        , LMG030C.EventShubetsu.TORIKOMI _
                        , LMG030C.EventShubetsu.SAVE _
                        , LMG030C.EventShubetsu.PRINT _
                        , LMG030C.EventShubetsu.IKKATU
                        'END YANAI 20111014 一括変更追加
                        MyBase.ShowMessage("E016")
                        Return False
                    Case Else                            '編集・取込・保存・印刷以外はTrueを返却
                        Return True
                End Select
        End Select

        '上記以外の権限区分はTrueを返却
        Return True

    End Function

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal SHUBETSU As LMG030C.EventShubetsu) As Boolean

        With Me._Frm

            '印刷種別
            .cmbPrint.ItemName = "印刷種別"
            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存処理時入力データチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSaveDataCheck() As Boolean

        With Me._Frm

            '【保管料】積数　1期
            If Convert.ToDecimal(.numSekiNb1.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】積数　1期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numSekiNb1)
                Return False
            End If
            If Convert.ToDecimal(.numSekiNb1.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】積数　1期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numSekiNb1)
                Return False
            End If

            '【保管料】積数　2期
            If Convert.ToDecimal(.numSekiNb2.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】積数　2期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numSekiNb1)
                Return False
            End If
            If Convert.ToDecimal(.numSekiNb2.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】積数　2期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numSekiNb1)
                Return False
            End If

            '【保管料】積数　3期
            If Convert.ToDecimal(.numSekiNb3.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】積数　3期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numSekiNb1)
                Return False
            End If
            If Convert.ToDecimal(.numSekiNb3.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】積数　3期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numSekiNb1)
                Return False
            End If

            '【保管料】単価　1期
            If Convert.ToDecimal(.numHokanTnk1.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】単価　1期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numHokanTnk1)
                Return False
            End If
            If Convert.ToDecimal(.numHokanTnk1.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】単価　1期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numHokanTnk1)
                Return False
            End If

            '【保管料】単価　2期
            If Convert.ToDecimal(.numHokanTnk2.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】単価　2期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numHokanTnk2)
                Return False
            End If
            If Convert.ToDecimal(.numHokanTnk2.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】単価　2期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numHokanTnk2)
                Return False
            End If

            '【保管料】単価　3期
            If Convert.ToDecimal(.numHokanTnk3.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】単価　3期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numHokanTnk3)
                Return False
            End If
            If Convert.ToDecimal(.numHokanTnk3.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】単価　3期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numHokanTnk3)
                Return False
            End If

            '【保管料】保管料
            'START YANAI 20111013 保管料自動計算廃止
            'If Convert.ToDecimal(.lblHokanAmt.TextValue) < 0 = True Then
            If Convert.ToDecimal(.numHokanAmt.TextValue) < 0 = True Then
                'END YANAI 20111013 保管料自動計算廃止
                MyBase.ShowMessage("E014", New String() {"【保管料】保管料", "0", "999999999.999"})
                'START YANAI 20111013 保管料自動計算廃止
                'Me._Vcon.SetErrorControl(.lblHokanAmt)
                Me._Vcon.SetErrorControl(.numHokanAmt)
                'END YANAI 20111013 保管料自動計算廃止
                Return False
            End If
            'START YANAI 20111013 保管料自動計算廃止
            'If Convert.ToDecimal(.lblHokanAmt.TextValue) > 999999999.999 = True Then
            If Convert.ToDecimal(.numHokanAmt.TextValue) > 999999999.999 = True Then
                'END YANAI 20111013 保管料自動計算廃止
                MyBase.ShowMessage("E014", New String() {"【保管料】保管料", "0", "999999999.999"})
                'START YANAI 20111013 保管料自動計算廃止
                'Me._Vcon.SetErrorControl(.lblHokanAmt)
                Me._Vcon.SetErrorControl(.numHokanAmt)
                'END YANAI 20111013 保管料自動計算廃止
                Return False
            End If

            '【保管料】変動保管料
            If Convert.ToDecimal(.numVarHokanAmt.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】変動保管料", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numVarHokanAmt)
                Return False
            End If
            If Convert.ToDecimal(.numVarHokanAmt.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【保管料】変動保管料", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numVarHokanAmt)
                Return False
            End If

            '【荷役料】入庫高
            If Convert.ToDecimal(.numInNb.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】入庫高", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numInNb)
                Return False
            End If
            If Convert.ToDecimal(.numInNb.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】入庫高", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numInNb)
                Return False
            End If

            '【荷役料】（入庫）単価　1期
            If Convert.ToDecimal(.numNiyakuInTnk1.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（入庫）単価　1期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuInTnk1)
                Return False
            End If
            If Convert.ToDecimal(.numNiyakuInTnk1.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（入庫）単価　1期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuInTnk1)
                Return False
            End If

            '【荷役料】（入庫）単価　2期
            If Convert.ToDecimal(.numNiyakuInTnk2.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（入庫）単価　2期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuInTnk2)
                Return False
            End If
            If Convert.ToDecimal(.numNiyakuInTnk2.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（入庫）単価　2期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuInTnk2)
                Return False
            End If

            '【荷役料】（入庫）単価　3期
            If Convert.ToDecimal(.numNiyakuInTnk3.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（入庫）単価　3期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuInTnk3)
                Return False
            End If
            If Convert.ToDecimal(.numNiyakuInTnk3.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（入庫）単価　3期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuInTnk3)
                Return False
            End If

            '【荷役料】出庫高
            If Convert.ToDecimal(.numOutNb.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】出庫高", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numOutNb)
                Return False
            End If
            If Convert.ToDecimal(.numOutNb.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】出庫高", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numOutNb)
                Return False
            End If

            '【荷役料】（出庫）単価　1期
            If Convert.ToDecimal(.numNiyakuOutTnk1.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（出庫）単価　1期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuOutTnk1)
                Return False
            End If
            If Convert.ToDecimal(.numNiyakuOutTnk1.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（出庫）単価　1期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuOutTnk1)
                Return False
            End If

            '【荷役料】（出庫）単価　2期
            If Convert.ToDecimal(.numNiyakuOutTnk2.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（出庫）単価　2期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuOutTnk2)
                Return False
            End If
            If Convert.ToDecimal(.numNiyakuOutTnk2.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（出庫）単価　2期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuOutTnk2)
                Return False
            End If

            '【荷役料】（出庫）単価　3期
            If Convert.ToDecimal(.numNiyakuOutTnk3.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（出庫）単価　3期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuOutTnk3)
                Return False
            End If
            If Convert.ToDecimal(.numNiyakuOutTnk3.TextValue) > 999999999.999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】（出庫）単価　3期", "0", "999999999.999"})
                Me._Vcon.SetErrorControl(.numNiyakuOutTnk3)
                Return False
            End If

            '【荷役料】荷役料
            If Convert.ToDecimal(.numNiyakuAmt.TextValue) < 0 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】荷役料", "0", "999999999"})
                Me._Vcon.SetErrorControl(.numNiyakuAmt)
                Return False
            End If
            If Convert.ToDecimal(.numNiyakuAmt.TextValue) > 999999999 = True Then
                MyBase.ShowMessage("E014", New String() {"【荷役料】荷役料", "0", "999999999"})
                Me._Vcon.SetErrorControl(.numNiyakuAmt)
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <param name="SHUBETSU"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function isRelationCheck(ByVal SHUBETSU As LMG030C.EventShubetsu) As Boolean

        Dim NrsHantei As Boolean = True

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        '営業所相違チェック
        'If LMUserInfoManager.GetNrsBrCd.Equals(Me._Frm.lblNrsBrCd.TextValue) = False Then
        '    NrsHantei = False
        'End If

        Select Case SHUBETSU
            Case LMG030C.EventShubetsu.HENSHU                       '編集処理

                '営業所相違チェック
                If NrsHantei = False = True Then
                    ShowMessage("E178", New String() {"処理"})
                    Return False
                End If

                '編集部編集対象有無チェック
                If String.IsNullOrEmpty(Me._Frm.lblCustNm2.TextValue) = True Then
                    ShowMessage("E033")
                    Return False
                End If

            Case LMG030C.EventShubetsu.TORIKOMI                      '取込処理

                '営業所相違チェック
                If NrsHantei = False = True Then
                    ShowMessage("E178", New String() {"取込"})
                    Return False
                End If

        End Select

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm

            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprMeisaiPrt)

            '商品コード
            vCell.SetValidateCell(0, LMG030G.sprMeisaiPrtDef.CUST_GOODS_CD.ColNo)
            vCell.ItemName = "商品コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If Me.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品名
            vCell.SetValidateCell(0, LMG030G.sprMeisaiPrtDef.CUST_GOODS_NM.ColNo)
            vCell.ItemName = "商品名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If Me.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ロット№
            vCell.SetValidateCell(0, LMG030G.sprMeisaiPrtDef.LOT_NO.ColNo)
            vCell.ItemName = "ロット№"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If Me.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'シリアル№
            vCell.SetValidateCell(0, LMG030G.sprMeisaiPrtDef.SERIAL_NO.ColNo)
            vCell.ItemName = "シリアル№"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If Me.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名称（小・極小）
            vCell.SetValidateCell(0, LMG030G.sprMeisaiPrtDef.CUST_NM_S_SS.ColNo)
            vCell.ItemName = "荷主名称（小・極小）"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 120
            If Me.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsIkkatuCheck(ByVal SHUBETSU As LMG030C.EventShubetsu) As Boolean

        With Me._Frm

            '修正項目
            .cmbIkkatu.ItemName = "修正項目"
            .cmbIkkatu.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbIkkatu) = False Then
                Return False
            End If

            '範囲チェック(一括変更値)
            If Convert.ToDecimal(.numIkkatu.TextValue) < 0 = True Then
                If ("07").Equals(.cmbIkkatu.SelectedValue) = False Then
                    MyBase.ShowMessage("E014", New String() {String.Concat(.cmbIkkatu.SelectedText, "の変更後の値"), "0", "999999999.999"})
                Else
                    MyBase.ShowMessage("E014", New String() {String.Concat(.cmbIkkatu.SelectedText, "の変更後の値"), "0", "999999999"})
                End If
                Me._Vcon.SetErrorControl(.numIkkatu)
                Return False
            End If

            '選択チェック
            Dim arr As ArrayList = Nothing
            arr = Me._Hcon.GetCheckList(.sprMeisaiPrt.ActiveSheet, LMG030C.SprColumnIndex.DEF)
            If arr.Count = 0 Then
                MyBase.ShowMessage("E009")
                Return False
            End If

        End With

        Return True

    End Function
    'END YANAI 20111014 一括変更追加

#End Region 'Method

End Class
