' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH
'  プログラムID     :  LMH820DAC : 入荷確認データ取込(UTI)
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH820DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH820DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"


#End Region

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(COA.NRS_BR_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                      " & vbNewLine

    ''' <summary>
    ''' M_EDI_FOLDER_PASSデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FOLDERPASS As String = " SELECT                                                                  " & vbNewLine _
                                            & "	      FOLDERPASS.NRS_BR_CD                        AS NRS_BR_CD                " & vbNewLine _
                                            & "	     ,FOLDERPASS.WH_CD                            AS WH_CD                    " & vbNewLine _
                                            & "	     ,FOLDERPASS.CUST_CD_L                        AS CUST_CD_L                " & vbNewLine _
                                            & "	     ,FOLDERPASS.CUST_CD_M                        AS CUST_CD_M                " & vbNewLine _
                                            & "	     ,FOLDERPASS.RCV_WORK_INPUT_DIR               AS RCV_WORK_INPUT_DIR       " & vbNewLine _
                                            & "	     ,FOLDERPASS.BACKUP_INPUT_DIR                 AS BACKUP_INPUT_DIR         " & vbNewLine _
                                            & "	     ,FOLDERPASS.RCV_FILE_EXTENTION               AS RCV_FILE_EXTENTION       " & vbNewLine _
                                            & "	     ,FOLDERPASS.ERROR_INPUT_DIR                  AS ERROR_INPUT_DIR          " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_FOLDERPASS As String = "FROM                                                                         " & vbNewLine _
                                          & "             $LM_MST$..M_EDI_FOLDER_PASS AS FOLDERPASS                             " & vbNewLine

#End Region

#Region "WHERE句"

    Private Const SQL_WHERE_FOLDERPASS As String = "WHERE                                                       " & vbNewLine _
                                         & "        FOLDERPASS.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                         & "AND     FOLDERPASS.WH_CD = @WH_CD                                   " & vbNewLine _
                                         & "AND     FOLDERPASS.CUST_CD_L = @CUST_CD_L                           " & vbNewLine _
                                         & "AND     FOLDERPASS.CUST_CD_M = @CUST_CD_M                           " & vbNewLine _
                                         & "AND     FOLDERPASS.EXECUTE_KB = '02'                                " & vbNewLine


#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_HED_UTI As String = "SELECT                                  " & vbNewLine _
                                            & "   COUNT(NRS_BR_CD)  AS REC_CNT          " & vbNewLine _
                                            & "FROM $LM_TRN$..H_INKAEDI_HED_UTI         " & vbNewLine _
                                            & "WHERE NRS_BR_CD        = @NRS_BR_CD      " & vbNewLine _
                                            & "  AND CUST_CD_L        = @CUST_CD_L      " & vbNewLine _
                                            & "  AND CUST_CD_M        = @CUST_CD_M      " & vbNewLine _
                                            & "  AND H4_DELIVERY_NO   = @H4_DELIVERY_NO " & vbNewLine _
                                            & "  AND DEL_KB           = '0'             " & vbNewLine

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' H_INKAEDI_HED_UT DELI
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_H_INKAEDI_HED_UTI As String = " UPDATE                               " & vbNewLine _
                                             & " $LM_TRN$..H_INKAEDI_HED_UTI                      " & vbNewLine _
                                             & " SET                                              " & vbNewLine _
                                             & "    DEL_KB                  = '1'                 " & vbNewLine _
                                             & "  , SYS_UPD_DATE            = @SYS_UPD_DATE       " & vbNewLine _
                                             & "  , SYS_UPD_TIME            = @SYS_UPD_TIME       " & vbNewLine _
                                             & "  , SYS_UPD_PGID            = @SYS_UPD_PGID       " & vbNewLine _
                                             & "  , SYS_UPD_USER            = @SYS_UPD_USER       " & vbNewLine _
                                             & "WHERE NRS_BR_CD             = @NRS_BR_CD          " & vbNewLine _
                                             & "  AND CUST_CD_L             = @CUST_CD_L          " & vbNewLine _
                                             & "  AND CUST_CD_M             = @CUST_CD_M          " & vbNewLine _
                                             & "  AND H4_DELIVERY_NO        = @H4_DELIVERY_NO     " & vbNewLine _
                                             & "--  AND SAP_ODER_NO        　 = @SAP_ODER_NO   --ADD 2018/09/13     " & vbNewLine _
                                             & "  AND DEL_KB                = '0'                 " & vbNewLine _
                                             & "  AND (SYS_UPD_DATE         <> @SYS_UPD_DATE      " & vbNewLine _
                                             & "    OR SYS_UPD_TIME         <> @SYS_UPD_TIME)     " & vbNewLine



    ''' <summary>
    ''' H_INKAEDI_HED_UTI
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_H_INKAEDI_HED_UTI As String = "INSERT INTO $LM_TRN$..H_INKAEDI_HED_UTI          " & vbNewLine _
                                                                          & "(                               " & vbNewLine _
                                                                          & "       DEL_KB                   " & vbNewLine _
                                                                          & "      ,CRT_DATE                 " & vbNewLine _
                                                                          & "      ,FILE_NAME                " & vbNewLine _
                                                                          & "      ,REC_NO                   " & vbNewLine _
                                                                          & "      ,NRS_BR_CD                " & vbNewLine _
                                                                          & "      ,EDI_CTL_NO               " & vbNewLine _
                                                                          & "      ,INKA_CTL_NO_L            " & vbNewLine _
                                                                          & "      ,CUST_CD_L                " & vbNewLine _
                                                                          & "      ,CUST_CD_M                " & vbNewLine _
                                                                          & "      ,PRTFLG                   " & vbNewLine _
                                                                          & "      ,CANCEL_FLG               " & vbNewLine _
                                                                          & "      ,INKA_TAG_FLG             " & vbNewLine _
                                                                          & "      ,H1_CODE                  " & vbNewLine _
                                                                          & "      ,H1_NAME1                 " & vbNewLine _
                                                                          & "      ,H1_NAME2                 " & vbNewLine _
                                                                          & "      ,H1_NAME3                 " & vbNewLine _
                                                                          & "      ,H1_ADRESS1               " & vbNewLine _
                                                                          & "      ,H1_ADRESS2               " & vbNewLine _
                                                                          & "      ,H1_POSTAL_CODE           " & vbNewLine _
                                                                          & "      ,H1_COUNTRY_CODE          " & vbNewLine _
                                                                          & "      ,H1_REGION_CODE           " & vbNewLine _
                                                                          & "      ,H1_TEL_NO                " & vbNewLine _
                                                                          & "      ,H1_FAX_NO                " & vbNewLine _
                                                                          & "      ,H2_CODE                  " & vbNewLine _
                                                                          & "      ,H2_NAME1                 " & vbNewLine _
                                                                          & "      ,H2_NAME2                 " & vbNewLine _
                                                                          & "      ,H2_NAME3                 " & vbNewLine _
                                                                          & "      ,H2_ADRESS1               " & vbNewLine _
                                                                          & "      ,H2_ADRESS2               " & vbNewLine _
                                                                          & "      ,H2_POSTAL_CODE           " & vbNewLine _
                                                                          & "      ,H2_COUNTRY_CODE          " & vbNewLine _
                                                                          & "      ,H2_REGION_CODE           " & vbNewLine _
                                                                          & "      ,H2_TEL_NO                " & vbNewLine _
                                                                          & "      ,H2_FAX_NO                " & vbNewLine _
                                                                          & "      ,H3_CODE                  " & vbNewLine _
                                                                          & "      ,H3_NAME1                 " & vbNewLine _
                                                                          & "      ,H3_NAME2                 " & vbNewLine _
                                                                          & "      ,H3_NAME3                 " & vbNewLine _
                                                                          & "      ,H3_ADRESS1               " & vbNewLine _
                                                                          & "      ,H3_ADRESS2               " & vbNewLine _
                                                                          & "      ,H3_POSTAL_CODE           " & vbNewLine _
                                                                          & "      ,H3_COUNTRY_CODE          " & vbNewLine _
                                                                          & "      ,H3_REGION_CODE           " & vbNewLine _
                                                                          & "      ,H3_TEL_NO                " & vbNewLine _
                                                                          & "      ,H3_FAX_NO                " & vbNewLine _
                                                                          & "      ,H4_INVO_NO               " & vbNewLine _
                                                                          & "      ,H4_DATE                  " & vbNewLine _
                                                                          & "      ,H4_MODE_OF_TRANSPORT     " & vbNewLine _
                                                                          & "      ,H4_DELIVERY_INCOTERM1    " & vbNewLine _
                                                                          & "      ,H4_DELIVERY_INCOTERM2    " & vbNewLine _
                                                                          & "      ,H4_DELIVERY_NO           " & vbNewLine _
                                                                          & "      ,H4_ROUTE                 " & vbNewLine _
                                                                          & "      ,H4_PAYMENT_TERMS         " & vbNewLine _
                                                                          & "      ,S1_TOTAL_AMOUNT          " & vbNewLine _
                                                                          & "      ,S1_CURRENCY              " & vbNewLine _
                                                                          & "      ,S1_SHIPPING_WEIGHT_NET   " & vbNewLine _
                                                                          & "      ,S1_GROSS                 " & vbNewLine _
                                                                          & "      ,S1_UOM                 " & vbNewLine _
                                                                          & "      ,SHIPMENT_NO              " & vbNewLine _
                                                                          & "      ,SAP_ODER_NO              " & vbNewLine _
                                                                          & "      ,CARRIER_NM               " & vbNewLine _
                                                                          & "      ,CONT_TRAILER_ID          " & vbNewLine _
                                                                          & "      ,SHIP_TO_PARTY_CITY       " & vbNewLine _
                                                                          & "      ,SHIP_COMPLETION_DATE     " & vbNewLine _
                                                                          & "      ,PLAN_DELIV_DATE_SHIP     " & vbNewLine _
                                                                          & "      ,CUSTOMER_REQ_DELIV_DATE_ORDER  " & vbNewLine _
                                                                          & "      ,VOYAGE_NO                " & vbNewLine _
                                                                          & "      ,VESSEL_AIRCRAFT_NM       " & vbNewLine _
                                                                          & "      ,SHIP_FROM_COUNTRY        " & vbNewLine _
                                                                          & "      ,ACT_GOODS_ISSUE_TIME     " & vbNewLine _
                                                                          & "      ,SHIPTO_CITY              " & vbNewLine _
                                                                          & "      ,DEST_COUNTRY             " & vbNewLine _
                                                                          & "      ,SHIP_POINT_NM            " & vbNewLine _
                                                                          & "      ,PLANT_NM                 " & vbNewLine _
                                                                          & "      ,DELIVERY_SHIPPNG_TYPE    " & vbNewLine _
                                                                          & "      ,TRANSPORTNG_MODE         " & vbNewLine _
                                                                          & "      ,MISOUCYAKU_SHORI_FLG     " & vbNewLine _
                                                                          & "      ,MISOUCYAKU_USER          " & vbNewLine _
                                                                          & "      ,MISOUCYAKU_DATE          " & vbNewLine _
                                                                          & "      ,MISOUCYAKU_TIME          " & vbNewLine _
                                                                          & "      ,DELETE_USER              " & vbNewLine _
                                                                          & "      ,DELETE_DATE              " & vbNewLine _
                                                                          & "      ,DELETE_TIME              " & vbNewLine _
                                                                          & "      ,DELETE_EDI_NO            " & vbNewLine _
                                                                          & "      ,PRT_USER                 " & vbNewLine _
                                                                          & "      ,PRT_DATE                 " & vbNewLine _
                                                                          & "      ,PRT_TIME                 " & vbNewLine _
                                                                          & "      ,EDI_USER                 " & vbNewLine _
                                                                          & "      ,EDI_DATE                 " & vbNewLine _
                                                                          & "      ,EDI_TIME                 " & vbNewLine _
                                                                          & "      ,INKA_USER                " & vbNewLine _
                                                                          & "      ,INKA_DATE                " & vbNewLine _
                                                                          & "      ,INKA_TIME                " & vbNewLine _
                                                                          & "      ,UPD_USER                 " & vbNewLine _
                                                                          & "      ,UPD_DATE                 " & vbNewLine _
                                                                          & "      ,UPD_TIME                 " & vbNewLine _
                                                                          & "      ,SYS_ENT_DATE             " & vbNewLine _
                                                                          & "      ,SYS_ENT_TIME             " & vbNewLine _
                                                                          & "      ,SYS_ENT_PGID             " & vbNewLine _
                                                                          & "      ,SYS_ENT_USER             " & vbNewLine _
                                                                          & "      ,SYS_UPD_DATE             " & vbNewLine _
                                                                          & "      ,SYS_UPD_TIME             " & vbNewLine _
                                                                          & "      ,SYS_UPD_PGID             " & vbNewLine _
                                                                          & "      ,SYS_UPD_USER             " & vbNewLine _
                                                                          & "      ,SYS_DEL_FLG              " & vbNewLine _
                                                                          & "      ) VALUES (                " & vbNewLine _
                                                                          & "       @DEL_KB                  " & vbNewLine _
                                                                          & "      ,@CRT_DATE                " & vbNewLine _
                                                                          & "      ,@FILE_NAME               " & vbNewLine _
                                                                          & "      ,@REC_NO                  " & vbNewLine _
                                                                          & "      ,@NRS_BR_CD               " & vbNewLine _
                                                                          & "      ,@EDI_CTL_NO              " & vbNewLine _
                                                                          & "      ,@INKA_CTL_NO_L           " & vbNewLine _
                                                                          & "      ,@CUST_CD_L               " & vbNewLine _
                                                                          & "      ,@CUST_CD_M               " & vbNewLine _
                                                                          & "      ,@PRTFLG                  " & vbNewLine _
                                                                          & "      ,@CANCEL_FLG              " & vbNewLine _
                                                                          & "      ,@INKA_TAG_FLG            " & vbNewLine _
                                                                          & "      ,@H1_CODE                 " & vbNewLine _
                                                                          & "      ,@H1_NAME1                " & vbNewLine _
                                                                          & "      ,@H1_NAME2                " & vbNewLine _
                                                                          & "      ,@H1_NAME3                " & vbNewLine _
                                                                          & "      ,@H1_ADRESS1              " & vbNewLine _
                                                                          & "      ,@H1_ADRESS2              " & vbNewLine _
                                                                          & "      ,@H1_POSTAL_CODE          " & vbNewLine _
                                                                          & "      ,@H1_COUNTRY_CODE         " & vbNewLine _
                                                                          & "      ,@H1_REGION_CODE          " & vbNewLine _
                                                                          & "      ,@H1_TEL_NO               " & vbNewLine _
                                                                          & "      ,@H1_FAX_NO               " & vbNewLine _
                                                                          & "      ,@H2_CODE                 " & vbNewLine _
                                                                          & "      ,@H2_NAME1                " & vbNewLine _
                                                                          & "      ,@H2_NAME2                " & vbNewLine _
                                                                          & "      ,@H2_NAME3                " & vbNewLine _
                                                                          & "      ,@H2_ADRESS1              " & vbNewLine _
                                                                          & "      ,@H2_ADRESS2              " & vbNewLine _
                                                                          & "      ,@H2_POSTAL_CODE          " & vbNewLine _
                                                                          & "      ,@H2_COUNTRY_CODE         " & vbNewLine _
                                                                          & "      ,@H2_REGION_CODE          " & vbNewLine _
                                                                          & "      ,@H2_TEL_NO               " & vbNewLine _
                                                                          & "      ,@H2_FAX_NO               " & vbNewLine _
                                                                          & "      ,@H3_CODE                 " & vbNewLine _
                                                                          & "      ,@H3_NAME1                " & vbNewLine _
                                                                          & "      ,@H3_NAME2                " & vbNewLine _
                                                                          & "      ,@H3_NAME3                " & vbNewLine _
                                                                          & "      ,@H3_ADRESS1              " & vbNewLine _
                                                                          & "      ,@H3_ADRESS2              " & vbNewLine _
                                                                          & "      ,@H3_POSTAL_CODE          " & vbNewLine _
                                                                          & "      ,@H3_COUNTRY_CODE         " & vbNewLine _
                                                                          & "      ,@H3_REGION_CODE          " & vbNewLine _
                                                                          & "      ,@H3_TEL_NO               " & vbNewLine _
                                                                          & "      ,@H3_FAX_NO               " & vbNewLine _
                                                                          & "      ,@H4_INVO_NO              " & vbNewLine _
                                                                          & "      ,@H4_DATE                 " & vbNewLine _
                                                                          & "      ,@H4_MODE_OF_TRANSPORT    " & vbNewLine _
                                                                          & "      ,@H4_DELIVERY_INCOTERM1   " & vbNewLine _
                                                                          & "      ,@H4_DELIVERY_INCOTERM2   " & vbNewLine _
                                                                          & "      ,@H4_DELIVERY_NO          " & vbNewLine _
                                                                          & "      ,@H4_ROUTE                " & vbNewLine _
                                                                          & "      ,@H4_PAYMENT_TERMS        " & vbNewLine _
                                                                          & "      ,@S1_TOTAL_AMOUNT         " & vbNewLine _
                                                                          & "      ,@S1_CURRENCY             " & vbNewLine _
                                                                          & "      ,@S1_SHIPPING_WEIGHT_NET  " & vbNewLine _
                                                                          & "      ,@S1_GROSS                " & vbNewLine _
                                                                          & "      ,@S1_UOM                  " & vbNewLine _
                                                                          & "      ,@SHIPMENT_NO             " & vbNewLine _
                                                                          & "      ,@SAP_ODER_NO             " & vbNewLine _
                                                                          & "      ,@CARRIER_NM              " & vbNewLine _
                                                                          & "      ,@CONT_TRAILER_ID         " & vbNewLine _
                                                                          & "      ,@SHIP_TO_PARTY_CITY      " & vbNewLine _
                                                                          & "      ,@SHIP_COMPLETION_DATE    " & vbNewLine _
                                                                          & "      ,@PLAN_DELIV_DATE_SHIP    " & vbNewLine _
                                                                          & "      ,@CUSTOMER_REQ_DELIV_DATE_ORDER  " & vbNewLine _
                                                                          & "      ,@VOYAGE_NO               " & vbNewLine _
                                                                          & "      ,@VESSEL_AIRCRAFT_NM      " & vbNewLine _
                                                                          & "      ,@SHIP_FROM_COUNTRY       " & vbNewLine _
                                                                          & "      ,@ACT_GOODS_ISSUE_TIME    " & vbNewLine _
                                                                          & "      ,@SHIPTO_CITY             " & vbNewLine _
                                                                          & "      ,@DEST_COUNTRY            " & vbNewLine _
                                                                          & "      ,@SHIP_POINT_NM           " & vbNewLine _
                                                                          & "      ,@PLANT_NM                " & vbNewLine _
                                                                          & "      ,@DELIVERY_SHIPPNG_TYPE   " & vbNewLine _
                                                                          & "      ,@TRANSPORTNG_MODE        " & vbNewLine _
                                                                          & "      ,@MISOUCYAKU_SHORI_FLG    " & vbNewLine _
                                                                          & "      ,@MISOUCYAKU_USER         " & vbNewLine _
                                                                          & "      ,@MISOUCYAKU_DATE         " & vbNewLine _
                                                                          & "      ,@MISOUCYAKU_TIME         " & vbNewLine _
                                                                          & "      ,@DELETE_USER             " & vbNewLine _
                                                                          & "      ,@DELETE_DATE             " & vbNewLine _
                                                                          & "      ,@DELETE_TIME             " & vbNewLine _
                                                                          & "      ,@DELETE_EDI_NO           " & vbNewLine _
                                                                          & "      ,@PRT_USER                " & vbNewLine _
                                                                          & "      ,@PRT_DATE                " & vbNewLine _
                                                                          & "      ,@PRT_TIME                " & vbNewLine _
                                                                          & "      ,@EDI_USER                " & vbNewLine _
                                                                          & "      ,@EDI_DATE                " & vbNewLine _
                                                                          & "      ,@EDI_TIME                " & vbNewLine _
                                                                          & "      ,@INKA_USER               " & vbNewLine _
                                                                          & "      ,@INKA_DATE               " & vbNewLine _
                                                                          & "      ,@INKA_TIME               " & vbNewLine _
                                                                          & "      ,@UPD_USER                " & vbNewLine _
                                                                          & "      ,@UPD_DATE                " & vbNewLine _
                                                                          & "      ,@UPD_TIME                " & vbNewLine _
                                                                          & "      ,@SYS_ENT_DATE            " & vbNewLine _
                                                                          & "      ,@SYS_ENT_TIME            " & vbNewLine _
                                                                          & "      ,@SYS_ENT_PGID            " & vbNewLine _
                                                                          & "      ,@SYS_ENT_USER            " & vbNewLine _
                                                                          & "      ,@SYS_UPD_DATE            " & vbNewLine _
                                                                          & "      ,@SYS_UPD_TIME            " & vbNewLine _
                                                                          & "      ,@SYS_UPD_PGID            " & vbNewLine _
                                                                          & "      ,@SYS_UPD_USER            " & vbNewLine _
                                                                          & "      ,@DEL_KB                  " & vbNewLine _
                                                                          & ")                               " & vbNewLine
    ''' <summary>                                                                                                       
    ''' H_INKAEDI_DTL_UTI DEL                                                                                               
    ''' </summary>                                                                                                      
    ''' <remarks></remarks>  
    Private Const SQL_DELETE_H_INKAEDI_DTL_UTI As String = " UPDATE $LM_TRN$..H_INKAEDI_DTL_UTI                    " & vbNewLine _
                                                                        & "  SET                                                  " & vbNewLine _
                                                                        & "    DEL_KB                  = '1'                      " & vbNewLine _
                                                                        & "  , SYS_UPD_DATE            = @SYS_UPD_DATE            " & vbNewLine _
                                                                        & "  , SYS_UPD_TIME            = @SYS_UPD_TIME            " & vbNewLine _
                                                                        & "  , SYS_UPD_PGID            = @SYS_UPD_PGID            " & vbNewLine _
                                                                        & "  , SYS_UPD_USER            = @SYS_UPD_USER            " & vbNewLine _
                                                                        & " FROM $LM_TRN$..H_INKAEDI_HED_UTI  HED                 " & vbNewLine _
                                                                        & " WHERE H_INKAEDI_DTL_UTI.DEL_KB    = '0'               " & vbNewLine _
                                                                        & "   AND H_INKAEDI_DTL_UTI.CRT_DATE  = HED.CRT_DATE      " & vbNewLine _
                                                                        & "   AND H_INKAEDI_DTL_UTI.FILE_NAME = HED.FILE_NAME     " & vbNewLine _
                                                                        & "   AND H_INKAEDI_DTL_UTI.REC_NO    = HED.REC_NO        " & vbNewLine _
                                                                        & "   AND HED.DEL_KB                  = '1'               " & vbNewLine _
                                                                        & "   AND HED.SYS_UPD_DATE            = @SYS_UPD_DATE     " & vbNewLine _
                                                                        & "   AND HED.SYS_UPD_TIME            = @SYS_UPD_TIME     " & vbNewLine _
                                                                        & "   AND HED.SYS_UPD_PGID            = @SYS_UPD_PGID     " & vbNewLine _
                                                                        & "   AND HED.SYS_UPD_USER            = @SYS_UPD_USER     " & vbNewLine



    ''' <summary>                                                                                                       
    ''' H_INKAEDI_DTL_UTI                                                                                               
    ''' </summary>                                                                                                      
    ''' <remarks></remarks>                                                                                             
    Private Const SQL_INSERT_H_INKAEDI_DTL_UTI As String = "INSERT INTO $LM_TRN$..H_INKAEDI_DTL_UTI                     " & vbNewLine _
                                                                          & "(                                          " & vbNewLine _
                                                                          & "       DEL_KB                              " & vbNewLine _
                                                                          & "      ,CRT_DATE                            " & vbNewLine _
                                                                          & "      ,FILE_NAME                           " & vbNewLine _
                                                                          & "      ,REC_NO                              " & vbNewLine _
                                                                          & "      ,GYO                                 " & vbNewLine _
                                                                          & "      ,NRS_BR_CD                           " & vbNewLine _
                                                                          & "      ,EDI_CTL_NO                          " & vbNewLine _
                                                                          & "      ,EDI_CTL_NO_CHU                      " & vbNewLine _
                                                                          & "      ,INKA_CTL_NO_L                       " & vbNewLine _
                                                                          & "      ,INKA_CTL_NO_M                       " & vbNewLine _
                                                                          & "      ,CUST_CD_L                           " & vbNewLine _
                                                                          & "      ,CUST_CD_M                           " & vbNewLine _
                                                                          & "      ,INKA_TAG_FLG                        " & vbNewLine _
                                                                          & "      ,L1_SALES_ORDER                      " & vbNewLine _
                                                                          & "      ,L1_PO                               " & vbNewLine _
                                                                          & "      ,L2_ITEM_CODE                        " & vbNewLine _
                                                                          & "      ,L2_NAME_INTERNAL                    " & vbNewLine _
                                                                          & "      ,L2_COMMODITY_CODE                   " & vbNewLine _
                                                                          & "      ,L2_BATCH_NO                         " & vbNewLine _
                                                                          & "      ,L2_ORIGIN                           " & vbNewLine _
                                                                          & "      ,L2_QUANTITY                         " & vbNewLine _
                                                                          & "      ,L2_QUANTITY_UOM                     " & vbNewLine _
                                                                          & "      ,L2_WEIGHT_NET                       " & vbNewLine _
                                                                          & "      ,L2_GROSS                            " & vbNewLine _
                                                                          & "      ,L2_UOM                              " & vbNewLine _
                                                                          & "      ,L2_PRICE                            " & vbNewLine _
                                                                          & "      ,L2_CURRENCY                         " & vbNewLine _
                                                                          & "      ,L2_AMOUNT                           " & vbNewLine _
                                                                          & "      ,L2_TEXT                             " & vbNewLine _
                                                                          & "      ,L2_PLANT_CODE                       " & vbNewLine _
                                                                          & "      ,L2_PRICE_UNIT                       " & vbNewLine _
                                                                          & "      ,L2_PRICE_QUANTITY                   " & vbNewLine _
                                                                          & "      ,L2_SHIPPING_POINT_CODE              " & vbNewLine _
                                                                          & "      ,L2_TEMP_CONDITION_TEXT              " & vbNewLine _
                                                                          & "      ,L2_STORAGE_CONDITION_CODE           " & vbNewLine _
                                                                          & "      ,L2_REFRIGERATED_CODE                " & vbNewLine _
                                                                          & "      ,L2_EXPORT_RESTRICT                  " & vbNewLine _
                                                                          & "      ,L2_EXPORT_RESTRICT_NEW              " & vbNewLine _
                                                                          & "      ,L3_PACKING_GROUP                    " & vbNewLine _
                                                                          & "      ,L3_CLASS_SUBRISK                    " & vbNewLine _
                                                                          & "      ,L3_UN_NUMBER                        " & vbNewLine _
                                                                          & "      ,L3_LIMITED_QUANTITY                 " & vbNewLine _
                                                                          & "      ,L3_PROPER_SHIP_NAME                 " & vbNewLine _
                                                                          & "      ,L3_TECHNICAL_NAME                   " & vbNewLine _
                                                                          & "      ,L3_IATA_PACKING_INSTRUCTION         " & vbNewLine _
                                                                          & "      ,L3_IATA_CARGO_ONLY                  " & vbNewLine _
                                                                          & "      ,L3_IMO_EMERGENCY_RESPONSE           " & vbNewLine _
                                                                          & "      ,L3_TDD_CLASS_LIMITED_QUANTITY       " & vbNewLine _
                                                                          & "      ,L3_EMERGENCY_TELEPHONE_NUMBERS      " & vbNewLine _
                                                                          & "      ,L3_FLASH_POINT_FOR_IMO              " & vbNewLine _
                                                                          & "      ,L3_MARINE_POLLUTANT_HIS_NAME        " & vbNewLine _
                                                                          & "      ,L3_HAZARDOUS_INFORMATION_STATUS     " & vbNewLine _
                                                                          & "      ,DELIVERY_QTY_IN_EA                  " & vbNewLine _
                                                                          & "      ,REFERENCE_DOC_NO                    " & vbNewLine _
                                                                          & "      ,RECORD_STATUS                       " & vbNewLine _
                                                                          & "      ,JISSEKI_SHORI_FLG                   " & vbNewLine _
                                                                          & "      ,JISSEKI_USER                        " & vbNewLine _
                                                                          & "      ,JISSEKI_DATE                        " & vbNewLine _
                                                                          & "      ,JISSEKI_TIME                        " & vbNewLine _
                                                                          & "      ,SEND_USER                           " & vbNewLine _
                                                                          & "      ,SEND_DATE                           " & vbNewLine _
                                                                          & "      ,SEND_TIME                           " & vbNewLine _
                                                                          & "      ,DELETE_USER                         " & vbNewLine _
                                                                          & "      ,DELETE_DATE                         " & vbNewLine _
                                                                          & "      ,DELETE_TIME                         " & vbNewLine _
                                                                          & "      ,DELETE_EDI_NO                       " & vbNewLine _
                                                                          & "      ,DELETE_EDI_NO_CHU                   " & vbNewLine _
                                                                          & "      ,UPD_USER                            " & vbNewLine _
                                                                          & "      ,UPD_DATE                            " & vbNewLine _
                                                                          & "      ,UPD_TIME                            " & vbNewLine _
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
                                                                          & "       @DEL_KB                             " & vbNewLine _
                                                                          & "      ,@CRT_DATE                           " & vbNewLine _
                                                                          & "      ,@FILE_NAME                          " & vbNewLine _
                                                                          & "      ,@REC_NO                             " & vbNewLine _
                                                                          & "      ,@GYO                                " & vbNewLine _
                                                                          & "      ,@NRS_BR_CD                          " & vbNewLine _
                                                                          & "      ,@EDI_CTL_NO                         " & vbNewLine _
                                                                          & "      ,@EDI_CTL_NO_CHU                     " & vbNewLine _
                                                                          & "      ,@INKA_CTL_NO_L                      " & vbNewLine _
                                                                          & "      ,@INKA_CTL_NO_M                      " & vbNewLine _
                                                                          & "      ,@CUST_CD_L                          " & vbNewLine _
                                                                          & "      ,@CUST_CD_M                          " & vbNewLine _
                                                                          & "      ,@INKA_TAG_FLG                       " & vbNewLine _
                                                                          & "      ,@L1_SALES_ORDER                     " & vbNewLine _
                                                                          & "      ,@L1_PO                              " & vbNewLine _
                                                                          & "      ,@L2_ITEM_CODE                       " & vbNewLine _
                                                                          & "      ,@L2_NAME_INTERNAL                   " & vbNewLine _
                                                                          & "      ,@L2_COMMODITY_CODE                  " & vbNewLine _
                                                                          & "      ,@L2_BATCH_NO                        " & vbNewLine _
                                                                          & "      ,@L2_ORIGIN                          " & vbNewLine _
                                                                          & "      ,@L2_QUANTITY                        " & vbNewLine _
                                                                          & "      ,@L2_QUANTITY_UOM                    " & vbNewLine _
                                                                          & "      ,@L2_WEIGHT_NET                      " & vbNewLine _
                                                                          & "      ,@L2_GROSS                           " & vbNewLine _
                                                                          & "      ,@L2_UOM                             " & vbNewLine _
                                                                          & "      ,@L2_PRICE                           " & vbNewLine _
                                                                          & "      ,@L2_CURRENCY                        " & vbNewLine _
                                                                          & "      ,@L2_AMOUNT                          " & vbNewLine _
                                                                          & "      ,@L2_TEXT                            " & vbNewLine _
                                                                          & "      ,@L2_PLANT_CODE                      " & vbNewLine _
                                                                          & "      ,@L2_PRICE_UNIT                      " & vbNewLine _
                                                                          & "      ,@L2_PRICE_QUANTITY                  " & vbNewLine _
                                                                          & "      ,@L2_SHIPPING_POINT_CODE             " & vbNewLine _
                                                                          & "      ,@L2_TEMP_CONDITION_TEXT             " & vbNewLine _
                                                                          & "      ,@L2_STORAGE_CONDITION_CODE          " & vbNewLine _
                                                                          & "      ,@L2_REFRIGERATED_CODE               " & vbNewLine _
                                                                          & "      ,@L2_EXPORT_RESTRICT                 " & vbNewLine _
                                                                          & "      ,@L2_EXPORT_RESTRICT_NEW             " & vbNewLine _
                                                                          & "      ,@L3_PACKING_GROUP                   " & vbNewLine _
                                                                          & "      ,@L3_CLASS_SUBRISK                   " & vbNewLine _
                                                                          & "      ,@L3_UN_NUMBER                       " & vbNewLine _
                                                                          & "      ,@L3_LIMITED_QUANTITY                " & vbNewLine _
                                                                          & "      ,@L3_PROPER_SHIP_NAME                " & vbNewLine _
                                                                          & "      ,@L3_TECHNICAL_NAME                  " & vbNewLine _
                                                                          & "      ,@L3_IATA_PACKING_INSTRUCTION        " & vbNewLine _
                                                                          & "      ,@L3_IATA_CARGO_ONLY                 " & vbNewLine _
                                                                          & "      ,@L3_IMO_EMERGENCY_RESPONSE          " & vbNewLine _
                                                                          & "      ,@L3_TDD_CLASS_LIMITED_QUANTITY      " & vbNewLine _
                                                                          & "      ,@L3_EMERGENCY_TELEPHONE_NUMBERS     " & vbNewLine _
                                                                          & "      ,@L3_FLASH_POINT_FOR_IMO             " & vbNewLine _
                                                                          & "      ,@L3_MARINE_POLLUTANT_HIS_NAME       " & vbNewLine _
                                                                          & "      ,@L3_HAZARDOUS_INFORMATION_STATUS    " & vbNewLine _
                                                                          & "      ,@DELIVERY_QTY_IN_EA                 " & vbNewLine _
                                                                          & "      ,@REFERENCE_DOC_NO                   " & vbNewLine _
                                                                          & "      ,@RECORD_STATUS                      " & vbNewLine _
                                                                          & "      ,@JISSEKI_SHORI_FLG                  " & vbNewLine _
                                                                          & "      ,@JISSEKI_USER                       " & vbNewLine _
                                                                          & "      ,@JISSEKI_DATE                       " & vbNewLine _
                                                                          & "      ,@JISSEKI_TIME                       " & vbNewLine _
                                                                          & "      ,@SEND_USER                          " & vbNewLine _
                                                                          & "      ,@SEND_DATE                          " & vbNewLine _
                                                                          & "      ,@SEND_TIME                          " & vbNewLine _
                                                                          & "      ,@DELETE_USER                        " & vbNewLine _
                                                                          & "      ,@DELETE_DATE                        " & vbNewLine _
                                                                          & "      ,@DELETE_TIME                        " & vbNewLine _
                                                                          & "      ,@DELETE_EDI_NO                      " & vbNewLine _
                                                                          & "      ,@DELETE_EDI_NO_CHU                  " & vbNewLine _
                                                                          & "      ,@UPD_USER                           " & vbNewLine _
                                                                          & "      ,@UPD_DATE                           " & vbNewLine _
                                                                          & "      ,@UPD_TIME                           " & vbNewLine _
                                                                          & "      ,@SYS_ENT_DATE                       " & vbNewLine _
                                                                          & "      ,@SYS_ENT_TIME                       " & vbNewLine _
                                                                          & "      ,@SYS_ENT_PGID                       " & vbNewLine _
                                                                          & "      ,@SYS_ENT_USER                       " & vbNewLine _
                                                                          & "      ,@SYS_UPD_DATE                       " & vbNewLine _
                                                                          & "      ,@SYS_UPD_TIME                       " & vbNewLine _
                                                                          & "      ,@SYS_UPD_PGID                       " & vbNewLine _
                                                                          & "      ,@SYS_UPD_USER                       " & vbNewLine _
                                                                          & "      ,@DEL_KB                             " & vbNewLine _
                                                                          & ")                                          " & vbNewLine

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
    ''' フォルダパス取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDIフォルダパスマスタ取得SQLの構築・発行</remarks>
    Private Function GetEdiFolderPass(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH820IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH820DAC.SQL_SELECT_FOLDERPASS)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH820DAC.SQL_FROM_FOLDERPASS)        'SQL構築(データ抽出用from句)
        Me._StrSql.Append(LMH820DAC.SQL_WHERE_FOLDERPASS)       'SQL構築(データ抽出用Where句)
        Call Me.SetinParameter()                                '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH820DAC", "GetCustCoaFolder", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("RCV_WORK_INPUT_DIR", "RCV_WORK_INPUT_DIR")
        map.Add("BACKUP_INPUT_DIR", "BACKUP_INPUT_DIR")
        map.Add("ERROR_INPUT_DIR", "ERROR_INPUT_DIR")
        map.Add("RCV_FILE_EXTENTION", "RCV_FILE_EXTENTION")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH820IN")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' UTI入荷確認EDIデータ(DTL)存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>UTI入荷確認EDIデータ(DTL)件数取得SQLの構築・発行</remarks>
    Private Function GetHinkaEdiHedCheck(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH820_H_INKAEDI_HED_UTI")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH820DAC.SQL_EXIT_HED_UTI, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH820DAC", "GetHinkaEdiHedCheck", cmd)

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
    ''' UTI入荷確認EDIデータ(HED)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>UTI入荷確認EDIデータ(DTL)削除SQLの構築・発行</remarks>
    Private Function DeleteHInkaEdiHedUti(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim hedTbl As DataTable = ds.Tables("LMH820_H_INKAEDI_HED_UTI")
        'Dim delTbl As DataTable = ds.Tables("LMH820_DEL")
        Dim delHedCnt As Integer = 0

        Dim max As Integer = hedTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = hedTbl.Rows(i)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH820DAC.SQL_DELETE_H_INKAEDI_HED_UTI, Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamDeleteHed(hedTbl)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH820DAC", "DeleteHInkaEdiHedUti", cmd)


            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

            delHedCnt = delHedCnt + MyBase.GetResultCount

        Next

        ds.Tables("LMH820Result").Rows(0).Item("DEL_HED_CNT") = delHedCnt

        Return ds

    End Function


    ''' <summary>
    ''' UTI入荷確認EDIデータ(DTL)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>UTI入荷確認EDIデータ(DTL)削除SQLの構築・発行</remarks>
    Private Function DeleteHInkaEdiDtlUti(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim hedTbl As DataTable = ds.Tables("LMH820_H_INKAEDI_HED_UTI")
        'Dim delTbl As DataTable = ds.Tables("LMH820_DEL")
        Dim delDtlCnt As Integer = 0

        'INTableの条件rowの格納
        Me._Row = hedTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH820DAC.SQL_DELETE_H_INKAEDI_DTL_UTI, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDeleteDtl(hedTbl)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH820DAC", "DeleteHInkaEdiDtlUti", cmd)

        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        delDtlCnt = delDtlCnt + MyBase.GetResultCount

        ds.Tables("LMH820Result").Rows(0).Item("DEL_DTL_CNT") = delDtlCnt

        Return ds

    End Function

    ''' <summary>
    ''' UTI入荷確認EDIデータ(HED)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>UTI入荷確認EDIデータ(DTL)新規登録SQLの構築・発行</remarks>
    Private Function InsertHInkaEdiHedUti(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim hedTbl As DataTable = ds.Tables("LMH820_H_INKAEDI_HED_UTI")
        Dim delTbl As DataTable = ds.Tables("LMH820_DEL")
        Dim insHedCnt As Integer = 0

        Dim max As Integer = hedTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = hedTbl.Rows(i)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH820DAC.SQL_INSERT_H_INKAEDI_HED_UTI, Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertHed(delTbl)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH820DAC", "InsertHInkaEdiHedUti", cmd)


            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            insHedCnt = insHedCnt + MyBase.GetResultCount

        Next

        ds.Tables("LMH820Result").Rows(0).Item("INS_HED_CNT") = insHedCnt

        Return ds

    End Function

    ''' <summary>
    ''' UTI入荷確認EDIデータ(DTL)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>UTI入荷確認EDIデータ(DTL)新規登録SQLの構築・発行</remarks>
    Private Function InsertHInkaEdiDtlUti(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtlTbl As DataTable = ds.Tables("LMH820_H_INKAEDI_DTL_UTI")
        Dim delTbl As DataTable = ds.Tables("LMH820_DEL")

        Dim insDtlCnt As Integer = 0

        Dim max As Integer = dtlTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = dtlTbl.Rows(i)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH820DAC.SQL_INSERT_H_INKAEDI_DTL_UTI, Me._Row.Item("NRS_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertDtl(delTbl)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH820DAC", "InsertHInkaEdiDtlUti", cmd)

            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            insDtlCnt = insDtlCnt + MyBase.GetResultCount

        Next

        ds.Tables("LMH820Result").Rows(0).Item("INS_DTL_CNT") = insDtlCnt

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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定(EDIフォルダパスマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetinParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(UTI入荷確認EDIデータ(EDI)存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_DELIVERY_NO", .Item("H4_DELIVERY_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDeleteHed(ByVal delTbl As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call SetDeletetHedParam(delTbl)

        'システム項目
        'Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除DTL)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDeleteDtl(ByVal delTbl As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call SetDeletetDtlParam(delTbl)

        'システム項目
        'Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertHed(ByVal delTbl As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call SetInsertHedParam(delTbl)

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録DTL)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertDtl(ByVal delTbl As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call SetInsertDtlParam(delTbl)

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    '''' <summary>
    '''' パラメータ設定モジュール(更新登録用)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SetParamUpdate()

    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    '共通項目
    '    Call Me.SetComParam()

    '    '更新項目
    '    Call Me.SetParamCommonSystemUpd()

    'End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDeletetHedParam(ByVal delTbl As DataTable)

        Dim max As Integer = delTbl.Rows.Count - 1
        Dim delParFlg As String = "0"

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_DELIVERY_NO", .Item("H4_DELIVERY_NO").ToString(), DBDataType.NVARCHAR))
            'DEL 2018/10/11            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAP_ODER_NO", .Item("SAP_ODER_NO").ToString(), DBDataType.NVARCHAR))            'ADD 2018/09/13
            'Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDeletetDtlParam(ByVal delTbl As DataTable)

        Dim max As Integer = delTbl.Rows.Count - 1
        Dim delParFlg As String = "0"

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInsertHedParam(ByVal delTbl As DataTable)

        Dim max As Integer = delTbl.Rows.Count - 1
        Dim delParFlg As String = "0"

        With Me._Row
            'パラメータ設定

            If max = -1 Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            Else

                For i As Integer = 0 To max
                    If (delTbl.Rows(i).Item("FILE_NAME").ToString()).Equals(.Item("FILE_NAME").ToString()) = True AndAlso _
                       (delTbl.Rows(i).Item("REC_NO").ToString()).Equals(.Item("REC_NO").ToString()) = True AndAlso _
                       (delTbl.Rows(i).Item("CRT_DATE").ToString()).Equals(MyBase.GetSystemDate()) = True Then
                        delParFlg = "1"
                        Exit For
                    ElseIf delParFlg.Equals("0") = True Then
                        delParFlg = "2"
                    End If

                Next

                If delParFlg.Equals("1") = True Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", delParFlg, DBDataType.CHAR))
                ElseIf delParFlg.Equals("2") = True Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
                End If

            End If
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", .Item("INKA_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", .Item("PRTFLG").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_FLG", .Item("CANCEL_FLG").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TAG_FLG", .Item("INKA_TAG_FLG").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_CODE", .Item("H1_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_NAME1", .Item("H1_NAME1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_NAME2", .Item("H1_NAME2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_NAME3", .Item("H1_NAME3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_ADRESS1", .Item("H1_ADRESS1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_ADRESS2", .Item("H1_ADRESS2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_POSTAL_CODE", .Item("H1_POSTAL_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_COUNTRY_CODE", .Item("H1_COUNTRY_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_REGION_CODE", .Item("H1_REGION_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_TEL_NO", .Item("H1_TEL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H1_FAX_NO", .Item("H1_FAX_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_CODE", .Item("H2_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_NAME1", .Item("H2_NAME1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_NAME2", .Item("H2_NAME2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_NAME3", .Item("H2_NAME3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_ADRESS1", .Item("H2_ADRESS1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_ADRESS2", .Item("H2_ADRESS2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_POSTAL_CODE", .Item("H2_POSTAL_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_COUNTRY_CODE", .Item("H2_COUNTRY_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_REGION_CODE", .Item("H2_REGION_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_TEL_NO", .Item("H2_TEL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H2_FAX_NO", .Item("H2_FAX_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_CODE", .Item("H3_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_NAME1", .Item("H3_NAME1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_NAME2", .Item("H3_NAME2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_NAME3", .Item("H3_NAME3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_ADRESS1", .Item("H3_ADRESS1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_ADRESS2", .Item("H3_ADRESS2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_POSTAL_CODE", .Item("H3_POSTAL_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_COUNTRY_CODE", .Item("H3_COUNTRY_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_REGION_CODE", .Item("H3_REGION_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_TEL_NO", .Item("H3_TEL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H3_FAX_NO", .Item("H3_FAX_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_INVO_NO", .Item("H4_INVO_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_DATE", .Item("H4_DATE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_MODE_OF_TRANSPORT", .Item("H4_MODE_OF_TRANSPORT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_DELIVERY_INCOTERM1", .Item("H4_DELIVERY_INCOTERM1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_DELIVERY_INCOTERM2", .Item("H4_DELIVERY_INCOTERM2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_DELIVERY_NO", .Item("H4_DELIVERY_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_ROUTE", .Item("H4_ROUTE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@H4_PAYMENT_TERMS", .Item("H4_PAYMENT_TERMS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@S1_TOTAL_AMOUNT", Me.FormatNumValue(.Item("S1_TOTAL_AMOUNT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@S1_CURRENCY", .Item("S1_CURRENCY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@S1_SHIPPING_WEIGHT_NET", Me.FormatNumValue(.Item("S1_SHIPPING_WEIGHT_NET").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@S1_GROSS", Me.FormatNumValue(.Item("S1_GROSS").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@S1_UOM", .Item("S1_UOM").ToString(), DBDataType.NVARCHAR))
            'ADD 2018/01/25 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIPMENT_NO", .Item("SHIPMENT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAP_ODER_NO", .Item("SAP_ODER_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CARRIER_NM", .Item("CARRIER_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CONT_TRAILER_ID", .Item("CONT_TRAILER_ID").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_TO_PARTY_CITY", .Item("SHIP_TO_PARTY_CITY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_COMPLETION_DATE", .Item("SHIP_COMPLETION_DATE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLAN_DELIV_DATE_SHIP", .Item("PLAN_DELIV_DATE_SHIP").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUSTOMER_REQ_DELIV_DATE_ORDER", .Item("CUSTOMER_REQ_DELIV_DATE_ORDER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VOYAGE_NO", .Item("VOYAGE_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VESSEL_AIRCRAFT_NM", .Item("VESSEL_AIRCRAFT_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_FROM_COUNTRY", .Item("SHIP_FROM_COUNTRY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ACT_GOODS_ISSUE_TIME", .Item("ACT_GOODS_ISSUE_TIME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIPTO_CITY", .Item("SHIPTO_CITY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_COUNTRY", .Item("DEST_COUNTRY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_POINT_NM", .Item("SHIP_POINT_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLANT_NM", .Item("PLANT_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELIVERY_SHIPPNG_TYPE", .Item("DELIVERY_SHIPPNG_TYPE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRANSPORTNG_MODE", .Item("TRANSPORTNG_MODE").ToString(), DBDataType.NVARCHAR))
            'ADD 2018/01/25 End
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISOUCYAKU_SHORI_FLG", .Item("MISOUCYAKU_SHORI_FLG").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISOUCYAKU_USER", .Item("MISOUCYAKU_USER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISOUCYAKU_DATE", .Item("MISOUCYAKU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISOUCYAKU_TIME", .Item("MISOUCYAKU_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", .Item("DELETE_USER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", .Item("DELETE_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", .Item("DELETE_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", .Item("DELETE_EDI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_USER", .Item("PRT_USER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_DATE", .Item("PRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TIME", .Item("PRT_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_USER", .Item("EDI_USER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE", .Item("EDI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_TIME", .Item("EDI_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_USER", .Item("INKA_USER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TIME", .Item("INKA_TIME").ToString(), DBDataType.CHAR))
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(DTL更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInsertDtlParam(ByVal delTbl As DataTable)

        Dim max As Integer = delTbl.Rows.Count - 1
        Dim delParFlg As String = "0"

        With Me._Row
            'パラメータ設定

            If max = -1 Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            Else

                For i As Integer = 0 To max
                    If (delTbl.Rows(i).Item("FILE_NAME").ToString()).Equals(.Item("FILE_NAME").ToString()) = True AndAlso _
                       (delTbl.Rows(i).Item("REC_NO").ToString()).Equals(.Item("REC_NO").ToString()) = True AndAlso _
                       (delTbl.Rows(i).Item("CRT_DATE").ToString()).Equals(MyBase.GetSystemDate()) = True AndAlso _
                       (delTbl.Rows(i).Item("GYO").ToString()).Equals(.Item("GYO").ToString()) = True Then
                        delParFlg = "1"
                        Exit For
                    ElseIf delParFlg.Equals("0") = True Then
                        delParFlg = "2"
                    End If

                Next

                If delParFlg.Equals("1") = True Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", delParFlg, DBDataType.CHAR))
                ElseIf delParFlg.Equals("2") = True Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
                End If

            End If

            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", .Item("INKA_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", .Item("INKA_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TAG_FLG", .Item("INKA_TAG_FLG").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L1_SALES_ORDER", .Item("L1_SALES_ORDER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L1_PO", .Item("L1_PO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_ITEM_CODE", .Item("L2_ITEM_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_NAME_INTERNAL", .Item("L2_NAME_INTERNAL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_COMMODITY_CODE", .Item("L2_COMMODITY_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_BATCH_NO", .Item("L2_BATCH_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_ORIGIN", .Item("L2_ORIGIN").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_QUANTITY", Me.FormatNumValue(.Item("L2_QUANTITY").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_QUANTITY_UOM", .Item("L2_QUANTITY_UOM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_WEIGHT_NET", Me.FormatNumValue(.Item("L2_WEIGHT_NET").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_GROSS", Me.FormatNumValue(.Item("L2_GROSS").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_UOM", .Item("L2_UOM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_PRICE", Me.FormatNumValue(.Item("L2_PRICE").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_CURRENCY", .Item("L2_CURRENCY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_AMOUNT", Me.FormatNumValue(.Item("L2_AMOUNT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_TEXT", .Item("L2_TEXT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_PLANT_CODE", .Item("L2_PLANT_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_PRICE_UNIT", .Item("L2_PRICE_UNIT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_PRICE_QUANTITY", Me.FormatNumValue(.Item("L2_PRICE_QUANTITY").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_SHIPPING_POINT_CODE", .Item("L2_SHIPPING_POINT_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_TEMP_CONDITION_TEXT", .Item("L2_TEMP_CONDITION_TEXT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_STORAGE_CONDITION_CODE", .Item("L2_STORAGE_CONDITION_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_REFRIGERATED_CODE", .Item("L2_REFRIGERATED_CODE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_EXPORT_RESTRICT", .Item("L2_EXPORT_RESTRICT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L2_EXPORT_RESTRICT_NEW", .Item("L2_EXPORT_RESTRICT_NEW").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_PACKING_GROUP", .Item("L3_PACKING_GROUP").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_CLASS_SUBRISK", .Item("L3_CLASS_SUBRISK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_UN_NUMBER", .Item("L3_UN_NUMBER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_LIMITED_QUANTITY", .Item("L3_LIMITED_QUANTITY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_PROPER_SHIP_NAME", .Item("L3_PROPER_SHIP_NAME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_TECHNICAL_NAME", .Item("L3_TECHNICAL_NAME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_IATA_PACKING_INSTRUCTION", .Item("L3_IATA_PACKING_INSTRUCTION").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_IATA_CARGO_ONLY", .Item("L3_IATA_CARGO_ONLY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_IMO_EMERGENCY_RESPONSE", .Item("L3_IMO_EMERGENCY_RESPONSE").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_TDD_CLASS_LIMITED_QUANTITY", .Item("L3_TDD_CLASS_LIMITED_QUANTITY").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_EMERGENCY_TELEPHONE_NUMBERS", .Item("L3_EMERGENCY_TELEPHONE_NUMBERS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_FLASH_POINT_FOR_IMO", .Item("L3_FLASH_POINT_FOR_IMO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_MARINE_POLLUTANT_HIS_NAME", .Item("L3_MARINE_POLLUTANT_HIS_NAME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@L3_HAZARDOUS_INFORMATION_STATUS", .Item("L3_HAZARDOUS_INFORMATION_STATUS").ToString(), DBDataType.NVARCHAR))
            'ADD 2018/01/25 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELIVERY_QTY_IN_EA", Me.FormatNumValue(.Item("DELIVERY_QTY_IN_EA").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REFERENCE_DOC_NO", .Item("REFERENCE_DOC_NO").ToString(), DBDataType.NVARCHAR))
            'ADD 2018/01/25 End
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", .Item("RECORD_STATUS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", .Item("JISSEKI_SHORI_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", .Item("JISSEKI_USER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", .Item("JISSEKI_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", .Item("SEND_USER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", .Item("SEND_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", .Item("SEND_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", .Item("DELETE_USER").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", .Item("DELETE_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", .Item("DELETE_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", .Item("DELETE_EDI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", .Item("DELETE_EDI_NO_CHU").ToString(), DBDataType.CHAR))
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))

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
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region

#End Region

#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region
#End Region

End Class
