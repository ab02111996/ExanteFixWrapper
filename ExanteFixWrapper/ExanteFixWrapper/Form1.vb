Imports QuickFix
Imports System.Threading
Public Class Form1
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Dim minPrice As Double
    Dim maxPrice As Double
    Sub InitializationMinAndMaxPrices()
        minPrice = 9999999
        maxPrice = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        feedReciever = New QuoteFixReciever(fixConfigPath, AddressOf CheckingState)
        InitializationMinAndMaxPrices()
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
        feedReciever.SubscribeForQuotes(ExanteIDTextBox.Text, AddressOf OnMarketDataUpdate)
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

        Dim currentMaxPrice As Double
        Dim currentMinPrice As Double
        If (askPrice > bidPrice) Then
            currentMaxPrice = askPrice
            currentMinPrice = bidPrice
        Else
            currentMaxPrice = bidPrice
            currentMinPrice = askPrice
        End If

        If (currentMaxPrice > maxPrice) Then
            maxPrice = currentMaxPrice
        End If

        If (currentMinPrice < minPrice) Then
            minPrice = currentMinPrice
        End If

        Chart1.Invoke(Sub()
                          Me.Chart1.Series("AskPrice").Points.AddXY(DateTime.Now.ToLongTimeString, askPrice)
                          Me.Chart1.Series("BidPrice").Points.AddXY(DateTime.Now.ToLongTimeString, bidPrice)
                          Me.Chart1.ChartAreas(0).AxisY.Minimum = minPrice - 10.0
                          Me.Chart1.ChartAreas(0).AxisY.Maximum = maxPrice + 10.0
                      End Sub)

        Chart2.Invoke(Sub()
                          Me.Chart2.Series("AskVolume").Points.AddXY(DateTime.Now.ToLongTimeString, askVolume)
                          Me.Chart2.Series("BidVolume").Points.AddXY(DateTime.Now.ToLongTimeString, bidVolume)
                      End Sub)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
