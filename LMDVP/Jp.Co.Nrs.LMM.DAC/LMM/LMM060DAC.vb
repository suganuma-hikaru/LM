' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM060DAC : 運賃タリフマスタ
'  作  成  者       :  kishi
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM060DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM060DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Field"
    ''' <summary>
    ''' カウントの保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Cnt As Integer = 0

#End Region

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(UNCHIN_TARIFF.UNCHIN_TARIFF_CD)                AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                  " & vbNewLine

    ''' <summary>
    ''' M_UNCHIN_TARIFFデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                            " & vbNewLine _
                                            & "	      UNCHIN_TARIFF.NRS_BR_CD                     AS NRS_BR_CD                    " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                             AS NRS_BR_NM                    " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.UNCHIN_TARIFF_CD              AS UNCHIN_TARIFF_CD             " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.DATA_TP                       AS DATA_TP                      " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                AS DATA_TP_NM                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.TABLE_TP                      AS TABLE_TP                     " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                                AS TABLE_TP_NM                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.STR_DATE                      AS STR_DATE                     " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.UNCHIN_TARIFF_REM             AS UNCHIN_TARIFF_REM            " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.UNCHIN_TARIFF_CD2             AS UNCHIN_TARIFF_CD2            " & vbNewLine _
                                            & "      ,UNCHIN_TARIFF.SYS_ENT_DATE                  AS SYS_ENT_DATE                 " & vbNewLine _
                                            & "      ,USER1.USER_NM                               AS SYS_ENT_USER_NM              " & vbNewLine _
                                            & "      ,UNCHIN_TARIFF.SYS_UPD_DATE                  AS SYS_UPD_DATE                 " & vbNewLine _
                                            & "      ,UNCHIN_TARIFF.SYS_UPD_TIME                  AS SYS_UPD_TIME                 " & vbNewLine _
                                            & "      ,USER2.USER_NM                               AS SYS_UPD_USER_NM              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.SYS_DEL_FLG                   AS SYS_DEL_FLG                  " & vbNewLine _
                                            & "	     ,KBN3.KBN_NM1                                AS SYS_DEL_NM                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.APPROVAL_CD                   AS APPROVAL_CD                  " & vbNewLine _
                                            & "	     ,KBN4.KBN_NM1                                AS APPROVAL_NM                  " & vbNewLine _
                                            & "	     ,USER3.USER_NM                               AS APPROVAL_USER                " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.APPROVAL_DATE                 AS APPROVAL_DATE                " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF.APPROVAL_TIME                 AS APPROVAL_TIME                " & vbNewLine

    'START YANAI 要望番号377
    '''' <summary>
    '''' M_UNCHIN_TARIFF(距離刻み/運賃)データ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA2 As String = " SELECT                                                                        " & vbNewLine _
    '                                        & "	      UNCHIN_TARIFF_DTL.NRS_BR_CD                 AS NRS_BR_CD                 " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD          " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD_EDA      AS UNCHIN_TARIFF_CD_EDA      " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.STR_DATE                  AS STR_DATE                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_REM         AS UNCHIN_TARIFF_REM         " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.DATA_TP                   AS DATA_TP                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.TABLE_TP                  AS TABLE_TP                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.CAR_TP                    AS CAR_TP                    " & vbNewLine _
    '                                        & "	     ,KBN1.KBN_NM1                                AS CAR_TP_S_NM               " & vbNewLine _
    '                                        & "	     ,KBN2.KBN_NM1                                AS CAR_TP_T_NM               " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.WT_LV                     AS WT_LV                     " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_1                   AS KYORI_1                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_2                   AS KYORI_2                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_3                   AS KYORI_3                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_4                   AS KYORI_4                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_5                   AS KYORI_5                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_6                   AS KYORI_6                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_7                   AS KYORI_7                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_8                   AS KYORI_8                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_9                   AS KYORI_9                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_10                  AS KYORI_10                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_11                  AS KYORI_11                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_12                  AS KYORI_12                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_13                  AS KYORI_13                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_14                  AS KYORI_14                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_15                  AS KYORI_15                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_16                  AS KYORI_16                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_17                  AS KYORI_17                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_18                  AS KYORI_18                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_19                  AS KYORI_19                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_20                  AS KYORI_20                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_21                  AS KYORI_21                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_22                  AS KYORI_22                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_23                  AS KYORI_23                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_24                  AS KYORI_24                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_25                  AS KYORI_25                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_26                  AS KYORI_26                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_27                  AS KYORI_27                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_28                  AS KYORI_28                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_29                  AS KYORI_29                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_30                  AS KYORI_30                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_31                  AS KYORI_31                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_32                  AS KYORI_32                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_33                  AS KYORI_33                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_34                  AS KYORI_34                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_35                  AS KYORI_35                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_36                  AS KYORI_36                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_37                  AS KYORI_37                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_38                  AS KYORI_38                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_39                  AS KYORI_39                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_40                  AS KYORI_40                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_41                  AS KYORI_41                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_42                  AS KYORI_42                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_43                  AS KYORI_43                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_44                  AS KYORI_44                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_45                  AS KYORI_45                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_46                  AS KYORI_46                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_47                  AS KYORI_47                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_48                  AS KYORI_48                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_49                  AS KYORI_49                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_50                  AS KYORI_50                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_51                  AS KYORI_51                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_52                  AS KYORI_52                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_53                  AS KYORI_53                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_54                  AS KYORI_54                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_55                  AS KYORI_55                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_56                  AS KYORI_56                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_57                  AS KYORI_57                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_58                  AS KYORI_58                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_59                  AS KYORI_59                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_60                  AS KYORI_60                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_61                  AS KYORI_61                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_62                  AS KYORI_62                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_63                  AS KYORI_63                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_64                  AS KYORI_64                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_65                  AS KYORI_65                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_66                  AS KYORI_66                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_67                  AS KYORI_67                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_68                  AS KYORI_68                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_69                  AS KYORI_69                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.KYORI_70                  AS KYORI_70                  " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.CITY_EXTC_A               AS CITY_EXTC_A               " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.CITY_EXTC_B               AS CITY_EXTC_B               " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.WINT_EXTC_A               AS WINT_EXTC_A               " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.WINT_EXTC_B               AS WINT_EXTC_B               " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.RELY_EXTC                 AS RELY_EXTC                 " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.INSU                      AS INSU                      " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.FRRY_EXTC_10KG            AS FRRY_EXTC_10KG            " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.FRRY_EXTC_PART            AS FRRY_EXTC_PART            " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD2         AS UNCHIN_TARIFF_CD2         " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF2.UNCHIN_TARIFF_REM            AS UNCHIN_TARIFF_REM2        " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_ENT_DATE              AS SYS_ENT_DATE              " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_ENT_TIME              AS SYS_ENT_TIME              " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_ENT_PGID              AS SYS_ENT_PGID              " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_ENT_USER              AS SYS_ENT_USER              " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_UPD_DATE              AS SYS_UPD_DATE              " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_UPD_TIME              AS SYS_UPD_TIME              " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_UPD_PGID              AS SYS_UPD_PGID              " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_UPD_USER              AS SYS_UPD_USER              " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_DTL.SYS_DEL_FLG               AS SYS_DEL_FLG               " & vbNewLine _
    '                                        & "	     ,'1'                                         AS UPD_FLG                   " & vbNewLine
    ''' <summary>
    ''' M_UNCHIN_TARIFF(距離刻み/運賃)データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = " SELECT                                                                        " & vbNewLine _
                                            & "	      UNCHIN_TARIFF_DTL.NRS_BR_CD                 AS NRS_BR_CD                 " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD          " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD_EDA      AS UNCHIN_TARIFF_CD_EDA      " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.STR_DATE                  AS STR_DATE                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_REM         AS UNCHIN_TARIFF_REM         " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.DATA_TP                   AS DATA_TP                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.TABLE_TP                  AS TABLE_TP                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.CAR_TP                    AS CAR_TP                    " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                AS CAR_TP_S_NM               " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                                AS CAR_TP_T_NM               " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.WT_LV                     AS WT_LV                     " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_1                   AS KYORI_1                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_2                   AS KYORI_2                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_3                   AS KYORI_3                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_4                   AS KYORI_4                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_5                   AS KYORI_5                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_6                   AS KYORI_6                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_7                   AS KYORI_7                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_8                   AS KYORI_8                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_9                   AS KYORI_9                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_10                  AS KYORI_10                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_11                  AS KYORI_11                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_12                  AS KYORI_12                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_13                  AS KYORI_13                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_14                  AS KYORI_14                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_15                  AS KYORI_15                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_16                  AS KYORI_16                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_17                  AS KYORI_17                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_18                  AS KYORI_18                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_19                  AS KYORI_19                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_20                  AS KYORI_20                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_21                  AS KYORI_21                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_22                  AS KYORI_22                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_23                  AS KYORI_23                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_24                  AS KYORI_24                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_25                  AS KYORI_25                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_26                  AS KYORI_26                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_27                  AS KYORI_27                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_28                  AS KYORI_28                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_29                  AS KYORI_29                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_30                  AS KYORI_30                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_31                  AS KYORI_31                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_32                  AS KYORI_32                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_33                  AS KYORI_33                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_34                  AS KYORI_34                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_35                  AS KYORI_35                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_36                  AS KYORI_36                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_37                  AS KYORI_37                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_38                  AS KYORI_38                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_39                  AS KYORI_39                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_40                  AS KYORI_40                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_41                  AS KYORI_41                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_42                  AS KYORI_42                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_43                  AS KYORI_43                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_44                  AS KYORI_44                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_45                  AS KYORI_45                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_46                  AS KYORI_46                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_47                  AS KYORI_47                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_48                  AS KYORI_48                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_49                  AS KYORI_49                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_50                  AS KYORI_50                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_51                  AS KYORI_51                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_52                  AS KYORI_52                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_53                  AS KYORI_53                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_54                  AS KYORI_54                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_55                  AS KYORI_55                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_56                  AS KYORI_56                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_57                  AS KYORI_57                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_58                  AS KYORI_58                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_59                  AS KYORI_59                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_60                  AS KYORI_60                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_61                  AS KYORI_61                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_62                  AS KYORI_62                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_63                  AS KYORI_63                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_64                  AS KYORI_64                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_65                  AS KYORI_65                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_66                  AS KYORI_66                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_67                  AS KYORI_67                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_68                  AS KYORI_68                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_69                  AS KYORI_69                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.KYORI_70                  AS KYORI_70                  " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.CITY_EXTC_A               AS CITY_EXTC_A               " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.CITY_EXTC_B               AS CITY_EXTC_B               " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.WINT_EXTC_A               AS WINT_EXTC_A               " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.WINT_EXTC_B               AS WINT_EXTC_B               " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.RELY_EXTC                 AS RELY_EXTC                 " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.INSU                      AS INSU                      " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.FRRY_EXTC_PART            AS FRRY_EXTC_PART            " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD2         AS UNCHIN_TARIFF_CD2         " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF2.UNCHIN_TARIFF_REM            AS UNCHIN_TARIFF_REM2        " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_ENT_DATE              AS SYS_ENT_DATE              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_ENT_TIME              AS SYS_ENT_TIME              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_ENT_PGID              AS SYS_ENT_PGID              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_ENT_USER              AS SYS_ENT_USER              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_UPD_DATE              AS SYS_UPD_DATE              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_UPD_TIME              AS SYS_UPD_TIME              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_UPD_PGID              AS SYS_UPD_PGID              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_UPD_USER              AS SYS_UPD_USER              " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_DTL.SYS_DEL_FLG               AS SYS_DEL_FLG               " & vbNewLine _
                                            & "	     ,'1'                                         AS UPD_FLG                   " & vbNewLine
    'END YANAI 要望番号377

    ''' <summary>
    ''' UNCHIN_TARIFF_M(MAX運賃タリフコード枝番)データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAX As String = " SELECT                                                                              " & vbNewLine _
                                            & "	      MAX(UNCHIN_TARIFF.UNCHIN_TARIFF_CD_EDA)     AS UNCHIN_TARIFF_MAXEDA          " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                                                " & vbNewLine _
                                      & "                   ( SELECT                                                                         " & vbNewLine _
                                      & "                       UNCHIN_TARIFF.NRS_BR_CD                    AS NRS_BR_CD                      " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.UNCHIN_TARIFF_CD             AS UNCHIN_TARIFF_CD               " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.STR_DATE                     AS STR_DATE                       " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.DATA_TP                      AS DATA_TP                        " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.TABLE_TP                     AS TABLE_TP                       " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.UNCHIN_TARIFF_REM            AS UNCHIN_TARIFF_REM              " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.UNCHIN_TARIFF_CD2            AS UNCHIN_TARIFF_CD2              " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.SYS_ENT_USER                 AS SYS_ENT_USER                   " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.SYS_UPD_USER                 AS SYS_UPD_USER                   " & vbNewLine _
                                      & "                      ,MAX(UNCHIN_TARIFF.SYS_ENT_DATE)            AS SYS_ENT_DATE                   " & vbNewLine _
                                      & "                      ,MAX(UNCHIN_TARIFF.SYS_UPD_DATE)            AS SYS_UPD_DATE                   " & vbNewLine _
                                      & "                      ,MAX(UNCHIN_TARIFF.SYS_UPD_TIME)            AS SYS_UPD_TIME                   " & vbNewLine _
                                      & "                      ,UNCHIN_TARIFF.SYS_DEL_FLG                  AS SYS_DEL_FLG                    " & vbNewLine _
                                      & "                      ,MAX(UNCHIN_TARIFF.APPROVAL_CD)             AS APPROVAL_CD                   " & vbNewLine _
                                      & "                      ,MAX(UNCHIN_TARIFF.APPROVAL_USER)           AS APPROVAL_USER                   " & vbNewLine _
                                      & "                      ,MAX(UNCHIN_TARIFF.APPROVAL_DATE)           AS APPROVAL_DATE                   " & vbNewLine _
                                      & "                      ,MAX(UNCHIN_TARIFF.APPROVAL_TIME)           AS APPROVAL_TIME                   " & vbNewLine _
                                      & "                     FROM                                                                           " & vbNewLine _
                                      & "                      $LM_MST$..M_UNCHIN_TARIFF   AS UNCHIN_TARIFF                                    " & vbNewLine _
                                      & "                     GROUP BY                                                                       " & vbNewLine _
                                      & "                        UNCHIN_TARIFF.NRS_BR_CD                                                     " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.UNCHIN_TARIFF_CD                                              " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.STR_DATE                                                      " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.DATA_TP                                                       " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.TABLE_TP                                                      " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.UNCHIN_TARIFF_REM                                             " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.UNCHIN_TARIFF_CD2                                             " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.SYS_ENT_USER                                                  " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.SYS_UPD_USER                                                  " & vbNewLine _
                                      & "                       ,UNCHIN_TARIFF.SYS_DEL_FLG                                                   " & vbNewLine _
                                      & "                      )    AS UNCHIN_TARIFF                                                         " & vbNewLine _
                                      & "      LEFT OUTER JOIN $LM_MST$..S_USER AS USER1                                                     " & vbNewLine _
                                      & "        ON UNCHIN_TARIFF.SYS_ENT_USER    = USER1.USER_CD                                            " & vbNewLine _
                                      & "       AND USER1.SYS_DEL_FLG    = '0'                                                               " & vbNewLine _
                                      & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER2                                                    " & vbNewLine _
                                      & "       ON UNCHIN_TARIFF.SYS_UPD_USER     = USER2.USER_CD                                            " & vbNewLine _
                                      & "       AND USER2.SYS_DEL_FLG    = '0'                                                               " & vbNewLine _
                                      & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER3                                                    " & vbNewLine _
                                      & "       ON UNCHIN_TARIFF.APPROVAL_USER    = USER3.USER_CD                                            " & vbNewLine _
                                      & "       AND USER3.SYS_DEL_FLG    = '0'                                                               " & vbNewLine _
                                      & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR                                                  " & vbNewLine _
                                      & "        ON UNCHIN_TARIFF.NRS_BR_CD       = NRSBR.NRS_BR_CD                                          " & vbNewLine _
                                      & "       AND NRSBR.SYS_DEL_FLG    = '0'                                                               " & vbNewLine _
                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                                                   " & vbNewLine _
                                      & "        ON UNCHIN_TARIFF.DATA_TP        = KBN1.KBN_CD                                               " & vbNewLine _
                                      & "       AND KBN1.KBN_GROUP_CD    = 'U010'                                                            " & vbNewLine _
                                      & "       AND KBN1.SYS_DEL_FLG     = '0'                                                               " & vbNewLine _
                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                                                   " & vbNewLine _
                                      & "        ON UNCHIN_TARIFF.TABLE_TP        = KBN2.KBN_CD                                              " & vbNewLine _
                                      & "       AND KBN2.KBN_GROUP_CD    = 'U011'                                                            " & vbNewLine _
                                      & "       AND KBN2.SYS_DEL_FLG     = '0'                                                               " & vbNewLine _
                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3                                                   " & vbNewLine _
                                      & "        ON UNCHIN_TARIFF.SYS_DEL_FLG     = KBN3.KBN_CD                                              " & vbNewLine _
                                      & "       AND KBN3.KBN_GROUP_CD    = 'S051'                                                            " & vbNewLine _
                                      & "       AND KBN3.SYS_DEL_FLG     = '0'                                                               " & vbNewLine _
                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN4                                                   " & vbNewLine _
                                      & "        ON UNCHIN_TARIFF.APPROVAL_CD     = KBN4.KBN_CD                                              " & vbNewLine _
                                      & "       AND KBN4.KBN_GROUP_CD    = 'A009'                                                            " & vbNewLine _
                                      & "       AND KBN4.SYS_DEL_FLG     = '0'                                                               " & vbNewLine


    Private Const SQL_FROM_DATA2 As String = "FROM                                                                                 " & vbNewLine _
                                          & "                      $LM_MST$..M_UNCHIN_TARIFF AS UNCHIN_TARIFF_DTL                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                                     " & vbNewLine _
                                          & "        ON UNCHIN_TARIFF_DTL.CAR_TP = KBN1.KBN_CD                                     " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD     = 'S012'                                             " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG      = '0'                                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                                     " & vbNewLine _
                                          & "        ON UNCHIN_TARIFF_DTL.CAR_TP = KBN2.KBN_CD                                     " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD     = 'T010'                                             " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG      = '0'                                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_UNCHIN_TARIFF AS UNCHIN_TARIFF2                     " & vbNewLine _
                                          & "        ON UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD2  = UNCHIN_TARIFF2.UNCHIN_TARIFF_CD     " & vbNewLine _
                                          & "       AND UNCHIN_TARIFF2.DATA_TP               = '00'                                " & vbNewLine


    Private Const SQL_FROM_MAX As String = "FROM                                                                   " & vbNewLine _
                                          & "                       $LM_MST$..M_UNCHIN_TARIFF  UNCHIN_TARIFF       " & vbNewLine

#End Region

#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY(M_UNCHIN_TARIFF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY                                               " & vbNewLine _
                                         & "     UNCHIN_TARIFF.NRS_BR_CD                           " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.NRS_BR_NM                           " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.UNCHIN_TARIFF_CD                    " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.DATA_TP                             " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.DATA_TP_NM                          " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.TABLE_TP                            " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.TABLE_TP_NM                         " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.STR_DATE                            " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.UNCHIN_TARIFF_REM                   " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.UNCHIN_TARIFF_CD2                   " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.SYS_ENT_DATE                        " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.SYS_DEL_FLG                         " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.SYS_DEL_NM                          " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(M_UNCHIN_TARIFF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     UNCHIN_TARIFF.UNCHIN_TARIFF_CD                    " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.STR_DATE                            " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.DATA_TP                             " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF.TABLE_TP                            " & vbNewLine


    ''' <summary>
    ''' ORDER BY_EXCEL(M_UNCHIN_TARIFF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "ORDER BY                                              " & vbNewLine _
                                         & "     UNCHIN_TARIFF_DTL.NRS_BR_CD                       " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD                " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF_DTL.STR_DATE                        " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF_DTL.DATA_TP                         " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF_DTL.TABLE_TP                        " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF_DTL.CAR_TP                          " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF_DTL.WT_LV                           " & vbNewLine _
                                         & "    ,UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD_EDA            " & vbNewLine


#End Region

#Region "検索処理 SQL"

#Region "UNCHIN_TARIFF"


#End Region

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 運賃タリフコード存在チェック用(ﾚｺｰﾄﾞｽﾃｰﾀｽ="新規"or"複写"で保存する場合)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_UNCHIN As String = "SELECT                                         " & vbNewLine _
                                            & "   COUNT(UNCHIN_TARIFF_CD)  AS REC_CNT         " & vbNewLine _
                                            & "FROM $LM_MST$..M_UNCHIN_TARIFF                 " & vbNewLine _
                                            & "WHERE UNCHIN_TARIFF_CD    = @UNCHIN_TARIFF_CD  " & vbNewLine _
                                            & "  AND STR_DATE            = @STR_DATE          " & vbNewLine


    ''' <summary>
    ''' 運賃タリフコード重複チェック用(ﾚｺｰﾄﾞｽﾃｰﾀｽ="正常"で保存する場合)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_UNCHIN_DOUBLE As String = "SELECT                                  " & vbNewLine _
                                            & "   COUNT(UNCHIN_TARIFF_CD)  AS REC_CNT         " & vbNewLine _
                                            & "FROM $LM_MST$..M_UNCHIN_TARIFF                 " & vbNewLine _
                                            & "WHERE UNCHIN_TARIFF_CD    = @UNCHIN_TARIFF_CD  " & vbNewLine _
                                            & "  AND STR_DATE            = @STR_DATE          " & vbNewLine _
                                            & "  AND DATA_TP             = @DATA_TP           " & vbNewLine _
                                            & "  AND TABLE_TP            = @TABLE_TP          " & vbNewLine _
                                            & "  AND CAR_TP              = @CAR_TP            " & vbNewLine _
                                            & "  AND WT_LV               = @WT_LV             " & vbNewLine



#End Region

#End Region

#Region "設定処理 SQL"

#Region "INSERT"

    'START YANAI 要望番号377
    '''' <summary>
    '''' 新規登録SQL（M_UNCHIN_TARIFF）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_UNCHIN_TARIFF      " & vbNewLine _
    '                                   & "(                                          " & vbNewLine _
    '                                   & "      NRS_BR_CD                            " & vbNewLine _
    '                                   & "      ,UNCHIN_TARIFF_CD                    " & vbNewLine _
    '                                   & "      ,UNCHIN_TARIFF_CD_EDA                " & vbNewLine _
    '                                   & "      ,STR_DATE                            " & vbNewLine _
    '                                   & "      ,UNCHIN_TARIFF_REM                   " & vbNewLine _
    '                                   & "      ,DATA_TP                             " & vbNewLine _
    '                                   & "      ,TABLE_TP                            " & vbNewLine _
    '                                   & "      ,CAR_TP                              " & vbNewLine _
    '                                   & "      ,WT_LV                               " & vbNewLine _
    '                                   & "      ,KYORI_1                             " & vbNewLine _
    '                                   & "      ,KYORI_2                             " & vbNewLine _
    '                                   & "      ,KYORI_3                             " & vbNewLine _
    '                                   & "      ,KYORI_4                             " & vbNewLine _
    '                                   & "      ,KYORI_5                             " & vbNewLine _
    '                                   & "      ,KYORI_6                             " & vbNewLine _
    '                                   & "      ,KYORI_7                             " & vbNewLine _
    '                                   & "      ,KYORI_8                             " & vbNewLine _
    '                                   & "      ,KYORI_9                             " & vbNewLine _
    '                                   & "      ,KYORI_10                            " & vbNewLine _
    '                                   & "      ,KYORI_11                            " & vbNewLine _
    '                                   & "      ,KYORI_12                            " & vbNewLine _
    '                                   & "      ,KYORI_13                            " & vbNewLine _
    '                                   & "      ,KYORI_14                            " & vbNewLine _
    '                                   & "      ,KYORI_15                            " & vbNewLine _
    '                                   & "      ,KYORI_16                            " & vbNewLine _
    '                                   & "      ,KYORI_17                            " & vbNewLine _
    '                                   & "      ,KYORI_18                            " & vbNewLine _
    '                                   & "      ,KYORI_19                            " & vbNewLine _
    '                                   & "      ,KYORI_20                            " & vbNewLine _
    '                                   & "      ,KYORI_21                            " & vbNewLine _
    '                                   & "      ,KYORI_22                            " & vbNewLine _
    '                                   & "      ,KYORI_23                            " & vbNewLine _
    '                                   & "      ,KYORI_24                            " & vbNewLine _
    '                                   & "      ,KYORI_25                            " & vbNewLine _
    '                                   & "      ,KYORI_26                            " & vbNewLine _
    '                                   & "      ,KYORI_27                            " & vbNewLine _
    '                                   & "      ,KYORI_28                            " & vbNewLine _
    '                                   & "      ,KYORI_29                            " & vbNewLine _
    '                                   & "      ,KYORI_30                            " & vbNewLine _
    '                                   & "      ,KYORI_31                            " & vbNewLine _
    '                                   & "      ,KYORI_32                            " & vbNewLine _
    '                                   & "      ,KYORI_33                            " & vbNewLine _
    '                                   & "      ,KYORI_34                            " & vbNewLine _
    '                                   & "      ,KYORI_35                            " & vbNewLine _
    '                                   & "      ,KYORI_36                            " & vbNewLine _
    '                                   & "      ,KYORI_37                            " & vbNewLine _
    '                                   & "      ,KYORI_38                            " & vbNewLine _
    '                                   & "      ,KYORI_39                            " & vbNewLine _
    '                                   & "      ,KYORI_40                            " & vbNewLine _
    '                                   & "      ,KYORI_41                            " & vbNewLine _
    '                                   & "      ,KYORI_42                            " & vbNewLine _
    '                                   & "      ,KYORI_43                            " & vbNewLine _
    '                                   & "      ,KYORI_44                            " & vbNewLine _
    '                                   & "      ,KYORI_45                            " & vbNewLine _
    '                                   & "      ,KYORI_46                            " & vbNewLine _
    '                                   & "      ,KYORI_47                            " & vbNewLine _
    '                                   & "      ,KYORI_48                            " & vbNewLine _
    '                                   & "      ,KYORI_49                            " & vbNewLine _
    '                                   & "      ,KYORI_50                            " & vbNewLine _
    '                                   & "      ,KYORI_51                            " & vbNewLine _
    '                                   & "      ,KYORI_52                            " & vbNewLine _
    '                                   & "      ,KYORI_53                            " & vbNewLine _
    '                                   & "      ,KYORI_54                            " & vbNewLine _
    '                                   & "      ,KYORI_55                            " & vbNewLine _
    '                                   & "      ,KYORI_56                            " & vbNewLine _
    '                                   & "      ,KYORI_57                            " & vbNewLine _
    '                                   & "      ,KYORI_58                            " & vbNewLine _
    '                                   & "      ,KYORI_59                            " & vbNewLine _
    '                                   & "      ,KYORI_60                            " & vbNewLine _
    '                                   & "      ,KYORI_61                            " & vbNewLine _
    '                                   & "      ,KYORI_62                            " & vbNewLine _
    '                                   & "      ,KYORI_63                            " & vbNewLine _
    '                                   & "      ,KYORI_64                            " & vbNewLine _
    '                                   & "      ,KYORI_65                            " & vbNewLine _
    '                                   & "      ,KYORI_66                            " & vbNewLine _
    '                                   & "      ,KYORI_67                            " & vbNewLine _
    '                                   & "      ,KYORI_68                            " & vbNewLine _
    '                                   & "      ,KYORI_69                            " & vbNewLine _
    '                                   & "      ,KYORI_70                            " & vbNewLine _
    '                                   & "      ,CITY_EXTC_A                         " & vbNewLine _
    '                                   & "      ,CITY_EXTC_B                         " & vbNewLine _
    '                                   & "      ,WINT_EXTC_A                         " & vbNewLine _
    '                                   & "      ,WINT_EXTC_B                         " & vbNewLine _
    '                                   & "      ,RELY_EXTC                           " & vbNewLine _
    '                                   & "      ,INSU                                " & vbNewLine _
    '                                   & "      ,FRRY_EXTC_10KG                      " & vbNewLine _
    '                                   & "      ,FRRY_EXTC_PART                      " & vbNewLine _
    '                                   & "      ,UNCHIN_TARIFF_CD2                   " & vbNewLine _
    '                                   & "      ,SYS_ENT_DATE                        " & vbNewLine _
    '                                   & "      ,SYS_ENT_TIME                        " & vbNewLine _
    '                                   & "      ,SYS_ENT_PGID                        " & vbNewLine _
    '                                   & "      ,SYS_ENT_USER                        " & vbNewLine _
    '                                   & "      ,SYS_UPD_DATE                        " & vbNewLine _
    '                                   & "      ,SYS_UPD_TIME                        " & vbNewLine _
    '                                   & "      ,SYS_UPD_PGID                        " & vbNewLine _
    '                                   & "      ,SYS_UPD_USER                        " & vbNewLine _
    '                                   & "      ,SYS_DEL_FLG                         " & vbNewLine _
    '                                   & "      ) VALUES (                           " & vbNewLine _
    '                                   & "      @NRS_BR_CD                           " & vbNewLine _
    '                                   & "      ,@UNCHIN_TARIFF_CD                   " & vbNewLine _
    '                                   & "      ,@UNCHIN_TARIFF_CD_EDA               " & vbNewLine _
    '                                   & "      ,@STR_DATE                           " & vbNewLine _
    '                                   & "      ,@UNCHIN_TARIFF_REM                  " & vbNewLine _
    '                                   & "      ,@DATA_TP                            " & vbNewLine _
    '                                   & "      ,@TABLE_TP                           " & vbNewLine _
    '                                   & "      ,@CAR_TP                             " & vbNewLine _
    '                                   & "      ,@WT_LV                              " & vbNewLine _
    '                                   & "      ,@KYORI_1                            " & vbNewLine _
    '                                   & "      ,@KYORI_2                            " & vbNewLine _
    '                                   & "      ,@KYORI_3                            " & vbNewLine _
    '                                   & "      ,@KYORI_4                            " & vbNewLine _
    '                                   & "      ,@KYORI_5                            " & vbNewLine _
    '                                   & "      ,@KYORI_6                            " & vbNewLine _
    '                                   & "      ,@KYORI_7                            " & vbNewLine _
    '                                   & "      ,@KYORI_8                            " & vbNewLine _
    '                                   & "      ,@KYORI_9                            " & vbNewLine _
    '                                   & "      ,@KYORI_10                           " & vbNewLine _
    '                                   & "      ,@KYORI_11                           " & vbNewLine _
    '                                   & "      ,@KYORI_12                           " & vbNewLine _
    '                                   & "      ,@KYORI_13                           " & vbNewLine _
    '                                   & "      ,@KYORI_14                           " & vbNewLine _
    '                                   & "      ,@KYORI_15                           " & vbNewLine _
    '                                   & "      ,@KYORI_16                           " & vbNewLine _
    '                                   & "      ,@KYORI_17                           " & vbNewLine _
    '                                   & "      ,@KYORI_18                           " & vbNewLine _
    '                                   & "      ,@KYORI_19                           " & vbNewLine _
    '                                   & "      ,@KYORI_20                           " & vbNewLine _
    '                                   & "      ,@KYORI_21                           " & vbNewLine _
    '                                   & "      ,@KYORI_22                           " & vbNewLine _
    '                                   & "      ,@KYORI_23                           " & vbNewLine _
    '                                   & "      ,@KYORI_24                           " & vbNewLine _
    '                                   & "      ,@KYORI_25                           " & vbNewLine _
    '                                   & "      ,@KYORI_26                           " & vbNewLine _
    '                                   & "      ,@KYORI_27                           " & vbNewLine _
    '                                   & "      ,@KYORI_28                           " & vbNewLine _
    '                                   & "      ,@KYORI_29                           " & vbNewLine _
    '                                   & "      ,@KYORI_30                           " & vbNewLine _
    '                                   & "      ,@KYORI_31                           " & vbNewLine _
    '                                   & "      ,@KYORI_32                           " & vbNewLine _
    '                                   & "      ,@KYORI_33                           " & vbNewLine _
    '                                   & "      ,@KYORI_34                           " & vbNewLine _
    '                                   & "      ,@KYORI_35                           " & vbNewLine _
    '                                   & "      ,@KYORI_36                           " & vbNewLine _
    '                                   & "      ,@KYORI_37                           " & vbNewLine _
    '                                   & "      ,@KYORI_38                           " & vbNewLine _
    '                                   & "      ,@KYORI_39                           " & vbNewLine _
    '                                   & "      ,@KYORI_40                           " & vbNewLine _
    '                                   & "      ,@KYORI_41                           " & vbNewLine _
    '                                   & "      ,@KYORI_42                           " & vbNewLine _
    '                                   & "      ,@KYORI_43                           " & vbNewLine _
    '                                   & "      ,@KYORI_44                           " & vbNewLine _
    '                                   & "      ,@KYORI_45                           " & vbNewLine _
    '                                   & "      ,@KYORI_46                           " & vbNewLine _
    '                                   & "      ,@KYORI_47                           " & vbNewLine _
    '                                   & "      ,@KYORI_48                           " & vbNewLine _
    '                                   & "      ,@KYORI_49                           " & vbNewLine _
    '                                   & "      ,@KYORI_50                           " & vbNewLine _
    '                                   & "      ,@KYORI_51                           " & vbNewLine _
    '                                   & "      ,@KYORI_52                           " & vbNewLine _
    '                                   & "      ,@KYORI_53                           " & vbNewLine _
    '                                   & "      ,@KYORI_54                           " & vbNewLine _
    '                                   & "      ,@KYORI_55                           " & vbNewLine _
    '                                   & "      ,@KYORI_56                           " & vbNewLine _
    '                                   & "      ,@KYORI_57                           " & vbNewLine _
    '                                   & "      ,@KYORI_58                           " & vbNewLine _
    '                                   & "      ,@KYORI_59                           " & vbNewLine _
    '                                   & "      ,@KYORI_60                           " & vbNewLine _
    '                                   & "      ,@KYORI_61                           " & vbNewLine _
    '                                   & "      ,@KYORI_62                           " & vbNewLine _
    '                                   & "      ,@KYORI_63                           " & vbNewLine _
    '                                   & "      ,@KYORI_64                           " & vbNewLine _
    '                                   & "      ,@KYORI_65                           " & vbNewLine _
    '                                   & "      ,@KYORI_66                           " & vbNewLine _
    '                                   & "      ,@KYORI_67                           " & vbNewLine _
    '                                   & "      ,@KYORI_68                           " & vbNewLine _
    '                                   & "      ,@KYORI_69                           " & vbNewLine _
    '                                   & "      ,@KYORI_70                           " & vbNewLine _
    '                                   & "      ,@CITY_EXTC_A                        " & vbNewLine _
    '                                   & "      ,@CITY_EXTC_B                        " & vbNewLine _
    '                                   & "      ,@WINT_EXTC_A                        " & vbNewLine _
    '                                   & "      ,@WINT_EXTC_B                        " & vbNewLine _
    '                                   & "      ,@RELY_EXTC                          " & vbNewLine _
    '                                   & "      ,@INSU                               " & vbNewLine _
    '                                   & "      ,@FRRY_EXTC_10KG                     " & vbNewLine _
    '                                   & "      ,@FRRY_EXTC_PART                     " & vbNewLine _
    '                                   & "      ,@UNCHIN_TARIFF_CD2                  " & vbNewLine _
    '                                   & "      ,@SYS_ENT_DATE         	             " & vbNewLine _
    '                                   & "      ,@SYS_ENT_TIME         	             " & vbNewLine _
    '                                   & "      ,@SYS_ENT_PGID         	             " & vbNewLine _
    '                                   & "      ,@SYS_ENT_USER         	             " & vbNewLine _
    '                                   & "      ,@SYS_UPD_DATE         	             " & vbNewLine _
    '                                   & "      ,@SYS_UPD_TIME         	             " & vbNewLine _
    '                                   & "      ,@SYS_UPD_PGID         	             " & vbNewLine _
    '                                   & "      ,@SYS_UPD_USER         	             " & vbNewLine _
    '                                   & "      ,@SYS_DEL_FLG         	             " & vbNewLine _
    '                                   & ")                                          " & vbNewLine
    ''' <summary>
    ''' 新規登録SQL（M_UNCHIN_TARIFF）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_UNCHIN_TARIFF      " & vbNewLine _
                                       & "(                                          " & vbNewLine _
                                       & "      NRS_BR_CD                            " & vbNewLine _
                                       & "      ,UNCHIN_TARIFF_CD                    " & vbNewLine _
                                       & "      ,UNCHIN_TARIFF_CD_EDA                " & vbNewLine _
                                       & "      ,STR_DATE                            " & vbNewLine _
                                       & "      ,UNCHIN_TARIFF_REM                   " & vbNewLine _
                                       & "      ,DATA_TP                             " & vbNewLine _
                                       & "      ,TABLE_TP                            " & vbNewLine _
                                       & "      ,CAR_TP                              " & vbNewLine _
                                       & "      ,WT_LV                               " & vbNewLine _
                                       & "      ,KYORI_1                             " & vbNewLine _
                                       & "      ,KYORI_2                             " & vbNewLine _
                                       & "      ,KYORI_3                             " & vbNewLine _
                                       & "      ,KYORI_4                             " & vbNewLine _
                                       & "      ,KYORI_5                             " & vbNewLine _
                                       & "      ,KYORI_6                             " & vbNewLine _
                                       & "      ,KYORI_7                             " & vbNewLine _
                                       & "      ,KYORI_8                             " & vbNewLine _
                                       & "      ,KYORI_9                             " & vbNewLine _
                                       & "      ,KYORI_10                            " & vbNewLine _
                                       & "      ,KYORI_11                            " & vbNewLine _
                                       & "      ,KYORI_12                            " & vbNewLine _
                                       & "      ,KYORI_13                            " & vbNewLine _
                                       & "      ,KYORI_14                            " & vbNewLine _
                                       & "      ,KYORI_15                            " & vbNewLine _
                                       & "      ,KYORI_16                            " & vbNewLine _
                                       & "      ,KYORI_17                            " & vbNewLine _
                                       & "      ,KYORI_18                            " & vbNewLine _
                                       & "      ,KYORI_19                            " & vbNewLine _
                                       & "      ,KYORI_20                            " & vbNewLine _
                                       & "      ,KYORI_21                            " & vbNewLine _
                                       & "      ,KYORI_22                            " & vbNewLine _
                                       & "      ,KYORI_23                            " & vbNewLine _
                                       & "      ,KYORI_24                            " & vbNewLine _
                                       & "      ,KYORI_25                            " & vbNewLine _
                                       & "      ,KYORI_26                            " & vbNewLine _
                                       & "      ,KYORI_27                            " & vbNewLine _
                                       & "      ,KYORI_28                            " & vbNewLine _
                                       & "      ,KYORI_29                            " & vbNewLine _
                                       & "      ,KYORI_30                            " & vbNewLine _
                                       & "      ,KYORI_31                            " & vbNewLine _
                                       & "      ,KYORI_32                            " & vbNewLine _
                                       & "      ,KYORI_33                            " & vbNewLine _
                                       & "      ,KYORI_34                            " & vbNewLine _
                                       & "      ,KYORI_35                            " & vbNewLine _
                                       & "      ,KYORI_36                            " & vbNewLine _
                                       & "      ,KYORI_37                            " & vbNewLine _
                                       & "      ,KYORI_38                            " & vbNewLine _
                                       & "      ,KYORI_39                            " & vbNewLine _
                                       & "      ,KYORI_40                            " & vbNewLine _
                                       & "      ,KYORI_41                            " & vbNewLine _
                                       & "      ,KYORI_42                            " & vbNewLine _
                                       & "      ,KYORI_43                            " & vbNewLine _
                                       & "      ,KYORI_44                            " & vbNewLine _
                                       & "      ,KYORI_45                            " & vbNewLine _
                                       & "      ,KYORI_46                            " & vbNewLine _
                                       & "      ,KYORI_47                            " & vbNewLine _
                                       & "      ,KYORI_48                            " & vbNewLine _
                                       & "      ,KYORI_49                            " & vbNewLine _
                                       & "      ,KYORI_50                            " & vbNewLine _
                                       & "      ,KYORI_51                            " & vbNewLine _
                                       & "      ,KYORI_52                            " & vbNewLine _
                                       & "      ,KYORI_53                            " & vbNewLine _
                                       & "      ,KYORI_54                            " & vbNewLine _
                                       & "      ,KYORI_55                            " & vbNewLine _
                                       & "      ,KYORI_56                            " & vbNewLine _
                                       & "      ,KYORI_57                            " & vbNewLine _
                                       & "      ,KYORI_58                            " & vbNewLine _
                                       & "      ,KYORI_59                            " & vbNewLine _
                                       & "      ,KYORI_60                            " & vbNewLine _
                                       & "      ,KYORI_61                            " & vbNewLine _
                                       & "      ,KYORI_62                            " & vbNewLine _
                                       & "      ,KYORI_63                            " & vbNewLine _
                                       & "      ,KYORI_64                            " & vbNewLine _
                                       & "      ,KYORI_65                            " & vbNewLine _
                                       & "      ,KYORI_66                            " & vbNewLine _
                                       & "      ,KYORI_67                            " & vbNewLine _
                                       & "      ,KYORI_68                            " & vbNewLine _
                                       & "      ,KYORI_69                            " & vbNewLine _
                                       & "      ,KYORI_70                            " & vbNewLine _
                                       & "      ,CITY_EXTC_A                         " & vbNewLine _
                                       & "      ,CITY_EXTC_B                         " & vbNewLine _
                                       & "      ,WINT_EXTC_A                         " & vbNewLine _
                                       & "      ,WINT_EXTC_B                         " & vbNewLine _
                                       & "      ,RELY_EXTC                           " & vbNewLine _
                                       & "      ,INSU                                " & vbNewLine _
                                       & "      ,FRRY_EXTC_PART                      " & vbNewLine _
                                       & "      ,UNCHIN_TARIFF_CD2                   " & vbNewLine _
                                       & "      ,APPROVAL_CD                         " & vbNewLine _
                                       & "      ,APPROVAL_USER                       " & vbNewLine _
                                       & "      ,APPROVAL_DATE                       " & vbNewLine _
                                       & "      ,APPROVAL_TIME                       " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                        " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                        " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                        " & vbNewLine _
                                       & "      ,SYS_ENT_USER                        " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                        " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                        " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                        " & vbNewLine _
                                       & "      ,SYS_UPD_USER                        " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                         " & vbNewLine _
                                       & "      ) VALUES (                           " & vbNewLine _
                                       & "      @NRS_BR_CD                           " & vbNewLine _
                                       & "      ,@UNCHIN_TARIFF_CD                   " & vbNewLine _
                                       & "      ,@UNCHIN_TARIFF_CD_EDA               " & vbNewLine _
                                       & "      ,@STR_DATE                           " & vbNewLine _
                                       & "      ,@UNCHIN_TARIFF_REM                  " & vbNewLine _
                                       & "      ,@DATA_TP                            " & vbNewLine _
                                       & "      ,@TABLE_TP                           " & vbNewLine _
                                       & "      ,@CAR_TP                             " & vbNewLine _
                                       & "      ,@WT_LV                              " & vbNewLine _
                                       & "      ,@KYORI_1                            " & vbNewLine _
                                       & "      ,@KYORI_2                            " & vbNewLine _
                                       & "      ,@KYORI_3                            " & vbNewLine _
                                       & "      ,@KYORI_4                            " & vbNewLine _
                                       & "      ,@KYORI_5                            " & vbNewLine _
                                       & "      ,@KYORI_6                            " & vbNewLine _
                                       & "      ,@KYORI_7                            " & vbNewLine _
                                       & "      ,@KYORI_8                            " & vbNewLine _
                                       & "      ,@KYORI_9                            " & vbNewLine _
                                       & "      ,@KYORI_10                           " & vbNewLine _
                                       & "      ,@KYORI_11                           " & vbNewLine _
                                       & "      ,@KYORI_12                           " & vbNewLine _
                                       & "      ,@KYORI_13                           " & vbNewLine _
                                       & "      ,@KYORI_14                           " & vbNewLine _
                                       & "      ,@KYORI_15                           " & vbNewLine _
                                       & "      ,@KYORI_16                           " & vbNewLine _
                                       & "      ,@KYORI_17                           " & vbNewLine _
                                       & "      ,@KYORI_18                           " & vbNewLine _
                                       & "      ,@KYORI_19                           " & vbNewLine _
                                       & "      ,@KYORI_20                           " & vbNewLine _
                                       & "      ,@KYORI_21                           " & vbNewLine _
                                       & "      ,@KYORI_22                           " & vbNewLine _
                                       & "      ,@KYORI_23                           " & vbNewLine _
                                       & "      ,@KYORI_24                           " & vbNewLine _
                                       & "      ,@KYORI_25                           " & vbNewLine _
                                       & "      ,@KYORI_26                           " & vbNewLine _
                                       & "      ,@KYORI_27                           " & vbNewLine _
                                       & "      ,@KYORI_28                           " & vbNewLine _
                                       & "      ,@KYORI_29                           " & vbNewLine _
                                       & "      ,@KYORI_30                           " & vbNewLine _
                                       & "      ,@KYORI_31                           " & vbNewLine _
                                       & "      ,@KYORI_32                           " & vbNewLine _
                                       & "      ,@KYORI_33                           " & vbNewLine _
                                       & "      ,@KYORI_34                           " & vbNewLine _
                                       & "      ,@KYORI_35                           " & vbNewLine _
                                       & "      ,@KYORI_36                           " & vbNewLine _
                                       & "      ,@KYORI_37                           " & vbNewLine _
                                       & "      ,@KYORI_38                           " & vbNewLine _
                                       & "      ,@KYORI_39                           " & vbNewLine _
                                       & "      ,@KYORI_40                           " & vbNewLine _
                                       & "      ,@KYORI_41                           " & vbNewLine _
                                       & "      ,@KYORI_42                           " & vbNewLine _
                                       & "      ,@KYORI_43                           " & vbNewLine _
                                       & "      ,@KYORI_44                           " & vbNewLine _
                                       & "      ,@KYORI_45                           " & vbNewLine _
                                       & "      ,@KYORI_46                           " & vbNewLine _
                                       & "      ,@KYORI_47                           " & vbNewLine _
                                       & "      ,@KYORI_48                           " & vbNewLine _
                                       & "      ,@KYORI_49                           " & vbNewLine _
                                       & "      ,@KYORI_50                           " & vbNewLine _
                                       & "      ,@KYORI_51                           " & vbNewLine _
                                       & "      ,@KYORI_52                           " & vbNewLine _
                                       & "      ,@KYORI_53                           " & vbNewLine _
                                       & "      ,@KYORI_54                           " & vbNewLine _
                                       & "      ,@KYORI_55                           " & vbNewLine _
                                       & "      ,@KYORI_56                           " & vbNewLine _
                                       & "      ,@KYORI_57                           " & vbNewLine _
                                       & "      ,@KYORI_58                           " & vbNewLine _
                                       & "      ,@KYORI_59                           " & vbNewLine _
                                       & "      ,@KYORI_60                           " & vbNewLine _
                                       & "      ,@KYORI_61                           " & vbNewLine _
                                       & "      ,@KYORI_62                           " & vbNewLine _
                                       & "      ,@KYORI_63                           " & vbNewLine _
                                       & "      ,@KYORI_64                           " & vbNewLine _
                                       & "      ,@KYORI_65                           " & vbNewLine _
                                       & "      ,@KYORI_66                           " & vbNewLine _
                                       & "      ,@KYORI_67                           " & vbNewLine _
                                       & "      ,@KYORI_68                           " & vbNewLine _
                                       & "      ,@KYORI_69                           " & vbNewLine _
                                       & "      ,@KYORI_70                           " & vbNewLine _
                                       & "      ,@CITY_EXTC_A                        " & vbNewLine _
                                       & "      ,@CITY_EXTC_B                        " & vbNewLine _
                                       & "      ,@WINT_EXTC_A                        " & vbNewLine _
                                       & "      ,@WINT_EXTC_B                        " & vbNewLine _
                                       & "      ,@RELY_EXTC                          " & vbNewLine _
                                       & "      ,@INSU                               " & vbNewLine _
                                       & "      ,@FRRY_EXTC_PART                     " & vbNewLine _
                                       & "      ,@UNCHIN_TARIFF_CD2                  " & vbNewLine _
                                       & "      ,@APPROVAL_CD                        " & vbNewLine _
                                       & "      ,@APPROVAL_USER                      " & vbNewLine _
                                       & "      ,@APPROVAL_DATE                      " & vbNewLine _
                                       & "      ,@APPROVAL_TIME                      " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         	             " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME         	             " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         	             " & vbNewLine _
                                       & "      ,@SYS_ENT_USER         	             " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE         	             " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME         	             " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID         	             " & vbNewLine _
                                       & "      ,@SYS_UPD_USER         	             " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         	             " & vbNewLine _
                                       & ")                                          " & vbNewLine
    'END YANAI 要望番号377


#End Region

#Region "Delete"

    ''' <summary>
    ''' 物理削除SQL（M_UNCHIN_TARIFF）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_UNCHIN_TARIFF As String = "DELETE FROM $LM_MST$..M_UNCHIN_TARIFF              " & vbNewLine _
                                              & "WHERE   UNCHIN_TARIFF_CD   = @UNCHIN_TARIFF_CD         " & vbNewLine _
                                       & "   AND  UNCHIN_TARIFF_CD_EDA      = @UNCHIN_TARIFF_CD_EDA     " & vbNewLine _
                                       & "   AND  STR_DATE                  = @STR_DATE                 " & vbNewLine _
                                       & "   AND  SYS_UPD_DATE              = @SYS_UPD_DATE             " & vbNewLine _
                                       & "   AND  SYS_UPD_TIME              = @SYS_UPD_TIME             " & vbNewLine

#End Region

#Region "UPDATE"

    'START YANAI 要望番号377
    '''' <summary>
    '''' 更新SQL（M_UNCHIN_TARIFF）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_UNCHIN_TARIFF SET                       " & vbNewLine _
    '                                   & "        NRS_BR_CD					= @NRS_BR_CD             " & vbNewLine _
    '                                   & "       ,UNCHIN_TARIFF_CD			= @UNCHIN_TARIFF_CD      " & vbNewLine _
    '                                   & "       ,UNCHIN_TARIFF_CD_EDA		= @UNCHIN_TARIFF_CD_EDA  " & vbNewLine _
    '                                   & "       ,STR_DATE					= @STR_DATE              " & vbNewLine _
    '                                   & "       ,UNCHIN_TARIFF_REM			= @UNCHIN_TARIFF_REM     " & vbNewLine _
    '                                   & "       ,DATA_TP					= @DATA_TP               " & vbNewLine _
    '                                   & "       ,TABLE_TP					= @TABLE_TP              " & vbNewLine _
    '                                   & "       ,CAR_TP					= @CAR_TP                " & vbNewLine _
    '                                   & "       ,WT_LV				        = @WT_LV                 " & vbNewLine _
    '                                   & "       ,KYORI_1					= @KYORI_1               " & vbNewLine _
    '                                   & "       ,KYORI_2					= @KYORI_2               " & vbNewLine _
    '                                   & "       ,KYORI_3					= @KYORI_3               " & vbNewLine _
    '                                   & "       ,KYORI_4					= @KYORI_4               " & vbNewLine _
    '                                   & "       ,KYORI_5					= @KYORI_5               " & vbNewLine _
    '                                   & "       ,KYORI_6					= @KYORI_6               " & vbNewLine _
    '                                   & "       ,KYORI_7					= @KYORI_7               " & vbNewLine _
    '                                   & "       ,KYORI_8					= @KYORI_8               " & vbNewLine _
    '                                   & "       ,KYORI_9					= @KYORI_9               " & vbNewLine _
    '                                   & "       ,KYORI_10					= @KYORI_10              " & vbNewLine _
    '                                   & "       ,KYORI_11					= @KYORI_11              " & vbNewLine _
    '                                   & "       ,KYORI_12					= @KYORI_12              " & vbNewLine _
    '                                   & "       ,KYORI_13					= @KYORI_13              " & vbNewLine _
    '                                   & "       ,KYORI_14					= @KYORI_14              " & vbNewLine _
    '                                   & "       ,KYORI_15					= @KYORI_15              " & vbNewLine _
    '                                   & "       ,KYORI_16					= @KYORI_16              " & vbNewLine _
    '                                   & "       ,KYORI_17					= @KYORI_17              " & vbNewLine _
    '                                   & "       ,KYORI_18					= @KYORI_18              " & vbNewLine _
    '                                   & "       ,KYORI_19					= @KYORI_19              " & vbNewLine _
    '                                   & "       ,KYORI_20					= @KYORI_20              " & vbNewLine _
    '                                   & "       ,KYORI_21					= @KYORI_21              " & vbNewLine _
    '                                   & "       ,KYORI_22					= @KYORI_22              " & vbNewLine _
    '                                   & "       ,KYORI_23					= @KYORI_23              " & vbNewLine _
    '                                   & "       ,KYORI_24					= @KYORI_24              " & vbNewLine _
    '                                   & "       ,KYORI_25					= @KYORI_25              " & vbNewLine _
    '                                   & "       ,KYORI_26					= @KYORI_26              " & vbNewLine _
    '                                   & "       ,KYORI_27					= @KYORI_27              " & vbNewLine _
    '                                   & "       ,KYORI_28					= @KYORI_28              " & vbNewLine _
    '                                   & "       ,KYORI_29					= @KYORI_29              " & vbNewLine _
    '                                   & "       ,KYORI_30					= @KYORI_30              " & vbNewLine _
    '                                   & "       ,KYORI_31					= @KYORI_31              " & vbNewLine _
    '                                   & "       ,KYORI_32					= @KYORI_32              " & vbNewLine _
    '                                   & "       ,KYORI_33					= @KYORI_33              " & vbNewLine _
    '                                   & "       ,KYORI_34					= @KYORI_34              " & vbNewLine _
    '                                   & "       ,KYORI_35					= @KYORI_35              " & vbNewLine _
    '                                   & "       ,KYORI_36					= @KYORI_36              " & vbNewLine _
    '                                   & "       ,KYORI_37					= @KYORI_37              " & vbNewLine _
    '                                   & "       ,KYORI_38					= @KYORI_38              " & vbNewLine _
    '                                   & "       ,KYORI_39					= @KYORI_39              " & vbNewLine _
    '                                   & "       ,KYORI_40					= @KYORI_40              " & vbNewLine _
    '                                   & "       ,KYORI_41					= @KYORI_41              " & vbNewLine _
    '                                   & "       ,KYORI_42					= @KYORI_42              " & vbNewLine _
    '                                   & "       ,KYORI_43					= @KYORI_43              " & vbNewLine _
    '                                   & "       ,KYORI_44					= @KYORI_44              " & vbNewLine _
    '                                   & "       ,KYORI_45					= @KYORI_45              " & vbNewLine _
    '                                   & "       ,KYORI_46					= @KYORI_46              " & vbNewLine _
    '                                   & "       ,KYORI_47					= @KYORI_47              " & vbNewLine _
    '                                   & "       ,KYORI_48					= @KYORI_48              " & vbNewLine _
    '                                   & "       ,KYORI_49					= @KYORI_49              " & vbNewLine _
    '                                   & "       ,KYORI_50					= @KYORI_50              " & vbNewLine _
    '                                   & "       ,KYORI_51					= @KYORI_51              " & vbNewLine _
    '                                   & "       ,KYORI_52					= @KYORI_52              " & vbNewLine _
    '                                   & "       ,KYORI_53					= @KYORI_53              " & vbNewLine _
    '                                   & "       ,KYORI_54					= @KYORI_54              " & vbNewLine _
    '                                   & "       ,KYORI_55					= @KYORI_55              " & vbNewLine _
    '                                   & "       ,KYORI_56					= @KYORI_56              " & vbNewLine _
    '                                   & "       ,KYORI_57					= @KYORI_57              " & vbNewLine _
    '                                   & "       ,KYORI_58					= @KYORI_58              " & vbNewLine _
    '                                   & "       ,KYORI_59					= @KYORI_59              " & vbNewLine _
    '                                   & "       ,KYORI_60					= @KYORI_60              " & vbNewLine _
    '                                   & "       ,KYORI_61					= @KYORI_61              " & vbNewLine _
    '                                   & "       ,KYORI_62					= @KYORI_62              " & vbNewLine _
    '                                   & "       ,KYORI_63					= @KYORI_63              " & vbNewLine _
    '                                   & "       ,KYORI_64					= @KYORI_64              " & vbNewLine _
    '                                   & "       ,KYORI_65					= @KYORI_65              " & vbNewLine _
    '                                   & "       ,KYORI_66					= @KYORI_66              " & vbNewLine _
    '                                   & "       ,KYORI_67					= @KYORI_67              " & vbNewLine _
    '                                   & "       ,KYORI_68					= @KYORI_68              " & vbNewLine _
    '                                   & "       ,KYORI_69					= @KYORI_69              " & vbNewLine _
    '                                   & "       ,KYORI_70					= @KYORI_70              " & vbNewLine _
    '                                   & "       ,CITY_EXTC_A				= @CITY_EXTC_A           " & vbNewLine _
    '                                   & "       ,CITY_EXTC_B				= @CITY_EXTC_B           " & vbNewLine _
    '                                   & "       ,WINT_EXTC_A				= @WINT_EXTC_A           " & vbNewLine _
    '                                   & "       ,WINT_EXTC_B				= @WINT_EXTC_B           " & vbNewLine _
    '                                   & "       ,RELY_EXTC					= @RELY_EXTC             " & vbNewLine _
    '                                   & "       ,INSU				        = @INSU                  " & vbNewLine _
    '                                   & "       ,FRRY_EXTC_10KG			= @FRRY_EXTC_10KG        " & vbNewLine _
    '                                   & "       ,FRRY_EXTC_PART			= @FRRY_EXTC_PART        " & vbNewLine _
    '                                   & "       ,UNCHIN_TARIFF_CD2			= @UNCHIN_TARIFF_CD2     " & vbNewLine _
    '                                   & "       ,SYS_UPD_DATE              = @SYS_UPD_DATE          " & vbNewLine _
    '                                   & "       ,SYS_UPD_TIME              = @SYS_UPD_TIME          " & vbNewLine _
    '                                   & "       ,SYS_UPD_PGID              = @SYS_UPD_PGID          " & vbNewLine _
    '                                   & "       ,SYS_UPD_USER              = @SYS_UPD_USER          " & vbNewLine _
    '                                   & " WHERE                                                     " & vbNewLine _
    '                                   & "         UNCHIN_TARIFF_CD         = @UNCHIN_TARIFF_CD      " & vbNewLine _
    '                                   & "   AND   UNCHIN_TARIFF_CD_EDA     = @UNCHIN_TARIFF_CD_EDA  " & vbNewLine _
    '                                   & "   AND   STR_DATE                 = @STR_DATE              " & vbNewLine
    ''' <summary>
    ''' 更新SQL（M_UNCHIN_TARIFF）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_UNCHIN_TARIFF SET                       " & vbNewLine _
                                       & "        NRS_BR_CD					= @NRS_BR_CD             " & vbNewLine _
                                       & "       ,UNCHIN_TARIFF_CD			= @UNCHIN_TARIFF_CD      " & vbNewLine _
                                       & "       ,UNCHIN_TARIFF_CD_EDA		= @UNCHIN_TARIFF_CD_EDA  " & vbNewLine _
                                       & "       ,STR_DATE					= @STR_DATE              " & vbNewLine _
                                       & "       ,UNCHIN_TARIFF_REM			= @UNCHIN_TARIFF_REM     " & vbNewLine _
                                       & "       ,DATA_TP					= @DATA_TP               " & vbNewLine _
                                       & "       ,TABLE_TP					= @TABLE_TP              " & vbNewLine _
                                       & "       ,CAR_TP					= @CAR_TP                " & vbNewLine _
                                       & "       ,WT_LV				        = @WT_LV                 " & vbNewLine _
                                       & "       ,KYORI_1					= @KYORI_1               " & vbNewLine _
                                       & "       ,KYORI_2					= @KYORI_2               " & vbNewLine _
                                       & "       ,KYORI_3					= @KYORI_3               " & vbNewLine _
                                       & "       ,KYORI_4					= @KYORI_4               " & vbNewLine _
                                       & "       ,KYORI_5					= @KYORI_5               " & vbNewLine _
                                       & "       ,KYORI_6					= @KYORI_6               " & vbNewLine _
                                       & "       ,KYORI_7					= @KYORI_7               " & vbNewLine _
                                       & "       ,KYORI_8					= @KYORI_8               " & vbNewLine _
                                       & "       ,KYORI_9					= @KYORI_9               " & vbNewLine _
                                       & "       ,KYORI_10					= @KYORI_10              " & vbNewLine _
                                       & "       ,KYORI_11					= @KYORI_11              " & vbNewLine _
                                       & "       ,KYORI_12					= @KYORI_12              " & vbNewLine _
                                       & "       ,KYORI_13					= @KYORI_13              " & vbNewLine _
                                       & "       ,KYORI_14					= @KYORI_14              " & vbNewLine _
                                       & "       ,KYORI_15					= @KYORI_15              " & vbNewLine _
                                       & "       ,KYORI_16					= @KYORI_16              " & vbNewLine _
                                       & "       ,KYORI_17					= @KYORI_17              " & vbNewLine _
                                       & "       ,KYORI_18					= @KYORI_18              " & vbNewLine _
                                       & "       ,KYORI_19					= @KYORI_19              " & vbNewLine _
                                       & "       ,KYORI_20					= @KYORI_20              " & vbNewLine _
                                       & "       ,KYORI_21					= @KYORI_21              " & vbNewLine _
                                       & "       ,KYORI_22					= @KYORI_22              " & vbNewLine _
                                       & "       ,KYORI_23					= @KYORI_23              " & vbNewLine _
                                       & "       ,KYORI_24					= @KYORI_24              " & vbNewLine _
                                       & "       ,KYORI_25					= @KYORI_25              " & vbNewLine _
                                       & "       ,KYORI_26					= @KYORI_26              " & vbNewLine _
                                       & "       ,KYORI_27					= @KYORI_27              " & vbNewLine _
                                       & "       ,KYORI_28					= @KYORI_28              " & vbNewLine _
                                       & "       ,KYORI_29					= @KYORI_29              " & vbNewLine _
                                       & "       ,KYORI_30					= @KYORI_30              " & vbNewLine _
                                       & "       ,KYORI_31					= @KYORI_31              " & vbNewLine _
                                       & "       ,KYORI_32					= @KYORI_32              " & vbNewLine _
                                       & "       ,KYORI_33					= @KYORI_33              " & vbNewLine _
                                       & "       ,KYORI_34					= @KYORI_34              " & vbNewLine _
                                       & "       ,KYORI_35					= @KYORI_35              " & vbNewLine _
                                       & "       ,KYORI_36					= @KYORI_36              " & vbNewLine _
                                       & "       ,KYORI_37					= @KYORI_37              " & vbNewLine _
                                       & "       ,KYORI_38					= @KYORI_38              " & vbNewLine _
                                       & "       ,KYORI_39					= @KYORI_39              " & vbNewLine _
                                       & "       ,KYORI_40					= @KYORI_40              " & vbNewLine _
                                       & "       ,KYORI_41					= @KYORI_41              " & vbNewLine _
                                       & "       ,KYORI_42					= @KYORI_42              " & vbNewLine _
                                       & "       ,KYORI_43					= @KYORI_43              " & vbNewLine _
                                       & "       ,KYORI_44					= @KYORI_44              " & vbNewLine _
                                       & "       ,KYORI_45					= @KYORI_45              " & vbNewLine _
                                       & "       ,KYORI_46					= @KYORI_46              " & vbNewLine _
                                       & "       ,KYORI_47					= @KYORI_47              " & vbNewLine _
                                       & "       ,KYORI_48					= @KYORI_48              " & vbNewLine _
                                       & "       ,KYORI_49					= @KYORI_49              " & vbNewLine _
                                       & "       ,KYORI_50					= @KYORI_50              " & vbNewLine _
                                       & "       ,KYORI_51					= @KYORI_51              " & vbNewLine _
                                       & "       ,KYORI_52					= @KYORI_52              " & vbNewLine _
                                       & "       ,KYORI_53					= @KYORI_53              " & vbNewLine _
                                       & "       ,KYORI_54					= @KYORI_54              " & vbNewLine _
                                       & "       ,KYORI_55					= @KYORI_55              " & vbNewLine _
                                       & "       ,KYORI_56					= @KYORI_56              " & vbNewLine _
                                       & "       ,KYORI_57					= @KYORI_57              " & vbNewLine _
                                       & "       ,KYORI_58					= @KYORI_58              " & vbNewLine _
                                       & "       ,KYORI_59					= @KYORI_59              " & vbNewLine _
                                       & "       ,KYORI_60					= @KYORI_60              " & vbNewLine _
                                       & "       ,KYORI_61					= @KYORI_61              " & vbNewLine _
                                       & "       ,KYORI_62					= @KYORI_62              " & vbNewLine _
                                       & "       ,KYORI_63					= @KYORI_63              " & vbNewLine _
                                       & "       ,KYORI_64					= @KYORI_64              " & vbNewLine _
                                       & "       ,KYORI_65					= @KYORI_65              " & vbNewLine _
                                       & "       ,KYORI_66					= @KYORI_66              " & vbNewLine _
                                       & "       ,KYORI_67					= @KYORI_67              " & vbNewLine _
                                       & "       ,KYORI_68					= @KYORI_68              " & vbNewLine _
                                       & "       ,KYORI_69					= @KYORI_69              " & vbNewLine _
                                       & "       ,KYORI_70					= @KYORI_70              " & vbNewLine _
                                       & "       ,CITY_EXTC_A				= @CITY_EXTC_A           " & vbNewLine _
                                       & "       ,CITY_EXTC_B				= @CITY_EXTC_B           " & vbNewLine _
                                       & "       ,WINT_EXTC_A				= @WINT_EXTC_A           " & vbNewLine _
                                       & "       ,WINT_EXTC_B				= @WINT_EXTC_B           " & vbNewLine _
                                       & "       ,RELY_EXTC					= @RELY_EXTC             " & vbNewLine _
                                       & "       ,INSU				        = @INSU                  " & vbNewLine _
                                       & "       ,FRRY_EXTC_PART			= @FRRY_EXTC_PART        " & vbNewLine _
                                       & "       ,UNCHIN_TARIFF_CD2			= @UNCHIN_TARIFF_CD2     " & vbNewLine _
                                       & "       ,APPROVAL_CD		    	= @APPROVAL_CD           " & vbNewLine _
                                       & "       ,APPROVAL_USER			    = @APPROVAL_USER         " & vbNewLine _
                                       & "       ,APPROVAL_DATE			    = @APPROVAL_DATE         " & vbNewLine _
                                       & "       ,APPROVAL_TIME			    = @APPROVAL_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_DATE              = @SYS_UPD_DATE          " & vbNewLine _
                                       & "       ,SYS_UPD_TIME              = @SYS_UPD_TIME          " & vbNewLine _
                                       & "       ,SYS_UPD_PGID              = @SYS_UPD_PGID          " & vbNewLine _
                                       & "       ,SYS_UPD_USER              = @SYS_UPD_USER          " & vbNewLine _
                                       & " WHERE                                                     " & vbNewLine _
                                       & "         UNCHIN_TARIFF_CD         = @UNCHIN_TARIFF_CD      " & vbNewLine _
                                       & "   AND   UNCHIN_TARIFF_CD_EDA     = @UNCHIN_TARIFF_CD_EDA  " & vbNewLine _
                                       & "   AND   STR_DATE                 = @STR_DATE              " & vbNewLine
    'END YANAI 要望番号377

#End Region

#Region "UPDATE_DEL_FLG"

    ''' <summary>
    ''' 削除・復活SQL（M_UNCHIN_TARIFF）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_UNCHIN_TARIFF SET                  " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "        UNCHIN_TARIFF_CD      = @UNCHIN_TARIFF_CD     " & vbNewLine _
                                       & "   AND  STR_DATE              = @STR_DATE             " & vbNewLine


#End Region

#Region "承認処理 SQL"

    ''' <summary>
    ''' 運賃タリフマスタ更新SQL(承認)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_APPROVAL As String _
            = "UPDATE                                            " & vbNewLine _
            & "    $LM_MST$..M_UNCHIN_TARIFF                     " & vbNewLine _
            & "SET                                               " & vbNewLine _
            & "      APPROVAL_CD         =    @APPROVAL_CD       " & vbNewLine _
            & "     ,APPROVAL_USER       =    @APPROVAL_USER     " & vbNewLine _
            & "     ,APPROVAL_DATE       =    @APPROVAL_DATE     " & vbNewLine _
            & "     ,APPROVAL_TIME       =    @APPROVAL_TIME     " & vbNewLine _
            & "     ,SYS_UPD_DATE        =    @SYS_UPD_DATE      " & vbNewLine _
            & "     ,SYS_UPD_TIME        =    @SYS_UPD_TIME      " & vbNewLine _
            & "     ,SYS_UPD_PGID        =    @SYS_UPD_PGID      " & vbNewLine _
            & "     ,SYS_UPD_USER        =    @SYS_UPD_USER      " & vbNewLine _
            & "WHERE                                             " & vbNewLine _
            & "      UNCHIN_TARIFF_CD    =    @UNCHIN_TARIFF_CD  " & vbNewLine _
            & "AND   STR_DATE            =    @STR_DATE          " & vbNewLine _
            & "AND   SYS_UPD_DATE        =    @HAITA_DATE        " & vbNewLine _
            & "AND   SYS_UPD_TIME        =    @HAITA_TIME        " & vbNewLine

#End Region

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運賃タリフマスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM060DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM060DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)        
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM060DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM060DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM060DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("DATA_TP", "DATA_TP")
        map.Add("DATA_TP_NM", "DATA_TP_NM")
        map.Add("TABLE_TP", "TABLE_TP")
        map.Add("TABLE_TP_NM", "TABLE_TP_NM")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("APPROVAL_CD", "APPROVAL_CD")
        map.Add("APPROVAL_NM", "APPROVAL_NM")
        map.Add("APPROVAL_USER", "APPROVAL_USER")
        map.Add("APPROVAL_DATE", "APPROVAL_DATE")
        map.Add("APPROVAL_TIME", "APPROVAL_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM060OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール_Spread上部検索用(M_UNCHIN_TARIFF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (UNCHIN_TARIFF.SYS_DEL_FLG = @SYS_DEL_FLG  OR UNCHIN_TARIFF.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (UNCHIN_TARIFF.NRS_BR_CD = @NRS_BR_CD OR UNCHIN_TARIFF.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNCHIN_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF.UNCHIN_TARIFF_CD LIKE @UNCHIN_TARIFF_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DATA_TP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF.DATA_TP = @DATA_TP")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_TP", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TABLE_TP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF.TABLE_TP = @TABLE_TP")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TABLE_TP", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("APPROVAL_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF.APPROVAL_CD = @APPROVAL_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_CD", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 運賃タリフマスタ(距離刻み/運賃)更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM060DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM060DAC.SQL_FROM_DATA2)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定
        Me._StrSql.Append(LMM060DAC.SQL_ORDER_BY2)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("UNCHIN_TARIFF_CD_EDA", "UNCHIN_TARIFF_CD_EDA")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("DATA_TP", "DATA_TP")
        map.Add("TABLE_TP", "TABLE_TP")
        map.Add("CAR_TP", "CAR_TP")
        map.Add("CAR_TP_S_NM", "CAR_TP_S_NM")
        map.Add("CAR_TP_T_NM", "CAR_TP_T_NM")
        map.Add("WT_LV", "WT_LV")
        map.Add("KYORI_1", "KYORI_1")
        map.Add("KYORI_2", "KYORI_2")
        map.Add("KYORI_3", "KYORI_3")
        map.Add("KYORI_4", "KYORI_4")
        map.Add("KYORI_5", "KYORI_5")
        map.Add("KYORI_6", "KYORI_6")
        map.Add("KYORI_7", "KYORI_7")
        map.Add("KYORI_8", "KYORI_8")
        map.Add("KYORI_9", "KYORI_9")
        map.Add("KYORI_10", "KYORI_10")
        map.Add("KYORI_11", "KYORI_11")
        map.Add("KYORI_12", "KYORI_12")
        map.Add("KYORI_13", "KYORI_13")
        map.Add("KYORI_14", "KYORI_14")
        map.Add("KYORI_15", "KYORI_15")
        map.Add("KYORI_16", "KYORI_16")
        map.Add("KYORI_17", "KYORI_17")
        map.Add("KYORI_18", "KYORI_18")
        map.Add("KYORI_19", "KYORI_19")
        map.Add("KYORI_20", "KYORI_20")
        map.Add("KYORI_21", "KYORI_21")
        map.Add("KYORI_22", "KYORI_22")
        map.Add("KYORI_23", "KYORI_23")
        map.Add("KYORI_24", "KYORI_24")
        map.Add("KYORI_25", "KYORI_25")
        map.Add("KYORI_26", "KYORI_26")
        map.Add("KYORI_27", "KYORI_27")
        map.Add("KYORI_28", "KYORI_28")
        map.Add("KYORI_29", "KYORI_29")
        map.Add("KYORI_30", "KYORI_30")
        map.Add("KYORI_31", "KYORI_31")
        map.Add("KYORI_32", "KYORI_32")
        map.Add("KYORI_33", "KYORI_33")
        map.Add("KYORI_34", "KYORI_34")
        map.Add("KYORI_35", "KYORI_35")
        map.Add("KYORI_36", "KYORI_36")
        map.Add("KYORI_37", "KYORI_37")
        map.Add("KYORI_38", "KYORI_38")
        map.Add("KYORI_39", "KYORI_39")
        map.Add("KYORI_40", "KYORI_40")
        map.Add("KYORI_41", "KYORI_41")
        map.Add("KYORI_42", "KYORI_42")
        map.Add("KYORI_43", "KYORI_43")
        map.Add("KYORI_44", "KYORI_44")
        map.Add("KYORI_45", "KYORI_45")
        map.Add("KYORI_46", "KYORI_46")
        map.Add("KYORI_47", "KYORI_47")
        map.Add("KYORI_48", "KYORI_48")
        map.Add("KYORI_49", "KYORI_49")
        map.Add("KYORI_50", "KYORI_50")
        map.Add("KYORI_51", "KYORI_51")
        map.Add("KYORI_52", "KYORI_52")
        map.Add("KYORI_53", "KYORI_53")
        map.Add("KYORI_54", "KYORI_54")
        map.Add("KYORI_55", "KYORI_55")
        map.Add("KYORI_56", "KYORI_56")
        map.Add("KYORI_57", "KYORI_57")
        map.Add("KYORI_58", "KYORI_58")
        map.Add("KYORI_59", "KYORI_59")
        map.Add("KYORI_60", "KYORI_60")
        map.Add("KYORI_61", "KYORI_61")
        map.Add("KYORI_62", "KYORI_62")
        map.Add("KYORI_63", "KYORI_63")
        map.Add("KYORI_64", "KYORI_64")
        map.Add("KYORI_65", "KYORI_65")
        map.Add("KYORI_66", "KYORI_66")
        map.Add("KYORI_67", "KYORI_67")
        map.Add("KYORI_68", "KYORI_68")
        map.Add("KYORI_69", "KYORI_69")
        map.Add("KYORI_70", "KYORI_70")
        map.Add("CITY_EXTC_A", "CITY_EXTC_A")
        map.Add("CITY_EXTC_B", "CITY_EXTC_B")
        map.Add("WINT_EXTC_A", "WINT_EXTC_A")
        map.Add("WINT_EXTC_B", "WINT_EXTC_B")
        map.Add("RELY_EXTC", "RELY_EXTC")
        map.Add("INSU", "INSU")
        'START YANAI 要望番号377
        'map.Add("FRRY_EXTC_10KG", "FRRY_EXTC_10KG")
        'END YANAI 要望番号377
        map.Add("FRRY_EXTC_PART", "FRRY_EXTC_PART")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")
        map.Add("UNCHIN_TARIFF_REM2", "UNCHIN_TARIFF_REM2")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("UPD_FLG", "UPD_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM060_KYORI")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール_Spread下部検索用(M_UNCHIN_TARIFF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF_DTL.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNCHIN_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD LIKE @UNCHIN_TARIFF_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' MAX運賃タリフコード枝番取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>MAX運賃タリフコード枝番取得SQLの構築・発行</remarks>
    Private Function SelectMaxUnchinCdEdaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM060DAC.SQL_SELECT_MAX)        'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM060DAC.SQL_FROM_MAX)          'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL3()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "SelectMaxUnchinCdEdaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UNCHIN_TARIFF_MAXEDA", "UNCHIN_TARIFF_MAXEDA")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM060_UNCHIN_TARRIF_MAXEDA")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール_MAX運賃タリフコード枝番取得用(M_UNCHIN_TARIFF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL3()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNCHIN_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF.UNCHIN_TARIFF_CD LIKE @UNCHIN_TARIFF_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("STR_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF.STR_DATE LIKE @STR_DATE")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 運賃タリフマスタExcel出力データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタExcel出力データ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListExcelData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM060DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM060DAC.SQL_FROM_DATA2)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterExcelSQL()               '条件設定
        Me._StrSql.Append(LMM060DAC.SQL_ORDER_BY2)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "SelectListExcelData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("UNCHIN_TARIFF_CD_EDA", "UNCHIN_TARIFF_CD_EDA")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("DATA_TP", "DATA_TP")
        map.Add("TABLE_TP", "TABLE_TP")
        map.Add("CAR_TP", "CAR_TP")
        map.Add("CAR_TP_S_NM", "CAR_TP_S_NM")
        map.Add("CAR_TP_T_NM", "CAR_TP_T_NM")
        map.Add("WT_LV", "WT_LV")
        map.Add("KYORI_1", "KYORI_1")
        map.Add("KYORI_2", "KYORI_2")
        map.Add("KYORI_3", "KYORI_3")
        map.Add("KYORI_4", "KYORI_4")
        map.Add("KYORI_5", "KYORI_5")
        map.Add("KYORI_6", "KYORI_6")
        map.Add("KYORI_7", "KYORI_7")
        map.Add("KYORI_8", "KYORI_8")
        map.Add("KYORI_9", "KYORI_9")
        map.Add("KYORI_10", "KYORI_10")
        map.Add("KYORI_11", "KYORI_11")
        map.Add("KYORI_12", "KYORI_12")
        map.Add("KYORI_13", "KYORI_13")
        map.Add("KYORI_14", "KYORI_14")
        map.Add("KYORI_15", "KYORI_15")
        map.Add("KYORI_16", "KYORI_16")
        map.Add("KYORI_17", "KYORI_17")
        map.Add("KYORI_18", "KYORI_18")
        map.Add("KYORI_19", "KYORI_19")
        map.Add("KYORI_20", "KYORI_20")
        map.Add("KYORI_21", "KYORI_21")
        map.Add("KYORI_22", "KYORI_22")
        map.Add("KYORI_23", "KYORI_23")
        map.Add("KYORI_24", "KYORI_24")
        map.Add("KYORI_25", "KYORI_25")
        map.Add("KYORI_26", "KYORI_26")
        map.Add("KYORI_27", "KYORI_27")
        map.Add("KYORI_28", "KYORI_28")
        map.Add("KYORI_29", "KYORI_29")
        map.Add("KYORI_30", "KYORI_30")
        map.Add("KYORI_31", "KYORI_31")
        map.Add("KYORI_32", "KYORI_32")
        map.Add("KYORI_33", "KYORI_33")
        map.Add("KYORI_34", "KYORI_34")
        map.Add("KYORI_35", "KYORI_35")
        map.Add("KYORI_36", "KYORI_36")
        map.Add("KYORI_37", "KYORI_37")
        map.Add("KYORI_38", "KYORI_38")
        map.Add("KYORI_39", "KYORI_39")
        map.Add("KYORI_40", "KYORI_40")
        map.Add("KYORI_41", "KYORI_41")
        map.Add("KYORI_42", "KYORI_42")
        map.Add("KYORI_43", "KYORI_43")
        map.Add("KYORI_44", "KYORI_44")
        map.Add("KYORI_45", "KYORI_45")
        map.Add("KYORI_46", "KYORI_46")
        map.Add("KYORI_47", "KYORI_47")
        map.Add("KYORI_48", "KYORI_48")
        map.Add("KYORI_49", "KYORI_49")
        map.Add("KYORI_50", "KYORI_50")
        map.Add("KYORI_51", "KYORI_51")
        map.Add("KYORI_52", "KYORI_52")
        map.Add("KYORI_53", "KYORI_53")
        map.Add("KYORI_54", "KYORI_54")
        map.Add("KYORI_55", "KYORI_55")
        map.Add("KYORI_56", "KYORI_56")
        map.Add("KYORI_57", "KYORI_57")
        map.Add("KYORI_58", "KYORI_58")
        map.Add("KYORI_59", "KYORI_59")
        map.Add("KYORI_60", "KYORI_60")
        map.Add("KYORI_61", "KYORI_61")
        map.Add("KYORI_62", "KYORI_62")
        map.Add("KYORI_63", "KYORI_63")
        map.Add("KYORI_64", "KYORI_64")
        map.Add("KYORI_65", "KYORI_65")
        map.Add("KYORI_66", "KYORI_66")
        map.Add("KYORI_67", "KYORI_67")
        map.Add("KYORI_68", "KYORI_68")
        map.Add("KYORI_69", "KYORI_69")
        map.Add("KYORI_70", "KYORI_70")
        map.Add("CITY_EXTC_A", "CITY_EXTC_A")
        map.Add("CITY_EXTC_B", "CITY_EXTC_B")
        map.Add("WINT_EXTC_A", "WINT_EXTC_A")
        map.Add("WINT_EXTC_B", "WINT_EXTC_B")
        map.Add("RELY_EXTC", "RELY_EXTC")
        map.Add("INSU", "INSU")
        'START YANAI 要望番号377
        'map.Add("FRRY_EXTC_10KG", "FRRY_EXTC_10KG")
        'END YANAI 要望番号377
        map.Add("FRRY_EXTC_PART", "FRRY_EXTC_PART")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")
        map.Add("UNCHIN_TARIFF_REM2", "UNCHIN_TARIFF_REM2")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("UPD_FLG", "UPD_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM060_KYORI")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール_Excel出力データ検索用(M_UNCHIN_TARIFF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterExcelSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF_DTL.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNCHIN_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF_DTL.UNCHIN_TARIFF_CD LIKE @UNCHIN_TARIFF_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("STR_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF_DTL.STR_DATE = @STR_DATE")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF_DTL.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 運賃タリフマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectUnchinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM060DAC.SQL_EXIT_UNCHIN)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "SelectUnchinM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistUnchinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060_KYORI")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM060DAC.SQL_EXIT_UNCHIN, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "CheckExistUnchinM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#Region "Insert"

    ''' <summary>
    ''' 運賃タリフマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertUnchinTariffM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060_KYORI")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        Dim max2 As Integer = inTbl.Rows.Count - 1

        '2011.08.23 検証結果一覧№32対応 START
        For i As Integer = 0 To max2
            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            '【更新区分の判定】INTableの更新区分によって新規登録ならば以下の処理を行う
            If inTbl.Rows(i).Item("UPD_FLG").ToString = LMConst.FLG.OFF Then

                '①-a) マスタ重複チェックを行う(ﾚｺｰﾄﾞｽﾃｰﾀｽ="新規"の場合)
                'SQL文のコンパイル
                Dim cmd2 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM060DAC.SQL_EXIT_UNCHIN_DOUBLE, Me._Row.Item("USER_BR_CD").ToString()))

                Dim reader As SqlDataReader = Nothing

                'SQLパラメータ初期化/設定
                Call Me.SetParamExistDoubleChk()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd2.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM060DAC", "CheckDoubleUnchinM", cmd2)

                'SQLの発行
                reader = MyBase.GetSelectResult(cmd2)

                cmd2.Parameters.Clear()

                '処理件数の設定
                reader.Read()
                MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
                reader.Close()

                '処理件数による判定
                If MyBase.GetResultCount() > 0 Then
                    MyBase.SetMessage("E010")
                    If max2 > 0 Then
                        '明細行(重量・車種・個数・数量・宅急便サイズ)のいずれかが重複した場合は以下のメッセージを表示
                        If inTbl.Rows(i - 1).Item("UNCHIN_TARIFF_CD").ToString = inTbl.Rows(i).Item("UNCHIN_TARIFF_CD").ToString _
                        AndAlso inTbl.Rows(i - 1).Item("STR_DATE").ToString = inTbl.Rows(i).Item("STR_DATE").ToString _
                        AndAlso inTbl.Rows(i - 1).Item("DATA_TP").ToString = inTbl.Rows(i).Item("DATA_TP").ToString _
                        AndAlso inTbl.Rows(i - 1).Item("TABLE_TP").ToString = inTbl.Rows(i).Item("TABLE_TP").ToString Then
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "00" _
                            OrElse inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "03" _
                            OrElse inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "07" Then
                                MyBase.SetMessage("E022", New String() {"重量"})
                            End If
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "01" Then
                                MyBase.SetMessage("E022", New String() {"車種"})
                            End If
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "02" _
                            OrElse inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "05" Then
                                MyBase.SetMessage("E022", New String() {"個数"})
                            End If
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "04" Then
                                MyBase.SetMessage("E022", New String() {"数量"})
                            End If
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "06" Then
                                MyBase.SetMessage("E022", New String() {"宅急便ｻｲｽﾞ"})
                            End If
                        End If
                    End If

                    Exit For
                    '2011.08.23 検証結果一覧№32対応 END
                Else
                    'SQL文のコンパイル
                    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM060DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

                    'SQLパラメータ初期化/設定
                    Me._SqlPrmList = New ArrayList()

                    'INTableの条件rowの格納
                    Me._Row = inTbl.Rows(i)

                    'パラメータ設定
                    Call Me.SetDataInsertParameter(Me._SqlPrmList)
                    Call Me.SetUnchinParam(Me._Row, Me._SqlPrmList)

                    'パラメータの反映
                    For Each obj As Object In Me._SqlPrmList
                        cmd.Parameters.Add(obj)
                    Next

                    MyBase.Logger.WriteSQLLog("LMM060DAC", "InsertUnchinTariffM", cmd)

                    'SQLの発行
                    MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

                    'パラメータの初期化
                    cmd.Parameters.Clear()

                End If

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    '''運賃タリフマスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ新規登録SQLの構築・発行</remarks>
    Private Function DelUnchinTariffM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM060DAC.SQL_DEL_UNCHIN_TARIFF, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete(True)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "DelUnchinTariffM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

#End Region

#Region "Update"

    ''' <summary>
    ''' 運賃タリフマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateUnchinTariffM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060_KYORI")

        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            '【更新区分の判定】INTableの更新区分によって新規登録か更新処理を行う。
            If inTbl.Rows(i).Item("UPD_FLG").ToString = LMConst.FLG.OFF Then

                '① 更新区分＝'0'(新規)の場合
                '①-a) マスタ重複チェックを行う(ﾚｺｰﾄﾞｽﾃｰﾀｽ="正常"の場合)
                'SQL文のコンパイル
                Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM060DAC.SQL_EXIT_UNCHIN_DOUBLE, Me._Row.Item("USER_BR_CD").ToString()))

                Dim reader As SqlDataReader = Nothing

                'SQLパラメータ初期化/設定
                Call Me.SetParamExistDoubleChk()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM060DAC", "CheckDoubleUnchinM", cmd)

                'SQLの発行
                reader = MyBase.GetSelectResult(cmd)

                cmd.Parameters.Clear()

                '処理件数の設定
                reader.Read()
                MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
                reader.Close()

                '処理件数による判定
                '1件以上の場合、マスタ存在メッセージを設定する
                If MyBase.GetResultCount() > 0 Then
                    MyBase.SetMessage("E010")

                    If max > 0 Then
                        '明細行(重量・車種・個数・数量・宅急便サイズ)のいずれかが重複した場合は以下のメッセージを表示
                        If inTbl.Rows(i - 1).Item("UNCHIN_TARIFF_CD").ToString = inTbl.Rows(i).Item("UNCHIN_TARIFF_CD").ToString _
                        AndAlso inTbl.Rows(i - 1).Item("STR_DATE").ToString = inTbl.Rows(i).Item("STR_DATE").ToString _
                        AndAlso inTbl.Rows(i - 1).Item("DATA_TP").ToString = inTbl.Rows(i).Item("DATA_TP").ToString _
                        AndAlso inTbl.Rows(i - 1).Item("TABLE_TP").ToString = inTbl.Rows(i).Item("TABLE_TP").ToString Then
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "00" _
                            OrElse inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "03" _
                            OrElse inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "07" Then
                                MyBase.SetMessage("E022", New String() {"重量"})
                            End If
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "01" Then
                                MyBase.SetMessage("E022", New String() {"車種"})
                            End If
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "02" _
                            OrElse inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "05" Then
                                MyBase.SetMessage("E022", New String() {"個数"})
                            End If
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "04" Then
                                MyBase.SetMessage("E022", New String() {"数量"})
                            End If
                            If inTbl.Rows(i - 1).Item("TABLE_TP").ToString = "06" Then
                                MyBase.SetMessage("E022", New String() {"宅急便ｻｲｽﾞ"})
                            End If
                        End If
                    End If

                    Exit For
                Else
                    '①-b) マスタ重複チェックでエラーでない場合、運賃タリフ情報の新規登録を行う
                    cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM060DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

                    'SQLパラメータ初期化/設定
                    Call Me.SetParamInsert()

                    'パラメータの反映
                    For Each obj As Object In Me._SqlPrmList
                        cmd.Parameters.Add(obj)
                    Next

                    MyBase.Logger.WriteSQLLog("LMM060DAC", "InsertUnchinTariffM", cmd)

                    'SQLの発行
                    'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

                    '更新結果の判定
                    If Me.GetInsertResult(cmd) > 0 Then
                        Continue For
                    Else
                        Exit For
                    End If

                End If

            Else
                '② 更新区分≠'0'(更新)の場合
                '【削除フラグの判定】INTableの削除フラグによって物理削除か更新処理を行う。
                If inTbl.Rows(i).Item("SYS_DEL_FLG").ToString = LMConst.FLG.ON Then

                    '②-a) 削除フラグ＝'1'の場合、運賃タリフ情報の物理削除を行う
                    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM060DAC.SQL_DEL_UNCHIN_TARIFF, Me._Row.Item("USER_BR_CD").ToString()))

                    'SQLパラメータ初期化/設定
                    Call Me.SetParamDelete(True)

                    'パラメータの反映
                    For Each obj As Object In Me._SqlPrmList
                        cmd.Parameters.Add(obj)
                    Next

                    MyBase.Logger.WriteSQLLog("LMM060DAC", "DelUnchinTariffM", cmd)

                    'SQLの発行
                    'MyBase.SetResultCount(MyBase.GetDeleteResult(cmd))

                    '更新結果の判定
                    If Me.GetDeleteResult(cmd) > 0 Then
                        Continue For
                    Else
                        Exit For
                    End If

                Else
                    '②-b) 削除フラグ≠'1'の場合、運賃タリフ情報の更新登録を行う
                    'SQL文のコンパイル
                    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM060DAC.SQL_UPDATE _
                                                                                                 , LMM060DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                                 , Me._Row.Item("USER_BR_CD").ToString()))
                    'SQLパラメータ初期化/設定
                    Call Me.SetParamUpdate()

                    'パラメータの反映
                    For Each obj As Object In Me._SqlPrmList
                        cmd.Parameters.Add(obj)
                    Next

                    MyBase.Logger.WriteSQLLog("LMM060DAC", "UpdateUnchinTariffM", cmd)

                    '更新時排他チェック
                    'Call Me.UpdateResultChk(cmd)

                    '更新結果の判定
                    If Me.UpdateResultChk(cmd) = True Then
                        Continue For
                    Else
                        Exit For
                    End If

                End If

            End If

        Next

        Return ds

    End Function

#End Region

#Region "Update_Delete"

    ''' <summary>
    ''' 運賃タリフマスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteUnchinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM060DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM060DAC", "DeleteUnchinM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

#End Region

#Region "承認処理"

    ''' <summary>
    ''' 運賃タリフマスタ更新（承認）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ更新SQLの構築・発行</remarks>
    Private Function ApprovalData(ByVal ds As DataSet) As DataSet

        Dim allCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM060IN")

        'データ数分繰り返し
        For i As Integer = 0 To inTbl.Rows.Count - 1

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL構築
            Me._StrSql.Append(LMM060DAC.SQL_UPD_APPROVAL)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータの設定
            Me._SqlPrmList = New ArrayList()
            With Me._Row
                .Item("APPROVAL_DATE") = If(String.IsNullOrEmpty(.Item("APPROVAL_DATE").ToString()), String.Empty, Me.GetSystemDate())
                .Item("APPROVAL_TIME") = If(String.IsNullOrEmpty(.Item("APPROVAL_TIME").ToString()), String.Empty, Left(Me.GetSystemTime(), 6))

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_CD", .Item("APPROVAL_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_USER", .Item("APPROVAL_USER").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_DATE", .Item("APPROVAL_DATE").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_TIME", .Item("APPROVAL_TIME").ToString(), DBDataType.CHAR))

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", .Item("STR_DATE").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
            End With

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM060DAC", "ApprovalData", cmd)

            'SQL実行
            Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

            'エラーメッセージの設定
            If cnt < 1 Then
                MyBase.SetMessage("E011")
                Exit For
            End If

            '全処理件数のカウントアップ
            allCnt += cnt

            cmd = New SqlCommand()

        Next

        '処理件数の設定
        MyBase.SetResultCount(allCnt)

        Return ds

    End Function

#End Region

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#Region "ユーティリティ"

    '''' <summary>
    '''' データ取得
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>データ取得SQLの構築・発行</remarks>
    'Private Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal ptn As LMM060DAC.SelectCondition, ByVal str As String()) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMM060IN")

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQL格納変数の初期化
    '    Me._StrSql = New StringBuilder()

    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    'パラメータ設定
    '    Call Me.SetConditionMasterSQL()

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("USER_BR_CD").ToString()))

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMM060DAC", "SelectListData", cmd)

    '    'SQLの発行
    '    Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '    'DataReader→DataTableへの転記
    '    Dim map As Hashtable = New Hashtable()

    '    '取得データの格納先をマッピング
    '    Dim max As Integer = str.Length - 1
    '    For i As Integer = 0 To max
    '        map.Add(str(i), str(i))
    '    Next

    '    Return MyBase.SetSelectResultToDataSet(map, ds, reader, tblNm)

    'End Function

#End Region


#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフマスタ存在チェック_新規・複写)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", .Item("STR_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフマスタ重複チェック_編集)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistDoubleChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", .Item("STR_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_TP", .Item("DATA_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TABLE_TP", .Item("TABLE_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_TP", .Item("CAR_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WT_LV", .Item("WT_LV").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        Call Me.SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録_運賃タリフＭ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    '''' <summary>
    '''' パラメータ設定モジュール(新規登録_担当者別荷主Ｍ)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SetParamTcustInsert()

    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    '共通項目
    '    Call Me.SetTcustParam(Me._Row, Me._SqlPrmList)

    '    'システム項目
    '    Call Me.SetParamCommonSystemIns()

    'End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam(True)

        '更新項目
        Call Me.SetParamCommonSystemUpd(True)

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete(Optional ByVal DelFlg As Boolean = False)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel(DelFlg)

        '論理削除の場合のみ、更新日/時刻を設定
        If DelFlg = False Then
            Call Me.SetParamCommonSystemUpd()
        End If

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_運賃タリフＭ用_1件登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam(Optional ByVal UpdFlg As Boolean = False)

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD_EDA", .Item("UNCHIN_TARIFF_CD_EDA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", .Item("STR_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_REM", .Item("UNCHIN_TARIFF_REM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_TP", .Item("DATA_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TABLE_TP", .Item("TABLE_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_TP", .Item("CAR_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WT_LV", .Item("WT_LV").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_1", .Item("KYORI_1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_2", .Item("KYORI_2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_3", .Item("KYORI_3").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_4", .Item("KYORI_4").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_5", .Item("KYORI_5").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_6", .Item("KYORI_6").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_7", .Item("KYORI_7").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_8", .Item("KYORI_8").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_9", .Item("KYORI_9").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_10", .Item("KYORI_10").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_11", .Item("KYORI_11").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_12", .Item("KYORI_12").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_13", .Item("KYORI_13").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_14", .Item("KYORI_14").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_15", .Item("KYORI_15").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_16", .Item("KYORI_16").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_17", .Item("KYORI_17").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_18", .Item("KYORI_18").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_19", .Item("KYORI_19").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_20", .Item("KYORI_20").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_21", .Item("KYORI_21").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_22", .Item("KYORI_22").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_23", .Item("KYORI_23").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_24", .Item("KYORI_24").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_25", .Item("KYORI_25").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_26", .Item("KYORI_26").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_27", .Item("KYORI_27").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_28", .Item("KYORI_28").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_29", .Item("KYORI_29").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_30", .Item("KYORI_30").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_31", .Item("KYORI_31").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_32", .Item("KYORI_32").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_33", .Item("KYORI_33").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_34", .Item("KYORI_34").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_35", .Item("KYORI_35").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_36", .Item("KYORI_36").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_37", .Item("KYORI_37").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_38", .Item("KYORI_38").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_39", .Item("KYORI_39").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_40", .Item("KYORI_40").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_41", .Item("KYORI_41").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_42", .Item("KYORI_42").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_43", .Item("KYORI_43").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_44", .Item("KYORI_44").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_45", .Item("KYORI_45").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_46", .Item("KYORI_46").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_47", .Item("KYORI_47").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_48", .Item("KYORI_48").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_49", .Item("KYORI_49").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_50", .Item("KYORI_50").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_51", .Item("KYORI_51").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_52", .Item("KYORI_52").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_53", .Item("KYORI_53").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_54", .Item("KYORI_54").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_55", .Item("KYORI_55").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_56", .Item("KYORI_56").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_57", .Item("KYORI_57").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_58", .Item("KYORI_58").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_59", .Item("KYORI_59").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_60", .Item("KYORI_60").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_61", .Item("KYORI_61").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_62", .Item("KYORI_62").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_63", .Item("KYORI_63").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_64", .Item("KYORI_64").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_65", .Item("KYORI_65").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_66", .Item("KYORI_66").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_67", .Item("KYORI_67").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_68", .Item("KYORI_68").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_69", .Item("KYORI_69").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_70", .Item("KYORI_70").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_EXTC_A", .Item("CITY_EXTC_A").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_EXTC_B", .Item("CITY_EXTC_B").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WINT_EXTC_A", .Item("WINT_EXTC_A").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WINT_EXTC_B", .Item("WINT_EXTC_B").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RELY_EXTC", .Item("RELY_EXTC").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INSU", .Item("INSU").ToString(), DBDataType.NUMERIC))
            'START YANAI 要望番号377
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRRY_EXTC_10KG", .Item("FRRY_EXTC_10KG").ToString(), DBDataType.NUMERIC))
            'END YANAI 要望番号377
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRRY_EXTC_PART", .Item("FRRY_EXTC_PART").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD2", .Item("UNCHIN_TARIFF_CD2").ToString(), DBDataType.NVARCHAR))
            'If UpdFlg = True Then
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
            'End If
            '承認項目は未に戻す
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_CD", "00", DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_USER", String.Empty, DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_DATE", String.Empty, DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_TIME", String.Empty, DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_運賃タリフＭ用_複数件登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnchinParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD_EDA", .Item("UNCHIN_TARIFF_CD_EDA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STR_DATE", .Item("STR_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_REM", .Item("UNCHIN_TARIFF_REM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_TP", .Item("DATA_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TABLE_TP", .Item("TABLE_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CAR_TP", .Item("CAR_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WT_LV", .Item("WT_LV").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_1", .Item("KYORI_1").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_2", .Item("KYORI_2").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_3", .Item("KYORI_3").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_4", .Item("KYORI_4").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_5", .Item("KYORI_5").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_6", .Item("KYORI_6").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_7", .Item("KYORI_7").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_8", .Item("KYORI_8").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_9", .Item("KYORI_9").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_10", .Item("KYORI_10").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_11", .Item("KYORI_11").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_12", .Item("KYORI_12").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_13", .Item("KYORI_13").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_14", .Item("KYORI_14").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_15", .Item("KYORI_15").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_16", .Item("KYORI_16").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_17", .Item("KYORI_17").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_18", .Item("KYORI_18").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_19", .Item("KYORI_19").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_20", .Item("KYORI_20").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_21", .Item("KYORI_21").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_22", .Item("KYORI_22").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_23", .Item("KYORI_23").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_24", .Item("KYORI_24").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_25", .Item("KYORI_25").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_26", .Item("KYORI_26").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_27", .Item("KYORI_27").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_28", .Item("KYORI_28").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_29", .Item("KYORI_29").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_30", .Item("KYORI_30").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_31", .Item("KYORI_31").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_32", .Item("KYORI_32").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_33", .Item("KYORI_33").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_34", .Item("KYORI_34").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_35", .Item("KYORI_35").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_36", .Item("KYORI_36").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_37", .Item("KYORI_37").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_38", .Item("KYORI_38").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_39", .Item("KYORI_39").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_40", .Item("KYORI_40").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_41", .Item("KYORI_41").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_42", .Item("KYORI_42").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_43", .Item("KYORI_43").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_44", .Item("KYORI_44").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_45", .Item("KYORI_45").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_46", .Item("KYORI_46").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_47", .Item("KYORI_47").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_48", .Item("KYORI_48").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_49", .Item("KYORI_49").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_50", .Item("KYORI_50").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_51", .Item("KYORI_51").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_52", .Item("KYORI_52").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_53", .Item("KYORI_53").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_54", .Item("KYORI_54").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_55", .Item("KYORI_55").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_56", .Item("KYORI_56").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_57", .Item("KYORI_57").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_58", .Item("KYORI_58").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_59", .Item("KYORI_59").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_60", .Item("KYORI_60").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_61", .Item("KYORI_61").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_62", .Item("KYORI_62").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_63", .Item("KYORI_63").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_64", .Item("KYORI_64").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_65", .Item("KYORI_65").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_66", .Item("KYORI_66").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_67", .Item("KYORI_67").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_68", .Item("KYORI_68").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_69", .Item("KYORI_69").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KYORI_70", .Item("KYORI_70").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CITY_EXTC_A", .Item("CITY_EXTC_A").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CITY_EXTC_B", .Item("CITY_EXTC_B").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@WINT_EXTC_A", .Item("WINT_EXTC_A").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@WINT_EXTC_B", .Item("WINT_EXTC_B").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@RELY_EXTC", .Item("RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INSU", .Item("INSU").ToString(), DBDataType.NUMERIC))
            'START YANAI 要望番号377
            'prmList.Add(MyBase.GetSqlParameter("@FRRY_EXTC_10KG", .Item("FRRY_EXTC_10KG").ToString(), DBDataType.NUMERIC))
            'END YANAI 要望番号377
            prmList.Add(MyBase.GetSqlParameter("@FRRY_EXTC_PART", .Item("FRRY_EXTC_PART").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD2", .Item("UNCHIN_TARIFF_CD2").ToString(), DBDataType.NVARCHAR))
            '承認項目は未に戻す
            prmList.Add(MyBase.GetSqlParameter("@APPROVAL_CD", "00", DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@APPROVAL_USER", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@APPROVAL_DATE", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@APPROVAL_TIME", String.Empty, DBDataType.NVARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(運賃タリフＭ登録時))
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd(Optional ByVal DelFlg As Boolean = False)

        ''パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(運賃タリフＭ更新時))
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel(Optional ByVal DelFlg As Boolean = False)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", Me._Row.Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", Me._Row.Item("STR_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        If DelFlg = True Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD_EDA", Me._Row.Item("UNCHIN_TARIFF_CD_EDA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End If

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        '画面パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
