' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM370G : 庭先情報マスタメンテ
'  作  成  者     : [wang]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMM360Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM370G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM370F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM370F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim unLock As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = LMMControlC.FUNCTION_F2_HENSHU
            .F3ButtonName = LMMControlC.FUNCTION_F3_FUKUSHA
            .F4ButtonName = LMMControlC.FUNCTION_F4_SAKUJO_HUKKATU
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = unLock
            .F3ButtonEnabled = unLock
            .F4ButtonEnabled = unLock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = unLock
            .F12ButtonEnabled = unLock

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' ステータス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(Optional ByVal dispMd As String = DispMode.VIEW, _
                                Optional ByVal recSts As String = RecordStatus.NOMAL_REC)

        With Me._Frm
            .lblSituation.DispMode = dispMd
            .lblSituation.RecordStatus = recSts
        End With

    End Sub

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .sprDetail.TabIndex = LMM370C.CtlTabIndex.DETAIL

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM360C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbBox()


    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        'numberCellの桁数を設定する
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d10 As Decimal = Convert.ToDecimal(9999999999)
            Dim d100 As Decimal = Convert.ToDecimal(100.0)
            Dim d9 As Decimal = Convert.ToDecimal(999999999)

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Me.ClearControl()

                Case DispMode.EDIT

                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC

                            '新規
                        Case RecordStatus.NEW_REC

                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me.ClearControlFukusha()

                    End Select

                Case DispMode.INIT
                    Me.ClearControl()


            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM370C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM370C.EventShubetsu.MAIN, LMM370C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM370C.EventShubetsu.SHINKI, LMM370C.EventShubetsu.HUKUSHA
                Case LMM370C.EventShubetsu.HENSHU
            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm


        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定        
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)              '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQTO_CD, "請求先" & vbCrLf & "コード", 70, True)
        Public Shared SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQTO_NM, "請求先名", 250, True)
        Public Shared PTN_CD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.PTN_CD, "請求パターン" & vbCrLf & "コード", 120, True)
        Public Shared GROUP_KB_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.GROUP_KB_NM, "請求グループ" & vbCrLf & "コード区分", 100, True)
        Public Shared GROUP_KB As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.GROUP_KB, "請求グループコード区分", 100, False)
        Public Shared SEIQKMK_CD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQKMK_CD, "請求項目" & vbCrLf & "コード", 100, True)
        Public Shared KEISAN_TLGK As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.KEISAN_TLGK, "計算額", 120, True)
        Public Shared NEBIKI_RT As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.NEBIKI_RT, "値引率", 100, True)
        Public Shared NEBIKI_GK As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.NEBIKI_GK, "固定値引額", 100, True)
        Public Shared TEKIYO As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.TEKIYO, "摘要", 300, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        Public Shared SEIQKMK_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQKMK_NM, "請求項目名", 60, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(下部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef2

        'スプレッド(タイトル列)の設定
        Public Shared KEISAI_DATE As SpreadColProperty = New SpreadColProperty(LMM370C.SprColumnIndex2.KEISAI_DATE, "掲載日", 80, True)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(LMM370C.SprColumnIndex2.FILE_NAME, "ファイル名", 120, True)
        Public Shared FILE_PATH As SpreadColProperty = New SpreadColProperty(LMM370C.SprColumnIndex2.FILE_PATH, "ファイルパス", 150, True)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()
        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 3

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef2)

            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            .sprDetail.SetCellStyle(0, LMM370G.sprDetailDef2.KEISAI_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM370G.sprDetailDef2.FILE_NAME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM370G.sprDetailDef2.FILE_PATH.ColNo, lblStyle)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM370F)

        Dim spr As LMSpread = frm.sprDetail

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = 0
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            '値設定
            For i As Integer = 0 To 3
                'セルスタイル設定
                .SetCellStyle(i, LMM370G.sprDetailDef2.KEISAI_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM370G.sprDetailDef2.FILE_NAME.ColNo, sLabel)
                .SetCellStyle(i, LMM370G.sprDetailDef2.FILE_PATH.ColNo, sLabel)


                'セルに値を設定
                .SetCellValue(i, LMM370G.sprDetailDef2.KEISAI_DATE.ColNo, String.Empty)
                .SetCellValue(i, LMM370G.sprDetailDef2.FILE_NAME.ColNo, String.Empty)
                .SetCellValue(i, LMM370G.sprDetailDef2.FILE_PATH.ColNo, String.Empty)

                .ResumeLayout(True)
            Next

        End With



    End Sub

#End Region 'Spread

#End Region

End Class
