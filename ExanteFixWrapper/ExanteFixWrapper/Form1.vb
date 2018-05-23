Imports QuickFix
Imports System.Threading
Public Class Form1
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Public pageList As List(Of Page) = New List(Of Page)
    Public isOnline As Boolean
    Public Shared movingAverageWindowSize As Integer
    Private oldWidth As Integer
    Private oldHeight As Integer

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

    Public Sub CaseN_AndDraw()
        Select Case TicksOrSeconds.SelectedItem
            Case "5 секунд"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 5)
            Case "10 секунд"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 10)
            Case "15 секунд"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 15)
            Case "30 секунд"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 30)
            Case "1 минута"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 60)
            Case "5 минут"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 300)
            Case "10 минут"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 600)
            Case "15 минут"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 900)
            Case "30 минут"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 1800)
            Case "1 час"
                pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 3600)
        End Select
    End Sub

    Private Sub GetHistory(sender As Object, e As EventArgs)
        Try
            Tabs.TabPages(Tabs.SelectedIndex).Text = ExanteIDTextBox0.Text
            Dim dbReader As New DataBaseReader("D:\Bases")
            Dim tuple = dbReader.GetListOfTrades5secPoints(ExanteIDTextBox0.Text)
            pageList(Tabs.SelectedIndex).cp.maxVolumeTradesNsec = tuple.Item2
            pageList(Tabs.SelectedIndex).cp.isSubscribed = True
            pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False
            pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
            pageList(Tabs.SelectedIndex).cp.pointsTrades5sec = tuple.Item1
            Dim movingAvgBuy As New MovingAverage(movingAverageWindowSize)
            Dim movingAvgSell As New MovingAverage(movingAverageWindowSize)
            Dim movingAvgBuyPlusSell As New MovingAverage(movingAverageWindowSize)
            For index = 0 To pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count - 1
                pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(index).avgBuy = movingAvgBuy.Calculate(pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(index).volumeBuy)
                pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(index).avgSell = movingAvgSell.Calculate(pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(index).volumeSell)
                pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(index).avgBuyPlusSell = movingAvgBuyPlusSell.Calculate(pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(index).volumeBuy + pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(index).volumeSell)
            Next
            pageList(Tabs.SelectedIndex).AddNSecondsPointOffline(pageList(Tabs.SelectedIndex).cp.pointsTrades5sec)
            CaseN_AndDraw()
            Charts0_SelectedIndexChanged(sender, e)
            pageList(Tabs.SelectedIndex).TabId = Tabs.SelectedIndex
        Catch ex As Exception
            MsgBox("Ошибка")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles SubscribreButton0.Click
        movingAverageWindowSize = WindowSizeTextBox.Text
        If (isOnline) Then
            Try
                pageList(Tabs.SelectedIndex).cp.isSubscribed = True
                Tabs.TabPages(Tabs.SelectedIndex).Text = ExanteIDTextBox0.Text
                Dim subscribes = feedReciever.GetSubscribeInfos()
                feedReciever.SubscribeForQuotes(ExanteIDTextBox0.Text, AddressOf pageList(Tabs.SelectedIndex).OnMarketDataUpdate)
                With pageList(Tabs.SelectedIndex)
                    .bufferTrades.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades10sec.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades15sec.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades30sec.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades60sec.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades300sec.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades600sec.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades900sec.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades1800sec.StartWritingData(ExanteIDTextBox0.Text)
                    .bufferTrades3600sec.StartWritingData(ExanteIDTextBox0.Text)
                    .TabId = Tabs.SelectedIndex
                    Dim firstPointNsec As New PointTradesNsec()
                    .cp.pointsTrades5sec.Add(firstPointNsec)
                    .cp.pointsTrades10sec.Add(firstPointNsec)
                    .cp.pointsTrades15sec.Add(firstPointNsec)
                    .cp.pointsTrades30sec.Add(firstPointNsec)
                    .cp.pointsTrades60sec.Add(firstPointNsec)
                    .cp.pointsTrades300sec.Add(firstPointNsec)
                    .cp.pointsTrades600sec.Add(firstPointNsec)
                    .cp.pointsTrades900sec.Add(firstPointNsec)
                    .cp.pointsTrades1800sec.Add(firstPointNsec)
                    .cp.pointsTrades3600sec.Add(firstPointNsec)
                End With
                pageList(Tabs.SelectedIndex).gettingHistory = True
                GetHistory(sender, e)
                pageList(Tabs.SelectedIndex).gettingHistory = False
            Catch ex As Exception
                MsgBox("Нет подключения")
            End Try
        Else
            GetHistory(sender, e)
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Not isOnline) Then
            Label1.Dispose()
            Button1.Dispose()
            AskPriceLabel.Dispose()
            BidPriceLabel.Dispose()
            TradePriceLabel.Dispose()
            TradeVolumeLabel.Dispose()
            SubscribreButton0.Text = "Загрузить"
        End If
        DoubleBuffered = True
        Dim newPage = New Page(New ChartPainting(Me), QuotesPctBox0, PricesQuotesPctBox0, TimesQuotesPctBox0, TradesPctBox0, PricesTradesPctBox0, TimesTradesPctBox0,
                LeftQuotesButton0, RightQuotesButton0, PlusQuotesButton0, MinusQuotesButton0, LeftTradesButton0, RightButtonTrades0, PlusTradesButton0, MinusTradesButton0, Charts0, VolumesTradesPctBox0, VolumesVolumesTradesPctBox0, WindowSizeTextBox.Text)
        pageList.Add(newPage)
        TicksOrSeconds.SelectedItem = "5 секунд"
        BuyPlusSell.Checked = True
        AddHandler Me.BuyAndSell.CheckedChanged, AddressOf RadiobuttonOnChange
        AddHandler Me.BuyPlusSell.CheckedChanged, AddressOf RadiobuttonOnChange
        AddHandler Me.MouseWheel, AddressOf MouseWheelScroll
        TypeOfGraphic.SelectedItem = "Японские свечи"
        pageList(Tabs.SelectedIndex).cp.isNeedShowAvg = False
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If feedReciever IsNot Nothing Then
            feedReciever.Logout()
        End If
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub QuotesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles QuotesPctBox0.MouseMove
        pageList(Tabs.SelectedIndex).cp.pointMouseMoveQuotes.X = e.X
        pageList(Tabs.SelectedIndex).cp.pointMouseMoveQuotes.Y = e.Y
        If (pageList.Count > 0) Then
            If (pageList(Tabs.SelectedIndex).cp.isSubscribed) Then
                Try
                    If (pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False) Then
                        pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
                    Else
                        pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False
                        pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
                        pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = True
                    End If
                    Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeQuotes - (e.Y / pageList(Tabs.SelectedIndex).QuotesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeQuotes
                    PriceLabel0.Text = Format((pageList(Tabs.SelectedIndex).cp.lowBorderQuotes) + proportion, "0.00")
                    pageList(Tabs.SelectedIndex).cp.currentQuotesPriceMM = Format((pageList(Tabs.SelectedIndex).cp.lowBorderQuotes) + proportion, "0.00")
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
                    If (pageList(Tabs.SelectedIndex).cp.isClickedQuotes) Then
                        If (e.X - pageList(Tabs.SelectedIndex).cp.positionOfClickQuotes.X > 50) Then
                            LeftQuotesButton_Click(sender, e)
                            pageList(Tabs.SelectedIndex).cp.positionOfClickQuotes = New PointF(e.X, e.Y)
                        End If
                    End If

                    If (pageList(Tabs.SelectedIndex).cp.isClickedQuotes) Then
                        If (e.X - pageList(Tabs.SelectedIndex).cp.positionOfClickQuotes.X < -50) Then
                            RightQuotesButton_Click(sender, e)
                            pageList(Tabs.SelectedIndex).cp.positionOfClickQuotes = New PointF(e.X, e.Y)
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

    Private Sub QuotesPctBox_MouseClick(sender As Object, e As MouseEventArgs) Handles QuotesPctBox0.MouseClick
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
    Private Sub LeftQuotesButton_Click(sender As Object, e As EventArgs) Handles LeftQuotesButton0.Click
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
    Private Sub RightQuotesButton_Click(sender As Object, e As EventArgs) Handles RightQuotesButton0.Click
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
    Private Sub PlusQuotesButton_Click(sender As Object, e As EventArgs) Handles PlusQuotesButton0.Click
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
    Private Sub MinusQuotesButton_Click(sender As Object, e As EventArgs) Handles MinusQuotesButton0.Click
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
    Private Sub LeftTradesButton_Click(sender As Object, e As EventArgs) Handles LeftTradesButton0.Click
        Select Case Me.TicksOrSeconds.SelectedItem
            Case "Тики"
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
            Case Else
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec - 10
                If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
                End If
                pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False
                Try
                    CaseN_AndDraw()
                Catch ex As Exception
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec -= 1
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
                    End If
                End Try
        End Select
    End Sub

    'right trades
    Private Sub RightTradesButton_Click(sender As Object, e As EventArgs) Handles RightButtonTrades0.Click
        Select Case Me.TicksOrSeconds.SelectedItem
            Case "Тики"
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
            Case Else
                Dim pointsTradesNsec As List(Of PointTradesNsec)
                Select Case TicksOrSeconds.SelectedItem
                    Case "5 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec
                    Case "10 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades10sec
                    Case "15 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades15sec
                    Case "30 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades30sec
                    Case "1 минута"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades60sec
                    Case "5 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades300sec
                    Case "10 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades600sec
                    Case "15 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades900sec
                    Case "30 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades1800sec
                    Case "1 час"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades3600sec
                    Case Else
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec
                End Select
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                If pointsTradesNsec.Count > pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec + 10
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec + pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec > pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pointsTradesNsec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec
                    End If

                    If (Not pageList(Tabs.SelectedIndex).cp.lastPointTradesNsec = pointsTradesNsec.Count - 1) Then
                        Try
                            CaseN_AndDraw()
                        Catch ex As Exception
                            pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec -= 1
                            If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0) Then
                                pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
                            End If
                        End Try
                    Else
                        pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = True
                    End If
                End If
        End Select
    End Sub

    '+ trades
    Private Sub PlusTradesButton_Click(sender As Object, e As EventArgs) Handles PlusTradesButton0.Click
        Select Case Me.TicksOrSeconds.SelectedItem
            Case "Тики"
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades += 15
                pageList(Tabs.SelectedIndex).cp.currentPointTrades -= 15
                If (pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
                End If
                If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades > pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenTrades) Then
                    pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades = pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenTrades
                    Exit Sub
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
            Case Else
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec += 15
                pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec -= 15
                If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
                End If
                If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec > pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenTradesNsec) Then
                    pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec = pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenTradesNsec
                    Exit Sub
                End If
                pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False
                If (pageList(Tabs.SelectedIndex).cp.lastPointTradesNsec < pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count - 1) Then
                    Try
                        CaseN_AndDraw()
                    Catch ex As Exception
                        pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec -= 1
                        If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0) Then
                            pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
                        End If
                    End Try
                Else
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec -= 7
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
                    End If
                    Try
                        CaseN_AndDraw()
                    Catch ex As Exception
                        pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec -= 1
                        If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0) Then
                            pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
                        End If
                    End Try
                End If
        End Select

    End Sub

    '- trades
    Private Sub MinusTradesButton_Click(sender As Object, e As EventArgs) Handles MinusTradesButton0.Click
        Select Case Me.TicksOrSeconds.SelectedItem
            Case "Тики"
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
            Case Else
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec -= 15
                If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec < pageList(Tabs.SelectedIndex).cp.minPointsOnScreenTradesNsec) Then
                    pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec = pageList(Tabs.SelectedIndex).cp.minPointsOnScreenTradesNsec
                End If
                pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False
                Try
                    CaseN_AndDraw()
                Catch ex As Exception
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec -= 1
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
                    End If
                End Try
        End Select
    End Sub

    Private Sub MouseWheelScroll(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        If pageList(Tabs.SelectedIndex).Chart.SelectedIndex = 1 Then
            If (e.Delta < -30) Then
                MinusTradesButton_Click(sender, e)
            End If

            If (e.Delta > 30) Then
                PlusTradesButton_Click(sender, e)
            End If
        Else
            If (e.Delta < -30) Then
                MinusQuotesButton_Click(sender, e)
            End If

            If (e.Delta > 30) Then
                PlusQuotesButton_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub TradesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseMove
        pageList(Tabs.SelectedIndex).cp.pointMouseMoveTrades.X = e.X
        pageList(Tabs.SelectedIndex).cp.pointMouseMoveTrades.Y = e.Y
        If (Me.TicksOrSeconds.SelectedItem = "Тики") Then
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False) Then
                    pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                Else
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
                    pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = True
                End If
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTrades = 0 And Not pageList(Tabs.SelectedIndex).cp.pointsTrades.Count = 0) Then
                    Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeTrades - (e.Y / pageList(Tabs.SelectedIndex).TradesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeTrades
                    PriceLabel0.Text = Format((pageList(Tabs.SelectedIndex).cp.lowBorderTrades) + proportion, "0.00")
                    pageList(Tabs.SelectedIndex).cp.currentTradePriceMM = Format((pageList(Tabs.SelectedIndex).cp.lowBorderTrades) + proportion, "0.00")
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

                If (pageList(Tabs.SelectedIndex).cp.isClickedTrades) Then
                    If (e.X - pageList(Tabs.SelectedIndex).cp.positionOfClickTrades.X > 50) Then
                        LeftTradesButton_Click(sender, e)
                        pageList(Tabs.SelectedIndex).cp.positionOfClickTrades = New PointF(e.X, e.Y)
                    End If
                End If

                If (pageList(Tabs.SelectedIndex).cp.isClickedTrades) Then
                    If (e.X - pageList(Tabs.SelectedIndex).cp.positionOfClickTrades.X < -50) Then
                        RightTradesButton_Click(sender, e)
                        pageList(Tabs.SelectedIndex).cp.positionOfClickTrades = New PointF(e.X, e.Y)
                    End If
                End If
            End If
        Else
            If (pageList.Count > 0) Then
                Dim pointsTradesNsec As List(Of PointTradesNsec)
                Select Case TicksOrSeconds.SelectedItem
                    Case "5 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec
                    Case "10 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades10sec
                    Case "15 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades15sec
                    Case "30 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades30sec
                    Case "1 минута"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades60sec
                    Case "5 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades300sec
                    Case "10 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades600sec
                    Case "15 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades900sec
                    Case "30 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades1800sec
                    Case "1 час"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades3600sec
                    Case Else
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec
                End Select
                If (pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False) Then
                    CaseN_AndDraw()
                Else
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False
                    CaseN_AndDraw()
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = True
                End If
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTradesNsec = 0 And Not pointsTradesNsec.Count = 0) Then
                    Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeTradesNsec - (e.Y / pageList(Tabs.SelectedIndex).TradesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeTradesNsec
                    PriceLabel0.Text = Format((pageList(Tabs.SelectedIndex).cp.lowBorderTradesNsec) + proportion, "0.00")
                    pageList(Tabs.SelectedIndex).cp.currentTradePriceMM = Format((pageList(Tabs.SelectedIndex).cp.lowBorderTradesNsec) + proportion, "0.00")
                    Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(Tabs.SelectedIndex).cp.intervalTradesNsec))
                    If (indexOfPoint < 0) Then
                        indexOfPoint = 0
                    End If
                    If (indexOfPoint >= pointsTradesNsec.Count) Then
                        indexOfPoint = pointsTradesNsec.Count - 1
                        If (indexOfPoint < 0) Then
                            indexOfPoint = 0
                        End If
                        TimeLabel0.Text = pointsTradesNsec(indexOfPoint).time.ToLongTimeString
                    Else
                        If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec + indexOfPoint > pointsTradesNsec.Count) Then
                            TimeLabel0.Text = pointsTradesNsec(pageList(Tabs.SelectedIndex).cp.lastPointTradesNsec).time.ToLongTimeString
                        Else
                            TimeLabel0.Text = pointsTradesNsec(pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec + indexOfPoint).time.ToLongTimeString
                        End If
                    End If
                End If
            End If
            If (pageList(Tabs.SelectedIndex).cp.isClickedTrades) Then
                If (e.X - pageList(Tabs.SelectedIndex).cp.positionOfClickTrades.X < -50) Then
                    RightTradesButton_Click(sender, e)
                    pageList(Tabs.SelectedIndex).cp.positionOfClickTrades = New PointF(e.X, e.Y)
                End If
            End If

            If (pageList(Tabs.SelectedIndex).cp.isClickedTrades) Then
                If (e.X - pageList(Tabs.SelectedIndex).cp.positionOfClickTrades.X > 50) Then
                    LeftTradesButton_Click(sender, e)
                    pageList(Tabs.SelectedIndex).cp.positionOfClickTrades = New PointF(e.X, e.Y)
                End If
            End If
        End If
    End Sub

    Private Sub DrawLineTrades_Click(sender As Object, e As EventArgs) Handles DrawLineTrades0.Click
        pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = True
        pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
    End Sub

    Private Sub TradesPctBox_MouseClick(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseClick
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
                            If (TicksOrSeconds.SelectedItem = "Тики") Then
                                pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                            Else
                                CaseN_AndDraw()
                            End If

                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub VolumesTradesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles VolumesTradesPctBox0.MouseMove
        pageList(Tabs.SelectedIndex).cp.pointMouseMoveVolumes.X = e.X
        pageList(Tabs.SelectedIndex).cp.pointMouseMoveVolumes.Y = e.Y
        If (TicksOrSeconds.SelectedItem = "Тики") Then
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTrades = 0) Then
                    Try
                        Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeVolumesTrades - (e.Y / pageList(Tabs.SelectedIndex).VolumesTradesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeVolumesTrades
                        VolumeLabel.Text = Format(proportion, "0.00")
                        pageList(Tabs.SelectedIndex).cp.currentVolumeMM = Format(proportion, "0.00")
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
                        If (pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False) Then
                            pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                        Else
                            pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
                            pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                            pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = True
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
        Else
            If (pageList.Count > 0) Then
                Dim pointsTradesNsec As List(Of PointTradesNsec)
                Select Case TicksOrSeconds.SelectedItem
                    Case "5 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec
                    Case "10 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades10sec
                    Case "15 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades15sec
                    Case "30 секунд"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades30sec
                    Case "1 минута"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades60sec
                    Case "5 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades300sec
                    Case "10 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades600sec
                    Case "15 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades900sec
                    Case "30 минут"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades1800sec
                    Case "1 час"
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades3600sec
                    Case Else
                        pointsTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec
                End Select
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTradesNsec = 0) Then
                    Try
                        Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeVolumesTradesNsec - (e.Y / pageList(Tabs.SelectedIndex).VolumesTradesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeVolumesTradesNsec
                        VolumeLabel.Text = Format(proportion, "0.00")
                        pageList(Tabs.SelectedIndex).cp.currentVolumeMM = Format(proportion, "0.00")
                        Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(Tabs.SelectedIndex).cp.intervalTradesNsec))
                        If (indexOfPoint < 0) Then
                            indexOfPoint = 0
                        End If
                        If (indexOfPoint >= pointsTradesNsec.Count) Then
                            indexOfPoint = pointsTradesNsec.Count - 1
                            CurVolumeLabel.Text = pointsTradesNsec(indexOfPoint).volumeBuy + pointsTradesNsec(indexOfPoint).volumeSell
                        Else
                            If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec + indexOfPoint > pointsTradesNsec.Count) Then
                                CurVolumeLabel.Text = pointsTradesNsec(pageList(Tabs.SelectedIndex).cp.lastPointTradesNsec).volumeBuy + pointsTradesNsec(pageList(Tabs.SelectedIndex).cp.lastPointTradesNsec).volumeSell

                            Else
                                CurVolumeLabel.Text = pointsTradesNsec(pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec + indexOfPoint).volumeSell + pointsTradesNsec(pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec + indexOfPoint).volumeBuy
                            End If
                        End If
                        If (pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False) Then
                            CaseN_AndDraw()
                        Else
                            pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False
                            CaseN_AndDraw()
                            pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = True
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles AddTab.Click
        Dim QuotesPctBox = New PictureBox()
        Dim PricesQuotesPctBox = New PictureBox()
        Dim TimesQuotesPctBox = New PictureBox()
        Dim TradesPctBox = New PictureBox()
        Dim PricesTradesPctBox = New PictureBox()
        Dim TimesTradesPctBox = New PictureBox()
        Dim VolumesTradesPctBox = New PictureBox()
        Dim VolumesVolumesTradesPctBox = New PictureBox()
        Dim LeftQuotesButton = New Button()
        Dim RightQuotesButton = New Button()
        Dim PlusQuotesButton = New Button()
        Dim MinusQuotesButton = New Button()
        Dim LeftTradesButton = New Button()
        Dim RightTradesButton = New Button()
        Dim PlusTradesButton = New Button()
        Dim MinusTradesButton = New Button()
        Dim Charts = New TabControl()
        Dim TabPage = New TabPage()
        Dim QuotesTab = New TabPage()
        Dim TradesTab = New TabPage()

        TabPage.Controls.Add(Charts)
        TabPage.Location = New System.Drawing.Point(4, 25)
        TabPage.Name = "TabPage"
        TabPage.Padding = New System.Windows.Forms.Padding(3)
        TabPage.Size = New System.Drawing.Size(1709, 845)
        TabPage.TabIndex = 0
        TabPage.Text = "TabPage"
        TabPage.UseVisualStyleBackColor = True
        Tabs.Controls.Add(TabPage)

        Charts.Location = New System.Drawing.Point(3, 6)
        Charts.Name = "Charts"
        Charts.SelectedIndex = 0
        Charts.Size = New System.Drawing.Size(1270, 650)
        Charts.TabIndex = 30
        '
        'PricesQuotesPctBox0
        '
        PricesQuotesPctBox.Location = New System.Drawing.Point(2, 2)
        PricesQuotesPctBox.Name = "PricesQuotesPctBox"
        'PricesQuotesPctBox.BackColor = Color.Green
        PricesQuotesPctBox.Size = New System.Drawing.Size(79, 545)
        PricesQuotesPctBox.TabIndex = 20
        PricesQuotesPctBox.TabStop = False
        '
        'MinusQuotesButton0
        '
        MinusQuotesButton.Location = New System.Drawing.Point(1225, 268)
        MinusQuotesButton.Name = "MinusQuotesButton"
        MinusQuotesButton.Size = New System.Drawing.Size(34, 265)
        MinusQuotesButton.TabIndex = 29
        MinusQuotesButton.Text = "-"
        MinusQuotesButton.UseVisualStyleBackColor = True
        '
        'QuotesPctBox0
        '
        QuotesPctBox.Location = New System.Drawing.Point(82, 2)
        'QuotesPctBox.BackColor = Color.Gray
        QuotesPctBox.Name = "QuotesPctBox"
        QuotesPctBox.Size = New System.Drawing.Size(1139, 545)
        QuotesPctBox.TabIndex = 18
        QuotesPctBox.TabStop = False
        '
        'PlusQuotesButton0
        '
        PlusQuotesButton.Location = New System.Drawing.Point(1225, 2)
        PlusQuotesButton.Name = "PlusQuotesButton"
        PlusQuotesButton.Size = New System.Drawing.Size(34, 260)
        PlusQuotesButton.TabIndex = 28
        PlusQuotesButton.Text = "+"
        PlusQuotesButton.UseVisualStyleBackColor = True
        '
        'RightQuotesButton0
        '
        RightQuotesButton.Location = New System.Drawing.Point(655, 595)
        RightQuotesButton.Name = "RightQuotesButton"
        RightQuotesButton.Size = New System.Drawing.Size(570, 27)
        RightQuotesButton.TabIndex = 27
        RightQuotesButton.Text = "Right ->"
        RightQuotesButton.UseVisualStyleBackColor = True
        '
        'LeftQuotesButton0
        '
        LeftQuotesButton.Location = New System.Drawing.Point(80, 595)
        LeftQuotesButton.Name = "LeftQuotesButton"
        LeftQuotesButton.Size = New System.Drawing.Size(570, 27)
        LeftQuotesButton.TabIndex = 26
        LeftQuotesButton.Text = "<- Left"
        LeftQuotesButton.UseVisualStyleBackColor = True
        '
        'TimesQuotesPctBox0
        '
        'TimesQuotesPctBox.BackColor = Color.Pink
        TimesQuotesPctBox.Location = New System.Drawing.Point(82, 549)
        TimesQuotesPctBox.Name = "TimesQuotesPctBox"
        TimesQuotesPctBox.Size = New System.Drawing.Size(1139, 36)
        TimesQuotesPctBox.TabIndex = 22
        TimesQuotesPctBox.TabStop = False

        '
        'VolumesVolumesTradesPctBox0
        '
        'VolumesVolumesTradesPctBox.BackColor = Color.DeepSkyBlue
        VolumesVolumesTradesPctBox.Location = New System.Drawing.Point(2, 350)
        VolumesVolumesTradesPctBox.Name = "VolumesVolumesTradesPctBox"
        VolumesVolumesTradesPctBox.Size = New System.Drawing.Size(79, 195)
        VolumesVolumesTradesPctBox.TabIndex = 39
        VolumesVolumesTradesPctBox.TabStop = False
        '
        'VolumesTradesPctBox0
        '
        'VolumesTradesPctBox.BackColor = Color.DimGray
        VolumesTradesPctBox.Location = New System.Drawing.Point(82, 350)
        VolumesTradesPctBox.Name = "VolumesTradesPctBox"
        VolumesTradesPctBox.Size = New System.Drawing.Size(1139, 195)
        VolumesTradesPctBox.TabIndex = 38
        VolumesTradesPctBox.TabStop = False
        '
        'PricesTradesPctBox0
        '
        'PricesTradesPctBox.BackColor = Color.Red
        PricesTradesPctBox.Location = New System.Drawing.Point(2, 2)
        PricesTradesPctBox.Name = "PricesTradesPctBox"
        PricesTradesPctBox.Size = New System.Drawing.Size(79, 342)
        PricesTradesPctBox.TabIndex = 37
        PricesTradesPctBox.TabStop = False
        '
        'MinusTradesButton0
        '
        MinusTradesButton.Location = New System.Drawing.Point(1225, 268)
        MinusTradesButton.Name = "MinusTradesButton"
        MinusTradesButton.Size = New System.Drawing.Size(34, 265)
        MinusTradesButton.TabIndex = 29
        MinusTradesButton.Text = "-"
        MinusTradesButton.UseVisualStyleBackColor = True
        '
        'TradesPctBox0
        '
        'TradesPctBox.BackColor = Color.Gray
        TradesPctBox.Location = New System.Drawing.Point(82, 2)
        TradesPctBox.Name = "TradesPctBox"
        TradesPctBox.Size = New System.Drawing.Size(1139, 342)
        TradesPctBox.TabIndex = 30
        TradesPctBox.TabStop = False
        '
        'PlusTradesButton0
        '
        PlusTradesButton.Location = New System.Drawing.Point(1225, 2)
        PlusTradesButton.Name = "PlusTradesButton"
        PlusTradesButton.Size = New System.Drawing.Size(34, 260)
        'PlusTradesButton.TabIndex = 28
        PlusTradesButton.Text = "+"
        PlusTradesButton.UseVisualStyleBackColor = True
        '
        'RightButtonTrades0
        '
        RightTradesButton.Location = New System.Drawing.Point(655, 595)
        RightTradesButton.Name = "RightTradesButton"
        RightTradesButton.Size = New System.Drawing.Size(570, 27)
        RightTradesButton.TabIndex = 27
        RightTradesButton.Text = "Right ->"
        RightTradesButton.UseVisualStyleBackColor = True
        '
        'LeftTradesButton0
        '
        LeftTradesButton.Location = New System.Drawing.Point(80, 595)
        LeftTradesButton.Name = "LeftTradesButton"
        LeftTradesButton.Size = New System.Drawing.Size(570, 27)
        LeftTradesButton.TabIndex = 26
        LeftTradesButton.Text = "<- Left"
        LeftTradesButton.UseVisualStyleBackColor = True
        '
        'TimesTradesPctBox0
        '
        'TimesTradesPctBox.BackColor = Color.DeepPink
        TimesTradesPctBox.Location = New System.Drawing.Point(82, 549)
        TimesTradesPctBox.Name = "TimesTradesPctBox"
        TimesTradesPctBox.Size = New System.Drawing.Size(1139, 36)
        TimesTradesPctBox.TabIndex = 22
        TimesTradesPctBox.TabStop = False


        QuotesTab.Controls.Add(PricesQuotesPctBox)
        QuotesTab.Controls.Add(MinusQuotesButton)
        QuotesTab.Controls.Add(QuotesPctBox)
        QuotesTab.Controls.Add(PlusQuotesButton)
        QuotesTab.Controls.Add(RightQuotesButton)
        QuotesTab.Controls.Add(LeftQuotesButton)
        QuotesTab.Controls.Add(TimesQuotesPctBox)
        QuotesTab.Location = New System.Drawing.Point(4, 25)
        QuotesTab.Name = "QuotesTab"
        QuotesTab.Padding = New System.Windows.Forms.Padding(3)
        QuotesTab.Size = New System.Drawing.Size(1689, 772)
        'QuotesTab.TabIndex = 0
        QuotesTab.Text = "Аск / Бид"
        QuotesTab.UseVisualStyleBackColor = True

        '
        'TradesTab0
        '
        TradesTab.Controls.Add(VolumesVolumesTradesPctBox)
        TradesTab.Controls.Add(VolumesTradesPctBox)
        TradesTab.Controls.Add(PricesTradesPctBox)
        TradesTab.Controls.Add(MinusTradesButton)
        TradesTab.Controls.Add(TradesPctBox)
        TradesTab.Controls.Add(PlusTradesButton)
        TradesTab.Controls.Add(RightTradesButton)
        TradesTab.Controls.Add(LeftTradesButton)
        TradesTab.Controls.Add(TimesTradesPctBox)
        TradesTab.Location = New System.Drawing.Point(4, 25)
        TradesTab.Name = "TradesTab"
        TradesTab.Padding = New System.Windows.Forms.Padding(3)
        TradesTab.Size = New System.Drawing.Size(1689, 772)
        TradesTab.TabIndex = 1
        TradesTab.Text = "Сделки"
        TradesTab.UseVisualStyleBackColor = True

        Charts.Controls.Add(QuotesTab)
        Charts.Controls.Add(TradesTab)

        AddHandler LeftQuotesButton.Click, AddressOf Me.LeftQuotesButton_Click
        AddHandler RightQuotesButton.Click, AddressOf Me.RightQuotesButton_Click
        AddHandler PlusQuotesButton.Click, AddressOf Me.PlusQuotesButton_Click
        AddHandler MinusQuotesButton.Click, AddressOf Me.MinusQuotesButton_Click
        AddHandler LeftTradesButton.Click, AddressOf Me.LeftTradesButton_Click
        AddHandler RightTradesButton.Click, AddressOf Me.RightTradesButton_Click
        AddHandler PlusTradesButton.Click, AddressOf Me.PlusTradesButton_Click
        AddHandler MinusTradesButton.Click, AddressOf Me.MinusTradesButton_Click
        AddHandler TradesPctBox.MouseMove, AddressOf Me.TradesPctBox_MouseMove
        AddHandler QuotesPctBox.MouseMove, AddressOf Me.QuotesPctBox_MouseMove
        AddHandler VolumesTradesPctBox.MouseMove, AddressOf Me.VolumesTradesPctBox_MouseMove
        AddHandler Charts.SelectedIndexChanged, AddressOf Me.Charts0_SelectedIndexChanged
        AddHandler TradesPctBox.MouseDown, AddressOf Me.TradesPctBox0_MouseDown
        AddHandler TradesPctBox.MouseUp, AddressOf Me.TradesPctBox0_MouseUp
        AddHandler TradesPctBox.MouseEnter, AddressOf Me.TradesPctBox0_MouseEnter
        AddHandler TradesPctBox.MouseLeave, AddressOf Me.TradesPctBox0_MouseLeave
        AddHandler VolumesTradesPctBox.MouseEnter, AddressOf Me.VolumesTradesPctBox0_MouseEnter
        AddHandler VolumesTradesPctBox.MouseLeave, AddressOf Me.VolumesTradesPctBox0_MouseLeave
        AddHandler QuotesPctBox.MouseEnter, AddressOf Me.QuotesPctBox0_MouseEnter
        AddHandler QuotesPctBox.MouseLeave, AddressOf Me.QuotesPctBox0_MouseLeave
        AddHandler QuotesPctBox.MouseDown, AddressOf Me.QuotesPctBox0_MouseDown
        AddHandler QuotesPctBox.MouseUp, AddressOf Me.QuotesPctBox0_MouseUp

        Dim newPage = New Page(New ChartPainting(Me), QuotesPctBox, PricesQuotesPctBox, TimesQuotesPctBox, TradesPctBox, PricesTradesPctBox, TimesTradesPctBox,
               LeftQuotesButton, RightQuotesButton, PlusQuotesButton, MinusQuotesButton, LeftTradesButton, RightTradesButton, PlusTradesButton, MinusTradesButton, Charts, VolumesTradesPctBox, VolumesVolumesTradesPctBox, WindowSizeTextBox.Text)
        pageList.Add(newPage)
    End Sub

    Private Sub TicksOrSeconds_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TicksOrSeconds.SelectedIndexChanged
        If (TicksOrSeconds.SelectedItem = "Тики") Then
            pageList(Tabs.SelectedIndex).cp.currentPointTrades = pageList(Tabs.SelectedIndex).cp.pointsTrades.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades - 1
            If pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0 Then
                pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
            End If
            pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = True
            pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
        Else
            Select Case TicksOrSeconds.SelectedItem
                Case "5 секунд"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "10 секунд"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades10sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "15 секунд"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades15sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "30 секунд"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades30sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "1 минута"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades60sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "5 минут"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades300sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "10 минут"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades600sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "15 минут"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades900sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "30 минут"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades1800sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
                Case "1 час"
                    pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = pageList(Tabs.SelectedIndex).cp.pointsTrades3600sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec - 1
            End Select
            If Not isOnline Then
                pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec += 1
            End If
            If pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec < 0 Then
                pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec = 0
            End If
            pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = True
            CaseN_AndDraw()
        End If
    End Sub

    Private Sub TypeOfGraphic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypeOfGraphic.SelectedIndexChanged
        If pageList.Count > 0 Then
            If (pageList(Tabs.SelectedIndex).Chart.SelectedIndex = 1) Then
                If (Not TicksOrSeconds.SelectedItem = "Тики") Then
                    If (pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False) Then
                        CaseN_AndDraw()
                    Else
                        pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False
                        CaseN_AndDraw()
                        pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = True
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub Charts0_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Charts0.SelectedIndexChanged
        If (pageList(Tabs.SelectedIndex).Chart.SelectedIndex = 0) Then
            pageList(Tabs.SelectedIndex).cp.currentPointQuotes = pageList(Tabs.SelectedIndex).cp.pointsQuotes.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenQuotes - 1
            If pageList(Tabs.SelectedIndex).cp.currentPointQuotes < 0 Then
                pageList(Tabs.SelectedIndex).cp.currentPointQuotes = 0
            End If
            pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = True
            pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
        Else
            TicksOrSeconds_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cloneForm As Form1Clone = New Form1Clone()
        cloneForm.isOnline = Me.isOnline
        Dim newPage = New Page(New ChartPainting(cloneForm), cloneForm.QuotesPctBox0, cloneForm.PricesQuotesPctBox0, cloneForm.TimesQuotesPctBox0, cloneForm.TradesPctBox0, cloneForm.PricesTradesPctBox0, cloneForm.TimesTradesPctBox0,
                cloneForm.LeftQuotesButton0, cloneForm.RightQuotesButton0, cloneForm.PlusQuotesButton0, cloneForm.MinusQuotesButton0, cloneForm.LeftTradesButton0, cloneForm.RightButtonTrades0, cloneForm.PlusTradesButton0, cloneForm.MinusTradesButton0, cloneForm.Charts0, cloneForm.VolumesTradesPctBox0, cloneForm.VolumesVolumesTradesPctBox0, Me.WindowSizeTextBox.Text)
        cloneForm.pageList.Add(newPage)
        cloneForm.pageList(0).cp.isCloned = True
        If (Not isOnline) Then
            cloneForm.pageList(0).cp.pointsTrades5sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades5sec
            cloneForm.pageList(0).cp.pointsTrades10sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades10sec
            cloneForm.pageList(0).cp.pointsTrades15sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades15sec
            cloneForm.pageList(0).cp.pointsTrades30sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades30sec
            cloneForm.pageList(0).cp.pointsTrades60sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades60sec
            cloneForm.pageList(0).cp.pointsTrades300sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades300sec
            cloneForm.pageList(0).cp.pointsTrades600sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades600sec
            cloneForm.pageList(0).cp.pointsTrades900sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades900sec
            cloneForm.pageList(0).cp.pointsTrades1800sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades1800sec
            cloneForm.pageList(0).cp.pointsTrades3600sec = Me.pageList(Tabs.SelectedIndex).cp.pointsTrades3600sec
        Else
            With cloneForm.pageList(0)
                .bufferTrades.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades10sec.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades15sec.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades30sec.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades60sec.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades300sec.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades600sec.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades900sec.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades1800sec.StartWritingData(ExanteIDTextBox0.Text)
                .bufferTrades3600sec.StartWritingData(ExanteIDTextBox0.Text)
                .TabId = Tabs.SelectedIndex
                Dim firstPointNsec As New PointTradesNsec()
                .cp.pointsTrades5sec.Add(firstPointNsec)
                .cp.pointsTrades10sec.Add(firstPointNsec)
                .cp.pointsTrades15sec.Add(firstPointNsec)
                .cp.pointsTrades30sec.Add(firstPointNsec)
                .cp.pointsTrades60sec.Add(firstPointNsec)
                .cp.pointsTrades300sec.Add(firstPointNsec)
                .cp.pointsTrades600sec.Add(firstPointNsec)
                .cp.pointsTrades900sec.Add(firstPointNsec)
                .cp.pointsTrades1800sec.Add(firstPointNsec)
                .cp.pointsTrades3600sec.Add(firstPointNsec)
            End With
        End If
        'cloneForm.ExanteIDTextBox0.Text = Me.ExanteIDTextBox0.Text
        cloneForm.Show()
        'cloneForm.TicksOrSeconds.SelectedItem = "5 секунд"
        'cloneForm.TypeOfGraphic.SelectedItem = "Японские свечи"
        pageList(Tabs.SelectedIndex).listOfClonedForms.Add(cloneForm)
    End Sub

    Private Sub RadiobuttonOnChange(sender As System.Object, e As System.EventArgs)
        Dim rb = CType(sender, RadioButton)
        TypeOfGraphic_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub Original_CheckedChanged(sender As Object, e As EventArgs) Handles Original.CheckedChanged
        Try
            TypeOfGraphic_SelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Average_CheckedChanged(sender As Object, e As EventArgs) Handles Average.CheckedChanged
        Original_CheckedChanged(sender, e)
    End Sub

    Private Sub WindowSizeBtn_Click(sender As Object, e As EventArgs) Handles WindowSizeBtn.Click
        pageList(Tabs.SelectedIndex).movingAvgBuy = New MovingAverage(WindowSizeTextBox.Text)
        pageList(Tabs.SelectedIndex).movingAvgSell = New MovingAverage(WindowSizeTextBox.Text)
        pageList(Tabs.SelectedIndex).movingAvgBuyPlusSell = New MovingAverage(WindowSizeTextBox.Text)
        pageList(Tabs.SelectedIndex).ReCalculateMovingAverage()
        TypeOfGraphic_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub TradesPctBox0_MouseEnter(sender As Object, e As EventArgs) Handles TradesPctBox0.MouseEnter
        pageList(Tabs.SelectedIndex).cp.isCursorOnTradesChart = True
    End Sub

    Private Sub TradesPctBox0_MouseLeave(sender As Object, e As EventArgs) Handles TradesPctBox0.MouseLeave
        pageList(Tabs.SelectedIndex).cp.isCursorOnTradesChart = False
        If (Me.TicksOrSeconds.SelectedItem = "Тики") Then
            If (pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False) Then
                pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
            Else
                pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
                pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = True
            End If
        Else
            If (pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False) Then
                CaseN_AndDraw()
            Else
                pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = False
                CaseN_AndDraw()
                pageList(Tabs.SelectedIndex).cp.needRePaintingTradesNsec = True
            End If
        End If
    End Sub

    Private Sub VolumesTradesPctBox0_MouseEnter(sender As Object, e As EventArgs) Handles VolumesTradesPctBox0.MouseEnter
        pageList(Tabs.SelectedIndex).cp.isCursorOnVolumesChart = True
    End Sub

    Private Sub VolumesTradesPctBox0_MouseLeave(sender As Object, e As EventArgs) Handles VolumesTradesPctBox0.MouseLeave
        pageList(Tabs.SelectedIndex).cp.isCursorOnVolumesChart = False
        TradesPctBox0_MouseLeave(sender, e)
    End Sub
    Private Sub Form1_ResizeBegin(sender As Object, e As EventArgs) Handles MyBase.ResizeBegin
        Me.oldHeight = Me.Height
        Me.oldWidth = Me.Width
    End Sub

    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        Dim deltaH, deltaW As Integer
        If Me.oldHeight <> Me.Height Or Me.oldWidth <> Me.Width Then
            deltaH = Me.Height - Me.oldHeight
            deltaW = Me.Width - Me.oldWidth
            Tabs.Height += deltaH
            Tabs.Width += deltaW
            ResizeChildren(Tabs, deltaH, deltaW)
        End If
    End Sub
    Private Sub ResizeChildren(control As Control, deltaH As Integer, deltaW As Integer)
        For Each child As Control In control.Controls
            If child.Name.IndexOf("Button") = -1 Then
                If child.Name.IndexOf("Prices") = -1 And child.Name.IndexOf("VolumesVolumes") = -1 Then
                    child.Width += deltaW
                End If
                If child.Name.IndexOf("Times") = -1 And child.Name.IndexOf("VolumesVolumes") = -1 And child.Name.IndexOf("VolumesTrades") = -1 Then
                    child.Height += deltaH
                End If
                If child.Name.IndexOf("Times") <> -1 Or child.Name.IndexOf("VolumesVolumes") <> -1 Or child.Name.IndexOf("VolumesTrades") <> -1 Then
                    child.Top += deltaH
                End If
            Else
                child.Top += deltaH
                child.Left += deltaW
            End If

            If child.HasChildren Then
                ResizeChildren(child, deltaH, deltaW)
            End If
        Next
    End Sub

    Private Sub TradesPctBox0_MouseUp(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseUp
        pageList(Tabs.SelectedIndex).cp.isClickedTrades = False
    End Sub

    Private Sub TradesPctBox0_MouseDown(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseDown
        pageList(Tabs.SelectedIndex).cp.isClickedTrades = True
        pageList(Tabs.SelectedIndex).cp.positionOfClickTrades = New PointF(e.X, e.Y)
    End Sub

    Private Sub QuotesPctBox0_MouseUp(sender As Object, e As MouseEventArgs) Handles QuotesPctBox0.MouseUp
        pageList(Tabs.SelectedIndex).cp.isClickedQuotes = False
    End Sub

    Private Sub QuotesPctBox0_MouseDown(sender As Object, e As MouseEventArgs) Handles QuotesPctBox0.MouseDown
        pageList(Tabs.SelectedIndex).cp.isClickedQuotes = True
        pageList(Tabs.SelectedIndex).cp.positionOfClickQuotes = New PointF(e.X, e.Y)
    End Sub

    Private Sub QuotesPctBox0_MouseEnter(sender As Object, e As EventArgs) Handles QuotesPctBox0.MouseEnter
        pageList(Tabs.SelectedIndex).cp.isCursorOnQuotesChart = True
    End Sub

    Private Sub QuotesPctBox0_MouseLeave(sender As Object, e As EventArgs) Handles QuotesPctBox0.MouseLeave
        pageList(Tabs.SelectedIndex).cp.isCursorOnQuotesChart = False
        If (pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False) Then
            pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
        Else
            pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False
            pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
            pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = True
        End If
    End Sub
End Class
