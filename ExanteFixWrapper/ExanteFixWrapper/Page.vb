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
    Private movingAvgBuy As MovingAverage
    Private movingAvgSell As MovingAverage
    Private movingAvgBuyPlusSell As MovingAverage
    Private counter15sec As Integer
    Private counter30sec As Integer
    Private counter60sec As Integer
    Private counter300sec As Integer
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
        'AddHandler LeftQuotesButton.Click 
        Me.bufferTrades = New Buffer(5000, False, "D:\Bases")
        AddHandler Me.bufferTrades.BufferClearing, AddressOf Add5SecondsPoint
        Me.Chart = Chart
        Me.VolumesTradesPctBox = VolumesTradesPctBox
        Me.VolumesVolumesTradesPctBox = VolumesVolumesTradesPctBox
        Me.counter15sec = 0
        Me.counter30sec = 0
        Me.counter60sec = 0
        Me.counter300sec = 0
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
        Else
            'сделки
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

    Public Sub AddNSecondsPointOffline(pointsTrades5sec As List(Of PointTradesNsec))
        Dim counter15sec As Integer = 0
        Dim counter30sec As Integer = 0
        Dim counter60sec As Integer = 0
        Dim counter300sec As Integer = 0
        Dim counter900sec As Integer = 0
        Dim counter1800sec As Integer = 0
        Dim counter3600sec As Integer = 0
        For index = 0 To pointsTrades5sec.Count - 1
            counter15sec += 1
            counter30sec += 1
            counter60sec += 1
            counter300sec += 1
            counter900sec += 1
            counter1800sec += 1
            counter3600sec += 1
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
        Dim count As Integer
        If (cp.isCloned) Then
            If (CType(cp.usedForm, Form1Clone).isOnline) Then
                count = cp.pointsTrades5sec.Count
            Else
                count = _index + 1
            End If
        Else
            If (CType(cp.usedForm, Form1).isOnline) Then
                count = cp.pointsTrades5sec.Count
            Else
                count = _index + 1
            End If
        End If

        Dim point As New PointTradesNsec
        point.time = cp.pointsTrades5sec((count - 1) - (counterNsec - 1)).time
        point.openPrice = cp.pointsTrades5sec((count - 1) - (counterNsec - 1)).openPrice
        point.closePrice = cp.pointsTrades5sec(count - 1).closePrice
        Dim highPrice As Double
        Dim lowPrice As Double
        Dim volumeBuy As Double = 0
        Dim volumeSell As Double = 0
        For index = (count - 1) - (counterNsec - 1) To (count - 1)
            volumeBuy += cp.pointsTrades5sec(index).volumeBuy
            volumeSell += cp.pointsTrades5sec(index).volumeSell
            If index = ((count - 1) - (counterNsec - 1)) Then
                highPrice = cp.pointsTrades5sec((count - 1) - (counterNsec - 1)).highPrice
                lowPrice = cp.pointsTrades5sec((count - 1) - (counterNsec - 1)).lowPrice
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
        point.avgBuy = cp.pointsTrades5sec(count - 1).avgBuy
        point.avgSell = cp.pointsTrades5sec(count - 1).avgSell
        point.avgBuyPlusSell = cp.pointsTrades5sec(count - 1).avgBuyPlusSell
        Console.WriteLine(counterNsec.ToString + " " + point.time.ToString + " " + point.highPrice.ToString + " " + point.openPrice.ToString + " " + point.closePrice.ToString + " " + point.lowPrice.ToString + " " + (point.volumeBuy + point.volumeSell).ToString)
        If (counterNsec = 3) Then
            cp.pointsTrades15sec.Add(point)
        ElseIf (counterNsec = 6) Then
            cp.pointsTrades30sec.Add(point)
        ElseIf (counterNsec = 12) Then
            cp.pointsTrades60sec.Add(point)
        ElseIf (counterNsec = 60) Then
            cp.pointsTrades300sec.Add(point)
        ElseIf (counterNsec = 180) Then
            cp.pointsTrades900sec.Add(point)
        ElseIf (counterNsec = 360) Then
            cp.pointsTrades1800sec.Add(point)
        ElseIf (counterNsec = 720) Then
            cp.pointsTrades3600sec.Add(point)
        End If
    End Sub

    Public Sub Add5SecondsPoint(sender As Object, e As EventArgs)
        Dim buffer = CType(sender, Buffer)
        Dim newPoint As New PointTradesNsec(sender)
        newPoint.avgBuy = movingAvgBuy.Calculate(newPoint.volumeBuy)
        newPoint.avgSell = movingAvgSell.Calculate(newPoint.volumeSell)
        newPoint.avgBuyPlusSell = movingAvgBuyPlusSell.Calculate(newPoint.volumeBuy + newPoint.volumeSell)
        cp.pointsTrades5sec.Add(newPoint)

        counter15sec += 1
        counter30sec += 1
        counter60sec += 1
        counter300sec += 1
        counter900sec += 1
        counter1800sec += 1
        counter3600sec += 1

        Console.WriteLine(CType(sender, Buffer).endTimeFrame.ToString + " " + CType(sender, Buffer).highPrice.ToString + " " + CType(sender, Buffer).openPrice.ToString + " " + CType(sender, Buffer).closePrice.ToString + " " + CType(sender, Buffer).lowPrice.ToString + " " + (Buffer.volumeBuy + Buffer.volumeSell).ToString)
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
                                   If (Not ticksOrSeconds.SelectedItem = "Тики") Then
                                       Select Case ticksOrSeconds.SelectedItem
                                           Case "5 секунд"
                                               cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 5)
                                           Case "15 секунд"
                                               cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 15)
                                           Case "30 секунд"
                                               cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 30)
                                           Case "1 минута"
                                               cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 60)
                                           Case "5 минут"
                                               cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 300)
                                           Case "15 минут"
                                               cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 900)
                                           Case "30 минут"
                                               cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 1800)
                                           Case "1 час"
                                               cp.paintingTradesNsec(TradesPctBox, TimesTradesPctBox, PricesTradesPctBox, VolumesTradesPctBox, VolumesVolumesTradesPctBox, 3600)
                                       End Select
                                   End If
                               End Sub)

        If (Me.listOfClonedForms IsNot Nothing) Then
            For Each clonedForm In listOfClonedForms
                clonedForm.pageList(0).Add5SecondsPoint(sender, e)
            Next
        End If
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
    Public countSell As Integer 'Кол-во продаж 
    Public countBuy As Integer 'Кол-во покупок
    Public priceSell As Double 'Общая цена продаж
    Public priceBuy As Double 'Общая цена покупок
    Private isQuotes As Boolean 'Является ли буфером для котировок
    Private bufferIsNotEmpty As Boolean 'Буфер не пустой
    Private quotesInfos As List(Of QuotesInfo) 'Список для накопления данных
    Private timer As Timer 'Таймер для отмера временного промежутка
    Private dbWriter As DataBaseWriter 'Объект класса для записи в БД
    Private _handlers As List(Of EventHandler) 'Список обработчиков события очищения буфера
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
        countSell = 0
        countBuy = 0
        priceSell = 0
        priceBuy = 0
        quotesInfos = Nothing
    End Sub
    'Конструктор буфера
    Public Sub New(timeframe As Double, isquotes As Boolean, dbPath As String)
        Me.timer = New Timer(timeframe)
        Me.isQuotes = isquotes
        Me.dbWriter = New DataBaseWriter()
        _handlers = New List(Of EventHandler)
        InitBuffer()
        dbWriter.SetDBPath(dbPath)
    End Sub
    'Начало записи данных в БД
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
    'Очищение буфера и запись накопленных данных в БД
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
        Me.closePrice = info.TradePrice
    End Sub
    Public Function IsQuotesBuffer() As Boolean
        Return Me.isQuotes
    End Function
    'Метод для получения тиковой информации
    Public Function GetBufferMetaData() As List(Of QuotesInfo)
        Return quotesInfos
    End Function
    'Метод для определения пуст ли буфер
    Public Function IsNotEmpty() As Boolean
        Return bufferIsNotEmpty
    End Function
End Class