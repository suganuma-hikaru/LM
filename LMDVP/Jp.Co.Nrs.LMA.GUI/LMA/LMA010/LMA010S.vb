' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  LMA010S : ログイン
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMA020セションクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' </histry>

Public Class LMA010S
    Inherits Jp.Co.Nrs.LM.Base.LMFormData

    ''' <summary>
    ''' ユーザーマスタアプリへのパラメータ設定
    ''' </summary>
    ''' <remarks>LMB010DSの行を設定します</remarks>
    Friend Function SetPrameterDs(ByVal frm As LMA010F, ByVal userDr As DataRow) As DataSet

        Dim ds As DataSet = New LMM010DS()

        '初期化行の取得
        Dim dt As DataTable = ds.Tables(LMControlC.LMM010C_TABLE_NM_IN)
        dt.ImportRow(userDr)
        Dim row As DataRow = dt.Rows(0)

        'パラメータ設定
        row.Item("PW") = frm.txtPassword.TextValue.Trim()
        row.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd().Trim()
        row.Item("SYS_UPD_DATE") = LMUserInfoManager.GetSysUpdDate().Trim()
        row.Item("SYS_UPD_TIME") = LMUserInfoManager.GetSysUpdTime().Trim()

        Return ds

    End Function

End Class