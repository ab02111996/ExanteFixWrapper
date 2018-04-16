Imports System.Timers
Imports System.Threading.Tasks

Public Class Page
    Public bufferTrades As Buffer
    Public subscribeInfo As SubscribeInfo
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
    Public Chart As TabControl
    Public VolumesTradesPctBox As PictureBox
    Public VolumesVolumesTradesPctBox As PictureBox
    Public TabId As Integer
    Public listOfClonedForms As List(Of Form1Clone)
    Private movingAvg As MovingAverage


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
                   MinusTradesButton As Button,
                   Chart As TabControl,
                   VolumesTradesPctBox As PictureBox,
                   VolumesVolumesTradesPctBox As PictureBox,
                   MovingAverage As MovingAverage)

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
        Me.movingAvg = MovingAverage
        'AddHandler LeftQuotesButton.Click 
        Me.bufferTrades = New Buffer(5000, False, "D:\Bases")
        AddHandler Me.bufferTrades.BufferClearing, AddressOf Add5SecondsPoint
        Me.Chart = Chart
        Me.VolumesTradesPctBox = VolumesTradesPctBox
        Me.VolumesVolumesTradesPctBox = VolumesVolumesTradesPctBox
    End Sub

    Public Sub OnMarketDataUpdate(quotesInfo As QuotesInfo)
        If (quotesInfo.AskPrice IsNot Nothing And quotesInfo.BidPrice IsNot Nothing) Then
            'котировки
            Dim pq As New PointQuotes(quotesInfo.AskPrice, quotesInfo.AskVolume, quotesInfo.BidPrice, quotesInfo.BidVolume, quotesInfo.TimeStamp)
            Me.cp.pointsQuotes.Add(pq)
            If QuotesPctBox.IsHandleCreated Then
                Me.QuotesPctBox.Invoke(Sub()
                                           If (Not Me.cp.isDrawingStartedQuotes) Then
                                               Me.cp.paintingQuotes(QuotesPctBox, TimesQuotesPctBox, PricesQuotesPctBox)
                                               Dim selInd = Form1.Tabs.SelectedIndex
                                               Dim c = Form1.pageList(selInd).cp.pointsQuotes.Count
                                               If (selInd = Me.TabId) Then
                                                   Form1.AskPriceLabel.Text = Form1.pageList(selInd).cp.pointsQuotes(c - 1).askPrice
                                                   Form1.BidPriceLabel.Text = Form1.pageList(selInd).cp.pointsQuotes(c - 1).bidPrice
                                               End If
                                           End If
                                       End Sub)
            End If
            bufferTrades.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
        Else
            'сделки
            quotesInfo.MovingAverage = movingAvg.Calculate(quotesInfo.TradePrice)
            Console.WriteLine(quotesInfo.MovingAverage)
            bufferTrades.PutInBuffer(quotesInfo)
            cp.pointsTrades.Add(New PointTrades(quotesInfo.TradePrice, quotesInfo.TradeVolume, quotesInfo.TimeStamp))

            Dim ticksOrSeconds As ComboBox
            If (cp.isCloned) Then
                ticksOrSeconds = CType(cp.usedForm, Form1Clone).TicksOrSeconds
            Else
                ticksOrSeconds = CType(cp.usedForm, Form1).TicksOrSeconds
            End If

            If TradesPctBox.IsHandleCreated Then
                Me.TradesPctBox.Invoke(Sub()
                                           If (ticksOrSeconds.SelectedItem = "Тики") Then
                                               Me.cp.paintingTrades(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox)
                                           End If
                                           Dim selInd = Form1.Tabs.SelectedIndex
                                           Dim c = Form1.pageList(selInd).cp.pointsTrades.Count
                                           If (selInd = Me.TabId) Then
                                               Form1.TradePriceLabel.Text = Form1.pageList(selInd).cp.pointsTrades(c - 1).tradePrice
                                               Form1.TradeVolumeLabel.Text = Form1.pageList(selInd).cp.pointsTrades(c - 1).tradeVolume
                                           End If
                                       End Sub)
            End If
        End If

        If (Me.listOfClonedForms IsNot Nothing) Then
            For Each clonedForm In listOfClonedForms
                clonedForm.pageList(0).OnMarketDataUpdate(quotesInfo)
            Next
        End If

    End Sub
    Public Sub Add5SecondsPoint(sender As Object, e As EventArgs)
        cp.pointsTrades5sec.Add(New PointTrades5sec(sender))
        Dim buffer = CType(sender, Buffer)
        If ((buffer.volumeBuy + buffer.volumeSell) > cp.maxVolumeTrades5sec) Then
            cp.maxVolumeTrades5sec = buffer.volumeBuy + buffer.volumeSell
        End If
        Console.WriteLine(CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (buffer.volumeBuy + buffer.volumeSell).ToString)

        Dim ticksOrSeconds As ComboBox
        If (cp.isCloned) Then
            ticksOrSeconds = CType(cp.usedForm, Form1Clone).TicksOrSeconds
        Else
            ticksOrSeconds = CType(cp.usedForm, Form1).TicksOrSeconds
        End If

        Me.TradesPctBox.Invoke(Sub()
                                   If (ticksOrSeconds.SelectedItem = "5 секунд") Then
                                       Me.cp.paintingTrades5sec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox)
                                   End If
                               End Sub)

        If (Me.listOfClonedForms IsNot Nothing) Then
            For Each clonedForm In listOfClonedForms
                clonedForm.pageList(0).Add5SecondsPoint(sender, e)
            Next
        End If
    End Sub

End Class

Public Class Buffer
    Public startTimeFrame As DateTime
    Public endTimeFrame As DateTime
    Public exanteID As String
    Public openPrice As Double
    Public highPrice As Double
    Public lowPrice As Double
    Public closePrice As Double
    Public lastClosePrice As Double
    Public midAskBid As Double
    Public volumeSell As Double
    Public volumeBuy As Double
    Public countSell As Integer
    Public countBuy As Integer
    Public priceSell As Double
    Public priceBuy As Double
    Public movingAverage As Double
    Private isQuotes As Boolean
    Private bufferIsNotEmpty As Boolean
    Private quotesInfos As List(Of QuotesInfo)
    Private timer As Timer
    Private dbWriter As DataBaseWriter
    Private _handlers As List(Of EventHandler)
    Public Custom Event BufferClearing As EventHandler
        AddHandler(ByVal value As EventHandler)
            _handlers.Add(value)
        End AddHandler

        RemoveHandler(ByVal value As EventHandler)
            If _handlers.Contains(value) Then
                _handlers.Remove(value)
            End If
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
            For Each handler As EventHandler In _handlers
                Try
                    handler.Invoke(sender, e)
                Catch ex As Exception
                    Debug.WriteLine("Exception while invoking event handler: " & ex.ToString())
                End Try
            Next
        End RaiseEvent
    End Event
    Private Sub InitBuffer()
        bufferIsNotEmpty = False
        openPrice = 0
        highPrice = 0
        lowPrice = 0
        closePrice = 0
        volumeSell = 0
        volumeBuy = 0
        countSell = 0
        countBuy = 0
        priceSell = 0
        priceBuy = 0
        movingAverage = 0
        quotesInfos = Nothing
    End Sub
    Public Sub New(timeframe As Double, isquotes As Boolean, dbPath As String)
        Me.timer = New Timer(timeframe)
        Me.isQuotes = isquotes
        Me.dbWriter = New DataBaseWriter()
        _handlers = New List(Of EventHandler)
        InitBuffer()
        dbWriter.SetDBPath(dbPath)

    End Sub
    Public Sub StartWritingData(exanteID As String)
        If Not timer.Enabled Then
            Me.exanteID = exanteID
            dbWriter.OpenConnection(Me.exanteID)
            Me.startTimeFrame = DateTime.Now
            Me.timer.Start()
            AddHandler Me.timer.Elapsed, AddressOf Me.Clear
            Me.lastClosePrice = 0
        End If
    End Sub
    Public Sub Clear(source As Object, e As ElapsedEventArgs)
        Me.endTimeFrame = Me.startTimeFrame.AddSeconds(5)
        If Me.openPrice = 0 And Me.closePrice = 0 Then
            If Me.lastClosePrice = 0 Then
                Me.openPrice = Me.midAskBid
                Me.closePrice = Me.midAskBid
                Me.highPrice = Me.midAskBid
                Me.lowPrice = Me.midAskBid
            Else
                Me.openPrice = Me.lastClosePrice
                Me.closePrice = Me.lastClosePrice
                Me.highPrice = Me.lastClosePrice
                Me.lowPrice = Me.lastClosePrice
            End If
        End If
        Me.lastClosePrice = Me.closePrice
        If Not Me.midAskBid = 0 Then
            RaiseEvent BufferClearing(Me, New EventArgs)
            dbWriter.InsertBufferIntoDB(Me)
            dbWriter.InsertBufferMetaDataIntoDB(Me)
        End If
        InitBuffer()
        Me.startTimeFrame = Me.endTimeFrame
    End Sub

    Public Sub PutInBuffer(info As QuotesInfo)
        If Not bufferIsNotEmpty Then
            Me.quotesInfos = New List(Of QuotesInfo)
            Me.exanteID = info.ExanteId
            Me.openPrice = info.TradePrice
            Me.highPrice = info.TradePrice
            Me.lowPrice = info.TradePrice
            Me.bufferIsNotEmpty = True
        End If
        quotesInfos.Add(info)
        If Me.highPrice < info.TradePrice Then
            Me.highPrice = info.TradePrice
        End If
        If Me.lowPrice > info.TradePrice Then
            Me.lowPrice = info.TradePrice
        End If
        If info.Direction = QuotesInfo.Directions.Sell Then
            Me.volumeSell += info.TradeVolume
            Me.countSell += 1
            Me.priceSell += info.TradePrice * info.TradeVolume
        Else
            Me.volumeBuy += info.TradeVolume
            Me.countBuy += 1
            Me.priceBuy += info.TradePrice * info.TradeVolume
        End If
        Me.movingAverage += info.MovingAverage
        Me.closePrice = info.TradePrice


    End Sub
    Public Function IsQuotesBuffer() As Boolean
        Return Me.isQuotes
    End Function
    Public Function GetBufferMetaData() As List(Of QuotesInfo)
        Return quotesInfos
    End Function
    Public Function IsNotEmpty() As Boolean
        Return bufferIsNotEmpty
    End Function
End Class