Public Class Page
    Public cp As ChartPainting
    Public QuotesPctBox As PictureBox
    Public PricesQuotesPctBox As PictureBox
    Public TimesQuotesPctBox As PictureBox
    Public TradesPctBox As PictureBox
    Public PricesTradesPctBox As PictureBox
    Public TimesTradesPctBox As PictureBox
    Public LeftQuotesButton As Button
    Public RightQuotesButton As Button
    Public PlusQuotesButton As Button
    Public MinusQuotesButton As Button
    Public LeftTradesButton As Button
    Public RightTradesButton As Button
    Public PlusTradesButton As Button
    Public MinusTradesButton As Button

    Public Sub New(cp As ChartPainting,
                   QuotesPctBox As PictureBox,
                   PricesQuotesPctBox As PictureBox,
                   TimesQuotesPctBox As PictureBox,
                   TradesPctBox As PictureBox,
                   PricesTradesPctBox As PictureBox,
                   TimesTradesPctBox As PictureBox,
                   LeftQuotesButton As Button,
                   RightQuotesButton As Button,
                   PlusQuotesButton As Button,
                   MinusQuotesButton As Button,
                   LeftTradesButton As Button,
                   RightTradesButton As Button,
                   PlusTradesButton As Button,
                   MinusTradesButton As Button)

        Me.cp = cp
        Me.QuotesPctBox = QuotesPctBox
        Me.PricesQuotesPctBox = PricesQuotesPctBox
        Me.TimesQuotesPctBox = TimesQuotesPctBox
        Me.TradesPctBox = TradesPctBox
        Me.PricesTradesPctBox = PricesTradesPctBox
        Me.TimesTradesPctBox = TimesTradesPctBox
        Me.LeftQuotesButton = LeftQuotesButton
        Me.RightQuotesButton = RightQuotesButton
        Me.PlusQuotesButton = PlusQuotesButton
        Me.MinusQuotesButton = MinusQuotesButton
        Me.LeftTradesButton = LeftTradesButton
        Me.RightTradesButton = RightTradesButton
        Me.PlusTradesButton = PlusTradesButton
        Me.MinusTradesButton = MinusTradesButton

    End Sub

    Public Sub OnMarketDataUpdate(quotesInfo As QuotesInfo)
        Dim currentMaxPriceQuotes As Double
        Dim currentMinPriceQuotes As Double
        If (quotesInfo.AskPrice IsNot Nothing And quotesInfo.BidPrice IsNot Nothing) Then
            'котировки
            If (quotesInfo.AskPrice > quotesInfo.BidPrice) Then
                currentMaxPriceQuotes = quotesInfo.AskPrice
                currentMinPriceQuotes = quotesInfo.BidPrice
            Else
                currentMaxPriceQuotes = quotesInfo.BidPrice
                currentMinPriceQuotes = quotesInfo.AskPrice
            End If

            If (currentMaxPriceQuotes > Me.cp.maxPriceQuotes) Then
                cp.maxPriceQuotes = currentMaxPriceQuotes
            End If

            If (currentMinPriceQuotes < cp.minPriceQuotes) Then
                Me.cp.minPriceQuotes = currentMinPriceQuotes
            End If

            Me.cp.pointsQuotes.Add(New PointQuotes(quotesInfo.AskPrice, quotesInfo.AskVolume, quotesInfo.BidPrice, quotesInfo.BidVolume, DateTime.Now))
            Me.QuotesPctBox.Invoke(Sub()
                                       If (Not Me.cp.isDrawingStartedQuotes) Then
                                           Me.cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
                                       End If
                                   End Sub)
        Else
            'сделки
            If (quotesInfo.TradePrice > cp.maxPriceTrades) Then
                cp.maxPriceTrades = quotesInfo.TradePrice
            End If

            If (quotesInfo.TradePrice < cp.minPriceTrades) Then
                cp.minPriceTrades = quotesInfo.TradePrice
            End If

            cp.pointsTrades.Add(New PointTrades(quotesInfo.TradePrice, quotesInfo.TradeVolume, DateTime.Now))
            Me.TradesPctBox.Invoke(Sub()
                                       Me.cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox)
                                   End Sub)
        End If
    End Sub
End Class