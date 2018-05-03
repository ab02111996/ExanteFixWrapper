Imports System.Timers
Imports System.Threading.Tasks

Public Class Page
    Public bufferTrades As Buffer
    Public bufferTrades10sec As Buffer
    Public bufferTrades15sec As Buffer
    Public bufferTrades30sec As Buffer
    Public bufferTrades60sec As Buffer
    Public bufferTrades300sec As Buffer
    Public bufferTrades600sec As Buffer
    Public bufferTrades900sec As Buffer
    Public bufferTrades1800sec As Buffer
    Public bufferTrades3600sec As Buffer
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
    Private movingAvgBuy As MovingAverage
    Private movingAvgSell As MovingAverage
    Private movingAvgBuyPlusSell As MovingAverage
    Private counter10sec As Integer
    Private counter15sec As Integer
    Private counter30sec As Integer
    Private counter60sec As Integer
    Private counter300sec As Integer
    Private counter600sec As Integer
    Private counter900sec As Integer
    Private counter1800sec As Integer
    Private counter3600sec As Integer

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
                   MovingAverageWindow As Integer)

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
        Me.movingAvgBuy = New MovingAverage(MovingAverageWindow)
        Me.movingAvgSell = New MovingAverage(MovingAverageWindow)
        Me.movingAvgBuyPlusSell = New MovingAverage(MovingAverageWindow)
        Me.listOfClonedForms = New List(Of Form1Clone)
        'AddHandler LeftQuotesButton.Click 
        Me.bufferTrades = New Buffer(5000, True, "D:\Bases")
        Me.bufferTrades.SetMovingAvg(Me.movingAvgBuy, Me.movingAvgSell, Me.movingAvgBuyPlusSell)
        Me.bufferTrades10sec = New Buffer(10000, False)
        Me.bufferTrades15sec = New Buffer(15000, False)
        Me.bufferTrades30sec = New Buffer(30000, False)
        Me.bufferTrades60sec = New Buffer(60000, False)
        Me.bufferTrades300sec = New Buffer(300000, False)
        Me.bufferTrades600sec = New Buffer(600000, False)
        Me.bufferTrades900sec = New Buffer(900000, False)
        Me.bufferTrades1800sec = New Buffer(1800000, False)
        Me.bufferTrades3600sec = New Buffer(3600000, False)
        AddHandler Me.bufferTrades.BufferClearing, AddressOf Add5SecondsPoint
        AddHandler Me.bufferTrades10sec.BufferClearing, AddressOf Add10SecondsPoint
        AddHandler Me.bufferTrades15sec.BufferClearing, AddressOf Add15SecondsPoint
        AddHandler Me.bufferTrades30sec.BufferClearing, AddressOf Add30SecondsPoint
        AddHandler Me.bufferTrades60sec.BufferClearing, AddressOf Add60SecondsPoint
        AddHandler Me.bufferTrades300sec.BufferClearing, AddressOf Add300SecondsPoint
        AddHandler Me.bufferTrades600sec.BufferClearing, AddressOf Add600SecondsPoint
        AddHandler Me.bufferTrades900sec.BufferClearing, AddressOf Add900SecondsPoint
        AddHandler Me.bufferTrades1800sec.BufferClearing, AddressOf Add1800SecondsPoint
        AddHandler Me.bufferTrades3600sec.BufferClearing, AddressOf Add3600SecondsPoint
        Me.Chart = Chart
        Me.VolumesTradesPctBox = VolumesTradesPctBox
        Me.VolumesVolumesTradesPctBox = VolumesVolumesTradesPctBox
        Me.counter10sec = 0
        Me.counter15sec = 0
        Me.counter30sec = 0
        Me.counter60sec = 0
        Me.counter300sec = 0
        Me.counter600sec = 0
        Me.counter900sec = 0
        Me.counter1800sec = 0
        Me.counter3600sec = 0
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
            bufferTrades10sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
            bufferTrades15sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
            bufferTrades30sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
            bufferTrades60sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
            bufferTrades300sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
            bufferTrades600sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
            bufferTrades900sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
            bufferTrades1800sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
            bufferTrades3600sec.midAskBid = (quotesInfo.AskPrice + quotesInfo.BidPrice) / 2
        Else
            'сделки
            bufferTrades.PutInBuffer(quotesInfo)
            bufferTrades10sec.PutInBuffer(quotesInfo)
            bufferTrades15sec.PutInBuffer(quotesInfo)
            bufferTrades30sec.PutInBuffer(quotesInfo)
            bufferTrades60sec.PutInBuffer(quotesInfo)
            bufferTrades300sec.PutInBuffer(quotesInfo)
            bufferTrades600sec.PutInBuffer(quotesInfo)
            bufferTrades900sec.PutInBuffer(quotesInfo)
            bufferTrades1800sec.PutInBuffer(quotesInfo)
            bufferTrades3600sec.PutInBuffer(quotesInfo)
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
                                           If Not ticksOrSeconds.SelectedItem = "Тики" Then
                                               If Not cp.isCloned Then
                                                   Dim N As Integer
                                                   Select Case ticksOrSeconds.SelectedItem
                                                       Case "5 секунд"
                                                           Dim point As New PointTradesNsec(bufferTrades)
                                                           Me.cp.pointsTrades5sec(cp.pointsTrades5sec.Count - 1) = point
                                                           N = 5
                                                           DrawEveryTick(N)
                                                           'If listOfClonedForms.Count > 0 Then
                                                           '    For Each form In listOfClonedForms
                                                           '        form.pageList(0).cp.pointsTrades5sec(form.pageList(0).cp.pointsTrades5sec.Count - 1) = point
                                                           '        If form.TicksOrSeconds.SelectedItem = "5 секунд" Then
                                                           '            form.pageList(0).DrawEveryTickInClonedForms(N, form)
                                                           '        End If
                                                           '    Next
                                                           'End If
                                                       Case "10 секунд"
                                                           Dim point As New PointTradesNsec(bufferTrades10sec)
                                                           Me.cp.pointsTrades10sec(cp.pointsTrades10sec.Count - 1) = point
                                                           N = 10
                                                           DrawEveryTick(N)
                                                       Case "15 секунд"
                                                           Dim point As New PointTradesNsec(bufferTrades15sec)
                                                           Me.cp.pointsTrades15sec(cp.pointsTrades15sec.Count - 1) = point
                                                           N = 15
                                                           DrawEveryTick(N)
                                                       Case "30 секунд"
                                                           Dim point As New PointTradesNsec(bufferTrades30sec)
                                                           Me.cp.pointsTrades30sec(cp.pointsTrades30sec.Count - 1) = point
                                                           N = 30
                                                           DrawEveryTick(N)
                                                       Case "1 минута"
                                                           Dim point As New PointTradesNsec(bufferTrades60sec)
                                                           Me.cp.pointsTrades60sec(cp.pointsTrades60sec.Count - 1) = point
                                                           N = 60
                                                           DrawEveryTick(N)
                                                       Case "5 минут"
                                                           Dim point As New PointTradesNsec(bufferTrades300sec)
                                                           Me.cp.pointsTrades300sec(cp.pointsTrades300sec.Count - 1) = point
                                                           N = 300
                                                           DrawEveryTick(N)
                                                       Case "10 минут"
                                                           Dim point As New PointTradesNsec(bufferTrades600sec)
                                                           Me.cp.pointsTrades600sec(cp.pointsTrades600sec.Count - 1) = point
                                                           N = 600
                                                           DrawEveryTick(N)
                                                       Case "15 минут"
                                                           Dim point As New PointTradesNsec(bufferTrades900sec)
                                                           Me.cp.pointsTrades900sec(cp.pointsTrades900sec.Count - 1) = point
                                                           N = 900
                                                           DrawEveryTick(N)
                                                       Case "30 минут"
                                                           Dim point As New PointTradesNsec(bufferTrades1800sec)
                                                           Me.cp.pointsTrades1800sec(cp.pointsTrades1800sec.Count - 1) = point
                                                           N = 1800
                                                           DrawEveryTick(N)
                                                       Case "60 минут"
                                                           Dim point As New PointTradesNsec(bufferTrades3600sec)
                                                           Me.cp.pointsTrades3600sec(cp.pointsTrades3600sec.Count - 1) = point
                                                           N = 3600
                                                           DrawEveryTick(N)
                                                   End Select
                                                   For Each form In listOfClonedForms

                                                       Select Case form.TicksOrSeconds.SelectedItem
                                                           Case "5 секунд"
                                                               Try
                                                                   Dim point As New PointTradesNsec(bufferTrades)
                                                                   form.pageList(0).cp.pointsTrades5sec(cp.pointsTrades5sec.Count - 1) = point
                                                                   N = 5
                                                                   DrawEveryTickInClonedForms(N, form)
                                                               Catch ex As Exception
                                                                   Console.WriteLine("Ошибка - Count = " + cp.pointsTrades5sec.Count.ToString)
                                                               End Try

                                                           Case "10 секунд"
                                                               Dim point As New PointTradesNsec(bufferTrades10sec)
                                                               form.pageList(0).cp.pointsTrades10sec(cp.pointsTrades10sec.Count - 1) = point
                                                               N = 10
                                                               DrawEveryTickInClonedForms(N, form)
                                                           Case "15 секунд"
                                                               Dim point As New PointTradesNsec(bufferTrades15sec)
                                                               form.pageList(0).cp.pointsTrades15sec(cp.pointsTrades15sec.Count - 1) = point
                                                               N = 15
                                                               DrawEveryTickInClonedForms(N, form)
                                                           Case "30 секунд"
                                                               Dim point As New PointTradesNsec(bufferTrades30sec)
                                                               form.pageList(0).cp.pointsTrades30sec(cp.pointsTrades30sec.Count - 1) = point
                                                               N = 30
                                                               DrawEveryTickInClonedForms(N, form)
                                                           Case "1 минута"
                                                               Dim point As New PointTradesNsec(bufferTrades60sec)
                                                               form.pageList(0).cp.pointsTrades60sec(cp.pointsTrades60sec.Count - 1) = point
                                                               N = 60
                                                               DrawEveryTickInClonedForms(N, form)
                                                           Case "5 минут"
                                                               Dim point As New PointTradesNsec(bufferTrades300sec)
                                                               form.pageList(0).cp.pointsTrades300sec(cp.pointsTrades300sec.Count - 1) = point
                                                               N = 300
                                                               DrawEveryTickInClonedForms(N, form)
                                                           Case "10 минут"
                                                               Dim point As New PointTradesNsec(bufferTrades600sec)
                                                               form.pageList(0).cp.pointsTrades600sec(cp.pointsTrades600sec.Count - 1) = point
                                                               N = 600
                                                               DrawEveryTickInClonedForms(N, form)
                                                           Case "15 минут"
                                                               Dim point As New PointTradesNsec(bufferTrades900sec)
                                                               form.pageList(0).cp.pointsTrades900sec(cp.pointsTrades900sec.Count - 1) = point
                                                               N = 900
                                                               DrawEveryTickInClonedForms(N, form)
                                                           Case "30 минут"
                                                               Dim point As New PointTradesNsec(bufferTrades1800sec)
                                                               form.pageList(0).cp.pointsTrades1800sec(cp.pointsTrades1800sec.Count - 1) = point
                                                               N = 1800
                                                               DrawEveryTickInClonedForms(N, form)
                                                           Case "60 минут"
                                                               Dim point As New PointTradesNsec(bufferTrades3600sec)
                                                               form.pageList(0).cp.pointsTrades3600sec(cp.pointsTrades3600sec.Count - 1) = point
                                                               N = 3600
                                                               DrawEveryTickInClonedForms(N, form)
                                                       End Select
                                                   Next
                                               End If
                                           End If
                                       End Sub)
            End If
        End If

        'If (Me.listOfClonedForms IsNot Nothing) Then
        '    For Each clonedForm In listOfClonedForms
        '        clonedForm.pageList(0).OnMarketDataUpdate(quotesInfo)
        '    Next
        'End If

    End Sub

    Private Sub DrawEveryTick(N As Integer)
        If (Me.cp.needRePaintingTradesNsec) Then
            cp.needRePaintingTradesNsec = False
            cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, N)
            cp.needRePaintingTradesNsec = True
        Else
            cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, N)
        End If
    End Sub

    Public Sub DrawEveryTickInClonedForms(N As Integer, form As Form1Clone)
        If (form.pageList(0).cp.needRePaintingTradesNsec) Then
            form.pageList(0).cp.needRePaintingTradesNsec = False
            form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, N)
            form.pageList(0).cp.needRePaintingTradesNsec = True
        Else
            form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, N)
        End If
    End Sub

    Public Sub AddNSecondsPointOffline(pointsTrades5sec As List(Of PointTradesNsec))
        Dim counter10sec As Integer = 0
        Dim counter15sec As Integer = 0
        Dim counter30sec As Integer = 0
        Dim counter60sec As Integer = 0
        Dim counter300sec As Integer = 0
        Dim counter600sec As Integer = 0
        Dim counter900sec As Integer = 0
        Dim counter1800sec As Integer = 0
        Dim counter3600sec As Integer = 0
        For index = 0 To pointsTrades5sec.Count - 1
            counter10sec += 1
            counter15sec += 1
            counter30sec += 1
            counter60sec += 1
            counter300sec += 1
            counter600sec += 1
            counter900sec += 1
            counter1800sec += 1
            counter3600sec += 1
            If counter10sec = 2 Then
                AddNSecondsPoint(counter10sec, index)
                counter10sec = 0
            End If
            If counter15sec = 3 Then
                AddNSecondsPoint(counter15sec, index)
                counter15sec = 0
            End If
            If counter30sec = 6 Then
                AddNSecondsPoint(counter30sec, index)
                counter30sec = 0
            End If
            If counter60sec = 12 Then
                AddNSecondsPoint(counter60sec, index)
                counter60sec = 0
            End If
            If counter300sec = 60 Then
                AddNSecondsPoint(counter300sec, index)
                counter300sec = 0
            End If
            If counter600sec = 120 Then
                AddNSecondsPoint(counter600sec, index)
                counter600sec = 0
            End If
            If counter900sec = 180 Then
                AddNSecondsPoint(counter900sec, index)
                counter900sec = 0
            End If
            If counter1800sec = 360 Then
                AddNSecondsPoint(counter1800sec, index)
                counter1800sec = 0
            End If
            If counter3600sec = 720 Then
                AddNSecondsPoint(counter3600sec, index)
                counter1800sec = 0
            End If
        Next
    End Sub

    Public Sub AddNSecondsPoint(counterNsec As Integer, _index As Integer)
        If Not cp.isCloned Then
            Dim count As Integer
            Dim ticksOrSeconds As String
            Dim isOnline As Boolean = False
            If (cp.isCloned) Then
                If (CType(cp.usedForm, Form1Clone).isOnline) Then
                    Me.cp.usedForm.Invoke(Sub()
                                              ticksOrSeconds = CType(cp.usedForm, Form1Clone).TicksOrSeconds.SelectedItem.ToString
                                          End Sub)
                    'ticksOrSeconds = CType(cp.usedForm, Form1Clone).TicksOrSeconds.SelectedItem
                    isOnline = True
                    count = cp.pointsTrades5sec.Count
                Else
                    count = _index + 2
                End If
            Else
                If (CType(cp.usedForm, Form1).isOnline) Then
                    Me.cp.usedForm.Invoke(Sub()
                                              ticksOrSeconds = CType(cp.usedForm, Form1).TicksOrSeconds.SelectedItem.ToString
                                          End Sub)
                    isOnline = True
                    count = cp.pointsTrades5sec.Count
                Else
                    count = _index + 2
                End If
            End If

            Dim point As New PointTradesNsec
            point.time = cp.pointsTrades5sec((count - 2) - (counterNsec - 1)).time
            point.openPrice = cp.pointsTrades5sec((count - 2) - (counterNsec - 1)).openPrice
            point.closePrice = cp.pointsTrades5sec(count - 2).closePrice
            Dim highPrice As Double
            Dim lowPrice As Double
            Dim volumeBuy As Double = 0
            Dim volumeSell As Double = 0
            For index = (count - 2) - (counterNsec - 1) To (count - 2)
                volumeBuy += cp.pointsTrades5sec(index).volumeBuy
                volumeSell += cp.pointsTrades5sec(index).volumeSell
                If index = ((count - 2) - (counterNsec - 1)) Then
                    highPrice = cp.pointsTrades5sec((count - 2) - (counterNsec - 1)).highPrice
                    lowPrice = cp.pointsTrades5sec((count - 2) - (counterNsec - 1)).lowPrice
                Else
                    If cp.pointsTrades5sec(index).highPrice > highPrice Then
                        highPrice = cp.pointsTrades5sec(index).highPrice
                    End If
                    If cp.pointsTrades5sec(index).lowPrice < lowPrice Then
                        lowPrice = cp.pointsTrades5sec(index).lowPrice
                    End If
                End If
            Next
            point.highPrice = highPrice
            point.lowPrice = lowPrice
            point.volumeBuy = volumeBuy
            point.volumeSell = volumeSell
            point.avgBuy = cp.pointsTrades5sec(count - 2).avgBuy
            point.avgSell = cp.pointsTrades5sec(count - 2).avgSell
            point.avgBuyPlusSell = cp.pointsTrades5sec(count - 2).avgBuyPlusSell
            ReCalculateMovingAverage()
            If (counterNsec = 3) Then
                If isOnline Then
                    cp.pointsTrades15sec(cp.pointsTrades15sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(15)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades15sec.Add(newPoint)
                    Me.TradesPctBox.Invoke(Sub()
                                               If listOfClonedForms.Count > 0 Then
                                                   For Each form In listOfClonedForms
                                                       form.pageList(0).cp.pointsTrades15sec(form.pageList(0).cp.pointsTrades15sec.Count - 1) = point
                                                       form.pageList(0).cp.pointsTrades15sec.Add(newPoint)
                                                       If form.TicksOrSeconds.SelectedItem = "15 секунд" Then
                                                           form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 15)
                                                       End If
                                                   Next
                                               End If
                                           End Sub)

                    If isOnline And ticksOrSeconds = "15 секунд" And cp.needRePaintingTradesNsec Then
                        Me.cp.usedForm.Invoke(Sub()
                                                  cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 15)
                                              End Sub)
                    End If
                Else
                    cp.pointsTrades15sec.Add(point)
                End If
            ElseIf (counterNsec = 2) Then
                If isOnline Then
                    cp.pointsTrades10sec(cp.pointsTrades10sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(10)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades10sec.Add(newPoint)
                    Me.TradesPctBox.Invoke(Sub()
                                               If listOfClonedForms.Count > 0 Then
                                                   For Each form In listOfClonedForms
                                                       form.pageList(0).cp.pointsTrades10sec(form.pageList(0).cp.pointsTrades10sec.Count - 1) = point
                                                       form.pageList(0).cp.pointsTrades10sec.Add(newPoint)
                                                       If form.TicksOrSeconds.SelectedItem = "10 секунд" Then
                                                           form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 10)
                                                       End If
                                                   Next
                                               End If
                                           End Sub)

                    If isOnline And ticksOrSeconds = "10 секунд" And cp.needRePaintingTradesNsec Then
                        Me.cp.usedForm.Invoke(Sub()
                                                  cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 10)
                                              End Sub)
                    End If
                Else
                    cp.pointsTrades10sec.Add(point)
                End If
            ElseIf (counterNsec = 6) Then
                If isOnline Then
                    cp.pointsTrades30sec(cp.pointsTrades30sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(30)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades30sec.Add(newPoint)
                    Me.TradesPctBox.Invoke(Sub()
                                               If listOfClonedForms.Count > 0 Then
                                                   For Each form In listOfClonedForms
                                                       form.pageList(0).cp.pointsTrades30sec(form.pageList(0).cp.pointsTrades30sec.Count - 1) = point
                                                       form.pageList(0).cp.pointsTrades30sec.Add(newPoint)
                                                       If form.TicksOrSeconds.SelectedItem = "30 секунд" Then
                                                           form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 30)
                                                       End If
                                                   Next
                                               End If
                                           End Sub)
                    If isOnline And ticksOrSeconds = "30 секунд" And cp.needRePaintingTradesNsec Then
                        Me.cp.usedForm.Invoke(Sub()
                                                  cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 30)
                                              End Sub)
                    End If
                Else
                    cp.pointsTrades30sec.Add(point)
                End If
            ElseIf (counterNsec = 12) Then
                If isOnline Then
                    cp.pointsTrades60sec(cp.pointsTrades60sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(60)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades60sec.Add(newPoint)
                    Me.cp.usedForm.Invoke(Sub()
                                              If listOfClonedForms.Count > 0 Then
                                                  For Each form In listOfClonedForms
                                                      form.pageList(0).cp.pointsTrades60sec(form.pageList(0).cp.pointsTrades60sec.Count - 1) = point
                                                      form.pageList(0).cp.pointsTrades60sec.Add(newPoint)
                                                      If form.TicksOrSeconds.SelectedItem = "1 минута" Then
                                                          form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 30)
                                                      End If
                                                  Next
                                              End If
                                          End Sub)
                    If isOnline And ticksOrSeconds = "1 минута" And cp.needRePaintingTradesNsec Then
                        Me.TradesPctBox.Invoke(Sub()
                                                   cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 60)
                                               End Sub)
                    End If
                Else
                    cp.pointsTrades60sec.Add(point)
                End If
            ElseIf (counterNsec = 60) Then
                If isOnline Then
                    cp.pointsTrades300sec(cp.pointsTrades300sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(300)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades300sec.Add(newPoint)
                    Me.TradesPctBox.Invoke(Sub()
                                               If listOfClonedForms.Count > 0 Then
                                                   For Each form In listOfClonedForms
                                                       form.pageList(0).cp.pointsTrades300sec(form.pageList(0).cp.pointsTrades300sec.Count - 1) = point
                                                       form.pageList(0).cp.pointsTrades300sec.Add(newPoint)
                                                       If form.TicksOrSeconds.SelectedItem = "5 минут" Then
                                                           form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 300)
                                                       End If
                                                   Next
                                               End If
                                           End Sub)
                    If isOnline And ticksOrSeconds = "5 минут" And cp.needRePaintingTradesNsec Then
                        Me.cp.usedForm.Invoke(Sub()
                                                  cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 300)
                                              End Sub)
                    End If
                Else
                    cp.pointsTrades300sec.Add(point)
                End If
            ElseIf (counterNsec = 120) Then
                If isOnline Then
                    cp.pointsTrades600sec(cp.pointsTrades600sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(600)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades600sec.Add(newPoint)
                    Me.TradesPctBox.Invoke(Sub()
                                               If listOfClonedForms.Count > 0 Then
                                                   For Each form In listOfClonedForms
                                                       form.pageList(0).cp.pointsTrades600sec(form.pageList(0).cp.pointsTrades600sec.Count - 1) = point
                                                       form.pageList(0).cp.pointsTrades600sec.Add(newPoint)
                                                       If form.TicksOrSeconds.SelectedItem = "10 минут" Then
                                                           ReCalculateMovingAverage()
                                                           form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 600)
                                                       End If
                                                   Next
                                               End If
                                           End Sub)
                    If isOnline And ticksOrSeconds = "10 минут" And cp.needRePaintingTradesNsec Then
                        Me.cp.usedForm.Invoke(Sub()
                                                  ReCalculateMovingAverage()
                                                  cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 600)
                                              End Sub)
                    End If
                Else
                    cp.pointsTrades600sec.Add(point)
                End If
            ElseIf (counterNsec = 180) Then
                If isOnline Then
                    cp.pointsTrades900sec(cp.pointsTrades900sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(900)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades900sec.Add(newPoint)
                    Me.TradesPctBox.Invoke(Sub()
                                               If listOfClonedForms.Count > 0 Then
                                                   For Each form In listOfClonedForms
                                                       form.pageList(0).cp.pointsTrades900sec(form.pageList(0).cp.pointsTrades900sec.Count - 1) = point
                                                       form.pageList(0).cp.pointsTrades900sec.Add(newPoint)
                                                       If form.TicksOrSeconds.SelectedItem = "15 минут" Then
                                                           ReCalculateMovingAverage()
                                                           form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 900)
                                                       End If
                                                   Next
                                               End If
                                           End Sub)
                    If isOnline And ticksOrSeconds = "15 минут" And cp.needRePaintingTradesNsec Then
                        Me.cp.usedForm.Invoke(Sub()
                                                  ReCalculateMovingAverage()
                                                  cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 900)
                                              End Sub)
                    End If
                Else
                    cp.pointsTrades900sec.Add(point)
                End If
            ElseIf (counterNsec = 360) Then
                If isOnline Then
                    cp.pointsTrades1800sec(cp.pointsTrades1800sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(1800)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades1800sec.Add(newPoint)
                    Me.TradesPctBox.Invoke(Sub()
                                               If listOfClonedForms.Count > 0 Then
                                                   For Each form In listOfClonedForms
                                                       form.pageList(0).cp.pointsTrades1800sec(form.pageList(0).cp.pointsTrades1800sec.Count - 1) = point
                                                       form.pageList(0).cp.pointsTrades1800sec.Add(newPoint)
                                                       If form.TicksOrSeconds.SelectedItem = "30 минут" Then
                                                           ReCalculateMovingAverage()
                                                           form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 1800)
                                                       End If
                                                   Next
                                               End If
                                           End Sub)
                    If isOnline And ticksOrSeconds = "30 минут" And cp.needRePaintingTradesNsec Then
                        Me.cp.usedForm.Invoke(Sub()
                                                  ReCalculateMovingAverage()
                                                  cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 1800)
                                              End Sub)
                    End If
                Else
                    cp.pointsTrades1800sec.Add(point)
                End If
            ElseIf (counterNsec = 720) Then
                If isOnline Then
                    cp.pointsTrades3600sec(cp.pointsTrades3600sec.Count - 1) = point
                    Dim newPoint As New PointTradesNsec()
                    newPoint.time = point.time.AddSeconds(3600)
                    SetPricesInNewPoint(newPoint, point)
                    cp.pointsTrades3600sec.Add(newPoint)
                    Me.TradesPctBox.Invoke(Sub()
                                               If listOfClonedForms.Count > 0 Then
                                                   For Each form In listOfClonedForms
                                                       form.pageList(0).cp.pointsTrades3600sec(form.pageList(0).cp.pointsTrades3600sec.Count - 1) = point
                                                       form.pageList(0).cp.pointsTrades3600sec.Add(newPoint)
                                                       If form.TicksOrSeconds.SelectedItem = "5 минут" Then
                                                           ReCalculateMovingAverage()
                                                           form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 3600)
                                                       End If
                                                   Next
                                               End If
                                           End Sub)
                    If isOnline And ticksOrSeconds = "1 час" And cp.needRePaintingTradesNsec Then
                        Me.cp.usedForm.Invoke(Sub()
                                                  ReCalculateMovingAverage()
                                                  cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 3600)
                                              End Sub)
                    End If
                Else
                    cp.pointsTrades3600sec.Add(point)
                End If
            End If
        End If
    End Sub

    Private Function GetMovingAvgWindowSize()
        Return Form1.WindowSizeTextBox.Text
    End Function

    Private Sub ReCalculateMovingAverage()
        ReCalculateMovingAvgList(Me.cp.pointsTrades5sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades10sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades15sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades30sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades60sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades300sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades600sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades900sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades1800sec)
        ReCalculateMovingAvgList(Me.cp.pointsTrades3600sec)
    End Sub

    Public Sub ReCalculateMovingAvgList(pointsTradesNsec As List(Of PointTradesNsec))
        Dim movingAverageWindowSize As Integer = GetMovingAvgWindowSize()
        Dim movingAvgBuy As New MovingAverage(movingAverageWindowSize)
        Dim movingAvgSell As New MovingAverage(movingAverageWindowSize)
        Dim movingAvgBuyPlusSell As New MovingAverage(movingAverageWindowSize)
        For index = 0 To pointsTradesNsec.Count - 1
            pointsTradesNsec(index).avgBuy = movingAvgBuy.Calculate(pointsTradesNsec(index).volumeBuy)
            pointsTradesNsec(index).avgSell = movingAvgSell.Calculate(pointsTradesNsec(index).volumeSell)
            pointsTradesNsec(index).avgBuyPlusSell = movingAvgBuyPlusSell.Calculate(pointsTradesNsec(index).volumeBuy + pointsTradesNsec(index).volumeSell)
        Next
    End Sub

    Private Sub SetPricesInNewPoint(newPoint As PointTradesNsec, point As PointTradesNsec)
        newPoint.highPrice = point.closePrice
        newPoint.openPrice = point.closePrice
        newPoint.closePrice = point.closePrice
        newPoint.lowPrice = point.closePrice
    End Sub

    Public Sub Add5SecondsPoint(sender As Object, e As EventArgs)
        If Not cp.isCloned Then
            Dim buffer = CType(sender, Buffer)
            Dim point As New PointTradesNsec(buffer)
            point.avgBuy = movingAvgBuy.Calculate(point.volumeBuy)
            point.avgSell = movingAvgSell.Calculate(point.volumeSell)
            point.avgBuyPlusSell = movingAvgBuyPlusSell.Calculate(point.volumeBuy + point.volumeSell)
            cp.pointsTrades5sec(cp.pointsTrades5sec.Count - 1) = point
            Dim newPoint As New PointTradesNsec()
            newPoint.time = point.time.AddSeconds(5)
            SetPricesInNewPoint(newPoint, point)
            cp.pointsTrades5sec.Add(newPoint)
            ReCalculateMovingAverage()
            If listOfClonedForms.Count > 0 Then
                For Each form In listOfClonedForms
                    form.pageList(0).cp.pointsTrades5sec(form.pageList(0).cp.pointsTrades5sec.Count - 1) = point
                    form.pageList(0).cp.pointsTrades5sec.Add(newPoint)
                Next
            End If

            counter10sec += 1
            counter15sec += 1
            counter30sec += 1
            counter60sec += 1
            counter300sec += 1
            counter600sec += 1
            counter900sec += 1
            counter1800sec += 1
            counter3600sec += 1

            If cp.isCloned Then
                Console.WriteLine("Cloned : 5 sec " + CType(sender, Buffer).endTimeFrame.ToString + ":" + CType(sender, Buffer).endTimeFrame.Millisecond.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (buffer.volumeBuy + buffer.volumeSell).ToString)
            Else
                Console.WriteLine("Not cloned :5 sec " + CType(sender, Buffer).endTimeFrame.ToString + ":" + CType(sender, Buffer).endTimeFrame.Millisecond.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (buffer.volumeBuy + buffer.volumeSell).ToString)
            End If

            If counter10sec = 2 Then
                AddNSecondsPoint(counter10sec, Nothing)
                counter10sec = 0
            End If
            If counter15sec = 3 Then
                AddNSecondsPoint(counter15sec, Nothing)
                counter15sec = 0
            End If
            If counter30sec = 6 Then
                AddNSecondsPoint(counter30sec, Nothing)
                counter30sec = 0
            End If
            If counter60sec = 12 Then
                AddNSecondsPoint(counter60sec, Nothing)
                counter60sec = 0
            End If
            If counter300sec = 60 Then
                AddNSecondsPoint(counter300sec, Nothing)
                counter300sec = 0
            End If
            If counter600sec = 120 Then
                AddNSecondsPoint(counter600sec, Nothing)
                counter600sec = 0
            End If
            If counter900sec = 180 Then
                AddNSecondsPoint(counter900sec, Nothing)
                counter900sec = 0
            End If
            If counter1800sec = 360 Then
                AddNSecondsPoint(counter1800sec, Nothing)
                counter1800sec = 0
            End If
            If counter3600sec = 720 Then
                AddNSecondsPoint(counter3600sec, Nothing)
                counter3600sec = 0
            End If
            Dim ticksOrSeconds As ComboBox
            If (cp.isCloned) Then
                ticksOrSeconds = CType(cp.usedForm, Form1Clone).TicksOrSeconds
            Else
                ticksOrSeconds = CType(cp.usedForm, Form1).TicksOrSeconds
            End If

            Me.TradesPctBox.Invoke(Sub()
                                       If (ticksOrSeconds.SelectedItem = "5 секунд") Then
                                           cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 5)
                                       End If
                                   End Sub)

            Me.TradesPctBox.Invoke(Sub()
                                       If listOfClonedForms.Count > 0 Then
                                           For Each form In listOfClonedForms
                                               If form.TicksOrSeconds.SelectedItem = "5 секунд" Then
                                                   form.pageList(0).cp.paintingTradesNsec(form.pageList(0).TradesPctBox, form.pageList(0).TimesTradesPctBox, form.pageList(0).PricesTradesPctBox, form.pageList(0).VolumesTradesPctBox, form.pageList(0).VolumesVolumesTradesPctBox, 5)
                                               End If
                                           Next
                                       End If
                                   End Sub)



            'If (Me.listOfClonedForms IsNot Nothing) Then
            '    For Each clonedForm In listOfClonedForms
            '        clonedForm.pageList(0).Add5SecondsPoint(sender, e)
            '    Next
            'End If
        End If
    End Sub
    Public Sub Add10SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("10 sec " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
    Public Sub Add15SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("15 sec " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
    Public Sub Add30SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("30 sec " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
    Public Sub Add60SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("1 min " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
    Public Sub Add300SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("5 min " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
    Public Sub Add600SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("10 min sec " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
    Public Sub Add900SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("15 min " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
    Public Sub Add1800SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("30 min " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
    Public Sub Add3600SecondsPoint(sender As Object, e As EventArgs)
        'Console.WriteLine("1 hour " + CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (CType(sender, Buffer).volumeBuy + CType(sender, Buffer).volumeSell).ToString)
    End Sub
End Class
'Класс накапливающий данные для определенного временного промежутка
Public Class Buffer
    Public startTimeFrame As DateTime 'Время начала промежутка
    Public endTimeFrame As DateTime 'Время конца промежутка
    Public exanteID As String 'Название инструмента
    Public openPrice As Double 'Открывающая цена
    Public highPrice As Double 'Максимальная цена
    Public lowPrice As Double 'Минимальная цена
    Public closePrice As Double 'Закрывающая цена
    Public lastClosePrice As Double 'Последняя закрывающая цена
    Public midAskBid As Double 'Среднее значение цены на текущий момент
    Public volumeSell As Double 'Объем продаж
    Public volumeBuy As Double 'Объем покупок
    Public avgVolumeSell As Double 'Значения сглаживающей
    Public avgVolumeBuy As Double
    Public avgVolumeCommon As Double
    Public countSell As Integer 'Кол-во продаж 
    Public countBuy As Integer 'Кол-во покупок
    Public priceSell As Double 'Общая цена продаж
    Public priceBuy As Double 'Общая цена покупок
    Private timeFrame As Integer ' Временной промежуток
    Private writeToDB As Boolean 'Записывать ли данные в базу
    Private bufferIsNotEmpty As Boolean 'Буфер не пустой
    Private quotesInfos As List(Of QuotesInfo) 'Список для накопления данных
    Private timer As Timer 'Таймер для отмера временного промежутка
    Private dbWriter As DataBaseWriter 'Объект класса для записи в БД
    Private _handlers As List(Of EventHandler) 'Список обработчиков события очищения буфера
    Private movingAvgBuy As MovingAverage
    Private movingAvgSell As MovingAverage
    Private movingAvgBuySell As MovingAverage
    'Событие очищения буфера
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
    'Метод инициализации буфера
    Private Sub InitBuffer()
        bufferIsNotEmpty = False
        openPrice = 0
        highPrice = 0
        lowPrice = 0
        closePrice = 0
        volumeSell = 0
        volumeBuy = 0
        avgVolumeSell = 0
        avgVolumeBuy = 0
        avgVolumeCommon = 0
        countSell = 0
        countBuy = 0
        priceSell = 0
        priceBuy = 0
        quotesInfos = Nothing
    End Sub
    'Конструктор буфера
    Public Sub New(timeframe As Integer, writeToDB As Boolean, Optional dbPath As String = "")
        Me.timeFrame = timeframe
        Me.timer = New Timer(timeframe)
        Me.writeToDB = writeToDB
        Me.dbWriter = New DataBaseWriter()
        _handlers = New List(Of EventHandler)
        InitBuffer()
        If Me.writeToDB Then
            dbWriter.SetDBPath(dbPath)
        End If
    End Sub
    'Начало записи данных в БД
    Public Sub StartWritingData(exanteID As String)
        If Not timer.Enabled Then
            Me.exanteID = exanteID
            If Me.writeToDB Then
                dbWriter.OpenConnection(Me.exanteID)
            End If
            Me.startTimeFrame = DateTime.Now
            Me.endTimeFrame = Me.startTimeFrame.AddMilliseconds(Me.timeFrame)
            Me.timer.Start()
            AddHandler Me.timer.Elapsed, AddressOf Me.Clear
            Me.lastClosePrice = 0
        End If
    End Sub
    'Очищение буфера и запись накопленных данных в БД
    Public Sub Clear(source As Object, e As ElapsedEventArgs)
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
            If Me.writeToDB Then
                dbWriter.InsertBufferIntoDB(Me)
                dbWriter.InsertBufferMetaDataIntoDB(Me)
            End If
        End If
        InitBuffer()
        Me.startTimeFrame = Me.endTimeFrame
        Me.endTimeFrame = Me.startTimeFrame.AddMilliseconds(Me.timeFrame)
    End Sub
    'Метод помещающий данные в буфер
    Public Sub PutInBuffer(info As QuotesInfo)
        'Если буфер пустой - запоминаем открывающую цену, инициализируем список с данными инициализируем максимальную и минимальную цену
        If Not bufferIsNotEmpty Then
            Me.quotesInfos = New List(Of QuotesInfo)
            Me.exanteID = info.ExanteId
            Me.openPrice = info.TradePrice
            Me.highPrice = info.TradePrice
            Me.lowPrice = info.TradePrice
            Me.bufferIsNotEmpty = True
        End If
        'Добавляем в список поступившие данные
        quotesInfos.Add(info)
        'Если текущая цена больше, чем записанная ранее максимальная - заменяем ее на текущую
        If Me.highPrice < info.TradePrice Then
            Me.highPrice = info.TradePrice
        End If
        'То же самое, но с минимальной ценой
        If Me.lowPrice > info.TradePrice Then
            Me.lowPrice = info.TradePrice
        End If
        'Считаем количество сделок по направлениям, если направление сделки не было определено, то считаем ее как Buy
        If info.Direction = QuotesInfo.Directions.Sell Then
            Me.volumeSell += info.TradeVolume
            Me.countSell += 1
            Me.priceSell += info.TradePrice * info.TradeVolume
        Else
            Me.volumeBuy += info.TradeVolume
            Me.countBuy += 1
            Me.priceBuy += info.TradePrice * info.TradeVolume
        End If
        If movingAvgBuy IsNot Nothing Then
            avgVolumeBuy = movingAvgBuy.Calculate(volumeBuy)
            avgVolumeSell = movingAvgSell.Calculate(volumeSell)
            avgVolumeCommon = movingAvgBuySell.Calculate(volumeSell + volumeBuy)
        End If
        Me.closePrice = info.TradePrice
    End Sub
    Public Sub SetMovingAvg(movingAvgBuy As MovingAverage, movingAvgSell As MovingAverage, movingAvgBuySell As MovingAverage)
        Me.movingAvgBuy = movingAvgBuy
        Me.movingAvgSell = movingAvgSell
        Me.movingAvgBuySell = movingAvgBuySell
    End Sub
    'Метод для получения тиковой информации
    Public Function GetBufferMetaData() As List(Of QuotesInfo)
        Return quotesInfos
    End Function
    'Метод для определения пуст ли буфер
    Public Function IsNotEmpty() As Boolean
        Return bufferIsNotEmpty
    End Function

End Class