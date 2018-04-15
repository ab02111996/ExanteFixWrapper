Imports QuickFix
Imports System.Threading
Public Class Form1Clone
    Dim fixConfigPath As String = "FIX\fix_vendor.ini"
    Dim feedReciever As QuoteFixReciever
    Public pageList As List(Of Page) = New List(Of Page)
    Public isOnline As Boolean

    Public Sub New(feedReciever As QuoteFixReciever)

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        Me.feedReciever = feedReciever
        ' Добавьте все инициализирующие действия после вызова InitializeComponent().

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ExanteIDTextBox0.Hide()
        DoubleBuffered = True
        Dim newPage = New Page(New ChartPainting, QuotesPctBox0, PricesQuotesPctBox0, TimesQuotesPctBox0, TradesPctBox0, PricesTradesPctBox0, TimesTradesPctBox0,
                LeftQuotesButton0, RightQuotesButton0, PlusQuotesButton0, MinusQuotesButton0, LeftTradesButton0, RightButtonTrades0, PlusTradesButton0, MinusTradesButton0, Charts0, VolumesTradesPctBox0, VolumesVolumesTradesPctBox0)
        pageList.Add(newPage)
        TicksOrSeconds.SelectedItem = "5 секунд"
        Try
            pageList(Tabs.SelectedIndex).cp.isSubscribed = True
            Tabs.TabPages(Tabs.SelectedIndex).Text = ExanteIDTextBox0.Text
            Dim subscribes = feedReciever.GetSubscribeInfos()
            feedReciever.SubscribeForQuotes(ExanteIDTextBox0.Text, AddressOf pageList(Tabs.SelectedIndex).OnMarketDataUpdate)
            pageList(Tabs.SelectedIndex).bufferTrades.StartWritingData(ExanteIDTextBox0.Text)
            pageList(Tabs.SelectedIndex).TabId = Tabs.SelectedIndex
        Catch ex As Exception
            MsgBox("Нет подключения")
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If feedReciever IsNot Nothing Then
            feedReciever.Logout()
        End If
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub QuotesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles QuotesPctBox0.MouseMove
        If (pageList.Count > 0) Then
            If (pageList(Tabs.SelectedIndex).cp.isSubscribed) Then
                Try
                    Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeQuotes - (e.Y / pageList(Tabs.SelectedIndex).QuotesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeQuotes
                    PriceLabel0.Text = Format((pageList(Tabs.SelectedIndex).cp.lowBorderQuotes) + proportion, "0.00")
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
            Case "5 секунд"
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec - 10
                If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = 0
                End If
                pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False
                Try
                    pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                Catch ex As Exception
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec -= 1
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec < 0) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = 0
                    End If
                End Try
            Case Else

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
            Case "5 секунд"
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                If (pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count > pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec + 10
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec + pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec > pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count - pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec
                    End If

                    If (Not pageList(Tabs.SelectedIndex).cp.lastPointTrades5sec = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count - 1) Then
                        Try
                            pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                        Catch ex As Exception
                            pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec -= 1
                            If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec < 0) Then
                                pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = 0
                            End If
                        End Try
                    Else
                        pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = True
                    End If
                End If
            Case Else

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
            Case "5 секунд"
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec += 15
                pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec -= 15
                If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec < 0) Then
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = 0
                End If
                If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec > pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenTrades5sec) Then
                    pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec = pageList(Tabs.SelectedIndex).cp.maxPointsOnScreenTrades5sec
                End If
                pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False
                If (pageList(Tabs.SelectedIndex).cp.lastPointTrades5sec < pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count - 1) Then
                    Try
                        pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                    Catch ex As Exception
                        pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec -= 1
                        If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec < 0) Then
                            pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = 0
                        End If
                    End Try
                Else
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec -= 7
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec < 0) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = 0
                    End If
                    Try
                        pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                    Catch ex As Exception
                        pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec -= 1
                        If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec < 0) Then
                            pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = 0
                        End If
                    End Try
                End If
            Case Else

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
            Case "5 секунд"
                pageList(Tabs.SelectedIndex).cp.needDrawLineTrades = False
                pageList(Tabs.SelectedIndex).cp.isLineReadyTrades = False
                pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec -= 15
                If (pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec < pageList(Tabs.SelectedIndex).cp.minPointsOnScreenTrades5sec) Then
                    pageList(Tabs.SelectedIndex).cp.pointsOnScreenTrades5sec = pageList(Tabs.SelectedIndex).cp.minPointsOnScreenTrades5sec
                End If
                pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False
                Try
                    pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                Catch ex As Exception
                    pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec -= 1
                    If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec < 0) Then
                        pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec = 0
                    End If
                End Try
            Case Else

        End Select

    End Sub

    Private Sub TradesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles TradesPctBox0.MouseMove
        If (Me.TicksOrSeconds.SelectedItem = "Тики") Then
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTrades = 0 And Not pageList(Tabs.SelectedIndex).cp.pointsTrades.Count = 0) Then
                    Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeTrades - (e.Y / pageList(Tabs.SelectedIndex).TradesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeTrades
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
        Else
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTrades5sec = 0 And Not pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count = 0) Then
                    Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeTrades5sec - (e.Y / pageList(Tabs.SelectedIndex).TradesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeTrades5sec
                    PriceLabel0.Text = Format((pageList(Tabs.SelectedIndex).cp.lowBorderTrades5sec) + proportion, "0.00")
                    Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(Tabs.SelectedIndex).cp.intervalTrades5sec))
                    If (indexOfPoint < 0) Then
                        indexOfPoint = 0
                    End If
                    If (indexOfPoint >= pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count) Then
                        indexOfPoint = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count - 1
                        If (indexOfPoint < 0) Then
                            indexOfPoint = 0
                        End If
                        TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(indexOfPoint).time.ToLongTimeString
                    Else
                        If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec + indexOfPoint > pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count) Then
                            TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(pageList(Tabs.SelectedIndex).cp.lastPointTrades5sec).time.ToLongTimeString
                        Else
                            TimeLabel0.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec + indexOfPoint).time.ToLongTimeString
                        End If
                    End If
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
                                pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                            End If

                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub VolumesTradesPctBox_MouseMove(sender As Object, e As MouseEventArgs) Handles VolumesTradesPctBox0.MouseMove
        If (TicksOrSeconds.SelectedItem = "Тики") Then
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTrades = 0) Then
                    Try
                        Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeVolumesTrades - (e.Y / pageList(Tabs.SelectedIndex).VolumesTradesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeVolumesTrades
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
        Else
            If (pageList.Count > 0) Then
                If (pageList(Tabs.SelectedIndex).cp.isSubscribed And Not pageList(Tabs.SelectedIndex).cp.intervalTrades5sec = 0) Then
                    Try
                        Dim proportion As Double = pageList(Tabs.SelectedIndex).cp.yRangeVolumesTrades5sec - (e.Y / pageList(Tabs.SelectedIndex).VolumesTradesPctBox.Height) * pageList(Tabs.SelectedIndex).cp.yRangeVolumesTrades5sec
                        VolumeLabel.Text = Format(proportion, "0.00")

                        Dim indexOfPoint = CInt(Math.Floor(e.X / pageList(Tabs.SelectedIndex).cp.intervalTrades5sec))
                        If (indexOfPoint < 0) Then
                            indexOfPoint = 0
                        End If
                        If (indexOfPoint >= pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count) Then
                            indexOfPoint = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count - 1
                            CurVolumeLabel.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(indexOfPoint).volumeBuy + pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(indexOfPoint).volumeSell
                        Else
                            If (pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec + indexOfPoint > pageList(Tabs.SelectedIndex).cp.pointsTrades5sec.Count) Then
                                CurVolumeLabel.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(pageList(Tabs.SelectedIndex).cp.lastPointTrades5sec).volumeBuy + pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(pageList(Tabs.SelectedIndex).cp.lastPointTrades5sec).volumeSell

                            Else
                                CurVolumeLabel.Text = pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec + indexOfPoint).volumeSell + pageList(Tabs.SelectedIndex).cp.pointsTrades5sec(pageList(Tabs.SelectedIndex).cp.currentPointTrades5sec + indexOfPoint).volumeBuy
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
        End If

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)
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

        Dim newPage = New Page(New ChartPainting, QuotesPctBox, PricesQuotesPctBox, TimesQuotesPctBox, TradesPctBox, PricesTradesPctBox, TimesTradesPctBox,
               LeftQuotesButton, RightQuotesButton, PlusQuotesButton, MinusQuotesButton, LeftTradesButton, RightTradesButton, PlusTradesButton, MinusTradesButton, Charts, VolumesTradesPctBox, VolumesVolumesTradesPctBox)
        pageList.Add(newPage)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TicksOrSeconds.SelectedIndexChanged
        If (pageList(Tabs.SelectedIndex).Chart.SelectedIndex = 1) Then
            If (TicksOrSeconds.SelectedItem = "Тики") Then
                If (pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False) Then
                    pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                Else
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = False
                    pageList(Tabs.SelectedIndex).cp.paintingTrades(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades = True
                End If
            Else
                If (pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False) Then
                    pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                Else
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False
                    pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = True
                End If
            End If
        End If
    End Sub

    Private Sub TypeOfGraphic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypeOfGraphic.SelectedIndexChanged
        If (pageList(Tabs.SelectedIndex).Chart.SelectedIndex = 1) Then
            If (TicksOrSeconds.SelectedItem = "5 секунд") Then
                If (pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False) Then
                    pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                Else
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False
                    pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = True
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
                If (pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False) Then
                    pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                Else
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = False
                    pageList(Tabs.SelectedIndex).cp.paintingTrades5sec(pageList(Tabs.SelectedIndex).TradesPctBox, pageList(Tabs.SelectedIndex).TimesTradesPctBox, pageList(Tabs.SelectedIndex).PricesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesTradesPctBox, pageList(Tabs.SelectedIndex).VolumesVolumesTradesPctBox)
                    pageList(Tabs.SelectedIndex).cp.needRePaintingTrades5sec = True
                End If
            End If
        End If
    End Sub
End Class
