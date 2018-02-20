Imports ADOX
Imports ADODB
Imports System.IO

Public Class DataBaseWriter
    Private currentConnection As Connection
    Private currentCatalog As Catalog
    Private connectionString As String
    Private dbPath As String
    Private currentFileName As String
    Private currentFileCreationDate As DateTime

    Sub New()
              '"Jet OLEDB:Engine Type=5"
    End Sub
    Private Sub CreateDBFile()
        currentFileName = DateTime.Now.Ticks.ToString() + ".accdb"
        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                    "Data Source=" + dbPath + "\" + currentFileName + ";"
        Try
            currentCatalog = New Catalog()
            currentCatalog.Create(connectionString)
            currentConnection = New Connection()
            currentConnection.Open(connectionString)
            currentCatalog.ActiveConnection = currentConnection
            Dim fiveSecondsDataTable = New Table()
            Dim primaryKeyColumn = New Column()
            primaryKeyColumn.Name = "ID"
            primaryKeyColumn.Type = ADOX.DataTypeEnum.adInteger
            primaryKeyColumn.ParentCatalog = currentCatalog
            primaryKeyColumn.Properties("AutoIncrement").Value = True
            fiveSecondsDataTable.Name = "FiveSecondsDataTable"
            fiveSecondsDataTable.Columns.Append(primaryKeyColumn)
            fiveSecondsDataTable.Keys.Append("PrimaryKey", KeyTypeEnum.adKeyPrimary, "ID")
            fiveSecondsDataTable.Columns.Append("Open", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("High", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("Low", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("Close", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("VolumeSell", ADOX.DataTypeEnum.adInteger)
            fiveSecondsDataTable.Columns.Append("VolumeBuy", ADOX.DataTypeEnum.adInteger)
            fiveSecondsDataTable.Columns.Append("CountSell", ADOX.DataTypeEnum.adInteger)
            fiveSecondsDataTable.Columns.Append("CountBuy", ADOX.DataTypeEnum.adInteger)
            fiveSecondsDataTable.Columns.Append("PriceSell", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("PriceBuy", ADOX.DataTypeEnum.adDouble)
            currentCatalog.Tables.Append(fiveSecondsDataTable)
            fiveSecondsDataTable = Nothing
            currentCatalog = Nothing
        Catch ex As System.Runtime.InteropServices.COMException
            currentCatalog = Nothing
        End Try

    End Sub
    Sub SetDBPath(path As String)
        Me.dbPath = path
    End Sub
    Sub OpenConnection()
        Dim files = Directory.GetFiles(dbPath, "*.accdb")
        currentFileCreationDate = New DateTime(0)
        For Each item As String In files
            If Directory.GetCreationTime(item) > currentFileCreationDate Then
                currentFileCreationDate = Directory.GetCreationTime(item)
                currentFileName = Path.GetFileName(item)
            End If
        Next
        If (DateTime.Now - currentFileCreationDate).Days > 7 Then
            Me.CreateDBFile()
        Else
            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                   "Data Source=" + dbPath + "\" + currentFileName + ";"
            currentConnection = New Connection()
            currentConnection.Open(connectionString)
        End If
    End Sub
    Sub CloseConnection()
        currentConnection.Close()
        currentConnection = Nothing
    End Sub


End Class
