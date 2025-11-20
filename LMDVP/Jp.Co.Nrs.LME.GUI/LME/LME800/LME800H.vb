' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 
'  プログラムID     :  LME800H : 現場作業指示
'  作  成  者       :  [hojo]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LME800ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
Public Class LME800H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconV As LMEControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconH As LMEControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconG As LMEControlG

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
        Select Case Me._PrmDs.Tables(LME800C.TABLE_NM.LME800IN).Rows(0).Item("PROC_TYPE").ToString
            Case LME800C.PROC_TYPE.INSTRUCT
                '作業指示
                Me.WHSagyoShiji(Me._PrmDs)

            Case LME800C.PROC_TYPE.CANCEL
                '作業指示取り消し
                Me.WHSagyoShijiCancel(Me._PrmDs)

        End Select

    End Sub

#End Region
#Region "現場作業指示"

    ''' <summary>
    ''' 現場作業指示
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function WHSagyoShiji(ByVal ds As DataSet) As DataSet


        Dim dt As DataTable = ds.Tables(LME800C.TABLE_NM.LME800IN)

        '更新件数確認
        If dt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E009")
            Return ds
        End If

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LME800BLF", "WHSagyoShiji", ds)

        'メッセージがある場合表示
        If MyBase.IsMessageStoreExist Then
            MyBase.SetMessage("E998")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
        End If

        Return ds

    End Function
#End Region

#Region "現場作業指示取消"
    ''' <summary>
    ''' 現場作業指示取消
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function WHSagyoShijiCancel(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LME800C.TABLE_NM.LME800IN)

        '更新件数確認
        If dt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E009")
            Return ds
        End If

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LME800BLF", "WHSagyoShijiCancel", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
