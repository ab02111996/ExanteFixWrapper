Public Class QuotesInfo
    Public AskPrice As Double?
    Public AskVolume As Double?
    Public BidPrice As Double?
    Public BidVolume As Double?
    Public TradePrice As Double?
    Public TradeVolume As Double?
    Public TimeStamp As DateTime

    Sub New()
        Me.AskPrice = Nothing
        Me.AskVolume = Nothing
        Me.BidPrice = Nothing
        Me.BidVolume = Nothing
        Me.TradePrice = Nothing
        Me.TradeVolume = Nothing
    End Sub
End Class
