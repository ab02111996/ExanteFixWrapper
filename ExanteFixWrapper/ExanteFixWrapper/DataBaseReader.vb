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
    Public Function GetListOfTrades5secPoints(instrumentName As String) As List(Of PointTrades5sec)
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
        Dim list5sec = New List(Of PointTrades5sec)
        Dim point As New PointTrades5sec
        While Not recordSet.EOF
            point.time = recordSet.Fields.Item(3).Value
            point.time.AddMilliseconds(recordSet.Fields.Item(4).Value)
            point.openPrice = recordSet.Fields.Item(6).Value
            point.closePrice = recordSet.Fields.Item(9).Value
            point.highPrice = recordSet.Fields.Item(7).Value
            point.lowPrice = recordSet.Fields.Item(8).Value
            point.volumeSell = recordSet.Fields.Item(10).Value
            point.volumeBuy = recordSet.Fields.Item(11).Value
            list5sec.Add(point)
            recordSet.MoveNext()
        End While

        Return list5sec
    End Function
End Class
