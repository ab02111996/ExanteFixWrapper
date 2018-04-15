Imports ExanteFixWrapper.Form1
Public Class ChartPainting
    Public pointsQuotes As List(Of PointQuotes)
    Public pointsTrades As List(Of PointTrades)
    Public pointsTrades5sec As List(Of PointTrades5sec)
    'котировки - тики
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
    'сделки - тики
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
    Public maxVolumeTrades As Double
    Public highBorderVolumesTrades As Double
    Public yRangeVolumesTrades As Double
    'сделки - 5 секунд (линии)
    Public pointsOnScreenTrades5sec As Integer
    Public minPriceTrades5sec As Double
    Public maxPriceTrades5sec As Double
    Public intervalTrades5sec As Double
    Public currentPointTrades5sec As Integer
    Public lastPointTrades5sec As Integer
    Public yRangeTrades5sec As Double
    Public needRePaintingTrades5sec As Boolean
    Public lowBorderTrades5sec As Double
    Public highBorderTrades5sec As Double
    Public maxPointsOnScreenTrades5sec As Integer
    Public minPointsOnScreenTrades5sec As Integer
    Public maxVolumeTrades5sec As Double
    Public highBorderVolumesTrades5sec As Double
    Public yRangeVolumesTrades5sec As Double
    'рисование линии - котировки
    Public needDrawLineQuotes As Boolean
    Public point1Quotes As PointF
    Public point2Quotes As PointF
    Public isDrawingStartedQuotes As Boolean
    Public isLineReadyQuotes As Boolean
    'рисование линии - сделки
    Public needDrawLineTrades As Boolean
    Public point1Trades As PointF
    Public point2Trades As PointF
    Public isDrawingStartedTrades As Boolean
    Public isLineReadyTrades As Boolean

    Public isCloned As Boolean
    Public usedForm As Form
    Public isSubscribed As Boolean

    Public Sub New(usedForm As Form)
        'котировки - тики
        Me.pointsQuotes = New List(Of PointQuotes)
        Me.pointsOnScreenQuotes = 20
        Me.minPriceQuotes = 999999999
        Me.maxPriceQuotes = 0
        Me.currentPointQuotes = 0
        Me.needRePaintingQuotes = True
        Me.maxPointsOnScreenQuotes = 400
        Me.minPointsOnScreenQuotes = 10
        'сделки - тики
        Me.pointsTrades = New List(Of PointTrades)
        Me.pointsOnScreenTrades = 20
        Me.minPriceTrades = 999999999
        Me.maxPriceTrades = 0
        Me.currentPointTrades = 0
        Me.needRePaintingTrades = True
        Me.maxPointsOnScreenTrades = 400
        Me.minPointsOnScreenTrades = 10
        Me.maxVolumeTrades = 0
        'сделки - 5 секунд
        Me.pointsTrades5sec = New List(Of PointTrades5sec)
        Me.pointsOnScreenTrades5sec = 20
        Me.minPriceTrades5sec = 999999999
        Me.maxPriceTrades5sec = 0
        Me.currentPointTrades5sec = 0
        Me.needRePaintingTrades5sec = True
        Me.maxPointsOnScreenTrades5sec = 400
        Me.minPointsOnScreenTrades5sec = 10
        Me.maxVolumeTrades5sec = 0
        If (Not Form1.isOnline) Then
            Me.currentPointTrades5sec = 0
        End If
        'рисование линий
        Me.needDrawLineQuotes = False
        Me.isDrawingStartedQuotes = False
        Me.isLineReadyQuotes = False
        Me.needDrawLineTrades = False
        Me.isDrawingStartedTrades = False
        Me.isLineReadyTrades = False

        Me.isCloned = False
        Me.usedForm = usedForm
        Me.isSubscribed = False
    End Sub

    Public Sub paintingQuotes(QuotesPctBox As PictureBox, TimesQuotesPctBox As PictureBox, PricesQuotesPctBox As PictureBox)
        Try
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
            Dim G_Prices As Graphics = PricesQuotesPctBox.CreateGraphics
            G_Prices.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim btm As New Bitmap(QuotesPctBox.Width, QuotesPctBox.Height)
            Dim G_btm = Graphics.FromImage(btm)
            G_btm.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim brush As New SolidBrush(Color.Black)
            Dim redBrush As New SolidBrush(Color.Red)
            Dim blueBrush As New SolidBrush(Color.Blue)
            Dim font As New Font("Arial", 8, FontStyle.Regular)
            Dim P_RedLine As New Pen(Color.Red, 1)
            Dim P_BlueLine As New Pen(Color.Blue, 1)
            Dim P_GrayLine As New Pen(Color.Gray, 1)
            If (Me.pointsQuotes.Count > 1) Then

                TimesQuotesPctBox.Refresh()
                PricesQuotesPctBox.Refresh()

                For index = Me.currentPointQuotes To Me.lastPointQuotes - 1
                    If (index = Me.currentPointQuotes) Then
                        If (Me.pointsQuotes(index).askPrice > Me.pointsQuotes(index).bidPrice) Then
                            highBorderQuotes = Me.pointsQuotes(index).askPrice
                            lowBorderQuotes = Me.pointsQuotes(index).bidPrice
                        Else
                            highBorderQuotes = Me.pointsQuotes(index).bidPrice
                            lowBorderQuotes = Me.pointsQuotes(index).askPrice
                        End If
                    Else
                        If (Me.pointsQuotes(index).askPrice > highBorderQuotes) Then
                            highBorderQuotes = Me.pointsQuotes(index).askPrice
                        End If
                        If (Me.pointsQuotes(index).bidPrice > highBorderQuotes) Then
                            highBorderQuotes = Me.pointsQuotes(index).bidPrice
                        End If

                        If (Me.pointsQuotes(index).askPrice < lowBorderQuotes) Then
                            lowBorderQuotes = Me.pointsQuotes(index).askPrice
                        End If
                        If (Me.pointsQuotes(index).bidPrice < lowBorderQuotes) Then
                            lowBorderQuotes = Me.pointsQuotes(index).bidPrice
                        End If
                    End If
                Next

                highBorderQuotes += highBorderQuotes * 0.0001
                lowBorderQuotes -= lowBorderQuotes * 0.0001
                yRangeQuotes = highBorderQuotes - lowBorderQuotes

                For index = Me.currentPointQuotes To Me.lastPointQuotes - 1
                    If (Me.pointsQuotes.Count > 1 And Me.yRangeQuotes = 0) Then
                        Exit Sub
                    End If
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
                        G_btm.DrawLine(P_GrayLine, p1, p2)
                        p1 = New PointF(0.0, QuotesPctBox.Height * 0.25)
                        p2 = New PointF(QuotesPctBox.Width, QuotesPctBox.Height * 0.25)
                        G_btm.DrawLine(P_GrayLine, p1, p2)
                        p1 = New PointF(0.0, QuotesPctBox.Height * 0.75)
                        p2 = New PointF(QuotesPctBox.Width, QuotesPctBox.Height * 0.75)
                        G_btm.DrawLine(P_GrayLine, p1, p2)
                    End If

                    G_btm.DrawLine(P_RedLine, p1Ask, p2Ask)
                    G_btm.DrawLine(P_BlueLine, p1Bid, p2Bid)

                    If (Me.pointsOnScreenQuotes <= 20) Then
                        G_btm.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                        G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                    Else
                        If (Me.pointsOnScreenQuotes > 20 And Me.pointsOnScreenQuotes <= 45) Then
                            If (index Mod 2 = 0) Then
                                G_btm.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                                G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                            End If
                        ElseIf (Me.pointsOnScreenQuotes > 45 And Me.pointsOnScreenQuotes <= 100) Then
                            If (index Mod 5 = 0) Then
                                G_btm.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                                G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                            End If
                        ElseIf (Me.pointsOnScreenQuotes > 100 And Me.pointsOnScreenQuotes <= 200) Then
                            If (index Mod 20 = 0) Then
                                G_btm.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                                G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                            End If
                        ElseIf (Me.pointsOnScreenQuotes > 200 And Me.pointsOnScreenQuotes <= 300) Then
                            If (index Mod 40 = 0) Then
                                G_btm.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                                G_Times.DrawString(Me.pointsQuotes(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesQuotesPctBox.Height / 2)
                            End If
                        ElseIf (Me.pointsOnScreenQuotes > 300 And Me.pointsOnScreenQuotes <= 400) Then
                            If (index Mod 75 = 0) Then
                                G_btm.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
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

                QuotesPctBox.Refresh()
                QuotesPctBox.Image = btm

            End If

            If (Me.isLineReadyQuotes) Then
                Dim P_BlackLine As New Pen(Color.Black, 1)
                G_btm.DrawLine(P_BlackLine, Me.point1Quotes, Me.point2Quotes)
            End If
        Catch ex As Exception
            Me.currentPointQuotes -= 1
            If (Me.needRePaintingQuotes = False) Then
                Me.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
            Else
                Me.needRePaintingQuotes = False
                Me.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
                Me.needRePaintingQuotes = True
            End If
        End Try
    End Sub

    Public Sub paintingTrades(TradesPctBox As PictureBox, TimesTradesPctBox As PictureBox, PricesTradesPctBox As PictureBox, VolumesTradesPctBox As PictureBox, VolumesVolumesTradesPctBox As PictureBox)
        Try
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
            Dim G_Prices As Graphics = PricesTradesPctBox.CreateGraphics
            G_Prices.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim G_Volumes As Graphics = VolumesTradesPctBox.CreateGraphics
            G_Volumes.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim G_VolumesVolumes = VolumesVolumesTradesPctBox.CreateGraphics
            G_VolumesVolumes.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim btmTrades As New Bitmap(TradesPctBox.Width, TradesPctBox.Height)
            Dim G_btmTrades = Graphics.FromImage(btmTrades)
            G_btmTrades.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim btmVolumes As New Bitmap(VolumesTradesPctBox.Width, VolumesTradesPctBox.Height)
            Dim G_btmVolumes = Graphics.FromImage(btmVolumes)
            G_btmVolumes.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim brush As New SolidBrush(Color.Black)
            Dim font As New Font("Arial", 8, FontStyle.Regular)
            Dim P_RedLine As New Pen(Color.Red, 1)
            Dim P_BlueLine As New Pen(Color.Blue, 1)
            Dim P_GrayLine As New Pen(Color.Gray, 0.3)
            Dim GreenBrush As New SolidBrush(Color.Green)

            If (Me.pointsTrades.Count > 1) Then

                TimesTradesPctBox.Refresh()
                PricesTradesPctBox.Refresh()
                VolumesVolumesTradesPctBox.Refresh()

                For index = Me.currentPointTrades To Me.lastPointTrades - 1
                    If (index = Me.currentPointTrades) Then
                        highBorderTrades = Me.pointsTrades(index).tradePrice
                        lowBorderTrades = Me.pointsTrades(index).tradePrice
                        highBorderVolumesTrades = Me.pointsTrades(index).tradeVolume
                    Else
                        If (Me.pointsTrades(index).tradePrice > highBorderTrades) Then
                            highBorderTrades = Me.pointsTrades(index).tradePrice
                        End If
                        If (Me.pointsTrades(index).tradePrice < lowBorderTrades) Then
                            lowBorderTrades = Me.pointsTrades(index).tradePrice
                        End If
                        If (Me.pointsTrades(index).tradeVolume > highBorderVolumesTrades) Then
                            highBorderVolumesTrades = Me.pointsTrades(index).tradeVolume
                        End If
                    End If


                Next

                highBorderTrades += highBorderTrades * 0.0001
                lowBorderTrades -= lowBorderTrades * 0.0001
                yRangeTrades = highBorderTrades - lowBorderTrades

                For index = Me.currentPointTrades To Me.lastPointTrades - 1
                    If (Me.pointsTrades.Count > 1 And Me.yRangeTrades = 0) Then
                        Exit Sub
                    End If
                    Dim procents1 As Double = ((Me.pointsTrades(index).tradePrice - lowBorderTrades) / yRangeTrades)
                    Dim procents2 As Double = ((Me.pointsTrades(index + 1).tradePrice - lowBorderTrades) / yRangeTrades)
                    Dim p1Trades As Drawing.PointF
                    Dim p2Trades As Drawing.PointF

                    p1Trades.X = (index - Me.currentPointTrades) * Me.intervalTrades
                    p1Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents1
                    p2Trades.X = (index + 1 - Me.currentPointTrades) * Me.intervalTrades
                    p2Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents2

                    'highBorderVolumesTrades = Me.maxVolumeTrades '+ Me.maxVolumeTrades * 0.025
                    yRangeVolumesTrades = highBorderVolumesTrades
                    Dim procentsRectangle As Double = Me.pointsTrades(index).tradeVolume / yRangeVolumesTrades
                    Dim rectangle As RectangleF
                    rectangle.X = (index - Me.currentPointTrades) * Me.intervalTrades
                    rectangle.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsRectangle
                    rectangle.Height = VolumesTradesPctBox.Height * procentsRectangle
                    rectangle.Width = Me.intervalTrades - 1
                    G_btmVolumes.FillRectangle(GreenBrush, rectangle)

                    If (index = Me.currentPointTrades) Then
                        Dim p1 As Drawing.PointF = New PointF(0.0, TradesPctBox.Height / 2)
                        Dim p2 As Drawing.PointF = New PointF(TradesPctBox.Width * 1.0, TradesPctBox.Height / 2)
                        G_btmTrades.DrawLine(P_GrayLine, p1, p2)
                        p1 = New PointF(0.0, TradesPctBox.Height * 0.25)
                        p2 = New PointF(TradesPctBox.Width, TradesPctBox.Height * 0.25)
                        G_btmTrades.DrawLine(P_GrayLine, p1, p2)
                        p1 = New PointF(0.0, TradesPctBox.Height * 0.75)
                        p2 = New PointF(TradesPctBox.Width, TradesPctBox.Height * 0.75)
                        G_btmTrades.DrawLine(P_GrayLine, p1, p2)

                        p1 = New PointF(0.0, VolumesTradesPctBox.Height / 2)
                        p2 = New PointF(VolumesTradesPctBox.Width * 1.0, VolumesTradesPctBox.Height / 2)
                        G_btmVolumes.DrawLine(P_GrayLine, p1, p2)
                    End If

                    If (Me.pointsTrades(index).tradePrice < Me.pointsTrades(index + 1).tradePrice) Then
                        G_btmTrades.DrawLine(P_BlueLine, p1Trades, p2Trades)
                        Form1.TradePriceLabel.ForeColor = Color.Blue
                    Else
                        G_btmTrades.DrawLine(P_RedLine, p1Trades, p2Trades)
                        Form1.TradePriceLabel.ForeColor = Color.Red
                    End If

                    If (Me.pointsOnScreenTrades <= 20) Then
                        G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                        G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                        G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                    Else
                        If (Me.pointsOnScreenTrades > 20 And Me.pointsOnScreenTrades <= 45) Then
                            If (index Mod 2 = 0) Then
                                G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                            End If
                        ElseIf (Me.pointsOnScreenTrades > 45 And Me.pointsOnScreenTrades <= 100) Then
                            If (index Mod 5 = 0) Then
                                G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                            End If
                        ElseIf (Me.pointsOnScreenTrades > 100 And Me.pointsOnScreenTrades <= 200) Then
                            If (index Mod 20 = 0) Then
                                G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                            End If
                        ElseIf (Me.pointsOnScreenTrades > 200 And Me.pointsOnScreenTrades <= 300) Then
                            If (index Mod 40 = 0) Then
                                G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                            End If
                        ElseIf (Me.pointsOnScreenTrades > 300 And Me.pointsOnScreenTrades <= 400) Then
                            If (index Mod 75 = 0) Then
                                G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                G_Times.DrawString(Me.pointsTrades(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                            End If
                        End If

                    End If

                    If (index = Me.lastPointTrades - 1) Then
                        G_Prices.DrawString(Format(lowBorderTrades, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height - 12)
                        G_Prices.DrawString(Format(highBorderTrades, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, 7)
                        G_Prices.DrawString(Format((lowBorderTrades + highBorderTrades) / 2, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height / 2)
                        G_Prices.DrawString(Format(lowBorderTrades + yRangeTrades * 0.25, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.75)
                        G_Prices.DrawString(Format(lowBorderTrades + yRangeTrades * 0.75, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.25)
                        G_VolumesVolumes.DrawString(Format(highBorderVolumesTrades, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, 7)
                        G_VolumesVolumes.DrawString(Format(highBorderVolumesTrades / 2, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, VolumesVolumesTradesPctBox.Height / 2)
                    End If
                Next
                TradesPctBox.Image = btmTrades
                VolumesTradesPctBox.Image = btmVolumes
            End If

            If (Me.isLineReadyTrades) Then
                Dim P_BlackLine As New Pen(Color.Black, 1)
                G_btmTrades.DrawLine(P_BlackLine, Me.point1Trades, Me.point2Trades)
            End If
            TradesPctBox.Refresh()
            VolumesTradesPctBox.Refresh()
        Catch ex As Exception
            Me.currentPointTrades -= 1
            If (Me.currentPointTrades < 0) Then
                Me.currentPointTrades = 0
            End If
            If (Me.needRePaintingTrades = False) Then
                Me.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox)
            Else
                Me.needRePaintingTrades = False
                Me.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox)
                Me.needRePaintingTrades = True
            End If
        End Try

    End Sub

    Public Sub paintingTrades5sec(TradesPctBox As PictureBox, TimesTradesPctBox As PictureBox, PricesTradesPctBox As PictureBox, VolumesTradesPctBox As PictureBox, VolumesVolumesTradesPctBox As PictureBox)
        Try
            If (Not Form1.isOnline) Then
                needRePaintingTrades5sec = False
            End If

            If (needRePaintingTrades5sec) Then
                If (Me.pointsTrades5sec.Count > Me.pointsOnScreenTrades5sec) Then
                    currentPointTrades5sec += 1
                End If
            End If

            If (Me.pointsTrades5sec.Count < pointsOnScreenTrades5sec) Then
                lastPointTrades5sec = Me.pointsTrades5sec.Count - 1
            Else
                lastPointTrades5sec = currentPointTrades5sec + pointsOnScreenTrades5sec - 1
            End If

            intervalTrades5sec = TradesPctBox.Width / pointsOnScreenTrades5sec

            Dim G_Trades As Graphics = TradesPctBox.CreateGraphics
            G_Trades.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim G_Times = TimesTradesPctBox.CreateGraphics
            G_Times.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim G_Prices As Graphics = PricesTradesPctBox.CreateGraphics
            G_Prices.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim G_Volumes As Graphics = VolumesTradesPctBox.CreateGraphics
            G_Volumes.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim G_VolumesVolumes = VolumesVolumesTradesPctBox.CreateGraphics
            G_VolumesVolumes.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim btmTrades As New Bitmap(TradesPctBox.Width, TradesPctBox.Height)
            Dim G_btmTrades = Graphics.FromImage(btmTrades)
            G_btmTrades.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim btmVolumes As New Bitmap(VolumesTradesPctBox.Width, VolumesTradesPctBox.Height)
            Dim G_btmVolumes = Graphics.FromImage(btmVolumes)
            G_btmVolumes.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            Dim brush As New SolidBrush(Color.Black)
            Dim font As New Font("Arial", 8, FontStyle.Regular)
            Dim P_RedLine As New Pen(Color.Red, 1)
            Dim P_BlueLine As New Pen(Color.Blue, 1)
            Dim P_GrayLine As New Pen(Color.Gray, 0.3)
            Dim GreenBrush As New SolidBrush(Color.Green)

            If (Me.pointsTrades5sec.Count > 1) Then

                TimesTradesPctBox.Refresh()
                PricesTradesPctBox.Refresh()
                VolumesVolumesTradesPctBox.Refresh()

                For index = Me.currentPointTrades5sec To Me.lastPointTrades5sec - 1
                    If (index = Me.currentPointTrades5sec) Then
                        highBorderTrades5sec = Me.pointsTrades5sec(index).closePrice
                        lowBorderTrades5sec = Me.pointsTrades5sec(index).closePrice
                    Else
                        If (Me.pointsTrades5sec(index).closePrice > highBorderTrades5sec) Then
                            highBorderTrades5sec = Me.pointsTrades5sec(index).closePrice
                        End If
                        If (Me.pointsTrades5sec(index).closePrice < lowBorderTrades5sec) Then
                            lowBorderTrades5sec = Me.pointsTrades5sec(index).closePrice
                        End If
                    End If
                Next

                Dim typeOfGraphic As ComboBox
                If (isCloned) Then
                    typeOfGraphic = CType(Me.usedForm, Form1Clone).TypeOfGraphic
                Else
                    typeOfGraphic = CType(Me.usedForm, Form1).TypeOfGraphic
                End If

                If (typeOfGraphic.SelectedItem = "Линии") Then 'тип графика - линейный
                    For index = Me.currentPointTrades5sec To Me.lastPointTrades5sec - 1
                        If (index = Me.currentPointTrades5sec) Then
                            highBorderTrades5sec = Me.pointsTrades5sec(index).closePrice
                            lowBorderTrades5sec = Me.pointsTrades5sec(index).closePrice
                        Else
                            If (Me.pointsTrades5sec(index).closePrice > highBorderTrades5sec) Then
                                highBorderTrades5sec = Me.pointsTrades5sec(index).closePrice
                            End If
                            If (Me.pointsTrades5sec(index).closePrice < lowBorderTrades5sec) Then
                                lowBorderTrades5sec = Me.pointsTrades5sec(index).closePrice
                            End If
                        End If
                    Next
                    highBorderTrades5sec += highBorderTrades5sec * 0.0001
                    lowBorderTrades5sec -= lowBorderTrades5sec * 0.0001
                    yRangeTrades5sec = highBorderTrades5sec - lowBorderTrades5sec

                    For index = Me.currentPointTrades5sec To Me.lastPointTrades5sec - 1
                        If (Me.pointsTrades5sec.Count > 1 And Me.yRangeTrades5sec = 0) Then
                            Exit Sub
                        End If
                        Dim procents1 As Double = ((Me.pointsTrades5sec(index).closePrice - lowBorderTrades5sec) / yRangeTrades5sec)
                        Dim procents2 As Double = ((Me.pointsTrades5sec(index + 1).closePrice - lowBorderTrades5sec) / yRangeTrades5sec)
                        Dim p1Trades As Drawing.PointF
                        Dim p2Trades As Drawing.PointF

                        p1Trades.X = (index - Me.currentPointTrades5sec) * Me.intervalTrades5sec
                        p1Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents1
                        p2Trades.X = (index + 1 - Me.currentPointTrades5sec) * Me.intervalTrades5sec
                        p2Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents2

                        highBorderVolumesTrades5sec = Me.maxVolumeTrades5sec '+ Me.maxVolumeTrades * 0.025
                        yRangeVolumesTrades5sec = highBorderVolumesTrades5sec
                        Dim buy As Boolean = False
                        Dim sell As Boolean = False
                        If (isCloned) Then
                            buy = CType(Me.usedForm, Form1Clone).Buy.Checked
                            sell = CType(Me.usedForm, Form1Clone).Sell.Checked
                        Else
                            buy = CType(Me.usedForm, Form1).Buy.Checked
                            sell = CType(Me.usedForm, Form1).Sell.Checked
                        End If

                        Dim procentsRectangle As Double

                        If (Not buy And Not sell) Then
                            procentsRectangle = (Me.pointsTrades5sec(index).volumeBuy + Me.pointsTrades5sec(index).volumeSell) / yRangeVolumesTrades5sec
                        ElseIf (buy) Then
                            procentsRectangle = (Me.pointsTrades5sec(index).volumeBuy) / yRangeVolumesTrades5sec
                        ElseIf (sell) Then
                            procentsRectangle = (Me.pointsTrades5sec(index).volumeSell) / yRangeVolumesTrades5sec
                        End If
                        Dim rectangle As RectangleF
                        rectangle.X = (index - Me.currentPointTrades5sec) * Me.intervalTrades5sec
                        rectangle.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsRectangle
                        rectangle.Height = VolumesTradesPctBox.Height * procentsRectangle
                        rectangle.Width = Me.intervalTrades5sec - 1
                        G_btmVolumes.FillRectangle(GreenBrush, rectangle)

                        If (index = Me.currentPointTrades5sec) Then
                            Dim p1 As Drawing.PointF = New PointF(0.0, TradesPctBox.Height / 2)
                            Dim p2 As Drawing.PointF = New PointF(TradesPctBox.Width * 1.0, TradesPctBox.Height / 2)
                            G_btmTrades.DrawLine(P_GrayLine, p1, p2)
                            p1 = New PointF(0.0, TradesPctBox.Height * 0.25)
                            p2 = New PointF(TradesPctBox.Width, TradesPctBox.Height * 0.25)
                            G_btmTrades.DrawLine(P_GrayLine, p1, p2)
                            p1 = New PointF(0.0, TradesPctBox.Height * 0.75)
                            p2 = New PointF(TradesPctBox.Width, TradesPctBox.Height * 0.75)
                            G_btmTrades.DrawLine(P_GrayLine, p1, p2)

                            p1 = New PointF(0.0, VolumesTradesPctBox.Height / 2)
                            p2 = New PointF(VolumesTradesPctBox.Width * 1.0, VolumesTradesPctBox.Height / 2)
                            G_btmVolumes.DrawLine(P_GrayLine, p1, p2)
                        End If

                        If (Me.pointsTrades5sec(index).closePrice < Me.pointsTrades5sec(index + 1).closePrice) Then
                            G_btmTrades.DrawLine(P_BlueLine, p1Trades, p2Trades)
                            Form1.TradePriceLabel.ForeColor = Color.Blue
                        Else
                            G_btmTrades.DrawLine(P_RedLine, p1Trades, p2Trades)
                            Form1.TradePriceLabel.ForeColor = Color.Red
                        End If

                        If (Not Form1.isOnline) Then
                            If (Not Me.pointsTrades5sec(index).time.Date = Me.pointsTrades5sec(index + 1).time.Date) Then
                                G_btmTrades.DrawLine(New Pen(Color.Orange, 2), p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                G_btmVolumes.DrawLine(New Pen(Color.Orange, 2), p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                            End If
                        End If

                        If (index = Me.currentPointQuotes) Then
                            Dim p1 As Drawing.PointF = New PointF(0.0, VolumesTradesPctBox.Height / 2)
                            Dim p2 As Drawing.PointF = New PointF(VolumesTradesPctBox.Width * 1.0, VolumesTradesPctBox.Height / 2)
                            G_btmVolumes.DrawLine(P_GrayLine, p1, p2)

                        End If

                        If (Me.pointsOnScreenTrades5sec <= 20) Then
                            G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                            G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                        Else
                            If (Me.pointsOnScreenTrades5sec > 20 And Me.pointsOnScreenTrades5sec <= 45) Then
                                If (index Mod 2 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTrades5sec > 45 And Me.pointsOnScreenTrades5sec <= 100) Then
                                If (index Mod 5 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTrades5sec > 100 And Me.pointsOnScreenTrades5sec <= 200) Then
                                If (index Mod 20 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTrades5sec > 200 And Me.pointsOnScreenTrades5sec <= 300) Then
                                If (index Mod 40 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTrades5sec > 300 And Me.pointsOnScreenTrades5sec <= 400) Then
                                If (index Mod 75 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            End If

                        End If

                        If (index = Me.lastPointTrades5sec - 1) Then
                            G_Prices.DrawString(Format(lowBorderTrades5sec, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height - 12)
                            G_Prices.DrawString(Format(highBorderTrades5sec, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, 7)
                            G_Prices.DrawString(Format((lowBorderTrades5sec + highBorderTrades5sec) / 2, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height / 2)
                            G_Prices.DrawString(Format(lowBorderTrades5sec + yRangeTrades5sec * 0.25, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.75)
                            G_Prices.DrawString(Format(lowBorderTrades5sec + yRangeTrades5sec * 0.75, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.25)
                            G_VolumesVolumes.DrawString(Format(highBorderVolumesTrades5sec, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, 7)
                            G_VolumesVolumes.DrawString(Format(highBorderVolumesTrades5sec / 2, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, VolumesVolumesTradesPctBox.Height / 2)
                        End If
                    Next
                Else 'тип графика - японские свечи
                    For index = Me.currentPointTrades5sec To Me.lastPointTrades5sec - 1
                        If (index = Me.currentPointTrades5sec) Then
                            highBorderTrades5sec = Me.pointsTrades5sec(index).highPrice
                            lowBorderTrades5sec = Me.pointsTrades5sec(index).lowPrice
                        Else
                            If (Me.pointsTrades5sec(index).highPrice > highBorderTrades5sec) Then
                                highBorderTrades5sec = Me.pointsTrades5sec(index).highPrice
                            End If
                            If (Me.pointsTrades5sec(index).lowPrice < lowBorderTrades5sec) Then
                                lowBorderTrades5sec = Me.pointsTrades5sec(index).lowPrice
                            End If
                        End If
                    Next
                    highBorderTrades5sec += highBorderTrades5sec * 0.0001
                    lowBorderTrades5sec -= lowBorderTrades5sec * 0.0001
                    yRangeTrades5sec = highBorderTrades5sec - lowBorderTrades5sec

                    For index = Me.currentPointTrades5sec To Me.lastPointTrades5sec - 1
                        If (Me.pointsTrades5sec.Count > 1 And Me.yRangeTrades5sec = 0) Then
                            Exit Sub
                        End If
                        Dim procents1 As Double = ((Me.pointsTrades5sec(index).highPrice - lowBorderTrades5sec) / yRangeTrades5sec)
                        Dim procents2 As Double = ((Me.pointsTrades5sec(index).lowPrice - lowBorderTrades5sec) / yRangeTrades5sec)
                        Dim procents3 As Double = ((Me.pointsTrades5sec(index).openPrice - lowBorderTrades5sec) / yRangeTrades5sec)
                        Dim procents4 As Double = ((Me.pointsTrades5sec(index).closePrice - lowBorderTrades5sec) / yRangeTrades5sec)
                        Dim p1Trades As Drawing.PointF
                        Dim p2Trades As Drawing.PointF
                        Dim p3Trades As Drawing.PointF
                        Dim p4Trades As Drawing.PointF

                        p1Trades.X = (index - Me.currentPointTrades5sec) * Me.intervalTrades5sec + Me.intervalTrades5sec / 2
                        p1Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents1

                        p2Trades.X = p1Trades.X
                        p2Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents2

                        p3Trades.X = (index - Me.currentPointTrades5sec) * Me.intervalTrades5sec
                        p3Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents3

                        p4Trades.X = p3Trades.X
                        p4Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents4

                        highBorderVolumesTrades5sec = Me.maxVolumeTrades5sec '+ Me.maxVolumeTrades * 0.025
                        yRangeVolumesTrades5sec = highBorderVolumesTrades5sec

                        Dim buy As Boolean = False
                        Dim sell As Boolean = False
                        If (isCloned) Then
                            buy = CType(Me.usedForm, Form1Clone).Buy.Checked
                            sell = CType(Me.usedForm, Form1Clone).Sell.Checked
                        Else
                            buy = CType(Me.usedForm, Form1).Buy.Checked
                            sell = CType(Me.usedForm, Form1).Sell.Checked
                        End If

                        Dim procentsRectangle As Double

                        If (Not buy And Not sell) Then
                            procentsRectangle = (Me.pointsTrades5sec(index).volumeBuy + Me.pointsTrades5sec(index).volumeSell) / yRangeVolumesTrades5sec
                        ElseIf (buy) Then
                            procentsRectangle = (Me.pointsTrades5sec(index).volumeBuy) / yRangeVolumesTrades5sec
                        ElseIf (sell) Then
                            procentsRectangle = (Me.pointsTrades5sec(index).volumeSell) / yRangeVolumesTrades5sec
                        End If

                        Dim rectangle As RectangleF
                        rectangle.X = (index - Me.currentPointTrades5sec) * Me.intervalTrades5sec
                        rectangle.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsRectangle
                        rectangle.Height = VolumesTradesPctBox.Height * procentsRectangle
                        rectangle.Width = Me.intervalTrades5sec - 1
                        G_btmVolumes.FillRectangle(GreenBrush, rectangle)

                        If (index = Me.currentPointTrades5sec) Then
                            Dim p1 As Drawing.PointF = New PointF(0.0, TradesPctBox.Height / 2)
                            Dim p2 As Drawing.PointF = New PointF(TradesPctBox.Width * 1.0, TradesPctBox.Height / 2)
                            G_btmTrades.DrawLine(P_GrayLine, p1, p2)
                            p1 = New PointF(0.0, TradesPctBox.Height * 0.25)
                            p2 = New PointF(TradesPctBox.Width, TradesPctBox.Height * 0.25)
                            G_btmTrades.DrawLine(P_GrayLine, p1, p2)
                            p1 = New PointF(0.0, TradesPctBox.Height * 0.75)
                            p2 = New PointF(TradesPctBox.Width, TradesPctBox.Height * 0.75)
                            G_btmTrades.DrawLine(P_GrayLine, p1, p2)

                            p1 = New PointF(0.0, VolumesTradesPctBox.Height / 2)
                            p2 = New PointF(VolumesTradesPctBox.Width * 1.0, VolumesTradesPctBox.Height / 2)
                            G_btmVolumes.DrawLine(P_GrayLine, p1, p2)
                        End If

                        Dim rectangleForCandle As RectangleF
                        If (p3Trades.Y > p4Trades.Y) Then
                            rectangleForCandle.X = p4Trades.X
                            rectangleForCandle.Y = p4Trades.Y
                            rectangleForCandle.Width = Me.intervalTrades5sec - 1
                            rectangleForCandle.Height = p3Trades.Y - p4Trades.Y
                            If (p3Trades.Y - p4Trades.Y < 2) Then
                                rectangleForCandle.Height = 2
                            End If
                        Else
                            rectangleForCandle.X = p3Trades.X
                            rectangleForCandle.Y = p3Trades.Y
                            rectangleForCandle.Width = Me.intervalTrades5sec - 1
                            rectangleForCandle.Height = p4Trades.Y - p3Trades.Y
                            If (p4Trades.Y - p3Trades.Y < 2) Then
                                rectangleForCandle.Height = 2
                            End If
                        End If


                        If (index = 0) Then
                            G_btmTrades.FillRectangle(Brushes.Red, rectangleForCandle)
                            G_btmTrades.DrawLine(New Pen(Color.Red, 2), p1Trades, p2Trades)
                        Else
                            If (Me.pointsTrades5sec(index - 1).closePrice < Me.pointsTrades5sec(index).closePrice) Then
                                G_btmTrades.FillRectangle(Brushes.Red, rectangleForCandle)
                                G_btmTrades.DrawLine(P_RedLine, p1Trades, p2Trades)
                            Else
                                G_btmTrades.FillRectangle(Brushes.Blue, rectangleForCandle)
                                G_btmTrades.DrawLine(P_BlueLine, p1Trades, p2Trades)
                            End If
                        End If

                        If (Me.pointsTrades5sec.Count > 1) Then

                        End If

                        p1Trades.X -= intervalTrades5sec / 2

                        If (Not Form1.isOnline) Then
                            If (Not Me.pointsTrades5sec(index).time.Date = Me.pointsTrades5sec(index + 1).time.Date) Then
                                G_btmTrades.DrawLine(New Pen(Color.Orange, 2), p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                G_btmVolumes.DrawLine(New Pen(Color.Orange, 2), p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                            End If
                        End If

                        If (Me.pointsOnScreenTrades5sec <= 20) Then
                            G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                            G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                        Else
                            If (Me.pointsOnScreenTrades5sec > 20 And Me.pointsOnScreenTrades5sec <= 45) Then
                                If (index Mod 2 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTrades5sec > 45 And Me.pointsOnScreenTrades5sec <= 100) Then
                                If (index Mod 5 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTrades5sec > 100 And Me.pointsOnScreenTrades5sec <= 200) Then
                                If (index Mod 20 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTrades5sec > 200 And Me.pointsOnScreenTrades5sec <= 300) Then
                                If (index Mod 40 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTrades5sec > 300 And Me.pointsOnScreenTrades5sec <= 400) Then
                                If (index Mod 75 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(Me.pointsTrades5sec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            End If

                        End If

                        If (index = Me.lastPointTrades5sec - 1) Then
                            G_Prices.DrawString(Format(lowBorderTrades5sec, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height - 12)
                            G_Prices.DrawString(Format(highBorderTrades5sec, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, 7)
                            G_Prices.DrawString(Format((lowBorderTrades5sec + highBorderTrades5sec) / 2, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height / 2)
                            G_Prices.DrawString(Format(lowBorderTrades5sec + yRangeTrades5sec * 0.25, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.75)
                            G_Prices.DrawString(Format(lowBorderTrades5sec + yRangeTrades5sec * 0.75, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.25)
                            G_VolumesVolumes.DrawString(Format(highBorderVolumesTrades5sec, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, 7)
                            G_VolumesVolumes.DrawString(Format(highBorderVolumesTrades5sec / 2, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, VolumesVolumesTradesPctBox.Height / 2)
                        End If
                    Next

                End If

                TradesPctBox.Image = btmTrades
                VolumesTradesPctBox.Image = btmVolumes
            End If

            If (Me.isLineReadyTrades) Then
                Dim P_BlackLine As New Pen(Color.Black, 1)
                G_btmTrades.DrawLine(P_BlackLine, Me.point1Trades, Me.point2Trades)
            End If
            TradesPctBox.Refresh()
            VolumesTradesPctBox.Refresh()

        Catch ex As Exception
            Me.currentPointTrades5sec -= 1
            If (currentPointTrades5sec < 0) Then
                currentPointTrades5sec = 0
            End If
            If (Me.needRePaintingTrades5sec = False) Then
                Me.paintingTrades5sec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox)
            Else
                Me.needRePaintingTrades5sec = False
                Me.paintingTrades5sec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox)
                Me.needRePaintingTrades5sec = True
            End If
        End Try


    End Sub
End Class