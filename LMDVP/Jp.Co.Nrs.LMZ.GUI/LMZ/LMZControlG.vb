' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB          : 
'  プログラムID     :  LMZControlG  : LMZ編集画面 共通処理
'  作  成  者       : 
' ==========================================================================
Imports GrapeCity.Win.Editors.Fields
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMZControl画面クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/03/01 SUZUKI
''' </histry>
Public Class LMZControlG
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByVal frm As Form)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByVal frm As LMFormPopLL)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

    End Sub


    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByVal frm As LMFormPopM)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

    End Sub
#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキー(LMPopupL)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Overloads Sub SetFunctionKey(ByVal frm As LMFormPopL, ByVal ptnF10 As LMZControlC.F10Pattern, ByVal ptnF11 As LMZControlC.F11Pattern)

        Call Me.SetFunctionKey(frm.FunctionKey, ptnF10, ptnF11)

    End Sub

    ''' <summary>
    ''' ファンクションキー(LMPopupLL)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ptnF10"></param>
    ''' <param name="ptnF11"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetFunctionKey(ByVal frm As LMFormPopLL, ByVal ptnF10 As LMZControlC.F10Pattern, ByVal ptnF11 As LMZControlC.F11Pattern)

        Call Me.SetFunctionKey(frm.FunctionKey, ptnF10, ptnF11)

    End Sub

    ''' <summary>
    ''' ファンクションキー(LMPopupM)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ptnF10"></param>
    ''' <param name="ptnF11"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetFunctionKey(ByVal frm As LMFormPopM, ByVal ptnF10 As LMZControlC.F10Pattern, ByVal ptnF11 As LMZControlC.F11Pattern)

        Call Me.SetFunctionKey(frm.FunctionKey, ptnF10, ptnF11)

    End Sub


    ''' <summary>
    ''' ファンクションキー設定
    ''' </summary>
    ''' <param name="fKey"></param>
    ''' <param name="ptnF10"></param>
    ''' <param name="ptnF11"></param>
    ''' <remarks></remarks>
    Private Overloads Sub SetFunctionKey(ByVal fKey As Win.InputMan.LMImFunctionKey, ByVal ptnF10 As LMZControlC.F10Pattern, ByVal ptnF11 As LMZControlC.F11Pattern)

        Dim always As Boolean = True
        Dim f10Lock As Boolean = False
        Dim f11Lock As Boolean = False
        Dim f10Str As String = String.Empty
        Dim f11Str As String = String.Empty

        With fKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.POP_L)
            .Enabled = True

            Select Case ptnF10
                Case LMZControlC.F10Pattern.ptn2
                    f10Str = "ロット指定"
                    f10Lock = True
                Case LMZControlC.F10Pattern.ptn3
                    f10Str = "積荷参照"
                    f10Lock = True
                    'START YANAI 要望番号604
                Case LMZControlC.F10Pattern.ptn4
                    f10Str = "大中設定"
                    f10Lock = True
                    'END YANAI 要望番号604
                Case LMZControlC.F10Pattern.ptn5
                    f10Str = "DIC検索"
                    f10Lock = True
                    'END YANAI 要望番号604
            End Select

            Select Case ptnF11
                Case LMZControlC.F11Pattern.ptn2
                    f11Str = "Ｏ　Ｋ"
                    f11Lock = True
            End Select

            'ファンクションキー個別設定
            .F9ButtonName = "検　索"
            .F10ButtonName = f10Str
            .F11ButtonName = f11Str
            .F12ButtonName = "キャンセル"

            'ファンクションキーの制御
            .F9ButtonEnabled = always
            .F10ButtonEnabled = f10Lock
            .F11ButtonEnabled = f11Lock
            .F12ButtonEnabled = always

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me.MyForm)

        End With

    End Sub

#End Region 'FunctionKey

#Region "画面制御"

    ''' <summary>
    ''' フォーカス取得
    ''' </summary>
    ''' <param name="spr"></param>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal spr As LMSpread)

        'spr.Focus()

        spr.ActiveSheet.SetActiveCell(0, 0)


    End Sub

    ''' <summary>
    ''' タブインデックス取得
    ''' </summary>
    ''' <param name="spr"></param>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex(ByVal spr As LMSpread)

        spr.TabIndex = 0

    End Sub


    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoCustCond(ByVal spr As LMSpreadSearch) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , "KBN_CD" _
                                                  , "KBN_NM2" _
                                                  , False _
                                                  , New String() {"KBN_GROUP_CD"} _
                                                  , New String() {LMKbnConst.KBN_U009} _
                                                  )

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoFlg(ByVal spr As LMSpreadSearch) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , "KBN_CD" _
                                                  , "KBN_NM2" _
                                                  , False _
                                                  , New String() {"KBN_GROUP_CD"} _
                                                  , New String() {LMKbnConst.KBN_O004} _
                                                  )

    End Function


    ''' <summary>
    ''' セルのプロパティを設定(税区分)
    ''' </summary>
    ''' <param name="spr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoTax(ByVal spr As LMSpreadSearch) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , "KBN_CD" _
                                                  , "KBN_NM3" _
                                                  , False _
                                                  , New String() {"KBN_GROUP_CD"} _
                                                  , New String() {LMKbnConst.KBN_Z001} _
                                                  )

    End Function
    ''' <summary>
    '''有無フラグスプレッド(○×)表示
    ''' </summary>
    ''' <param name="kbnCd"></param>
    ''' <param name="cd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SpreadMaruBatsu(ByVal kbnCd As String, ByVal cd As String) As String

        If kbnCd.Equals(cd) = True Then
            Return LMZControlC.UMU_ON
        Else
            Return LMZControlC.UMU_OFF
        End If

    End Function

#End Region

#End Region

#Region "キャッシュテーブルより値取得"

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRow(ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "00" _
                                          , Optional ByVal custSCd As String = "00" _
                                          , Optional ByVal custSSCd As String = "00" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custLCd, custMCd, custSCd, custSSCd))

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectCustString(ByVal custLCd As String _
                                     , ByVal custMCd As String _
                                     , ByVal custSCd As String _
                                     , ByVal custSSCd As String _
                                     ) As String

        SelectCustString = String.Empty

        '削除フラグ
        SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")

        '荷主コード（大）
        SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        If String.IsNullOrEmpty(custMCd) = False Then

            '荷主コード（中）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        End If

        If String.IsNullOrEmpty(custSCd) = False Then

            '荷主コード（小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_S = ", " '", custSCd, "' ")

        End If

        If String.IsNullOrEmpty(custSSCd) = False Then

            '荷主コード（極小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_SS = ", " '", custSSCd, "' ")

        End If

        Return SelectCustString

    End Function

    ''' <summary>
    ''' ユーザーマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userCd">ユーザーコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUserListDataRow(ByVal userCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(Me.SelectUserString(userCd))

    End Function


    ''' <summary>
    ''' ユーザーマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userCd">ユーザーコード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUserString(ByVal userCd As String) As String

        SelectUserString = String.Empty

        '削除フラグ
        SelectUserString = String.Concat(SelectUserString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        'SelectUserString = String.Concat(SelectUserString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        'ユーザーコード
        SelectUserString = String.Concat(SelectUserString, " AND ", "USER_CD = ", " '", userCd, "' ")

        Return SelectUserString

    End Function


    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnGroupCd">区分グループコード</param>
    ''' <param name="kbnCd">区分コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKbnListDataRow(ByVal kbnGroupCd As String, Optional ByVal kbnCd As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectKbnString(kbnGroupCd, kbnCd))

    End Function


    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnGroupCd">区分グループコード</param>
    ''' <param name="kbnCd">区分コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectKbnString(ByVal kbnGroupCd As String, ByVal kbnCd As String) As String

        SelectKbnString = String.Empty

        '削除フラグ
        SelectKbnString = String.Concat(SelectKbnString, " SYS_DEL_FLG = '0' ")

        '区分グループコード
        SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_GROUP_CD = ", " '", kbnGroupCd, "' ")

        If String.IsNullOrEmpty(kbnCd) = False Then

            '区分コード
            SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_CD = ", " '", kbnCd, "' ")

        End If
        

        Return SelectKbnString

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoBrCd">運送会社支店コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnsocoListDataRow(ByVal unsoCd As String, Optional ByVal unsoBrCd As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(Me.SelectUnsocoString(unsoCd, unsoBrCd))

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoBrCd">運送会社支店コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsocoString(ByVal unsoCd As String, Optional ByVal unsoBrCd As String = "") As String

        SelectUnsocoString = String.Empty

        '削除フラグ
        SelectUnsocoString = String.Concat(SelectUnsocoString, " SYS_DEL_FLG = '0' ")

        '運送会社コード
        SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_CD = ", " '", unsoCd, "' ")

        '運送会社支店コード
        If String.IsNullOrEmpty(unsoBrCd) = False Then
            SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_BR_CD = ", " '", unsoBrCd, "' ")
        End If

        Return SelectUnsocoString

    End Function

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Friend Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

#End Region

End Class
