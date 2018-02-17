Imports QuickFix
Imports System.Threading
Public Class Form1
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Public cp As ChartPainting
    Public yRangePublic As Double
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
        cp = New ChartPainting()
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

            cp.points.Add(New Point(quotesInfo.AskPrice, quotesInfo.AskVolume, quotesInfo.BidPrice, quotesInfo.BidVolume, volume, DateTime.Now, Nothing, Nothing))

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
        Try
            Dim proportion As Double = cp.yRange - (e.Y / QuotesPctBox.Height) * cp.yRange
            Label4.Text = Format((cp.minPrice - cp.minPrice * 0.0025) + proportion, "0.00")

            Dim indexOfPoint = CInt(Math.Floor(e.X / cp.interval))
            If (indexOfPoint < 0) Then
                indexOfPoint = 0
            End If
            If (indexOfPoint >= cp.points.Count) Then
                indexOfPoint = cp.points.Count - 1
                Label5.Text = cp.points(indexOfPoint).time.ToLongTimeString
            Else
                If (cp.currentPoint + indexOfPoint > cp.points.Count) Then
                    Label5.Text = cp.points(cp.lastPoint).time.ToLongTimeString

                Else
                    Label5.Text = cp.points(cp.currentPoint + indexOfPoint).time.ToLongTimeString
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    'left
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cp.currentPoint = cp.currentPoint - 10
        If (cp.currentPoint < 0) Then
            cp.currentPoint = 0
        End If
        cp.needRePainting = False
        Try
            cp.painting(QuotesPctBox, TimesPctBox, PricesPctBox)
        Catch ex As Exception
            cp.currentPoint -= 1
            If (cp.currentPoint < 0) Then
                cp.currentPoint = 0
            End If
        End Try
    End Sub

    'rigth
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If (cp.points.Count > cp.pointsOnScreen) Then
            cp.currentPoint = cp.currentPoint + 10
            If (cp.currentPoint + cp.pointsOnScreen > cp.points.Count) Then
                cp.currentPoint = cp.points.Count - cp.pointsOnScreen
            End If

            If (Not cp.lastPoint = cp.points.Count - 1) Then
                Try
                    cp.painting(QuotesPctBox, TimesPctBox, PricesPctBox)
                Catch ex As Exception
                    cp.currentPoint -= 1
                    If (cp.currentPoint < 0) Then
                        cp.currentPoint = 0
                    End If
                End Try
            Else
                cp.needRePainting = True
            End If
        End If

    End Sub

    '+
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        cp.pointsOnScreen += 15
        If (cp.pointsOnScreen > cp.maxPointsOnScreen) Then
            cp.pointsOnScreen = cp.maxPointsOnScreen
        End If
        cp.needRePainting = False
        If (cp.lastPoint < cp.points.Count - 1) Then
            Try
                cp.painting(QuotesPctBox, TimesPctBox, PricesPctBox)
            Catch ex As Exception
                cp.currentPoint -= 1
                If (cp.currentPoint < 0) Then
                    cp.currentPoint = 0
                End If
            End Try
        Else
            cp.currentPoint -= 7
            If (cp.currentPoint < 0) Then
                cp.currentPoint = 0
            End If
            Try
                cp.painting(QuotesPctBox, TimesPctBox, PricesPctBox)
            Catch ex As Exception
                cp.currentPoint -= 1
                If (cp.currentPoint < 0) Then
                    cp.currentPoint = 0
                End If
            End Try
        End If
    End Sub

    '-
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        cp.pointsOnScreen -= 15
        If (cp.pointsOnScreen < cp.minPointsOnScreen) Then
            cp.pointsOnScreen = cp.minPointsOnScreen
        End If
        cp.needRePainting = False
        Try
            cp.painting(QuotesPctBox, TimesPctBox, PricesPctBox)
        Catch ex As Exception
            cp.currentPoint -= 1
            If (cp.currentPoint < 0) Then
                cp.currentPoint = 0
            End If
        End Try
    End Sub
End Class
