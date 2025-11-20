' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LML       : 協力会社
'  プログラムID     :  LML010    : 協力会社
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
'Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LML010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LML010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LML010DAC = New LML010DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

    'データセット名称
    Private Const TABLE_NM_INKA_L As String = "LML010_INKA_L"

#End Region

#Region "Const"

    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Method"


#End Region

#Region "チェック"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

    Private Function Sagyo_CHK1(ByVal ds As DataSet) As DataSet

        'DACアクセス
        ds = Me.DacAccess(ds, "Sagyo_CHK1")

        'エラーがあるかを判定
        'Return Not MyBase.IsMessageExist()
        Return ds
    End Function

    Private Function Sagyo_CHK2(ByVal ds As DataSet) As DataSet

        'DACアクセス
        ds = Me.DacAccess(ds, "Sagyo_CHK2")

        Return ds
    End Function

#End Region

#Region "データ作成処理"

    ''' <summary>
    ''' データ作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Data_creat(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False

        '作成処理
        rtnResult = Me.Data_creat_rtn(ds)

        Return ds
    End Function


    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function Data_creat_rtn(ByVal ds As DataSet) As Boolean
        Dim sMsg As String = String.Empty

#If False Then  'DEL 2021/07/08 022255   【LMS】協力会社機能_商品マスタと届け先マスタの取込方法変更(群馬大隅)
        sMsg = "M_GOODS削除"
        Dim rtnResult As Boolean = Me.SyoriAccess(ds, "DeleteGOODS")

        sMsg = "M_GOODS_DETAILS削除"
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteGOODS_DETAILS")
#Else
        Dim rtnResult As Boolean = True

#End If

#If True Then   'ADD 2022/03/30 
        'WK_RT_GOODS詳細削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteWK_RTGOODS_DETAILS")

        'WK_RT_GOODS削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteWK_RTGOODS")
#End If
        'M_GOODS詳細作成　詳細を先に処理する
#If False Then  'UPD  2022/03/30 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertGOODS_DETAILS")
#Else
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertWK_RTGOODS_DETAILS")

        'WK_RT_GOODS_DETAILS 更新
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateWK_RTGOODS_DETAILS")

        'M_GOODS詳細追加
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertGOODS_DETAILS")
#End If

#If False Then  'UPD  2022/03/30 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertGOODS")
#Else
        'WK_RT_GOODS 作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertWK_RTGOODS")

        'WK_RT_GOODS 更新
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateWK_RTGOODS")

        'M_GOODS
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertGOODS")
#End If

#If False Then  'DEL 2021/07/08 022255   【LMS】協力会社機能_商品マスタと届け先マスタの取込方法変更(群馬大隅)
        'M_DEST削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteDEST")

        'M_DEST_DETAILS削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteDEST_DETAILS")

#End If

#If True Then   'ADD 2022/03/31　 028346
        'WK_RT_DEST_DETAIL削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteWK_RTDEST_DETAILS")

        'WK_RT_DEST削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteWK_RTDEST")
#End If

        'M_DEST詳細作成 詳細を先に処理する
#If False Then  'UPD  2022/03/30 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertDEST_DETAILS")

＃Else
        'WK_RT_DEST_DETAILS 追加分作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertWK_RTDEST_DETAILS")

        'WK_RT_DEST_DETAISL 更新
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateWK_RTDEST_DETAILS")

        'M_DEST_DETAILS詳細追加
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertDEST_DETAILS")

#End If

#If True Then   'ADD 2022/03/31　 028346
        'WK_RT_DEST作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertWK_RTDEST")

        'WK_RT_DEST 更新
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateWK_RTDEST")

        'M_DEST追加
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertDEST")

#End If

        'INKA_L削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteINKA_L")

        'INKA_M削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteINKA_M")

        'INKA_S削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteINKA_S")

        'INKA_L作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertINKA_L")

        'INKA_L更新　ADD 2022/03/18 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateINKA_L")

        'INKA_M作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertINKA_M")

        'INKA_M更新　ADD 2022/03/18 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateINKA_M")

        'INKA_S作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertINKA_S")

        'INKA_S更新　ADD 2022/03/18 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateINKA_S")

        'OUTKA_L削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteOUTKA_L")

        'OUTLA_M削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteOUTKA_M")

        'OUTKA_S削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteOUTKA_S")

        'OUTKA_L作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertOUTKA_L")

        'OUTKA_L更新　ADD 2022/03/18 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateOUTKA_L")

        'OUTKA_M作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertOUTKA_M")

        'OUTKA_M更新　ADD 2022/03/18 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateOUTKA_M")

        'OUTKA_S作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertOUTKA_S")
        '----------------------------
        'OUTKA_M更新　ADD 2022/03/18 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateOUTKA_S")

        'D_ZAI_TRS削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteZAI_TRS")

        'D_ZAI_TRS作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertZAI_TRS")

        'D_ZAI_TRS更新　ADD 2022/03/29 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateZAI_TRS")

        '-------------------
#If True Then   'F_UNSO_LLはやらない
        'F_UNSO_LL削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteUNSO_LL")
#End If
        'F_UNSO_L削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteUNSO_L")

        'F_UNSO_M削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteUNSO_M")

#If True Then   'F_UNSO_LLはやらない やってる
        'F_UNSO_LL作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertUNSO_LL")

        'F_UNSO_LL更新　ADD 2022/04/01 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateUNSO_LL")

#End If

        'F_UNSO_L作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertUNSO_L")

        'F_UNSO_L更新　ADD 2022/03/24 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateUNSO_L")

        'F_UNSO_M作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertUNSO_M")

        'F_UNSO_M更新　
        'ADD 2022/03/17 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateUNSO_M")
        '-------------------

        'F_UNCHIN_TRS削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteUNCHIN_TRS")

        'F_UNCHIN_TRS作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertUNCHIN_TRS")

        'F_UNCHIN_TRS更新 ADD 2022/03/24 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateUNCHIN_TRS")

        '-------------------------
        'E_SAGYO削除
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "DeleteSAGYO")

        'E_SAGYO作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertSAGYO")

        'E_SAGYO更新 ADD 2022/03/24 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateSAGYO")

#If True Then   'ADD 2021/06/24  021756   【LMS】LML010　将栄物流取込　D_IDO_TRSへの書き込み機能追加And荷主コードの2番目も選択できるようにする
        'D_IDO_TRS削除"
        rtnResult = Me.SyoriAccess(ds, "DeleteD_IDO_TRS")

        'D_IDO_TRS作成
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "InsertD_IDO_TRS")

#End If

        'D_IDO_TRS更新  ADD 2022/03/29 028346
        rtnResult = rtnResult AndAlso Me.SyoriAccess(ds, "UpdateD_IDO_TRS")

        If rtnResult = False Then
            sMsg = String.Concat(sMsg, "で異常終了しました", vbCr, "再実行してください")
            MyBase.SetMessage("E01U", {sMsg})
        End If

        Return rtnResult

    End Function

    ''' <summary>SyoriAccess    ''' 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SyoriAccess(ByVal ds As DataSet, ByVal DelNM As String) As Boolean
        'DACアクセス
        ds = Me.DacAccess(ds, DelNM)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

        'Return ds
    End Function

#End Region

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


#End Region

End Class
