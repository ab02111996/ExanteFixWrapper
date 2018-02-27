Imports ADOX
Imports ADODB
Imports System.IO
Imports System.Text

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
        currentFileCreationDate = DateTime.Now
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
            fiveSecondsDataTable.Columns.Append("StartTime", ADOX.DataTypeEnum.adDate)
            fiveSecondsDataTable.Columns.Append("StartTimeMilliseconds", ADOX.DataTypeEnum.adInteger)
            fiveSecondsDataTable.Columns.Append("EndTime", ADOX.DataTypeEnum.adDate)
            fiveSecondsDataTable.Columns.Append("EndTimeMilliseconds", ADOX.DataTypeEnum.adInteger)
            fiveSecondsDataTable.Columns.Append("Instrument", ADOX.DataTypeEnum.adVarWChar)
            fiveSecondsDataTable.Columns.Append("Open", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("High", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("Low", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("Close", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("VolumeSell", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("VolumeBuy", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("CountSell", ADOX.DataTypeEnum.adInteger)
            fiveSecondsDataTable.Columns.Append("CountBuy", ADOX.DataTypeEnum.adInteger)
            fiveSecondsDataTable.Columns.Append("PriceSell", ADOX.DataTypeEnum.adDouble)
            fiveSecondsDataTable.Columns.Append("PriceBuy", ADOX.DataTypeEnum.adDouble)
            currentCatalog.Tables.Append(fiveSecondsDataTable)
            Dim metaDataTable = New Table()
            primaryKeyColumn = New Column()
            primaryKeyColumn.Name = "ID"
            primaryKeyColumn.Type = ADOX.DataTypeEnum.adInteger
            primaryKeyColumn.ParentCatalog = currentCatalog
            primaryKeyColumn.Properties("AutoIncrement").Value = True
            metaDataTable.Name = "MetaDataTable"
            metaDataTable.Columns.Append(primaryKeyColumn)
            metaDataTable.Keys.Append("PrimaryKey", KeyTypeEnum.adKeyPrimary, "ID")
            metaDataTable.Columns.Append("Time", ADOX.DataTypeEnum.adDate)
            metaDataTable.Columns.Append("Milliseconds", ADOX.DataTypeEnum.adInteger)
            metaDataTable.Columns.Append("LocalTime", ADOX.DataTypeEnum.adDate)
            metaDataTable.Columns.Append("LocalTimeMilliseconds", ADOX.DataTypeEnum.adInteger)
            metaDataTable.Columns.Append("Message", ADOX.DataTypeEnum.adVarWChar)
            metaDataTable.Columns.Append("Instrument", ADOX.DataTypeEnum.adVarWChar)
            metaDataTable.Columns.Append("Direction", ADOX.DataTypeEnum.adVarWChar)
            metaDataTable.Columns.Append("Price", ADOX.DataTypeEnum.adDouble)
            metaDataTable.Columns.Append("Volume", ADOX.DataTypeEnum.adDouble)
            currentCatalog.Tables.Append(metaDataTable)
            primaryKeyColumn = Nothing
            fiveSecondsDataTable = Nothing
            metaDataTable = Nothing
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
                currentFileName = Path.GetFileName(item)
                currentFileCreationDate = Directory.GetCreationTime(item)

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
    Sub InsertBufferIntoDB(buffer As Buffer)
        If (DateTime.Now - currentFileCreationDate).Days > 7 Then
            Me.OpenConnection()
        Else
            Dim metaData = buffer.GetBufferMetaData()
            If metaData IsNot Nothing Then
                Dim sb = New StringBuilder()
                sb.Append("INSERT INTO FiveSecondsDataTable([StartTime], [StartTimeMilliseconds], [EndTime], [EndTimeMilliseconds], " +
                          "[Instrument], [Open], [High], [Low], [Close], [VolumeSell], [VolumeBuy], [CountSell], [CountBuy], [PriceSell], " +
                          "[PriceBuy]) VALUES")
                sb.Append("(")
                sb.Append("'" + buffer.startTimeFrame + "',")
                sb.Append(buffer.startTimeFrame.Millisecond.ToString() + ",")
                sb.Append("'" + buffer.endTimeFrame + "',")
                sb.Append(buffer.endTimeFrame.Millisecond.ToString() + ",")
                sb.Append("'" + buffer.exanteID + "',")
                sb.Append(buffer.openPrice.ToString().Replace(",", ".") + ",")
                sb.Append(buffer.highPrice.ToString().Replace(",", ".") + ",")
                sb.Append(buffer.lowPrice.ToString().Replace(",", ".") + ",")
                sb.Append(buffer.closePrice.ToString().Replace(",", ".") + ",")
                sb.Append(buffer.volumeSell.ToString().Replace(",", ".") + ",")
                sb.Append(buffer.volumeBuy.ToString().Replace(",", ".") + ",")
                sb.Append(buffer.countSell.ToString() + ",")
                sb.Append(buffer.countBuy.ToString() + ",")
                sb.Append(buffer.priceSell.ToString().Replace(",", ".") + ",")
                sb.Append(buffer.priceBuy.ToString().Replace(",", "."))
                sb.Append(");")
                currentConnection.Execute(sb.ToString())
            End If
        End If
    End Sub
    Sub InsertBufferMetaDataIntoDB(buffer As Buffer)
        If (DateTime.Now - currentFileCreationDate).Days > 7 Then
            Me.OpenConnection()
        Else
            Dim metaData = buffer.GetBufferMetaData()
            If metaData IsNot Nothing Then
                For Each qi As QuotesInfo In metaData
                    Dim sb As StringBuilder = New StringBuilder()
                    sb.Append("INSERT INTO MetaDataTable([Time], [Milliseconds], [LocalTime], [LocalTimeMilliseconds], [Message], [Instrument], [Direction], [Price], [Volume]) VALUES")
                    sb.Append("(")
                    sb.Append("'" + qi.TimeStamp + "',")
                    sb.Append(qi.TimeStamp.Millisecond.ToString() + ",")
                    sb.Append("'" + qi.LocalTimeStamp + "',")
                    sb.Append(qi.LocalTimeStamp.Millisecond.ToString() + ",")
                    sb.Append("'" + qi.Message + "',")
                    sb.Append("'" + qi.ExanteId + "',")
                    sb.Append("'" + qi.Direction.ToString() + "',")
                    sb.Append(qi.TradePrice.ToString().Replace(",", ".") + ",")
                    sb.Append(qi.TradeVolume.ToString().Replace(",", "."))
                    sb.Append(")")
                    currentConnection.Execute(sb.ToString())
                Next
            End If
        End If
    End Sub
    Sub CloseConnection()
        currentConnection.Close()
        currentConnection = Nothing
    End Sub
End Class
