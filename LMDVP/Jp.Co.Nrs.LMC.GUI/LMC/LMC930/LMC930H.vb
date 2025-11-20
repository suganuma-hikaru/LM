' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC930H : 現場作業指示
'  作  成  者       :  [HOJO]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility


''' <summary>
''' LMC930ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
Public Class LMC930H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    '''  ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        '処理種別の判定
        Select Case Me._PrmDs.Tables(LMC930C.TABLE_NM.LMC930IN).Rows(0).Item("PROC_TYPE").ToString
            Case LMC930C.PROC_TYPE.INSTRUCT
                '作業指示
                Me.WHSagyoShiji(Me._PrmDs)

            Case LMC930C.PROC_TYPE.CANCEL
                '作業指示取り消し
                Me.WHSagyoShijiCancel(Me._PrmDs)

        End Select

    End Sub

#End Region

#Region "作業指示"
    ''' <summary>
    ''' 作業指示
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function WHSagyoShiji(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMC930C.TABLE_NM.LMC930IN)

        '件数確認
        If dt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E009")
            Return ds
        End If

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC930BLF", "WHSagyoShiji", ds)

        'メッセージがある場合表示
        If MyBase.IsMessageStoreExist Then
            MyBase.SetMessage("E998")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
        End If

        Return ds

    End Function

#End Region

#Region "作業指示取り消し"
    ''' <summary>
    ''' 作業指示取り消し
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function WHSagyoShijiCancel(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMC930C.TABLE_NM.LMC930IN)

        '件数確認
        If dt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E009")
            Return ds
        End If

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC930BLF", "WHSagyoShijiCancel", ds)

        'メッセージがある場合表示
        If MyBase.IsMessageStoreExist Then
            MyBase.SetMessage("E998")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
        End If

        Return ds

    End Function

#End Region

#Region "チェック"
    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function IsSingleCheck(ByVal ds As DataSet) As Boolean

    End Function

#End Region

#End Region

End Class
