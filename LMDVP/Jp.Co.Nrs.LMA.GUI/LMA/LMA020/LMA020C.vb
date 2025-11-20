' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  LMA020C : メニュー
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility.Spread

''' <summary>
''' LMA020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMA020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst


    Friend Const SUB_SYSTEM_ID_LMB As String = "LMB"
    Friend Const SUB_SYSTEM_ID_LMC As String = "LMC"
    Friend Const SUB_SYSTEM_ID_LMD As String = "LMD"
    Friend Const SUB_SYSTEM_ID_LME As String = "LME"
    Friend Const SUB_SYSTEM_ID_LMF As String = "LMF"
    Friend Const SUB_SYSTEM_ID_LMG As String = "LMG"
    Friend Const SUB_SYSTEM_ID_LMH As String = "LMH"
    Friend Const SUB_SYSTEM_ID_LMI As String = "LMI"
    Friend Const SUB_SYSTEM_ID_LMJ As String = "LMJ"
    Friend Const SUB_SYSTEM_ID_LMK As String = "LMK"
    Friend Const SUB_SYSTEM_ID_LML As String = "LML"
    Friend Const SUB_SYSTEM_ID_LMM As String = "LMM"
    Friend Const SUB_SYSTEM_ID_LMN As String = "LMN"
    Friend Const SUB_SYSTEM_ID_LMQ As String = "LMQ"
    Friend Const SUB_SYSTEM_ID_LMR As String = "LMR"
    Friend Const SUB_SYSTEM_ID_LMS As String = "LMS"
    Friend Const SUB_SYSTEM_ID_LMT As String = "LMT"
    Friend Const SUB_SYSTEM_ID_LMU As String = "LMU"
    Friend Const SUB_SYSTEM_ID_LMZ As String = "LMZ"
    
    Friend Const MENU_TITLE_LMB As String = "  入  荷  "
    Friend Const MENU_TITLE_LMC As String = "  出  荷  "
    Friend Const MENU_TITLE_LMD As String = "  在  庫  "
    Friend Const MENU_TITLE_LME As String = "  作  業  "
    Friend Const MENU_TITLE_LMF As String = "  運  送  "
    Friend Const MENU_TITLE_LMG As String = "  請  求  "
    Friend Const MENU_TITLE_LMH As String = "  E D I   "
    Friend Const MENU_TITLE_LMI As String = " 特定荷主 " & vbNewLine & "  機　能  "
    Friend Const MENU_TITLE_LMJ As String = " システム " & vbNewLine & "  管  理  "
    'yamanaka 2012.08.08 Start
    Friend Const MENU_TITLE_LMK As String = "  支　払  "
    'yamanaka 2012.08.08 End
    Friend Const MENU_TITLE_LML As String = " 協力会社 " & vbNewLine & "  管  理  "    'UPD 2021/01025 018023   【LMS】将栄物流_協力会社機能作成
    Friend Const MENU_TITLE_LMM1 As String = "基本マスタ"
    Friend Const MENU_TITLE_LMM2 As String = " 他マスタ "
    Friend Const MENU_TITLE_LMM3 As String = " メ ン テ " & vbNewLine & " ナ ン ス "
    Friend Const MENU_TITLE_LMN As String = "  S C M   "
    Friend Const MENU_TITLE_LMQ As String = "データ抽出"
    Friend Const MENU_TITLE_LMR As String = "  完  了  "
    Friend Const MENU_TITLE_LMS As String = "          "
    Friend Const MENU_TITLE_LMT As String = "          "
    Friend Const MENU_TITLE_LMU As String = " スキャン " & vbNewLine & "  取　込  "
    'Friend Const MENU_TITLE_LMZ As String = "          "

    '20151102 tsunehira 
    Friend Const MENU_TITLE_LMB_ENG As String = " Inbound  "
    Friend Const MENU_TITLE_LMC_ENG As String = " Outbound "
    Friend Const MENU_TITLE_LMD_ENG As String = "Inventory "
    Friend Const MENU_TITLE_LME_ENG As String = "   Work   "
    Friend Const MENU_TITLE_LMF_ENG As String = "   TPTN   "
    Friend Const MENU_TITLE_LMG_ENG As String = " Invoice  "
    Friend Const MENU_TITLE_LMH_ENG As String = "  E D I   "
    Friend Const MENU_TITLE_LMI_ENG As String = " Specific " & vbNewLine & " shipper "
    Friend Const MENU_TITLE_LMJ_ENG As String = "  System  " & vbNewLine & "   Mgt   "
    Friend Const MENU_TITLE_LMK_ENG As String = " Payment  "

    Friend Const MENU_TITLE_LML_ENG As String = "          "
    Friend Const MENU_TITLE_LMM1_ENG As String = "  Normal  " & vbNewLine & " Master "
    Friend Const MENU_TITLE_LMM2_ENG As String = "   Other  " & vbNewLine & " Master "
    Friend Const MENU_TITLE_LMM3_ENG As String = "  Mainte " & vbNewLine & "  nance "
    Friend Const MENU_TITLE_LMN_ENG As String = "  S C M   "
    Friend Const MENU_TITLE_LMQ_ENG As String = "  Report  "
    Friend Const MENU_TITLE_LMR_ENG As String = "  Confirm "
    Friend Const MENU_TITLE_LMS_ENG As String = "          "
    Friend Const MENU_TITLE_LMT_ENG As String = "          "
    Friend Const MENU_TITLE_LMU_ENG As String = "   Scan   "
    Friend Const MENU_TITLE_LMZ_ENG As String = ""


    '2017/09/25 追加 李↓
    Friend Const MENU_TITLE_LMB_KR As String = "  입  하  "
    Friend Const MENU_TITLE_LMC_KR As String = "  출  하  "
    Friend Const MENU_TITLE_LMD_KR As String = "  재  고  "
    Friend Const MENU_TITLE_LME_KR As String = "  작  업  "
    Friend Const MENU_TITLE_LMF_KR As String = "  운  송  "
    Friend Const MENU_TITLE_LMG_KR As String = "  청  구  "
    Friend Const MENU_TITLE_LMH_KR As String = "  E D I   "
    Friend Const MENU_TITLE_LMI_KR As String = "특정 물품주" & vbNewLine & "  기　능  "
    Friend Const MENU_TITLE_LMJ_KR As String = "  시스템  " & vbNewLine & "  관  리  "
    Friend Const MENU_TITLE_LMK_KR As String = "  지　불  "
    Friend Const MENU_TITLE_LML_KR As String = "          "
    Friend Const MENU_TITLE_LMM1_KR As String = "  기 본  " & vbNewLine & "  마스터  "
    Friend Const MENU_TITLE_LMM2_KR As String = "타 마스터 "
    Friend Const MENU_TITLE_LMM3_KR As String = " 멘테넌스 "
    Friend Const MENU_TITLE_LMN_KR As String = "  S C M   "
    Friend Const MENU_TITLE_LMQ_KR As String = "  데이터  " & vbNewLine & "  추　출  "
    Friend Const MENU_TITLE_LMR_KR As String = "  완  료  "
    Friend Const MENU_TITLE_LMS_KR As String = "          "
    Friend Const MENU_TITLE_LMT_KR As String = "          "
    Friend Const MENU_TITLE_LMU_KR As String = "  스  캔  "
    '2017/09/25 追加 李↑



    Friend Const MENU_FLG_NOMAL As String = "1"
    Friend Const MENU_FLG_OTHER As String = "2"
    Friend Const MENU_FLG_MAINT As String = "3"

#Region "Const"

    '2015.11.02 tusnehira add
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

    ''' <summary>
    ''' タブインデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        LMB = 0
        LMC
        LMD
        LME
        LMF
        LMG
        LMH
        LMI
        LMJ
        LMK
        LML
        LMM1
        LMM2
        LMM3
        LMN
        LMQ
        LMR
        LMS
        LMT
        LMU
        'LMZ

    End Enum

End Class

