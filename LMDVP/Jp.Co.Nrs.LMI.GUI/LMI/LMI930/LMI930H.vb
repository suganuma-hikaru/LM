' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI930  : 住化カラー実績報告データ作成
'  作  成  者       :  [umano]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
'Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.Text
Imports System.IO

''' <summary>
''' LMI930ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI930H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMI930F = New LMI930F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'TXT出力データ検索/DBフラグ更新(EDI出荷・受信TBL)処理
        Dim rtnDs As DataSet = Me.selectTxtFlgUpd(frm, prmDs)

        'TXT出力データ作成処理
        Dim rtnFlg As Boolean = Me.MakeTXT(frm, rtnDs, prmDs)

        prm.ReturnFlg = rtnFlg

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' TXT出力データ検索/DBフラグ更新(EDI出荷・受信TBL)処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function selectTxtFlgUpd(ByVal frm As LMI930F, ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectTXT")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI930BLF", _
                                                         "selectTxtFlgUpd", _
                                                         ds, _
                                                         -1, _
                                                         -1)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectTXT")

        Return rtnDs

    End Function

    ''' <summary>
    ''' TXT作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeTXTメソッド呼出</remarks>
    Private Function MakeTXT(ByVal frm As LMI930F, ByVal ds As DataSet, ByVal inDs As DataSet) As Boolean

        If ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows.Count = 0 Then
            MyBase.SetMessage("E320", New String() {"実績送信対象データが存在しない", "ファイル作成"})
            Return False
        End If

        Dim strData As String = String.Empty
        Dim max As Integer = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows.Count - 1

        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'TXT出力処理
        Dim setData As StringBuilder = New StringBuilder()

        For i As Integer = 0 To max

            '会社コード(2byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("COMPANY_CD").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("COMPANY_CD").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.COMPANY_CD Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.COMPANY_CD, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.COMPANY_CD - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '赤黒区分(1byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("RB_KB").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("RB_KB").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.RB_KB Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.RB_KB, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.RB_KB - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If


            '伝票番号(7byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DENP_NO").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DENP_NO").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DENP_NO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DENP_NO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DENP_NO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '伝票番号枝番(1byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DENP_NO_EDA").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DENP_NO_EDA").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DENP_NO_EDA Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DENP_NO_EDA, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DENP_NO_EDA - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '伝票番号連番(2byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DENP_NO_REN").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DENP_NO_REN").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DENP_NO_REN Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DENP_NO_REN, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DENP_NO_REN - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '伝票番号行(1byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DENP_NO_GYO").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DENP_NO_GYO").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DENP_NO_GYO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DENP_NO_GYO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DENP_NO_GYO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '処理日(8byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SHORI_DATE").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SHORI_DATE").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.SHORI_DATE Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.SHORI_DATE, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.SHORI_DATE - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '出荷日(8byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("OUTKA_PLAN_DATE").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("OUTKA_PLAN_DATE").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.OUTKA_PLAN_DATE Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.OUTKA_PLAN_DATE, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.OUTKA_PLAN_DATE - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '受払場所（中継地）(3byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("UKE_CHUKEI").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("UKE_CHUKEI").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.UKE_CHUKEI Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.UKE_CHUKEI, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.UKE_CHUKEI - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '受払場所（相手）(3byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("UKE_AITE").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("UKE_AITE").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.UKE_AITE Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.UKE_AITE, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.UKE_AITE - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '受払種別(3byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("UKE_SHUBETSU").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("UKE_SHUBETSU").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.UKE_SHUBETSU Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.UKE_SHUBETSU, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.UKE_SHUBETSU - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '品種(4byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("GOODS_CLASS").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("GOODS_CLASS").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.GOODS_CLASS Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.GOODS_CLASS, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.GOODS_CLASS - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '商品コード(6byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("GOODS_CD").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("GOODS_CD").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.GOODS_CD Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.GOODS_CD, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.GOODS_CD - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '商品名(26byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("GOODS_NM").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("GOODS_NM").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.GOODS_NM Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.GOODS_NM, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.GOODS_NM - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷姿(2byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("NISUGATA").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("NISUGATA").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.NISUGATA Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.NISUGATA, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.NISUGATA - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '容量(数値：8byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("IRIME").ToString) = False Then
                strData = Replace(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("IRIME").ToString, ".", String.Empty)
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.IRIME Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadLeft(LMI930C.IRIME, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadLeft(LMI930C.IRIME - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '棚卸区分(2byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("TANA_KB").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("TANA_KB").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.TANA_KB Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.TANA_KB, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.TANA_KB - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '在庫区分(1byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("ZAIKO_KB").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("ZAIKO_KB").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.ZAIKO_KB Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.ZAIKO_KB, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.ZAIKO_KB - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            'ロット番号(15byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("LOT_NO").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("LOT_NO").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.LOT_NO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.LOT_NO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.LOT_NO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '生産工場(3byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SEISAN_KOJO").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SEISAN_KOJO").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.SEISAN_KOJO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.SEISAN_KOJO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.SEISAN_KOJO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '個数(数値：5byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("KOSU").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("KOSU").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.KOSU Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadLeft(LMI930C.KOSU, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadLeft(LMI930C.KOSU - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '数量(数値：13byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SURYO").ToString) = False Then
                strData = Replace(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SURYO").ToString, ".", String.Empty)
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.SURYO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadLeft(LMI930C.SURYO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadLeft(LMI930C.SURYO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷着日(8byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("ARR_PLAN_DATE").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("ARR_PLAN_DATE").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.ARR_PLAN_DATE Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.ARR_PLAN_DATE, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.ARR_PLAN_DATE - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '容器条件(2byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("YOUKI_JOKEN").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("YOUKI_JOKEN").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.YOUKI_JOKEN Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.YOUKI_JOKEN, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.YOUKI_JOKEN - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷渡条件(2byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("NIWATASHI_JOKEN").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("NIWATASHI_JOKEN").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.NIWATASHI_JOKEN Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.NIWATASHI_JOKEN, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.NIWATASHI_JOKEN - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '試験表有無(1byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIKEN_HYO").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SHIKEN_HYO").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.SHIKEN_HYO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.SHIKEN_HYO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.SHIKEN_HYO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '指定帳票(1byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SHITEI_DENPYO").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SHITEI_DENPYO").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.SHITEI_DENPYO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.SHITEI_DENPYO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.SHITEI_DENPYO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷受主コード(6byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_CD").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_CD").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DEST_CD Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_CD, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DEST_CD - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷受主郵便番号(8byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_ZIP").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_ZIP").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DEST_ZIP Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_ZIP, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DEST_ZIP - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷受主電話番号(18byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_TEL").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_TEL").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DEST_TEL Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_TEL, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DEST_TEL - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷受主宛メッセージ(全角:40byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_MSG").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_MSG").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If strData.Length = LMI930C.DEST_MSG Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_MSG, Convert.ToChar("　")))
                'setData.Append(strData.PadRight(LMI930C.DEST_MSG - strData.Length, Convert.ToChar("　")))

            End If

            '相手先オーダー番号(20byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("BUYER_ORD_NO_DTL").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("BUYER_ORD_NO_DTL").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.BUYER_ORD_NO_DTL Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.BUYER_ORD_NO_DTL, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.BUYER_ORD_NO_DTL - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '注文主コード(6byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("BUYER_CD").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("BUYER_CD").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.BUYER_CD Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.BUYER_CD, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.BUYER_CD - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '注文主電話番号(18byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("BUYER_TEL").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("BUYER_TEL").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.BUYER_TEL Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.BUYER_TEL, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.BUYER_TEL - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '工場宛てメッセージ(全角：40byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("KOJO_MSG").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("KOJO_MSG").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If strData.Length = LMI930C.KOJO_MSG Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.KOJO_MSG, Convert.ToChar("　")))
                'setData.Append(strData.PadRight(LMI930C.KOJO_MSG - strData.Length, Convert.ToChar("　")))

            End If

            '販売担当課(5byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("HANBAI_KA").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("HANBAI_KA").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.HANBAI_KA Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.HANBAI_KA, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.HANBAI_KA - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '販売担当者(4byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("HANBAI_TANTO").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("HANBAI_TANTO").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.HANBAI_TANTO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.HANBAI_TANTO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.HANBAI_TANTO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            'オーダー区分(1byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("ORDER_KB").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("ORDER_KB").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.ORDER_KB Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.ORDER_KB, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.ORDER_KB - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷受主名(全角：80byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_NM").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_NM").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If strData.Length = LMI930C.DEST_NM Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_NM, Convert.ToChar("　")))
                'setData.Append(strData.PadRight(LMI930C.DEST_NM - strData.Length, Convert.ToChar("　")))

            End If

            '荷受主住所１(全角：30byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_AD_1").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_AD_1").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If strData.Length = LMI930C.DEST_AD_1 Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_AD_1, Convert.ToChar("　")))
                'setData.Append(strData.PadRight(LMI930C.DEST_AD_1 - strData.Length, Convert.ToChar("　")))

            End If

            '荷受主住所２(全角：30byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_AD_2").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_AD_2").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If strData.Length = LMI930C.DEST_AD_2 Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_AD_2, Convert.ToChar("　")))
                'setData.Append(strData.PadRight(LMI930C.DEST_AD_2 - strData.Length, Convert.ToChar("　")))

            End If

            '注文主取引先名(全角：80byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("BUYER_NM").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("BUYER_NM").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If strData.Length = LMI930C.BUYER_NM Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.BUYER_NM, Convert.ToChar("　")))
                'setData.Append(strData.PadRight(LMI930C.BUYER_NM - strData.Length, Convert.ToChar("　")))

            End If

            '注文主取引先名カナ(25byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_NM_KANA").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_NM_KANA").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DEST_NM_KANA Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_NM_KANA, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DEST_NM_KANA - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '注文主住所１カナ(20byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_AD_KANA1").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_AD_KANA1").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DEST_AD_KANA1 Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_AD_KANA1, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DEST_AD_KANA1 - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '注文主住所２カナ(20byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_AD_KANA2").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DEST_AD_KANA2").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DEST_AD_KANA2 Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DEST_AD_KANA2, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DEST_AD_KANA2 - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '作成日(8byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SAKUSEI_DATE").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SAKUSEI_DATE").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.SAKUSEI_DATE Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.SAKUSEI_DATE, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.SAKUSEI_DATE - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '作成時間(8byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SAKUSEI_TIME").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("SAKUSEI_TIME").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.SAKUSEI_TIME Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.SAKUSEI_TIME, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.SAKUSEI_TIME - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            'ラック番号(3byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("RACK_NO").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("RACK_NO").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.RACK_NO Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.RACK_NO, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.RACK_NO - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '荷主注文番号(8byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("CUST_ORD_NO_DTL").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("CUST_ORD_NO_DTL").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.CUST_ORD_NO_DTL Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.CUST_ORD_NO_DTL, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.CUST_ORD_NO_DTL - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '理由(2byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("RIYUU").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("RIYUU").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.RIYUU Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.RIYUU, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.RIYUU - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            'データ区分(5byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DATA_KB").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("DATA_KB").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.DATA_KB Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.DATA_KB, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.DATA_KB - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '輸送手段(10byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("YUSO_SHUDAN").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("YUSO_SHUDAN").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.YUSO_SHUDAN Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.YUSO_SHUDAN, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.YUSO_SHUDAN - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '輸送業者(10byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("YUSO_GYOSHA").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("YUSO_GYOSHA").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.YUSO_GYOSHA Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.YUSO_GYOSHA, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.YUSO_GYOSHA - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '市区村コード(5byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("YUSO_JIS_CD").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("YUSO_JIS_CD").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.YUSO_JIS_CD Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.YUSO_JIS_CD, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.YUSO_JIS_CD - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            '実績区分(1byte)
            If String.IsNullOrEmpty(ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("JISSEKI_KBN").ToString) = False Then
                strData = ds.Tables(LMI930C.TABLE_NM_OUT_TXT).Rows(i).Item("JISSEKI_KBN").ToString
            Else
                strData = String.Empty
            End If
            'setData.Append(String.Concat("", strData, "", vbTab))
            If enc.GetByteCount(strData) = LMI930C.JISSEKI_KBN Then
                setData.Append(strData)
            Else
                setData.Append(strData.PadRight(LMI930C.JISSEKI_KBN, Convert.ToChar(Space(1))))
                'setData.Append(strData.PadRight(LMI930C.JISSEKI_KBN - (enc.GetByteCount(strData) - strData.Length), Convert.ToChar(Space(1))))

            End If

            setData.Append(Space(LMI930C.REC_SPACE_CNT))
            setData.Append(vbNewLine)

        Next

        '保存先のTXTファイルのパス
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'T023' AND ", _
                                                                                                        "KBN_CD = '01'"))

        Dim sysdate As String() = MyBase.GetSystemDateTime()

        Dim TXTPath As String = String.Concat(kbnDr(0).Item("KBN_NM1").ToString, _
                                              kbnDr(0).Item("KBN_NM2").ToString, _
                                              sysdate(0), "_", sysdate(1), _
                                              ".txt")

        'TXTファイルに書き込むときに使うEncoding
        'Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM1").ToString)
        Dim sr As StreamWriter = New StreamWriter(TXTPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

        Return True

    End Function

#End Region

#Region "DataSet設定"

#End Region

#End Region 'Method

End Class
