' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : EDI
'  プログラムID     :  LMH070BLF : 先行手入力入出荷とEDIの紐付け
'  作  成  者       :  nishikawa
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMH070BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH070BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH070BLC = New LMH070BLC()

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectEdi(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectEdi", ds)

    End Function

    ''' <summary>
    ''' 検索処理(入出荷)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInOutka(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectInOutka", ds)

    End Function

#End Region

#Region "保存処理"

    Private Function Hozon(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC
        Dim inTableNm As String = String.Empty
        Dim jobNm As String = String.Empty
        Dim rtnResult As Boolean = False
        Dim shoriFlg As String = String.Empty
        Dim inOutFlg As String = String.Empty

        If ds.DataSetName.Equals("LMH010DS") Then
            '入荷登録
            inTableNm = "LMH010INOUT"
            jobNm = "InkaToroku"
            inOutFlg = "1"
        Else
            '出荷登録
            inTableNm = "LMH030INOUT"
            jobNm = "OutkaToroku"
            inOutFlg = "0"
        End If

        shoriFlg = ds.Tables(inTableNm).Rows(0)("SHORI_FLG").ToString()
        '処理フラグ判定
        If shoriFlg.Equals("01") Then
            '処理済の場合は処理終了
            Return ds
        End If

        Dim ediIndex As Integer = Convert.ToInt32(ds.Tables(inTableNm).Rows(0)("EDI_CUST_INDEX"))

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            rtnBlc = getBLC(ediIndex, inOutFlg)

            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

            'エラーがあるかを判定
            'rtnResult = Not MyBase.IsMessageExist()
            'rtnResult = Not MyBase.IsMessageStoreExist()

            If MyBase.IsMessageStoreExist() = False Then
                If ds.Tables("WARNING_DTL").Rows.Count = 0 Then
                    rtnResult = True
                End If
            End If

            If rtnResult = True Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)
            
            End If

        End Using

        Return ds

    End Function

#End Region

#Region "BLC設定処理"
    ''' <summary>
    ''' BLC設定処理
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBLC(ByVal ediIndex As Integer, ByVal inOutFlg As String) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        If inOutFlg.Equals("0") = True Then     '出荷の場合

            Select ediIndex

                'EDI荷主INDEX番号
                'サクラファインテック(横浜):出荷
                Case 1

                    Dim blc401 As LMH030BLC401 = New LMH030BLC401
                    setBlc = blc401

                    '2012.04.11 デュポン千葉→横浜移送(00089):出荷 追加
                    'デュポン(横浜):出荷
                    'デュポン(大阪):出荷
                    'デュポン(千葉):出荷
                Case 18, 36, 37, 81, 86, 87, 88, 89, 47, 35, 76, 60, 79, 80

                    Dim blc403 As LMH030BLC403 = New LMH030BLC403
                    setBlc = blc403

                    '日本合成化学(名古屋):出荷
                Case 57

                    Dim blc601 As LMH030BLC601 = New LMH030BLC601
                    setBlc = blc601

                    '東邦化学(大阪):出荷
                Case 65

                    Dim blc201 As LMH030BLC201 = New LMH030BLC201
                    setBlc = blc201

                    'ダウケミ(大阪):出荷
                    'ダウケミ(高石):出荷
                Case 43, 44

                    Dim blc202 As LMH030BLC202 = New LMH030BLC202
                    setBlc = blc202

                    '浮間合成(大阪):出荷
                    '浮間合成(岩槻):出荷
                Case 91, 3

                    Dim blc203 As LMH030BLC203 = New LMH030BLC203
                    setBlc = blc203

                    '三井化学(大阪):出荷
                    '三井化学(日本特殊塗料)(大阪):出荷
                    '三井化学(大阪):出荷
                    '（千葉）三井化学:出荷
                Case 17, 21, 16, 45

                    Dim blc204 As LMH030BLC204 = New LMH030BLC204
                    setBlc = blc204

                    '20120607 追加START
                    'ディック物流大阪:出荷
                    '(岩槻)ディック物流埼玉(東ケミ):出荷
                    '(岩槻)ディック物流埼玉(東ケミ０１):出荷
                    '(岩槻)ディック物流春日部:出荷
                    '(岩槻)ディック物流千葉-岩槻分:出荷
                    '(岩槻)ディック物流埼玉(その他):出荷
                    '(岩槻)ディック物流（戸田）東京営業所:出荷
                    '(岩槻)ディック物流館林:出荷
                    '(岩槻)ディック物流埼玉リナブルー:出荷
                    '(岩槻)ディック物流群馬日パケ:出荷
                    '(岩槻)ディック物流群馬トートタンク:出荷
                    '(岩槻)ディック物流群馬:出荷
                Case 28, 11, 41, 5, 8, 13, 39, 12, 68, 31, 26, 25

                    Dim blc205 As LMH030BLC205 = New LMH030BLC205
                    setBlc = blc205
                    '20120607 追加END

                    '大日本住友製薬(医薬品)(大阪):出荷
                    '大日本住友製薬(動物薬)(大阪):出荷
                Case 82, 85

                    Dim blc206 As LMH030BLC206 = New LMH030BLC206
                    setBlc = blc206

                    'ジャパンコンポジット(大阪):出荷
                    'アイカ工業(大阪):出荷
                Case 34, 40

                    Dim blc207 As LMH030BLC207 = New LMH030BLC207
                    setBlc = blc207

                    '2012.05.01 追加START
                    '千葉：日医工
                Case 92
                    Dim blc101 As LMH030BLC101 = New LMH030BLC101
                    setBlc = blc101
                    '2012.05.01 追加END

                    '20120607 追加START
                    '大日精化(東京製造)(岩槻)
                    '大日精化(化成品)(岩槻)
                    '大日精化(応顔２課)(岩槻)
                Case 9, 10, 24
                    Dim blc504 As LMH030BLC504 = New LMH030BLC504
                    setBlc = blc504

                    '住化カラー(岩槻)
                    '住化カラー(市原) 2012.10.09 追加
                Case 6, 72, 73
                    Dim blc502 As LMH030BLC502 = New LMH030BLC502
                    setBlc = blc502

                    'ゴードー溶剤(岩槻)：出荷
                Case 4
                    Dim blc506 As LMH030BLC506 = New LMH030BLC506
                    setBlc = blc506
                    '20120607 追加END

                    '20120702 追加START
                    '篠崎運送(群馬)
                Case 58
                    Dim blc301 As LMH030BLC301 = New LMH030BLC301
                    setBlc = blc301
                    '20120702 追加END

                    '2012.10.09 追加START
                    '（千葉）ビックケミー
                    '（千葉）ビックケミー（テツタニ）
                    '（千葉）ビックケミー（長瀬）
                Case 93, 94, 95
                    Dim blc102 As LMH030BLC102 = New LMH030BLC102
                    setBlc = blc102

                    '（千葉）富士フイルム
                Case 96
                    Dim blc103 As LMH030BLC103 = New LMH030BLC103
                    setBlc = blc103

                    '（千葉）美浜
                Case 78
                    Dim blc109 As LMH030BLC109 = New LMH030BLC109
                    setBlc = blc109

                    '（千葉）ジェイティ物流
                Case 78
                    Dim blc111 As LMH030BLC111 = New LMH030BLC111
                    setBlc = blc111

                    '（千葉）日産物流
                Case 32
                    Dim blc104 As LMH030BLC104 = New LMH030BLC104
                    setBlc = blc104
                    '2012.10.09 追加END

                    '2012.10.16 追加START
                    '（千葉）ユーティーアイ
                Case 22
                    'Dim blc112 As LMH030BLC112 = New LMH030BLC112
                    'setBlc = blc112

                    '（千葉）東レダウ
                Case 23
                    Dim blc106 As LMH030BLC106 = New LMH030BLC106
                    setBlc = blc106
                    '2012.10.16 追加END

                    '2012.12.19 追加END
                    '（岩槻）ビーピー・カストロール
                Case 77
                    Dim blc502 As LMH030BLC502 = New LMH030BLC502
                    setBlc = blc502

                    '2013.01.09 追加START
                    '（大阪）日興産業
                Case 98
                    Dim blc208 As LMH030BLC208 = New LMH030BLC208
                    setBlc = blc208
                    '2013.01.09 追加END

                    '2013.02.05 追加START
                    '（千葉）ハネウェル（市原：市原）
                    '（千葉）ハネウェル（市原：市原Ｂ＆Ｊ）
                    '（千葉）ハネウェル（大阪：兵機）
                    '（千葉）ハネウェル（名古屋：由良）
                    '（千葉）ハネウェル（北海道：三和）
                    '（千葉）ハネウェル（九州：博多）
                    '（千葉）ハネウェル（横浜：舟津） 
                Case 48, 49, 50, 51, 52, 53, 56
                    Dim blc105 As LMH030BLC105 = New LMH030BLC105
                    setBlc = blc105

                Case Else
                    setBlc = Nothing
                    '2013.02.05 追加END

            End Select

        Else                                    '入荷の場合

            Select Case ediIndex

                'EDI荷主INDEX番号

                '2012.04.11 デュポン千葉→横浜移送(00089):入荷 追加
                'デュポン(横浜):入荷
                'デュポン(大阪):入荷
                'デュポン(千葉):入荷
                Case 3, 15, 16, 32, 33, 34, 35, 36, 4, 14, 23, 29, 31, 41

                    Dim blc403 As LMH010BLC403 = New LMH010BLC403
                    setBlc = blc403

                    '日本合成化学(名古屋):入荷
                Case 24

                    Dim blc601 As LMH010BLC601 = New LMH010BLC601
                    setBlc = blc601

                    '東邦化学(大阪):入荷
                Case 26

                    Dim blc201 As LMH010BLC201 = New LMH010BLC201
                    setBlc = blc201

                    '浮間合成(大阪):入荷
                    '浮間合成(岩槻):入荷
                Case 38, 1

                    Dim blc203 As LMH010BLC203 = New LMH010BLC203
                    setBlc = blc203

                    'ダウケミ(大阪):入荷
                    'ダウケミ(高石):入荷
                Case 17, 18

                    Dim blc202 As LMH010BLC202 = New LMH010BLC202
                    setBlc = blc202

                    '2012.04.26 追加START
                    '千葉：日医工
                Case 39
                    Dim blc101 As LMH010BLC101 = New LMH010BLC101
                    setBlc = blc101
                    '2012.04.26 追加END

                    '20120607 追加START
                    '住化カラー(岩槻)：入荷
                Case 2
                    Dim blc502 As LMH010BLC502 = New LMH010BLC502
                    setBlc = blc502
                    '20120607 追加END

                    '2012.10.09 追加START
                    '（千葉）日産物流：入荷
                Case 13
                    Dim blc104 As LMH010BLC104 = New LMH010BLC104
                    setBlc = blc104

                    '（千葉）富士フイルム：入荷
                Case 40
                    Dim blc103 As LMH010BLC103 = New LMH010BLC103
                    setBlc = blc103
                    '2012.10.09 追加END

                    '2012.12.12 追加START
                    '（岩槻）ビーピー・カストロール：入荷
                Case 30
                    Dim blc501 As LMH010BLC501 = New LMH010BLC501
                    setBlc = blc501
                    '2012.12.12 追加END

                Case Else
                    setBlc = Nothing

            End Select

        End If

        Return setBlc

    End Function
#End Region

End Class
