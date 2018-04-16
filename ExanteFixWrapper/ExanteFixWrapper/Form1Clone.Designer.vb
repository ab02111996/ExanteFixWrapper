<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1Clone
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
        Me.DrawLineTrades0 = New System.Windows.Forms.Button()
        Me.DrawLineQuotes0 = New System.Windows.Forms.Button()
        Me.TimeLabel0 = New System.Windows.Forms.Label()
        Me.PriceLabel0 = New System.Windows.Forms.Label()
        Me.VolumeLabel = New System.Windows.Forms.Label()
        Me.CurVolumeLabel = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.AskPriceLabel = New System.Windows.Forms.Label()
        Me.BidPriceLabel = New System.Windows.Forms.Label()
        Me.TradeVolumeLabel = New System.Windows.Forms.Label()
        Me.TradePriceLabel = New System.Windows.Forms.Label()
        Me.TabPage0 = New System.Windows.Forms.TabPage()
        Me.Charts0 = New System.Windows.Forms.TabControl()
        Me.QuotesTab0 = New System.Windows.Forms.TabPage()
        Me.PricesQuotesPctBox0 = New System.Windows.Forms.PictureBox()
        Me.MinusQuotesButton0 = New System.Windows.Forms.Button()
        Me.QuotesPctBox0 = New System.Windows.Forms.PictureBox()
        Me.PlusQuotesButton0 = New System.Windows.Forms.Button()
        Me.RightQuotesButton0 = New System.Windows.Forms.Button()
        Me.LeftQuotesButton0 = New System.Windows.Forms.Button()
        Me.TimesQuotesPctBox0 = New System.Windows.Forms.PictureBox()
        Me.TradesTab0 = New System.Windows.Forms.TabPage()
        Me.VolumesVolumesTradesPctBox0 = New System.Windows.Forms.PictureBox()
        Me.VolumesTradesPctBox0 = New System.Windows.Forms.PictureBox()
        Me.PricesTradesPctBox0 = New System.Windows.Forms.PictureBox()
        Me.MinusTradesButton0 = New System.Windows.Forms.Button()
        Me.TradesPctBox0 = New System.Windows.Forms.PictureBox()
        Me.PlusTradesButton0 = New System.Windows.Forms.Button()
        Me.RightButtonTrades0 = New System.Windows.Forms.Button()
        Me.LeftTradesButton0 = New System.Windows.Forms.Button()
        Me.TimesTradesPctBox0 = New System.Windows.Forms.PictureBox()
        Me.Tabs = New System.Windows.Forms.TabControl()
        Me.TicksOrSeconds = New System.Windows.Forms.ComboBox()
        Me.TypeOfGraphic = New System.Windows.Forms.ComboBox()
        Me.BuyAndSell = New System.Windows.Forms.RadioButton()
        Me.Sell = New System.Windows.Forms.RadioButton()
        Me.Buy = New System.Windows.Forms.RadioButton()
        Me.TabPage0.SuspendLayout()
        Me.Charts0.SuspendLayout()
        Me.QuotesTab0.SuspendLayout()
        CType(Me.PricesQuotesPctBox0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QuotesPctBox0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimesQuotesPctBox0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TradesTab0.SuspendLayout()
        CType(Me.VolumesVolumesTradesPctBox0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VolumesTradesPctBox0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PricesTradesPctBox0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TradesPctBox0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimesTradesPctBox0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tabs.SuspendLayout()
        Me.SuspendLayout()
        '
        'DrawLineTrades0
        '
        Me.DrawLineTrades0.Location = New System.Drawing.Point(1440, 7)
        Me.DrawLineTrades0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DrawLineTrades0.Name = "DrawLineTrades0"
        Me.DrawLineTrades0.Size = New System.Drawing.Size(212, 30)
        Me.DrawLineTrades0.TabIndex = 32
        Me.DrawLineTrades0.Text = "Рисовать линию (Сделки)"
        Me.DrawLineTrades0.UseVisualStyleBackColor = True
        '
        'DrawLineQuotes0
        '
        Me.DrawLineQuotes0.Location = New System.Drawing.Point(1221, 7)
        Me.DrawLineQuotes0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DrawLineQuotes0.Name = "DrawLineQuotes0"
        Me.DrawLineQuotes0.Size = New System.Drawing.Size(212, 30)
        Me.DrawLineQuotes0.TabIndex = 31
        Me.DrawLineQuotes0.Text = "Рисовать линию (Аск / Бид)"
        Me.DrawLineQuotes0.UseVisualStyleBackColor = True
        '
        'TimeLabel0
        '
        Me.TimeLabel0.AutoSize = True
        Me.TimeLabel0.Location = New System.Drawing.Point(947, 31)
        Me.TimeLabel0.Name = "TimeLabel0"
        Me.TimeLabel0.Size = New System.Drawing.Size(13, 17)
        Me.TimeLabel0.TabIndex = 25
        Me.TimeLabel0.Text = "-"
        '
        'PriceLabel0
        '
        Me.PriceLabel0.AutoSize = True
        Me.PriceLabel0.Location = New System.Drawing.Point(867, 31)
        Me.PriceLabel0.Name = "PriceLabel0"
        Me.PriceLabel0.Size = New System.Drawing.Size(13, 17)
        Me.PriceLabel0.TabIndex = 24
        Me.PriceLabel0.Text = "-"
        '
        'VolumeLabel
        '
        Me.VolumeLabel.AutoSize = True
        Me.VolumeLabel.Location = New System.Drawing.Point(1019, 31)
        Me.VolumeLabel.Name = "VolumeLabel"
        Me.VolumeLabel.Size = New System.Drawing.Size(13, 17)
        Me.VolumeLabel.TabIndex = 33
        Me.VolumeLabel.Text = "-"
        '
        'CurVolumeLabel
        '
        Me.CurVolumeLabel.AutoSize = True
        Me.CurVolumeLabel.Location = New System.Drawing.Point(1104, 31)
        Me.CurVolumeLabel.Name = "CurVolumeLabel"
        Me.CurVolumeLabel.Size = New System.Drawing.Size(13, 17)
        Me.CurVolumeLabel.TabIndex = 34
        Me.CurVolumeLabel.Text = "-"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1104, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 17)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Cur Volume"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1019, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 17)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Volume"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(947, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 17)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Time"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(867, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 17)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "Price"
        '
        'AskPriceLabel
        '
        Me.AskPriceLabel.AutoSize = True
        Me.AskPriceLabel.ForeColor = System.Drawing.Color.Red
        Me.AskPriceLabel.Location = New System.Drawing.Point(1232, 39)
        Me.AskPriceLabel.Name = "AskPriceLabel"
        Me.AskPriceLabel.Size = New System.Drawing.Size(13, 17)
        Me.AskPriceLabel.TabIndex = 39
        Me.AskPriceLabel.Text = "-"
        '
        'BidPriceLabel
        '
        Me.BidPriceLabel.AutoSize = True
        Me.BidPriceLabel.ForeColor = System.Drawing.Color.Blue
        Me.BidPriceLabel.Location = New System.Drawing.Point(1341, 39)
        Me.BidPriceLabel.Name = "BidPriceLabel"
        Me.BidPriceLabel.Size = New System.Drawing.Size(13, 17)
        Me.BidPriceLabel.TabIndex = 40
        Me.BidPriceLabel.Text = "-"
        '
        'TradeVolumeLabel
        '
        Me.TradeVolumeLabel.AutoSize = True
        Me.TradeVolumeLabel.ForeColor = System.Drawing.Color.Green
        Me.TradeVolumeLabel.Location = New System.Drawing.Point(1541, 39)
        Me.TradeVolumeLabel.Name = "TradeVolumeLabel"
        Me.TradeVolumeLabel.Size = New System.Drawing.Size(13, 17)
        Me.TradeVolumeLabel.TabIndex = 42
        Me.TradeVolumeLabel.Text = "-"
        '
        'TradePriceLabel
        '
        Me.TradePriceLabel.AutoSize = True
        Me.TradePriceLabel.ForeColor = System.Drawing.Color.Red
        Me.TradePriceLabel.Location = New System.Drawing.Point(1448, 39)
        Me.TradePriceLabel.Name = "TradePriceLabel"
        Me.TradePriceLabel.Size = New System.Drawing.Size(13, 17)
        Me.TradePriceLabel.TabIndex = 41
        Me.TradePriceLabel.Text = "-"
        '
        'TabPage0
        '
        Me.TabPage0.Controls.Add(Me.Charts0)
        Me.TabPage0.Location = New System.Drawing.Point(4, 25)
        Me.TabPage0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage0.Name = "TabPage0"
        Me.TabPage0.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage0.Size = New System.Drawing.Size(1709, 845)
        Me.TabPage0.TabIndex = 0
        Me.TabPage0.Text = "TabPage0"
        Me.TabPage0.UseVisualStyleBackColor = True
        '
        'Charts0
        '
        Me.Charts0.Controls.Add(Me.QuotesTab0)
        Me.Charts0.Controls.Add(Me.TradesTab0)
        Me.Charts0.Location = New System.Drawing.Point(3, 6)
        Me.Charts0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Charts0.Name = "Charts0"
        Me.Charts0.SelectedIndex = 0
        Me.Charts0.Size = New System.Drawing.Size(1697, 801)
        Me.Charts0.TabIndex = 30
        '
        'QuotesTab0
        '
        Me.QuotesTab0.Controls.Add(Me.PricesQuotesPctBox0)
        Me.QuotesTab0.Controls.Add(Me.MinusQuotesButton0)
        Me.QuotesTab0.Controls.Add(Me.QuotesPctBox0)
        Me.QuotesTab0.Controls.Add(Me.PlusQuotesButton0)
        Me.QuotesTab0.Controls.Add(Me.RightQuotesButton0)
        Me.QuotesTab0.Controls.Add(Me.LeftQuotesButton0)
        Me.QuotesTab0.Controls.Add(Me.TimesQuotesPctBox0)
        Me.QuotesTab0.Location = New System.Drawing.Point(4, 25)
        Me.QuotesTab0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.QuotesTab0.Name = "QuotesTab0"
        Me.QuotesTab0.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.QuotesTab0.Size = New System.Drawing.Size(1689, 772)
        Me.QuotesTab0.TabIndex = 0
        Me.QuotesTab0.Text = "Аск / Бид"
        Me.QuotesTab0.UseVisualStyleBackColor = True
        '
        'PricesQuotesPctBox0
        '
        Me.PricesQuotesPctBox0.Location = New System.Drawing.Point(3, 0)
        Me.PricesQuotesPctBox0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PricesQuotesPctBox0.Name = "PricesQuotesPctBox0"
        Me.PricesQuotesPctBox0.Size = New System.Drawing.Size(105, 686)
        Me.PricesQuotesPctBox0.TabIndex = 20
        Me.PricesQuotesPctBox0.TabStop = False
        '
        'MinusQuotesButton0
        '
        Me.MinusQuotesButton0.Location = New System.Drawing.Point(1635, 336)
        Me.MinusQuotesButton0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MinusQuotesButton0.Name = "MinusQuotesButton0"
        Me.MinusQuotesButton0.Size = New System.Drawing.Size(47, 318)
        Me.MinusQuotesButton0.TabIndex = 29
        Me.MinusQuotesButton0.Text = "-"
        Me.MinusQuotesButton0.UseVisualStyleBackColor = True
        '
        'QuotesPctBox0
        '
        Me.QuotesPctBox0.Location = New System.Drawing.Point(108, 0)
        Me.QuotesPctBox0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.QuotesPctBox0.Name = "QuotesPctBox0"
        Me.QuotesPctBox0.Size = New System.Drawing.Size(1521, 686)
        Me.QuotesPctBox0.TabIndex = 18
        Me.QuotesPctBox0.TabStop = False
        '
        'PlusQuotesButton0
        '
        Me.PlusQuotesButton0.Location = New System.Drawing.Point(1635, 2)
        Me.PlusQuotesButton0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PlusQuotesButton0.Name = "PlusQuotesButton0"
        Me.PlusQuotesButton0.Size = New System.Drawing.Size(48, 327)
        Me.PlusQuotesButton0.TabIndex = 28
        Me.PlusQuotesButton0.Text = "+"
        Me.PlusQuotesButton0.UseVisualStyleBackColor = True
        '
        'RightQuotesButton0
        '
        Me.RightQuotesButton0.Location = New System.Drawing.Point(887, 734)
        Me.RightQuotesButton0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RightQuotesButton0.Name = "RightQuotesButton0"
        Me.RightQuotesButton0.Size = New System.Drawing.Size(741, 33)
        Me.RightQuotesButton0.TabIndex = 27
        Me.RightQuotesButton0.Text = "Right ->"
        Me.RightQuotesButton0.UseVisualStyleBackColor = True
        '
        'LeftQuotesButton0
        '
        Me.LeftQuotesButton0.Location = New System.Drawing.Point(108, 734)
        Me.LeftQuotesButton0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LeftQuotesButton0.Name = "LeftQuotesButton0"
        Me.LeftQuotesButton0.Size = New System.Drawing.Size(773, 33)
        Me.LeftQuotesButton0.TabIndex = 26
        Me.LeftQuotesButton0.Text = "<- Left"
        Me.LeftQuotesButton0.UseVisualStyleBackColor = True
        '
        'TimesQuotesPctBox0
        '
        Me.TimesQuotesPctBox0.Location = New System.Drawing.Point(108, 692)
        Me.TimesQuotesPctBox0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TimesQuotesPctBox0.Name = "TimesQuotesPctBox0"
        Me.TimesQuotesPctBox0.Size = New System.Drawing.Size(1521, 36)
        Me.TimesQuotesPctBox0.TabIndex = 22
        Me.TimesQuotesPctBox0.TabStop = False
        '
        'TradesTab0
        '
        Me.TradesTab0.Controls.Add(Me.VolumesVolumesTradesPctBox0)
        Me.TradesTab0.Controls.Add(Me.VolumesTradesPctBox0)
        Me.TradesTab0.Controls.Add(Me.PricesTradesPctBox0)
        Me.TradesTab0.Controls.Add(Me.MinusTradesButton0)
        Me.TradesTab0.Controls.Add(Me.TradesPctBox0)
        Me.TradesTab0.Controls.Add(Me.PlusTradesButton0)
        Me.TradesTab0.Controls.Add(Me.RightButtonTrades0)
        Me.TradesTab0.Controls.Add(Me.LeftTradesButton0)
        Me.TradesTab0.Controls.Add(Me.TimesTradesPctBox0)
        Me.TradesTab0.Location = New System.Drawing.Point(4, 25)
        Me.TradesTab0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TradesTab0.Name = "TradesTab0"
        Me.TradesTab0.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TradesTab0.Size = New System.Drawing.Size(1689, 772)
        Me.TradesTab0.TabIndex = 1
        Me.TradesTab0.Text = "Сделки"
        Me.TradesTab0.UseVisualStyleBackColor = True
        '
        'VolumesVolumesTradesPctBox0
        '
        Me.VolumesVolumesTradesPctBox0.Location = New System.Drawing.Point(3, 431)
        Me.VolumesVolumesTradesPctBox0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.VolumesVolumesTradesPctBox0.Name = "VolumesVolumesTradesPctBox0"
        Me.VolumesVolumesTradesPctBox0.Size = New System.Drawing.Size(105, 254)
        Me.VolumesVolumesTradesPctBox0.TabIndex = 39
        Me.VolumesVolumesTradesPctBox0.TabStop = False
        '
        'VolumesTradesPctBox0
        '
        Me.VolumesTradesPctBox0.Location = New System.Drawing.Point(108, 431)
        Me.VolumesTradesPctBox0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.VolumesTradesPctBox0.Name = "VolumesTradesPctBox0"
        Me.VolumesTradesPctBox0.Size = New System.Drawing.Size(1521, 254)
        Me.VolumesTradesPctBox0.TabIndex = 38
        Me.VolumesTradesPctBox0.TabStop = False
        '
        'PricesTradesPctBox0
        '
        Me.PricesTradesPctBox0.Location = New System.Drawing.Point(3, 2)
        Me.PricesTradesPctBox0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PricesTradesPctBox0.Name = "PricesTradesPctBox0"
        Me.PricesTradesPctBox0.Size = New System.Drawing.Size(105, 422)
        Me.PricesTradesPctBox0.TabIndex = 37
        Me.PricesTradesPctBox0.TabStop = False
        '
        'MinusTradesButton0
        '
        Me.MinusTradesButton0.Location = New System.Drawing.Point(1635, 353)
        Me.MinusTradesButton0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MinusTradesButton0.Name = "MinusTradesButton0"
        Me.MinusTradesButton0.Size = New System.Drawing.Size(47, 345)
        Me.MinusTradesButton0.TabIndex = 36
        Me.MinusTradesButton0.Text = "-"
        Me.MinusTradesButton0.UseVisualStyleBackColor = True
        '
        'TradesPctBox0
        '
        Me.TradesPctBox0.Location = New System.Drawing.Point(108, 0)
        Me.TradesPctBox0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TradesPctBox0.Name = "TradesPctBox0"
        Me.TradesPctBox0.Size = New System.Drawing.Size(1521, 425)
        Me.TradesPctBox0.TabIndex = 30
        Me.TradesPctBox0.TabStop = False
        '
        'PlusTradesButton0
        '
        Me.PlusTradesButton0.Location = New System.Drawing.Point(1635, 6)
        Me.PlusTradesButton0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PlusTradesButton0.Name = "PlusTradesButton0"
        Me.PlusTradesButton0.Size = New System.Drawing.Size(48, 341)
        Me.PlusTradesButton0.TabIndex = 35
        Me.PlusTradesButton0.Text = "+"
        Me.PlusTradesButton0.UseVisualStyleBackColor = True
        '
        'RightButtonTrades0
        '
        Me.RightButtonTrades0.Location = New System.Drawing.Point(869, 734)
        Me.RightButtonTrades0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RightButtonTrades0.Name = "RightButtonTrades0"
        Me.RightButtonTrades0.Size = New System.Drawing.Size(759, 33)
        Me.RightButtonTrades0.TabIndex = 34
        Me.RightButtonTrades0.Text = "Right ->"
        Me.RightButtonTrades0.UseVisualStyleBackColor = True
        '
        'LeftTradesButton0
        '
        Me.LeftTradesButton0.Location = New System.Drawing.Point(108, 734)
        Me.LeftTradesButton0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LeftTradesButton0.Name = "LeftTradesButton0"
        Me.LeftTradesButton0.Size = New System.Drawing.Size(756, 33)
        Me.LeftTradesButton0.TabIndex = 33
        Me.LeftTradesButton0.Text = "<- Left"
        Me.LeftTradesButton0.UseVisualStyleBackColor = True
        '
        'TimesTradesPctBox0
        '
        Me.TimesTradesPctBox0.Location = New System.Drawing.Point(108, 690)
        Me.TimesTradesPctBox0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TimesTradesPctBox0.Name = "TimesTradesPctBox0"
        Me.TimesTradesPctBox0.Size = New System.Drawing.Size(1521, 36)
        Me.TimesTradesPctBox0.TabIndex = 32
        Me.TimesTradesPctBox0.TabStop = False
        '
        'Tabs
        '
        Me.Tabs.Controls.Add(Me.TabPage0)
        Me.Tabs.Location = New System.Drawing.Point(23, 82)
        Me.Tabs.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(1717, 874)
        Me.Tabs.TabIndex = 31
        '
        'TicksOrSeconds
        '
        Me.TicksOrSeconds.FormattingEnabled = True
        Me.TicksOrSeconds.Items.AddRange(New Object() {"5 секунд", "Тики"})
        Me.TicksOrSeconds.Location = New System.Drawing.Point(289, 11)
        Me.TicksOrSeconds.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TicksOrSeconds.Name = "TicksOrSeconds"
        Me.TicksOrSeconds.Size = New System.Drawing.Size(291, 24)
        Me.TicksOrSeconds.TabIndex = 44
        '
        'TypeOfGraphic
        '
        Me.TypeOfGraphic.FormattingEnabled = True
        Me.TypeOfGraphic.Items.AddRange(New Object() {"Линии", "Японские свечи", "Бары"})
        Me.TypeOfGraphic.Location = New System.Drawing.Point(601, 10)
        Me.TypeOfGraphic.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TypeOfGraphic.Name = "TypeOfGraphic"
        Me.TypeOfGraphic.Size = New System.Drawing.Size(179, 24)
        Me.TypeOfGraphic.TabIndex = 45
        '
        'BuyAndSell
        '
        Me.BuyAndSell.AutoSize = True
        Me.BuyAndSell.Location = New System.Drawing.Point(473, 56)
        Me.BuyAndSell.Name = "BuyAndSell"
        Me.BuyAndSell.Size = New System.Drawing.Size(151, 21)
        Me.BuyAndSell.TabIndex = 52
        Me.BuyAndSell.TabStop = True
        Me.BuyAndSell.Text = "Покупка+Продажа"
        Me.BuyAndSell.UseVisualStyleBackColor = True
        '
        'Sell
        '
        Me.Sell.AutoSize = True
        Me.Sell.Location = New System.Drawing.Point(379, 56)
        Me.Sell.Name = "Sell"
        Me.Sell.Size = New System.Drawing.Size(88, 21)
        Me.Sell.TabIndex = 51
        Me.Sell.TabStop = True
        Me.Sell.Text = "Продажа"
        Me.Sell.UseVisualStyleBackColor = True
        '
        'Buy
        '
        Me.Buy.AutoSize = True
        Me.Buy.Location = New System.Drawing.Point(289, 56)
        Me.Buy.Name = "Buy"
        Me.Buy.Size = New System.Drawing.Size(84, 21)
        Me.Buy.TabIndex = 50
        Me.Buy.TabStop = True
        Me.Buy.Text = "Покупка"
        Me.Buy.UseVisualStyleBackColor = True
        '
        'Form1Clone
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1740, 1017)
        Me.Controls.Add(Me.BuyAndSell)
        Me.Controls.Add(Me.Sell)
        Me.Controls.Add(Me.Buy)
        Me.Controls.Add(Me.TypeOfGraphic)
        Me.Controls.Add(Me.TicksOrSeconds)
        Me.Controls.Add(Me.TradeVolumeLabel)
        Me.Controls.Add(Me.BidPriceLabel)
        Me.Controls.Add(Me.AskPriceLabel)
        Me.Controls.Add(Me.TradePriceLabel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CurVolumeLabel)
        Me.Controls.Add(Me.VolumeLabel)
        Me.Controls.Add(Me.DrawLineTrades0)
        Me.Controls.Add(Me.TimeLabel0)
        Me.Controls.Add(Me.DrawLineQuotes0)
        Me.Controls.Add(Me.PriceLabel0)
        Me.Controls.Add(Me.Tabs)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form1Clone"
        Me.Text = "A&K Trader 1.0"
        Me.TabPage0.ResumeLayout(False)
        Me.Charts0.ResumeLayout(False)
        Me.QuotesTab0.ResumeLayout(False)
        CType(Me.PricesQuotesPctBox0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QuotesPctBox0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimesQuotesPctBox0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TradesTab0.ResumeLayout(False)
        CType(Me.VolumesVolumesTradesPctBox0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VolumesTradesPctBox0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PricesTradesPctBox0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TradesPctBox0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimesTradesPctBox0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tabs.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DrawLineTrades0 As Button
    Friend WithEvents DrawLineQuotes0 As Button
    Friend WithEvents TimeLabel0 As Label
    Friend WithEvents PriceLabel0 As Label
    Friend WithEvents VolumeLabel As Label
    Friend WithEvents CurVolumeLabel As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents AskPriceLabel As Label
    Friend WithEvents BidPriceLabel As Label
    Friend WithEvents TradeVolumeLabel As Label
    Friend WithEvents TradePriceLabel As Label
    Friend WithEvents TabPage0 As TabPage
    Friend WithEvents Charts0 As TabControl
    Friend WithEvents QuotesTab0 As TabPage
    Friend WithEvents PricesQuotesPctBox0 As PictureBox
    Friend WithEvents MinusQuotesButton0 As Button
    Friend WithEvents QuotesPctBox0 As PictureBox
    Friend WithEvents PlusQuotesButton0 As Button
    Friend WithEvents RightQuotesButton0 As Button
    Friend WithEvents LeftQuotesButton0 As Button
    Friend WithEvents TimesQuotesPctBox0 As PictureBox
    Friend WithEvents TradesTab0 As TabPage
    Friend WithEvents VolumesVolumesTradesPctBox0 As PictureBox
    Friend WithEvents VolumesTradesPctBox0 As PictureBox
    Friend WithEvents PricesTradesPctBox0 As PictureBox
    Friend WithEvents MinusTradesButton0 As Button
    Friend WithEvents TradesPctBox0 As PictureBox
    Friend WithEvents PlusTradesButton0 As Button
    Friend WithEvents RightButtonTrades0 As Button
    Friend WithEvents LeftTradesButton0 As Button
    Friend WithEvents TimesTradesPctBox0 As PictureBox
    Friend WithEvents Tabs As TabControl
    Friend WithEvents TicksOrSeconds As ComboBox
    Friend WithEvents TypeOfGraphic As ComboBox
    Friend WithEvents BuyAndSell As RadioButton
    Friend WithEvents Sell As RadioButton
    Friend WithEvents Buy As RadioButton
End Class
