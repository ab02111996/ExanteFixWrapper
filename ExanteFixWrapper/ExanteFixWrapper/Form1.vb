Imports QuickFix
Imports System.Threading
Public Class Form1
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Public pageList As List(Of Page) = New List(Of Page)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If feedReciever IsNot Nothing Then
            feedReciever = Nothing
        End If
        feedReciever = New QuoteFixReciever(fixConfigPath, AddressOf CheckingState)

        Dim newPage = New Page(New ChartPainting, QuotesPctBox0, PricesQuotesPctBox0, TimesQuotesPctBox0, TradesPctBox0, PricesTradesPctBox0, TimesTradesPctBox0,
                LeftQuotesButton0, RightQuotesButton0, PlusQuotesButton0, MinusQuotesButton0, LeftTradesButton0, RightButtonTrades0, PlusTradesButton0, MinusTradesButton0, Charts0, VolumesTradesPctBox0, VolumesVolumesTradesPctBox0)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox1, PricesQuotesPctBox1, TimesQuotesPctBox1, TradesPctBox1, PricesTradesPctBox1, TimesTradesPctBox1,
                LeftQuotesButton1, RightQuotesButton1, PlusQuotesButton1, MinusQuotesButton1, LeftTradesButton1, RightButtonTrades1, PlusTradesButton1, MinusTradesButton1, Charts1, VolumesTradesPctBox1, VolumesVolumesTradesPctBox1)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox2, PricesQuotesPctBox2, TimesQuotesPctBox2, TradesPctBox2, PricesTradesPctBox2, TimesTradesPctBox2,
                LeftQuotesButton2, RightQuotesButton2, PlusQuotesButton2, MinusQuotesButton2, LeftTradesButton2, RightButtonTrades2, PlusTradesButton2, MinusTradesButton2, Charts2, VolumesTradesPctBox2, VolumesVolumesTradesPctBox2)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox3, PricesQuotesPctBox3, TimesQuotesPctBox3, TradesPctBox3, PricesTradesPctBox3, TimesTradesPctBox3,
                LeftQuotesButton3, RightQuotesButton3, PlusQuotesButton3, MinusQuotesButton3, LeftTradesButton3, RightButtonTrades3, PlusTradesButton3, MinusTradesButton3, Charts3, VolumesTradesPctBox3, VolumesVolumesTradesPctBox3)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox4, PricesQuotesPctBox4, TimesQuotesPctBox4, TradesPctBox4, PricesTradesPctBox4, TimesTradesPctBox4,
                LeftQuotesButton4, RightQuotesButton4, PlusQuotesButton4, MinusQuotesButton4, LeftTradesButton4, RightButtonTrades4, PlusTradesButton4, MinusTradesButton4, Charts4, VolumesTradesPctBox4, VolumesVolumesTradesPctBox4)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox5, PricesQuotesPctBox5, TimesQuotesPctBox5, TradesPctBox5, PricesTradesPctBox5, TimesTradesPctBox5,
                LeftQuotesButton5, RightQuotesButton5, PlusQuotesButton5, MinusQuotesButton5, LeftTradesButton5, RightButtonTrades5, PlusTradesButton5, MinusTradesButton5, Charts5, VolumesTradesPctBox5, VolumesVolumesTradesPctBox5)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox6, PricesQuotesPctBox6, TimesQuotesPctBox6, TradesPctBox6, PricesTradesPctBox6, TimesTradesPctBox6,
                LeftQuotesButton6, RightQuotesButton6, PlusQuotesButton6, MinusQuotesButton6, LeftTradesButton6, RightButtonTrades6, PlusTradesButton6, MinusTradesButton6, Charts6, VolumesTradesPctBox6, VolumesVolumesTradesPctBox6)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox7, PricesQuotesPctBox7, TimesQuotesPctBox7, TradesPctBox7, PricesTradesPctBox7, TimesTradesPctBox7,
                LeftQuotesButton7, RightQuotesButton7, PlusQuotesButton7, MinusQuotesButton7, LeftTradesButton7, RightButtonTrades7, PlusTradesButton7, MinusTradesButton7, Charts7, VolumesTradesPctBox7, VolumesVolumesTradesPctBox7)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox8, PricesQuotesPctBox8, TimesQuotesPctBox8, TradesPctBox8, PricesTradesPctBox8, TimesTradesPctBox8,
                LeftQuotesButton8, RightQuotesButton8, PlusQuotesButton8, MinusQuotesButton8, LeftTradesButton8, RightButtonTrades8, PlusTradesButton8, MinusTradesButton8, Charts8, VolumesTradesPctBox8, VolumesVolumesTradesPctBox8)
        pageList.Add(newPage)
        newPage = New Page(New ChartPainting, QuotesPctBox9, PricesQuotesPctBox9, TimesQuotesPctBox9, TradesPctBox9, PricesTradesPctBox9, TimesTradesPctBox9,
                LeftQuotesButton9, RightQuotesButton9, PlusQuotesButton9, MinusQuotesButton9, LeftTradesButton9, RightButtonTrades9, PlusTradesButton9, MinusTradesButton9, Charts9, VolumesTradesPctBox9, VolumesVolumesTradesPctBox9)
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
        Try
            pageList(Tabs.SelectedIndex).cp.isSubscribed = True
            Tabs.TabPages(Tabs.SelectedIndex).Text = ExanteIDTextBox0.Text
            Dim subscribes = feedReciever.GetSubscribeInfos()
            feedReciever.SubscribeForQuotes(ExanteIDTextBox0.Text, AddressOf pageList(Tabs.SelectedIndex).OnMarketDataUpdate)
            pageList(Tabs.SelectedIndex).bufferTrades.StartWritingData(ExanteIDTextBox0.Text)
            pageList(Tabs.SelectedIndex).TabId = Tabs.SelectedIndex
        Catch ex As Exception
            MsgBox("Нет подключения")
        End Try    End Sub

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
        If (pageList.Count > 0) Then
            If (pageList(Tabs.SelectedIndex).cp.isSubscribed) Then
                Try
                    Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeQuotes - (e.Y / QuotesPctBox0.Height) * pageList(Tabs.SelectedIndex).cp.yRangeQuotes
                    PriceLabel0.Text = Format((pageList(Tabs.SelectedIndex).cp.minPriceQuotes - pageList(Tabs.SelectedIndex).cp.minPriceQuotes * 0.0001) + proportion, "0.00")
                    'PriceLabel0.Text = Format((pageList(TabControl.SelectedIndex).cp.minPriceQuotes) + proportion, "0.00")
                    Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(Tabs.SelectedIndex).cp.intervalQuotes))
                    If (indexOfPoint < 0) Then
                        indexOfPoint = 0
                    End If
                    If (indexOfPoint >= pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count) Then
                        indexOfPoint = pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count - 1
                        TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsQuotes(indexOfPoint).time.ToLongTimeString
                    Else
                        If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes + indexOfPoint > pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count) Then
                            TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsQuotes(pageList(Tabs.SelectedIndex).cp.lastPointQuotes).time.ToLongTimeString

                        Else
                            TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsQuotes(pageList(Tabs.SelectedIndex).cp.currentPointQuotes + indexOfPoint).time.ToLongTimeString
                        End If
                    End If
                Catch ex As Exception

                End Try
            End If

        End If
    End Sub

    Private Sub DrawLineQuotes_Click(sender As Object, e As EventArgs) Handles DrawLineQuotes0.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineQuotes = True
        pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False
    End Sub

    Private Sub QuotesPctBox_MouseClick(sender As Object, e As MouseEventArgs) Handles QuotesPctBox0.MouseClick, TradesPctBox3.MouseClick, QuotesPctBox9.MouseClick, QuotesPctBox8.MouseClick, QuotesPctBox7.MouseClick, QuotesPctBox6.MouseClick, QuotesPctBox5.MouseClick, QuotesPctBox4.MouseClick, QuotesPctBox3.MouseClick, QuotesPctBox2.MouseClick, QuotesPctBox1.MouseClick
        If (pageList IsNot Nothing) Then
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.needDrawLineQuotes And Not pageList(Tabs.SelectedIndex).cp.isDrawingStartedQuotes) Then
                    pageList(Tabs.SelectedIndex).cp.point1Quotes.X = e.X
                    pageList(Tabs.SelectedIndex).cp.point1Quotes.Y = e.Y
                    pageList(Tabs.SelectedIndex).cp.isDrawingStartedQuotes = True
                    Exit Sub
                End If
                If (pageList(Tabs.SelectedIndex).cp.needDrawLineQuotes And pageList(Tabs.SelectedIndex).cp.isDrawingStartedQuotes) Then
                    pageList(Tabs.SelectedIndex).cp.point2Quotes.X = e.X
                    pageList(Tabs.SelectedIndex).cp.point2Quotes.Y = e.Y
                    pageList(Tabs.SelectedIndex).cp.isDrawingStartedQuotes = False
                    pageList(Tabs.SelectedIndex).cp.isLineReadyQuotes = True
                    pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    'left quotes
    Private Sub LeftQuotesButton_Click(sender As Object, e As EventArgs) Handles LeftQuotesButton0.Click, LeftQuotesButton1.Click, LeftQuotesButton2.Click, LeftQuotesButton8.Click, LeftTradesButton7.Click, LeftQuotesButton7.Click, LeftQuotesButton6.Click, LeftQuotesButton5.Click, LeftQuotesButton9.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineQuotes = False
        pageList(Tabs.SelectedIndex).cp.isLineReadyQuotes = False
        pageList(Tabs.SelectedIndex).cp.currentPointQuotes = pageList(Tabs.SelectedIndex).cp.currentPointQuotes - 10
        If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes < 0) Then
            pageList(Tabs.SelectedIndex).cp.currentPointQuotes = 0
        End If
        pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False
        Try
            pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
        Catch ex As Exception
            pageList(Tabs.SelectedIndex).cp.currentPointQuotes -= 1
            If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes < 0) Then
                pageList(Tabs.SelectedIndex).cp.currentPointQuotes = 0
            End If
        End Try
    End Sub

    'rigth quotes
    Private Sub RightQuotesButton_Click(sender As Object, e As EventArgs) Handles RightQuotesButton0.Click, RightQuotesButton3.Click, RightQuotesButton2.Click, RightQuotesButton1.Click, RightQuotesButton8.Click, RightButtonTrades7.Click, RightQuotesButton7.Click, RightQuotesButton6.Click, RightQuotesButton5.Click, RightQuotesButton4.Click, RightQuotesButton9.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineQuotes = False
        pageList(Tabs.SelectedIndex).cp.isLineReadyQuotes = False
        If (pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count > pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes) Then
            pageList(Tabs.SelectedIndex).cp.currentPointQuotes = pageList(Tabs.SelectedIndex).cp.currentPointQuotes + 10
            If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes + pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes > pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count) Then
                pageList(Tabs.SelectedIndex).cp.currentPointQuotes = pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes
            End If

            If (Not pageList(Tabs.SelectedIndex).cp.lastPointQuotes = pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count - 1) Then
                Try
                    pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
                Catch ex As Exception
                    pageList(Tabs.SelectedIndex).cp.currentPointQuotes -= 1
                    If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes < 0) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointQuotes = 0
                    End If
                End Try
            Else
                pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = True
            End If
        End If

    End Sub

    '+ quotes
    Private Sub PlusQuotesButton_Click(sender As Object, e As EventArgs) Handles PlusQuotesButton0.Click, PlusQuotesButton3.Click, PlusQuotesButton2.Click, PlusQuotesButton1.Click, PlusQuotesButton8.Click, PlusQuotesButton7.Click, PlusQuotesButton6.Click, PlusQuotesButton5.Click, LeftQuotesButton4.Click, PlusQuotesButton4.Click, PlusQuotesButton9.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineQuotes = False
        pageList(Tabs.SelectedIndex).cp.isLineReadyQuotes = False
        pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes += 15
        pageList(Tabs.SelectedIndex).cp.currentPointQuotes -= 16
        If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes < 0) Then
            pageList(Tabs.SelectedIndex).cp.currentPointQuotes = 0
        End If
        If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes > pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenQuotes) Then
            pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes = pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenQuotes
        End If
        pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False
        If (pageList(Tabs.SelectedIndex).cp.lastPointQuotes < pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count - 1) Then
            Try
                pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
            Catch ex As Exception
                pageList(Tabs.SelectedIndex).cp.currentPointQuotes -= 1
                If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointQuotes = 0
                End If
            End Try
        Else
            Try
                pageList(Tabs.SelectedIndex).cp.currentPointQuotes -= 1
                If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointQuotes = 0
                End If
                pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
            Catch ex As Exception

            End Try
        End If
    End Sub

    '- quotes
    Private Sub MinusQuotesButton_Click(sender As Object, e As EventArgs) Handles MinusQuotesButton0.Click, LeftQuotesButton3.Click, MinusQuotesButton3.Click, MinusQuotesButton2.Click, MinusQuotesButton1.Click, MinusQuotesButton8.Click, MinusQuotesButton7.Click, MinusQuotesButton6.Click, MinusQuotesButton5.Click, MinusQuotesButton4.Click, MinusQuotesButton9.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineQuotes = False
        pageList(Tabs.SelectedIndex).cp.isLineReadyQuotes = False
        pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes -= 15
        If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes < pageList(Tabs.SelectedIndex).cp.minPointsOnScreenQuotes) Then
            pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes = pageList(Tabs.SelectedIndex).cp.minPointsOnScreenQuotes
        End If
        pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False
        Try
            pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
        Catch ex As Exception
            pageList(Tabs.SelectedIndex).cp.currentPointQuotes -= 1
            If (pageList(Tabs.SelectedIndex).cp.currentPointQuotes < 0) Then
                pageList(Tabs.SelectedIndex).cp.currentPointQuotes = 0
            End If
        End Try
    End Sub

    'left trades
    Private Sub LeftTradesButton_Click(sender As Object, e As EventArgs) Handles LeftTradesButton0.Click, LeftTradesButton3.Click, LeftTradesButton1.Click, LeftTradesButton8.Click, LeftTradesButton6.Click, LeftTradesButton5.Click, LeftTradesButton4.Click, LeftTradesButton2.Click, LeftTradesButton9.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
        pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
        pageList(Tabs.SelectedIndex).cp.currentPointTrades = pageList(Tabs.SelectedIndex).cp.currentPointTrades - 10
        If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
            pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
        End If
        pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
        Try
            pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
        Catch ex As Exception
            pageList(Tabs.SelectedIndex).cp.currentPointTrades -= 1
            If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
                pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
            End If
        End Try
    End Sub

    'right trades
    Private Sub RightTradesButton_Click(sender As Object, e As EventArgs) Handles RightButtonTrades0.Click, RightButtonTrades3.Click, RightButtonTrades1.Click, RightButtonTrades8.Click, RightButtonTrades6.Click, RightButtonTrades5.Click, RightButtonTrades4.Click, RightButtonTrades2.Click, RightButtonTrades9.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
        pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
        If (pageList(Tabs.SelectedIndex).cp.pointsTrades.Count > pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades) Then
            pageList(Tabs.SelectedIndex).cp.currentPointTrades = pageList(Tabs.SelectedIndex).cp.currentPointTrades + 10
            If (pageList(Tabs.SelectedIndex).cp.currentPointTrades + pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades > pageList(Tabs.SelectedIndex).cp.pointsTrades.Count) Then
                pageList(Tabs.SelectedIndex).cp.currentPointTrades = pageList(Tabs.SelectedIndex).cp.pointsTrades.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades
            End If

            If (Not pageList(Tabs.SelectedIndex).cp.lastPointTrades = pageList(Tabs.SelectedIndex).cp.pointsTrades.Count - 1) Then
                Try
                    pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                Catch ex As Exception
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades -= 1
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
                    End If
                End Try
            Else
                pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = True
            End If
        End If
    End Sub

    '+ trades
    Private Sub PlusTradesButton_Click(sender As Object, e As EventArgs) Handles PlusTradesButton0.Click, PlusTradesButton1.Click, PlusTradesButton8.Click, PlusTradesButton7.Click, PlusTradesButton6.Click, PlusTradesButton5.Click, PlusTradesButton4.Click, PlusTradesButton3.Click, PlusTradesButton2.Click, PlusTradesButton9.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
        pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
        pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades += 15
        pageList(Tabs.SelectedIndex).cp.currentPointTrades -= 15
        If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
            pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
        End If
        If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades > pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenTrades) Then
            pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades = pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenTrades
        End If
        pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
        If (pageList(Tabs.SelectedIndex).cp.lastPointTrades < pageList(Tabs.SelectedIndex).cp.pointsTrades.Count - 1) Then
            Try
                pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
            Catch ex As Exception
                pageList(Tabs.SelectedIndex).cp.currentPointTrades -= 1
                If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
                End If
            End Try
        Else
            pageList(Tabs.SelectedIndex).cp.currentPointTrades -= 7
            If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
                pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
            End If
            Try
                pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
            Catch ex As Exception
                pageList(Tabs.SelectedIndex).cp.currentPointTrades -= 1
                If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
                End If
            End Try
        End If
    End Sub

    '- trades
    Private Sub MinusTradesButton_Click(sender As Object, e As EventArgs) Handles MinusTradesButton0.Click, MinusTradesButton1.Click, MinusTradesButton8.Click, MinusTradesButton7.Click, MinusTradesButton6.Click, MinusTradesButton5.Click, MinusTradesButton4.Click, MinusTradesButton3.Click, MinusTradesButton2.Click, MinusTradesButton9.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
        pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
        pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades -= 15
        If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades < pageList(Tabs.SelectedIndex).cp.minPointsOnScreenTrades) Then
            pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades = pageList(Tabs.SelectedIndex).cp.minPointsOnScreenTrades
        End If
        pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
        Try
            pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
        Catch ex As Exception
            pageList(Tabs.SelectedIndex).cp.currentPointTrades -= 1
            If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
                pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
            End If
        End Try
    End Sub

    Private Sub TradesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseMove, TradesPctBox9.MouseMove, TradesPctBox8.MouseMove, TradesPctBox7.MouseMove, TradesPctBox6.MouseMove, TradesPctBox5.MouseMove, TradesPctBox4.MouseMove, TradesPctBox3.MouseMove
        If (pageList.Count > 0) Then
            If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTrades = 0) Then
                Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeTrades - (e.Y / TradesPctBox0.Height) * pageList(Tabs.SelectedIndex).cp.yRangeTrades
                'PriceLabel0.Text = Format((pageList(Tabs.SelectedIndex).cp.minPriceTrades - pageList(Tabs.SelectedIndex).cp.minPriceTrades * 0.0001) + proportion, "0.00")
                PriceLabel0.Text = Format((pageList(Tabs.SelectedIndex).cp.lowBorderTrades) + proportion, "0.00")
                Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(Tabs.SelectedIndex).cp.intervalTrades))
                If (indexOfPoint < 0) Then
                    indexOfPoint = 0
                End If
                If (indexOfPoint >= pageList(Tabs.SelectedIndex).cp.pointsTrades.Count) Then
                    indexOfPoint = pageList(Tabs.SelectedIndex).cp.pointsTrades.Count - 1
                    If (indexOfPoint < 0) Then
                        indexOfPoint = 0
                    End If
                    TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades(indexOfPoint).time.ToLongTimeString
                Else
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTrades + indexOfPoint > pageList(Tabs.SelectedIndex).cp.pointsTrades.Count) Then
                        TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades(pageList(Tabs.SelectedIndex).cp.lastPointTrades).time.ToLongTimeString
                    Else
                        TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades(pageList(Tabs.SelectedIndex).cp.currentPointTrades + indexOfPoint).time.ToLongTimeString
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub DrawLineTrades_Click(sender As Object, e As EventArgs) Handles DrawLineTrades0.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = True
        pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
    End Sub

    Private Sub TradesPctBox_MouseClick(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseClick, TradesPctBox9.MouseClick, TradesPctBox8.MouseClick, TradesPctBox7.MouseClick, TradesPctBox6.MouseClick, TradesPctBox5.MouseClick, TradesPctBox4.MouseClick, TradesPctBox2.MouseClick, TradesPctBox1.MouseMove, TradesPctBox1.MouseClick
        If (pageList IsNot Nothing) Then
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.needDrawLineTrades And Not pageList(Tabs.SelectedIndex).cp.isDrawingStartedTrades) Then
                    pageList(Tabs.SelectedIndex).cp.point1Trades.X = e.X
                    pageList(Tabs.SelectedIndex).cp.point1Trades.Y = e.Y
                    pageList(Tabs.SelectedIndex).cp.isDrawingStartedTrades = True
                    Exit Sub
                End If
                If (pageList(Tabs.SelectedIndex).cp.needDrawLineTrades And pageList(Tabs.SelectedIndex).cp.isDrawingStartedTrades) Then
                    pageList(Tabs.SelectedIndex).cp.point2Trades.X = e.X
                    pageList(Tabs.SelectedIndex).cp.point2Trades.Y = e.Y
                    pageList(Tabs.SelectedIndex).cp.isDrawingStartedTrades = False
                    pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = True
                    pageList(Tabs.SelectedIndex).cp.paintingTrades(TradesPctBox0, TimesTradesPctBox0, PricesTradesPctBox0, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub TradesTab_Click(sender As Object, e As EventArgs) Handles TradesTab0.Click

    End Sub

    Private Sub TabControl_TabIndexChanged(sender As Object, e As EventArgs)


    End Sub

    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tabs.SelectedIndexChanged
        If (pageList IsNot Nothing) Then
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed) Then
                    Try
                        pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False
                        pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
                        If (pageList(Tabs.SelectedIndex).Chart.SelectedIndex = 0) Then
                            pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
                        Else
                            pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub VolumesTradesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles VolumesTradesPctBox0.MouseMove, VolumesTradesPctBox9.MouseMove, VolumesTradesPctBox8.MouseMove, VolumesTradesPctBox7.MouseMove, VolumesTradesPctBox6.MouseMove, VolumesTradesPctBox5.MouseMove, VolumesTradesPctBox4.MouseMove, VolumesTradesPctBox3.MouseMove, VolumesTradesPctBox2.MouseMove, VolumesTradesPctBox1.MouseMove
        If (pageList.Count > 0) Then
            If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTrades = 0) Then
                Try
                    Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeVolumesTrades - (e.Y / TradesPctBox0.Height) * pageList(Tabs.SelectedIndex).cp.yRangeVolumesTrades
                    VolumeLabel.Text = Format(proportion, "0.00")

                    Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(Tabs.SelectedIndex).cp.intervalTrades))
                    If (indexOfPoint < 0) Then
                        indexOfPoint = 0
                    End If
                    If (indexOfPoint >= pageList(Tabs.SelectedIndex).cp.pointsTrades.Count) Then
                        indexOfPoint = pageList(Tabs.SelectedIndex).cp.pointsTrades.Count - 1
                        CurVolumeLabel.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades(indexOfPoint).tradeVolume
                    Else
                        If (pageList(Tabs.SelectedIndex).cp.currentPointTrades + indexOfPoint > pageList(Tabs.SelectedIndex).cp.pointsTrades.Count) Then
                            CurVolumeLabel.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades(pageList(Tabs.SelectedIndex).cp.lastPointTrades).tradeVolume

                        Else
                            CurVolumeLabel.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades(pageList(Tabs.SelectedIndex).cp.currentPointTrades + indexOfPoint).tradeVolume
                        End If
                    End If
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub
End Class
