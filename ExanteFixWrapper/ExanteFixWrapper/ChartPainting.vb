Imports ExanteFixWrapper.Form1
Public Class ChartPainting
    Public points As List(Of Point)
    Public pointsOnScreen As Integer
    Public minPrice As Double
    Public maxPrice As Double
    Public interval As Double
    Public currentPoint As Integer
    Public lastPoint As Integer
    Public yRange As Double
    Public needRePainting As Boolean
    Public lowBorder As Double
    Public highBorder As Double
    Public maxPointsOnScreen As Integer
    Public minPointsOnScreen As Integer

    Public Sub New()
        Me.points = New List(Of Point)
        Me.pointsOnScreen = 20
        Me.minPrice = 999999999
        Me.maxPrice = 0
        Me.currentPoint = 0
        Me.needRePainting = True
        Me.maxPointsOnScreen = 400
        Me.minPointsOnScreen = 10
    End Sub

    Public Sub painting(QuotesPctBox As PictureBox, TimesPctBox As PictureBox, PricesPctBox As PictureBox)
        'lastPoint = Me.points.Count

        If (needRePainting) Then
            If (Me.points.Count > Me.pointsOnScreen) Then
                currentPoint += 1
            End If
        End If

        If (Me.points.Count < pointsOnScreen) Then
            lastPoint = Me.points.Count - 1
        Else
            lastPoint = currentPoint + pointsOnScreen - 1
        End If

        interval = QuotesPctBox.Width / pointsOnScreen

        Dim G_Quotes As Graphics = QuotesPctBox.CreateGraphics
        G_Quotes.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim G_Times = TimesPctBox.CreateGraphics
        G_Times.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim brush As New SolidBrush(Color.Black)

        Dim font As New Font("Arial", 8, FontStyle.Regular)

        Dim G_Prices As Graphics = PricesPctBox.CreateGraphics
        G_Prices.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim P_Ask As New Pen(Color.Red, 1)
        Dim P_Bid As New Pen(Color.Blue, 1)
        Dim P_GrayLine As New Pen(Color.Gray, 0.3)
        If (Me.points.Count > 1) Then
            G_Quotes.Clear(Color.White)
            G_Times.Clear(Color.White)
            G_Prices.Clear(Color.White)
            For index = Me.currentPoint To Me.lastPoint - 1
                highBorder = Me.maxPrice + Me.maxPrice * 0.0025
                lowBorder = Me.minPrice - Me.minPrice * 0.0025
                yRange = highBorder - lowBorder
                Dim procentsAsk1 As Double = ((Me.points(index).askPrice - lowBorder) / yRange)
                Dim procentsAsk2 As Double = ((Me.points(index + 1).askPrice - lowBorder) / yRange)
                Dim procentsBid1 As Double = ((Me.points(index).bidPrice - lowBorder) / yRange)
                Dim procentsBid2 As Double = ((Me.points(index + 1).bidPrice - lowBorder) / yRange)
                Dim p1Ask As Drawing.PointF
                Dim p2Ask As Drawing.PointF
                Dim p1Bid As Drawing.PointF
                Dim p2Bid As Drawing.PointF
                p1Ask.X = (index - Me.currentPoint) * Me.interval
                p1Ask.Y = QuotesPctBox.Height - QuotesPctBox.Height * procentsAsk1
                p2Ask.X = (index + 1 - Me.currentPoint) * Me.interval
                p2Ask.Y = QuotesPctBox.Height - QuotesPctBox.Height * procentsAsk2
                p1Bid.X = (index - Me.currentPoint) * Me.interval
                p1Bid.Y = QuotesPctBox.Height - QuotesPctBox.Height * procentsBid1
                p2Bid.X = (index + 1 - Me.currentPoint) * Me.interval
                p2Bid.Y = QuotesPctBox.Height - QuotesPctBox.Height * procentsBid2

                If (index = Me.currentPoint) Then
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

                G_Quotes.DrawLine(P_Ask, p1Ask, p2Ask)
                G_Quotes.DrawLine(P_Bid, p1Bid, p2Bid)

                If (Me.pointsOnScreen <= 20) Then
                    G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                    G_Times.DrawString(Me.points(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesPctBox.Height / 2)
                Else
                    If (Me.pointsOnScreen > 20 And Me.pointsOnScreen <= 45) Then
                        If (index Mod 2 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.points(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreen > 45 And Me.pointsOnScreen <= 100) Then
                        If (index Mod 5 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.points(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreen > 100 And Me.pointsOnScreen <= 200) Then
                        If (index Mod 20 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.points(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreen > 200 And Me.pointsOnScreen <= 300) Then
                        If (index Mod 40 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.points(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesPctBox.Height / 2)
                        End If
                    ElseIf (Me.pointsOnScreen > 300 And Me.pointsOnScreen <= 400) Then
                        If (index Mod 75 = 0) Then
                            G_Quotes.DrawLine(P_GrayLine, p1Ask.X, 0, p1Ask.X, QuotesPctBox.Height)
                            G_Times.DrawString(Me.points(index).time.ToLongTimeString, font, brush, p1Ask.X, TimesPctBox.Height / 2)
                        End If
                    End If

                End If

                If (index = Me.lastPoint - 1) Then
                    G_Prices.DrawString(Format(lowBorder, "0.00"), font, brush, PricesPctBox.Width / 2 - 15, PricesPctBox.Height - 12)
                    G_Prices.DrawString(Format(highBorder, "0.00"), font, brush, PricesPctBox.Width / 2 - 15, 7)
                    G_Prices.DrawString(Format((lowBorder + highBorder) / 2, "0.00"), font, brush, PricesPctBox.Width / 2 - 15, PricesPctBox.Height / 2)
                    G_Prices.DrawString(Format(lowBorder + yRange * 0.25, "0.00"), font, brush, PricesPctBox.Width / 2 - 15, PricesPctBox.Height * 0.75)
                    G_Prices.DrawString(Format(lowBorder + yRange * 0.75, "0.00"), font, brush, PricesPctBox.Width / 2 - 15, PricesPctBox.Height * 0.25)
                End If

            Next
        End If

    End Sub

End Class