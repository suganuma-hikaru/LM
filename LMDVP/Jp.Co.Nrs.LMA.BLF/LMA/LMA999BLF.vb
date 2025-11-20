
Imports Jp.Co.Nrs.LM.BLC

Public Class LMA999BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

    Private Function CALL_DAC(ByVal ds As DataSet) As DataSet

        MyBase.CallBLC(New LMA999BLC, "CALL_DAC", ds)

        'For i As Integer = 1 To 3

        '    If i <> 3 Then
        '        '入力チェック

        '        MyBase.SetMessageStore("01", "E078", New String() {"請求先マスタ" & i.ToString}, i.ToString, "keyたいとる")
        '    Else

        '    End If

        'Next

        'If IsMessageStoreExist(3) = True Then

        'End If

        Return ds

    End Function

End Class
