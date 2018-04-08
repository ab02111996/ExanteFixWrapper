Public Class PointTrades5sec
    Public openPrice As Double
    Public closePrice As Double
    Public highPrice As Double
    Public lowPrice As Double
    Public volumeSell As Double
    Public volumeBuy As Double
    Public time As DateTime

    Public Sub New(buffer As Buffer)
        openPrice = buffer.openPrice
        closePrice = buffer.closePrice
        highPrice = buffer.highPrice
        lowPrice = buffer.lowPrice
        volumeBuy = buffer.volumeBuy
        volumeSell = buffer.volumeSell
        time = buffer.endTimeFrame
    End Sub
End Class
