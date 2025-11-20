' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG900V : 請求処理 請求取込データ抽出作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG900Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMG900V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 取得データ整合チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function ReturnChk(ByVal ds As DataSet) As Boolean

        Dim checkdt As DataTable = ds.Tables(LMG900C.TABLE_NM_OUT)
        Dim max As Integer = checkdt.Rows.Count - 1

        For i As Integer = 0 To max
            With checkdt.Rows(i)

                '各項目の必須チェックを行う
                '【請求グループコード区分】
                If String.IsNullOrEmpty(.Item("GROUP_KB").ToString) Then
                    Return False
                End If

                '【請求項目コード】
                If String.IsNullOrEmpty(.Item("SEIQKMK_CD").ToString) Then
                    Return False
                End If

                '【経理科目コード】
                If String.IsNullOrEmpty(.Item("KEIRI_KB").ToString) Then
                    Return False
                End If

                '【課税区分】
                If String.IsNullOrEmpty(.Item("TAX_KB").ToString) Then
                    Return False
                End If

                '【作成種別区分】
                If String.IsNullOrEmpty(.Item("MAKE_SYU_KB").ToString) Then
                    Return False
                End If

                '【部署コード】
                If String.IsNullOrEmpty(.Item("BUSYO_CD").ToString) Then
                    Return False
                End If

                '【計算額】
                If String.IsNullOrEmpty(.Item("KEISAN_TLGK").ToString) Then
                    Return False
                End If

                '【値引率】
                If String.IsNullOrEmpty(.Item("NEBIKI_RT").ToString) Then
                    Return False
                End If

                '【固定値引額】
                If String.IsNullOrEmpty(.Item("NEBIKI_GK").ToString) Then
                    Return False
                End If

                '【印刷順番】
                If String.IsNullOrEmpty(.Item("PRINT_SORT").ToString) Then
                    Return False
                End If

                '【テンプレート取込フラグ】
                If String.IsNullOrEmpty(.Item("TEMPLATE_IMP_FLG").ToString) Then
                    Return False
                End If

                '【削除フラグ】
                If String.IsNullOrEmpty(.Item("SYS_DEL_FLG").ToString) Then
                    Return False
                End If

                '【新規フラグ】
                If String.IsNullOrEmpty(.Item("INS_FLG").ToString) Then
                    Return False
                End If

            End With
        Next

        Return True

    End Function

#End Region

End Class
