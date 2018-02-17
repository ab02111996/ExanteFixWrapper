Imports QuickFix
Imports System.Threading
Public Class Form1
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Public cp As ChartPainting
    Public yRangePublic As Double
    Dim volume As Double = 0
    Dim currentMaxPriceQuotes As Double
    Dim currentMinPriceQuotes As Double
    Dim currentMaxPriceTrades As Double
    Dim currentMinPriceTrades As Double

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
        cp = New ChartPainting()
        Dim subscribes = feedReciever.GetSubscribeInfos()
        If subscribes.Count > 0 Then
            feedReciever.UnsubscribeForQuotes(subscribes(0))
        End If
        feedReciever.SubscribeForQuotes(ExanteIDTextBox.Text, AddressOf OnMarketDataUpdate)
    End Sub

    Sub OnMarketDataUpdate(quotesInfo As QuotesInfo)
        If (quotesInfo.AskPrice IsNot Nothing And quotesInfo.BidPrice IsNot Nothing) Then
            'котировки
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
                currentMaxPriceQuotes = quotesInfo.AskPrice
                currentMinPriceQuotes = quotesInfo.BidPrice
            Else
                currentMaxPriceQuotes = quotesInfo.BidPrice
                currentMinPriceQuotes = quotesInfo.AskPrice
            End If

            If (currentMaxPriceQuotes > cp.maxPriceQuotes) Then
                cp.maxPriceQuotes = currentMaxPriceQuotes
            End If

            If (currentMinPriceQuotes < cp.minPriceQuotes) Then
                cp.minPriceQuotes = currentMinPriceQuotes
            End If

            cp.pointsQuotes.Add(New PointQuotes(quotesInfo.AskPrice, quotesInfo.AskVolume, quotesInfo.BidPrice, quotesInfo.BidVolume, DateTime.Now))
            Me.Invoke(Sub()
                          cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
                      End Sub)

        Else
            'сделки
            TradePriceTextBox.Invoke(Sub()
                                         TradePriceTextBox.Text = quotesInfo.TradePrice
                                     End Sub)
            TradeVolumeTextBox.Invoke(Sub()
                                          TradeVolumeTextBox.Text = quotesInfo.TradeVolume
                                      End Sub)

            If (quotesInfo.TradePrice > cp.maxPriceTrades) Then
                cp.maxPriceTrades = quotesInfo.TradePrice
            End If

            If (quotesInfo.TradePrice < cp.minPriceTrades) Then
                cp.minPriceTrades = quotesInfo.TradePrice
            End If

            cp.pointsTrades.Add(New PointTrades(quotesInfo.TradePrice, quotesInfo.TradeVolume, DateTime.Now))
            Me.Invoke(Sub()
                          cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox)
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
        If (Not cp Is Nothing) Then
            Dim proportion As Double = cp.yRangeQuotes - (e.Y / QuotesPctBox.Height) * cp.yRangeQuotes
            Label4.Text = Format((cp.minPriceQuotes - cp.minPriceQuotes * 0.0025) + proportion, "0.00")

            Dim indexOfPoint = CInt(Math.Floor(e.X / cp.intervalQuotes))
            If (indexOfPoint < 0) Then
                indexOfPoint = 0
            End If
            If (indexOfPoint >= cp.pointsQuotes.Count) Then
                indexOfPoint = cp.pointsQuotes.Count - 1
                Label5.Text = cp.pointsQuotes(indexOfPoint).time.ToLongTimeString
            Else
                If (cp.currentPointQuotes + indexOfPoint > cp.pointsQuotes.Count) Then
                    Label5.Text = cp.pointsQuotes(cp.lastPointQuotes).time.ToLongTimeString

                Else
                    Label5.Text = cp.pointsQuotes(cp.currentPointQuotes + indexOfPoint).time.ToLongTimeString
                End If
            End If
        End If


    End Sub

    'left
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cp.currentPointQuotes = cp.currentPointQuotes - 10
        If (cp.currentPointQuotes < 0) Then
            cp.currentPointQuotes = 0
        End If
        cp.needRePaintingQuotes = False
        Try
            cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
        Catch ex As Exception
            cp.currentPointQuotes -= 1
            If (cp.currentPointQuotes < 0) Then
                cp.currentPointQuotes = 0
            End If
        End Try
    End Sub

    'rigth
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If (cp.pointsQuotes.Count > cp.pointsOnScreenQuotes) Then
            cp.currentPointQuotes = cp.currentPointQuotes + 10
            If (cp.currentPointQuotes + cp.pointsOnScreenQuotes > cp.pointsQuotes.Count) Then
                cp.currentPointQuotes = cp.pointsQuotes.Count - cp.pointsOnScreenQuotes
            End If

            If (Not cp.lastPointQuotes = cp.pointsQuotes.Count - 1) Then
                Try
                    cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
                Catch ex As Exception
                    cp.currentPointQuotes -= 1
                    If (cp.currentPointQuotes < 0) Then
                        cp.currentPointQuotes = 0
                    End If
                End Try
            Else
                cp.needRePaintingQuotes = True
            End If
        End If

    End Sub

    '+
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        cp.pointsOnScreenQuotes += 15
        If (cp.pointsOnScreenQuotes > cp.maxPointsOnScreenQuotes) Then
            cp.pointsOnScreenQuotes = cp.maxPointsOnScreenQuotes
        End If
        cp.needRePaintingQuotes = False
        If (cp.lastPointQuotes < cp.pointsQuotes.Count - 1) Then
            Try
                cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
            Catch ex As Exception
                cp.currentPointQuotes -= 1
                If (cp.currentPointQuotes < 0) Then
                    cp.currentPointQuotes = 0
                End If
            End Try
        Else
            cp.currentPointQuotes -= 7
            If (cp.currentPointQuotes < 0) Then
                cp.currentPointQuotes = 0
            End If
            Try
                cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
            Catch ex As Exception
                cp.currentPointQuotes -= 1
                If (cp.currentPointQuotes < 0) Then
                    cp.currentPointQuotes = 0
                End If
            End Try
        End If
    End Sub

    '-
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        cp.pointsOnScreenQuotes -= 15
        If (cp.pointsOnScreenQuotes < cp.minPointsOnScreenQuotes) Then
            cp.pointsOnScreenQuotes = cp.minPointsOnScreenQuotes
        End If
        cp.needRePaintingQuotes = False
        Try
            cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
        Catch ex As Exception
            cp.currentPointQuotes -= 1
            If (cp.currentPointQuotes < 0) Then
                cp.currentPointQuotes = 0
            End If
        End Try
    End Sub

    'left
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        cp.currentPointTrades = cp.currentPointTrades - 10
        If (cp.currentPointTrades < 0) Then
            cp.currentPointTrades = 0
        End If
        cp.needRePaintingTrades = False
        Try
            cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox)
        Catch ex As Exception
            cp.currentPointTrades -= 1
            If (cp.currentPointTrades < 0) Then
                cp.currentPointTrades = 0
            End If
        End Try
    End Sub

    'right
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If (cp.pointsTrades.Count > cp.pointsOnScreenTrades) Then
            cp.currentPointTrades = cp.currentPointTrades + 10
            If (cp.currentPointTrades + cp.pointsOnScreenTrades > cp.pointsTrades.Count) Then
                cp.currentPointTrades = cp.pointsTrades.Count - cp.pointsOnScreenTrades
            End If

            If (Not cp.lastPointTrades = cp.pointsTrades.Count - 1) Then
                Try
                    cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox)
                Catch ex As Exception
                    cp.currentPointTrades -= 1
                    If (cp.currentPointTrades < 0) Then
                        cp.currentPointTrades = 0
                    End If
                End Try
            Else
                cp.needRePaintingTrades = True
            End If
        End If
    End Sub

    '+
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        cp.pointsOnScreenTrades += 15
        If (cp.pointsOnScreenTrades > cp.maxPointsOnScreenTrades) Then
            cp.pointsOnScreenTrades = cp.maxPointsOnScreenTrades
        End If
        cp.needRePaintingTrades = False
        If (cp.lastPointTrades < cp.pointsTrades.Count - 1) Then
            Try
                cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox)
            Catch ex As Exception
                cp.currentPointTrades -= 1
                If (cp.currentPointTrades < 0) Then
                    cp.currentPointTrades = 0
                End If
            End Try
        Else
            cp.currentPointTrades -= 7
            If (cp.currentPointTrades < 0) Then
                cp.currentPointTrades = 0
            End If
            Try
                cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox)
            Catch ex As Exception
                cp.currentPointTrades -= 1
                If (cp.currentPointTrades < 0) Then
                    cp.currentPointTrades = 0
                End If
            End Try
        End If
    End Sub

    '-
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        cp.pointsOnScreenTrades -= 15
        If (cp.pointsOnScreenTrades < cp.minPointsOnScreenTrades) Then
            cp.pointsOnScreenTrades = cp.minPointsOnScreenTrades
        End If
        cp.needRePaintingTrades = False
        Try
            cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox)
        Catch ex As Exception
            cp.currentPointTrades -= 1
            If (cp.currentPointTrades < 0) Then
                cp.currentPointTrades = 0
            End If
        End Try
    End Sub

    Private Sub TradesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles TradesPctBox.MouseMove
        Dim proportion As Double = cp.yRangeTrades - (e.Y / TradesPctBox.Height) * cp.yRangeTrades
        Label4.Text = Format((cp.minPriceTrades - cp.minPriceTrades * 0.0025) + proportion, "0.00")

        Dim indexOfPoint = CInt(Math.Floor(e.X / cp.intervalTrades))
        If (indexOfPoint < 0) Then
            indexOfPoint = 0
        End If
        If (indexOfPoint >= cp.pointsTrades.Count) Then
            indexOfPoint = cp.pointsTrades.Count - 1
            Label5.Text = cp.pointsTrades(indexOfPoint).time.ToLongTimeString
        Else
            If (cp.currentPointTrades + indexOfPoint > cp.pointsTrades.Count) Then
                Label5.Text = cp.pointsTrades(cp.lastPointTrades).time.ToLongTimeString

            Else
                Label5.Text = cp.pointsTrades(cp.currentPointTrades + indexOfPoint).time.ToLongTimeString
            End If
        End If
    End Sub
End Class
