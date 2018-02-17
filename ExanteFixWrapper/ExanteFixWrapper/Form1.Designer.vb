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
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PricesPctBox = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.TimesPctBox = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        CType(Me.QuotesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PricesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TimesPctBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(110, 59)
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
        Me.Label1.Location = New System.Drawing.Point(107, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Нет соединения"
        '
        'ExanteIDTextBox
        '
        Me.ExanteIDTextBox.Location = New System.Drawing.Point(321, 27)
        Me.ExanteIDTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ExanteIDTextBox.Name = "ExanteIDTextBox"
        Me.ExanteIDTextBox.Size = New System.Drawing.Size(291, 22)
        Me.ExanteIDTextBox.TabIndex = 2
        Me.ExanteIDTextBox.Text = "BTC.EXANTE"
        '
        'BidPriceTextBox
        '
        Me.BidPriceTextBox.Location = New System.Drawing.Point(1189, 75)
        Me.BidPriceTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.BidPriceTextBox.Name = "BidPriceTextBox"
        Me.BidPriceTextBox.Size = New System.Drawing.Size(132, 22)
        Me.BidPriceTextBox.TabIndex = 3
        '
        'LabelBid
        '
        Me.LabelBid.AutoSize = True
        Me.LabelBid.Location = New System.Drawing.Point(1133, 78)
        Me.LabelBid.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelBid.Name = "LabelBid"
        Me.LabelBid.Size = New System.Drawing.Size(28, 17)
        Me.LabelBid.TabIndex = 4
        Me.LabelBid.Text = "Bid"
        '
        'LabelAsk
        '
        Me.LabelAsk.AutoSize = True
        Me.LabelAsk.Location = New System.Drawing.Point(1133, 27)
        Me.LabelAsk.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelAsk.Name = "LabelAsk"
        Me.LabelAsk.Size = New System.Drawing.Size(31, 17)
        Me.LabelAsk.TabIndex = 5
        Me.LabelAsk.Text = "Ask"
        '
        'AskPriceTextBox
        '
        Me.AskPriceTextBox.Location = New System.Drawing.Point(1189, 27)
        Me.AskPriceTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AskPriceTextBox.Name = "AskPriceTextBox"
        Me.AskPriceTextBox.Size = New System.Drawing.Size(132, 22)
        Me.AskPriceTextBox.TabIndex = 6
        '
        'AskVolumeTextBox
        '
        Me.AskVolumeTextBox.Location = New System.Drawing.Point(1348, 27)
        Me.AskVolumeTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.AskVolumeTextBox.Name = "AskVolumeTextBox"
        Me.AskVolumeTextBox.Size = New System.Drawing.Size(132, 22)
        Me.AskVolumeTextBox.TabIndex = 8
        '
        'BidVolumeTextBox
        '
        Me.BidVolumeTextBox.Location = New System.Drawing.Point(1348, 75)
        Me.BidVolumeTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.BidVolumeTextBox.Name = "BidVolumeTextBox"
        Me.BidVolumeTextBox.Size = New System.Drawing.Size(132, 22)
        Me.BidVolumeTextBox.TabIndex = 7
        '
        'PriceLabel
        '
        Me.PriceLabel.AutoSize = True
        Me.PriceLabel.Location = New System.Drawing.Point(1233, 6)
        Me.PriceLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PriceLabel.Name = "PriceLabel"
        Me.PriceLabel.Size = New System.Drawing.Size(40, 17)
        Me.PriceLabel.TabIndex = 9
        Me.PriceLabel.Text = "Price"
        '
        'VolumeLabel
        '
        Me.VolumeLabel.AutoSize = True
        Me.VolumeLabel.Location = New System.Drawing.Point(1389, 6)
        Me.VolumeLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.VolumeLabel.Name = "VolumeLabel"
        Me.VolumeLabel.Size = New System.Drawing.Size(55, 17)
        Me.VolumeLabel.TabIndex = 10
        Me.VolumeLabel.Text = "Volume"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(400, 59)
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
        Me.Label2.Location = New System.Drawing.Point(757, 27)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(178, 17)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Объем последней сделки"
        '
        'TradeVolumeTextBox
        '
        Me.TradeVolumeTextBox.Location = New System.Drawing.Point(959, 28)
        Me.TradeVolumeTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.TradeVolumeTextBox.Name = "TradeVolumeTextBox"
        Me.TradeVolumeTextBox.Size = New System.Drawing.Size(132, 22)
        Me.TradeVolumeTextBox.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(767, 70)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(168, 17)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Цена последней сделки"
        '
        'TradePriceTextBox
        '
        Me.TradePriceTextBox.Location = New System.Drawing.Point(959, 74)
        Me.TradePriceTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.TradePriceTextBox.Name = "TradePriceTextBox"
        Me.TradePriceTextBox.Size = New System.Drawing.Size(132, 22)
        Me.TradePriceTextBox.TabIndex = 17
        '
        'QuotesPctBox
        '
        Me.QuotesPctBox.Location = New System.Drawing.Point(138, 109)
        Me.QuotesPctBox.Name = "QuotesPctBox"
        Me.QuotesPctBox.Size = New System.Drawing.Size(1374, 465)
        Me.QuotesPctBox.TabIndex = 18
        Me.QuotesPctBox.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(138, 661)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(1374, 165)
        Me.PictureBox2.TabIndex = 19
        Me.PictureBox2.TabStop = False
        '
        'PricesPctBox
        '
        Me.PricesPctBox.Location = New System.Drawing.Point(33, 109)
        Me.PricesPctBox.Name = "PricesPctBox"
        Me.PricesPctBox.Size = New System.Drawing.Size(105, 465)
        Me.PricesPctBox.TabIndex = 20
        Me.PricesPctBox.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Location = New System.Drawing.Point(33, 661)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(105, 165)
        Me.PictureBox4.TabIndex = 21
        Me.PictureBox4.TabStop = False
        '
        'TimesPctBox
        '
        Me.TimesPctBox.Location = New System.Drawing.Point(138, 580)
        Me.TimesPctBox.Name = "TimesPctBox"
        Me.TimesPctBox.Size = New System.Drawing.Size(1374, 36)
        Me.TimesPctBox.TabIndex = 22
        Me.TimesPctBox.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Location = New System.Drawing.Point(138, 823)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(1374, 36)
        Me.PictureBox6.TabIndex = 23
        Me.PictureBox6.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(651, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 17)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Price"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(651, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 17)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Time"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(138, 622)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(675, 33)
        Me.Button3.TabIndex = 26
        Me.Button3.Text = "<- Left"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(829, 622)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(683, 33)
        Me.Button4.TabIndex = 27
        Me.Button4.Text = "Right ->"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(1518, 109)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(48, 233)
        Me.Button5.TabIndex = 28
        Me.Button5.Text = "+"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(1519, 348)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(47, 233)
        Me.Button6.TabIndex = 29
        Me.Button6.Text = "-"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1682, 885)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.TimesPctBox)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PricesPctBox)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.QuotesPctBox)
        Me.Controls.Add(Me.TradePriceTextBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TradeVolumeTextBox)
        Me.Controls.Add(Me.Label2)
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
        CType(Me.QuotesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PricesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TimesPctBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PricesPctBox As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents TimesPctBox As PictureBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
End Class
