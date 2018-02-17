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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TradeVolumeTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TradePriceTextBox = New System.Windows.Forms.TextBox()
        Me.QuotesPctBox = New System.Windows.Forms.PictureBox()
        Me.PricesQuotesPctBox = New System.Windows.Forms.PictureBox()
        Me.TimesQuotesPctBox = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Charts = New System.Windows.Forms.TabControl()
        Me.QuotesTab = New System.Windows.Forms.TabPage()
        Me.TradesTab = New System.Windows.Forms.TabPage()
        Me.PricesTradesPctBox = New System.Windows.Forms.PictureBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.TradesPctBox = New System.Windows.Forms.PictureBox()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.TimesTradesPctBox = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        CType(Me.QuotesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PricesQuotesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimesQuotesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Charts.SuspendLayout()
        Me.QuotesTab.SuspendLayout()
        Me.TradesTab.SuspendLayout()
        CType(Me.PricesTradesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TradesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimesTradesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(150, 6)
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
        Me.Label1.Location = New System.Drawing.Point(26, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Нет соединения"
        '
        'ExanteIDTextBox
        '
        Me.ExanteIDTextBox.Location = New System.Drawing.Point(42, 35)
        Me.ExanteIDTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ExanteIDTextBox.Name = "ExanteIDTextBox"
        Me.ExanteIDTextBox.Size = New System.Drawing.Size(291, 22)
        Me.ExanteIDTextBox.TabIndex = 2
        Me.ExanteIDTextBox.Text = "BTC.EXANTE"
        '
        'BidPriceTextBox
        '
        Me.BidPriceTextBox.Location = New System.Drawing.Point(1369, 97)
        Me.BidPriceTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.BidPriceTextBox.Name = "BidPriceTextBox"
        Me.BidPriceTextBox.Size = New System.Drawing.Size(132, 22)
        Me.BidPriceTextBox.TabIndex = 3
        '
        'LabelBid
        '
        Me.LabelBid.AutoSize = True
        Me.LabelBid.Location = New System.Drawing.Point(1313, 100)
        Me.LabelBid.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBid.Name = "LabelBid"
        Me.LabelBid.Size = New System.Drawing.Size(28, 17)
        Me.LabelBid.TabIndex = 4
        Me.LabelBid.Text = "Bid"
        '
        'LabelAsk
        '
        Me.LabelAsk.AutoSize = True
        Me.LabelAsk.Location = New System.Drawing.Point(1313, 49)
        Me.LabelAsk.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelAsk.Name = "LabelAsk"
        Me.LabelAsk.Size = New System.Drawing.Size(31, 17)
        Me.LabelAsk.TabIndex = 5
        Me.LabelAsk.Text = "Ask"
        '
        'AskPriceTextBox
        '
        Me.AskPriceTextBox.Location = New System.Drawing.Point(1369, 49)
        Me.AskPriceTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AskPriceTextBox.Name = "AskPriceTextBox"
        Me.AskPriceTextBox.Size = New System.Drawing.Size(132, 22)
        Me.AskPriceTextBox.TabIndex = 6
        '
        'AskVolumeTextBox
        '
        Me.AskVolumeTextBox.Location = New System.Drawing.Point(1528, 49)
        Me.AskVolumeTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AskVolumeTextBox.Name = "AskVolumeTextBox"
        Me.AskVolumeTextBox.Size = New System.Drawing.Size(132, 22)
        Me.AskVolumeTextBox.TabIndex = 8
        '
        'BidVolumeTextBox
        '
        Me.BidVolumeTextBox.Location = New System.Drawing.Point(1528, 97)
        Me.BidVolumeTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.BidVolumeTextBox.Name = "BidVolumeTextBox"
        Me.BidVolumeTextBox.Size = New System.Drawing.Size(132, 22)
        Me.BidVolumeTextBox.TabIndex = 7
        '
        'PriceLabel
        '
        Me.PriceLabel.AutoSize = True
        Me.PriceLabel.Location = New System.Drawing.Point(1408, 17)
        Me.PriceLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PriceLabel.Name = "PriceLabel"
        Me.PriceLabel.Size = New System.Drawing.Size(40, 17)
        Me.PriceLabel.TabIndex = 9
        Me.PriceLabel.Text = "Price"
        '
        'VolumeLabel
        '
        Me.VolumeLabel.AutoSize = True
        Me.VolumeLabel.Location = New System.Drawing.Point(1564, 17)
        Me.VolumeLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.VolumeLabel.Name = "VolumeLabel"
        Me.VolumeLabel.Size = New System.Drawing.Size(55, 17)
        Me.VolumeLabel.TabIndex = 10
        Me.VolumeLabel.Text = "Volume"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(121, 67)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(116, 28)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Подписаться"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(905, 49)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(178, 17)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Объем последней сделки"
        '
        'TradeVolumeTextBox
        '
        Me.TradeVolumeTextBox.Location = New System.Drawing.Point(1107, 50)
        Me.TradeVolumeTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.TradeVolumeTextBox.Name = "TradeVolumeTextBox"
        Me.TradeVolumeTextBox.Size = New System.Drawing.Size(132, 22)
        Me.TradeVolumeTextBox.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(915, 92)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(168, 17)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Цена последней сделки"
        '
        'TradePriceTextBox
        '
        Me.TradePriceTextBox.Location = New System.Drawing.Point(1107, 96)
        Me.TradePriceTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.TradePriceTextBox.Name = "TradePriceTextBox"
        Me.TradePriceTextBox.Size = New System.Drawing.Size(132, 22)
        Me.TradePriceTextBox.TabIndex = 17
        '
        'QuotesPctBox
        '
        Me.QuotesPctBox.Location = New System.Drawing.Point(108, 0)
        Me.QuotesPctBox.Name = "QuotesPctBox"
        Me.QuotesPctBox.Size = New System.Drawing.Size(1521, 654)
        Me.QuotesPctBox.TabIndex = 18
        Me.QuotesPctBox.TabStop = False
        '
        'PricesQuotesPctBox
        '
        Me.PricesQuotesPctBox.Location = New System.Drawing.Point(3, 0)
        Me.PricesQuotesPctBox.Name = "PricesQuotesPctBox"
        Me.PricesQuotesPctBox.Size = New System.Drawing.Size(105, 654)
        Me.PricesQuotesPctBox.TabIndex = 20
        Me.PricesQuotesPctBox.TabStop = False
        '
        'TimesQuotesPctBox
        '
        Me.TimesQuotesPctBox.Location = New System.Drawing.Point(108, 660)
        Me.TimesQuotesPctBox.Name = "TimesQuotesPctBox"
        Me.TimesQuotesPctBox.Size = New System.Drawing.Size(1521, 36)
        Me.TimesQuotesPctBox.TabIndex = 22
        Me.TimesQuotesPctBox.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(381, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 17)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Price"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(381, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 17)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Time"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(108, 702)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(773, 33)
        Me.Button3.TabIndex = 26
        Me.Button3.Text = "<- Left"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(887, 702)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(742, 33)
        Me.Button4.TabIndex = 27
        Me.Button4.Text = "Right ->"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(1635, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(48, 327)
        Me.Button5.TabIndex = 28
        Me.Button5.Text = "+"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(1635, 336)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(47, 318)
        Me.Button6.TabIndex = 29
        Me.Button6.Text = "-"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Charts
        '
        Me.Charts.Controls.Add(Me.QuotesTab)
        Me.Charts.Controls.Add(Me.TradesTab)
        Me.Charts.Location = New System.Drawing.Point(6, 129)
        Me.Charts.Name = "Charts"
        Me.Charts.SelectedIndex = 0
        Me.Charts.Size = New System.Drawing.Size(1697, 770)
        Me.Charts.TabIndex = 30
        '
        'QuotesTab
        '
        Me.QuotesTab.Controls.Add(Me.PricesQuotesPctBox)
        Me.QuotesTab.Controls.Add(Me.Button6)
        Me.QuotesTab.Controls.Add(Me.QuotesPctBox)
        Me.QuotesTab.Controls.Add(Me.Button5)
        Me.QuotesTab.Controls.Add(Me.Button4)
        Me.QuotesTab.Controls.Add(Me.Button3)
        Me.QuotesTab.Controls.Add(Me.TimesQuotesPctBox)
        Me.QuotesTab.Location = New System.Drawing.Point(4, 25)
        Me.QuotesTab.Name = "QuotesTab"
        Me.QuotesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.QuotesTab.Size = New System.Drawing.Size(1689, 741)
        Me.QuotesTab.TabIndex = 0
        Me.QuotesTab.Text = "Аск / Бид"
        Me.QuotesTab.UseVisualStyleBackColor = True
        '
        'TradesTab
        '
        Me.TradesTab.Controls.Add(Me.PricesTradesPctBox)
        Me.TradesTab.Controls.Add(Me.Button7)
        Me.TradesTab.Controls.Add(Me.TradesPctBox)
        Me.TradesTab.Controls.Add(Me.Button8)
        Me.TradesTab.Controls.Add(Me.Button9)
        Me.TradesTab.Controls.Add(Me.Button10)
        Me.TradesTab.Controls.Add(Me.TimesTradesPctBox)
        Me.TradesTab.Location = New System.Drawing.Point(4, 25)
        Me.TradesTab.Name = "TradesTab"
        Me.TradesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.TradesTab.Size = New System.Drawing.Size(1689, 741)
        Me.TradesTab.TabIndex = 1
        Me.TradesTab.Text = "Сделки"
        Me.TradesTab.UseVisualStyleBackColor = True
        '
        'PricesTradesPctBox
        '
        Me.PricesTradesPctBox.Location = New System.Drawing.Point(3, 3)
        Me.PricesTradesPctBox.Name = "PricesTradesPctBox"
        Me.PricesTradesPctBox.Size = New System.Drawing.Size(105, 507)
        Me.PricesTradesPctBox.TabIndex = 37
        Me.PricesTradesPctBox.TabStop = False
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(1635, 246)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(47, 267)
        Me.Button7.TabIndex = 36
        Me.Button7.Text = "-"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'TradesPctBox
        '
        Me.TradesPctBox.Location = New System.Drawing.Point(108, 0)
        Me.TradesPctBox.Name = "TradesPctBox"
        Me.TradesPctBox.Size = New System.Drawing.Size(1521, 510)
        Me.TradesPctBox.TabIndex = 30
        Me.TradesPctBox.TabStop = False
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(1635, 6)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(48, 234)
        Me.Button8.TabIndex = 35
        Me.Button8.Text = "+"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(870, 558)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(759, 33)
        Me.Button9.TabIndex = 34
        Me.Button9.Text = "Right ->"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(108, 558)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(756, 33)
        Me.Button10.TabIndex = 33
        Me.Button10.Text = "<- Left"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'TimesTradesPctBox
        '
        Me.TimesTradesPctBox.Location = New System.Drawing.Point(108, 516)
        Me.TimesTradesPctBox.Name = "TimesTradesPctBox"
        Me.TimesTradesPctBox.Size = New System.Drawing.Size(1521, 36)
        Me.TimesTradesPctBox.TabIndex = 32
        Me.TimesTradesPctBox.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 41)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1717, 943)
        Me.TabControl1.TabIndex = 31
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Charts)
        Me.TabPage1.Controls.Add(Me.ExanteIDTextBox)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.BidPriceTextBox)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.LabelBid)
        Me.TabPage1.Controls.Add(Me.TradePriceTextBox)
        Me.TabPage1.Controls.Add(Me.LabelAsk)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.AskPriceTextBox)
        Me.TabPage1.Controls.Add(Me.TradeVolumeTextBox)
        Me.TabPage1.Controls.Add(Me.BidVolumeTextBox)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.AskVolumeTextBox)
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.PriceLabel)
        Me.TabPage1.Controls.Add(Me.VolumeLabel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1709, 914)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1709, 914)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1735, 1045)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form1"
        Me.Text = "A&K Trader 1.0"
        CType(Me.QuotesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PricesQuotesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimesQuotesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Charts.ResumeLayout(False)
        Me.QuotesTab.ResumeLayout(False)
        Me.TradesTab.ResumeLayout(False)
        CType(Me.PricesTradesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TradesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimesTradesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TradeVolumeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TradePriceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents QuotesPctBox As PictureBox
    Friend WithEvents PricesQuotesPctBox As PictureBox
    Friend WithEvents TimesQuotesPctBox As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Charts As TabControl
    Friend WithEvents QuotesTab As TabPage
    Friend WithEvents TradesTab As TabPage
    Friend WithEvents Button7 As Button
    Friend WithEvents TradesPctBox As PictureBox
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents TimesTradesPctBox As PictureBox
    Friend WithEvents PricesTradesPctBox As PictureBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
End Class
