Public Class PointTradesNsec
    Public openPrice As Double
    Public closePrice As Double
    Public highPrice As Double
    Public lowPrice As Double
    Public volumeSell As Double
    Public volumeBuy As Double
    Public time As DateTime
    Public avgBuy As Double
    Public avgSell As Double
    Public avgBuyPlusSell As Double
    Public Sub New()

    End Sub
    Public Sub New(buffer As Buffer)
        openPrice = buffer.openPrice
        closePrice = buffer.closePrice
        highPrice = buffer.highPrice
        lowPrice = buffer.lowPrice
        volumeBuy = buffer.volumeBuy
        volumeSell = buffer.volumeSell
        time = buffer.startTimeFrame
        avgBuy = Nothing
        avgSell = Nothing
        avgBuyPlusSell = Nothing
    End Sub
End Class
