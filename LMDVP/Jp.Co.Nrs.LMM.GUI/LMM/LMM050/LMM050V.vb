' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM050V : 請求先マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL


''' <summary>
''' LMM050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM050F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMMControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMMControlG

    ''' <summary>
    ''' 非必須部署フラグを格納する
    ''' </summary>
    Private _NotHissuBusyo As Boolean = False

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM050F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSaveInputChk(Optional ByVal notHissuBusyo As Boolean = False) As Boolean

        'trim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        If Me.IsSaveSingleCheck(notHissuBusyo) = False Then
            Return False
        End If

        '関連チェック
        If Me.IsSaveCheck() = False Then
            Return False
        End If

        'マスタ存在チェック
        If Me.IsSeiqtoExistCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm
            .txtSeiqtoCd.TextValue = .txtSeiqtoCd.TextValue.Trim()
            .txtSeiqtoNm.TextValue = .txtSeiqtoNm.TextValue.Trim()
            .txtSeiqtoBusyoNm.TextValue = .txtSeiqtoBusyoNm.TextValue.Trim()
            .txtOyaPic.TextValue = .txtOyaPic.TextValue.Trim()
            .txtNrsKeiriCd1.TextValue = .txtNrsKeiriCd1.TextValue.Trim()
            .txtNrsKeiriCd2.TextValue = .txtNrsKeiriCd2.TextValue.Trim()
            .txtSeiqSndPeriod.TextValue = .txtSeiqSndPeriod.TextValue.Trim()
            .txtCustKagamiType1.TextValue = .txtCustKagamiType1.TextValue.Trim()
            .txtCustKagamiType2.TextValue = .txtCustKagamiType2.TextValue.Trim()
            .txtCustKagamiType3.TextValue = .txtCustKagamiType3.TextValue.Trim()
            .txtOyaPic.TextValue = .txtOyaPic.TextValue.Trim()
            .txtTel.TextValue = .txtTel.TextValue.Trim()
            .txtFax.TextValue = .txtFax.TextValue.Trim()
            .txtZip.TextValue = .txtZip.TextValue.Trim()
            .txtAd1.TextValue = .txtAd1.TextValue.Trim()
            .txtAd2.TextValue = .txtAd2.TextValue.Trim()
            .txtAd3.TextValue = .txtAd3.TextValue.Trim()
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            .txtCustKagamiType4.TextValue = .txtCustKagamiType4.TextValue.Trim()
            .txtCustKagamiType5.TextValue = .txtCustKagamiType5.TextValue.Trim()
            .txtCustKagamiType6.TextValue = .txtCustKagamiType6.TextValue.Trim()
            .txtCustKagamiType7.TextValue = .txtCustKagamiType7.TextValue.Trim()
            .txtCustKagamiType8.TextValue = .txtCustKagamiType8.TextValue.Trim()
            .txtCustKagamiType9.TextValue = .txtCustKagamiType9.TextValue.Trim()
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
        End With

    End Sub

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck(Optional ByVal notHissuBusyo As Boolean = False) As Boolean

        With Me._Frm
            '**********編集部のチェック

            '営業所
            '2016.01.06 UMANO 英語化対応START
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.ItemName = .TitlelblEigyo.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '請求先
            '2016.01.06 UMANO 英語化対応START
            '.txtSeiqtoCd.ItemName = "請求先コード"
            .txtSeiqtoCd.ItemName = .TitlelblSeiqCd.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtSeiqtoCd.IsHissuCheck = True
            '.txtSeiqtoCd.IsFullByteCheck = 7
            .txtSeiqtoCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtSeiqtoCd) = False Then
                Return False
            End If

            '請求先会社名
            '2016.01.06 UMANO 英語化対応START
            '.txtSeiqtoNm.ItemName = "請求先会社名"
            .txtSeiqtoNm.ItemName = .TitlelblSeiqNm.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtSeiqtoNm.IsHissuCheck = True
            .txtSeiqtoNm.IsForbiddenWordsCheck = True
            .txtSeiqtoNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtSeiqtoNm) = False Then
                Return False
            End If

            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            '担当者名
            .txtEigyoTanto.ItemName = .TitlelbPic.Text()
            .txtEigyoTanto.IsForbiddenWordsCheck = True
            .txtEigyoTanto.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtEigyoTanto) = False Then
                Return False
            End If
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end

            '請求先部署名
            '2016.01.06 UMANO 英語化対応START
            '.txtSeiqtoBusyoNm.ItemName = "請求先部署名"
            .txtSeiqtoBusyoNm.ItemName = .TitlelblSeiqBushoNm.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtSeiqtoBusyoNm.IsForbiddenWordsCheck = True
            .txtSeiqtoBusyoNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtSeiqtoBusyoNm) = False Then
                Return False
            End If

            '鑑口座区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbKouzaKbn.ItemName = "鑑口座区分"
            .cmbKouzaKbn.ItemName = .TitlelblKagamiKB.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbKouzaKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKouzaKbn) = False Then
                Return False
            End If

            '鑑名義区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbMeigiKbn.ItemName = "鑑名義区分"
            .cmbMeigiKbn.ItemName = .lblKagamiMeigi.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbMeigiKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbMeigiKbn) = False Then
                Return False
            End If

            '請求通貨コード
            .cmbSeiqCurrCd.ItemName = .lblTitleSeiqCurrCd.Text()
            .cmbSeiqCurrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSeiqCurrCd) = False Then
                Return False
            End If

            '親請求先コード
            '2016.01.06 UMANO 英語化対応START
            '.txtNrsKeiriCd1.ItemName = "親請求先コード"
            .txtNrsKeiriCd1.ItemName = .lblTitleNrsKeiriCd1.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtNrsKeiriCd1.IsHissuCheck = True
            '.txtNrsKeiriCd1.IsFullByteCheck = 7
            .txtNrsKeiriCd1.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtNrsKeiriCd1) = False Then
                Return False
            End If

            '日陸経理コード(JDE)
            '2016.01.06 UMANO 英語化対応START
            '.txtNrsKeiriCd2.ItemName = "日陸経理コード(JDE)"
            .txtNrsKeiriCd2.ItemName = .TitlelblNrsCd.Text()
            '2016.01.06 UMANO 英語化対応END

            .txtNrsKeiriCd2.IsForbiddenWordsCheck = True
            .txtNrsKeiriCd2.IsByteCheck = 10
            .txtNrsKeiriCd2.IsMiddleSpace = True

            '2021.01.16 ログインIDによっては非必須項目START
            If notHissuBusyo = True Then
                .txtNrsKeiriCd2.HissuLabelVisible = False
                .txtNrsKeiriCd2.IsHissuCheck = False
                'MyBase.ShowMessage("")
            Else
                .txtNrsKeiriCd2.HissuLabelVisible = True
                .txtNrsKeiriCd2.IsHissuCheck = True
            End If
            '2021.01.16 ログインIDによっては非必須項目END

            If MyBase.IsValidateCheck(.txtNrsKeiriCd2) = False Then
                Return False
            End If


            '請求書・送付期限
            '2016.01.06 UMANO 英語化対応START
            '.txtSeiqSndPeriod.ItemName = "請求書・送付期限"
            .txtSeiqSndPeriod.ItemName = .TitlelblSentPeriod.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtSeiqSndPeriod.IsForbiddenWordsCheck = True
            .txtSeiqSndPeriod.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtSeiqSndPeriod) = False Then
                Return False
            End If

            '荷主鑑分類種別1
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType1.ItemName = "荷主鑑分類種別1"
            .txtCustKagamiType1.ItemName = .TitlelblCustKagami1.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType1.IsForbiddenWordsCheck = True
            .txtCustKagamiType1.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType1) = False Then
                Return False
            End If

            '荷主鑑分類種別2
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType2.ItemName = "荷主鑑分類種別2"
            .txtCustKagamiType2.ItemName = .TitlelblCustKagami2.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType2.IsForbiddenWordsCheck = True
            .txtCustKagamiType2.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType2) = False Then
                Return False
            End If

            '荷主鑑分類種別3
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType3.ItemName = "荷主鑑分類種別3"
            .txtCustKagamiType3.ItemName = .TitlelblCustKagami3.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType3.IsForbiddenWordsCheck = True
            .txtCustKagamiType3.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType3) = False Then
                Return False
            End If

            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            '荷主鑑分類種別4
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType4.ItemName = "荷主鑑分類種別4"
            .txtCustKagamiType4.ItemName = .TitlelblCustKagami4.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType4.IsForbiddenWordsCheck = True
            .txtCustKagamiType4.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType4) = False Then
                Return False
            End If

            '荷主鑑分類種別5
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType5.ItemName = "荷主鑑分類種別5"
            .txtCustKagamiType5.ItemName = .TitlelblCustKagami5.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType5.IsForbiddenWordsCheck = True
            .txtCustKagamiType5.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType5) = False Then
                Return False
            End If

            '荷主鑑分類種別6
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType6.ItemName = "荷主鑑分類種別6"
            .txtCustKagamiType6.ItemName = .TitlelblCustKagami6.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType6.IsForbiddenWordsCheck = True
            .txtCustKagamiType6.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType6) = False Then
                Return False
            End If

            '荷主鑑分類種別7
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType7.ItemName = "荷主鑑分類種別7"
            .txtCustKagamiType7.ItemName = .TitlelblCustKagami7.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType7.IsForbiddenWordsCheck = True
            .txtCustKagamiType7.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType7) = False Then
                Return False
            End If

            '荷主鑑分類種別8
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType8.ItemName = "荷主鑑分類種別8"
            .txtCustKagamiType8.ItemName = .TitlelblCustKagami8.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType8.IsForbiddenWordsCheck = True
            .txtCustKagamiType8.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType8) = False Then
                Return False
            End If

            '荷主鑑分類種別9
            '2016.01.06 UMANO 英語化対応START
            '.txtCustKagamiType9.ItemName = "荷主鑑分類種別9"
            .txtCustKagamiType9.ItemName = .TitlelblCustKagami9.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustKagamiType9.IsForbiddenWordsCheck = True
            .txtCustKagamiType9.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCustKagamiType9) = False Then
                Return False
            End If
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --

            '担当者名
            '2016.01.06 UMANO 英語化対応START
            '.txtOyaPic.ItemName = "担当者名"
            .txtOyaPic.ItemName = .TitlelbPic.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtOyaPic.IsForbiddenWordsCheck = True
            .txtOyaPic.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtOyaPic) = False Then
                Return False
            End If

            '電話番号
            '2016.01.06 UMANO 英語化対応START
            '.txtTel.ItemName = "電話番号"
            .txtTel.ItemName = .TitlelblTel.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtTel.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtTel) = False Then
                Return False
            End If

            'FAX番号
            '2016.01.06 UMANO 英語化対応START
            '.txtFax.ItemName = "FAX番号"
            .txtFax.ItemName = .TitlelblTelTitlelblFax.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtFax.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtFax) = False Then
                Return False
            End If

            '締日区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbCloseKBN.ItemName = "締日区分"
            .cmbCloseKBN.ItemName = .TitlelblCloseKbn.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbCloseKBN.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbCloseKBN) = False Then
                Return False
            End If

            '郵便番号
            '2016.01.06 UMANO 英語化対応START
            '.txtZip.ItemName = "郵便番号"
            .txtZip.ItemName = .lblTitleZip.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtZip.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtZip) = False Then
                Return False
            End If

            '住所1
            '2016.01.06 UMANO 英語化対応START
            '.txtAd1.ItemName = "住所1"
            .txtAd1.ItemName = .TitlelblAd1.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd1.IsForbiddenWordsCheck = True
            .txtAd1.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd1) = False Then
                Return False
            End If

            '住所2
            '2016.01.06 UMANO 英語化対応START
            '.txtAd2.ItemName = "住所2"
            .txtAd2.ItemName = .TitlelblAd2.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd2.IsForbiddenWordsCheck = True
            .txtAd2.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd2) = False Then
                Return False
            End If

            '住所3
            '2016.01.06 UMANO 英語化対応START
            '.txtAd3.ItemName = "住所3"
            .txtAd3.ItemName = .TitlelblAd3.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd3.IsForbiddenWordsCheck = True
            .txtAd3.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd3) = False Then
                Return False
            End If

            '請求書パターン
            'START YANAI 要望番号661
            '2016.01.06 UMANO 英語化対応START
            '.cmbDocPtn.ItemName = "請求書パターン"
            '.cmbDocPtn.ItemName = "請求書パターン(値引)"
            .cmbDocPtn.ItemName = .TitlelblDocPtn.Text()
            '2016.01.06 UMANO 英語化対応END
            'END YANAI 要望番号661
            .cmbDocPtn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbDocPtn) = False Then
                Return False
            End If

            'START YANAI 要望番号661
            '請求書パターン(通常)
            '2016.01.06 UMANO 英語化対応START
            '.cmbDocPtnNomal.ItemName = "請求書パターン(通常)"
            .cmbDocPtnNomal.ItemName = .TitlelblDocPtnNomal.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbDocPtnNomal.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbDocPtnNomal) = False Then
                Return False
            End If
            'END YANAI 要望番号661

#If True Then       'ADD 2019/07/10 002520
            '適用
            .txtTekiyo.ItemName = .TitleltxtTekiyo.Text()
            .txtTekiyo.IsForbiddenWordsCheck = True
            .txtTekiyo.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtTekiyo) = False Then
                Return False
            End If

#End If

            '保管料最低保証設定フラグ
            .cmbStorageZeroFlgKBN.ItemName = .lblZeroMin.Text()
            .cmbStorageZeroFlgKBN.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbStorageZeroFlgKBN) = False Then
                Return False
            End If

            '荷役料最低保証設定フラグ
            .cmbHandlingZeroFlgKBN.ItemName = .lblZeroMin.Text()
            .cmbHandlingZeroFlgKBN.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHandlingZeroFlgKBN) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck() As Boolean

        '請求書種別正
        Dim chksei As String = String.Empty
        '請求書種別副
        Dim chkhuku As String = String.Empty
        '請求書種別控
        Dim chkhikae As String = String.Empty
        '請求書種別控(経理)
        Dim chkkeiri As String = String.Empty

        With Me._Frm
            chksei = .chkSei.GetBinaryValue
            chkhuku = .chkFuku.GetBinaryValue
            chkhikae = .chkHikae.GetBinaryValue
            chkkeiri = .chkKeiri.GetBinaryValue
        End With
        '少なくとも１つはチェックされていないとエラー
        If chksei.Equals(LMConst.FLG.OFF) = True _
            AndAlso chkhuku.Equals(LMConst.FLG.OFF) = True _
            AndAlso chkhikae.Equals(LMConst.FLG.OFF) = True _
            AndAlso chkkeiri.Equals(LMConst.FLG.OFF) = True _
            Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E199", New String() {"請求書種別"})
            MyBase.ShowMessage("E199", New String() {Me._Frm.TitlelblDocKind.Text()})
            '2016.01.06 UMANO 英語化対応END
            Me._Frm.chkSei.Focus()
            Return False
        End If

        '鑑最低保証額チェック
        Dim storageTotalFlg As String = String.Empty
        Dim handlingTotalFlg As String = String.Empty
        Dim unchinTotalFlg As String = String.Empty
        Dim sagyoTotalFlg As String = String.Empty
        With Me._Frm
            storageTotalFlg = .chkStorageTotalFlg.GetBinaryValue
            handlingTotalFlg = .chkHandlingTotalFlg.GetBinaryValue
            unchinTotalFlg = .chkUnchinTotalFlg.GetBinaryValue
            sagyoTotalFlg = .chkSagyoTotalFlg.GetBinaryValue
        End With
        If Me._Frm.numTotalMinSeiqAmt.TextValue.Equals("0") _
                AndAlso (storageTotalFlg.Equals(LMConst.FLG.ON) = True _
                        OrElse handlingTotalFlg.Equals(LMConst.FLG.ON) = True _
                        OrElse unchinTotalFlg.Equals(LMConst.FLG.ON) = True _
                        OrElse sagyoTotalFlg.Equals(LMConst.FLG.ON) = True) _
            Then
            MyBase.ShowMessage("E019", New String() {Me._Frm.lblTotalMinSeiqAmt.Text()})
            Me._Frm.numTotalMinSeiqAmt.Focus()
            Return False
        ElseIf Not Me._Frm.numTotalMinSeiqAmt.TextValue.Equals("0") _
                AndAlso storageTotalFlg.Equals(LMConst.FLG.OFF) = True _
                AndAlso handlingTotalFlg.Equals(LMConst.FLG.OFF) = True _
                AndAlso unchinTotalFlg.Equals(LMConst.FLG.OFF) = True _
                AndAlso sagyoTotalFlg.Equals(LMConst.FLG.OFF) = True _
            Then
            MyBase.ShowMessage("E019", New String() {Me._Frm.lblTotalMinChk.Text()})
            Me._Frm.numTotalMinSeiqAmt.Focus()
            Return False
        End If

        '変動保管倍率
        If Me._Frm.optVarStrageFlgY.Checked Then
            '変動保管料[あり]の場合
            If Me._Frm.cmbVarRate3.SelectedIndex > Me._Frm.cmbVarRate6.SelectedIndex Then
                MyBase.ShowMessage("E505", New String() {"変動保管倍率6ヶ月に", "変動保管倍率3ヶ月"})
                Me._Frm.cmbVarRate6.Focus()
                Return False
            End If
        End If

        Return True


    End Function

    ''' <summary>
    ''' 請求先コード存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqtoExistCheck() As Boolean

        With Me._Frm

            '請求先、新請求先が同じ場合
            Dim shinSeiqtoCd As String = .txtNrsKeiriCd1.TextValue
            If .txtSeiqtoCd.TextValue.Equals(shinSeiqtoCd) = True Then

                '新規、複写時はチェックを行わない
                Select Case .lblSituation.RecordStatus

                    Case RecordStatus.NEW_REC, RecordStatus.COPY_REC

                        Return True

                End Select

            End If

            Dim drs As DataRow() = Nothing

            drs = Me._Vcon.SelectSeiqtoListDataRow(.cmbNrsBrCd.SelectedValue.ToString(), .txtNrsKeiriCd1.TextValue)

            If drs.Length < 1 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E079", New String() {"請求先マスタ", .txtNrsKeiriCd1.TextValue})
                MyBase.ShowMessage("E830", New String() {.txtNrsKeiriCd1.TextValue})
                '2016.01.06 UMANO 英語化対応END
                Call Me._Vcon.SetErrorControl(.txtNrsKeiriCd1)
                Return False
            End If

            'マスタの値を設定
            .txtNrsKeiriCd1.TextValue = drs(0).Item("SEIQTO_CD").ToString()

        End With

        Return True


    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM050F) As Boolean

        If frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM050F, ByVal eventShubetsu As LMM050C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM050C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM050C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM050C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount() As ArrayList

        Dim defNo As Integer = LMM050G.sprDetailDef.DEF.ColNo

        With Me._Frm.sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If _Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'Trimチェック
        '検索
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)

        '単項目チェック
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時スプレッド単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '請求先コード
            vCell.SetValidateCell(0, LMM050G.sprDetailDef.SEIQTO_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "請求先コード"
            vCell.ItemName = LMM050G.sprDetailDef.SEIQTO_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '請求先会社名
            vCell.SetValidateCell(0, LMM050G.sprDetailDef.SEIQTO_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "請求先会社名"
            vCell.ItemName = LMM050G.sprDetailDef.SEIQTO_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '請求先部署名
            vCell.SetValidateCell(0, LMM050G.sprDetailDef.SEIQTO_BUSYO_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "請求先部署名"
            vCell.ItemName = LMM050G.sprDetailDef.SEIQTO_BUSYO_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '担当者名
            vCell.SetValidateCell(0, LMM050G.sprDetailDef.OYA_PIC.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "担当者名"
            vCell.ItemName = LMM050G.sprDetailDef.OYA_PIC.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


        End With

        Return True


    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM050C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM050C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM050C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM050C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select


            Case LMM050C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM050C.EventShubetsu.KENSAKU         '検索
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM050C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM050C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM050C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM050C.EventShubetsu.DCLICK         'ダブルクリック
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM050C.EventShubetsu.ENTER          'Enter
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM050C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'Return Me._Vcon.SetFocusErrMessage()
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM050C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim focusCtl As Control = Me._Frm.ActiveControl

        With Me._Frm

            Select Case objNm

                Case .txtNrsKeiriCd1.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtNrsKeiriCd1}
                    msg = New String() {.lblTitleNrsKeiriCd1.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.txtNrsKeiriCd1}

                Case .txtZip.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtZip}
                    msg = New String() {.lblTitleZip.Text}


            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#End Region 'Method

End Class
