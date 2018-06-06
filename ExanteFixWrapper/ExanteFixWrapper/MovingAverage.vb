Public Class MovingAverage
    Private windowSize As Integer
    Private windowPoints As List(Of Double)

    Public Sub New(windowSize As Integer)
        Me.windowSize = windowSize
    End Sub
    Public Sub Reinitialize()
        If windowPoints IsNot Nothing Then
            windowPoints.Clear()
        End If
    End Sub
    Public Function Calculate(currentValue As Double) As Double
        Dim sum As Double = 0.0
        'При первом использовании инициализируем список предыдущих значений текущим
        If windowPoints Is Nothing Then
            windowPoints = New List(Of Double)
            windowPoints.Add(currentValue)
            sum = currentValue
        Else
            'Далее все предыдущие значения сдвигаются в конец, а в начало списка добавляется текущее значение
            If windowPoints.Count >= windowSize Then
                For i = 0 To windowSize - 1
                    If i < (windowSize - 1) Then
                        windowPoints(i) = windowPoints(i + 1)
                        sum += windowPoints(i)
                    Else
                        windowPoints(i) = currentValue
                        sum += windowPoints(i)
                    End If
                Next
            Else
                windowPoints.Add(currentValue)
                sum += currentValue
            End If
        End If
        Return sum / windowSize
    End Function
End Class
