' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM240C : 帳票パターンマスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM240定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM240C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM240IN"
    Public Const TABLE_NM_OUT As String = "LMM240OUT"

    'メッセージ置換文字
    Public Const IDMSG As String = "帳票種類ID"
    Public Const CDMSG As String = "帳票パターンコード"

    '帳票出力先区分
    Public Const NICHIJO As String = "01"
    Public Const TAIRYO As String = "02"
    Public Const NIHUDA As String = "03"


    '帳票種類ID区分値
    Public Const PTNIDKBNT007 As String = "T007"
    Public Const T007VALUE As String = "1.000"

    'レポートファイル名区分値
    Public Const PTNIDKBNR010 As String = "R010"

    Public Const KBNCD As String = "KBN_CD"
    Public Const KBNNM1 As String = "KBN_NM1"
    Public Const KBNNM2 As String = "KBN_NM2"
    Public Const KBNGROUPCD As String = "KBN_GROUP_CD"


    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        INIT
        KENSAKU
        SANSHO
        SHINKI
        HENSHU
        HUKUSHA
        SAKUJO
        HOZON
        VALUECHANGE
        ENTER
        TOJIRU
        DCLICK

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        PTNID
        STANDARDFLAG
        PTNCD
        PTNNM
        RPTID
        PTNCD2
        COPIESNB1
        COPIESNB2
        RPTOUTKB
        OUTPUTKB
        PRINTERNM
        JOBID
        HISTORYFLAG
        REMARK
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum


    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        PTN_ID
        PTN_ID_NM
        STANDARD_FLAG
        STANDARD_FLAG_NM
        PTN_CD
        PTN_NM
        RPT_ID
        RPT_NM
        RPTOUT_KB
        RPTOUT_KB_NM
        COPIES_NB1
        PTN_CD2
        PTN_NM2
        COPIES_NB2
        OUTPUT_KB
        PRINTER_NM
        JOB_ID
        HISTORY_FLAG
        REMARK
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

End Class
