Imports System.Threading
Imports QuickFix
Imports QuickFix44

Public Class OrderFixExecutor
    Dim initiator As SocketInitiator
    Dim settings As SessionSettings
    Dim application As QuickFIXBrokerApplication
    Dim activeOrders As Dictionary(Of String, OrderInfo)
    Dim currentState As Boolean
    Delegate Sub UpdateConnectionStateCallBack(State As Boolean)
    Delegate Sub OrderStatusCallbackFIX(order As OrderInfo)

    Public updateConnStateCallback As UpdateConnectionStateCallBack
    Public updateOrderStateCallback As OrderStatusCallbackFIX
    Dim updateConnStatusThread As Thread
    Public Sub New(fixFeedConfigPath As String, updStateCallback As UpdateConnectionStateCallBack, updateOrderStateCallback As OrderStatusCallbackFIX)
        currentState = False
        Try
            settings = New SessionSettings(fixFeedConfigPath)
            Dim sessions As ArrayList = settings.getSessions()
            Dim dict As QuickFix.Dictionary = settings.get(CType(sessions(0), QuickFix.SessionID))
            Dim fixBrokerPass As String = dict.getString("password")
            Dim storeFactory As FileStoreFactory = New FileStoreFactory(settings)
            application = New QuickFIXBrokerApplication(fixBrokerPass, AddressOf UpdateOrder)
            Dim logFactory As FileLogFactory = New FileLogFactory(settings)
            Dim messageFactory As QuickFix.MessageFactory = New DefaultMessageFactory()
            initiator = New SocketInitiator(application, storeFactory, settings, logFactory, messageFactory)
            initiator.start()
            updateConnStateCallback = updStateCallback
            If updateConnStateCallback <> Nothing Then
                updateConnStatusThread = New Thread(AddressOf CheckingConnectonState)
                updateConnStatusThread.Start()
            End If
            Me.updateOrderStateCallback = updateOrderStateCallback
            activeOrders = New Dictionary(Of String, OrderInfo)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub PlaceOrder(order As OrderInfo)
        activeOrders(order.ClientOrderID) = order
        Dim clientOrderID As ClOrdID = New ClOrdID(order.ClientOrderID)
        Dim orderSide As Side = If(order.Side = OrderInfo.OrderSide.BUY, New Side(Side.BUY), New Side(Side.SELL))
        Dim placeOrder As NewOrderSingle = New NewOrderSingle(clientOrderID, orderSide, New TransactTime(order.OrderDateTime), New OrdType(OrdType.MARKET))
        placeOrder.set(New TimeInForce(TimeInForce.GOOD_TILL_CANCEL))
        placeOrder.set(New OrderQty(order.OrderQuantity))
        placeOrder.set(New Symbol(order.Instrument))
        placeOrder.set(New SecurityID(order.Instrument))
        placeOrder.set(New SecurityIDSource("111"))
        QuickFix.Session.sendToTarget(placeOrder, initiator.getSessions(0))
        updateOrderStateCallback.Invoke(order)
    End Sub
    Public Sub GetTrades()
        Dim request As TradeCaptureReportRequest = New TradeCaptureReportRequest(New TradeRequestID(System.Guid.NewGuid().ToString()), New TradeRequestType(0))
        Dim tDate = Date.Now.AddDays(-7).Date.ToString("yyyyMMdd")
        Dim nDates = New QuickFix44.TradeCaptureReportRequest.NoDates()
        nDates.set(New TradeDate(tDate))
        request.addGroup(nDates)
        QuickFix.Session.sendToTarget(request, initiator.getSessions(0))
    End Sub

    Public Sub Logout()
        Try
            updateConnStatusThread.Abort()
            updateConnStatusThread.Join()
            updateConnStatusThread = Nothing
        Catch ex As Exception
            Console.WriteLine("Logout errors")
        End Try
    End Sub
    Public Sub CheckingConnectonState()
        While True
            Thread.Sleep(1000)
            If isConnected() <> currentState Then
                currentState = isConnected()
                If updateConnStatusThread IsNot Nothing Then
                    updateConnStateCallback.Invoke(currentState)
                Else
                    Exit While
                End If
            End If
        End While
    End Sub
    Public Function isConnected() As Boolean
        If (initiator Is Nothing) Then
            Return False
        End If
        Return initiator.isLoggedOn
    End Function
    Private Sub UpdateOrder(message As QuickFix.Message)
        Dim orderStatus As OrdStatus = New OrdStatus
        Dim parsedOrderStatus As OrderInfo.OrderStatus
        message.getField(orderStatus)
        Select Case orderStatus.getValue()
            Case "0"
                parsedOrderStatus = OrderInfo.OrderStatus.NEWORDER
            Case "1"
                parsedOrderStatus = OrderInfo.OrderStatus.PARTIALLY_FILLED
            Case "2"
                parsedOrderStatus = OrderInfo.OrderStatus.FILLED
            Case "4"
                parsedOrderStatus = OrderInfo.OrderStatus.CANCELED
            Case "6"
                parsedOrderStatus = OrderInfo.OrderStatus.PENDING_CANCEL
            Case "8"
                parsedOrderStatus = OrderInfo.OrderStatus.REJECTED
            Case "A"
                parsedOrderStatus = OrderInfo.OrderStatus.PENDING_NEW
        End Select
        Dim ClientOrderID As ClOrdID = New ClOrdID()
        message.getField(ClientOrderID)
        If activeOrders.ContainsKey(ClientOrderID.getValue()) Then
            activeOrders(ClientOrderID.getValue()).Status = parsedOrderStatus
        End If
        updateOrderStateCallback.Invoke(activeOrders(ClientOrderID.getValue()))
    End Sub
End Class
