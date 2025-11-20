' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI960  : 出荷データ確認（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI960BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI960BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI960DAC = New LMI960DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 実績作成対象データ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SAKUSEI_TARGET As String = "LMI960SAKUSEI_TARGET"

    ''' <summary>
    ''' 処理制御データ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_PROC_CTRL As String = "LMI960PROC_CTRL"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(出荷ピック)データ元データ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_JISSEKI_DATA As String = "LMI960JISSEKI_DATA"

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI960IN"

    'ワーニング処理用データテーブル
    Public Const TABLE_NM_WARNING_HED As String = "WARNING_HED"
    Public Const TABLE_NM_WARNING_DTL As String = "WARNING_DTL"
    Public Const TABLE_NM_WARNING_SHORI As String = "WARNING_SHORI"

    ''' <summary>
    ''' ワーニングIDフォーマット
    ''' </summary>
    Public Class WARNING_ID_FMT
        Public Class MST_FLG
            ''' <summary>
            ''' マスタフラグ開始位置
            ''' </summary>
            Public Const START_IDX As Integer = 7

            ''' <summary>
            ''' マスタフラグ文字長
            ''' </summary>
            Public Const LEN As Integer = 1

            ''' <summary>
            ''' 指定なし
            ''' </summary>
            Public Const NONE As String = "0"

            ''' <summary>
            ''' 商品マスタ
            ''' </summary>
            Public Const M_GOODS As String = "1"

            ''' <summary>
            ''' 届先マスタ
            ''' </summary>
            Public Const M_DEST As String = "2"
        End Class

        Public Class UNIT_OF_WARNING
            ''' <summary>
            ''' 単位フラグ開始位置
            ''' </summary>
            Public Const START_IDX As Integer = 9

            ''' <summary>
            ''' 単位フラグ文字長
            ''' </summary>
            Public Const LEN As Integer = 1

            ''' <summary>
            ''' 出荷L単位
            ''' </summary>
            Public Const L As String = "L"

            ''' <summary>
            ''' 出荷M単位
            ''' </summary>
            Public Const M As String = "M"
        End Class
    End Class

    ''' <summary>
    ''' ワーニングID
    ''' </summary>
    ''' <remarks>
    ''' 桁数     意味
    ''' -----    ----------
    ''' 1-3      荷主ごとに一意な値。流用元(LMH030BLC)での付番ルールは不明。ハネウェル専用なので"###"とする
    ''' 4        常に"_"
    ''' 5-6      不明。流用元(LMH030BLC)では常に"OT"
    ''' 7        常に"_"
    ''' 8        マスタフラグ。WARNING_ID_FMTを参照
    ''' 9        常に"_"
    ''' 10       L:出荷Lレベル    M:出荷Mレベル
    ''' 11-13    1-3および10桁目が同一なものごとに001からの連番
    ''' </remarks>
    Public Class WARNING_ID
        ''' <summary>
        ''' 届先マスタ未登録
        ''' </summary>
        Public Const M_DEST_NOT_FOUND As String = "###_OT_0_L001"

        ''' <summary>
        ''' 届先マスタ未登録（運送発地）
        ''' </summary>
        Public Const M_DEST_NOT_FOUND_UNSO_ORIG As String = "###_OT_0_L002"

        ''' <summary>
        ''' 届先マスタ未登録（運送届先）
        ''' </summary>
        Public Const M_DEST_NOT_FOUND_UNSO_DEST As String = "###_OT_0_L003"

        ''' <summary>
        ''' 届先マスタ未登録（売上先）
        ''' </summary>
        Public Const M_DEST_NOT_FOUND_BUYID As String = "###_OT_0_L004"

        ''' <summary>
        ''' 商品を一意に特定できない
        ''' </summary>
        Public Const GOODS_NOT_IDENTIFIED As String = "###_OT_1_M001"

        ''' <summary>
        ''' 商品マスタ未登録
        ''' </summary>
        Public Const GOODS_NOT_FOUND As String = "###_OT_0_M002"
    End Class


    ''' <summary>
    ''' 進捗区分(受注ステータス)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ShinchokuKbJuchu As Integer
        Mishori = 1            '未処理
        JuchuOK                 '受注OK
        JuchuNG                 '受注NG
        NyuShukkaTourokuZumi    '入出荷/受注登録済
        JissekiSakuseiZumi      '実績作成済
        EdiTorikeshi            'EDI取消
    End Enum

    ''' <summary>
    ''' 場所区分(cmbBashoKb選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbBashoKbItems
        Public Const Tsumikomi As String = "積込場"
        Public Const Nioroshi As String = "荷下場"
        Public Const NonyuYotei As String = "納入予定"
    End Class
    'ADD E 2020/02/07 010901

    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    Public Class NrsBrCd
        Public Const Chiba As String = "10"
        Public Const Forwarding As String = "90"
    End Class

    ''' <summary>
    ''' 現場作業指示ステータス
    ''' </summary>
    Public Class WH_TAB_STATUS
        Public Const NOT_INSTRUCTED As String = "00"
        Public Const INSTRUCTED As String = "01"
        Public Const CHANGED As String = "02"
        Public Const UNNECESSARY As String = "99"
    End Class

    ''' <summary>
    ''' 現場作業指示ステータス
    ''' </summary>
    Public Class WH_TAB_YN
        Public Const NO As String = "00"
        Public Const YES As String = "01"
    End Class
#End Region

#Region "検索処理"

    ''' <summary>
    ''' 対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'メッセージコードの設定
        Call Me.SetSelectErrMes(MyBase.GetResultCount())

        Return ds

    End Function

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

#End Region

#Region "実績作成処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(出荷ピック)データの登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdi(ByVal ds As DataSet) As DataSet

        Dim inTblRow As DataRow = ds.Tables(LMI960BLC.TABLE_NM_IN).Rows(0)  'ADD 2020/02/07 010901

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow
        Dim max As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).NewRow
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        Dim dtJissekiData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_JISSEKI_DATA)

        For i As Integer = 0 To max

            drSakuseiTarget = dtSakuseiTarget.Rows(i)

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO") = i

            'DEL S 2020/02/07 010901
            ''ShipmentIDの重複チェック
            'ds = Me.DacAccess(ds, "SelectCntShipmentID")

            'If MyBase.GetResultCount() > 1 Then
            '    '複数件該当する場合はエラー
            '    MyBase.SetMessage("E494", New String() {String.Concat("Load Number=", drSakuseiTarget.Item("SHIPMENT_ID")), "ハネウェルEDI受信データ(ShipmentDetails)", ""})
            '    Return ds
            'End If
            'DEL E 2020/02/07 010901

            '作成対象のハネウェルＥＤＩ送信(出荷ピック)データの元データを取得
            ds = Me.DacAccess(ds, "SelectOutkaEdiData")

            If dtJissekiData.Rows.Count <> 1 Then
                '該当データなしの場合はエラー
                MyBase.SetMessage("E011")
                Return ds
            End If

            'ハネウェルＥＤＩ送信(出荷ピック)データの登録
            If inTblRow.Item("BASHO_KB").ToString = CmbBashoKbItems.NonyuYotei Then
                '納入予定
                ds = Me.DacAccess(ds, "InsertSendOutEdi2")
            Else
                '積込場・荷下場
                ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
            End If

            'ハネウェルＥＤＩ受信データ(Header)進捗区分更新
            ds = Me.DacAccess(ds, "UpdateOutkaEdiHedShinchoku")

            'ADD S 2020/02/07 010901
            If inTblRow.Item("SHINCHOKU_KB_JUCHU").ToString = CStr(CInt(ShinchokuKbJuchu.JissekiSakuseiZumi)) _
            AndAlso inTblRow.Item("BASHO_KB").ToString() = CmbBashoKbItems.Nioroshi Then
                'ハネウェルＥＤＩ受信データ(Header)進捗区分(受注ステータス)更新
                ds = Me.DacAccess(ds, "UpdateOutkaEdiHedShinchokuJuchu")
            End If
            'ADD E 2020/02/07 010901
        Next

        Return ds

    End Function

#End Region

    'ADD S 2019/12/12 009741
#Region "受注作成処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データの登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiJuchu(ByVal ds As DataSet) As DataSet

        Dim inTblRow As DataRow = ds.Tables(LMI960BLC.TABLE_NM_IN).Rows(0)  'ADD 2020/03/06 011377

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow
        Dim max As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).NewRow
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        Dim dtSendOutEdi As DataTable = ds.Tables("LMI960H_SENDOUTEDI_HWL")

        Dim dtJissekiData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_JISSEKI_DATA)

        Dim where As New System.Text.StringBuilder
        Dim wkDrArr() As DataRow

        ' 処理対象範囲の行の H_OUTKAEDI_HED_HWL の SYS_ENT_DATE, SYS_ENT_TIME の取得と
        ' 実績作成対象データへの値設定
        ds = Me.DacAccess(ds, "SelectHedSpmSendoutediByPkey")
        For i As Integer = 0 To max
            drSakuseiTarget = dtSakuseiTarget.Rows(i)
            where.Clear()
            where.Append(String.Concat("CRT_DATE = '", drSakuseiTarget.Item("HED_CRT_DATE").ToString(), "' AND "))
            where.Append(String.Concat("FILE_NAME = '", drSakuseiTarget.Item("HED_FILE_NAME").ToString(), "'"))
            wkDrArr = ds.Tables("LMI960_OUTKAEDI_HED").Select(where.ToString())
            If wkDrArr.Count > 0 Then
                drSakuseiTarget.Item("HED_SYS_ENT_DATE") = wkDrArr(0).Item("SYS_ENT_DATE").ToString()
                drSakuseiTarget.Item("HED_SYS_ENT_TIME") = wkDrArr(0).Item("SYS_ENT_TIME").ToString()
            Else
                ' 存在しないことは考えにくいが念のため
                drSakuseiTarget.Item("HED_SYS_ENT_DATE") = New String("0"c, "yyyyMMdd".Length)
                drSakuseiTarget.Item("HED_SYS_ENT_TIME") = New String("0"c, "HHmmssfff".Length)
            End If
        Next
        ' 実績作成対象データの並べ替え
        ' (応答データを時系列で作成するため。時系列での作成とならない場合はエラーと判定するため)
        Dim tmpDt As DataTable = dtSakuseiTarget.Clone()
        Dim tmpDr() As DataRow = Nothing
        tmpDr = dtSakuseiTarget.Select(String.Empty, "SHIPMENT_ID ASC, HED_SYS_ENT_DATE ASC, HED_SYS_ENT_TIME ASC")
        For Each row As DataRow In tmpDr
            tmpDt.ImportRow(row)
        Next
        Call dtSakuseiTarget.Clear()
        Call dtSakuseiTarget.Merge(tmpDt)

        For i As Integer = 0 To max

            drSakuseiTarget = dtSakuseiTarget.Rows(i)

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO") = i

            'DEL S 2020/02/07 010901
            ''ShipmentIDの重複チェック
            'ds = Me.DacAccess(ds, "SelectCntShipmentID")

            'If MyBase.GetResultCount() > 1 Then
            '    '複数件該当する場合はエラー
            '    MyBase.SetMessage("E494", New String() {String.Concat("Load Number=", drSakuseiTarget.Item("SHIPMENT_ID")), "ハネウェルEDI受信データ(ShipmentDetails)", ""})
            '    Return ds
            'End If
            'DEL E 2020/02/07 010901

            'ハネウェルＥＤＩ送信(受注可否)データ存在チェック
            ds = Me.DacAccess(ds, "SelectSendJuchuData")

            If dtSendOutEdi.Rows.Count = 0 Then
                '存在しない場合（受注ステータス戻し処理（受注OK⇒未処理）をしていない場合）

                '作成対象の存在チェック
                'ADD S 2020/03/06 011377
                If inTblRow.Item("SHINCHOKU_KB_JUCHU").ToString = CStr(CInt(ShinchokuKbJuchu.JuchuOK)) Then
                    '受注OK作成用存在チェック(出荷管理番号の存在もチェックする)
                    ds = Me.DacAccess(ds, "SelectCountOutkaEdiDataJuchuOK")
                Else
                    'ADD E 2020/03/06 011377
                    '受注作成用存在チェック
                    ds = Me.DacAccess(ds, "SelectCountOutkaEdiDataJuchu")
                End If  'ADD 2020/03/06 011377

                If MyBase.GetResultCount() <> 1 Then
                    '該当データなしの場合はエラー
                    MyBase.SetMessage("E011")
                    Return ds
                End If

                ' Booked解除送信処理関連
                ' 受注送信順序チェック
                ' 対象行と 同一の Load Number で、対象行より後に受信し、かつ
                ' 応答レコードがある H_OUTKAEDI_HED_HWL の CRT_DATE, FILE_NAME の取得
                ds = Me.DacAccess(ds, "SelectHedSpmSendoutediByShipmentId")
                If ds.Tables("LMI960_OUTKAEDI_HED").Rows.Count > 0 Then
                    MyBase.SetMessageStore("00",
                                       "E428",
                                       {"同一Load Numberで、対象行より後に受信し、先に受注送信が済んでいる受注がある", "、受注送信", ""},
                                       drSakuseiTarget.Item("ROW_NO").ToString(),
                                       "Load Number",
                                       drSakuseiTarget.Item("SHIPMENT_ID").ToString())
                    Continue For
                End If

                ' Booked解除送信処理 実行判定
                ' 対象行と 同一の Load Number で、対象行より前に受信し、かつ
                ' 応答レコードがある納入日情報と、対象行の納入日情報の取得
                ' および上記レコード間で納入日変更ありか否かの判定
                ' (画面処理対象行分も取得する。
                '  添字行と (添字 + 1)行を比較するので添字上限は 0起算で(取得件数 - 2)までとする)
                ds = Me.DacAccess(ds, "SelectSendoutediOutkaediHedDtlStp")
                For j As Integer = 0 To ds.Tables("LMI960_SENDOUTEDI_OUTKAEDI_DTL_STP").Rows.Count - 2
                    Dim drStpLast As DataRow = ds.Tables("LMI960_SENDOUTEDI_OUTKAEDI_DTL_STP").Rows(j)
                    Dim drStpCurrent As DataRow = ds.Tables("LMI960_SENDOUTEDI_OUTKAEDI_DTL_STP").Rows(j + 1)
                    If drStpLast.Item("SCHEDULE_START_DATE_TIME").ToString() <> drStpCurrent.Item("SCHEDULE_START_DATE_TIME").ToString() OrElse
                        drStpLast.Item("SCHEDULE_END_DATE_TIME").ToString() <> drStpCurrent.Item("SCHEDULE_END_DATE_TIME").ToString() OrElse
                        drStpLast.Item("REQUEST_START_DATE_TIME").ToString() <> drStpCurrent.Item("REQUEST_START_DATE_TIME").ToString() Then
                        ' 対象行と 同一の Load Number で、対象行より前に受信し、かつ
                        ' 応答レコードがある納入日情報と、対象行の納入日情報の間で変更ありの場合

                        ' 納入日変更前レコードに対する Booked解除レコードが作成済みか否かの判定
                        ds.Tables("LMI960_SELECT_SENDOUTEDI_BY_PKEY_IN").Clear()
                        Dim row As DataRow = ds.Tables("LMI960_SELECT_SENDOUTEDI_BY_PKEY_IN").NewRow()
                        row("CRT_DATE") = drStpLast.Item("CRT_DATE").ToString()
                        row("FILE_NAME") = drStpLast.Item("FILE_NAME").ToString()
                        ds.Tables("LMI960_SELECT_SENDOUTEDI_BY_PKEY_IN").Rows.Add(row)
                        ds = Me.DacAccess(ds, "SelectSendoutedi990CancelOr214CancelByPkey")
                        Dim sendoutedi990CancelOr214CancelCount As Integer = MyBase.GetResultCount()
                        If sendoutedi990CancelOr214CancelCount = 0 Then
                            ' 納入日変更前レコードに対する Booked解除レコードが作成されていない場合

                            ds = Me.DacAccess(ds, "SelectSendoutedi990Or214ByPkey")
                            Dim sendoutedi990Or214Count As Integer = MyBase.GetResultCount()
                            If sendoutedi990Or214Count = 2 Then
                                ' 納入日変更前レコードに対して注文応答990および注文応答214(受注OK)の両レコードが存在する場合

                                ' ハネウェルＥＤＩ送信(受注可否)データ登録(990 取消)
                                ds = Me.DacAccess(ds, "InsertSendOutEdiJuchuCancel")

                                'ハネウェルＥＤＩ送信(受注可否)データの登録(214 取消)
                                ds = Me.DacAccess(ds, "InsertSendOutEdiJuchu214Cancel")
                            ElseIf sendoutedi990Or214Count = 1 Then
                                ' 納入日変更前レコードに対して注文応答990のレコードのみが存在する場合

                                ' ハネウェルＥＤＩ送信(受注可否)データ登録(990 取消)
                                ds = Me.DacAccess(ds, "InsertSendOutEdiJuchuCancel")
                            End If
                        End If
                    End If
                Next

                'ハネウェルＥＤＩ送信(受注可否)データの登録(990)
                ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

                'ADD S 2020/03/06 011377
                If inTblRow.Item("SHINCHOKU_KB_JUCHU").ToString = CStr(CInt(ShinchokuKbJuchu.JuchuOK)) Then
                    'ハネウェルＥＤＩ送信(受注可否)データの登録(214)
                    ds = Me.DacAccess(ds, "InsertSendOutEdiJuchu214")
                End If
                'ADD E 2020/03/06 011377

            ElseIf inTblRow.Item("SHINCHOKU_KB_JUCHU").ToString = CStr(CInt(ShinchokuKbJuchu.JuchuNG)) Then
                '受注ステータス戻し処理（受注OK⇒未処理）後に受注NGにしようとした場合
                MyBase.SetMessageStore("00",
                                       "E428",
                                       {"すでに受注可否Yesとして受注送信済みの", "、受注可否Noに", ""},
                                       drSakuseiTarget.Item("ROW_NO").ToString,
                                       "Load Number",
                                       drSakuseiTarget.Item("SHIPMENT_ID").ToString)
                Continue For

            End If

            'ハネウェルＥＤＩ受信データ(Header)進捗区分(受注ステータス)更新
            ds = Me.DacAccess(ds, "UpdateOutkaEdiHedShinchokuJuchu")

            If MyBase.GetResultCount() <> 1 Then
                '該当データなしの場合はエラー
                MyBase.SetMessage("E011")
                Return ds
            End If

        Next

        Return ds

    End Function

#End Region
    'ADD E 2019/12/12 009741

#Region "遅延送信処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(遅延理由)データの登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiDelay(ByVal ds As DataSet) As DataSet

        Dim inTblRow As DataRow = ds.Tables(LMI960BLC.TABLE_NM_IN).Rows(0)

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow
        Dim max As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).NewRow
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        Dim dtSendOutEdi As DataTable = ds.Tables("LMI960H_SENDOUTEDI_HWL")

        Dim dtJissekiData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_JISSEKI_DATA)

        For i As Integer = 0 To max

            drSakuseiTarget = dtSakuseiTarget.Rows(i)

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO") = i


            'ハネウェルＥＤＩ送信(遅延理由)データ存在チェック
            ds = Me.DacAccess(ds, "SelectSendDelayData")

            If dtSendOutEdi.Rows.Count > 0 Then
                '存在する場合はエラー
                'MyBase.SetMessageStore("00",
                '                       "E263",
                '                       {"配送遅延データ", "作成", "作成日時：" & FormatDateString(dtSendOutEdi.Rows(0).Item("CREAT_DATE_TIME").ToString)},
                '                       drSakuseiTarget.Item("ROW_NO").ToString,
                '                       "Load Number",
                '                       drSakuseiTarget.Item("SHIPMENT_ID").ToString)
                'Continue For
                MyBase.SetMessage("E011")
                Return ds
            End If


            '作成対象のハネウェルＥＤＩ送信(遅延理由)データの元データを取得
            ds = Me.DacAccess(ds, "SelectOutkaEdiData")

            If dtJissekiData.Rows.Count <> 1 Then
                '該当データなしの場合はエラー
                MyBase.SetMessage("E011")
                Return ds
            End If

            'ハネウェルＥＤＩ送信(遅延理由)データの登録
            ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Next

        Return ds

    End Function

#End Region

    'ADD S 2020/02/27 010901
#Region "荷主振り分け処理"

    ''' <summary>
    ''' 荷主自動振り分け
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCustAuto(ByVal ds As DataSet) As DataSet

        Dim drIn As DataRow = ds.Tables(LMI960BLC.TABLE_NM_IN).Rows(0)
        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow
        Dim max As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).NewRow
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        Dim dtJissekiData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_JISSEKI_DATA)

        Dim skipCnt As Integer = 0

        For i As Integer = 0 To max

            drSakuseiTarget = dtSakuseiTarget.Rows(i)

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO") = i

            ds = Me.DacAccess(ds, "SelectStpCityForSpecifyCust")

            Dim dtStop As DataTable = ds.Tables("LMI960STP_FOR_SPECIFY_CUST")

            If dtStop.Rows.Count = 0 Then
                '排他エラー
                MyBase.SetMessage("E011")
                Return ds

            ElseIf dtStop.Rows.Count > 1 Then
                'データ不備
                MyBase.SetMessageStore("00",
                                       "E01X",
                                       {"", "STOP_TYPE='P'のデータが複数存在します。"},
                                       drSakuseiTarget.Item("ROW_NO").ToString,
                                       "Load Number",
                                       drSakuseiTarget.Item("SHIPMENT_ID").ToString)
                Continue For

            End If

            Dim drStop As DataRow = dtStop.Rows(0)
            Dim dtCust As DataTable = ds.Tables("LMI960M_CUST")
            Dim drCust As DataRow

            If drIn.Item("NRS_BR_CD").ToString = NrsBrCd.Chiba Then
                '千葉BCの場合
                Select Case drStop("ZIP_CODE").ToString.Trim
                    Case "1234567", "4980068"  'Yatomi
                        dtCust.Clear()
                        drCust = dtCust.NewRow
                        drCust("CUST_CD_L") = "20630"
                        drCust("CUST_CD_M") = "00"
                        dtCust.Rows.Add(drCust)

                    'Case "2260006", "2310811", "2358501"  'Yokohama
                    '    dtCust.Clear()
                    '    drCust = dtCust.NewRow
                    '    drCust("CUST_CD_L") = "50630"
                    '    drCust("CUST_CD_M") = "00"
                    '    dtCust.Rows.Add(drCust)

                    Case "2900045", "2900067", "2930011", "2990108"  'Ichihara
                        ds = Me.DacAccess(ds, "SelectCustCdBySkuNumber")
                        If ds.Tables("LMI960M_CUST").Rows.Count > 1 Then
                            '一意に絞れない場合
                            Dim str As String = String.Empty
                            For Each row As DataRow In dtCust.Rows
                                If String.IsNullOrEmpty(str) Then
                                    str += String.Concat(" AND ((CUST_CD_L = '", row("CUST_CD_L").ToString, "' AND CUST_CD_M = '", row("CUST_CD_M").ToString, "')")
                                Else
                                    str += String.Concat(" OR (CUST_CD_L = '", row("CUST_CD_L").ToString, "' AND CUST_CD_M = '", row("CUST_CD_M").ToString, "')")
                                End If
                            Next
                            str += ")"

                            drSakuseiTarget.Item("NARROW_DOWN_LIST") = str

                            skipCnt += 1
                            Continue For

                        ElseIf ds.Tables("LMI960M_CUST").Rows.Count = 0 Then
                            '該当データなしの場合
                            drSakuseiTarget.Item("NARROW_DOWN_LIST") = " AND ((CUST_CD_L = '00630' AND CUST_CD_M = '00') OR (CUST_CD_L = '00632' AND CUST_CD_M = '00') OR (CUST_CD_L = '70630' AND CUST_CD_M = '00'))"

                            skipCnt += 1
                            Continue For
                        End If

                    Case Else
                        '自動振り分け対象外
                        drSakuseiTarget.Item("NARROW_DOWN_LIST") = "対象外"
                        skipCnt += 1
                        Continue For

                End Select
            End If

            If drIn.Item("NRS_BR_CD").ToString = NrsBrCd.Forwarding Then
                'フォワーディング事業部の場合
                Select Case drStop("ZIP_CODE").ToString.Trim
                    Case "2260006", "2310811", "2358501"  'Yokohama
                        dtCust.Clear()
                        drCust = dtCust.NewRow
                        drCust("CUST_CD_L") = "50630"
                        drCust("CUST_CD_M") = "00"
                        dtCust.Rows.Add(drCust)

                    Case Else
                        '自動振り分け対象外
                        skipCnt += 1
                        Continue For

                End Select
            End If

            'ハネウェルＥＤＩ受信データ(Header)荷主コード更新
            ds = Me.DacAccess(ds, "UpdateOutkaEdiHedCust")

            If MyBase.GetResultCount <> 1 Then
                '排他エラー
                MyBase.SetMessage("E011")
                Return ds
            End If

        Next

        '荷主を特定できなかった件数
        Dim dtProcResult As DataTable = ds.Tables("LMI960PROC_RESULT")
        dtProcResult.Clear()
        Dim drProcResult As DataRow = dtProcResult.NewRow
        drProcResult("SKIP") = skipCnt.ToString
        dtProcResult.Rows.Add(drProcResult)

        Return ds

    End Function

    ''' <summary>
    ''' 荷主手動振り分け
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCustManual(ByVal ds As DataSet) As DataSet

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).NewRow
        drProcCtrlData("ROW_NO") = 0
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        'LMZ260OUTからLMI960M_CUSTに転記
        Dim drLmz260Out As DataRow = ds.Tables("LMZ260OUT").Rows(0)

        Dim dtCust As DataTable = ds.Tables("LMI960M_CUST")
        Dim drCust As DataRow = dtCust.NewRow
        drCust("CUST_CD_L") = drLmz260Out("CUST_CD_L").ToString
        drCust("CUST_CD_M") = drLmz260Out("CUST_CD_M").ToString
        dtCust.Rows.Add(drCust)

        'ハネウェルＥＤＩ受信データ(Header)荷主コード更新
        ds = Me.DacAccess(ds, "UpdateOutkaEdiHedCust")

        If MyBase.GetResultCount <> 1 Then
            '排他エラー
            MyBase.SetMessage("E011")
            Return ds
        End If

        Return ds

    End Function

#End Region
    'ADD E 2020/02/27 010901

    'ADD S 2020/02/07 010901
#Region "出荷登録処理"

    ''' <summary>
    ''' 出荷登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShukkaTouroku(ByVal ds As DataSet) As DataSet

        Dim num As New NumberMasterUtility

        Dim nrsBrCd As String = ds.Tables("LMI960IN").Rows(0)("NRS_BR_CD").ToString

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow

        '処理制御データテーブルから現在処理行を取得
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = dtProcCtrlData.Rows(0)
        Dim i As Integer = CInt(dtProcCtrlData.Rows(0).Item("ROW_NO"))

        Dim dtWarnShori As DataTable = ds.Tables(TABLE_NM_WARNING_SHORI)

        Dim dtOutkaL As DataTable = ds.Tables("LMI960IN_OUTKA_L")
        Dim dtOutkaM As DataTable = ds.Tables("LMI960IN_OUTKA_M")

        '出荷登録用データテーブルをクリア
        dtOutkaL.Clear()
        dtOutkaM.Clear()

        drSakuseiTarget = dtSakuseiTarget.Rows(i)

        '出荷登録の元情報（Header、ShipmentDetails、Stops）取得
        ds = Me.DacAccess(ds, "SelectHedSpmStpForInsInOutka")
        Dim dtSrc1 As DataTable = ds.Tables("LMI960HED_SPM_STP_FOR_INS_INOUTKA")

        If dtSrc1.Rows.Count = 0 Then
            '排他エラー
            MyBase.SetMessage("E011")
            Return ds

        ElseIf dtSrc1.Rows.Count > 1 Then
            'データ不備
            MyBase.SetMessageStore("00",
                                   "E01X",
                                   {"", "STOP_TYPE='P'または'D'のデータが複数存在します。"},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            Return ds

        End If

        Dim drSrc1 As DataRow = dtSrc1.Rows(0)

        If Not String.IsNullOrWhiteSpace(drSrc1.Item("TRACTOR_NUMBER").ToString) AndAlso
           Left(drSrc1.Item("TRACTOR_NUMBER").ToString, 1) <> "O" Then
            '入荷/運送登録後、受注ステータス戻し処理（受注OK⇒未処理）して、出荷登録しようとした場合
            MyBase.SetMessageStore("00",
                                   "E428",
                                   {"すでに入荷または輸送データとして受注送信済みの", "、出荷登録", ""},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            Return ds
        End If

        '出荷登録用荷主情報取得
        ds = Me.DacAccess(ds, "SelectMCustForInsInOutka")
        Dim drCust As DataRow = ds.Tables("LMI960M_CUST").Rows(0)

        '出荷管理番号L
        Dim outkaNoL As String = String.Empty
        If drSrc1("INOUT_KB").ToString = LMI960DAC.InOutKb.Outka Then
            outkaNoL = drSrc1("OUTKA_CTL_NO").ToString
        End If
        If String.IsNullOrWhiteSpace(outkaNoL) Then
            '出荷管理番号Lを採番
            outkaNoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, nrsBrCd)
        End If

        '出荷管理番号Mをクリア
        Dim outkaNoM As Integer = 0
        '出荷データＬ用出荷梱包個数のクリア
        Dim sumOutkaPkgNb As Decimal = 0

        '出荷データＬの値を設定
        Dim drOutkaL As DataRow = dtOutkaL.NewRow
        drOutkaL("NRS_BR_CD") = nrsBrCd
        drOutkaL("OUTKA_NO_L") = outkaNoL
        drOutkaL("WH_CD") = drCust("DEFAULT_SOKO_CD").ToString
        drOutkaL("OUTKA_PLAN_DATE") = drSrc1("OUTKA_PLAN_DATE").ToString
        drOutkaL("OUTKO_DATE") = drSrc1("OUTKA_PLAN_DATE").ToString
        drOutkaL("ARR_PLAN_DATE") = drSrc1("ARR_PLAN_DATE").ToString
        drOutkaL("CUST_CD_L") = drSrc1("CUST_CD_L").ToString
        drOutkaL("CUST_CD_M") = drSrc1("CUST_CD_M").ToString
        drOutkaL("WH_TAB_YN") = drCust("WH_TAB_YN").ToString
        If WH_TAB_YN.YES.Equals(drCust("WH_TAB_YN").ToString) Then
            drOutkaL("WH_TAB_STATUS") = WH_TAB_STATUS.NOT_INSTRUCTED
        Else
            drOutkaL("WH_TAB_STATUS") = WH_TAB_STATUS.UNNECESSARY
        End If

        Dim con As String = drSrc1("CON").ToString
        If Not String.IsNullOrEmpty(con) AndAlso con.Length >= 10 Then
            drOutkaL("CUST_ORD_NO") = con.Substring(3, 7)
        End If

        'ワーニング有無
        Dim hasWarning As Boolean = False

        '出荷登録用届先情報取得
        ds = Me.DacAccess(ds, "SelectMDestForInsInOutka")
        If ds.Tables("LMI960M_DEST").Rows.Count > 0 Then
            Dim drDest As DataRow = ds.Tables("LMI960M_DEST").Rows(0)

            drOutkaL("DEST_CD") = drDest("DEST_CD").ToString
            drOutkaL("DEST_AD_3") = drDest("AD_3").ToString
            drOutkaL("DEST_TEL") = drDest("TEL").ToString
            drOutkaL("SP_NHS_KB") = drDest("SP_NHS_KB").ToString
            drOutkaL("COA_YN") = drDest("COA_YN").ToString
        Else
            '届先マスタにない

            Dim allowBlank As Boolean = False

            If dtWarnShori.Rows.Count > 0 Then
                'ワーニング画面で処理を選択した場合

                '当該商品の処理結果を検索
                Dim filter As String = "EDI_WARNING_ID = '" & WARNING_ID.M_DEST_NOT_FOUND & "'" &
                                       " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                                       " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'"

                Dim drsWarnShori As DataRow() = dtWarnShori.Select(filter)

                If drsWarnShori.Length > 0 Then
                    'ワーニング画面で空白を許可
                    allowBlank = True
                End If
            End If

            If Not allowBlank Then
                'ワーニングを設定
                ds = Me.SetComWarningM("W297",
                                       {"届先マスタ", "届先コードが'" & drSrc1("LOCATION_ID").ToString & "'の届先", "届先を空にして出荷登録", "", ""},
                                       WARNING_ID.M_DEST_NOT_FOUND,
                                       ds,
                                       "Location ID",
                                       drSrc1("LOCATION_ID").ToString,
                                       "")
                drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                hasWarning = True
            End If
        End If

        '出荷登録用売上先(届先)情報取得
        If String.IsNullOrEmpty(drSrc1("BUYID").ToString) Then
            drOutkaL("SHIP_CD_L") = String.Empty

        Else
            ds.Tables("LMI960M_DEST").Rows.Clear()
            ds = Me.DacAccess(ds, "SelectMDestShipForInsOutka")
            If ds.Tables("LMI960M_DEST").Rows.Count > 0 Then
                Dim drDest As DataRow = ds.Tables("LMI960M_DEST").Rows(0)

                drOutkaL("SHIP_CD_L") = drDest("DEST_CD").ToString
            Else
                '届先マスタにない

                Dim allowBlank As Boolean = False

                If dtWarnShori.Rows.Count > 0 Then
                    'ワーニング画面で処理を選択した場合

                    '当該商品の処理結果を検索
                    Dim filter As String = "EDI_WARNING_ID = '" & WARNING_ID.M_DEST_NOT_FOUND_BUYID & "'" &
                                       " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                                       " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'"

                    Dim drsWarnShori As DataRow() = dtWarnShori.Select(filter)

                    If drsWarnShori.Length > 0 Then
                        'ワーニング画面で空白を許可
                        allowBlank = True
                    End If
                End If

                If Not allowBlank Then
                    'ワーニングを設定
                    ds = Me.SetComWarningM("W308",
                                       {"売上先コード'" & drSrc1("BUYID").ToString & "'", "届先", "届先マスタ", "売上先を空にして出荷登録", "", ""},
                                       WARNING_ID.M_DEST_NOT_FOUND_BUYID,
                                       ds,
                                       "Buy ID",
                                       drSrc1("BUYID").ToString,
                                       "")
                    drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                    hasWarning = True
                End If
            End If
        End If

        '出荷登録の元情報（Commodity）取得
        ds = Me.DacAccess(ds, "SelectCmdForInsInOutka")
        Dim dtSrc2 As DataTable = ds.Tables("LMI960CMD_FOR_INS_INOUTKA")
        Dim maxCmd As Integer = dtSrc2.Rows.Count - 1

        'Commodity(商品)ごとにループ
        For j As Integer = 0 To maxCmd

            Dim drSrc2 As DataRow = dtSrc2.Rows(j)

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO_CMD") = j

            If dtWarnShori.Rows.Count > 0 Then
                'ワーニング画面で処理を選択した場合

                '当該商品の処理結果を検索【商品マスタ未登録】
                Dim filter As String = "EDI_WARNING_ID = '" & WARNING_ID.GOODS_NOT_FOUND & "'" &
                                       " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                                       " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'" &
                                       " AND CMD_GYO = '" & drSrc2("CMD_GYO").ToString & "'"

                Dim drsWarnShori As DataRow() = dtWarnShori.Select(filter)

                If drsWarnShori.Length > 0 Then
                    'ワーニング画面で「この商品を除いて出荷登録」を選択
                    Continue For
                End If

                '当該商品の処理結果を検索【商品を一意に特定できない】
                filter = "EDI_WARNING_ID = '" & WARNING_ID.GOODS_NOT_IDENTIFIED & "'" &
                         " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                         " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'" &
                         " AND CMD_GYO = '" & drSrc2("CMD_GYO").ToString & "'"

                drsWarnShori = dtWarnShori.Select(filter)

                If drsWarnShori.Length > 0 Then
                    '当該商品の処理結果より商品KEYを取得
                    drSrc2("GOODS_CD_NRS") = drsWarnShori(0).Item("MST_VALUE").ToString
                End If

            End If

            '出荷登録用商品情報取得
            ds = Me.DacAccess(ds, "SelectMGoodsForInsInOutka")
            Dim dtGoods As DataTable = ds.Tables("LMI960M_GOODS")
            If dtGoods.Rows.Count = 0 Then
                '該当商品がない
                ''MyBase.SetMessageStore("00",
                ''                       "E769",
                ''                       {"荷主カテゴリ2が'" & drSrc2("SKU_NUMBER").ToString & "'の商品"},
                ''                       drSakuseiTarget.Item("ROW_NO").ToString,
                ''                       "Load Number",
                ''                       drSakuseiTarget.Item("SHIPMENT_ID").ToString)
                ds = Me.SetComWarningM("W297",
                                       {"荷主CD：" & drSrc1("CUST_CD_L").ToString, "荷主カテゴリ2が'" & drSrc2("SKU_NUMBER").ToString & "'の商品", "この商品を除いて出荷登録", "", ""},
                                       WARNING_ID.GOODS_NOT_FOUND,
                                       ds,
                                       "SKU Number",
                                       drSrc2("SKU_NUMBER").ToString,
                                       "")
                drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                hasWarning = True
            ElseIf dtGoods.Rows.Count > 1 Then
                '商品を一意に特定できない
                ds = Me.SetComWarningM("W296",
                                       {"荷主カテゴリ2（SKU Number）", "", "", "", ""},
                                       WARNING_ID.GOODS_NOT_IDENTIFIED,
                                       ds,
                                       "SKU Number",
                                       drSrc2("SKU_NUMBER").ToString,
                                       "")
                drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                hasWarning = True
            End If

            If hasWarning Then
                'ワーニングがある場合、商品のチェック処理のみ継続
                Continue For
            End If

            Dim drGoods As DataRow = ds.Tables("LMI960M_GOODS").Rows(0)

            '出荷管理番号Mを採番
            outkaNoM += 1

            Dim numberPices As Decimal = CDec(drSrc2("NUMBER_PIECES").ToString)
            Dim pkgNb As Decimal = CDec(drGoods("PKG_NB").ToString)
            Dim stdIrimeNb As Decimal = CDec(drGoods("STD_IRIME_NB").ToString)
            Dim outkaPkgNb As Decimal = Math.Truncate(numberPices / pkgNb)
            Dim outkaHasu As Decimal = numberPices Mod pkgNb

            '出荷データＭの値を設定
            Dim drOutkaM As DataRow = dtOutkaM.NewRow
            drOutkaM("NRS_BR_CD") = nrsBrCd
            drOutkaM("OUTKA_NO_L") = outkaNoL
            drOutkaM("OUTKA_NO_M") = outkaNoM.ToString("000")
            drOutkaM("COA_YN") = drGoods("COA_YN").ToString
            drOutkaM("GOODS_CD_NRS") = drGoods("GOODS_CD_NRS").ToString
            drOutkaM("ALCTD_KB") = drGoods("ALCTD_KB").ToString
            drOutkaM("OUTKA_PKG_NB") = outkaPkgNb.ToString
            drOutkaM("OUTKA_HASU") = outkaHasu.ToString
            drOutkaM("OUTKA_QT") = (outkaHasu * stdIrimeNb).ToString
            drOutkaM("OUTKA_TTL_NB") = numberPices.ToString
            drOutkaM("OUTKA_TTL_QT") = (numberPices * stdIrimeNb).ToString
            drOutkaM("BACKLOG_NB") = drOutkaM("OUTKA_TTL_NB")
            drOutkaM("BACKLOG_QT") = drOutkaM("OUTKA_TTL_QT")
            drOutkaM("UNSO_ONDO_KB") = drGoods("UNSO_ONDO_KB").ToString
            drOutkaM("IRIME") = drGoods("STD_IRIME_NB").ToString
            drOutkaM("IRIME_UT") = drGoods("STD_IRIME_UT").ToString
            drOutkaM("OUTKA_M_PKG_NB") = drOutkaM("OUTKA_PKG_NB")
            drOutkaM("REMARK") = drGoods("OUTKA_ATT").ToString
            drOutkaM("SIZE_KB") = drGoods("SIZE_KB").ToString
            dtOutkaM.Rows.Add(drOutkaM)

            '出荷データＬ用出荷梱包個数の加算
            sumOutkaPkgNb += outkaPkgNb + If(outkaHasu > 0, 1, 0)

        Next

        If hasWarning Then
            'ワーニングがある場合
            Return ds
        End If


        If outkaNoM > 0 Then
            '出荷データＭの登録データが存在する場合

            '出荷データＬの値を設定
            drOutkaL("OUTKA_PKG_NB") = sumOutkaPkgNb
            dtOutkaL.Rows.Add(drOutkaL)

            'ハネウェルＥＤＩ受信データ(Header)更新
            ds = Me.DacAccess(ds, "UpdateOutkaEdiHedForInsInOutka")

            '排他チェック
            If MyBase.GetResultCount <> 1 Then
                '排他エラー
                MyBase.SetMessage("E011")
                Return ds
            End If

            '出荷データＬ登録
            ds = Me.DacAccess(ds, "InsertCOutkaL")
            '出荷データＭ登録
            ds = Me.DacAccess(ds, "InsertCOutkaM")

        Else
            '出荷データＭなしのため登録不可
            MyBase.SetMessageStore("00",
                                   "E428",
                                   {"商品が1件もない", "、出荷登録", ""},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="msgArray">メッセージパラメータ</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <param name="fieldName">項目名</param>
    ''' <param name="ediValue">項目値</param>
    ''' <param name="mstValue">マスタ値</param>
    ''' <param name="addtionalFieldValue">追加項目値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetComWarningM(ByVal msgId As String _
                                 , ByVal msgArray() As String _
                                 , ByVal warningId As String _
                                 , ByVal ds As DataSet _
                                 , ByVal fieldName As String _
                                 , ByVal ediValue As String _
                                 , ByVal mstValue As String _
                                 , Optional ByVal addtionalFieldValue As String = "") As DataSet

        '処理制御データテーブルから現在処理行を取得
        Dim i As Integer = CInt(ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim drSakuseiTarget As DataRow = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET).Rows(i)
        Dim drSrc1 As DataRow = ds.Tables("LMI960HED_SPM_STP_FOR_INS_INOUTKA").Rows(0)
        Dim drCust As DataRow = ds.Tables("LMI960M_CUST").Rows(0)
        Dim drWarnDtl As DataRow = ds.Tables(TABLE_NM_WARNING_DTL).NewRow()

        drWarnDtl("CUST_CD_L") = drSrc1("CUST_CD_L").ToString
        drWarnDtl("CUST_CD_M") = drSrc1("CUST_CD_M").ToString
        drWarnDtl("WH_CD") = drCust("DEFAULT_SOKO_CD").ToString
        drWarnDtl("SHIPMENT_ID") = drSakuseiTarget("SHIPMENT_ID")
        drWarnDtl("CRT_DATE") = drSakuseiTarget("HED_CRT_DATE")
        drWarnDtl("FILE_NAME") = drSakuseiTarget("HED_FILE_NAME")
        If warningId.Substring(WARNING_ID_FMT.UNIT_OF_WARNING.START_IDX, WARNING_ID_FMT.UNIT_OF_WARNING.LEN) = WARNING_ID_FMT.UNIT_OF_WARNING.M Then
            If ds.Tables("LMI960CMD_FOR_INS_INOUTKA").Rows.Count > 0 Then
                Dim j As Integer = CInt(ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO_CMD"))
                Dim drSrc2 As DataRow = ds.Tables("LMI960CMD_FOR_INS_INOUTKA").Rows(j)
                drWarnDtl("CMD_GYO") = drSrc2("CMD_GYO")
            End If
        End If
        drWarnDtl("EDI_CTL_NO_L") = String.Empty
        drWarnDtl("EDI_CTL_NO_M") = String.Empty
        drWarnDtl("CUST_ORD_NO") = String.Empty
        drWarnDtl("CUST_ORD_NO_DTL") = String.Empty
        drWarnDtl("INOUTKA_NO") = String.Empty
        drWarnDtl("INOUTKA_NO_CHU_MAX") = String.Empty
        drWarnDtl("MESSAGE_ID") = msgId
        drWarnDtl("PARA1") = msgArray(0)
        drWarnDtl("PARA2") = msgArray(1)
        drWarnDtl("PARA3") = msgArray(2)
        drWarnDtl("PARA4") = msgArray(3)
        drWarnDtl("PARA5") = msgArray(4)
        Dim mstFlg As String = warningId.Substring(WARNING_ID_FMT.MST_FLG.START_IDX, WARNING_ID_FMT.MST_FLG.LEN)
        If mstFlg = WARNING_ID_FMT.MST_FLG.M_GOODS Then
            drWarnDtl("GOODS_NM") = String.Empty
        Else
            drWarnDtl("GOODS_NM") = String.Empty
        End If
        drWarnDtl("FIELD_NM") = fieldName
        drWarnDtl("FIELD_VALUE") = ediValue
        drWarnDtl("ADDITIONAL_FIELD_VALUE_1") = addtionalFieldValue
        drWarnDtl("MST_VALUE") = mstValue
        drWarnDtl("EDI_WARNING_ID") = warningId

        ds.Tables("WARNING_DTL").Rows.Add(drWarnDtl)

        Return ds

    End Function

#End Region

#Region "入荷登録処理"

    ''' <summary>
    ''' 入荷登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NyukaTouroku(ByVal ds As DataSet) As DataSet

        Dim num As New NumberMasterUtility

        Dim nrsBrCd As String = ds.Tables("LMI960IN").Rows(0)("NRS_BR_CD").ToString

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow

        '処理制御データテーブルから現在処理行を取得
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = dtProcCtrlData.Rows(0)
        Dim i As Integer = CInt(dtProcCtrlData.Rows(0).Item("ROW_NO"))

        Dim dtWarnShori As DataTable = ds.Tables(TABLE_NM_WARNING_SHORI)

        Dim dtInkaL As DataTable = ds.Tables("LMI960IN_INKA_L")
        Dim dtUnsoL As DataTable = ds.Tables("LMI960IN_UNSO_L")

        '入荷登録用データテーブルをクリア
        dtInkaL.Clear()
        dtUnsoL.Clear()

        drSakuseiTarget = dtSakuseiTarget.Rows(i)

        '入荷登録の元情報（Header、ShipmentDetails、Stops）取得
        ds = Me.DacAccess(ds, "SelectHedSpmStpForInsInOutka")
        Dim dtSrc1 As DataTable = ds.Tables("LMI960HED_SPM_STP_FOR_INS_INOUTKA")

        If dtSrc1.Rows.Count = 0 Then
            '排他エラー
            MyBase.SetMessage("E011")
            Return ds

        ElseIf dtSrc1.Rows.Count > 1 Then
            'データ不備
            MyBase.SetMessageStore("00",
                                   "E01X",
                                   {"", "STOP_TYPE='P'または'D'のデータが複数存在します。"},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            Return ds

        End If

        Dim drSrc1 As DataRow = dtSrc1.Rows(0)

        If Not String.IsNullOrWhiteSpace(drSrc1.Item("TRACTOR_NUMBER").ToString) AndAlso
           Left(drSrc1.Item("TRACTOR_NUMBER").ToString, 1) <> "I" Then
            '出荷/運送登録後、受注ステータス戻し処理（受注OK⇒未処理）して、入荷登録しようとした場合
            MyBase.SetMessageStore("00",
                                   "E428",
                                   {"すでに出荷または輸送データとして受注送信済みの", "、入荷登録", ""},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            Return ds
        End If

        '入荷登録用荷主情報取得
        ds = Me.DacAccess(ds, "SelectMCustForInsInOutka")
        Dim drCust As DataRow = ds.Tables("LMI960M_CUST").Rows(0)

        '入荷管理番号L
        Dim inkaNoL As String = String.Empty
        If drSrc1("INOUT_KB").ToString = LMI960DAC.InOutKb.Inka Then
            inkaNoL = drSrc1("OUTKA_CTL_NO").ToString
        End If
        If String.IsNullOrWhiteSpace(inkaNoL) Then
            '入荷管理番号Lを採番
            inkaNoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, nrsBrCd)
        End If

        '入荷データＬの値を設定
        Dim drInkaL As DataRow = dtInkaL.NewRow
        drInkaL("NRS_BR_CD") = nrsBrCd
        drInkaL("INKA_NO_L") = inkaNoL
        drInkaL("INKA_TP") = "10"
        drInkaL("INKA_KB") = "10"
        drInkaL("INKA_STATE_KB") = "10"
        drInkaL("INKA_DATE") = drSrc1("INKA_INKA_DATE").ToString
        drInkaL("WH_CD") = drCust("DEFAULT_SOKO_CD").ToString
        drInkaL("CUST_CD_L") = drSrc1("CUST_CD_L").ToString
        drInkaL("CUST_CD_M") = drSrc1("CUST_CD_M").ToString
        drInkaL("INKA_PLAN_QT") = "0"
        drInkaL("INKA_TTL_NB") = "0"
        drInkaL("TOUKI_HOKAN_YN") = "01"
        drInkaL("HOKAN_YN") = "01"
        drInkaL("HOKAN_FREE_KIKAN") = drCust("HOKAN_FREE_KIKAN").ToString
        drInkaL("NIYAKU_YN") = "01"
        drInkaL("TAX_KB") = drCust("TAX_KB").ToString
        drInkaL("UNCHIN_TP") = drCust("UNSO_TEHAI_KB").ToString
        drInkaL("UNCHIN_KB") = drCust("TARIFF_BUNRUI_KB").ToString
        drInkaL("WH_TAB_STATUS") = "00"
        drInkaL("WH_TAB_YN") = drCust("WH_TAB_YN").ToString
        drInkaL("WH_TAB_IMP_YN") = "00"
        drInkaL("WH_TAB_NO_SIJI_FLG") = "00"

        Dim con As String = drSrc1("CON").ToString
        If Not String.IsNullOrEmpty(con) AndAlso con.Length >= 10 Then
            drInkaL("OUTKA_FROM_ORD_NO_L") = con.Substring(0, 10)
        End If

        dtInkaL.Rows.Add(drInkaL)

        'ワーニング有無
        Dim hasWarning As Boolean = False

        '入荷登録用届先情報取得
        ds = Me.DacAccess(ds, "SelectMDestForInsInOutka")
        If ds.Tables("LMI960M_DEST").Rows.Count > 0 Then
            Dim drDest As DataRow = ds.Tables("LMI960M_DEST").Rows(0)

            '運送Ｌの値を設定
            Dim drUnsoL As DataRow = dtUnsoL.NewRow
            drUnsoL("NRS_BR_CD") = drInkaL("NRS_BR_CD")
            drUnsoL("UNSO_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
            drUnsoL("YUSO_BR_CD") = drInkaL("NRS_BR_CD")
            drUnsoL("INOUTKA_NO_L") = drInkaL("INKA_NO_L")
            drUnsoL("BIN_KB") = "01"
            drUnsoL("JIYU_KB") = "02"
            drUnsoL("OUTKA_PLAN_DATE") = drInkaL("INKA_DATE")
            drUnsoL("ARR_PLAN_DATE") = drInkaL("INKA_DATE")
            drUnsoL("CUST_CD_L") = drInkaL("CUST_CD_L")
            drUnsoL("CUST_CD_M") = drInkaL("CUST_CD_M")
            drUnsoL("CUST_REF_NO") = drInkaL("OUTKA_FROM_ORD_NO_L")
            drUnsoL("ORIG_CD") = drDest("DEST_CD").ToString
            drUnsoL("DEST_CD") = drCust("SOKO_DEST_CD").ToString
            drUnsoL("UNSO_PKG_NB") = "0"
            drUnsoL("UNSO_WT") = "0"
            drUnsoL("PC_KB") = "01"
            drUnsoL("TARIFF_BUNRUI_KB") = drInkaL("UNCHIN_KB")
            drUnsoL("MOTO_DATA_KB") = "10"
            drUnsoL("TAX_KB") = drInkaL("TAX_KB")
            drUnsoL("UNSO_TEHAI_KB") = drInkaL("UNCHIN_TP")
            drUnsoL("TYUKEI_HAISO_FLG") = "00"
            dtUnsoL.Rows.Add(drUnsoL)

        Else
            '届先マスタにない

            Dim allowBlank As Boolean = False

            If dtWarnShori.Rows.Count > 0 Then
                'ワーニング画面で処理を選択した場合

                '当該商品の処理結果を検索
                Dim filter As String = "EDI_WARNING_ID = '" & WARNING_ID.M_DEST_NOT_FOUND & "'" &
                                       " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                                       " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'"

                Dim drsWarnShori As DataRow() = dtWarnShori.Select(filter)

                If drsWarnShori.Length > 0 Then
                    'ワーニング画面で空白を許可
                    allowBlank = True
                End If
            End If

            If Not allowBlank Then
                'ワーニングを設定
                ds = Me.SetComWarningM("W297",
                                       {"届先マスタ", "届先コードが'" & drSrc1("INKA_ORIG_CD").ToString & "'の届先", "出荷元を空にして入荷登録", "", ""},
                                       WARNING_ID.M_DEST_NOT_FOUND,
                                       ds,
                                       "Location ID",
                                       drSrc1("INKA_ORIG_CD").ToString,
                                       "")
                drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                hasWarning = True
            End If
        End If

        If hasWarning Then
            'ワーニングがある場合
            Return ds
        End If

        '入荷登録の元情報（Commodity）取得
        ds = Me.DacAccess(ds, "SelectCmdForInsInOutka")
        Dim dtSrc2 As DataTable = ds.Tables("LMI960CMD_FOR_INS_INOUTKA")
        Dim maxCmd As Integer = dtSrc2.Rows.Count - 1

        'Commodity(商品)ごとにループ
        For j As Integer = 0 To maxCmd

            Dim drSrc2 As DataRow = dtSrc2.Rows(j)

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO_CMD") = j

            '入荷登録用商品情報取得
            ds = Me.DacAccess(ds, "SelectMGoodsForInsInOutka")
            Dim dtGoods As DataTable = ds.Tables("LMI960M_GOODS")
            If dtGoods.Rows.Count = 0 Then
                '該当商品がない
                MyBase.SetMessageStore("02",
                                       "E769",
                                       {"荷主カテゴリ2が'" & drSrc2("SKU_NUMBER").ToString & "'の商品"},
                                       drSakuseiTarget.Item("ROW_NO").ToString,
                                       "Load Number",
                                       drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            ElseIf dtGoods.Rows.Count > 1 Then
                '商品を一意に特定できない
                MyBase.SetMessageStore("02",
                                       "E975",
                                       {"荷主カテゴリ2が'" & drSrc2("SKU_NUMBER").ToString & "'の商品"},
                                       drSakuseiTarget.Item("ROW_NO").ToString,
                                       "Load Number",
                                       drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            End If

        Next


        'ハネウェルＥＤＩ受信データ(Header)更新
        ds = Me.DacAccess(ds, "UpdateOutkaEdiHedForInsInOutka")

        '排他チェック
        If MyBase.GetResultCount <> 1 Then
            '排他エラー
            MyBase.SetMessage("E011")
            Return ds
        End If

        '入荷データＬ登録
        ds = Me.DacAccess(ds, "InsertBInkaL")
        '運送Ｌ登録
        ds = Me.DacAccess(ds, "InsertFUnsoL")

        Return ds

    End Function

#End Region '入荷登録処理

#Region "運送登録処理"

    ''' <summary>
    ''' 運送登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnsoTouroku(ByVal ds As DataSet) As DataSet

        Dim num As New NumberMasterUtility

        Dim nrsBrCd As String = ds.Tables("LMI960IN").Rows(0)("NRS_BR_CD").ToString

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow

        '処理制御データテーブルから現在処理行を取得
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = dtProcCtrlData.Rows(0)
        Dim i As Integer = CInt(dtProcCtrlData.Rows(0).Item("ROW_NO"))

        Dim dtWarnShori As DataTable = ds.Tables(TABLE_NM_WARNING_SHORI)

        Dim dtUnsoL As DataTable = ds.Tables("F_UNSO_L")
        Dim dtUnsoM As DataTable = ds.Tables("F_UNSO_M")

        '運送登録用データテーブルをクリア
        dtUnsoL.Clear()
        dtUnsoM.Clear()

        drSakuseiTarget = dtSakuseiTarget.Rows(i)

        '運送登録の元情報（Header、ShipmentDetails、Stops）取得
        ds = Me.DacAccess(ds, "SelectHedSpmStpForInsInOutka")
        Dim dtSrc1 As DataTable = ds.Tables("LMI960HED_SPM_STP_FOR_INS_INOUTKA")

        If dtSrc1.Rows.Count = 0 Then
            '排他エラー
            MyBase.SetMessage("E011")
            Return ds

        ElseIf dtSrc1.Rows.Count > 1 Then
            'データ不備
            MyBase.SetMessageStore("00",
                                   "E01X",
                                   {"", "STOP_TYPE='P'または'D'のデータが複数存在します。"},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            Return ds

        End If

        Dim drSrc1 As DataRow = dtSrc1.Rows(0)

        If Not String.IsNullOrWhiteSpace(drSrc1.Item("TRACTOR_NUMBER").ToString) AndAlso
           Left(drSrc1.Item("TRACTOR_NUMBER").ToString, 1) <> "U" Then
            '出荷/入荷登録後、受注ステータス戻し処理（受注OK⇒未処理）して、運送登録しようとした場合
            MyBase.SetMessageStore("00",
                                   "E428",
                                   {"すでに入荷または出荷データとして受注送信済みの", "、輸送登録", ""},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            Return ds
        End If

        '運送登録用荷主情報取得
        ds = Me.DacAccess(ds, "SelectMCustForInsInOutka")
        Dim drCust As DataRow = ds.Tables("LMI960M_CUST").Rows(0)

        '運送番号L
        Dim unsoNoL As String = String.Empty
        If drSrc1("INOUT_KB").ToString = LMI960DAC.InOutKb.Unso Then
            unsoNoL = drSrc1("OUTKA_CTL_NO").ToString
        End If
        If String.IsNullOrWhiteSpace(unsoNoL) Then
            '運送番号Lを採番
            unsoNoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
        End If

        '運送番号Mをクリア
        Dim unsoNoM As Integer = 0

        'ワーニング有無
        Dim hasWarning As Boolean = False

        '運送データＬの初期値を取得（常に最低1レコード）
        ds = Me.DacAccess(ds, "SelectUnsoLSource")

        '最初のレコード以外は不要なので削除
        For idx As Integer = dtUnsoL.Rows.Count - 1 To 1 Step -1
            dtUnsoL.Rows.RemoveAt(idx)
        Next



        '運送データＬの値を設定(1)
        Dim drUnsoL As DataRow = dtUnsoL.Rows(0)
        drUnsoL("UNSO_NO_L") = unsoNoL
        drUnsoL("OUTKA_PLAN_DATE") = drSrc1("OUTKA_PLAN_DATE").ToString
        drUnsoL("ARR_PLAN_DATE") = drSrc1("ARR_PLAN_DATE").ToString

        'DAC呼び出しに際しLMI960PROC_CTRL.INOUT_KBを一時的に書き換えるため、復元用にバックアップ
        Dim procCtrlInoutKbBackup As String = drProcCtrlData("INOUT_KB").ToString

        '運送登録用届先情報取得【発地】
        drProcCtrlData("INOUT_KB") = LMI960DAC.InOutKb.Inka
        ds = Me.DacAccess(ds, "SelectMDestForInsInOutka")
        If ds.Tables("LMI960M_DEST").Rows.Count > 0 Then
            Dim drDest As DataRow = ds.Tables("LMI960M_DEST").Rows(0)

            drUnsoL("ORIG_CD") = drDest("DEST_CD").ToString
        Else
            '届先マスタにない

            Dim allowBlank As Boolean = False

            If dtWarnShori.Rows.Count > 0 Then
                'ワーニング画面で処理を選択した場合

                '当該商品の処理結果を検索
                Dim filter As String = "EDI_WARNING_ID = '" & WARNING_ID.M_DEST_NOT_FOUND_UNSO_ORIG & "'" &
                                       " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                                       " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'"

                Dim drsWarnShori As DataRow() = dtWarnShori.Select(filter)

                If drsWarnShori.Length > 0 Then
                    'ワーニング画面で空白を許可
                    allowBlank = True
                End If
            End If

            If Not allowBlank Then
                'ワーニングを設定
                ds = Me.SetComWarningM("W297",
                                       {"届先マスタ", "届先コードが'" & drSrc1("INKA_ORIG_CD").ToString & "'の届先", "積込先を空にして輸送登録", "", ""},
                                       WARNING_ID.M_DEST_NOT_FOUND_UNSO_ORIG,
                                       ds,
                                       "Location ID",
                                       drSrc1("INKA_ORIG_CD").ToString,
                                       "")
                drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                hasWarning = True
            End If
        End If

        '運送登録用届先情報取得【届先】
        drProcCtrlData("INOUT_KB") = LMI960DAC.InOutKb.Outka
        ds = Me.DacAccess(ds, "SelectMDestForInsInOutka")
        If ds.Tables("LMI960M_DEST").Rows.Count > 0 Then
            Dim drDest As DataRow = ds.Tables("LMI960M_DEST").Rows(0)

            drUnsoL("DEST_CD") = drDest("DEST_CD").ToString
            drUnsoL("AD_3") = drDest("AD_3").ToString
        Else
            '届先マスタにない

            Dim allowBlank As Boolean = False

            If dtWarnShori.Rows.Count > 0 Then
                'ワーニング画面で処理を選択した場合

                '当該商品の処理結果を検索
                Dim filter As String = "EDI_WARNING_ID = '" & WARNING_ID.M_DEST_NOT_FOUND_UNSO_DEST & "'" &
                                       " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                                       " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'"

                Dim drsWarnShori As DataRow() = dtWarnShori.Select(filter)

                If drsWarnShori.Length > 0 Then
                    'ワーニング画面で空白を許可
                    allowBlank = True
                End If
            End If

            If Not allowBlank Then
                'ワーニングを設定
                ds = Me.SetComWarningM("W297",
                                       {"届先マスタ", "届先コードが'" & drSrc1("LOCATION_ID").ToString & "'の届先", "荷降先を空にして輸送登録", "", ""},
                                       WARNING_ID.M_DEST_NOT_FOUND_UNSO_DEST,
                                       ds,
                                       "Location ID",
                                       drSrc1("LOCATION_ID").ToString,
                                       "")
                drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                hasWarning = True
            End If
        End If

        'DAC呼び出しが終了したのでLMI960PROC_CTRL.INOUT_KBをバックアップから復元
        drProcCtrlData("INOUT_KB") = procCtrlInoutKbBackup



        '運送登録の元情報（Commodity）取得
        ds = Me.DacAccess(ds, "SelectCmdForInsInOutka")
        Dim dtSrc2 As DataTable = ds.Tables("LMI960CMD_FOR_INS_INOUTKA")
        Dim maxCmd As Integer = dtSrc2.Rows.Count - 1

        'Commodity(商品)ごとにループ
        For j As Integer = 0 To maxCmd

            Dim drSrc2 As DataRow = dtSrc2.Rows(j)

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO_CMD") = j

            If dtWarnShori.Rows.Count > 0 Then
                'ワーニング画面で処理を選択した場合

                '当該商品の処理結果を検索【商品マスタ未登録】
                Dim filter As String = "EDI_WARNING_ID = '" & WARNING_ID.GOODS_NOT_FOUND & "'" &
                                       " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                                       " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'" &
                                       " AND CMD_GYO = '" & drSrc2("CMD_GYO").ToString & "'"

                Dim drsWarnShori As DataRow() = dtWarnShori.Select(filter)

                If drsWarnShori.Length > 0 Then
                    'ワーニング画面で「この商品を除いて出荷登録」を選択
                    Continue For
                End If

                '当該商品の処理結果を検索【商品を一意に特定できない】
                filter = "EDI_WARNING_ID = '" & WARNING_ID.GOODS_NOT_IDENTIFIED & "'" &
                         " AND CRT_DATE = '" & drSakuseiTarget("HED_CRT_DATE").ToString & "'" &
                         " AND FILE_NAME = '" & drSakuseiTarget("HED_FILE_NAME").ToString & "'" &
                         " AND CMD_GYO = '" & drSrc2("CMD_GYO").ToString & "'"

                drsWarnShori = dtWarnShori.Select(filter)

                If drsWarnShori.Length > 0 Then
                    '当該商品の処理結果より商品KEYを取得
                    drSrc2("GOODS_CD_NRS") = drsWarnShori(0).Item("MST_VALUE").ToString
                End If

            End If

            '運送登録用商品情報取得
            ds = Me.DacAccess(ds, "SelectMGoodsForInsInOutka")
            Dim dtGoods As DataTable = ds.Tables("LMI960M_GOODS")
            If dtGoods.Rows.Count = 0 Then
                '該当商品がない
                ds = Me.SetComWarningM("W297",
                                       {"荷主CD：" & drSrc1("CUST_CD_L").ToString, "荷主カテゴリ2が'" & drSrc2("SKU_NUMBER").ToString & "'の商品", "この商品を除いて輸送登録", "", ""},
                                       WARNING_ID.GOODS_NOT_FOUND,
                                       ds,
                                       "SKU Number",
                                       drSrc2("SKU_NUMBER").ToString,
                                       "")
                drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                hasWarning = True
            ElseIf dtGoods.Rows.Count > 1 Then
                '商品を一意に特定できない
                ds = Me.SetComWarningM("W296",
                                       {"荷主カテゴリ2（SKU Number）", "", "", "", ""},
                                       WARNING_ID.GOODS_NOT_IDENTIFIED,
                                       ds,
                                       "SKU Number",
                                       drSrc2("SKU_NUMBER").ToString,
                                       "")
                drProcCtrlData("WARNING_FLG") = LMConst.FLG.ON
                hasWarning = True
            End If

            If hasWarning Then
                'ワーニングがある場合、商品のチェック処理のみ継続
                Continue For
            End If

            Dim drGoods As DataRow = ds.Tables("LMI960M_GOODS").Rows(0)

            Dim numberPices As Decimal = CDec(drSrc2("NUMBER_PIECES").ToString)
            Dim pkgNb As Decimal = CDec(drGoods("PKG_NB").ToString)
            Dim stdIrimeNb As Decimal = CDec(drGoods("STD_IRIME_NB").ToString)
            Dim hasu As Decimal = 0

            '運送数量
            Dim unsoTtlQt As Decimal = numberPices * pkgNb * stdIrimeNb + hasu * stdIrimeNb

            If unsoTtlQt < 0 OrElse unsoTtlQt > 999999999.999 Then
                '運送数量が有効範囲外のため、登録不可⇒当該商品はスキップ
                MyBase.SetMessageStore("00",
                                       "E428",
                                       {"運送数量が0～999,999,999.999でない",
                                        "、一部の商品は輸送登録",
                                        $"SKU Number={drSrc2("SKU_NUMBER").ToString}　Number pieces={numberPices.ToString("#,#.#")}　運送数量={unsoTtlQt.ToString("#,#.#")}"
                                       },
                                       drSakuseiTarget.Item("ROW_NO").ToString,
                                       "Load Number",
                                       drSakuseiTarget.Item("SHIPMENT_ID").ToString)
                Continue For
            End If

            '運送管理番号Mを採番
            unsoNoM += 1

            '出荷データＭの値を設定
            Dim drUnsoM As DataRow = dtUnsoM.NewRow
            drUnsoM("NRS_BR_CD") = drUnsoL("NRS_BR_CD")
            drUnsoM("UNSO_NO_L") = unsoNoL
            drUnsoM("UNSO_NO_M") = unsoNoM.ToString("000")
            drUnsoM("GOODS_CD_NRS") = drGoods("GOODS_CD_NRS")
            drUnsoM("GOODS_NM") = drGoods("GOODS_NM_1")
            drUnsoM("UNSO_TTL_NB") = drSrc2("NUMBER_PIECES")
            drUnsoM("NB_UT") = drGoods("NB_UT")
            drUnsoM("UNSO_TTL_QT") = unsoTtlQt
            drUnsoM("QT_UT") = drGoods("PKG_UT")
            drUnsoM("HASU") = hasu
            drUnsoM("ZAI_REC_NO") = ""
            drUnsoM("UNSO_ONDO_KB") = ""
            drUnsoM("IRIME") = drGoods("STD_IRIME_NB")
            drUnsoM("IRIME_UT") = drGoods("STD_IRIME_UT")
            drUnsoM("BETU_WT") = drGoods("STD_WT_KGS")
            drUnsoM("SIZE_KB") = ""
            drUnsoM("ZBUKA_CD") = ""
            drUnsoM("ABUKA_CD") = ""
            drUnsoM("PKG_NB") = drGoods("PKG_NB")
            drUnsoM("LOT_NO") = ""
            drUnsoM("REMARK") = ""
            drUnsoM("PRINT_SORT") = "99"
            drUnsoM("TARE_YN") = drGoods("TARE_YN")
            dtUnsoM.Rows.Add(drUnsoM)

        Next

        If hasWarning Then
            'ワーニングがある場合
            Return ds
        End If



        '運送データＬの値を設定(2)

        '運賃タリフセットマスタを取得
        Dim dtTariff As DataTable = ds.Tables("LMI960INOUT_M_UNCHIN_TARIFF_SET")
        dtTariff.Clear()
        Dim drTariff As DataRow = dtTariff.NewRow
        drTariff("NRS_BR_CD") = drUnsoL("NRS_BR_CD")
        drTariff("CUST_CD_L") = drUnsoL("CUST_CD_L")
        drTariff("CUST_CD_M") = drUnsoL("CUST_CD_M")
        drTariff("SET_KB") = "01" '01:届先
        drTariff("DEST_CD") = drUnsoL("DEST_CD")
        dtTariff.Rows.Add(drTariff)
        ds = Me.DacAccess(ds, "SelectUnchinTariffSet")

        If dtTariff.Rows.Count > 0 Then
            'マスタから取得した値を設定
            Dim dr As DataRow = dtTariff.Rows(0)
            Dim tariffKbn As String = dr.Item("TARIFF_BUNRUI_KB").ToString()

            drUnsoL("TARIFF_BUNRUI_KB") = tariffKbn
            drUnsoL("SEIQ_TARIFF_CD") = Me.GetTariffSetCd(dr, tariffKbn)
            drUnsoL("SEIQ_ETARIFF_CD") = dr.Item("EXTC_TARIFF_CD").ToString()
        End If

        Dim tareYnUnso As String = drUnsoL("TARE_YN").ToString  '風袋加算フラグ
        If "01".Equals(tareYnUnso) Then
            '運送重量の計算（GetJuryoCalcData）で使用する区分マスタを取得
            Dim dtZKbn As DataTable = ds.Tables("LMI960INOUT_Z_KBN")
            dtZKbn.Clear()
            Dim drZKbn As DataRow = dtZKbn.NewRow
            drZKbn("KBN_GROUP_CD") = LMKbnConst.KBN_N001
            dtZKbn.Rows.Add(drZKbn)
            ds = Me.DacAccess(ds, "SelectZKbnHanyo")
        End If

        Dim dtGoodsDtl As DataTable = ds.Tables("LMI960INOUT_M_GOODS_DETAILS")
        Dim konpoSu As Decimal = 0  '運送梱包数
        Dim juryo As Decimal = 0    '運送重量

        For Each drUnsoM As DataRow In dtUnsoM.Rows
            Dim futaiJuryo As String = String.Empty
            If "01".Equals(tareYnUnso) Then
                '風袋重量の取得
                dtGoodsDtl.Clear()
                Dim drGoodsDtl As DataRow = dtGoodsDtl.NewRow
                drGoodsDtl("NRS_BR_CD") = drUnsoM("NRS_BR_CD")
                drGoodsDtl("GOODS_CD_NRS") = drUnsoM("GOODS_CD_NRS")
                drGoodsDtl("SUB_KB") = "16"  '16:容器重量
                dtGoodsDtl.Rows.Add(drGoodsDtl)

                ds = Me.DacAccess(ds, "SelectMGoodsDetails")
                If dtGoodsDtl.Rows.Count > 0 Then
                    futaiJuryo = dtGoodsDtl.Rows(0).Item("SET_NAIYO").ToString  '風袋重量
                End If
            End If

            '運送梱包数
            konpoSu += Me.GetUnsoKonpoData(CDec(drUnsoM("UNSO_TTL_NB")), CDec(drUnsoM("HASU")), CDec(drUnsoM("PKG_NB")))

            '運送重量
            juryo += Me.GetJuryoCalcData(drUnsoM("TARE_YN").ToString,
                                         tareYnUnso,
                                         drUnsoM("QT_UT").ToString,
                                         CDec(drUnsoM("IRIME")),
                                         CDec(drUnsoM("PKG_NB")) * CDec(drUnsoM("UNSO_TTL_NB")) + CDec(drUnsoM("HASU")),
                                         CDec(drUnsoM("IRIME")),
                                         CDec(drUnsoM("BETU_WT")),
                                         CDec(drUnsoM("UNSO_TTL_QT")),
                                         futaiJuryo,
                                         ds
                                        )
        Next

        '運送重量の切り上げ処理
        juryo = Math.Ceiling(juryo)

        If konpoSu < 0 OrElse konpoSu > 9999999999 Then
            '運送梱包数が有効範囲外のため、登録不可
            MyBase.SetMessageStore("00",
                                   "E428",
                                   {"運送梱包数が0～9,999,999,999でない", "、輸送登録", $"運送梱包数={konpoSu.ToString("#,#")}"},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            Return ds
        End If

        If juryo < 0 OrElse juryo > 999999999 Then
            '運送重量が有効範囲外のため、登録不可
            MyBase.SetMessageStore("00",
                                   "E428",
                                   {"運送重量が0～999,999,999でない", "、輸送登録", $"運送重量={juryo.ToString("#,#")}"},
                                   drSakuseiTarget.Item("ROW_NO").ToString,
                                   "Load Number",
                                   drSakuseiTarget.Item("SHIPMENT_ID").ToString)
            Return ds
        End If

        drUnsoL("UNSO_PKG_NB") = konpoSu
        drUnsoL("UNSO_WT") = juryo



        'ハネウェルＥＤＩ受信データ(Header)更新
        ds = Me.DacAccess(ds, "UpdateOutkaEdiHedForInsInOutka")

        '排他チェック
        If MyBase.GetResultCount <> 1 Then
            '排他エラー
            MyBase.SetMessage("E011")
            Return ds
        End If

        '運送データＬ登録
        ds = Me.DacAccess(ds, "InsertFUnsoL")
        '運送データＭ登録
        ds = Me.DacAccess(ds, "InsertFUnsoM")

        Return ds

    End Function

    ''' <summary>
    ''' タリフコードを取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="tariffKbn">タリフ分類区分</param>
    ''' <returns>タリフコード</returns>
    ''' <remarks>LMF020Gより移植</remarks>
    Private Function GetTariffSetCd(ByVal dr As DataRow, ByVal tariffKbn As String) As String

        GetTariffSetCd = String.Empty

        Select Case tariffKbn

            Case "20" '20:車扱い

                GetTariffSetCd = dr.Item("UNCHIN_TARIFF_CD2").ToString()

            Case "40" '40:横持ち

                GetTariffSetCd = dr.Item("YOKO_TARIFF_CD").ToString()

            Case Else

                GetTariffSetCd = dr.Item("UNCHIN_TARIFF_CD1").ToString()

        End Select

        Return GetTariffSetCd

    End Function

    ''' <summary>
    ''' 運送梱包数の計算
    ''' </summary>
    ''' <param name="kosu">運送個数</param>
    ''' <param name="hasu">端数</param>
    ''' <param name="konp">梱包個数</param>
    ''' <returns>運送梱包数</returns>
    ''' <remarks>LMFControlHより移植</remarks>
    Friend Function GetUnsoKonpoData(ByVal kosu As Decimal _
                                      , ByVal hasu As Decimal _
                                      , ByVal konp As Decimal
                                      ) As Decimal

        GetUnsoKonpoData = 0

        If kosu = 0 Then

            '切り上げした値を設定
            If konp <> 0 Then
                GetUnsoKonpoData = System.Math.Ceiling(hasu / konp)
            End If

        Else

            '端数が0より大きい場合、+1
            If 0 < hasu Then
                kosu = kosu + 1
            End If
            GetUnsoKonpoData = kosu

        End If

        Return GetUnsoKonpoData

    End Function

    ''' <summary>
    ''' 運送重量を取得(実計算)
    ''' </summary>
    ''' <param name="calcFlg">風袋加算フラグ(商品)</param>
    ''' <param name="unsoCalcFlg">風袋加算フラグ(運送会社)</param>
    ''' <param name="suryoTani">数量単位</param>
    ''' <param name="irime">入目</param>
    ''' <param name="kosu">個数</param>
    ''' <param name="stdIrime">標準入目</param>
    ''' <param name="stdWt">標準重量</param>
    ''' <param name="suryo">数量</param>
    ''' <returns>運送重量(行単位)</returns>
    ''' <remarks>LMFControlHより移植(一部改変)</remarks>
    Friend Function GetJuryoCalcData(ByVal calcFlg As String _
                                         , ByVal unsoCalcFlg As String _
                                         , ByVal suryoTani As String _
                                         , ByVal irime As Decimal _
                                         , ByVal kosu As Decimal _
                                         , ByVal stdIrime As Decimal _
                                         , ByVal stdWt As Decimal _
                                         , ByVal suryo As Decimal _
                                         , ByVal hutaiJyuryo As String _
                                         , ByVal ds As DataSet
                                         ) As Decimal

        Dim futai As Decimal = 0

        '計算フラグ = 01の場合、風袋重量の取得
        If "01".Equals(calcFlg) = True AndAlso "01".Equals(unsoCalcFlg) = True Then

            If hutaiJyuryo.ToString.Trim.Equals("") = True Then
                '区分マスタから値取得
                Dim sql As String = String.Concat(" KBN_CD = '", suryoTani, "' AND KBN_GROUP_CD = '", LMKbnConst.KBN_N001, "' ")
                Dim drs As DataRow() = ds.Tables("LMI960INOUT_Z_KBN").Select(sql)
                If 0 < drs.Length Then
                    futai = Convert.ToDecimal(drs(0).Item("VALUE1").ToString())
                End If
            Else
                '荷主明細マスタから値取得
                futai = Convert.ToDecimal(hutaiJyuryo.ToString())
            End If

        End If

        Dim juryo As Decimal = 0

        '出荷数量＜標準入目ならば、小分けとみなす
        If suryo < stdIrime Then
            '[小分け出荷] 商品１つあたりの重量 = （商品）標準重量 * 数量 / （商品）標準入目
            juryo = stdWt * suryo / stdIrime
            Return juryo

        Else
            '[通常出荷用] 商品１つあたりの重量 = （商品）標準重量 * 数量 / （商品）標準入目 + 風袋重量
            If stdIrime <> 0 Then
                juryo = stdWt * Convert.ToDecimal(irime) / stdIrime + futai
            End If
            Return juryo * kosu

        End If

    End Function

#End Region '運送登録

#Region "GLIS受注登録・削除処理"

    ''' <summary>
    ''' GLIS受注更新処理の入力データ作成処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MakeInputDataForUpdBooking(ByVal ds As DataSet) As DataSet

        '受注参照キーデータ取得
        ds = Me.DacAccess(ds, "SelectGLZ9300InBookUpdKey")

        If MyBase.GetResultCount() <> 1 Then
            '該当データなしの場合はエラー
            MyBase.SetMessage("E011")
            Return ds
        End If

        '受注更新用HWLデータ取得
        ds = Me.DacAccess(ds, "SelectGLZ9300InBookingData")

        If MyBase.GetResultCount() <> 1 Then
            '該当データが1件でない場合はエラー
            MyBase.SetMessage("E334", {"Load Number=" & ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET).Rows(0).Item("SHIPMENT_ID").ToString})
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' GLIS受注削除処理の入力データ作成処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MakeInputDataForDelBooking(ByVal ds As DataSet) As DataSet

        '受注削除キーデータ取得
        ds = Me.DacAccess(ds, "SelectGLZ9300InBookDelKey")

        If MyBase.GetResultCount() <> 1 Then
            '該当データなしの場合はエラー
            MyBase.SetMessage("E011")
            Return ds
        End If

        Return ds

    End Function

#End Region
    'ADD E 2020/02/07 010901

#Region "シリンダー取込処理"

    ''' <summary>
    ''' シリンダー取込処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ImportCylinder(ByVal ds As DataSet) As DataSet

        'シリンダーシリアルNo更新
        ds = Me.DacAccess(ds, "UpdateOutkaEdiHedCylinderSerial")

        If MyBase.GetResultCount() <> 1 Then
            '1件でない場合はエラー
            MyBase.SetMessage("E011")
            Return ds
        End If

        Return ds

    End Function

#End Region 'シリンダー取込処理

    'ADD START 2019/03/27
#Region "一括変更処理"

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IkkatsuChange(ByVal ds As DataSet) As DataSet

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow
        Dim max As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).NewRow
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        For i As Integer = 0 To max

            drSakuseiTarget = dtSakuseiTarget.Rows(i)

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO") = i

            'DEL S 2020/02/07 010901
            ''ShipmentIDの重複チェック
            'ds = Me.DacAccess(ds, "SelectCntShipmentID")

            'If MyBase.GetResultCount() <> 1 Then
            '    '1件でない場合はエラー
            '    MyBase.SetMessage("E494", New String() {String.Concat("Load Number=", drSakuseiTarget.Item("SHIPMENT_ID")), "ハネウェルEDI受信データ(ShipmentDetails)", ""})
            '    Return ds
            'End If
            'DEL E 2020/02/07 010901

            'ハネウェルＥＤＩ受信データ(Stops)更新
            ds = Me.DacAccess(ds, "UpdateOutkaEdiDtlStpReqStartDateTime")

            If MyBase.GetResultCount() <> 1 Then
                '1件でない場合はエラー
                MyBase.SetMessage("E011")
                Return ds
            End If

            'RequestedStartDateTime更新時のHeader更新
            ds = Me.DacAccess(ds, "UpdateOutkaEdiHedWhenUpdStpReqStartDateTime")

        Next

        Return ds

    End Function

#End Region
    'ADD END   2019/03/27

#Region "受注ステータス戻し処理"

    ''' <summary>
    ''' 受注ステータス戻し処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RollbackJuchuStatus(ByVal ds As DataSet) As DataSet

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim max As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).NewRow
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        For i As Integer = 0 To max

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO") = i

            '入出荷区分
            Dim inoutKb As String = dtSakuseiTarget.Rows(i).Item("INOUT_KB").ToString

            'ハネウェルＥＤＩ受信データ(Header)更新(受注ステータス戻し処理)
            ds = Me.DacAccess(ds, "UpdateOutkaEdiHedForRollback")

            If MyBase.GetResultCount() <> 1 Then
                '1件でない場合はエラー
                MyBase.SetMessage("E011")
                Return ds
            End If

            Select Case inoutKb
                Case LMI960DAC.InOutKb.Outka
                    '出荷データLを物理削除
                    ds = Me.DacAccess(ds, "DeleteOutkaL")

                    If MyBase.GetResultCount() <> 1 Then
                        '1件でない場合はエラー
                        MyBase.SetMessage("E011")
                        Return ds
                    End If

                    '出荷データMを物理削除
                    ds = Me.DacAccess(ds, "DeleteOutkaM")

                    '出荷データSを物理削除
                    ds = Me.DacAccess(ds, "DeleteOutkaS")

                    '輸出データLを物理削除
                    ds = Me.DacAccess(ds, "DeleteCExportL")

                    'シッピングマークHEDを物理削除
                    ds = Me.DacAccess(ds, "DeleteCMarkHed")

                    'シッピングマークDTLを物理削除
                    ds = Me.DacAccess(ds, "DeleteCMarkDtl")

                Case LMI960DAC.InOutKb.Inka
                    '入荷データLを物理削除
                    ds = Me.DacAccess(ds, "DeleteInkaL")

                    If MyBase.GetResultCount() <> 1 Then
                        '1件でない場合はエラー
                        MyBase.SetMessage("E011")
                        Return ds
                    End If

                    '入荷データMを物理削除
                    ds = Me.DacAccess(ds, "DeleteInkaM")

                    '入荷データSを物理削除
                    ds = Me.DacAccess(ds, "DeleteInkaS")

                Case LMI960DAC.InOutKb.Unso
                    '運送データLを物理削除
                    ds = Me.DacAccess(ds, "DeleteUnsoL")

                    If MyBase.GetResultCount() <> 1 Then
                        '1件でない場合はエラー
                        MyBase.SetMessage("E011")
                        Return ds
                    End If

            End Select

        Next

        Return ds

    End Function

#End Region '受注ステータス戻し処理

#Region "JOB NO変更処理"

    ''' <summary>
    ''' JOB NO変更処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModJobNo(ByVal ds As DataSet) As DataSet

        'OUTKA_CTL_NO(JOB NO)更新
        ds = Me.DacAccess(ds, "UpdateOutkaEdiHedOutkaCtlNo")

        If MyBase.GetResultCount() < 1 Then
            '1件以上でない場合はエラー
            MyBase.SetMessage("E011")
            Return ds
        End If

        Return ds

    End Function

#End Region 'JOB NO変更処理

#Region "EDI取消処理"

    ''' <summary>
    ''' 未処理⇔EDI取消処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiTorikeshi(ByVal ds As DataSet) As DataSet

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLC.TABLE_NM_SAKUSEI_TARGET)
        Dim max As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960BLC.TABLE_NM_PROC_CTRL).NewRow
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        For i As Integer = 0 To max

            '処理制御データテーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_NO") = i

            'ハネウェルＥＤＩ受信データ(Header)進捗区分(受注ステータス)更新
            ds = Me.DacAccess(ds, "UpdateOutkaEdiHedShinchokuJuchu")

            If MyBase.GetResultCount() <> 1 Then
                '1件でない場合はエラー
                MyBase.SetMessage("E011")
                Return ds
            End If

        Next

        Return ds

    End Function

#End Region 'EDI取消処理

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

    ''' <summary>
    ''' 検索処理のエラーメッセージを設定
    ''' </summary>
    ''' <param name="count">件数</param>
    ''' <remarks></remarks>
    Private Sub SetSelectErrMes(ByVal count As Integer)

        'メッセージコードの設定
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

    End Sub



    Private Function FormatDateString(ByVal dateString As String) As String

        If String.IsNullOrEmpty(dateString) OrElse dateString.Length < 12 Then
            Return dateString
        End If

        'yyyyMMddHHmmss→yyyy/MM/dd HH:mm
        Return dateString.Substring(0, 12).Insert(10, ":").Insert(8, " ").Insert(6, "/").Insert(4, "/")

    End Function

#End Region

End Class
