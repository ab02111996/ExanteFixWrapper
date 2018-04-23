﻿Imports ExanteFixWrapper.Form1
'данный класс реализует логику отрисовки графиков
'класс содержит три метода - отрисовка котировок и отрисовка сделок для тикового и пятисекундного графика
Public Class ChartPainting
    Public pointsQuotes As List(Of PointQuotes) 'список котировок
    Public pointsTrades As List(Of PointTrades) 'список сделок
    Public pointsAverage As List(Of PointTrades)
    Public pointsTrades5sec As List(Of PointTradesNsec) 'список сделок для 5-секундного графика
    'Public pointsAverage5sec As List(Of PointTradesNsec)
    Public pointsTrades15sec As List(Of PointTradesNsec)
    'Public pointsAverage15sec As List(Of PointTradesNsec)
    Public pointsTrades30sec As List(Of PointTradesNsec)
    'Public pointsAverage30sec As List(Of PointTradesNsec)
    Public pointsTrades60sec As List(Of PointTradesNsec)
    'Public pointsAverage60sec As List(Of PointTradesNsec)
    Public pointsTrades300sec As List(Of PointTradesNsec)
    Public pointsTrades900sec As List(Of PointTradesNsec)
    Public pointsTrades1800sec As List(Of PointTradesNsec)
    Public pointsTrades3600sec As List(Of PointTradesNsec)
    'котировки - тики
    Public pointsOnScreenQuotes As Integer 'количество точек на экране
    Public intervalQuotes As Double 'интервал, приходящийся на одну точку графика в пикселях (по оси Х)
    Public currentPointQuotes As Integer 'первая точка графика на экране
    Public lastPointQuotes As Integer 'последняя точка графика на экране
    Public yRangeQuotes As Double 'интервал, приходящйся на ось Y в пикселях
    Public needRePaintingQuotes As Boolean 'нужно ли обновлять график (если да, то при каждом тике currentPointQuotes увеличивается на 1, график сдвигается вправо)
    Public lowBorderQuotes As Double 'нижняя граница в денежных единицах
    Public highBorderQuotes As Double 'верхняя граница в денежных единицах
    Public maxPointsOnScreenQuotes As Integer 'максимальное количество точек на экране
    Public minPointsOnScreenQuotes As Integer 'минимальное количество точек на экране
    'сделки - тики
    Public pointsOnScreenTrades As Integer
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
    Public pointsOnScreenTradesNsec As Integer
    Public intervalTradesNsec As Double
    Public currentPointTradesNsec As Integer
    Public lastPointTradesNsec As Integer
    Public yRangeTradesNsec As Double
    Public needRePaintingTradesNsec As Boolean
    Public lowBorderTradesNsec As Double
    Public highBorderTradesNsec As Double
    Public maxPointsOnScreenTradesNsec As Integer
    Public minPointsOnScreenTradesNsec As Integer
    Public maxVolumeTradesNsec As Double
    Public highBorderVolumesTradesNsec As Double
    Public yRangeVolumesTradesNsec As Double
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
    Public isNeedShowAvg As Boolean

    Public Sub New(usedForm As Form)
        'котировки - тики
        Me.pointsQuotes = New List(Of PointQuotes)
        Me.pointsOnScreenQuotes = 20
        Me.currentPointQuotes = 0
        Me.needRePaintingQuotes = True
        Me.maxPointsOnScreenQuotes = 400
        Me.minPointsOnScreenQuotes = 10
        'сделки - тики
        Me.pointsTrades = New List(Of PointTrades)
        Me.pointsAverage = New List(Of PointTrades)
        Me.pointsOnScreenTrades = 40
        Me.currentPointTrades = 0
        Me.needRePaintingTrades = True
        Me.maxPointsOnScreenTrades = 400
        Me.minPointsOnScreenTrades = 10
        Me.maxVolumeTrades = 0
        'сделки - N секунд
        Me.pointsTrades5sec = New List(Of PointTradesNsec)
        Me.pointsTrades15sec = New List(Of PointTradesNsec)
        Me.pointsTrades30sec = New List(Of PointTradesNsec)
        Me.pointsTrades60sec = New List(Of PointTradesNsec)
        Me.pointsTrades300sec = New List(Of PointTradesNsec)
        Me.pointsTrades900sec = New List(Of PointTradesNsec)
        Me.pointsTrades1800sec = New List(Of PointTradesNsec)
        Me.pointsTrades3600sec = New List(Of PointTradesNsec)
        Me.pointsOnScreenTradesNsec = 40
        Me.currentPointTradesNsec = 0
        Me.needRePaintingTradesNsec = True
        Me.maxPointsOnScreenTradesNsec = 400
        Me.minPointsOnScreenTradesNsec = 10
        Me.maxVolumeTradesNsec = 0
        If (Not Form1.isOnline) Then
            Me.currentPointTradesNsec = 0
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

                    If (Me.isNeedShowAvg) Then
                        procents1 = ((Me.pointsAverage(index).tradePrice - lowBorderTrades) / yRangeTrades)
                        procents2 = ((Me.pointsAverage(index + 1).tradePrice - lowBorderTrades) / yRangeTrades)
                        Dim p1Avg As PointF
                        Dim p2Avg As PointF
                        p1Avg.X = (index - Me.currentPointTrades) * Me.intervalTrades
                        p1Avg.Y = TradesPctBox.Height - TradesPctBox.Height * procents1
                        p2Avg.X = (index + 1 - Me.currentPointTrades) * Me.intervalTrades
                        p2Avg.Y = TradesPctBox.Height - TradesPctBox.Height * procents2
                        G_btmTrades.DrawLine(New Pen(Color.LightPink), p1Avg, p2Avg)
                    End If

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
                        G_Prices.DrawString(Format(lowBorderTrades + yRangeTrades * 0.75, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.25)
                        G_Prices.DrawString(Format(lowBorderTrades, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height - 12)
                        G_Prices.DrawString(Format(highBorderTrades, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, 7)
                        G_Prices.DrawString(Format((lowBorderTrades + highBorderTrades) / 2, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height / 2)
                        G_Prices.DrawString(Format(lowBorderTrades + yRangeTrades * 0.25, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.75)
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

    Public Sub paintingTradesNsec(TradesPctBox As PictureBox, TimesTradesPctBox As PictureBox, PricesTradesPctBox As PictureBox, VolumesTradesPctBox As PictureBox, VolumesVolumesTradesPctBox As PictureBox, N As Integer)
        Try
            Dim pointsTradesNsec As List(Of PointTradesNsec)
            Select Case N
                Case 5
                    pointsTradesNsec = Me.pointsTrades5sec
                Case 15
                    pointsTradesNsec = Me.pointsTrades15sec
                Case 30
                    pointsTradesNsec = Me.pointsTrades30sec
                Case 60
                    pointsTradesNsec = Me.pointsTrades60sec
                Case 300
                    pointsTradesNsec = Me.pointsTrades300sec
                Case 900
                    pointsTradesNsec = Me.pointsTrades900sec
                Case 1800
                    pointsTradesNsec = Me.pointsTrades1800sec
                Case 3600
                    pointsTradesNsec = Me.pointsTrades3600sec
                Case Else
                    pointsTradesNsec = Me.pointsTrades5sec
            End Select

            If (Not Form1.isOnline) Then
                needRePaintingTradesNsec = False
            End If

            If (needRePaintingTradesNsec) Then
                If (pointsTradesNsec.Count > Me.pointsOnScreenTradesNsec) Then
                    currentPointTradesNsec += 1
                End If
            End If

            If (pointsTradesNsec.Count < pointsOnScreenTradesNsec) Then
                lastPointTradesNsec = pointsTradesNsec.Count - 1
            Else
                lastPointTradesNsec = currentPointTradesNsec + pointsOnScreenTradesNsec - 1
            End If

            intervalTradesNsec = TradesPctBox.Width / pointsOnScreenTradesNsec

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

            If (pointsTradesNsec.Count > 0) Then
                TimesTradesPctBox.Refresh()
                PricesTradesPctBox.Refresh()
                VolumesVolumesTradesPctBox.Refresh()

                For index = Me.currentPointTradesNsec To Me.lastPointTradesNsec - 1
                    If (index = Me.currentPointTradesNsec) Then
                        highBorderTradesNsec = pointsTradesNsec(index).closePrice
                        lowBorderTradesNsec = pointsTradesNsec(index).closePrice
                        highBorderVolumesTradesNsec = pointsTradesNsec(index).volumeBuy + pointsTradesNsec(index).volumeSell
                    Else
                        If (pointsTradesNsec(index).closePrice > highBorderTradesNsec) Then
                            highBorderTradesNsec = pointsTradesNsec(index).closePrice
                        End If
                        If (pointsTradesNsec(index).closePrice < lowBorderTradesNsec) Then
                            lowBorderTradesNsec = pointsTradesNsec(index).closePrice
                        End If
                        If (pointsTradesNsec(index).volumeBuy + pointsTradesNsec(index).volumeSell > highBorderVolumesTradesNsec) Then
                            highBorderVolumesTradesNsec = pointsTradesNsec(index).volumeBuy + pointsTradesNsec(index).volumeSell
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
                    For index = currentPointTradesNsec To lastPointTradesNsec
                        If (index = currentPointTradesNsec) Then
                            highBorderTradesNsec = pointsTradesNsec(index).closePrice
                            lowBorderTradesNsec = pointsTradesNsec(index).closePrice
                        Else
                            If (pointsTradesNsec(index).closePrice > highBorderTradesNsec) Then
                                highBorderTradesNsec = pointsTradesNsec(index).closePrice
                            End If
                            If (pointsTradesNsec(index).closePrice < lowBorderTradesNsec) Then
                                lowBorderTradesNsec = pointsTradesNsec(index).closePrice
                            End If
                        End If
                    Next
                    highBorderTradesNsec += highBorderTradesNsec * 0.0001
                    lowBorderTradesNsec -= lowBorderTradesNsec * 0.0001
                    yRangeTradesNsec = highBorderTradesNsec - lowBorderTradesNsec

                    For index = currentPointTradesNsec To lastPointTradesNsec - 1
                        If (pointsTradesNsec.Count > 1 And yRangeTradesNsec = 0) Then
                            Exit Sub
                        End If

                        Dim procents1 As Double = ((pointsTradesNsec(index).closePrice - lowBorderTradesNsec) / yRangeTradesNsec)
                        Dim procents2 As Double = ((pointsTradesNsec(index + 1).closePrice - lowBorderTradesNsec) / yRangeTradesNsec)
                        Dim p1Trades As Drawing.PointF
                        Dim p2Trades As Drawing.PointF

                        If (Not index = Me.lastPointTradesNsec) Then
                            p1Trades.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                            p1Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents1
                            p2Trades.X = (index + 1 - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                            p2Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents2

                            If (pointsTradesNsec(index).closePrice < pointsTradesNsec(index + 1).closePrice) Then
                                G_btmTrades.DrawLine(P_BlueLine, p1Trades, p2Trades)
                                Form1.TradePriceLabel.ForeColor = Color.Blue
                            Else
                                G_btmTrades.DrawLine(P_RedLine, p1Trades, p2Trades)
                                Form1.TradePriceLabel.ForeColor = Color.Red
                            End If
                        End If

                        yRangeVolumesTradesNsec = highBorderVolumesTradesNsec

                        Dim buyPlusSell As Boolean = False
                        Dim original As Boolean = False
                        Dim average As Boolean = False
                        If (isCloned) Then
                            buyPlusSell = CType(Me.usedForm, Form1Clone).BuyPlusSell.Checked
                            original = CType(Me.usedForm, Form1Clone).Original.Checked
                            average = CType(Me.usedForm, Form1Clone).Average.Checked
                        Else
                            buyPlusSell = CType(Me.usedForm, Form1).BuyPlusSell.Checked
                            original = CType(Me.usedForm, Form1).Original.Checked
                            average = CType(Me.usedForm, Form1).Average.Checked
                        End If

                        If (buyPlusSell) Then
                            If (original) Then
                                Dim procentsRectangle As Double = (pointsTradesNsec(index).volumeBuy + pointsTradesNsec(index).volumeSell) / yRangeVolumesTradesNsec
                                Dim rectangle As RectangleF
                                rectangle.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                rectangle.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsRectangle
                                rectangle.Height = VolumesTradesPctBox.Height * procentsRectangle
                                rectangle.Width = Me.intervalTradesNsec - 1
                                G_btmVolumes.FillRectangle(GreenBrush, rectangle)
                            End If
                            If (average) Then
                                If (Not index = Me.lastPointTradesNsec) Then
                                    Dim procentsAvg1 As Double = ((pointsTradesNsec(index).avgBuyPlusSell) / yRangeVolumesTradesNsec)
                                    Dim procentsAvg2 As Double = ((pointsTradesNsec(index + 1).avgBuyPlusSell) / yRangeVolumesTradesNsec)
                                    Dim p1Avg As Drawing.PointF
                                    Dim p2Avg As Drawing.PointF
                                    p1Avg.X = p1Trades.X
                                    p1Avg.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvg1
                                    p2Avg.X = p2Trades.X
                                    p2Avg.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvg2
                                    G_btmVolumes.DrawLine(New Pen(Color.LightPink), p1Avg, p2Avg)
                                End If
                            End If
                        Else
                            If (original) Then
                                If (Not index = Me.lastPointTradesNsec) Then
                                    Dim procentsSell1 As Double = ((pointsTradesNsec(index).volumeSell) / yRangeVolumesTradesNsec)
                                    Dim procentsSell2 As Double = ((pointsTradesNsec(index + 1).volumeSell) / yRangeVolumesTradesNsec)
                                    Dim procentsBuy1 As Double = ((pointsTradesNsec(index).volumeBuy) / yRangeVolumesTradesNsec)
                                    Dim procentsBuy2 As Double = ((pointsTradesNsec(index + 1).volumeBuy) / yRangeVolumesTradesNsec)
                                    Dim p1Sell As Drawing.PointF
                                    Dim p2Sell As Drawing.PointF
                                    Dim p1Buy As Drawing.PointF
                                    Dim p2Buy As Drawing.PointF
                                    p1Sell.X = p1Trades.X
                                    p1Sell.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsSell1
                                    p2Sell.X = p2Trades.X
                                    p2Sell.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsSell2
                                    p1Buy.X = p1Trades.X
                                    p1Buy.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsBuy1
                                    p2Buy.X = p2Trades.X
                                    p2Buy.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsBuy2
                                    G_btmVolumes.DrawLine(P_RedLine, p1Sell, p2Sell)
                                    G_btmVolumes.DrawLine(P_BlueLine, p1Buy, p2Buy)
                                End If
                            End If
                            If (average) Then
                                If (Not index = Me.lastPointTradesNsec) Then
                                    Dim procentsAvgBuy1 As Double = ((pointsTradesNsec(index).avgBuy) / yRangeVolumesTradesNsec)
                                    Dim procentsAvgBuy2 As Double = ((pointsTradesNsec(index + 1).avgBuy) / yRangeVolumesTradesNsec)
                                    Dim p1AvgBuy As Drawing.PointF
                                    Dim p2AvgBuy As Drawing.PointF
                                    p1AvgBuy.X = p1Trades.X
                                    p1AvgBuy.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvgBuy1
                                    p2AvgBuy.X = p2Trades.X
                                    p2AvgBuy.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvgBuy2
                                    G_btmVolumes.DrawLine(New Pen(Color.LightPink), p1AvgBuy, p2AvgBuy)

                                    Dim procentsAvgSell1 As Double = ((pointsTradesNsec(index).avgSell) / yRangeVolumesTradesNsec)
                                    Dim procentsAvgSell2 As Double = ((pointsTradesNsec(index + 1).avgSell) / yRangeVolumesTradesNsec)
                                    Dim p1AvgSell As Drawing.PointF
                                    Dim p2AvgSell As Drawing.PointF
                                    p1AvgSell.X = p1Trades.X
                                    p1AvgSell.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvgSell1
                                    p2AvgSell.X = p2Trades.X
                                    p2AvgSell.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvgSell2
                                    G_btmVolumes.DrawLine(New Pen(Color.LightBlue), p1AvgSell, p2AvgSell)
                                End If
                            End If
                        End If

                        If (index = Me.currentPointTradesNsec) Then
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

                        If (Not Form1.isOnline) Then
                            If (Not index = Me.lastPointTradesNsec) Then
                                If (Not pointsTradesNsec(index).time.Date = pointsTradesNsec(index + 1).time.Date) Then
                                    G_btmTrades.DrawLine(New Pen(Color.Orange, 2), p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_btmVolumes.DrawLine(New Pen(Color.Orange, 2), p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            End If
                        End If

                        If (index = Me.currentPointQuotes) Then
                            Dim p1 As Drawing.PointF = New PointF(0.0, VolumesTradesPctBox.Height / 2)
                            Dim p2 As Drawing.PointF = New PointF(VolumesTradesPctBox.Width * 1.0, VolumesTradesPctBox.Height / 2)
                            G_btmVolumes.DrawLine(P_GrayLine, p1, p2)
                        End If

                        If (Me.pointsOnScreenTradesNsec <= 20) Then
                            G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                            G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                        Else
                            If (Me.pointsOnScreenTradesNsec > 20 And Me.pointsOnScreenTradesNsec <= 45) Then
                                If (index Mod 2 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTradesNsec > 45 And Me.pointsOnScreenTradesNsec <= 100) Then
                                If (index Mod 5 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTradesNsec > 100 And Me.pointsOnScreenTradesNsec <= 200) Then
                                If (index Mod 20 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTradesNsec > 200 And Me.pointsOnScreenTradesNsec <= 300) Then
                                If (index Mod 40 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTradesNsec > 300 And Me.pointsOnScreenTradesNsec <= 400) Then
                                If (index Mod 75 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            End If
                        End If

                        If (index = Me.lastPointTradesNsec - 1) Then
                            G_Prices.DrawString(Format(lowBorderTradesNsec, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height - 12)
                            G_Prices.DrawString(Format(highBorderTradesNsec, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, 7)
                            G_Prices.DrawString(Format((lowBorderTradesNsec + highBorderTradesNsec) / 2, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height / 2)
                            G_Prices.DrawString(Format(lowBorderTradesNsec + yRangeTradesNsec * 0.25, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.75)
                            G_Prices.DrawString(Format(lowBorderTradesNsec + yRangeTradesNsec * 0.75, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.25)
                            G_VolumesVolumes.DrawString(Format(highBorderVolumesTradesNsec, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, 7)
                            G_VolumesVolumes.DrawString(Format(highBorderVolumesTradesNsec / 2, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, VolumesVolumesTradesPctBox.Height / 2)
                        End If
                    Next
                Else 'тип графика - японские свечи / бары
                    For index = Me.currentPointTradesNsec To Me.lastPointTradesNsec
                        If (index = Me.currentPointTradesNsec) Then
                            highBorderTradesNsec = pointsTradesNsec(index).highPrice
                            lowBorderTradesNsec = pointsTradesNsec(index).lowPrice
                        Else
                            If (pointsTradesNsec(index).highPrice > highBorderTradesNsec) Then
                                highBorderTradesNsec = pointsTradesNsec(index).highPrice
                            End If
                            If (pointsTradesNsec(index).lowPrice < lowBorderTradesNsec) Then
                                lowBorderTradesNsec = pointsTradesNsec(index).lowPrice
                            End If
                        End If
                    Next
                    highBorderTradesNsec += highBorderTradesNsec * 0.0001
                    lowBorderTradesNsec -= lowBorderTradesNsec * 0.0001
                    yRangeTradesNsec = highBorderTradesNsec - lowBorderTradesNsec

                    For index = Me.currentPointTradesNsec To Me.lastPointTradesNsec
                        If (pointsTradesNsec.Count > 1 And Me.yRangeTradesNsec = 0) Then
                            Exit Sub
                        End If
                        Dim procents1 As Double = ((pointsTradesNsec(index).highPrice - lowBorderTradesNsec) / yRangeTradesNsec)
                        Dim procents2 As Double = ((pointsTradesNsec(index).lowPrice - lowBorderTradesNsec) / yRangeTradesNsec)
                        Dim procents3 As Double = ((pointsTradesNsec(index).openPrice - lowBorderTradesNsec) / yRangeTradesNsec)
                        Dim procents4 As Double = ((pointsTradesNsec(index).closePrice - lowBorderTradesNsec) / yRangeTradesNsec)
                        Dim p1Trades As Drawing.PointF
                        Dim p2Trades As Drawing.PointF
                        Dim p3Trades As Drawing.PointF
                        Dim p4Trades As Drawing.PointF

                        p1Trades.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec + Me.intervalTradesNsec / 2
                        p1Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents1

                        p2Trades.X = p1Trades.X
                        p2Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents2

                        p3Trades.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                        p3Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents3

                        p4Trades.X = p3Trades.X
                        p4Trades.Y = TradesPctBox.Height - TradesPctBox.Height * procents4

                        yRangeVolumesTradesNsec = highBorderVolumesTradesNsec

                        Dim buyPlusSell As Boolean = False
                        Dim original As Boolean = False
                        Dim average As Boolean = False
                        If (isCloned) Then
                            buyPlusSell = CType(Me.usedForm, Form1Clone).BuyPlusSell.Checked
                            original = CType(Me.usedForm, Form1Clone).Original.Checked
                            average = CType(Me.usedForm, Form1Clone).Average.Checked
                        Else
                            buyPlusSell = CType(Me.usedForm, Form1).BuyPlusSell.Checked
                            original = CType(Me.usedForm, Form1).Original.Checked
                            average = CType(Me.usedForm, Form1).Average.Checked
                        End If

                        If (buyPlusSell) Then
                            If (original) Then
                                Dim procentsRectangle As Double = (pointsTradesNsec(index).volumeBuy + pointsTradesNsec(index).volumeSell) / yRangeVolumesTradesNsec
                                Dim rectangle As RectangleF
                                rectangle.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                rectangle.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsRectangle
                                rectangle.Height = VolumesTradesPctBox.Height * procentsRectangle
                                rectangle.Width = Me.intervalTradesNsec - 1
                                G_btmVolumes.FillRectangle(GreenBrush, rectangle)
                            End If
                            If (average) Then
                                If (Not index = Me.lastPointTradesNsec) Then
                                    Dim procentsAvg1 As Double = ((pointsTradesNsec(index).avgBuyPlusSell) / yRangeVolumesTradesNsec)
                                    Dim procentsAvg2 As Double = ((pointsTradesNsec(index + 1).avgBuyPlusSell) / yRangeVolumesTradesNsec)
                                    Dim p1Avg As Drawing.PointF
                                    Dim p2Avg As Drawing.PointF
                                    p1Avg.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p1Avg.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvg1
                                    p2Avg.X = (index + 1 - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p2Avg.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvg2
                                    G_btmVolumes.DrawLine(New Pen(Color.LightPink), p1Avg, p2Avg)
                                End If
                            End If
                        Else
                            If (original) Then
                                If (Not index = Me.lastPointTradesNsec) Then
                                    Dim procentsSell1 As Double = ((pointsTradesNsec(index).volumeSell) / yRangeVolumesTradesNsec)
                                    Dim procentsSell2 As Double = ((pointsTradesNsec(index + 1).volumeSell) / yRangeVolumesTradesNsec)
                                    Dim procentsBuy1 As Double = ((pointsTradesNsec(index).volumeBuy) / yRangeVolumesTradesNsec)
                                    Dim procentsBuy2 As Double = ((pointsTradesNsec(index + 1).volumeBuy) / yRangeVolumesTradesNsec)
                                    Dim p1Sell As Drawing.PointF
                                    Dim p2Sell As Drawing.PointF
                                    Dim p1Buy As Drawing.PointF
                                    Dim p2Buy As Drawing.PointF
                                    p1Sell.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p1Sell.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsSell1
                                    p2Sell.X = (index + 1 - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p2Sell.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsSell2
                                    p1Buy.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p1Buy.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsBuy1
                                    p2Buy.X = (index + 1 - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p2Buy.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsBuy2
                                    G_btmVolumes.DrawLine(P_RedLine, p1Sell, p2Sell)
                                    G_btmVolumes.DrawLine(P_BlueLine, p1Buy, p2Buy)
                                End If

                            End If
                            If (average) Then
                                If (Not index = Me.lastPointTradesNsec) Then
                                    Dim procentsAvgBuy1 As Double = ((pointsTradesNsec(index).avgBuy) / yRangeVolumesTradesNsec)
                                    Dim procentsAvgBuy2 As Double = ((pointsTradesNsec(index + 1).avgBuy) / yRangeVolumesTradesNsec)
                                    Dim p1AvgBuy As Drawing.PointF
                                    Dim p2AvgBuy As Drawing.PointF
                                    p1AvgBuy.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p1AvgBuy.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvgBuy1
                                    p2AvgBuy.X = (index + 1 - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p2AvgBuy.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvgBuy2
                                    G_btmVolumes.DrawLine(New Pen(Color.LightPink), p1AvgBuy, p2AvgBuy)

                                    Dim procentsAvgSell1 As Double = ((pointsTradesNsec(index).avgSell) / yRangeVolumesTradesNsec)
                                    Dim procentsAvgSell2 As Double = ((pointsTradesNsec(index + 1).avgSell) / yRangeVolumesTradesNsec)
                                    Dim p1AvgSell As Drawing.PointF
                                    Dim p2AvgSell As Drawing.PointF
                                    p1AvgSell.X = (index - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p1AvgSell.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvgSell1
                                    p2AvgSell.X = (index + 1 - Me.currentPointTradesNsec) * Me.intervalTradesNsec
                                    p2AvgSell.Y = VolumesTradesPctBox.Height - VolumesTradesPctBox.Height * procentsAvgSell2
                                    G_btmVolumes.DrawLine(New Pen(Color.LightBlue), p1AvgSell, p2AvgSell)
                                End If
                            End If
                        End If


                        If (index = Me.currentPointTradesNsec) Then
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

                        If (typeOfGraphic.SelectedItem = "Японские свечи") Then
                            Dim rectangleForCandle As RectangleF
                            If (p3Trades.Y > p4Trades.Y) Then
                                rectangleForCandle.X = p4Trades.X
                                rectangleForCandle.Y = p4Trades.Y
                                rectangleForCandle.Width = Me.intervalTradesNsec - 1
                                rectangleForCandle.Height = p3Trades.Y - p4Trades.Y
                                If (p3Trades.Y - p4Trades.Y < 2) Then
                                    rectangleForCandle.Height = 2
                                End If
                            Else
                                rectangleForCandle.X = p3Trades.X
                                rectangleForCandle.Y = p3Trades.Y
                                rectangleForCandle.Width = Me.intervalTradesNsec - 1
                                rectangleForCandle.Height = p4Trades.Y - p3Trades.Y
                                If (p4Trades.Y - p3Trades.Y < 2) Then
                                    rectangleForCandle.Height = 2
                                End If
                            End If

                            If (index = 0) Then
                                G_btmTrades.FillRectangle(Brushes.Red, rectangleForCandle)
                                G_btmTrades.DrawLine(New Pen(Color.Red, 2), p1Trades, p2Trades)
                            Else
                                If (pointsTradesNsec(index - 1).closePrice < pointsTradesNsec(index).closePrice) Then
                                    G_btmTrades.FillRectangle(Brushes.Red, rectangleForCandle)
                                    G_btmTrades.DrawLine(P_RedLine, p1Trades, p2Trades)
                                Else
                                    G_btmTrades.FillRectangle(Brushes.Blue, rectangleForCandle)
                                    G_btmTrades.DrawLine(P_BlueLine, p1Trades, p2Trades)
                                End If
                            End If
                        Else
                            If (index = 0) Then
                                G_btmTrades.DrawLine(New Pen(Color.Red, 2), p3Trades, New PointF(p3Trades.X + intervalTradesNsec / 2, p3Trades.Y))
                                G_btmTrades.DrawLine(New Pen(Color.Red, 2), New PointF(p4Trades.X + intervalTradesNsec / 2, p4Trades.Y), New PointF(p4Trades.X + intervalTradesNsec, p4Trades.Y))
                                G_btmTrades.DrawLine(New Pen(Color.Red, 2), p1Trades, p2Trades)
                            Else
                                If (pointsTradesNsec(index - 1).closePrice < pointsTradesNsec(index).closePrice) Then
                                    G_btmTrades.DrawLine(New Pen(Color.Red, 2), p3Trades, New PointF(p3Trades.X + intervalTradesNsec / 2, p3Trades.Y))
                                    G_btmTrades.DrawLine(New Pen(Color.Red, 2), New PointF(p4Trades.X + intervalTradesNsec / 2, p4Trades.Y), New PointF(p4Trades.X + intervalTradesNsec, p4Trades.Y))
                                    G_btmTrades.DrawLine(New Pen(Color.Red, 2), p1Trades, p2Trades)
                                Else
                                    G_btmTrades.DrawLine(New Pen(Color.Blue, 2), p3Trades, New PointF(p3Trades.X + intervalTradesNsec / 2, p3Trades.Y))
                                    G_btmTrades.DrawLine(New Pen(Color.Blue, 2), New PointF(p4Trades.X + intervalTradesNsec / 2, p4Trades.Y), New PointF(p4Trades.X + intervalTradesNsec, p4Trades.Y))
                                    G_btmTrades.DrawLine(New Pen(Color.Blue, 2), p1Trades, p2Trades)
                                End If
                            End If
                        End If

                        If (pointsTradesNsec.Count > 1) Then

                        End If

                        p1Trades.X -= intervalTradesNsec / 2

                        If (Not Form1.isOnline) Then
                            If (Not index = Me.lastPointTradesNsec) Then
                                If (Not pointsTradesNsec(index).time.Date = pointsTradesNsec(index + 1).time.Date) Then
                                    G_btmTrades.DrawLine(New Pen(Color.Orange, 2), p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_btmVolumes.DrawLine(New Pen(Color.Orange, 2), p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            End If
                        End If

                        If (Me.pointsOnScreenTradesNsec <= 20) Then
                            G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                            G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                            G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                        Else
                            If (Me.pointsOnScreenTradesNsec > 20 And Me.pointsOnScreenTradesNsec <= 45) Then
                                If (index Mod 2 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTradesNsec > 45 And Me.pointsOnScreenTradesNsec <= 100) Then
                                If (index Mod 5 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTradesNsec > 100 And Me.pointsOnScreenTradesNsec <= 200) Then
                                If (index Mod 20 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTradesNsec > 200 And Me.pointsOnScreenTradesNsec <= 300) Then
                                If (index Mod 40 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            ElseIf (Me.pointsOnScreenTradesNsec > 300 And Me.pointsOnScreenTradesNsec <= 400) Then
                                If (index Mod 75 = 0) Then
                                    G_btmTrades.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, TradesPctBox.Height)
                                    G_Times.DrawString(pointsTradesNsec(index).time.ToLongTimeString, font, brush, p1Trades.X, TimesTradesPctBox.Height / 2)
                                    G_btmVolumes.DrawLine(P_GrayLine, p1Trades.X, 0, p1Trades.X, VolumesTradesPctBox.Height)
                                End If
                            End If

                        End If

                        If (index = Me.lastPointTradesNsec - 1) Then
                            G_Prices.DrawString(Format(lowBorderTradesNsec, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height - 12)
                            G_Prices.DrawString(Format(highBorderTradesNsec, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, 7)
                            G_Prices.DrawString(Format((lowBorderTradesNsec + highBorderTradesNsec) / 2, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height / 2)
                            G_Prices.DrawString(Format(lowBorderTradesNsec + yRangeTradesNsec * 0.25, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.75)
                            G_Prices.DrawString(Format(lowBorderTradesNsec + yRangeTradesNsec * 0.75, "0.00"), font, brush, PricesTradesPctBox.Width / 2 - 15, PricesTradesPctBox.Height * 0.25)
                            G_VolumesVolumes.DrawString(Format(highBorderVolumesTradesNsec, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, 7)
                            G_VolumesVolumes.DrawString(Format(highBorderVolumesTradesNsec / 2, "0.00"), font, brush, VolumesVolumesTradesPctBox.Width / 2 - 15, VolumesVolumesTradesPctBox.Height / 2)
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
            Me.currentPointTradesNsec -= 1
            If (currentPointTradesNsec < 0) Then
                currentPointTradesNsec = 0
            End If
            If (Me.needRePaintingTradesNsec = False) Then
                Me.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, N)
            Else
                Me.needRePaintingTradesNsec = False
                Me.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, N)
                Me.needRePaintingTradesNsec = True
            End If
        End Try

    End Sub
End Class