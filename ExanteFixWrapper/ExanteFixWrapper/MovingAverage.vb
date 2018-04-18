Public Class MovingAverage
    Private windowSize As Integer
    Private windowPoints As List(Of Double)

    Public Sub New(windowSize As Integer)
        Me.windowSize = windowSize
    End Sub
    Public Function Calculate(currentValue As Double) As Double
        Dim sum As Double = 0.0
        If windowPoints Is Nothing Then
            windowPoints = New List(Of Double)
            For i = 0 To windowSize - 1
                windowPoints.Add(currentValue)
            Next
            sum = currentValue * windowSize
        Else
            For i = 0 To windowSize - 1
                If i < (windowSize - 1) Then
                    windowPoints(i) = windowPoints(i + 1)
                    sum += windowPoints(i)
                Else
                    windowPoints(i) = currentValue
                    sum += windowPoints(i)
                End If
            Next
        End If
        Return sum / windowSize
    End Function
End Class
