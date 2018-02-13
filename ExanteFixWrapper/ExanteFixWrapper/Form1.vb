Imports QuickFix
Imports System.Threading
Public Class Form1
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Public cp As ChartPainting
    Dim volume As Double = 0
    Dim currentMaxPrice As Double
    Dim currentMinPrice As Double


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If feedReciever IsNot Nothing Then
            feedReciever = Nothing
        End If
        feedReciever = New QuoteFixReciever(fixConfigPath, AddressOf CheckingState)
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
        cp = New ChartPainting(New List(Of Point), 20, 999999999, 0, 0)
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


            If (quotesInfo.AskPrice > quotesInfo.BidPrice) Then
                currentMaxPrice = quotesInfo.AskPrice
                currentMinPrice = quotesInfo.BidPrice
            Else
                currentMaxPrice = quotesInfo.BidPrice
                currentMinPrice = quotesInfo.AskPrice
            End If

            If (currentMaxPrice > cp.maxPrice) Then
                cp.maxPrice = currentMaxPrice
            End If

            If (currentMinPrice < cp.minPrice) Then
                cp.minPrice = currentMinPrice
            End If

            cp.points.Add(New Point(quotesInfo.AskPrice, quotesInfo.AskVolume, quotesInfo.BidPrice, quotesInfo.BidVolume, volume, DateTime.Now))
            cp.prePainting(QuotesPctBox.Width)
            cp.painting(QuotesPctBox, TimesPctBox, PricesPctBox)

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
        DoubleBuffered = True
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If feedReciever IsNot Nothing Then
            feedReciever.Logout()
        End If
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub QuotesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles QuotesPctBox.MouseMove
        Dim p As PointF = e.Location
        Label4.Text = e.X.ToString() + " " + e.Y.ToString()

    End Sub
End Class
