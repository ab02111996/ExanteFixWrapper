Imports System.IO
Imports ADODB

Public Class DataBaseReader
    Private dbPath As String
    Private currentFileName As String
    Private connectionString As String
    Private currentConnection As Connection

    Public Sub New(dbPath As String)
        Me.dbPath = dbPath
    End Sub
    Public Function GetListOfTrades5secPoints(instrumentName As String) As Tuple(Of List(Of PointTradesNsec), Double)
        Dim files = Directory.GetFiles(dbPath, instrumentName.Replace("/", "_" + "_") + "_*.accdb")
        Dim lastFileCreationDate = New DateTime(0)
        For Each item As String In files
            If Directory.GetCreationTime(item) > lastFileCreationDate Then
                currentFileName = Path.GetFileName(item)
                lastFileCreationDate = Directory.GetCreationTime(item)
            End If
        Next
        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                   "Data Source=" + dbPath + "\" + currentFileName + ";"
        currentConnection = New Connection()
        currentConnection.Open(connectionString)
        Dim recordSet As ADODB.Recordset = New ADODB.Recordset
        recordSet.ActiveConnection = currentConnection
        recordSet.Open("FiveSecondsDataTable")
        Dim list5sec = New List(Of PointTradesNsec)
        Dim maxVolume As Double = -1
        While Not recordSet.EOF
            Dim point As New PointTradesNsec
            point.time = recordSet.Fields.Item(3).Value
            point.time.AddMilliseconds(recordSet.Fields.Item(4).Value)
            point.openPrice = recordSet.Fields.Item(6).Value
            point.closePrice = recordSet.Fields.Item(9).Value
            point.highPrice = recordSet.Fields.Item(7).Value
            point.lowPrice = recordSet.Fields.Item(8).Value
            point.volumeSell = recordSet.Fields.Item(10).Value
            point.volumeBuy = recordSet.Fields.Item(11).Value
            If ((point.volumeBuy + point.volumeSell) > maxVolume) Then
                maxVolume = point.volumeBuy + point.volumeSell
            End If
            list5sec.Add(point)
            recordSet.MoveNext()
        End While
        Dim tuple As New Tuple(Of List(Of PointTradesNsec), Double)(list5sec, maxVolume)
        Return tuple
    End Function
End Class
