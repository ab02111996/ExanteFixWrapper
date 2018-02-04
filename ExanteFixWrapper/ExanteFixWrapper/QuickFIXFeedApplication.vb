Imports QuickFix
Imports QuickFix44


Public Class QuickFIXFeedApplication
    Implements QuickFix.Application
    Public Delegate Sub UpdateQuoteCourseCallBack(askPrice As Double, askVolume As Double, bidPrice As Double, bidVolume As Double, timeStamp As DateTime)
    Dim updateQuotesInfoCallback As UpdateQuoteCourseCallBack
    Private sessionid As QuickFix.SessionID
    Private fixPassword As String

    Sub New(password As String)
        Me.fixPassword = password
    End Sub
    Protected Overrides Sub Finalize()
        Try
            Session.lookupSession(sessionid).logout()
        Catch ex As Exception

        Finally
            MyBase.Finalize()
        End Try
    End Sub
    Public Sub fromAdmin(message As QuickFix.Message, sessionID As SessionID) Implements Application.fromAdmin

    End Sub

    Public Sub fromApp(message As QuickFix.Message, sessionID As SessionID) Implements Application.fromApp
        Dim msgType As MsgType = New MsgType()
        Dim marketSnapshotDataMessage As MarketDataSnapshotFullRefresh = CType(message, MarketDataSnapshotFullRefresh)
        marketSnapshotDataMessage.getHeader().getField(msgType)
        If msgType.getValue() = msgType.MarketDataSnapshotFullRefresh Then
            Try
                Dim requestId As String = marketSnapshotDataMessage.getMDReqID().ToString()
                Dim noMDEntries As NoMDEntries = marketSnapshotDataMessage.getNoMDEntries()
                Dim mdEntriesGroup As QuickFix44.MarketDataSnapshotFullRefresh.NoMDEntries = New QuickFix44.MarketDataSnapshotFullRefresh.NoMDEntries()
                Dim mdEntryType As MDEntryType = New MDEntryType()
                Dim mdEntryPx As MDEntryPx = New MDEntryPx()
                Dim mDEntrySize As MDEntrySize = New MDEntrySize()
                Dim askPrice, askVolume, bidPrice, bidVolume As Double
                For index As UInteger = 1 To noMDEntries.getValue()
                    message.getGroup(index, mdEntriesGroup)
                    mdEntriesGroup.get(mdEntryType)
                    mdEntriesGroup.get(mdEntryPx)
                    mdEntriesGroup.get(mDEntrySize)
                    Select Case mdEntryType.getValue()
                        Case mdEntryType.BID
                            bidPrice = mdEntryPx.getValue()
                            bidVolume = mDEntrySize.getValue()
                        Case mdEntryType.OFFER
                            askPrice = mdEntryPx.getValue()
                            askVolume = mDEntrySize.getValue()
                        Case Else
                    End Select
                Next
                updateQuotesInfoCallback.Invoke(askPrice, askVolume, bidPrice, bidVolume, DateTime.Now)
            Catch ex As Exception

            End Try
        End If

    End Sub

    Public Sub onCreate(sessionID As SessionID) Implements Application.onCreate

    End Sub

    Public Sub onLogon(sessionID As SessionID) Implements Application.onLogon
        Me.sessionid = sessionID
    End Sub

    Public Sub onLogout(sessionID As SessionID) Implements Application.onLogout

    End Sub

    Public Sub toAdmin(message As QuickFix.Message, sessionID As SessionID) Implements Application.toAdmin
        Dim msgType As MsgType = New MsgType()
        message.getHeader().getField(msgType)
        If msgType.getValue = QuickFix.MsgType.Logon Then

            message.setField(New Password(Me.fixPassword))
        End If
    End Sub

    Public Sub toApp(message As QuickFix.Message, sessionID As SessionID) Implements Application.toApp

    End Sub
    Public Sub SubscribeOnQuoteInfo(updatequotesinfocallback As UpdateQuoteCourseCallBack)
        Me.updateQuotesInfoCallback = updatequotesinfocallback
    End Sub
End Class
