' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD080  : 荷主システム在庫数と日陸在庫数との照合
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports System.IO

''' <summary>
''' LMD080Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD080V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD080F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMDControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD080F, ByVal v As LMDControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMD080C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            If LMD080C.EventShubetsu.CHECK.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.TORIKOMI.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.SHUKEI.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.SHOGO.Equals(eventShubetsu) = True Then
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsHissuCheck() = True
                .txtCustCdL.IsForbiddenWordsCheck() = True
                .txtCustCdL.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If  LMD080C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LMD080C.EventShubetsu.CHECK.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.TORIKOMI.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.SHUKEI.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.SHOGO.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsHissuCheck() = True
                .txtCustCdM.IsForbiddenWordsCheck() = True
                .txtCustCdM.IsFullByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LMD080C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LMD080C.EventShubetsu.CHECK.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.TORIKOMI.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.SHUKEI.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.SHOGO.Equals(eventShubetsu) = True Then
                '実施日
                .imdJisshiDate.ItemName() = .lblTitleJisshiDate.TextValue
                .imdJisshiDate.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdJisshiDate) = False Then
                    Return False
                End If
                If .imdJisshiDate.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {.lblTitleJisshiDate.TextValue, "8"})
                    Return False
                End If
            End If

            If LMD080C.EventShubetsu.CHECK.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.TORIKOMI.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.SHUKEI.Equals(eventShubetsu) = True OrElse _
                LMD080C.EventShubetsu.SHOGO.Equals(eventShubetsu) = True Then
                '荷主在庫レイアウト
                .cmbLayout.ItemName() = .lblTitleLayout.TextValue
                .cmbLayout.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbLayout) = False Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMD080C.EventShubetsu) As Boolean

        '【関連項目チェック】
        With Me._Frm

            If LMD080C.EventShubetsu.TORIKOMI.Equals(eventShubetsu) = True Then
                'ファイルの存在チェック
                Dim folderNm As String = .lblFolder.TextValue
                Dim fileNm As String = .lblFile.TextValue

                If Me.IsExistFileChk(folderNm, fileNm) = False Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' ファイル存在チェック
    ''' </summary>
    ''' <param name="filePath">ファイルパス</param>
    ''' <param name="fileNm">ファイル名称</param>
    ''' <returns>True ：エラーなし False：エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistFileChk(ByVal filePath As String, _
                                   ByVal fileNm As String) As Boolean

        If String.IsNullOrEmpty(filePath) = True OrElse _
            String.IsNullOrEmpty(fileNm) = True Then
            MyBase.ShowMessage("E469", New String() {"取込対象のファイル"})
            Return False
        End If

        Dim fi As FileInfo = New FileInfo(String.Concat(filePath, fileNm))

        If fi.Exists = True Then
            Return True
        Else
            MyBase.ShowMessage("E469", New String() {"取込対象のファイル"})
            Return False
        End If

    End Function

    ''' <summary>
    ''' ファイル取込時、単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsTorikomiSingleCheck(ByVal dr As DataRow, ByVal rowCnt As Integer) As Boolean

        Dim strSplit() As String = Nothing

        '【単項目チェック】
        With Me._Frm

            '商品コード
            .txtFileTextBox.TextValue = dr.Item("GOODS_CD_CUST").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEGOODSCD
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEGOODSCD, String.Concat(rowCnt, "行目")})
                Return False
            End If
            '20151026 荷主商品コード バイト数 12→20に変更 adachi
            If 20 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEGOODSCD, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '商品名
            .txtFileTextBox.TextValue = dr.Item("GOODS_NM").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEGOODSNM
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEGOODSNM, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 120 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEGOODSNM, String.Concat(rowCnt, "行目")})
                Return False
            End If

            'ロット№
            .txtFileTextBox.TextValue = dr.Item("LOT_NO").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILELOTNO
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILELOTNO, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '2015/10/26 LOT バイト数 20→40に変更　 adachi
            If 40 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILELOTNO, String.Concat(rowCnt, "行目")})
                Return False
            End If

            'シリアル№
            .txtFileTextBox.TextValue = dr.Item("SERIAL_NO").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILESERIALNO
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILESERIALNO, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '2015/10/26 シリアル バイト数 20→40に変更 adachi
            If 40 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILESERIALNO, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '入目
            .txtFileTextBox.TextValue = dr.Item("IRIME").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEIRIME
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 6 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '入目単位
            .txtFileTextBox.TextValue = dr.Item("IRIME_UT").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEIRIMEUT
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEIRIMEUT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 2 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEIRIMEUT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '分類項目１
            .txtFileTextBox.TextValue = dr.Item("CLASS_1").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILECLASS1
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILECLASS1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 40 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILECLASS1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '分類項目２
            .txtFileTextBox.TextValue = dr.Item("CLASS_2").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILECLASS2
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILECLASS2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 40 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILECLASS2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '分類項目３
            .txtFileTextBox.TextValue = dr.Item("CLASS_3").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILECLASS3
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILECLASS3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 40 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILECLASS3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '分類項目４
            .txtFileTextBox.TextValue = dr.Item("CLASS_4").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILECLASS4
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILECLASS4, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 40 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILECLASS4, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '分類項目５
            .txtFileTextBox.TextValue = dr.Item("CLASS_5").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILECLASS5
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILECLASS5, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 40 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILECLASS5, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '個数
            .txtFileTextBox.TextValue = dr.Item("NB").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILENB
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILENB, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILENB, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILENB, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 10 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILENB, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '数量
            .txtFileTextBox.TextValue = dr.Item("QT").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEQT
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０１
            .txtFileTextBox.TextValue = dr.Item("FREE_N01").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN1
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN1, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０２
            .txtFileTextBox.TextValue = dr.Item("FREE_N02").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN2
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN2, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０３
            .txtFileTextBox.TextValue = dr.Item("FREE_N03").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN3
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN3, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０４
            .txtFileTextBox.TextValue = dr.Item("FREE_N04").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN4
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN4, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN4, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN4, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN4, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN4, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０５
            .txtFileTextBox.TextValue = dr.Item("FREE_N05").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN5
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN5, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN5, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN5, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN5, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN5, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０６
            .txtFileTextBox.TextValue = dr.Item("FREE_N06").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN6
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN6, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN6, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN6, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN6, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN6, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０７
            .txtFileTextBox.TextValue = dr.Item("FREE_N07").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN7
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN7, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN7, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN7, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN7, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN7, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０８
            .txtFileTextBox.TextValue = dr.Item("FREE_N08").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN8
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN8, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN8, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN8, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN8, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN8, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値０９
            .txtFileTextBox.TextValue = dr.Item("FREE_N09").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN9
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN9, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN9, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN9, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN9, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN9, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '数値１０
            .txtFileTextBox.TextValue = dr.Item("FREE_N10").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEN10
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEN10, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEFREEN10, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEFREEN10, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN10, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEN10, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '文字列０１
            .txtFileTextBox.TextValue = dr.Item("FREE_C01").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC1
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列０２
            .txtFileTextBox.TextValue = dr.Item("FREE_C02").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC2
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列０３
            .txtFileTextBox.TextValue = dr.Item("FREE_C03").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC3
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列０４
            .txtFileTextBox.TextValue = dr.Item("FREE_C04").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC4
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC4, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC4, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列０５
            .txtFileTextBox.TextValue = dr.Item("FREE_C05").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC5
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC5, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC5, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列０６
            .txtFileTextBox.TextValue = dr.Item("FREE_C06").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC6
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC6, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC6, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列０７
            .txtFileTextBox.TextValue = dr.Item("FREE_C07").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC7
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC7, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC7, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列０８
            .txtFileTextBox.TextValue = dr.Item("FREE_C08").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC8
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC8, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC8, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列０９
            .txtFileTextBox.TextValue = dr.Item("FREE_C09").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC9
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC9, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC9, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '文字列１０
            .txtFileTextBox.TextValue = dr.Item("FREE_C10").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEFREEC10
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEFREEC10, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 200 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEFREEC10, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '赤データ条件１
            .txtFileTextBox.TextValue = dr.Item("DATA_RB_1").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILERBKB1
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILERBKB1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 20 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILERBKB1, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '赤データ条件２
            .txtFileTextBox.TextValue = dr.Item("DATA_RB_2").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILERBKB2
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILERBKB2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 20 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILERBKB2, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '赤データ条件３
            .txtFileTextBox.TextValue = dr.Item("DATA_RB_3").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILERBKB3
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILERBKB3, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 20 < System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Me._Frm.txtFileTextBox.TextValue) Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILERBKB3, String.Concat(rowCnt, "行目")})
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' ファイル取込時、単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsTorikomiSingleCheck2(ByVal dr As DataRow, ByVal rowCnt As Integer) As Boolean

        Dim strSplit() As String = Nothing

        '【単項目チェック】
        With Me._Frm

            '入目
            .txtFileTextBox.TextValue = dr.Item("IRIME").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEIRIME
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 6 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEIRIME, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

            '個数
            .txtFileTextBox.TextValue = dr.Item("NB").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILENB
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILENB, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILENB, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILENB, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 10 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILENB, String.Concat(rowCnt, "行目")})
                Return False
            End If

            '数量
            .txtFileTextBox.TextValue = dr.Item("QT").ToString
            .txtFileTextBox.ItemName() = LMD080C.FILEQT
            .txtFileTextBox.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E478", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsNumericCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E476", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            .txtFileTextBox.IsHankakuCheck() = True
            If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                MyBase.ShowMessage("E475", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            strSplit = .txtFileTextBox.TextValue.Split("."c)
            If 9 < strSplit(0).Length Then
                MyBase.ShowMessage("E474", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                Return False
            End If

            If 1 < strSplit.Length Then
                If 3 < strSplit(1).Length Then
                    MyBase.ShowMessage("E474", New String() {LMD080C.FILEQT, String.Concat(rowCnt, "行目")})
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMD080C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMD080C.EventShubetsu.CHECK        'チェック
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD080C.EventShubetsu.TORIKOMI     '取込
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD080C.EventShubetsu.SHUKEI       '集計
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD080C.EventShubetsu.SHOGO        '照合
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD080C.EventShubetsu.MASTER       'マスタ参照
                '10:閲覧者の場合エラー
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

            Case LMD080C.EventShubetsu.CLOSE        '閉じる
                'すべての権限許可
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
                        kengenFlg = True
                End Select

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

#End Region 'Method

End Class
