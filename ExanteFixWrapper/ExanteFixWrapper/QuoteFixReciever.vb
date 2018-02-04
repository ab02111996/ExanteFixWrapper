Imports QuickFix
Imports System.Threading
Imports ExanteFixWrapper.QuickFIXFeedApplication

Public Class QuoteFixReciever
    Dim initiator As SocketInitiator
    Dim settings As SessionSettings
    Dim application As QuickFIXFeedApplication
    Dim currentState As Boolean
    Delegate Sub UpdateConnectionStateCallBack(State As Boolean)
    Public updateConnStateCallback As UpdateConnectionStateCallBack
    Dim updateConnStatusThread As Thread
    Public Sub New(fixFeedConfigPath As String, updStateCallback As UpdateConnectionStateCallBack)
        currentState = False
        Try
            settings = New SessionSettings(fixFeedConfigPath)
            Dim sessions As ArrayList = settings.getSessions()
            Dim dict As QuickFix.Dictionary = settings.get(CType(sessions(0), QuickFix.SessionID))
            Dim fixFeedPass As String = dict.getString("password")
            Dim storeFactory As FileStoreFactory = New FileStoreFactory(settings)
            application = New QuickFIXFeedApplication(fixFeedPass)
            Dim logFactory As FileLogFactory = New FileLogFactory(settings)
            Dim messageFactory As MessageFactory = New DefaultMessageFactory()
            initiator = New SocketInitiator(application, storeFactory, settings, logFactory, messageFactory)
            initiator.start()
            updateConnStateCallback = updStateCallback
            If updateConnStateCallback <> Nothing Then
                updateConnStatusThread = New Thread(AddressOf CheckingConnectonState)
                updateConnStatusThread.Start()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub SubscribeForQuotes(exanteID As String, updateQuotesCallback As UpdateQuoteCourseCallBack)
        Dim marketDataRequest As QuickFix44.MarketDataRequest = New QuickFix44.MarketDataRequest()
        marketDataRequest.set(New MDReqID(System.Guid.NewGuid().ToString()))
        marketDataRequest.set(New SubscriptionRequestType(SubscriptionRequestType.SNAPSHOT_PLUS_UPDATES))
        marketDataRequest.set(New MarketDepth(0))
        marketDataRequest.set(New MDUpdateType(MDUpdateType.FULL_REFRESH))
        marketDataRequest.set(New AggregatedBook(True))
        Dim entryTypesGroup As QuickFix44.MarketDataRequest.NoMDEntryTypes = New QuickFix44.MarketDataRequest.NoMDEntryTypes()
        entryTypesGroup.set(New MDEntryType(MDEntryType.BID))
        marketDataRequest.addGroup(entryTypesGroup)
        entryTypesGroup.set(New MDEntryType(MDEntryType.OFFER))
        marketDataRequest.addGroup(entryTypesGroup)
        Dim relatedSymGroup As QuickFix44.MarketDataRequest.NoRelatedSym = New QuickFix44.MarketDataRequest.NoRelatedSym()
        relatedSymGroup.set(New SecurityIDSource("111"))
        relatedSymGroup.set(New SecurityID(exanteID))
        relatedSymGroup.set(New Symbol(exanteID))
        marketDataRequest.addGroup(relatedSymGroup)
        Session.sendToTarget(marketDataRequest, initiator.getSessions(0))
        application.SubscribeOnQuoteInfo(updateQuotesCallback)
    End Sub

    Sub CheckingConnectonState()
        While True
            Thread.Sleep(1000)
            If isConnected() <> currentState Then
                currentState = isConnected()
                updateConnStateCallback.Invoke(currentState)
            End If
        End While
    End Sub


    Public Function isConnected() As Boolean
        If (initiator Is Nothing) Then
            Return False
        End If
        Return initiator.isLoggedOn
    End Function
End Class
