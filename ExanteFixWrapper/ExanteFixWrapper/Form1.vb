Imports QuickFix
Imports System.Threading
Public Class Form1
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        feedReciever = New QuoteFixReciever(fixConfigPath, AddressOf CheckingState)

    End Sub

    Sub CheckingState(state As Boolean)
        Label1.Invoke(Sub()
                          If state = True Then
                              Label1.Text = "OK"
                          Else
                              Label1.Text = "Disconnect"
                          End If
                      End Sub)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        feedReciever.SubscribeForQuotes("BTC.EXANTE", AddressOf OnMarketDataUpdate)
    End Sub

    Sub OnMarketDataUpdate(askPrice As Double, askVolume As Double, bidPrice As Double, bidVolume As Double, timeStamp As DateTime)
        AskPriceTextBox.Invoke(Sub()
                                   AskPriceTextBox.Text = askPrice
                               End Sub)
        AskVolumeTextBox.Invoke(Sub()
                                    AskVolumeTextBox.Text = askVolume
                                End Sub)
        BidPriceTextBox.Invoke(Sub()
                                   BidPriceTextBox.Text = bidPrice
                               End Sub)
        BidVolumeTextBox.Invoke(Sub()
                                    BidVolumeTextBox.Text = bidVolume
                                End Sub)
        
    End Sub

End Class
