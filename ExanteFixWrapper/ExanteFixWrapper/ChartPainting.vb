Imports ExanteFixWrapper.Form1
Public Class ChartPainting
    Public pointsQuotes As List(Of PointQuotes)
    Public pointsTrades As List(Of PointTrades)
    'котировки
    Public pointsOnScreenQuotes As Integer
    Public minPriceQuotes As Double
    Public maxPriceQuotes As Double
    Public intervalQuotes As Double
    Public currentPointQuotes As Integer
    Public lastPointQuotes As Integer
    Public yRangeQuotes As Double
    Public needRePaintingQuotes As Boolean
    Public lowBorderQuotes As Double
    Public highBorderQuotes As Double
    Public maxPointsOnScreenQuotes As Integer
    Public minPointsOnScreenQuotes As Integer
    'сделки
    Public pointsOnScreenTrades As Integer
    Public minPriceTrades As Double
    Public maxPriceTrades As Double
    Public intervalTrades As Double
    Public currentPointTrades As Integer
    Public lastPointTrades As Integer
    Public yRangeTrades As Double
    Public needRePaintingTrades As Boolean
    Public lowBorderTrades As Double
    Public highBorderTrades As Double
    Public maxPointsOnScreenTrades As Integer
    Public minPointsOnScreenTrades As Integer

    Public Sub New()
        'котировки
        Me.pointsQuotes = New List(Of PointQuotes)
        Me.pointsOnScreenQuotes = 20
        Me.minPriceQuotes = 999999999
        Me.maxPriceQuotes = 0
        Me.currentPointQuotes = 0
        Me.needRePaintingQuotes = True
        Me.maxPointsOnScreenQuotes = 400
        Me.minPointsOnScreenQuotes = 10
        'сделки
        Me.pointsTrades = New List(Of PointTrades)
        Me.pointsOnScreenTrades = 20
        Me.minPriceTrades = 999999999
        Me.maxPriceTrades = 0
        Me.currentPointTrades = 0
        Me.needRePaintingTrades = True
        Me.maxPointsOnScreenTrades = 400
        Me.minPointsOnScreenTrades = 10
    End Sub

    Public Sub paintingQuotes(QuotesPctBox As PictureBox, TimesQuotesPctBox As PictureBox, PricesQuotesPctBox As PictureBox)

        If (needRePaintingQuotes) Then
            If (Me.pointsQuotes.Count > Me.pointsOnScreenQuotes) Then
                currentPointQuotes += 1
            End If
        End If

        If (Me.pointsQuotes.Count < pointsOnScreenQuotes) Then
            lastPointQuotes = Me.pointsQuotes.Count - 1
        Else
            lastPointQuotes = currentPointQuotes + pointsOnScreenQuotes - 1
        End If

        intervalQuotes = QuotesPctBox.Width / pointsOnScreenQuotes

        Dim G_Quotes As Graphics = QuotesPctBox.CreateGraphics
        G_Quotes.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim G_Times = TimesQuotesPctBox.CreateGraphics
        G_Times.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim brush As New SolidBrush(Color.Black)

        Dim font As New Font("Arial", 8, FontStyle.Regular)

        Dim G_Prices As Graphics = PricesQuotesPctBox.CreateGraphics
        G_Prices.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim P_RedLine As New Pen(Color.Red, 1)
        Dim P_BlueLine As New Pen(Color.Blue, 1)
        Dim P_GrayLine As New Pen(Color.Gray, 0.3)
        If (Me.pointsQuotes.Count > 1) Then
            G_Quotes.Clear(Color.White)
            G_Times.Clear(Color.White)
            G_Prices.Clear(Color.White)
            For index = Me.currentPointQuotes To Me.lastPointQuotes - 1
                highBorderQuotes = Me.maxPriceQuotes + Me.maxPriceQuotes * 0.0025
                lowBorderQuotes = Me.minPriceQuotes - Me.minPriceQuotes * 0.0025
                yRangeQuotes = highBorderQuotes - lowBorderQuotes
                Dim procentsAsk1 As Double = ((Me.pointsQuotes(index).askPrice - lowBorderQuotes) / yRangeQuotes)
                Dim procentsAsk2 As Double = ((Me.pointsQuotes(index + 1).askPrice - lowBorderQuotes) / yRangeQuotes)
                Dim procentsBid1 As Double = ((Me.pointsQuotes(index).bidPrice - lowBorderQuotes) / yRangeQuotes)
                Dim procentsBid2 As Double = ((Me.pointsQuotes(index + 1).bidPrice - lowBorderQuotes) / yRangeQuotes)
                Dim p1Ask As Drawing.PointF
                Dim p2Ask As Drawing.PointF
                Dim p1Bid As Drawing.PointF
                Dim p2Bid As Drawing.PointF
                p1Ask.X = (index - Me.currentPointQuotes) * Me.intervalQuotes
                p1Ask.Y = QuotesPctBox.Height - QuotesPctBox.Height * procentsAsk1
                p2Ask.X = (index + 1 - Me.currentPointQuotes) * Me.intervalQuotes
                p2Ask.Y = QuotesPctBox.Height - QuotesPctBox.Height * procentsAsk2
                p1Bid.X = (index - Me.currentPointQuotes) * Me.intervalQuotes
                p1Bid.Y = QuotesPctBox.Height - QuotesPctBox.Height * procentsBid1
                p2Bid.X = (index + 1 - Me.currentPointQuotes) * Me.intervalQuotes
                p2Bid.Y = QuotesPctBox.Height - QuotesPctBox.Height * procentsBid2

                If (index = Me.currentPointQuotes) Then
                    Dim p1 As Drawing.PointF = New PointF(0.0, QuotesPctBox.Height / 2)
                    Dim p2 As Drawing.PointF = New PointF(QuotesPctBox.Width * 1.0, QuotesPctBox.Height / 2)
                    G_Quotes.DrawLine(P_GrayLine, p1, p2)
                    p1 = New PointF(0.0, QuotesPctBox.Height * 0.25)
                    p2 = New PointF(QuotesPctBox.Width, QuotesPctBox.Height * 0.25)
                    G_Quotes.DrawLine(P_GrayLine, p1, p2)
                    p1 = New PointF(0.0, QuotesPctBox.Height * 0.75)
                    p2 = New PointF(QuotesPctBox.Width, QuotesPctBox.Height * 0.75)
                    G_Quotes.DrawLine(P_GrayLine, p1, p2)
                End If

                G_Quotes.DrawLine(P_RedLine, p1Ask, p2Ask)
                G_Quotes.DrawLine(P_BlueLine, p1Bid, p2Bid)

                If (Me.pointsOnScreenQuotes <= 20) Then
                    G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                    G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                Else
                    If (Me.pointsOnScreenQuotes > 20 And Me.pointsOnScreenQuotes <= 45) Then
                        If (index Mod 2 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreenQuotes > 45 And Me.pointsOnScreenQuotes <= 100) Then
                        If (index Mod 5 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreenQuotes > 100 And Me.pointsOnScreenQuotes <= 200) Then
                        If (index Mod 20 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreenQuotes > 200 And Me.pointsOnScreenQuotes <= 300) Then
                        If (index Mod 40 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreenQuotes > 300 And Me.pointsOnScreenQuotes <= 400) Then
                        If (index Mod 75 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                        End If
                    End If

                End If

                If (index = Me.lastPointQuotes - 1) Then
                    G_Prices.DrawString(Format(lowBorderQuotes, "0.00"), font, brush, PricesQuotesPctBox.Width / 2 - 15, PricesQuotesPctBox.Height - 12)
                    G_Prices.DrawString(Format(highBorderQuotes, "0.00"), font, brush, PricesQuotesPctBox.Width / 2 - 15, 7)
                    G_Prices.DrawString(Format((lowBorderQuotes + highBorderQuotes) / 2, "0.00"), font, brush, PricesQuotesPctBox.Width / 2 - 15, PricesQuotesPctBox.Height / 2)
                    G_Prices.DrawString(Format(lowBorderQuotes + yRangeQuotes * 0.25, "0.00"), font, brush, PricesQuotesPctBox.Width / 2 - 15, PricesQuotesPctBox.Height * 0.75)
                    G_Prices.DrawString(Format(lowBorderQuotes + yRangeQuotes * 0.75, "0.00"), font, brush, PricesQuotesPctBox.Width / 2 - 15, PricesQuotesPctBox.Height * 0.25)
                End If

            Next
        End If

    End Sub

    Public Sub paintingTrades(TradesPctBox As PictureBox, TimesTradesPctBox As PictureBox, PricesTradesPctBox As PictureBox)

        If (needRePaintingTrades) Then
            If (Me.pointsTrades.Count > Me.pointsOnScreenTrades) Then
                currentPointTrades += 1
            End If
        End If

        If (Me.pointsTrades.Count < pointsOnScreenTrades) Then
            lastPointTrades = Me.pointsTrades.Count - 1
        Else
            lastPointTrades = currentPointTrades + pointsOnScreenTrades - 1
        End If

        intervalTrades = TradesPctBox.Width / pointsOnScreenTrades

        Dim G_Trades As Graphics = TradesPctBox.CreateGraphics
        G_Trades.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim G_Times = TimesTradesPctBox.CreateGraphics
        G_Times.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim brush As New SolidBrush(Color.Black)

        Dim font As New Font("Arial", 8, FontStyle.Regular)

        Dim G_Prices As Graphics = PricesTradesPctBox.CreateGraphics
        G_Prices.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim P_RedLine As New Pen(Color.Red, 1)
        Dim P_BlueLine As New Pen(Color.Blue, 1)
        Dim P_GrayLine As New Pen(Color.Gray, 0.3)
        If (Me.pointsTrades.Count > 1) Then
            G_Trades.Clear(Color.White)
            G_Times.Clear(Color.White)
            G_Prices.Clear(Color.White)
            For index = Me.currentPointTrades To Me.lastPointTrades - 1
                highBorderTrades = Me.maxPriceTrades + Me.maxPriceTrades * 0.0025
                lowBorderTrades = Me.minPriceTrades - Me.minPriceTrades * 0.0025
                yRangeTrades = highBorderTrades - lowBorderTrades
                Dim procents1 As Double = ((Me.pointsTrades(index).tradePrice - lowBorderTrades) / yRangeTrades)
                Dim procents2 As Double = ((Me.pointsTrades(index + 1).tradePrice - lowBorderTrades) / yRangeTrades)
                Dim p1Trades As Drawing.PointF
                Dim p2Trades As Drawing.PointF

                p1Trades.X = (index - Me.currentPointTrades) * Me.intervalTrades
                p1Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents1
                p2Trades.X = (index + 1 - Me.currentPointTrades) * Me.intervalTrades
                p2Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents2


                If (index = Me.currentPointTrades) Then
                    Dim p1 As Drawing.PointF = New PointF(0.0, TradesPctBox.Height / 2)
                    Dim p2 As Drawing.PointF = New PointF(TradesPctBox.Width * 1.0, TradesPctBox.Height / 2)
                    G_Trades.DrawLine(P_GrayLine, p1, p2)
                    p1 = New PointF(0.0, TradesPctBox.Height * 0.25)
                    p2 = New PointF(TradesPctBox.Width, TradesPctBox.Height * 0.25)
                    G_Trades.DrawLine(P_GrayLine, p1, p2)
                    p1 = New PointF(0.0, TradesPctBox.Height * 0.75)
                    p2 = New PointF(TradesPctBox.Width, TradesPctBox.Height * 0.75)
                    G_Trades.DrawLine(P_GrayLine, p1, p2)
                End If

                If (Me.pointsTrades(index).tradePrice < Me.pointsTrades(index + 1).tradePrice) Then
                    G_Trades.DrawLine(P_BlueLine, p1Trades, p2Trades)
                Else
                    G_Trades.DrawLine(P_RedLine, p1Trades, p2Trades)
                End If


                If (Me.pointsOnScreenTrades <= 20) Then
                    G_Trades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                    G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                Else
                    If (Me.pointsOnScreenTrades > 20 And Me.pointsOnScreenTrades <= 45) Then
                        If (index Mod 2 = 0) Then
                            G_Trades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreenTrades > 45 And Me.pointsOnScreenTrades <= 100) Then
                        If (index Mod 5 = 0) Then
                            G_Trades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreenTrades > 100 And Me.pointsOnScreenTrades <= 200) Then
                        If (index Mod 20 = 0) Then
                            G_Trades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreenTrades > 200 And Me.pointsOnScreenTrades <= 300) Then
                        If (index Mod 40 = 0) Then
                            G_Trades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreenTrades > 300 And Me.pointsOnScreenTrades <= 400) Then
                        If (index Mod 75 = 0) Then
                            G_Trades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                        End If
                    End If

                End If

                If (index = Me.lastPointTrades - 1) Then
                    G_Prices.DrawString(Format(lowBorderTrades, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height - 12)
                    G_Prices.DrawString(Format(highBorderTrades, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, 7)
                    G_Prices.DrawString(Format((lowBorderTrades + highBorderTrades) / 2, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height / 2)
                    G_Prices.DrawString(Format(lowBorderTrades + yRangeTrades * 0.25, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.75)
                    G_Prices.DrawString(Format(lowBorderTrades + yRangeTrades * 0.75, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.25)
                End If

            Next
        End If

    End Sub
End Class