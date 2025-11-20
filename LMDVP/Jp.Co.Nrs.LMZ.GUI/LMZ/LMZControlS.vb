' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ          : 
'  プログラムID     :  LMZControlS  : LMZPOPUP画面 共通処理
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMMControlセッションクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/04/06 hirayama
''' </histry>
Public Class LMZControlS

#Region "Field"

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Dim _PrmDs As DataSet

    ''' <summary>
    ''' 最新の検索時取得DS
    ''' </summary>
    ''' <remarks></remarks>
    Dim _OutDs As DataSet

    ''' <summary>
    ''' パラメータのNFFormDataをクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FormPrm As LMFormData

    ''' <summary>
    ''' サーバーアクセス時の取得件数
    ''' </summary>
    ''' <remarks></remarks>
    Private _Cnt As Integer

#End Region 'Field

#Region "Constructor"

    Public Sub New()

    End Sub

#End Region

#Region "Property"

    ''' <summary>
    ''' データセットを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property PrmDs() As DataSet
        Get
            Return _PrmDs
        End Get
        Set(ByVal value As DataSet)
            _PrmDs = value
        End Set
    End Property

    ''' <summary>
    ''' データセットを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property OutDs() As DataSet
        Get
            Return _OutDs
        End Get
        Set(ByVal value As DataSet)
            _OutDs = value
        End Set
    End Property

    ''' <summary>
    ''' 画面のモードを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property FormPrm() As LMFormData
        Get
            Return _FormPrm
        End Get
        Set(ByVal value As LMFormData)
            _FormPrm = value
        End Set
    End Property

    ''' <summary>
    ''' カウントを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property Cnt() As Integer
        Get
            Return _Cnt
        End Get
        Set(ByVal value As Integer)
            _Cnt = value
        End Set
    End Property
#End Region

#Region "Method"

   



#End Region

End Class
