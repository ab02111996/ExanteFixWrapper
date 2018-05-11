Imports QuickFix
Imports System.Threading
Public Class Form1Clone
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Public pageList As List(Of Page) = New List(Of Page)
    Public isOnline As Boolean
    Public movingAverageWindowSize As Integer
    Private oldWidth As Integer
    Private oldHeight As Integer

    Public Sub New()

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        ' Добавьте все инициализирующие действия после вызова InitializeComponent().

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Public Sub CaseN_AndDraw()
        Select Case TicksOrSeconds.SelectedItem
            Case "5 секунд"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 5)
                End If
            Case "10 секунд"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades10sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 10)
                End If
            Case "15 секунд"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades15sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 15)
                End If
            Case "30 секунд"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades30sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 30)
                End If
            Case "1 минута"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades60sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 60)
                End If
            Case "5 минут"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades300sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 300)
                End If
            Case "10 минут"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades600sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 600)
                End If
            Case "15 минут"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades900sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 900)
                End If
            Case "30 минут"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades1800sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 1800)
                End If
            Case "1 час"
                If pageList(Tabs.SelectedIndex).cp.pointsTrades3600sec(0).highPrice <> 0 Then
                    pageList(Tabs.SelectedIndex).cp.paintingTradesNsec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox, 3600)
                End If
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        movingAverageWindowSize = WindowSizeTextBox.Text
        If (Not isOnline) Then
            AskPriceLabel.Dispose()
            BidPriceLabel.Dispose()
            TradePriceLabel.Dispose()
            TradeVolumeLabel.Dispose()
        End If
        'ExanteIDTextBox0.Hide()
        DoubleBuffered = True


        TicksOrSeconds.SelectedItem = "5 секунд"
        BuyPlusSell.Checked = True
        AddHandler Me.BuyAndSell.CheckedChanged, AddressOf RadiobuttonOnChange
        AddHandler Me.BuyPlusSell.CheckedChanged, AddressOf RadiobuttonOnChange
        TypeOfGraphic.SelectedItem = "Японские свечи"
        Try
            pageList(Tabs.SelectedIndex).cp.isSubscribed = True
            Tabs.TabPages(Tabs.SelectedIndex).Text = Form1.Tabs.TabPages(Form1.Tabs.SelectedIndex).Text
            Me.Text = Form1.Tabs.TabPages(Form1.Tabs.SelectedIndex).Text
        Catch ex As Exception
            MsgBox("Нет подключения")
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If feedReciever IsNot Nothing Then
        '    feedReciever.Logout()
        'End If
        'System.Windows.Forms.Application.Exit()
        Me.Hide()
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
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTradesNsec + pageList(Tabs.SelectedIndex).cp.pointsOnScreenTradesNsec > pointsTradesNsec.Count) Then
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
                    If (e.X - pageList(Tabs.SelectedIndex).cp.positionOfClickTrades.X > 10) Then
                        LeftTradesButton_Click(sender, e)
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

    Private Sub TicksOrSeconds_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TicksOrSeconds.SelectedIndexChanged
        If (TicksOrSeconds.SelectedItem = "Тики") Then
            If pageList(Tabs.SelectedIndex).cp.pointsTrades(0).tradePrice <> 0 And pageList(Tabs.SelectedIndex).cp.pointsTrades(0).tradeVolume <> 0 Then
                pageList(Tabs.SelectedIndex).cp.currentPointTrades = pageList(Tabs.SelectedIndex).cp.pointsTrades.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades - 1
                If pageList(Tabs.SelectedIndex).cp.currentPointTrades < 0 Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades = 0
                End If
                pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = True
                pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
            End If

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
            If (pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False) Then
                pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
            Else
                pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = False
                pageList(Tabs.SelectedIndex).cp.paintingQuotes(pageList(Tabs.SelectedIndex).QuotesPctBox, pageList(Tabs.SelectedIndex).TimesQuotesPctBox, pageList(Tabs.SelectedIndex).PricesQuotesPctBox)
                pageList(Tabs.SelectedIndex).cp.needRePaintingQuotes = True
            End If
        Else
            If (TicksOrSeconds.SelectedItem = "Тики") Then
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
        End If
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
        movingAverageWindowSize = WindowSizeTextBox.Text
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades5sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades10sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades15sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades30sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades60sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades300sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades600sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades900sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades1800sec)
        ReCalculateMovingAverage(pageList(Tabs.SelectedIndex).cp.pointsTrades3600sec)
        TypeOfGraphic_SelectedIndexChanged(sender, e)
    End Sub

    Public Sub ReCalculateMovingAverage(pointsTradesNsec As List(Of PointTradesNsec))
        Dim movingAvgBuy As New MovingAverage(movingAverageWindowSize)
        Dim movingAvgSell As New MovingAverage(movingAverageWindowSize)
        Dim movingAvgBuyPlusSell As New MovingAverage(movingAverageWindowSize)
        For index = 0 To pointsTradesNsec.Count - 1
            pointsTradesNsec(index).avgBuy = movingAvgBuy.Calculate(pointsTradesNsec(index).volumeBuy)
            pointsTradesNsec(index).avgSell = movingAvgSell.Calculate(pointsTradesNsec(index).volumeSell)
            pointsTradesNsec(index).avgBuyPlusSell = movingAvgBuyPlusSell.Calculate(pointsTradesNsec(index).volumeBuy + pointsTradesNsec(index).volumeSell)
        Next
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
