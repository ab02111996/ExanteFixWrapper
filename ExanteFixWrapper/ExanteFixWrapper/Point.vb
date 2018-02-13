Public Class Point
    Public askPrice As Double
    Public askVolume As Double
    Public bidPrice As Double
    Public bidVolume As Double
    Public time As DateTime
    Public tradeVolume As Double
    Public tradePrice As Double
    Public Sub New()

    End Sub

    Public Sub New(askPrice As Double, askVolume As Double, bidPrice As Double, bidVolume As Double, volume As Double, time As DateTime, tradeVolume As Double, tradePrice As Double)
        Me.askPrice = askPrice
        Me.askVolume = askVolume
        Me.bidPrice = bidPrice
        Me.bidVolume = bidVolume
        Me.time = time
        Me.tradeVolume = tradeVolume
        Me.tradePrice = tradePrice
    End Sub
End Class
