<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ExanteIDTextBox = New System.Windows.Forms.TextBox()
        Me.BidPriceTextBox = New System.Windows.Forms.TextBox()
        Me.LabelBid = New System.Windows.Forms.Label()
        Me.LabelAsk = New System.Windows.Forms.Label()
        Me.AskPriceTextBox = New System.Windows.Forms.TextBox()
        Me.AskVolumeTextBox = New System.Windows.Forms.TextBox()
        Me.BidVolumeTextBox = New System.Windows.Forms.TextBox()
        Me.PriceLabel = New System.Windows.Forms.Label()
        Me.VolumeLabel = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(66, 59)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 28)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Соединиться"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(62, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Нет соединения"
        '
        'ExanteIDTextBox
        '
        Me.ExanteIDTextBox.Location = New System.Drawing.Point(399, 27)
        Me.ExanteIDTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ExanteIDTextBox.Name = "ExanteIDTextBox"
        Me.ExanteIDTextBox.Size = New System.Drawing.Size(291, 22)
        Me.ExanteIDTextBox.TabIndex = 2
        Me.ExanteIDTextBox.Text = "BTC.EXANTE"
        '
        'BidPriceTextBox
        '
        Me.BidPriceTextBox.Location = New System.Drawing.Point(934, 77)
        Me.BidPriceTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.BidPriceTextBox.Name = "BidPriceTextBox"
        Me.BidPriceTextBox.Size = New System.Drawing.Size(132, 22)
        Me.BidPriceTextBox.TabIndex = 3
        '
        'LabelBid
        '
        Me.LabelBid.AutoSize = True
        Me.LabelBid.Location = New System.Drawing.Point(878, 81)
        Me.LabelBid.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBid.Name = "LabelBid"
        Me.LabelBid.Size = New System.Drawing.Size(28, 17)
        Me.LabelBid.TabIndex = 4
        Me.LabelBid.Text = "Bid"
        '
        'LabelAsk
        '
        Me.LabelAsk.AutoSize = True
        Me.LabelAsk.Location = New System.Drawing.Point(878, 30)
        Me.LabelAsk.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelAsk.Name = "LabelAsk"
        Me.LabelAsk.Size = New System.Drawing.Size(31, 17)
        Me.LabelAsk.TabIndex = 5
        Me.LabelAsk.Text = "Ask"
        '
        'AskPriceTextBox
        '
        Me.AskPriceTextBox.Location = New System.Drawing.Point(934, 30)
        Me.AskPriceTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AskPriceTextBox.Name = "AskPriceTextBox"
        Me.AskPriceTextBox.Size = New System.Drawing.Size(132, 22)
        Me.AskPriceTextBox.TabIndex = 6
        '
        'AskVolumeTextBox
        '
        Me.AskVolumeTextBox.Location = New System.Drawing.Point(1092, 30)
        Me.AskVolumeTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AskVolumeTextBox.Name = "AskVolumeTextBox"
        Me.AskVolumeTextBox.Size = New System.Drawing.Size(132, 22)
        Me.AskVolumeTextBox.TabIndex = 8
        '
        'BidVolumeTextBox
        '
        Me.BidVolumeTextBox.Location = New System.Drawing.Point(1092, 77)
        Me.BidVolumeTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.BidVolumeTextBox.Name = "BidVolumeTextBox"
        Me.BidVolumeTextBox.Size = New System.Drawing.Size(132, 22)
        Me.BidVolumeTextBox.TabIndex = 7
        '
        'PriceLabel
        '
        Me.PriceLabel.AutoSize = True
        Me.PriceLabel.Location = New System.Drawing.Point(977, 9)
        Me.PriceLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PriceLabel.Name = "PriceLabel"
        Me.PriceLabel.Size = New System.Drawing.Size(40, 17)
        Me.PriceLabel.TabIndex = 9
        Me.PriceLabel.Text = "Price"
        '
        'VolumeLabel
        '
        Me.VolumeLabel.AutoSize = True
        Me.VolumeLabel.Location = New System.Drawing.Point(1133, 9)
        Me.VolumeLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.VolumeLabel.Name = "VolumeLabel"
        Me.VolumeLabel.Size = New System.Drawing.Size(55, 17)
        Me.VolumeLabel.TabIndex = 10
        Me.VolumeLabel.Text = "Volume"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(711, 23)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(116, 28)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Подписаться"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Chart1
        '
        Me.Chart1.BorderlineColor = System.Drawing.Color.Black
        Me.Chart1.BorderSkin.BorderWidth = 4
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.Location = New System.Drawing.Point(52, 120)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Me.Chart1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Name = "AskPrice"
        Series1.YValuesPerPoint = 4
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series2.Name = "BidPrice"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Series.Add(Series2)
        Me.Chart1.Size = New System.Drawing.Size(1275, 475)
        Me.Chart1.TabIndex = 12
        Me.Chart1.Text = "Chart1"
        '
        'Chart2
        '
        ChartArea2.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea2)
        Me.Chart2.Location = New System.Drawing.Point(100, 601)
        Me.Chart2.Name = "Chart2"
        Me.Chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Series3.ChartArea = "ChartArea1"
        Series3.Name = "AskVolume"
        Series4.ChartArea = "ChartArea1"
        Series4.Name = "BidVolume"
        Me.Chart2.Series.Add(Series3)
        Me.Chart2.Series.Add(Series4)
        Me.Chart2.Size = New System.Drawing.Size(1227, 173)
        Me.Chart2.TabIndex = 13
        Me.Chart2.Text = "Chart2"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1401, 786)
        Me.Controls.Add(Me.Chart2)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.VolumeLabel)
        Me.Controls.Add(Me.PriceLabel)
        Me.Controls.Add(Me.AskVolumeTextBox)
        Me.Controls.Add(Me.BidVolumeTextBox)
        Me.Controls.Add(Me.AskPriceTextBox)
        Me.Controls.Add(Me.LabelAsk)
        Me.Controls.Add(Me.LabelBid)
        Me.Controls.Add(Me.BidPriceTextBox)
        Me.Controls.Add(Me.ExanteIDTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ExanteIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents BidPriceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LabelBid As System.Windows.Forms.Label
    Friend WithEvents LabelAsk As System.Windows.Forms.Label
    Friend WithEvents AskPriceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AskVolumeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents BidVolumeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PriceLabel As System.Windows.Forms.Label
    Friend WithEvents VolumeLabel As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents Chart2 As DataVisualization.Charting.Chart
End Class
