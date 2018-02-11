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
        If feedReciever IsNot Nothing Then
            feedReciever = Nothing
        End If
        feedReciever = New QuoteFixReciever(fixConfigPath, AddressOf CheckingState)
        InitializationMinAndMaxPrices()
    End Sub

    Sub CheckingState(state As Boolean, threadAlive As Boolean)
        If threadAlive Then
            Label1.Invoke(Sub()
                              If state = True Then
                                  Label1.Text = "OK"
                              Else
                                  Label1.Text = "Disconnect"
                              End If
                          End Sub)
        Else
            System.Windows.Forms.Application.ExitThread()
        End If

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim subscribes = feedReciever.GetSubscribeInfos()
        If subscribes.Count > 0 Then
            feedReciever.UnsubscribeForQuotes(subscribes(0))
        End If
        feedReciever.SubscribeForQuotes(ExanteIDTextBox.Text, AddressOf OnMarketDataUpdate)
    End Sub

    Sub OnMarketDataUpdate(quotesInfo As QuotesInfo)
        If (quotesInfo.AskPrice IsNot Nothing And quotesInfo.BidPrice IsNot Nothing) Then
            AskPriceTextBox.Invoke(Sub()
                                       AskPriceTextBox.Text = quotesInfo.AskPrice
                                   End Sub)
            AskVolumeTextBox.Invoke(Sub()
                                        AskVolumeTextBox.Text = quotesInfo.AskVolume
                                    End Sub)
            BidPriceTextBox.Invoke(Sub()
                                       BidPriceTextBox.Text = quotesInfo.BidPrice
                                   End Sub)
            BidVolumeTextBox.Invoke(Sub()
                                        BidVolumeTextBox.Text = quotesInfo.BidVolume
                                    End Sub)

            Dim currentMaxPrice As Double
            Dim currentMinPrice As Double
            If (quotesInfo.AskPrice > quotesInfo.BidPrice) Then
                currentMaxPrice = quotesInfo.AskPrice
                currentMinPrice = quotesInfo.BidPrice
            Else
                currentMaxPrice = quotesInfo.BidPrice
                currentMinPrice = quotesInfo.AskPrice
            End If

            If (currentMaxPrice > maxPrice) Then
                maxPrice = currentMaxPrice
            End If

            If (currentMinPrice < minPrice) Then
                minPrice = currentMinPrice
            End If

            Chart1.Invoke(Sub()
                              Me.Chart1.Series("AskPrice").Points.AddXY(DateTime.Now.ToLongTimeString, quotesInfo.AskPrice)
                              Me.Chart1.Series("BidPrice").Points.AddXY(DateTime.Now.ToLongTimeString, quotesInfo.BidPrice)
                              Me.Chart1.ChartAreas(0).AxisY.Minimum = minPrice - 10.0
                              Me.Chart1.ChartAreas(0).AxisY.Maximum = maxPrice + 10.0
                          End Sub)

            Chart2.Invoke(Sub()
                              Me.Chart2.Series("AskVolume").Points.AddXY(DateTime.Now.ToLongTimeString, quotesInfo.AskVolume)
                              Me.Chart2.Series("BidVolume").Points.AddXY(DateTime.Now.ToLongTimeString, quotesInfo.BidVolume)
                          End Sub)
        Else
            TradePriceTextBox.Invoke(Sub()
                                         TradePriceTextBox.Text = quotesInfo.TradePrice
                                     End Sub)
            TradeVolumeTextBox.Invoke(Sub()
                                          TradeVolumeTextBox.Text = quotesInfo.TradeVolume
                                      End Sub)
        End If


        
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If feedReciever IsNot Nothing Then
            feedReciever.Logout()
        End If
        System.Windows.Forms.Application.Exit()
    End Sub
End Class
