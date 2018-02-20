Imports QuickFix
Imports System.Threading
Public Class Form1
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Public cp As ChartPainting
    Public yRangePublic As Double
    Dim volume As Double = 0
    Dim pageList As List(Of Page) = New List(Of Page)


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If feedReciever IsNot Nothing Then
            feedReciever = Nothing
        End If
        feedReciever = New QuoteFixReciever(fixConfigPath, AddressOf CheckingState)

        Dim newPage = New Page(Nothing, New ChartPainting, QuotesPctBox0, PricesQuotesPctBox0, TimesQuotesPctBox0, TradesPctBox0, PricesTradesPctBox0, TimesTradesPctBox0,
                LeftQuotesButton0, RightQuotesButton0, PlusQuotesButton0, MinusQuotesButton0, LeftTradesButton0, RightButtonTrades0, PlusTradesButton0, MinusTradesButton0)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox1, PricesQuotesPctBox1, TimesQuotesPctBox1, TradesPctBox1, PricesTradesPctBox1, TimesTradesPctBox1,
                LeftQuotesButton1, RightQuotesButton1, PlusQuotesButton1, MinusQuotesButton1, LeftTradesButton1, RightButtonTrades1, PlusTradesButton1, MinusTradesButton1)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox2, PricesQuotesPctBox2, TimesQuotesPctBox2, TradesPctBox2, PricesTradesPctBox2, TimesTradesPctBox2,
                LeftQuotesButton2, RightQuotesButton2, PlusQuotesButton2, MinusQuotesButton2, LeftTradesButton2, RightButtonTrades2, PlusTradesButton2, MinusTradesButton2)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox3, PricesQuotesPctBox3, TimesQuotesPctBox3, TradesPctBox3, PricesTradesPctBox3, TimesTradesPctBox3,
                LeftQuotesButton3, RightQuotesButton3, PlusQuotesButton3, MinusQuotesButton3, LeftTradesButton3, RightButtonTrades3, PlusTradesButton3, MinusTradesButton3)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox4, PricesQuotesPctBox4, TimesQuotesPctBox4, TradesPctBox4, PricesTradesPctBox4, TimesTradesPctBox4,
                LeftQuotesButton4, RightQuotesButton4, PlusQuotesButton4, MinusQuotesButton4, LeftTradesButton4, RightButtonTrades4, PlusTradesButton4, MinusTradesButton4)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox5, PricesQuotesPctBox5, TimesQuotesPctBox5, TradesPctBox5, PricesTradesPctBox5, TimesTradesPctBox5,
                LeftQuotesButton5, RightQuotesButton5, PlusQuotesButton5, MinusQuotesButton5, LeftTradesButton5, RightButtonTrades5, PlusTradesButton5, MinusTradesButton5)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox6, PricesQuotesPctBox6, TimesQuotesPctBox6, TradesPctBox6, PricesTradesPctBox6, TimesTradesPctBox6,
                LeftQuotesButton6, RightQuotesButton6, PlusQuotesButton6, MinusQuotesButton6, LeftTradesButton6, RightButtonTrades6, PlusTradesButton6, MinusTradesButton6)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox7, PricesQuotesPctBox7, TimesQuotesPctBox7, TradesPctBox7, PricesTradesPctBox7, TimesTradesPctBox7,
                LeftQuotesButton7, RightQuotesButton7, PlusQuotesButton7, MinusQuotesButton7, LeftTradesButton7, RightButtonTrades7, PlusTradesButton7, MinusTradesButton7)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox8, PricesQuotesPctBox8, TimesQuotesPctBox8, TradesPctBox8, PricesTradesPctBox8, TimesTradesPctBox8,
                LeftQuotesButton8, RightQuotesButton8, PlusQuotesButton8, MinusQuotesButton8, LeftTradesButton8, RightButtonTrades8, PlusTradesButton8, MinusTradesButton8)
        pageList.Add(newPage)
        newPage = New Page(Nothing, New ChartPainting, QuotesPctBox9, PricesQuotesPctBox9, TimesQuotesPctBox9, TradesPctBox9, PricesTradesPctBox9, TimesTradesPctBox9,
                LeftQuotesButton9, RightQuotesButton9, PlusQuotesButton9, MinusQuotesButton9, LeftTradesButton9, RightButtonTrades9, PlusTradesButton9, MinusTradesButton9)
        pageList.Add(newPage)
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles SubscribreButton0.Click
        TabControl.TabPages(TabControl.SelectedIndex).Text = ExanteIDTextBox0.Text
        Dim subscribes = feedReciever.GetSubscribeInfos()
        feedReciever.SubscribeForQuotes(ExanteIDTextBox0.Text, AddressOf pageList(TabControl.SelectedIndex).OnMarketDataUpdate)
    End Sub

    'Sub OnMarketDataUpdate(quotesInfo As QuotesInfo)
    '    If (quotesInfo.AskPrice IsNot Nothing And quotesInfo.BidPrice IsNot Nothing) Then
    '        'котировки
    '        AskPriceTextBox.Invoke(Sub()
    '                                   AskPriceTextBox.Text = quotesInfo.AskPrice
    '                               End Sub)
    '        AskVolumeTextBox.Invoke(Sub()
    '                                    AskVolumeTextBox.Text = quotesInfo.AskVolume
    '                                End Sub)
    '        BidPriceTextBox.Invoke(Sub()
    '                                   BidPriceTextBox.Text = quotesInfo.BidPrice
    '                               End Sub)
    '        BidVolumeTextBox.Invoke(Sub()
    '                                    BidVolumeTextBox.Text = quotesInfo.BidVolume
    '                                End Sub)

    '        If (quotesInfo.AskPrice > quotesInfo.BidPrice) Then
    '            currentMaxPriceQuotes = quotesInfo.AskPrice
    '            currentMinPriceQuotes = quotesInfo.BidPrice
    '        Else
    '            currentMaxPriceQuotes = quotesInfo.BidPrice
    '            currentMinPriceQuotes = quotesInfo.AskPrice
    '        End If

    '        If (currentMaxPriceQuotes > cp.maxPriceQuotes) Then
    '            cp.maxPriceQuotes = currentMaxPriceQuotes
    '        End If

    '        If (currentMinPriceQuotes < cp.minPriceQuotes) Then
    '            cp.minPriceQuotes = currentMinPriceQuotes
    '        End If

    '        cp.pointsQuotes.Add(New PointQuotes(quotesInfo.AskPrice, quotesInfo.AskVolume, quotesInfo.BidPrice, quotesInfo.BidVolume, DateTime.Now))
    '        Me.Invoke(Sub()

    '                      If (Not cp.isDrawingStartedQuotes) Then
    '                          cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
    '                      End If
    '                  End Sub)
    '    Else
    '        'сделки
    '        TradePriceTextBox.Invoke(Sub()
    '                                     TradePriceTextBox.Text = quotesInfo.TradePrice
    '                                 End Sub)
    '        TradeVolumeTextBox.Invoke(Sub()
    '                                      TradeVolumeTextBox.Text = quotesInfo.TradeVolume
    '                                  End Sub)

    '        If (quotesInfo.TradePrice > cp.maxPriceTrades) Then
    '            cp.maxPriceTrades = quotesInfo.TradePrice
    '        End If

    '        If (quotesInfo.TradePrice < cp.minPriceTrades) Then
    '            cp.minPriceTrades = quotesInfo.TradePrice
    '        End If

    '        cp.pointsTrades.Add(New PointTrades(quotesInfo.TradePrice, quotesInfo.TradeVolume, DateTime.Now))
    '        Me.Invoke(Sub()
    '                      cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox)
    '                  End Sub)
    '    End If



    'End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DoubleBuffered = True
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If feedReciever IsNot Nothing Then
            feedReciever.Logout()
        End If
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub QuotesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles QuotesPctBox0.MouseMove, TradesPctBox2.MouseMove, QuotesPctBox9.MouseMove, QuotesPctBox8.MouseMove, QuotesPctBox7.MouseMove, QuotesPctBox6.MouseMove, QuotesPctBox5.MouseMove, QuotesPctBox4.MouseMove, QuotesPctBox3.MouseMove, QuotesPctBox2.MouseMove, QuotesPctBox1.MouseMove
        If (Not pageList Is Nothing) Then
            Try
                Dim proportion As Double = pageList(TabControl.SelectedIndex).cp.yRangeQuotes - (e.Y / QuotesPctBox0.Height) * pageList(TabControl.SelectedIndex).cp.yRangeQuotes
                PriceLabel0.Text = Format((pageList(TabControl.SelectedIndex).cp.minPriceQuotes - pageList(TabControl.SelectedIndex).cp.minPriceQuotes * 0.0025) + proportion, "0.00")

                Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(TabControl.SelectedIndex).cp.intervalQuotes))
                If (indexOfPoint < 0) Then
                    indexOfPoint = 0
                End If
                If (indexOfPoint >= pageList(TabControl.SelectedIndex).cp.pointsQuotes.Count) Then
                    indexOfPoint = pageList(TabControl.SelectedIndex).cp.pointsQuotes.Count - 1
                    TimeLabel0.Text = pageList(TabControl.SelectedIndex).cp.pointsQuotes(indexOfPoint).time.ToLongTimeString
                Else
                    If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes + indexOfPoint > pageList(TabControl.SelectedIndex).cp.pointsQuotes.Count) Then
                        TimeLabel0.Text = pageList(TabControl.SelectedIndex).cp.pointsQuotes(pageList(TabControl.SelectedIndex).cp.lastPointQuotes).time.ToLongTimeString

                    Else
                        TimeLabel0.Text = pageList(TabControl.SelectedIndex).cp.pointsQuotes(pageList(TabControl.SelectedIndex).cp.currentPointQuotes + indexOfPoint).time.ToLongTimeString
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub DrawLineQuotes_Click(sender As Object, e As EventArgs) Handles DrawLineQuotes0.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineQuotes = True
        pageList(TabControl.SelectedIndex).cp.needRePaintingQuotes = False
    End Sub

    Private Sub QuotesPctBox_MouseClick(sender As Object, e As MouseEventArgs) Handles QuotesPctBox0.MouseClick, TradesPctBox3.MouseClick, QuotesPctBox9.MouseClick, QuotesPctBox8.MouseClick, QuotesPctBox7.MouseClick, QuotesPctBox6.MouseClick, QuotesPctBox5.MouseClick, QuotesPctBox4.MouseClick, QuotesPctBox3.MouseClick, QuotesPctBox2.MouseClick, QuotesPctBox1.MouseClick
        If (pageList(TabControl.SelectedIndex).cp.needDrawLineQuotes And Not pageList(TabControl.SelectedIndex).cp.isDrawingStartedQuotes) Then
            pageList(TabControl.SelectedIndex).cp.point1Quotes.X = e.X
            pageList(TabControl.SelectedIndex).cp.point1Quotes.Y = e.Y
            pageList(TabControl.SelectedIndex).cp.isDrawingStartedQuotes = True
            Exit Sub
        End If
        If (pageList(TabControl.SelectedIndex).cp.needDrawLineQuotes And pageList(TabControl.SelectedIndex).cp.isDrawingStartedQuotes) Then
            pageList(TabControl.SelectedIndex).cp.point2Quotes.X = e.X
            pageList(TabControl.SelectedIndex).cp.point2Quotes.Y = e.Y
            pageList(TabControl.SelectedIndex).cp.isDrawingStartedQuotes = False
            pageList(TabControl.SelectedIndex).cp.isLineReadyQuotes = True
            pageList(TabControl.SelectedIndex).cp.paintingQuotes(QuotesPctBox0, TimesQuotesPctBox0, PricesQuotesPctBox0)
            Exit Sub
        End If
    End Sub

    'left quotes
    Private Sub LeftQuotesButton_Click(sender As Object, e As EventArgs) Handles LeftQuotesButton0.Click, LeftQuotesButton1.Click, LeftQuotesButton2.Click, LeftQuotesButton8.Click, LeftTradesButton7.Click, LeftQuotesButton7.Click, LeftQuotesButton6.Click, LeftQuotesButton5.Click, LeftQuotesButton9.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineQuotes = False
        pageList(TabControl.SelectedIndex).cp.isLineReadyQuotes = False
        pageList(TabControl.SelectedIndex).cp.currentPointQuotes = pageList(TabControl.SelectedIndex).cp.currentPointQuotes - 10
        If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes < 0) Then
            pageList(TabControl.SelectedIndex).cp.currentPointQuotes = 0
        End If
        pageList(TabControl.SelectedIndex).cp.needRePaintingQuotes = False
        Try
            pageList(TabControl.SelectedIndex).cp.paintingQuotes(pageList(TabControl.SelectedIndex).QuotesPctBox, pageList(TabControl.SelectedIndex).TimesQuotesPctBox, pageList(TabControl.SelectedIndex).PricesQuotesPctBox)
        Catch ex As Exception
            pageList(TabControl.SelectedIndex).cp.currentPointQuotes -= 1
            If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes < 0) Then
                pageList(TabControl.SelectedIndex).cp.currentPointQuotes = 0
            End If
        End Try
    End Sub

    'rigth quotes
    Private Sub RightQuotesButton_Click(sender As Object, e As EventArgs) Handles RightQuotesButton0.Click, RightQuotesButton3.Click, RightQuotesButton2.Click, RightQuotesButton1.Click, RightQuotesButton8.Click, RightButtonTrades7.Click, RightQuotesButton7.Click, RightQuotesButton6.Click, RightQuotesButton5.Click, RightQuotesButton4.Click, RightQuotesButton9.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineQuotes = False
        pageList(TabControl.SelectedIndex).cp.isLineReadyQuotes = False
        If (pageList(TabControl.SelectedIndex).cp.pointsQuotes.Count > pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes) Then
            pageList(TabControl.SelectedIndex).cp.currentPointQuotes = pageList(TabControl.SelectedIndex).cp.currentPointQuotes + 10
            If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes + pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes > pageList(TabControl.SelectedIndex).cp.pointsQuotes.Count) Then
                pageList(TabControl.SelectedIndex).cp.currentPointQuotes = pageList(TabControl.SelectedIndex).cp.pointsQuotes.Count - pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes
            End If

            If (Not pageList(TabControl.SelectedIndex).cp.lastPointQuotes = pageList(TabControl.SelectedIndex).cp.pointsQuotes.Count - 1) Then
                Try
                    pageList(TabControl.SelectedIndex).cp.paintingQuotes(pageList(TabControl.SelectedIndex).QuotesPctBox, pageList(TabControl.SelectedIndex).TimesQuotesPctBox, pageList(TabControl.SelectedIndex).PricesQuotesPctBox)
                Catch ex As Exception
                    pageList(TabControl.SelectedIndex).cp.currentPointQuotes -= 1
                    If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes < 0) Then
                        pageList(TabControl.SelectedIndex).cp.currentPointQuotes = 0
                    End If
                End Try
            Else
                pageList(TabControl.SelectedIndex).cp.needRePaintingQuotes = True
            End If
        End If

    End Sub

    '+ quotes
    Private Sub PlusQuotesButton_Click(sender As Object, e As EventArgs) Handles PlusQuotesButton0.Click, PlusQuotesButton3.Click, PlusQuotesButton2.Click, PlusQuotesButton1.Click, PlusQuotesButton8.Click, PlusQuotesButton7.Click, PlusQuotesButton6.Click, PlusQuotesButton5.Click, LeftQuotesButton4.Click, PlusQuotesButton4.Click, PlusQuotesButton9.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineQuotes = False
        pageList(TabControl.SelectedIndex).cp.isLineReadyQuotes = False
        pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes += 15
        If (pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes > pageList(TabControl.SelectedIndex).cp.maxPointsOnScreenQuotes) Then
            pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes = pageList(TabControl.SelectedIndex).cp.maxPointsOnScreenQuotes
        End If
        pageList(TabControl.SelectedIndex).cp.needRePaintingQuotes = False
        If (pageList(TabControl.SelectedIndex).cp.lastPointQuotes < pageList(TabControl.SelectedIndex).cp.pointsQuotes.Count - 1) Then
            Try
                pageList(TabControl.SelectedIndex).cp.paintingQuotes(QuotesPctBox0, TimesQuotesPctBox0, PricesQuotesPctBox0)
            Catch ex As Exception
                pageList(TabControl.SelectedIndex).cp.currentPointQuotes -= 1
                If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes < 0) Then
                    pageList(TabControl.SelectedIndex).cp.currentPointQuotes = 0
                End If
            End Try
        Else
            pageList(TabControl.SelectedIndex).cp.currentPointQuotes -= 7
            If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes < 0) Then
                pageList(TabControl.SelectedIndex).cp.currentPointQuotes = 0
            End If
            Try
                pageList(TabControl.SelectedIndex).cp.paintingQuotes(pageList(TabControl.SelectedIndex).QuotesPctBox, pageList(TabControl.SelectedIndex).TimesQuotesPctBox, pageList(TabControl.SelectedIndex).PricesQuotesPctBox)
            Catch ex As Exception
                pageList(TabControl.SelectedIndex).cp.currentPointQuotes -= 1
                If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes < 0) Then
                    pageList(TabControl.SelectedIndex).cp.currentPointQuotes = 0
                End If
            End Try
        End If
    End Sub

    '- quotes
    Private Sub MinusQuotesButton_Click(sender As Object, e As EventArgs) Handles MinusQuotesButton0.Click, LeftQuotesButton3.Click, MinusQuotesButton3.Click, MinusQuotesButton2.Click, MinusQuotesButton1.Click, MinusQuotesButton8.Click, MinusQuotesButton7.Click, MinusQuotesButton6.Click, MinusQuotesButton5.Click, MinusQuotesButton4.Click, MinusQuotesButton9.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineQuotes = False
        pageList(TabControl.SelectedIndex).cp.isLineReadyQuotes = False
        pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes -= 15
        If (pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes < pageList(TabControl.SelectedIndex).cp.minPointsOnScreenQuotes) Then
            pageList(TabControl.SelectedIndex).cp.pointsOnScreenQuotes = pageList(TabControl.SelectedIndex).cp.minPointsOnScreenQuotes
        End If
        pageList(TabControl.SelectedIndex).cp.needRePaintingQuotes = False
        Try
            pageList(TabControl.SelectedIndex).cp.paintingQuotes(pageList(TabControl.SelectedIndex).QuotesPctBox, pageList(TabControl.SelectedIndex).TimesQuotesPctBox, pageList(TabControl.SelectedIndex).PricesQuotesPctBox)
        Catch ex As Exception
            pageList(TabControl.SelectedIndex).cp.currentPointQuotes -= 1
            If (pageList(TabControl.SelectedIndex).cp.currentPointQuotes < 0) Then
                pageList(TabControl.SelectedIndex).cp.currentPointQuotes = 0
            End If
        End Try
    End Sub

    'left trades
    Private Sub LeftTradesButton_Click(sender As Object, e As EventArgs) Handles LeftTradesButton0.Click, LeftTradesButton3.Click, LeftTradesButton1.Click, LeftTradesButton8.Click, LeftTradesButton6.Click, LeftTradesButton5.Click, LeftTradesButton4.Click, LeftTradesButton2.Click, LeftTradesButton9.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineTrades = False
        pageList(TabControl.SelectedIndex).cp.isLineReadyTrades = False
        pageList(TabControl.SelectedIndex).cp.currentPointTrades = pageList(TabControl.SelectedIndex).cp.currentPointTrades - 10
        If (pageList(TabControl.SelectedIndex).cp.currentPointTrades < 0) Then
            pageList(TabControl.SelectedIndex).cp.currentPointTrades = 0
        End If
        pageList(TabControl.SelectedIndex).cp.needRePaintingTrades = False
        Try
            pageList(TabControl.SelectedIndex).cp.paintingTrades(pageList(TabControl.SelectedIndex).TradesPctBox, pageList(TabControl.SelectedIndex).TimesTradesPctBox, pageList(TabControl.SelectedIndex).PricesTradesPctBox)
        Catch ex As Exception
            pageList(TabControl.SelectedIndex).cp.currentPointTrades -= 1
            If (pageList(TabControl.SelectedIndex).cp.currentPointTrades < 0) Then
                pageList(TabControl.SelectedIndex).cp.currentPointTrades = 0
            End If
        End Try
    End Sub

    'right trades
    Private Sub RightTradesButton_Click(sender As Object, e As EventArgs) Handles RightButtonTrades0.Click, RightButtonTrades3.Click, RightButtonTrades1.Click, RightButtonTrades8.Click, RightButtonTrades6.Click, RightButtonTrades5.Click, RightButtonTrades4.Click, RightButtonTrades2.Click, RightButtonTrades9.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineTrades = False
        pageList(TabControl.SelectedIndex).cp.isLineReadyTrades = False
        If (pageList(TabControl.SelectedIndex).cp.pointsTrades.Count > pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades) Then
            pageList(TabControl.SelectedIndex).cp.currentPointTrades = pageList(TabControl.SelectedIndex).cp.currentPointTrades + 10
            If (pageList(TabControl.SelectedIndex).cp.currentPointTrades + pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades > pageList(TabControl.SelectedIndex).cp.pointsTrades.Count) Then
                pageList(TabControl.SelectedIndex).cp.currentPointTrades = pageList(TabControl.SelectedIndex).cp.pointsTrades.Count - pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades
            End If

            If (Not pageList(TabControl.SelectedIndex).cp.lastPointTrades = pageList(TabControl.SelectedIndex).cp.pointsTrades.Count - 1) Then
                Try
                    pageList(TabControl.SelectedIndex).cp.paintingTrades(pageList(TabControl.SelectedIndex).TradesPctBox, pageList(TabControl.SelectedIndex).TimesTradesPctBox, pageList(TabControl.SelectedIndex).PricesTradesPctBox)
                Catch ex As Exception
                    pageList(TabControl.SelectedIndex).cp.currentPointTrades -= 1
                    If (pageList(TabControl.SelectedIndex).cp.currentPointTrades < 0) Then
                        pageList(TabControl.SelectedIndex).cp.currentPointTrades = 0
                    End If
                End Try
            Else
                pageList(TabControl.SelectedIndex).cp.needRePaintingTrades = True
            End If
        End If
    End Sub

    '+ trades
    Private Sub PlusTradesButton_Click(sender As Object, e As EventArgs) Handles PlusTradesButton0.Click, PlusTradesButton1.Click, PlusTradesButton8.Click, PlusTradesButton7.Click, PlusTradesButton6.Click, PlusTradesButton5.Click, PlusTradesButton4.Click, PlusTradesButton3.Click, PlusTradesButton2.Click, PlusTradesButton9.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineTrades = False
        pageList(TabControl.SelectedIndex).cp.isLineReadyTrades = False
        pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades += 15
        If (pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades > pageList(TabControl.SelectedIndex).cp.maxPointsOnScreenTrades) Then
            pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades = pageList(TabControl.SelectedIndex).cp.maxPointsOnScreenTrades
        End If
        pageList(TabControl.SelectedIndex).cp.needRePaintingTrades = False
        If (pageList(TabControl.SelectedIndex).cp.lastPointTrades < pageList(TabControl.SelectedIndex).cp.pointsTrades.Count - 1) Then
            Try
                pageList(TabControl.SelectedIndex).cp.paintingTrades(pageList(TabControl.SelectedIndex).TradesPctBox, pageList(TabControl.SelectedIndex).TimesTradesPctBox, pageList(TabControl.SelectedIndex).PricesTradesPctBox)
            Catch ex As Exception
                pageList(TabControl.SelectedIndex).cp.currentPointTrades -= 1
                If (pageList(TabControl.SelectedIndex).cp.currentPointTrades < 0) Then
                    pageList(TabControl.SelectedIndex).cp.currentPointTrades = 0
                End If
            End Try
        Else
            pageList(TabControl.SelectedIndex).cp.currentPointTrades -= 7
            If (pageList(TabControl.SelectedIndex).cp.currentPointTrades < 0) Then
                pageList(TabControl.SelectedIndex).cp.currentPointTrades = 0
            End If
            Try
                pageList(TabControl.SelectedIndex).cp.paintingTrades(pageList(TabControl.SelectedIndex).TradesPctBox, pageList(TabControl.SelectedIndex).TimesTradesPctBox, pageList(TabControl.SelectedIndex).PricesTradesPctBox)
            Catch ex As Exception
                pageList(TabControl.SelectedIndex).cp.currentPointTrades -= 1
                If (pageList(TabControl.SelectedIndex).cp.currentPointTrades < 0) Then
                    pageList(TabControl.SelectedIndex).cp.currentPointTrades = 0
                End If
            End Try
        End If
    End Sub

    '- trades
    Private Sub MinusTradesButton_Click(sender As Object, e As EventArgs) Handles MinusTradesButton0.Click, MinusTradesButton1.Click, MinusTradesButton8.Click, MinusTradesButton7.Click, MinusTradesButton6.Click, MinusTradesButton5.Click, MinusTradesButton4.Click, MinusTradesButton3.Click, MinusTradesButton2.Click, MinusTradesButton9.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineTrades = False
        pageList(TabControl.SelectedIndex).cp.isLineReadyTrades = False
        pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades -= 15
        If (pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades < pageList(TabControl.SelectedIndex).cp.minPointsOnScreenTrades) Then
            pageList(TabControl.SelectedIndex).cp.pointsOnScreenTrades = pageList(TabControl.SelectedIndex).cp.minPointsOnScreenTrades
        End If
        pageList(TabControl.SelectedIndex).cp.needRePaintingTrades = False
        Try
            pageList(TabControl.SelectedIndex).cp.paintingTrades(pageList(TabControl.SelectedIndex).TradesPctBox, pageList(TabControl.SelectedIndex).TimesTradesPctBox, pageList(TabControl.SelectedIndex).PricesTradesPctBox)
        Catch ex As Exception
            pageList(TabControl.SelectedIndex).cp.currentPointTrades -= 1
            If (pageList(TabControl.SelectedIndex).cp.currentPointTrades < 0) Then
                pageList(TabControl.SelectedIndex).cp.currentPointTrades = 0
            End If
        End Try
    End Sub

    Private Sub TradesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseMove, TradesPctBox9.MouseMove, TradesPctBox8.MouseMove, TradesPctBox7.MouseMove, TradesPctBox6.MouseMove, TradesPctBox5.MouseMove, TradesPctBox4.MouseMove, TradesPctBox3.MouseMove
        If (Not pageList Is Nothing) Then
            Try
                Dim proportion As Double = pageList(TabControl.SelectedIndex).cp.yRangeTrades - (e.Y / TradesPctBox0.Height) * pageList(TabControl.SelectedIndex).cp.yRangeTrades
                PriceLabel0.Text = Format((pageList(TabControl.SelectedIndex).cp.minPriceTrades - pageList(TabControl.SelectedIndex).cp.minPriceTrades * 0.0025) + proportion, "0.00")

                Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(TabControl.SelectedIndex).cp.intervalTrades))
                If (indexOfPoint < 0) Then
                    indexOfPoint = 0
                End If
                If (indexOfPoint >= pageList(TabControl.SelectedIndex).cp.pointsTrades.Count) Then
                    indexOfPoint = pageList(TabControl.SelectedIndex).cp.pointsTrades.Count - 1
                    TimeLabel0.Text = pageList(TabControl.SelectedIndex).cp.pointsTrades(indexOfPoint).time.ToLongTimeString
                Else
                    If (pageList(TabControl.SelectedIndex).cp.currentPointTrades + indexOfPoint > pageList(TabControl.SelectedIndex).cp.pointsTrades.Count) Then
                        TimeLabel0.Text = pageList(TabControl.SelectedIndex).cp.pointsTrades(pageList(TabControl.SelectedIndex).cp.lastPointTrades).time.ToLongTimeString

                    Else
                        TimeLabel0.Text = pageList(TabControl.SelectedIndex).cp.pointsTrades(pageList(TabControl.SelectedIndex).cp.currentPointTrades + indexOfPoint).time.ToLongTimeString
                    End If
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub DrawLineTrades_Click(sender As Object, e As EventArgs) Handles DrawLineTrades0.Click
        pageList(TabControl.SelectedIndex).cp.needDrawLineTrades = True
        pageList(TabControl.SelectedIndex).cp.needRePaintingTrades = False
    End Sub

    Private Sub TradesPctBox_MouseClick(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseClick, TradesPctBox9.MouseClick, TradesPctBox8.MouseClick, TradesPctBox7.MouseClick, TradesPctBox6.MouseClick, TradesPctBox5.MouseClick, TradesPctBox4.MouseClick, TradesPctBox2.MouseClick, TradesPctBox1.MouseMove, TradesPctBox1.MouseClick
        If (pageList(TabControl.SelectedIndex).cp.needDrawLineTrades And Not pageList(TabControl.SelectedIndex).cp.isDrawingStartedTrades) Then
            pageList(TabControl.SelectedIndex).cp.point1Trades.X = e.X
            pageList(TabControl.SelectedIndex).cp.point1Trades.Y = e.Y
            pageList(TabControl.SelectedIndex).cp.isDrawingStartedTrades = True
            Exit Sub
        End If
        If (pageList(TabControl.SelectedIndex).cp.needDrawLineTrades And pageList(TabControl.SelectedIndex).cp.isDrawingStartedTrades) Then
            pageList(TabControl.SelectedIndex).cp.point2Trades.X = e.X
            pageList(TabControl.SelectedIndex).cp.point2Trades.Y = e.Y
            pageList(TabControl.SelectedIndex).cp.isDrawingStartedTrades = False
            pageList(TabControl.SelectedIndex).cp.isLineReadyTrades = True
            pageList(TabControl.SelectedIndex).cp.paintingTrades(TradesPctBox0, TimesTradesPctBox0, PricesTradesPctBox0)
            Exit Sub
        End If
    End Sub

    Private Sub TradesTab_Click(sender As Object, e As EventArgs) Handles TradesTab0.Click

    End Sub
End Class
