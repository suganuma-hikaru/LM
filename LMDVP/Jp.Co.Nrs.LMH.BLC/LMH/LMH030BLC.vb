' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索(共通チェック)
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC

    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC = New LMH030DAC()

#End Region

#Region "Const"
    ''' <summary>
    ''' EDIワーニングID定数定義
    ''' </summary>
    ''' <remarks></remarks>

    Public Const SAKURA_WID_L001 As String = "401_OT_0_L001"  'サクラ(横浜)用:Lレベル
    Public Const SAKURA_WID_L002 As String = "402_OT_0_L002"  'サクラ(横浜)用:Lレベル

    Public Const DUPONT_WID_L001 As String = "403_OT_0_L001"  'デュポン(横浜)用:Lレベル
    Public Const DUPONT_WID_L002 As String = "403_OT_0_L002"  'デュポン(横浜)用:Lレベル
    Public Const DUPONT_WID_L003 As String = "403_OT_0_L003"  'デュポン(横浜)用:Lレベル
    Public Const DUPONT_WID_L004 As String = "403_OT_0_L004"  'デュポン(横浜)用:Lレベル
    Public Const DUPONT_WID_L005 As String = "403_OT_0_L005"  'デュポン(横浜)用:Lレベル

    Public Const NCGO_WID_L001 As String = "601_OT_0_L001"    '日本合成化学(名古屋)用:Lレベル
    Public Const NCGO_WID_L002 As String = "601_OT_0_L002"    '日本合成化学(名古屋)用:Lレベル
    Public Const NCGO_WID_L003 As String = "601_OT_0_L003"    '日本合成化学(名古屋)用:Lレベル
    Public Const NCGO_WID_L004 As String = "601_OT_0_L004"    '日本合成化学(名古屋)用:Lレベル
    Public Const NCGO_WID_L005 As String = "601_OT_0_L005"    '日本合成化学(名古屋)用:Lレベル
    Public Const NCGO_WID_L006 As String = "601_OT_0_L006"    '日本合成化学(名古屋)用:Lレベル
    Public Const NCGO_WID_L007 As String = "601_OT_0_L007"    '日本合成化学(名古屋)用:Lレベル
    Public Const NCGO_WID_L008 As String = "601_OT_0_L008"    '日本合成化学(名古屋)用:Lレベル
    Public Const NCGO_WID_L009 As String = "601_OT_0_L009"    '日本合成化学(名古屋)用:Lレベル

    Public Const DSP_WID_L001 As String = "206_OT_0_L001"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L002 As String = "206_OT_0_L002"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L003 As String = "206_OT_0_L003"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L004 As String = "206_OT_0_L004"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L005 As String = "206_OT_0_L005"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L006 As String = "206_OT_0_L006"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L007 As String = "206_OT_0_L007"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L008 As String = "206_OT_0_L008"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L009 As String = "206_OT_0_L009"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L010 As String = "206_OT_0_L010"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L011 As String = "206_OT_0_L011"     '大日本住友製薬(大阪)用:Lレベル
    Public Const DSP_WID_L012 As String = "206_OT_0_L012"     '大日本住友製薬(大阪)用:Lレベル

    Public Const TOHO_WID_L001 As String = "201_OT_0_L001"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L002 As String = "201_OT_0_L002"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L003 As String = "201_OT_0_L003"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L004 As String = "201_OT_0_L004"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L005 As String = "201_OT_0_L005"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L006 As String = "201_OT_0_L006"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L007 As String = "201_OT_0_L007"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L008 As String = "201_OT_0_L008"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L009 As String = "201_OT_0_L009"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L010 As String = "201_OT_0_L010"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L011 As String = "201_OT_0_L011"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L012 As String = "201_OT_0_L012"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L013 As String = "201_OT_0_L013"     '東邦化学(大阪)用:Lレベル
    Public Const TOHO_WID_L014 As String = "201_OT_0_L014"     '東邦化学(大阪)用:Lレベル

    Public Const DOW_WID_L001 As String = "201_OT_0_L001"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L002 As String = "201_OT_0_L002"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L003 As String = "201_OT_0_L003"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L004 As String = "201_OT_0_L004"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L005 As String = "201_OT_0_L005"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L006 As String = "201_OT_0_L006"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L007 As String = "201_OT_0_L007"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L008 As String = "201_OT_0_L008"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L009 As String = "201_OT_0_L009"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L010 As String = "201_OT_0_L010"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L011 As String = "201_OT_0_L011"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L012 As String = "201_OT_0_L012"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L013 As String = "201_OT_0_L013"     'ダウケミ(大阪)用:Lレベル
    Public Const DOW_WID_L014 As String = "201_OT_0_L014"     'ダウケミ(大阪)用:Lレベル

    Public Const DIC_WID_L001 As String = "205_OT_0_L001"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L002 As String = "205_OT_0_L002"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L003 As String = "205_OT_0_L003"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L004 As String = "205_OT_0_L004"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L005 As String = "205_OT_0_L005"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L006 As String = "205_OT_0_L006"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L007 As String = "205_OT_0_L007"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L008 As String = "205_OT_0_L008"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L009 As String = "205_OT_0_L009"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L010 As String = "205_OT_0_L010"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L011 As String = "205_OT_0_L011"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L012 As String = "205_OT_0_L012"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L013 As String = "205_OT_0_L013"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L014 As String = "205_OT_0_L014"     'ディック(大阪)用:Lレベル
    Public Const DIC_WID_L015 As String = "205_OT_2_L015"     'ディック(埼玉)用:Lレベル
    Public Const DIC_WID_L016 As String = "205_OT_2_L016"     'ディック(群馬)用:Lレベル
#If True Then ' 届先自動変換対応(日立物流) 20170407 added by inoue 
    Public Const DIC_WID_L017 As String = "205_OT_2_L017"     'ディック(群馬)用:Lレベル
#End If




    Public Const UKIMA_WID_L001 As String = "203_OT_0_L001"     '浮間合成(大阪)用:Lレベル
    Public Const UKIMA_WID_L002 As String = "203_OT_0_L002"     '浮間合成(大阪)用:Lレベル
    Public Const UKIMA_WID_L003 As String = "203_OT_0_L003"     '浮間合成(大阪)用:Lレベル
    Public Const UKIMA_WID_L004 As String = "203_OT_0_L004"     '浮間合成(大阪)用:Lレベル
    Public Const UKIMA_WID_L005 As String = "203_OT_0_L005"     '浮間合成(大阪)用:Lレベル

    Public Const MITUI_WID_L001 As String = "204_OT_0_L001"     '三井化学(大阪)用:Lレベル
    Public Const MITUI_WID_L002 As String = "204_OT_0_L002"     '三井化学(大阪)用:Lレベル
    Public Const MITUI_WID_L003 As String = "204_OT_0_L003"     '三井化学(大阪)用:Lレベル
    Public Const MITUI_WID_L004 As String = "204_OT_0_L004"     '三井化学(大阪)用:Lレベル
    Public Const MITUI_WID_L005 As String = "204_OT_0_L005"     '三井化学(大阪)用:Lレベル
    Public Const MITUI_WID_L006 As String = "204_OT_0_L006"     '三井化学(大阪)用:Lレベル
    Public Const MITUI_WID_L007 As String = "204_OT_0_L007"     '三井化学(大阪)用:Lレベル
    Public Const MITUI_WID_L008 As String = "204_OT_0_L008"     '三井化学(大阪)用:Lレベル

    Public Const JPNCOMP_WID_L001 As String = "207_OT_0_L001"     'JC,JC大秦化工,アイカ(大阪)用:Lレベル
    Public Const JPNCOMP_WID_L002 As String = "207_OT_0_L002"     'JC,JC大秦化工,アイカ(大阪)用:Lレベル
    Public Const JPNCOMP_WID_L003 As String = "207_OT_0_L003"     'JC,JC大秦化工,アイカ(大阪)用:Lレベル
    Public Const JPNCOMP_WID_L004 As String = "207_OT_0_L004"     'JC,JC大秦化工,アイカ(大阪)用:Lレベル
    Public Const JPNCOMP_WID_L005 As String = "207_OT_0_L005"     'JC,JC大秦化工,アイカ(大阪)用:Lレベル
    Public Const JPNCOMP_WID_L006 As String = "207_OT_0_L006"     'JC,JC大秦化工,アイカ(大阪)用:Lレベル
    Public Const JPNCOMP_WID_L007 As String = "207_OT_0_L007"     'JC,JC大秦化工,アイカ(大阪)用:Lレベル

    Public Const NIK_WID_L001 As String = "101_OT_0_L001"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L002 As String = "101_OT_0_L002"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L003 As String = "101_OT_0_L003"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L004 As String = "101_OT_0_L004"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L005 As String = "101_OT_0_L005"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L006 As String = "101_OT_0_L006"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L007 As String = "101_OT_0_L007"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L008 As String = "101_OT_0_L008"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L009 As String = "101_OT_0_L009"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L010 As String = "101_OT_0_L010"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L011 As String = "101_OT_0_L011"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L012 As String = "101_OT_0_L012"     '日医工(千葉)用:Lレベル '2012.05.01 ADD
    Public Const NIK_WID_L013 As String = "101_OT_0_L013"     '日医工(千葉)用:Lレベル '2012.05.01 ADD

    Public Const SAKURA_WID_M001 As String = "401_OT_0_M001"  'サクラ(横浜)用:Mレベル
    Public Const SAKURA_WID_M002 As String = "401_OT_0_M002"  'サクラ(横浜)用:Mレベル
    Public Const SAKURA_WID_M003 As String = "401_OT_0_M003"  'サクラ(横浜)用:Mレベル

    Public Const DUPONT_WID_M001 As String = "403_OT_1_M001"  'デュポン(横浜)用:Mレベル
    Public Const DUPONT_WID_M002 As String = "403_OT_0_M002"  'デュポン(横浜)用:Mレベル
    Public Const DUPONT_WID_M003 As String = "403_OT_0_M003"  'デュポン(横浜)用:Mレベル(SFTP塗料,PVFM専用)
    Public Const DUPONT_WID_M004 As String = "403_OT_0_M004"    'デュポン(横浜)用:Mレベル(SFTP塗料,PVFM専用)

    Public Const NCGO_WID_M001 As String = "601_OT_1_M001"    '日本合成化学(名古屋)用:Mレベル

    Public Const DSP_WID_M001 As String = "206_OT_1_M001"     '大日本住友製薬(大阪)用:Mレベル

    Public Const TOHO_WID_M001 As String = "201_OT_1_M001"     '東邦化学(大阪)用:Mレベル
    Public Const TOHO_WID_M002 As String = "201_OT_0_M002"     '東邦化学(大阪)用:Mレベル
    Public Const TOHO_WID_M003 As String = "201_OT_0_M003"     '東邦化学(大阪)用:Mレベル

    Public Const DIC_WID_M001 As String = "205_OT_1_M001"     'ディック(大阪)用:Mレベル
    Public Const DIC_WID_M002 As String = "205_OT_0_M002"     'ディック(大阪)用:Mレベル
    Public Const DIC_WID_M003 As String = "205_OT_0_M003"     'ディック(大阪)用:Mレベル
    Public Const DIC_WID_M004 As String = "205_OT_0_M004"     'ディック(群馬)用:Mレベル (2013.04.11)要望番号2017

    Public Const UKIMA_WID_M001 As String = "203_OT_0_M001"     '浮間合成(大阪)用:Mレベル
    Public Const UKIMA_WID_M002 As String = "203_OT_0_M002"     '浮間合成(大阪)用:Mレベル
    Public Const UKIMA_WID_M003 As String = "203_OT_0_M003"     '浮間合成(大阪)用:Mレベル
    Public Const UKIMA_WID_M004 As String = "203_OT_0_M004"     '浮間合成(大阪)用:Mレベル
    '2012.04.17 要望番号1000 追加START(商品選択ワーニングの追加)
    Public Const UKIMA_WID_M005 As String = "203_OT_1_M005"     '浮間合成(大阪)用:Mレベル
    '2012.04.17 要望番号1000 追加END

    Public Const MITUI_WID_M001 As String = "204_OT_1_M001"     '三井化学(大阪)用:Mレベル

    Public Const DOW_WID_M001 As String = "201_OT_1_M001"     'ダウケミ(大阪)用:Mレベル
    Public Const DOW_WID_M002 As String = "201_OT_0_M002"     'ダウケミ(大阪)用:Mレベル

    Public Const JPNCOMP_WID_M001 As String = "207_OT_1_M001"     'JC,JC大秦化工,アイカ(大阪)用:Mレベル
    Public Const JPNCOMP_WID_M002 As String = "207_OT_0_M002"     'JC,JC大秦化工,アイカ(大阪)用:Mレベル

    Public Const NIK_WID_M001 As String = "101_OT_1_M001"     '日医工(千葉)用:Mレベル   '2012.05.01 ADD
    Public Const NIK_WID_M002 As String = "101_OT_0_M002"     '日医工(千葉)用:Mレベル   '2012.05.01 ADD

    Public Const DNS_WID_L001 As String = "504_OT_0_L001"     '大日精化(岩槻)用:Lレベル   '2012.05.28 ADD
    Public Const DNS_WID_L002 As String = "504_OT_0_L002"     '大日精化(岩槻)用:Lレベル   '2012.05.28 ADD
    Public Const DNS_WID_L003 As String = "504_OT_0_L003"     '大日精化(岩槻)用:Lレベル   '2012.05.28 ADD
    Public Const DNS_WID_L004 As String = "504_OT_0_L004"     '大日精化(岩槻)用:Lレベル   '2012.05.28 ADD
    Public Const DNS_WID_L005 As String = "504_OT_0_L005"     '大日精化(岩槻)用:Lレベル   '2012.05.28 ADD
    Public Const DNS_WID_L006 As String = "504_OT_0_L006"     '大日精化(岩槻)用:Lレベル   '2012.05.28 ADD
    Public Const DNS_WID_L007 As String = "504_OT_0_L007"     '大日精化(岩槻)用:Lレベル   '2012.05.28 ADD
    Public Const DNS_WID_M001 As String = "504_OT_0_M001"     '大日精化(岩槻)用:Mレベル   '2012.05.28 ADD
    Public Const DNS_WID_M002 As String = "504_OT_0_M002"     '大日精化(岩槻)用:Mレベル   '2012.05.28 ADD
    Public Const DNS_WID_M003 As String = "504_OT_0_M003"     '大日精化(岩槻)用:Mレベル   '2012.05.28 ADD
    Public Const DNS_WID_M004 As String = "504_OT_1_M004"     '大日精化(岩槻)用:Mレベル   '2012.05.28 ADD

    Public Const SMK_WID_L001 As String = "502_OT_0_L001"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L002 As String = "502_OT_0_L002"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L003 As String = "502_OT_0_L003"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L004 As String = "502_OT_0_L004"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L005 As String = "502_OT_0_L005"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L006 As String = "502_OT_0_L006"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L007 As String = "502_OT_0_L007"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L008 As String = "502_OT_0_L008"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L009 As String = "502_OT_0_L009"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L010 As String = "502_OT_0_L010"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L011 As String = "502_OT_0_L011"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L012 As String = "502_OT_0_L012"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L013 As String = "502_OT_0_L013"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_L014 As String = "502_OT_0_L014"     '住化カラー(岩槻)用:Lレベル
    Public Const SMK_WID_M001 As String = "502_OT_1_M001"     '住化カラー(岩槻)用:Mレベル
    Public Const SMK_WID_M002 As String = "502_OT_0_M002"     '住化カラー(岩槻)用:Mレベル
    Public Const SMK_WID_M003 As String = "502_OT_0_M003"     '住化カラー(岩槻)用:Mレベル

    Public Const GODO_WID_L001 As String = "506_OT_0_L001"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_L002 As String = "506_OT_0_L002"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_L003 As String = "506_OT_0_L003"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_L004 As String = "506_OT_0_L004"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_L005 As String = "506_OT_0_L005"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_L006 As String = "506_OT_0_L006"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_L007 As String = "506_OT_0_L007"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_L008 As String = "506_OT_0_L008"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_L009 As String = "506_OT_0_L009"     'ゴードー溶剤(岩槻)用:Lレベル   '2012.05.31 ADD
    Public Const GODO_WID_M001 As String = "506_OT_0_M001"     'ゴードー溶剤(岩槻)用:Mレベル   '2012.05.31 ADD

    Public Const DICUNSO_WID_L001 As String = "505_OT_0_L001"     'ディック物流(運送EDI)用:Lレベル   '2012.06.06 ADD
    Public Const DICUNSO_WID_L002 As String = "505_OT_0_L002"     'ディック物流(運送EDI)用:Lレベル   '2012.06.06 ADD
    Public Const DICUNSO_WID_L003 As String = "505_OT_0_L003"     'ディック物流(運送EDI)用:Lレベル   '2012.06.06 ADD
    Public Const DICUNSO_WID_L004 As String = "505_OT_0_L004"     'ディック物流(運送EDI)用:Lレベル   '2012.06.06 ADD
    Public Const DICUNSO_WID_L005 As String = "505_OT_0_L005"     'ディック物流(運送EDI)用:Lレベル   '2012.06.06 ADD
    Public Const DICUNSO_WID_L006 As String = "505_OT_0_L006"     'ディック物流(運送EDI)用:Lレベル   '2012.06.06 ADD
    Public Const DICUNSO_WID_L007 As String = "505_OT_0_L007"     'ディック物流(運送EDI)用:Lレベル   '2012.06.06 ADD
    Public Const DICUNSO_WID_M001 As String = "505_OT_1_M001"     'ディック物流(運送EDI)用:Mレベル   '2012.06.06 ADD
    '要望番号1246 馬野START
    Public Const DICUNSO_WID_M002 As String = "505_OT_0_M002"     'ディック物流(運送EDI)用:Mレベル   '2012.07.06 ADD
    '要望番号1246 馬野END

    Public Const KTK_WID_L001 As String = "551_OT_0_L001"     '関塗工(運送EDI)用:Lレベル   '2012.06.13 ADD
    Public Const KTK_WID_L002 As String = "551_OT_0_L002"     '関塗工(運送EDI)用:Lレベル   '2012.06.13 ADD
    Public Const KTK_WID_L003 As String = "551_OT_0_L003"     '関塗工(運送EDI)用:Lレベル   '2012.06.13 ADD
    Public Const KTK_WID_L004 As String = "551_OT_0_L004"     '関塗工(運送EDI)用:Lレベル   '2012.06.13 ADD
    Public Const KTK_WID_L005 As String = "551_OT_0_L005"     '関塗工(運送EDI)用:Lレベル   '2012.06.13 ADD
    Public Const KTK_WID_L006 As String = "551_OT_0_L006"     '関塗工(運送EDI)用:Lレベル   '2012.06.13 ADD
    Public Const KTK_WID_L007 As String = "551_OT_0_L007"     '関塗工(運送EDI)用:Lレベル   '2012.06.13 ADD
    Public Const KTK_WID_L008 As String = "551_OT_0_L008"     '関塗工(運送EDI)用:Lレベル   '2013.06.19 ADD
    Public Const KTK_WID_L009 As String = "551_OT_0_L009"     '関塗工(運送EDI)用:Lレベル   '2013.06.24 ADD
    Public Const KTK_WID_M001 As String = "551_OT_1_M001"     '関塗工(運送EDI)用:Mレベル   '2012.06.13 ADD

    Public Const SNZ_WID_L001 As String = "301_OT_0_L001"     '篠崎運送用:Lレベル   '2012.06.25 ADD
    Public Const SNZ_WID_L002 As String = "301_OT_0_L002"     '篠崎運送用:Lレベル   '2012.06.25 ADD
    Public Const SNZ_WID_L003 As String = "301_OT_0_L003"     '篠崎運送用:Lレベル   '2012.06.25 ADD
    Public Const SNZ_WID_M001 As String = "301_OT_1_M001"     '篠崎運送用:Mレベル   '2012.06.25 ADD
    Public Const SNZ_WID_M002 As String = "301_OT_0_M002"     '篠崎運送用:Mレベル   '2012.06.25 ADD

    Public Const BYK_WID_L001 As String = "102_OT_0_L001"     'ビックケミー用:Lレベル   '2012.07.11 ADD
    Public Const BYK_WID_L002 As String = "102_OT_0_L002"     'ビックケミー用:Lレベル   '2012.07.11 ADD
    Public Const BYK_WID_L003 As String = "102_OT_0_L003"     'ビックケミー用:Lレベル   '2012.07.11 ADD
    Public Const BYK_WID_L004 As String = "102_OT_0_L004"     'ビックケミー用:Lレベル   '2012.07.11 ADD
    Public Const BYK_WID_L005 As String = "102_OT_0_L005"     'ビックケミー用:Lレベル   '2012.07.11 ADD
    Public Const BYK_WID_L006 As String = "102_OT_0_L006"     'ビックケミー用:Lレベル   '2012.07.11 ADD
    Public Const BYK_WID_L007 As String = "102_OT_0_L007"     'ビックケミー用:Lレベル   '2012.07.11 ADD
    Public Const BYK_WID_L008 As String = "102_OT_0_L008"     'ビックケミー用:Lレベル   '2013.10.02 ADD
    Public Const BYK_WID_M001 As String = "102_OT_1_M001"     'ビックケミー用:Mレベル   '2012.07.11 ADD
    Public Const BYK_WID_M002 As String = "102_OT_0_M002"     'ビックケミー用:Mレベル   '2012.07.11 ADD
    Public Const BYK_WID_M003 As String = "102_OT_0_M003"     'ビックケミー用:Mレベル   '2012.07.27 ADD
    Public Const BYK_WID_M004 As String = "102_OT_0_M004"     'ビックケミー用:Mレベル   '2012.07.27 ADD

    Public Const FJF_WID_L001 As String = "103_OT_0_L001"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L002 As String = "103_OT_0_L002"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L003 As String = "103_OT_0_L003"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L004 As String = "103_OT_0_L004"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L005 As String = "103_OT_0_L005"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L006 As String = "103_OT_0_L006"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L007 As String = "103_OT_0_L007"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L008 As String = "103_OT_0_L008"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L009 As String = "103_OT_0_L009"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L010 As String = "103_OT_0_L010"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L011 As String = "103_OT_0_L011"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L012 As String = "103_OT_0_L012"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L013 As String = "103_OT_0_L013"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_L014 As String = "103_OT_0_L014"     '富士フイルム用:Lレベル   '2012.08.14 ADD
    Public Const FJF_WID_M001 As String = "103_OT_1_M001"     '富士フイルム用:Mレベル   '2012.08.14 ADD
    Public Const FJF_WID_M002 As String = "103_OT_0_M002"     '富士フイルム用:Mレベル   '2012.08.14 ADD
    Public Const FJF_WID_M003 As String = "103_OT_0_M003"     '富士フイルム用:Mレベル   '2012.08.14 ADD
    Public Const FJF_WID_M004 As String = "103_OT_0_M004"     '富士フイルム用:Mレベル   '2012.08.14 ADD
    Public Const FJF_WID_M005 As String = "103_OT_0_M005"     '富士フイルム用:Mレベル   '2012.08.14 ADD
    Public Const FJF_WID_M006 As String = "103_OT_0_M006"     '富士フイルム用:Mレベル   '2012.08.14 ADD

    Public Const JT_WID_L001 As String = "111_OT_0_L001"        'ジェイティ物流用:Lレベル   '2012.09.05 ADD
    Public Const JT_WID_L002 As String = "111_OT_0_L002"        'ジェイティ物流用:Lレベル   '2012.09.05 ADD
    Public Const JT_WID_L003 As String = "111_OT_0_L003"        'ジェイティ物流用:Lレベル   '2012.09.05 ADD
    Public Const JT_WID_L004 As String = "111_OT_0_L004"        'ジェイティ物流用:Lレベル   '2012.09.05 ADD
    Public Const JT_WID_L005 As String = "111_OT_0_L005"        'ジェイティ物流用:Lレベル   '2012.09.05 ADD
    Public Const JT_WID_L006 As String = "111_OT_0_L006"        'ジェイティ物流用:Lレベル   '2012.09.05 ADD
    Public Const JT_WID_L007 As String = "111_OT_0_L007"        'ジェイティ物流用:Lレベル   '2012.09.05 ADD
    Public Const JT_WID_M001 As String = "111_OT_1_M001"        'ジェイティ物流用:Mレベル   '2012.09.05 ADD
    Public Const JT_WID_M002 As String = "111_OT_0_M002"        'ジェイティ物流用:Mレベル   '2012.09.05 ADD
    Public Const JT_WID_M003 As String = "111_OT_0_M003"        'ジェイティ物流用:Mレベル   '2012.09.05 ADD
    Public Const JT_WID_M004 As String = "111_OT_0_M004"        'ジェイティ物流用:Mレベル   '2012.09.05 ADD

    Public Const MHM_WID_L001 As String = "109_OT_0_L001"        '美浜用:Lレベル   '2012.09.05 ADD
    Public Const MHM_WID_L002 As String = "109_OT_0_L002"        '美浜用:Lレベル   '2012.09.05 ADD
    Public Const MHM_WID_L003 As String = "109_OT_0_L003"        '美浜用:Lレベル   '2012.09.05 ADD
    Public Const MHM_WID_L004 As String = "109_OT_0_L004"        '美浜用:Lレベル   '2012.09.05 ADD
    Public Const MHM_WID_L005 As String = "109_OT_0_L005"        '美浜用:Lレベル   '2012.09.05 ADD
    Public Const MHM_WID_L006 As String = "109_OT_0_L006"        '美浜用:Lレベル   '2012.09.05 ADD
    Public Const MHM_WID_L007 As String = "109_OT_0_L007"        '美浜用:Lレベル   '2012.09.05 ADD
    Public Const MHM_WID_M001 As String = "109_OT_1_M001"        '美浜用:Mレベル   '2012.09.05 ADD
    Public Const MHM_WID_M002 As String = "109_OT_0_M002"        '美浜用:Mレベル   '2012.09.05 ADD
    Public Const MHM_WID_M003 As String = "109_OT_0_M003"        '美浜用:Mレベル   '2012.09.05 ADD
    Public Const MHM_WID_M004 As String = "109_OT_0_M004"        '美浜用:Mレベル   '2012.09.05 ADD

    Public Const NSN_WID_L001 As String = "104_OT_0_L001"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L002 As String = "104_OT_0_L002"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L003 As String = "104_OT_0_L003"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L004 As String = "104_OT_0_L004"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L005 As String = "104_OT_0_L005"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L006 As String = "104_OT_0_L006"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L007 As String = "104_OT_0_L007"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L008 As String = "104_OT_0_L008"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L009 As String = "104_OT_0_L009"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L010 As String = "104_OT_0_L010"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L011 As String = "104_OT_0_L011"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L012 As String = "104_OT_0_L012"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_L013 As String = "104_OT_0_L013"     '日産物流用:Lレベル   '2012.10.11 ADD
    Public Const NSN_WID_M001 As String = "104_OT_1_M001"     '日産物流用:Mレベル   '2012.10.11 ADD
    Public Const NSN_WID_M002 As String = "104_OT_0_M002"     '日産物流用:Mレベル   '2012.10.11 ADD

    Public Const UTI_WID_L001 As String = "112_OT_0_L001"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L002 As String = "112_OT_0_L002"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L003 As String = "112_OT_0_L003"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L004 As String = "112_OT_0_L004"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L005 As String = "112_OT_0_L005"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L006 As String = "112_OT_0_L006"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L007 As String = "112_OT_0_L007"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L008 As String = "112_OT_0_L008"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L009 As String = "112_OT_0_L009"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L010 As String = "112_OT_0_L010"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L011 As String = "112_OT_0_L011"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L012 As String = "112_OT_0_L012"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_L013 As String = "112_OT_0_L013"     'ユーティーアイ用:Lレベル   '2012.10.11 ADD
    Public Const UTI_WID_M001 As String = "112_OT_1_M001"     'ユーティーアイ用:Mレベル   '2012.10.11 ADD
    Public Const UTI_WID_M002 As String = "112_OT_0_M002"     'ユーティーアイ用:Mレベル   '2012.10.11 ADD

    Public Const TOR_WID_L001 As String = "106_OT_0_L001"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L002 As String = "106_OT_0_L002"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L003 As String = "106_OT_0_L003"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L004 As String = "106_OT_0_L004"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L005 As String = "106_OT_0_L005"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L006 As String = "106_OT_0_L006"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L007 As String = "106_OT_0_L007"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L008 As String = "106_OT_0_L008"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L009 As String = "106_OT_0_L009"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L010 As String = "106_OT_0_L010"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L011 As String = "106_OT_0_L011"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L012 As String = "106_OT_0_L012"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_L013 As String = "106_OT_0_L013"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_M001 As String = "106_OT_1_M001"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_M002 As String = "106_OT_0_M002"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_M003 As String = "106_OT_0_M003"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.10.15 ADD
    Public Const TOR_WID_M004 As String = "106_OT_0_M004"     '東レダウ・日通（東レダウ）用:Lレベル   '2012.11.20 ADD

    Public Const SNK_WID_L001 As String = "001_OT_0_L001"        'センコー用:Lレベル   '2012.10.22 ADD
    Public Const SNK_WID_L002 As String = "001_OT_0_L002"        'センコー用:Lレベル   '2012.10.22 ADD
    Public Const SNK_WID_L003 As String = "001_OT_0_L003"        'センコー用:Lレベル   '2012.10.22 ADD
    Public Const SNK_WID_L004 As String = "001_OT_0_L004"        'センコー用:Lレベル   '2012.10.22 ADD
    Public Const SNK_WID_L005 As String = "001_OT_0_L005"        'センコー用:Lレベル   '2012.10.22 ADD
    Public Const SNK_WID_M001 As String = "001_OT_1_M001"        'センコー用:Mレベル   '2012.10.22 ADD
    Public Const SNK_WID_M002 As String = "001_OT_0_M002"        'センコー用:Mレベル   '2012.10.22 ADD
    Public Const SNK_WID_M003 As String = "001_OT_0_M003"        'センコー用:Mレベル   '2012.10.22 ADD
    Public Const SNK_WID_M004 As String = "001_OT_0_M004"        'センコー用:Mレベル   '2012.10.22 ADD
    Public Const SNK_WID_M005 As String = "001_OT_0_M005"        'センコー用:Mレベル   '2012.10.22 ADD
    Public Const SNK_WID_M006 As String = "001_OT_0_M006"        'センコー用:Mレベル   '2012.10.22 ADD

    Public Const ASH_WID_L013 As String = "108_OT_0_L013"        '旭化成ケミカルズ・旭化成イーマテリアルズ用:Lレベル   '2012.11.26 ADD
    Public Const ASH_WID_L005 As String = "108_OT_0_L005"        '旭化成ケミカルズ・旭化成イーマテリアルズ用:Lレベル   '2012.11.26 ADD
    Public Const ASH_WID_L014 As String = "108_OT_0_L014"        '旭化成ケミカルズ・旭化成イーマテリアルズ用:Lレベル   '2012.11.26 ADD
    Public Const ASH_WID_M001 As String = "108_OT_1_M001"        '旭化成ケミカルズ・旭化成イーマテリアルズ用:Mレベル   '2012.11.26 ADD
    Public Const ASH_WID_M002 As String = "108_OT_0_M002"        '旭化成ケミカルズ・旭化成イーマテリアルズ用:Mレベル   '2012.11.26 ADD

    Public Const LNZ_WID_L001 As String = "110_OT_0_L001"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L002 As String = "110_OT_0_L002"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L003 As String = "110_OT_0_L003"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L004 As String = "110_OT_0_L004"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L005 As String = "110_OT_0_L005"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L006 As String = "110_OT_0_L006"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L007 As String = "110_OT_0_L007"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L008 As String = "110_OT_0_L008"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L009 As String = "110_OT_0_L009"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L010 As String = "110_OT_0_L010"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L011 As String = "110_OT_0_L011"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L012 As String = "110_OT_0_L012"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L013 As String = "110_OT_0_L013"       'ロンザ用:Lレベル   '2012.11.28 ADD
    Public Const LNZ_WID_L014 As String = "110_OT_0_L014"       'ロンザ用:Lレベル   '2013.02.04 ADD
    Public Const LNZ_WID_M001 As String = "110_OT_1_M001"       'ロンザ用:Mレベル   '2012.11.28 ADD
    Public Const LNZ_WID_M002 As String = "110_OT_0_M002"       'ロンザ用:Mレベル   '2012.11.28 ADD
    Public Const LNZ_WID_M003 As String = "110_OT_0_M003"       'ロンザ用:Mレベル   '2012.11.28 ADD
    Public Const LNZ_WID_M004 As String = "110_OT_0_M004"       'ロンザ用:Mレベル   '2012.11.28 ADD

    Public Const BP_WID_L001 As String = "501_OT_0_L001"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L002 As String = "501_OT_0_L002"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L003 As String = "501_OT_0_L003"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L004 As String = "501_OT_0_L004"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L005 As String = "501_OT_0_L005"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L006 As String = "501_OT_0_L006"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L007 As String = "501_OT_0_L007"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L008 As String = "501_OT_0_L008"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L009 As String = "501_OT_0_L009"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L010 As String = "501_OT_0_L010"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L011 As String = "501_OT_0_L011"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L012 As String = "501_OT_0_L012"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L013 As String = "501_OT_0_L013"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_L014 As String = "501_OT_0_L014"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_M001 As String = "501_OT_1_M001"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_M002 As String = "501_OT_0_M002"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD
    Public Const BP_WID_M003 As String = "501_OT_0_M003"     'ビーピー・カストロール(岩槻)用:Lレベル   '2012.12.19 ADD

    Public Const NKS_WID_L001 As String = "208_OT_0_L001"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_L002 As String = "208_OT_0_L002"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_L003 As String = "208_OT_0_L003"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_L004 As String = "208_OT_0_L004"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_L005 As String = "208_OT_0_L005"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_L006 As String = "208_OT_0_L006"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_L007 As String = "208_OT_0_L007"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_L008 As String = "208_OT_0_L008"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_L009 As String = "208_OT_0_L009"     '日興産業(大阪)用:Lレベル   '2013.01.07 ADD
    Public Const NKS_WID_M001 As String = "208_OT_1_M001"     '日興産業(大阪)用:Mレベル   '2013.01.07 ADD

    Public Const CSO_WID_L001 As String = "502_OT_0_L001"     'チッソ(千葉)用:Lレベル   '2013.01.23 ADD
    Public Const CSO_WID_L002 As String = "502_OT_0_L002"     'チッソ(千葉)用:Lレベル   '2013.01.23 ADD
    Public Const CSO_WID_L003 As String = "502_OT_0_L003"     'チッソ(千葉)用:Lレベル   '2013.01.23 ADD
    Public Const CSO_WID_L004 As String = "502_OT_0_L004"     'チッソ(千葉)用:Lレベル   '2013.01.23 ADD
    Public Const CSO_WID_L005 As String = "502_OT_0_L005"     'チッソ(千葉)用:Lレベル   '2013.01.23 ADD
    Public Const CSO_WID_L006 As String = "502_OT_0_L006"     'チッソ(千葉)用:Lレベル   '2013.01.23 ADD
    Public Const CSO_WID_L007 As String = "502_OT_0_L007"     'チッソ(千葉)用:Lレベル   '2013.01.23 ADD
    Public Const CSO_WID_L008 As String = "502_OT_0_L008"     'チッソ(千葉)用:Lレベル   '2013.01.23 ADD
    Public Const CSO_WID_M001 As String = "502_OT_1_M001"     'チッソ(千葉)用:Mレベル   '2013.01.23 ADD
    Public Const CSO_WID_M002 As String = "502_OT_0_M002"     'チッソ(千葉)用:Mレベル   '2013.01.23 ADD
    Public Const CSO_WID_M003 As String = "502_OT_0_M003"     'チッソ(千葉)用:Mレベル   '2013.01.23 ADD
    Public Const CSO_WID_M004 As String = "502_OT_0_M004"     'チッソ(千葉)用:Mレベル   '2013.01.23 ADD

    Public Const HON_WID_L001 As String = "105_OT_0_L001"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L002 As String = "105_OT_0_L002"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L003 As String = "105_OT_0_L003"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L004 As String = "105_OT_0_L004"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L005 As String = "105_OT_0_L005"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L006 As String = "105_OT_0_L006"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L007 As String = "105_OT_0_L007"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L008 As String = "105_OT_0_L008"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L009 As String = "105_OT_0_L009"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L010 As String = "105_OT_0_L010"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L011 As String = "105_OT_0_L011"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L012 As String = "105_OT_0_L012"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L013 As String = "105_OT_0_L013"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_L014 As String = "105_OT_0_L014"     'ハネウェル(岩槻)用:Lレベル
    Public Const HON_WID_M001 As String = "105_OT_1_M001"     'ハネウェル(岩槻)用:Mレベル
    Public Const HON_WID_M002 As String = "105_OT_0_M002"     'ハネウェル(岩槻)用:Mレベル
    Public Const HON_WID_M003 As String = "105_OT_0_M003"     'ハネウェル(岩槻)用:Mレベル

    Public Const STD_WID_L001 As String = "000_OT_0_L001"     '標準用Lレベル
    Public Const STD_WID_L002 As String = "000_OT_0_L002"     '標準用Lレベル
    Public Const STD_WID_L003 As String = "000_OT_0_L003"     '標準用Lレベル
    Public Const STD_WID_L004 As String = "000_OT_0_L004"     '標準用Lレベル
    Public Const STD_WID_L005 As String = "000_OT_0_L005"     '標準用Lレベル
    Public Const STD_WID_L006 As String = "000_OT_0_L006"     '標準用Lレベル
    Public Const STD_WID_L007 As String = "000_OT_0_L007"     '標準用Lレベル
    Public Const STD_WID_L008 As String = "000_OT_0_L008"     '標準用Lレベル
    Public Const STD_WID_L009 As String = "000_OT_0_L009"     '標準用Lレベル

    Public Const STD_WID_L010 As String = "000_OT_2_L010"     '標準内フィルメ用Lレベル

    Public Const ATS_WID_L001 As String = "114_OT_0_L001"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_L002 As String = "114_OT_0_L002"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_L003 As String = "114_OT_0_L003"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_L005 As String = "114_OT_0_L005"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_L006 As String = "114_OT_0_L006"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_L007 As String = "114_OT_0_L007"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_L008 As String = "114_OT_0_L008"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_L009 As String = "114_OT_0_L009"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_L010 As String = "114_OT_2_L010"     'アクタス(千葉)用Lレベル
    Public Const ATS_WID_M001 As String = "114_OT_1_M001"     'アクタス(千葉)用:Mレベル

    Public Const DSPGKY_WID_L001 As String = "130_OT_0_L001"     'アクタス(千葉)用Lレベル
    Public Const DSPGKY_WID_M001 As String = "130_OT_1_M001"     'アクタス(千葉)用:Mレベル



    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "EDI管理番号"
    Public Const EXCEL_COLTITLE_SEMIEDI As String = "受信ファイル名"

    '要望番号1282 追加START
    ''' <summary>
    ''' EDI出荷(大)届先コード桁数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEST_CD_LENGTH As Integer = 15
    '要望番号1282 追加END

    '要望番号1282 追加START
    ''' <summary>
    ''' INSERTFLGの値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FLG_OFF As Integer = 0
    Public Const FLG_ON As Integer = 1

    '要望番号1282 追加END


#If True Then 'フィルメニッヒ セミEDI対応  20160926 added inoue

    ''' <summary>
    ''' ワーニングIDマスタフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Class WARNING_ID_MST_FLG

        ''' <summary>
        ''' 未指定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NONE As String = "0"

        ''' <summary>
        ''' 商品マスタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const M_GOODS As String = "1"


        ''' <summary>
        ''' 届先マスタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const M_DEST As String = "2"

    End Class

#End If


#End Region

#Region "EDI荷主INDEX"
    'イベント種別
    Public Enum EdiCustIndex As Integer

        Sakura00237_00 = 1               'サクラファインテック(横浜)
        Ncgo32516_00 = 57                '日本合成化学(名古屋)
        Dupont00089_00 = 18              'デュポン(テフロン)(千葉)→(横浜)移送  '2012.04.11 ADD
        Dupont00295_00 = 37              'デュポン(横浜)
        Dupont00331_00 = 87              'デュポン(DCSE)(横浜)
        Dupont00331_02 = 88              'デュポン(ABS)(横浜)
        Dupont00331_03 = 90              'デュポン()(横浜)
        Dupont00588_00 = 89              'デュポン(SFTP塗料)(横浜)
        Dupont00300_00 = 36              'デュポン(EP:大阪)
        Dupont00689_00 = 81              'デュポン(PVFM:大阪)
        Dupont00700_00 = 86              'デュポン(DCSE:大阪)
        DupontChb00187_00 = 47           'デュポン(ブタサイト:千葉)
        DupontChb00188_00 = 35           'デュポン(EP:千葉)
        DupontChb00587_00 = 76           'デュポン(農業:千葉)
        DupontChb00589_00 = 60           'デュポン(特殊化学品:千葉)
        DupontChb00688_00 = 79           'デュポン(電子材料事業:千葉)
        DupontChb00689_00 = 80           'デュポン(PVFM:千葉)

        Dsp00293_00 = 82                 '大日本住友製薬(大阪)
        Dspah08251_00 = 85               '大日本住友製薬(動物薬)(大阪)
        '2012.05.15 追加START
        Toho00275_00 = 65                '東邦化学(大阪)
        Toho00347_00 = 111               '東邦化学(千葉)　'長浦倉庫/袖ヶ浦倉庫/市原倉庫  ADD 2017/02/23
        UkimaOsk00856_00 = 91            '浮間合成(大阪)
        Mitui00369_00 = 17               '三井化学(大阪)
        Mitui00375_00 = 21               '三井化学(日本特殊塗料)(大阪)
        Dow00109_00 = 43                 'ダウケミ(大阪)
        DowTaka00109_01 = 44             'ダウケミ(大阪・高石)
        DicOsk00010_00 = 28              'ディック（大阪)

        Jc31022_00 = 34                  'ジャパンコンポジット（大阪)
        Jc31022_01 = 42                  'ジャパンコンポジット（大阪)大泰化工
        Aika31023_00 = 40                'アイカ工業（大阪)
        Nik00171_00 = 92                 '日医工(千葉) '2012.05.01 ADD
        '2012.05.15 追加END

        '2012.05.28 追加START
        UkimaItk00856_00 = 3            '（岩槻）浮間合成
        Godo00950_00 = 4                '（岩槻）ゴードー溶剤
        Smk00952_00 = 6                 '（岩槻）住化カラー

        Dns20000_00 = 9                 '（岩槻）大日精化（東京製造）
        Dns20000_01 = 10                '（岩槻）大日精化（化成品）
        Dns20000_02 = 24                '（岩槻）大日精化（洗顔２課）

        DicItk00899_00 = 11             '（岩槻）ディック物流埼玉
        DicItk00899_01 = 41             '（岩槻）ディック物流埼玉
        DicItk10001_00 = 5              '（岩槻）ディック物流春日部
        DicItk10002_00 = 8              '（岩槻）ディック物流千葉－岩槻分
        DicItk10003_00 = 13             '（岩槻）ディック物流埼玉
        DicItk10007_00 = 39             '（岩槻）ディック物流東京営業所
        DicItk10008_00 = 12             '（岩槻）ディック物流館林
        DicItk10009_00 = 68             '（岩槻）ディック物流埼玉リナブルー
        'DicItkXXXXX_XX = ?              '（岩槻）ディック（荷主特定不能分）
        DicItk10005_00 = 7              '（岩槻）ディック共同配送（505倉庫）
        '2012.05.28 追加END

        DicKkb10001_00 = 62             '（春日部）DIC春日部
        DicKkb10001_03 = 63             '（春日部）DIC春日部顔料
        DicKkb10012_00 = 69             '（春日部）DIC春日部関東工場
        DicKkb10013_00 = 64             '（春日部）DIC春日部他社物流

        KtkKkb10009_00 = 67             '（春日部）関塗工（大宝）

        SnzGnm00021_00 = 58             '（群馬）篠崎運輸
        DicGnm00039_00 = 31             '（群馬）ディック物流群馬日パケ
        DicGnm00072_00 = 26             '（群馬）ディック物流群馬トートタンク
        DicGnm00076_00 = 25             '（群馬）ディック物流群馬

        BykChb00266_00 = 93             '（千葉）ビックケミー
        BykChb00266_01 = 94             '（千葉）ビックケミー(テツタニ)
        BykChb00266_02 = 95             '（千葉）ビックケミー(長瀬)
        '2013.07.30 追加START
        BykChb00729_00 = 100            '（千葉）ビックケミー(エカルト)
        '2013.07.30 追加END

        FjfChb00195_00 = 96             '（千葉）富士フイルム
        JtChb00444_00 = 70              '（千葉）ジェイティ物流
        MhmChb00117_00 = 78             '（千葉）美浜株式会社
        LnzChb00182_00 = 84             '（千葉）ロンザジャパン
        SmkChb00002_00 = 72             '（千葉）住化カラー(市原)
        SmkChb00404_00 = 73             '（千葉）住化カラー(市原)
        MituiChb00456_00 = 45           '（千葉）三井化学
        NsnChb00145_00 = 32             '（千葉）日産物流
        UtiChb00625_00 = 22             '（千葉）ユーティーアイ
        TorChb00041_00 = 23             '（千葉）東レダウ
        TorChb00637_00 = 59             '（千葉）日通（東レダウ

        AshChb00070_00 = 61             '（千葉）旭化成ケミカルズ
        AshChb00071_00 = 33             '（千葉）旭化成イーマテリアルズ

        SnkHns10005_00 = 97             '（本社）センコー

        BP00023_00 = 77                 '（岩槻）ビーピー・カストロール

        NksOsk33224_00 = 98             '（大阪）日興産業

        ChissoChb00067_00 = 2           '（千葉）チッソ

        DicChbYuso10010_00 = 46         '（千葉）ディック千葉輸送

        HonChb00630_00 = 48             '（千葉）ハネウェル（市原：市原）
        HonChb00632_00 = 56             '（千葉）ハネウェル（市原：市原Ｂ＆Ｊ）
        HonChb10630_00 = 49             '（千葉）ハネウェル（大阪：兵機）
        HonChb20630_00 = 50             '（千葉）ハネウェル（名古屋：由良）
        HonChb30630_00 = 51             '（千葉）ハネウェル（北海道：三和）
        HonChb40630_00 = 52             '（千葉）ハネウェル（九州：博多）
        HonChb50630_00 = 53             '（千葉）ハネウェル（横浜：舟津） 

        DicChbBC00010_00 = 102          '（千葉）ディック物流千葉
        '2018/03/26 セミEDI_千葉ITW_新規登録対応 Annen add start
        ITW00750_00 = 121               '（千葉）ITW
        '2018/03/26 セミEDI_千葉ITW_新規登録対応 Annen add end
        TORAY00951_00 = 125             '千葉東レ

    End Enum

#End Region

#Region "Method"

#Region "共通入力チェック"

#Region "EDI出荷(大)"

#Region "出荷管理番号"

    ''' <summary>
    ''' 空白チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function OutkaCtlNoCheck(ByVal dt As DataTable) As Boolean

        Dim outkaCtlNo As String = dt.Rows(0).Item("OUTKA_CTL_NO").ToString()

        If String.IsNullOrEmpty(outkaCtlNo) = False Then

            Return False
        End If

        Return True

    End Function

#End Region

#Region "出荷報告有無"

    ''' <summary>
    ''' 値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function OutkaHokokuYnCheck(ByVal dt As DataTable) As Boolean

        Dim hokokuF As Integer = Convert.ToInt32(dt.Rows(0).Item("OUTKAHOKOKU_YN"))

        If hokokuF = 0 OrElse hokokuF = 1 Then

        Else
            Return False
        End If

        Return True

    End Function

#End Region

#Region "出荷予定日"

    ''' <summary>
    ''' 空白,日付チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function OutkaPlanDateCheck(ByVal dt As DataTable) As Boolean

        Dim outkaDate As String = dt.Rows(0).Item("OUTKA_PLAN_DATE").ToString()

        If String.IsNullOrEmpty(outkaDate) = True Then

            Return False
        Else

            If outkaDate.Length < 8 Then

                Return False
            Else
                outkaDate = String.Concat(outkaDate.Substring(0, 4), "/", outkaDate.Substring(4, 2), "/", outkaDate.Substring(6, 2))

                If IsDate(outkaDate) = False Then

                    Return False
                Else

                End If

            End If

        End If

        Return True

        'If IsDate(DateTime.ParseExact(outkaDate, "yyyyMMdd", Nothing)) = False Then

        '    Return False
        'Else

        'End If

    End Function

#End Region

#Region "出庫日"

    ''' <summary>
    ''' 空白,日付チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function OutkoDateCheck(ByVal dt As DataTable) As Boolean

        Dim outkoDate As String = dt.Rows(0).Item("OUTKO_DATE").ToString()

        If String.IsNullOrEmpty(outkoDate) = True Then

            Return False
        Else

            If outkoDate.Length < 8 Then

                Return False
            Else
                outkoDate = String.Concat(outkoDate.Substring(0, 4), "/", outkoDate.Substring(4, 2), "/", outkoDate.Substring(6, 2))

                If IsDate(outkoDate) = False Then

                    Return False
                Else

                End If

            End If

        End If

        Return True

        'If IsDate(DateTime.ParseExact(outkoDate, "yyyyMMdd", Nothing)) = False Then

        '    Return False
        'Else

        'End If

    End Function

#End Region

#Region "出荷予定日+出庫日"

    ''' <summary>
    ''' 日付大小チェック(出庫日 > 出荷予定日)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function OutkaPlanLargeSmallCheck(ByVal dt As DataTable) As Boolean

        Dim outkaDate As Date = DateTime.ParseExact(dt.Rows(0).Item("OUTKA_PLAN_DATE").ToString(), "yyyyMMdd", Nothing)
        Dim outkoDate As Date = DateTime.ParseExact(dt.Rows(0).Item("OUTKO_DATE").ToString(), "yyyyMMdd", Nothing)

        If Date.Compare(outkaDate, outkoDate) < 0 Then

            Return False

        End If

        Return True

    End Function

#End Region

#Region "納入予定日"

    ''' <summary>
    ''' 空白、日付チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function arrPlanDateCheck(ByVal dt As DataTable) As Boolean

        Dim arrPlanDate As String = dt.Rows(0).Item("ARR_PLAN_DATE").ToString()

        If String.IsNullOrEmpty(arrPlanDate) = True Then

            Return False
        Else

            If arrPlanDate.Length < 8 Then

                Return False
            Else
                arrPlanDate = String.Concat(arrPlanDate.Substring(0, 4), "/", arrPlanDate.Substring(4, 2), "/", arrPlanDate.Substring(6, 2))

                If IsDate(arrPlanDate) = False Then

                    Return False
                Else

                End If

            End If

        End If

        Return True

        'If IsDate(DateTime.ParseExact(arrPlanDate, "yyyyMMdd", Nothing)) = False Then

        '    Return False
        'Else

        'End If

    End Function

#End Region

#Region "出荷報告日"

    ''' <summary>
    ''' 空白、日付チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function HokokuDateCheck(ByVal dt As DataTable) As Boolean

        Dim hokokuDate As String = dt.Rows(0).Item("HOKOKU_DATE").ToString()

        If String.IsNullOrEmpty(hokokuDate) = True Then

        Else

            If hokokuDate.Length < 8 Then

                Return False
            Else
                hokokuDate = String.Concat(hokokuDate.Substring(0, 4), "/", hokokuDate.Substring(4, 2), "/", hokokuDate.Substring(6, 2))

                If IsDate(hokokuDate) = False Then

                    Return False
                Else

                End If

            End If

        End If

        Return True

        'If IsDate(DateTime.ParseExact(hokokuDate, "yyyyMMdd", Nothing)) = False Then

        '    Return False
        'Else

        'End If

    End Function

#End Region

#Region "荷主コード(大)"

    ''' <summary>
    ''' 空白チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function CustCdLCheck(ByVal dt As DataTable) As Boolean

        Dim custCdL As String = dt.Rows(0).Item("CUST_CD_L").ToString()

        If String.IsNullOrEmpty(custCdL) = True Then

            Return False
        End If

        Return True

    End Function

#End Region

#Region "荷主コード(中)"

    ''' <summary>
    ''' 空白チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function CustCdMCheck(ByVal dt As DataTable) As Boolean

        Dim custCdM As String = dt.Rows(0).Item("CUST_CD_M").ToString()

        If String.IsNullOrEmpty(custCdM) = True Then

            Return False
        End If

        Return True

    End Function

#End Region

#Region "送り状作成有無"

    ''' <summary>
    ''' 値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function DenpYnCheck(ByVal dt As DataTable) As Boolean

        Dim hokokuF As Integer = Convert.ToInt32(dt.Rows(0).Item("DENP_YN"))

        If hokokuF.Equals(String.Empty) = True Then

        Else

            If hokokuF = 0 OrElse hokokuF = 1 Then

            Else

                Return False
            End If

        End If

        Return True

    End Function

#End Region

#End Region

#Region "EDI出荷(中)"

#Region "引当単位区分"

    ''' <summary>
    ''' 値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function AlctdKbCheck(ByVal dt As DataTable) As Boolean

        Dim max As Integer = dt.Rows.Count - 1
        Dim AlctdKb As String = String.Empty

        For i As Integer = 0 To max

            AlctdKb = dt.Rows(i).Item("ALCTD_KB").ToString()

            If AlctdKb.Equals("01") = True OrElse AlctdKb.Equals("02") = True OrElse AlctdKb.Equals("04") = True Then

            Else
                Return False
            End If

            Return True

        Next

    End Function

#End Region

#Region "温度区分 + 便区分"

    ''' <summary>
    ''' 値チェック
    ''' </summary>
    ''' <param name="dtL"></param>
    ''' <param name="dtM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function OndoBinKbCheck(ByVal dtL As DataTable, ByVal dtM As DataTable) As Boolean

        Dim max As Integer = dtM.Rows.Count - 1
        Dim binKb As String = dtL.Rows(0).Item("BIN_KB").ToString()
        Dim ondoKb As String = String.Empty

        For i As Integer = 0 To max

            ondoKb = dtM.Rows(i).Item("ONDO_KB").ToString()

            If String.IsNullOrEmpty(binKb) OrElse binKb.Equals("91") OrElse binKb.Equals("92") = True Then

            Else
                If ondoKb.Equals("10") OrElse ondoKb.Equals("20") = True Then
                    Return False
                End If

            End If

        Next

        Return True

    End Function

#End Region

#Region "出荷端数"

    ''' <summary>
    ''' マイナスチェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function OutkaHasuCheck(ByVal dt As DataTable) As Boolean

        Dim max As Integer = dt.Rows.Count - 1
        Dim outkaHasu As Decimal = 0

        For i As Integer = 0 To max

            outkaHasu = Convert.ToDecimal(dt.Rows(i).Item("OUTKA_HASU"))

            If outkaHasu < 0 Then

                Return False
            End If

            Return True

        Next

    End Function

#End Region

#Region "引当単位区分 + 出荷包装個数 + 出荷端数 + 出荷総個数 + 入目 + 出荷総数量"

    ''' <summary>
    ''' 値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function IrimeSosuryoLargeSmallCheck(ByVal dt As DataTable) As Boolean

        Dim max As Integer = dt.Rows.Count - 1

        Dim AlctdKb As String = String.Empty
        Dim outkaPkgNb As Decimal = 0
        Dim outkaHasu As Decimal = 0
        Dim outkaTtlNb As Decimal = 0
        Dim outkaTtlQt As Decimal = 0
        Dim irime As Decimal = 0

        For i As Integer = 0 To max

            AlctdKb = dt.Rows(i).Item("ALCTD_KB").ToString()
            outkaPkgNb = Convert.ToDecimal(dt.Rows(i).Item("OUTKA_PKG_NB"))
            outkaHasu = Convert.ToDecimal(dt.Rows(i).Item("OUTKA_HASU"))
            outkaTtlNb = Convert.ToDecimal(dt.Rows(i).Item("OUTKA_TTL_NB"))
            outkaTtlQt = Convert.ToDecimal(dt.Rows(i).Item("OUTKA_TTL_QT"))
            irime = Convert.ToDecimal(dt.Rows(i).Item("IRIME"))

            If AlctdKb.Equals("03") = True AndAlso outkaPkgNb = 0 _
                AndAlso outkaHasu = 0 AndAlso outkaTtlNb = 0 _
                AndAlso outkaTtlQt < irime Then

                Return False
            End If

            Return True

        Next

    End Function

#End Region

#Region "赤黒区分"

    ''' <summary>
    ''' 値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function AkakuroKbCheck(ByVal dt As DataTable) As Boolean

        Dim max As Integer = dt.Rows.Count - 1
        Dim akakuroKb As Integer = 0

        For i As Integer = 0 To max

            akakuroKb = Convert.ToInt32(dt.Rows(i).Item("AKAKURO_KB"))

            If akakuroKb = 0 Then

            Else
                Return False
            End If
        Next

        Return True

    End Function

#End Region


#End Region

#End Region

#Region "バイト切捨て(LeftB)"
    ''' <summary>Left関数のバイト版。文字数をバイト数で指定して文字列を切捨て。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <param name="Length">切り抜く文字列のバイト数</param>
    ''' <returns>切捨てられた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Function LeftB(ByVal str As String, Optional ByVal Length As Integer = 0) As String

        If str = "" Then
            Return ""
        End If

        'Lengthが0か、バイト数をオーバーする場合は全バイトが指定されたものとみなす。
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

        If Length = 0 OrElse Length > RestLength Then
            Length = RestLength
        End If

        '切捨て
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

        Array.Copy(SJIS.GetBytes(str), 0, B, 0, Length)

        Dim st1 As String = SJIS.GetString(B)

        '切捨てた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1)

        If Length = ResultLength - 1 Then
            Return st1.Substring(0, st1.Length - 1)
        Else
            Return st1
        End If

    End Function
#End Region

#Region "バイト切り抜き(MidB)"
    ''' <summary>Mid関数のバイト版。文字数と位置をバイト数で指定して文字列を切り抜く。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <param name="Start">切り抜き開始位置。全角文字を分割するよう位置が指定された場合、戻り値の文字列の先頭は意味不明の半角文字となる。</param>
    ''' <param name="Length">切り抜く文字列のバイト数</param>
    ''' <returns>切り抜かれた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Function MidB(ByVal str As String, ByVal Start As Integer, Optional ByVal Length As Integer = 0) As String

        '空文字に対しては常に空文字を返す
        If str.Equals(String.Empty) Then
            Return String.Empty
        End If

        'Startが対象文字列バイト数より大きい場合は空文字を返す
        If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str) < Start Then
            Return String.Empty
        End If

        'Lengthが0か、Start以降のバイト数をオーバーする場合はStart以降の全バイトが指定されたものとみなす。
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str) - Start + 1

        If Length = 0 OrElse Length > RestLength Then
            Length = RestLength
        End If

        '切り抜き
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

        Array.Copy(SJIS.GetBytes(str), Start - 1, B, 0, Length)

        Dim st1 As String = SJIS.GetString(B)

        '切り抜いた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1) - Start + 1

        If Length = ResultLength - 1 Then
            Return st1.Substring(0, st1.Length - 1)
        Else
            Return st1
        End If

    End Function
#End Region

    '2012/06/01 Honmyo BCLXXXから移送 Start
#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NullConvertString(ByVal value As Object) As Object

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
    Public Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function
#End Region

#Region "左埋処理"
    ''' <summary>
    ''' 0埋処理
    ''' </summary>
    ''' <param name="val">対象文字列</param>
    ''' <param name="keta">0埋後の桁数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FormatZero(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta, "0"c)

        Return val

    End Function

    ''' <summary>
    ''' スペース埋処理
    ''' </summary>
    ''' <param name="val"></param>
    ''' <param name="keta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FormatSpace(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta)

        Return val

    End Function


#End Region

#Region "SPACE除去 + 文字変換"
    ''' <summary>
    ''' SPACE除去 + 文字変換
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SpaceCutChk(ByVal chkFld As String) As String

        chkFld = Replace(Trim(chkFld), Space(1), String.Empty)
        chkFld = Replace(chkFld, "　", String.Empty)
        chkFld = StrConv(chkFld, VbStrConv.Wide)

        Return chkFld

    End Function

#End Region

#Region "株式会社（株）・・除去"
    ''' <summary>
    ''' 株式会社（株）・・除去
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReplaceCompany(ByVal chkFld As String) As String

        '"㈱"、"（株）"、"（株式会社）"、"株式会社"、"㈲"、 "（有）"、 "（有限会社）"、 "有限会社"を除く
        chkFld = chkFld.Replace("㈱", "")
        chkFld = chkFld.Replace("（株）", "")
        chkFld = chkFld.Replace("（株式会社）", "")
        chkFld = chkFld.Replace("株式会社", "")

        chkFld = chkFld.Replace("㈲", "")
        chkFld = chkFld.Replace("（有）", "")
        chkFld = chkFld.Replace("（有限会社）", "")
        chkFld = chkFld.Replace("有限会社", "")

        Return chkFld

    End Function

#End Region

#Region "ハイフン置換"
    ''' <summary>
    ''' ハイフン置換
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReplaceHyphen(ByVal chkFld As String) As String

        '"ー"、"―"を"－"に置換
        chkFld = chkFld.Replace("ー", "－")
        chkFld = chkFld.Replace("―", "－")

        Return chkFld

    End Function

#End Region

#Region "促音置換"
    ''' <summary>
    ''' 促音置換
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReplaceSokuon(ByVal chkFld As String) As String

        '"ｯ","ｬ","ｭ","ｮ"を"ﾂ","ﾔ","ﾕ","ﾖ" に置換する

        '現行PG処理
        chkFld = chkFld.Replace("ｯ", "ﾂ")
        chkFld = chkFld.Replace("ｬ", "ﾔ")
        chkFld = chkFld.Replace("ｭ", "ﾕ")
        chkFld = chkFld.Replace("ｮ", "ﾖ")

        '新規追加
        chkFld = chkFld.Replace("ｧ", "ｱ")
        chkFld = chkFld.Replace("ｨ", "ｲ")
        chkFld = chkFld.Replace("ｩ", "ｳ")
        chkFld = chkFld.Replace("ｪ", "ｴ")
        chkFld = chkFld.Replace("ｫ", "ｵ")

        Return chkFld

    End Function

#End Region
    '2012/06/01 Honmyo BCLXXXから移送 End

    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
#Region "メッセージ作成"
    ''' <summary>
    ''' メッセージ作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetErrMsgE493(ByVal ds As DataSet) As String

        'E493[%3]用のエラーメッセージを作成する

        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Dim sEDI_CTL_NO As String = dtM.Rows(0).Item("EDI_CTL_NO").ToString             'EDI_CTL_NO
        Dim sEDI_CTL_NO_CHU As String = dtM.Rows(0).Item("EDI_CTL_NO_CHU").ToString     'EDI_CTL_NO_CHU
        Dim sCUST_GOODS_CD As String = dtM.Rows(0).Item("CUST_GOODS_CD").ToString       '荷主商品コード
        Dim sGOODS_NM As String = dtM.Rows(0).Item("GOODS_NM").ToString                 '商品名

        Dim sErrMsg As String = String.Concat(" EDI管理番号 = ", sEDI_CTL_NO, "-", sEDI_CTL_NO_CHU _
                                           , "、荷主商品コード = ", sCUST_GOODS_CD _
                                           , "、商品名 = ", sGOODS_NM)
        Return sErrMsg
    End Function

#End Region
    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End


#Region "ワーニング設定(EDI出荷(大))"

#If False Then 'フィルメニッヒ セミEDI対応  20160926 changed inoue
        ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks>ワーニング画面表示用データを作成する</remarks>
    Public Function SetComWarningL(ByVal msgId As String, ByVal warningId As String, _
                                    ByVal ds As DataSet, ByVal msgArray() As String, _
                                    ByVal ediValue As String, ByVal mstValue As String) As DataSet

        Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        drW.Item("EDI_CTL_NO_L") = drEdiL("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = String.Empty
        drW.Item("CUST_ORD_NO") = drEdiL("CUST_ORD_NO")
        drW.Item("CUST_ORD_NO_DTL") = String.Empty
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = NullConvertString(msgArray(1))
        drW.Item("PARA2") = NullConvertString(msgArray(2))
        drW.Item("PARA3") = NullConvertString(msgArray(3))
        drW.Item("PARA4") = NullConvertString(msgArray(4))
        drW.Item("PARA5") = NullConvertString(msgArray(5))
        drW.Item("GOODS_NM") = String.Empty
        drW.Item("FIELD_NM") = msgArray(1)
        drW.Item("FIELD_VALUE") = ediValue
        drW.Item("MST_VALUE") = mstValue
        drW.Item("EDI_WARNING_ID") = warningId

        '2012.06.04 ディック対応　追加START
        Dim mstFlg As String = warningId.Substring(7, 1)
        If mstFlg = "2" Then
            drW.Item("DEST_NM") = String.Empty
        Else
            drW.Item("DEST_NM") = drEdiL("DEST_NM")
        End If
        '2012.06.04 ディック対応　追加END

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function
#Else
    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <param name="msgArray"></param>
    ''' <param name="ediValue"></param>
    ''' <param name="mstValue"></param>
    ''' <param name="addtionalFieldValue"></param>
    ''' <returns>データセット</returns>
    ''' <remarks>ワーニング画面表示用データを作成する</remarks>
    Public Function SetComWarningL(ByVal msgId As String _
                                 , ByVal warningId As String _
                                 , ByVal ds As DataSet, ByVal msgArray() As String _
                                 , ByVal ediValue As String _
                                 , ByVal mstValue As String _
                                 , Optional ByVal addtionalFieldValue As String = "") As DataSet

        Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        drW.Item("EDI_CTL_NO_L") = drEdiL("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = String.Empty
        drW.Item("CUST_ORD_NO") = drEdiL("CUST_ORD_NO")
        drW.Item("CUST_ORD_NO_DTL") = String.Empty
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = NullConvertString(msgArray(1))
        drW.Item("PARA2") = NullConvertString(msgArray(2))
        drW.Item("PARA3") = NullConvertString(msgArray(3))
        drW.Item("PARA4") = NullConvertString(msgArray(4))
        drW.Item("PARA5") = NullConvertString(msgArray(5))
        drW.Item("GOODS_NM") = String.Empty
        drW.Item("FIELD_NM") = msgArray(1)
        drW.Item("FIELD_VALUE") = ediValue
        drW.Item("MST_VALUE") = mstValue
        drW.Item("EDI_WARNING_ID") = warningId

        drW.Item("ADDITIONAL_FIELD_VALUE_1") = addtionalFieldValue

        '2012.06.04 ディック対応　追加START
        Dim mstFlg As String = warningId.Substring(7, 1)
        If WARNING_ID_MST_FLG.M_DEST.Equals(mstFlg) Then
            drW.Item("DEST_NM") = String.Empty
        Else
            drW.Item("DEST_NM") = drEdiL("DEST_NM")
        End If
        '2012.06.04 ディック対応　追加END

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function

#End If







#End Region

#Region "ワーニング設定(EDI出荷(中))"

#If False Then ' フィルメニッヒ セミEDI対応  20160912 changed inoue

    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks>ワーニング画面表示用データを作成する</remarks>
    Public Function SetComWarningM(ByVal msgId As String, ByVal warningId As String, _
                                    ByVal ds As DataSet, ByVal dsM As DataSet, ByVal msgArray() As String, _
                                    ByVal ediValue As String, ByVal mstValue As String) As DataSet

            Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim drEdiM As DataRow = dsM.Tables("LMH030_OUTKAEDI_M").Rows(0)

        drW.Item("EDI_CTL_NO_L") = drEdiM("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = drEdiM("EDI_CTL_NO_CHU")
        drW.Item("CUST_ORD_NO") = drEdiL("CUST_ORD_NO")
        drW.Item("CUST_ORD_NO_DTL") = drEdiM("CUST_ORD_NO_DTL")
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = msgArray(1)
        drW.Item("PARA2") = msgArray(2)
        drW.Item("PARA3") = msgArray(3)
        drW.Item("PARA4") = msgArray(4)
        drW.Item("PARA5") = String.Empty

        Dim mstFlg As String = warningId.Substring(7, 1)
        If mstFlg = "1" Then
            drW.Item("GOODS_NM") = String.Empty
        Else
            drW.Item("GOODS_NM") = drEdiM("GOODS_NM")
        End If

        drW.Item("FIELD_NM") = msgArray(1)
        drW.Item("FIELD_VALUE") = ediValue
        drW.Item("MST_VALUE") = mstValue
        drW.Item("EDI_WARNING_ID") = warningId

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function

#Else

    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <param name="dsM">データセット</param>
    ''' <param name="msgArray">メッセージパラメータ</param>
    ''' <param name="ediValue">項目値</param>
    ''' <param name="mstValue">マスタ値</param>
    ''' <param name="addtionalFieldValue">追加項目値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetComWarningM(ByVal msgId As String _
                                 , ByVal warningId As String _
                                 , ByVal ds As DataSet _
                                 , ByVal dsM As DataSet _
                                 , ByVal msgArray() As String _
                                 , ByVal ediValue As String _
                                 , ByVal mstValue As String _
                                 , Optional ByVal addtionalFieldValue As String = "") As DataSet

        Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim drEdiM As DataRow = dsM.Tables("LMH030_OUTKAEDI_M").Rows(0)

        drW.Item("EDI_CTL_NO_L") = drEdiM("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = drEdiM("EDI_CTL_NO_CHU")
        drW.Item("CUST_ORD_NO") = drEdiL("CUST_ORD_NO")
        drW.Item("CUST_ORD_NO_DTL") = drEdiM("CUST_ORD_NO_DTL")
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = msgArray(1)
        drW.Item("PARA2") = msgArray(2)
        drW.Item("PARA3") = msgArray(3)
        drW.Item("PARA4") = msgArray(4)
        drW.Item("PARA5") = String.Empty

        Dim mstFlg As String = warningId.Substring(7, 1)
        If mstFlg = "1" Then
            drW.Item("GOODS_NM") = String.Empty
        Else
            drW.Item("GOODS_NM") = drEdiM("GOODS_NM")
        End If

        drW.Item("FIELD_NM") = msgArray(1)
        drW.Item("FIELD_VALUE") = ediValue

        drW.Item("ADDITIONAL_FIELD_VALUE_1") = addtionalFieldValue

        drW.Item("MST_VALUE") = mstValue
        drW.Item("EDI_WARNING_ID") = warningId

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function

#End If

#End Region

#Region "ワーニング設定(実績作成：中レベル) デュポン(横浜)荷主コード:00588(SFTP塗料)"
    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks>ワーニング画面表示用データを作成する</remarks>
    Public Function SetComWarningJissekiM(ByVal msgId As String, ByVal warningId As String, _
                                  ByVal ds As DataSet, ByVal msgArray() As String, _
                                  ByVal count As Integer) As DataSet
        Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drJisM As DataRow = ds.Tables("LMH030_H_SEND_CHECK_DPN").Rows(count)

        drW.Item("EDI_CTL_NO_L") = drJisM("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = drJisM("EDI_CTL_NO_CHU")
        drW.Item("CUST_ORD_NO") = String.Empty
        drW.Item("CUST_ORD_NO_DTL") = drJisM("CUST_ORD_NO_DTL")
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = msgArray(2)
        drW.Item("PARA2") = msgArray(3)
        drW.Item("PARA3") = msgArray(4)
        drW.Item("PARA4") = msgArray(5)
        drW.Item("PARA5") = String.Empty
        drW.Item("GOODS_NM") = String.Empty
        drW.Item("FIELD_NM") = msgArray(1)
        drW.Item("FIELD_VALUE") = String.Empty
        drW.Item("MST_VALUE") = String.Empty
        drW.Item("EDI_WARNING_ID") = warningId

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function
#End Region

#Region "検索処理"

    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 upd start
        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectListData", ds)
        rtnDs = MyBase.CallDAC(Me._Dac, "ConvEdiDtlNm", ds)
        Return rtnDs
        'Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)
        '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 upd end

    End Function

#End Region

    '2012.03.23 追加START

#Region "時間スラッシュ編集"
    ''' <summary>
    ''' 時間スラッシュ編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Public Function GetSlashEditDate(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 4), "/", value.Substring(4, 2), "/", value.Substring(6, 2))

    End Function
#End Region
    '2012.03.23 追加END

    '▼▼▼二次
#Region "EDI取消"
    ''' <summary>
    ''' EDI取消
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>EDI出荷(大),EDI出荷(中),EDI受信(HED),EDI受信(DTL)の削除フラグ変更</remarks>
    Private Function EdiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim rcvNmHed As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_DTL").ToString()

        '2012.03.18 大阪対応 START
        Dim rcvNmExt As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_EXT").ToString()
        '2012.03.18 大阪対応 END

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        'EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        End If

        '2012.03.18 大阪対応 START
        If String.IsNullOrEmpty(rcvNmExt) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvExtData", ds)
        End If
        '2012.03.18 大阪対応 END

        Return ds

    End Function
#End Region

#Region "実績取消処理"
    ''' <summary>
    ''' 実績取消処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim rcvNmHed As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_DTL").ToString()

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        'EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        End If

        Return ds

    End Function

#End Region

#Region "実績作成済⇒実績未,実績送信済⇒実績未(実行処理)"
    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JikkouSyori(ByVal ds As DataSet) As DataSet

        Dim rcvNmHed As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_DTL").ToString()
        Dim sndNm As String = ds.Tables("LMH030INOUT").Rows(0).Item("SND_NM").ToString()
        '2012.11.09 センコー対応追加START
        Dim unsoFlg As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_UNSOFLG").ToString()
        '2012.11.09 センコー対応追加END

        '2013.03.21 追加START 出荷取消データの実績作成済⇒実績未の対応(テルモ)
        Dim outkaDelFlg As String = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("SYS_DEL_FLG").ToString()
        '2013.03.21 追加END

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)

        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

            If MyBase.GetResultCount = 0 Then
                Return ds
            End If
        End If

        'EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        End If

        'EDI送信の更新
        If String.IsNullOrEmpty(sndNm) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "DeleteEdiSendLData", ds)
        End If

        '2012.11.09 センコー対応追加START
        '運送荷主の場合には、出荷を更新しない
        If unsoFlg.Equals("1") = True Then
        Else

            '2013.03.21 追加START 出荷取消データの実績作成済⇒実績未の対応(テルモ)
            '出荷(大)の更新
            If outkaDelFlg.Equals("1") = False Then
                ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
            End If
            '2013.03.21 追加END

            If MyBase.GetResultCount = 0 Then
                Return ds
            End If
        End If
        '2012.11.09 センコー対応追加END

        Return ds

    End Function

#End Region

#Region "実績送信済⇒送信待"
    ''' <summary>
    ''' 実績送信済⇒送信待
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SousinSousinmi(ByVal ds As DataSet) As DataSet

        Dim rcvNmHed As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_DTL").ToString()
        Dim sndNm As String = ds.Tables("LMH030INOUT").Rows(0).Item("SND_NM").ToString()

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
            If MyBase.GetResultCount = 0 Then
                Return ds
            End If
        End If

        'EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        End If

        'EDI送信の更新
        If String.IsNullOrEmpty(sndNm) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiSendLData", ds)
            If MyBase.GetResultCount = 0 Then
                Return ds
            End If
        End If

        Return ds

    End Function

#End Region

#Region "出荷取消⇒未登録時同一まとめレコード取得処理"
    ''' <summary>
    ''' 出荷取消⇒未登録時同一まとめレコード取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMatomeTorikesi(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatomeTorikesi", ds)

        Return ds

    End Function

#End Region

#Region "EDI取消⇒未登録"
    ''' <summary>
    ''' EDI取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>EDI出荷(大),EDI出荷(中),EDI受信(HED),EDI受信(DTL)の削除フラグ変更</remarks>
    Private Function EdiMitouroku(ByVal ds As DataSet) As DataSet

        Dim rcvNmHed As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_DTL").ToString()
        '2012.03.18 大阪対応 START
        Dim rcvNmExt As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_EXT").ToString()
        '2012.03.18 大阪対応 END

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
            If MyBase.GetResultCount = 0 Then
                Return ds
            End If
        End If

        'EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        End If

        '2012.03.18 大阪対応 START
        If String.IsNullOrEmpty(rcvNmExt) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvExtData", ds)
        End If
        '2012.03.18 大阪対応 END

        Return ds

    End Function
#End Region

#Region "出荷取消⇒未登録"
    ''' <summary>
    ''' 出荷取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Mitouroku(ByVal ds As DataSet) As DataSet

        Dim rcvNmHed As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_DTL").ToString()
        '2012.03.18 大阪対応 START
        Dim rcvNmExt As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_EXT").ToString()
        '2012.03.18 大阪対応 END

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
            If MyBase.GetResultCount = 0 Then
                Return ds
            End If
        End If

        'EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        End If

        '2012.03.18 大阪対応 START
        If String.IsNullOrEmpty(rcvNmExt) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvExtData", ds)
        End If
        '2012.03.18 大阪対応 END

        Return ds

    End Function

#End Region

    '2012.04.04 大阪対応START
#Region "運送取消⇒未登録"
    ''' <summary>
    ''' 運送取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnsoMitouroku(ByVal ds As DataSet) As DataSet

        Dim rcvNmHed As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_DTL").ToString()
        '2012.03.18 大阪対応 START
        Dim rcvNmExt As String = ds.Tables("LMH030INOUT").Rows(0).Item("RCV_NM_EXT").ToString()
        '2012.03.18 大阪対応 END

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
            If MyBase.GetResultCount = 0 Then
                Return ds
            End If
        End If

        'EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        End If

        '2012.03.18 大阪対応 START
        If String.IsNullOrEmpty(rcvNmExt) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvExtData", ds)
        End If
        '2012.03.18 大阪対応 END

        Return ds

    End Function

#End Region
    '2012.04.04 大阪対応END

#Region "一括変更処理"

    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHenko(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim ediDest_CD As String = ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0).Item("EDIT_ITEM_VALUE1").ToString()     ''ADD 2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更
        Dim rtnResult As Boolean = False

        If ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_KBN").ToString() = "02" Then

            If Not String.IsNullOrEmpty(ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE1").ToString()) _
            OrElse Not String.IsNullOrEmpty(ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE2").ToString()) Then   'ADD 2018/11/14 要望管理002327
                '運送会社コード・運送会社支店コードが入力されている場合のみ

                '運送会社マスタの存在チェック＋運送名称の設定
                ds = MyBase.CallDAC(Me._Dac, "SelectUnsoNM", ds)

                If MyBase.IsMessageExist = True Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E079", New String() {"運送会社マスタ", "運送会社コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds
                End If

            End If  'ADD 2018/11/14 要望管理002327

        End If

        'ADD Start 2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更
        If ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_KBN").ToString() = "06" Then

            '届先一括変更時、届先M取得
            ds = MyBase.CallDAC(Me._Dac, "SelectIkkatsuM_DEST", ds)

            If MyBase.IsMessageExist = True Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E079", New String() {"届先マスタ", "届先コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If
        'ADD End   2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更


        ds = MyBase.CallDAC(Me._Dac, "UpdateHenko", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        Return ds

    End Function

#End Region

    '2012.03.14 大阪対応START
#Region "印刷フラグ更新"
    ''' <summary>
    ''' 印刷フラグ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintFlagUpDate(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "UpdatePrintFlag", ds)

        Return ds

    End Function

#End Region
    '2012.03.14 大阪対応END
    '▲▲▲二次

    '要望番号1007 2012.05.08 追加START
#Region "EDI印刷対象テーブル追加"
    ''' <summary>
    ''' EDI印刷対象テーブル追加(削除⇒追加)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteInsertHEdiPrint(ByVal ds As DataSet) As DataSet

        Dim dtPrt As DataTable = ds.Tables("H_EDI_PRINT")
        Dim max As Integer = dtPrt.Rows.Count - 1
        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtPrt As DataTable = setDs.Tables("H_EDI_PRINT")

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDtPrt.ImportRow(dtPrt.Rows(i))

            '①EDI印刷対象テーブル物理削除
            setDs = MyBase.CallDAC(Me._Dac, "DeleteHEdiPrint", setDs)

            '②EDI印刷対象テーブル新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertHEdiPrint", setDs)

        Next

        Return ds

    End Function

#End Region
    '要望番号1007 2012.05.08 追加END

    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し①商品マスタの存在チェックは外す) 2012/06/28 本明 Start
#Region "ダミー商品データセット作成"
    ''' <summary>
    ''' ダミー商品データセット作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDummyGoodsM(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal i As Integer) As DataSet

        ' ''値のクリア
        ''setDs.Clear()

        'OUTKAEDI_Mの設定
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)

        'LMH030_M_GOODSに１行追加（ダミー商品データセット）
        setDs.Tables("LMH030_M_GOODS").Rows.Add()

        'ダミー商品データセットの１行目を設定
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)

        'ダミー商品データセットの１行目の項目に値を代入(内容は要精査)
        If Len(ediMDr("COA_YN").ToString) = 1 Then
            mGoodsDr("COA_YN") = String.Concat("0", ediMDr("COA_YN"))       '分析表区分
        Else
            mGoodsDr("COA_YN") = String.Empty                               '""を設定
        End If

        mGoodsDr("GOODS_CD_NRS") = ediMDr("NRS_GOODS_CD")                   '商品KEY
        mGoodsDr("GOODS_NM_1") = ediMDr("GOODS_NM")                         '商品名
        mGoodsDr("ALCTD_KB") = ediMDr("ALCTD_KB")                           '引当単位区分
        mGoodsDr("ONDO_KB") = ediMDr("ONDO_KB")                             '温度区分
        mGoodsDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")                   '運送温度区分
        mGoodsDr("STD_IRIME_NB") = ediMDr("IRIME")                          '入目
        mGoodsDr("STD_IRIME_UT") = ediMDr("IRIME_UT")                       '入目単位
        mGoodsDr("STD_WT_KGS") = ediMDr("BETU_WT")                          '個別重量(KGS)
        mGoodsDr("NB_UT") = ediMDr("KB_UT")                                 '個数単位
        '要望番号1241 (2012.07.06) umano 修正START
        If Convert.ToInt32(ediMDr("PKG_NB")) = 0 Then
            ediMDr("PKG_NB") = 1                                            '包装個数(ダミー商品は 1固定)
        End If
        '要望番号1241 (2012.07.06) umano 修正END

        mGoodsDr("PKG_UT") = ediMDr("PKG_UT")                               '包装単位
        mGoodsDr("TARE_YN") = ediMDr("TARE_YN")                             '風袋加算フラグ

        'mGoodsDr("OUTKA_KAKO_SAGYO_KB_1") = ediMDr("OUTKA_KAKO_SAGYO_KB_1") '出荷時加工作業区分1
        'mGoodsDr("OUTKA_KAKO_SAGYO_KB_2") = ediMDr("OUTKA_KAKO_SAGYO_KB_2") '出荷時加工作業区分2
        'mGoodsDr("OUTKA_KAKO_SAGYO_KB_3") = ediMDr("OUTKA_KAKO_SAGYO_KB_3") '出荷時加工作業区分3
        'mGoodsDr("OUTKA_KAKO_SAGYO_KB_4") = ediMDr("OUTKA_KAKO_SAGYO_KB_4") '出荷時加工作業区分4
        'mGoodsDr("OUTKA_KAKO_SAGYO_KB_5") = ediMDr("OUTKA_KAKO_SAGYO_KB_5") '出荷時加工作業区分5

        Return setDs

    End Function

#End Region
    '要望番号:1209(【春日部】出荷EDI→運送登録仕様見直し②商品マスタの存在チェックは外す) 2012/06/28 本明 End

    'START UMANO 要望番号1302 支払運賃に伴う修正。支払追加処理
#Region "出荷登録処理(支払運賃作成)"

    ''' <summary>
    ''' 出荷登録処理(支払運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShiharaiSakusei(ByVal ds As DataSet) As DataSet

        '支払運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertShiharaiUnchinData", ds)

        Return ds

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。支払追加処理

#Region "タブレット項目初期値設定"
    ''' <summary>
    ''' タブレット項目初期値設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutnkaLTabletData(ByVal ds As DataSet) As DataSet

        Dim tabletYn As String = "00"

        'タブレット使用営業所の場合 現場作業ON
        ds = MyBase.CallDAC(Me._Dac, "SelectDataTabletYN", ds)
        If MyBase.GetResultCount > 0 Then
            tabletYn = "01"
        End If

        '倉庫がロケ管理対象でない場合 現場作業OFF
        ds = MyBase.CallDAC(Me._Dac, "SelectDataLocMngYn", ds)
        If MyBase.GetResultCount = 0 Then
            tabletYn = "00"
        End If

        '荷主(L+M)が現場作業対象でない場合 現場作業OFF
        ds = MyBase.CallDAC(Me._Dac, "SelectDataCustDtlTabletYN", ds)
        If MyBase.GetResultCount > 0 Then
            tabletYn = "00"
        End If

        For Each dr As DataRow In ds.Tables("LMH030_C_OUTKA_L").Rows
            '現場作業指示ステータス
            If "00".Equals(tabletYn) Then
                dr.Item("WH_TAB_STATUS") = "99"
            Else
                dr.Item("WH_TAB_STATUS") = "00"
            End If

            '現場作業有無
            dr.Item("WH_TAB_YN") = tabletYn
        Next

        Return ds
    End Function

#End Region
#End Region

End Class


