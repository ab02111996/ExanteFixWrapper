<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
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
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(39, 57)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Соединиться"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Нет соединения"
        '
        'ExanteIDTextBox
        '
        Me.ExanteIDTextBox.Location = New System.Drawing.Point(299, 22)
        Me.ExanteIDTextBox.Name = "ExanteIDTextBox"
        Me.ExanteIDTextBox.Size = New System.Drawing.Size(219, 20)
        Me.ExanteIDTextBox.TabIndex = 2
        Me.ExanteIDTextBox.Text = "BTC.EXANTE"
        '
        'BidPriceTextBox
        '
        Me.BidPriceTextBox.Location = New System.Drawing.Point(338, 121)
        Me.BidPriceTextBox.Name = "BidPriceTextBox"
        Me.BidPriceTextBox.Size = New System.Drawing.Size(100, 20)
        Me.BidPriceTextBox.TabIndex = 3
        '
        'LabelBid
        '
        Me.LabelBid.AutoSize = True
        Me.LabelBid.Location = New System.Drawing.Point(296, 124)
        Me.LabelBid.Name = "LabelBid"
        Me.LabelBid.Size = New System.Drawing.Size(22, 13)
        Me.LabelBid.TabIndex = 4
        Me.LabelBid.Text = "Bid"
        '
        'LabelAsk
        '
        Me.LabelAsk.AutoSize = True
        Me.LabelAsk.Location = New System.Drawing.Point(296, 83)
        Me.LabelAsk.Name = "LabelAsk"
        Me.LabelAsk.Size = New System.Drawing.Size(25, 13)
        Me.LabelAsk.TabIndex = 5
        Me.LabelAsk.Text = "Ask"
        '
        'AskPriceTextBox
        '
        Me.AskPriceTextBox.Location = New System.Drawing.Point(338, 83)
        Me.AskPriceTextBox.Name = "AskPriceTextBox"
        Me.AskPriceTextBox.Size = New System.Drawing.Size(100, 20)
        Me.AskPriceTextBox.TabIndex = 6
        '
        'AskVolumeTextBox
        '
        Me.AskVolumeTextBox.Location = New System.Drawing.Point(457, 83)
        Me.AskVolumeTextBox.Name = "AskVolumeTextBox"
        Me.AskVolumeTextBox.Size = New System.Drawing.Size(100, 20)
        Me.AskVolumeTextBox.TabIndex = 8
        '
        'BidVolumeTextBox
        '
        Me.BidVolumeTextBox.Location = New System.Drawing.Point(457, 121)
        Me.BidVolumeTextBox.Name = "BidVolumeTextBox"
        Me.BidVolumeTextBox.Size = New System.Drawing.Size(100, 20)
        Me.BidVolumeTextBox.TabIndex = 7
        '
        'PriceLabel
        '
        Me.PriceLabel.AutoSize = True
        Me.PriceLabel.Location = New System.Drawing.Point(365, 55)
        Me.PriceLabel.Name = "PriceLabel"
        Me.PriceLabel.Size = New System.Drawing.Size(31, 13)
        Me.PriceLabel.TabIndex = 9
        Me.PriceLabel.Text = "Price"
        '
        'VolumeLabel
        '
        Me.VolumeLabel.AutoSize = True
        Me.VolumeLabel.Location = New System.Drawing.Point(482, 55)
        Me.VolumeLabel.Name = "VolumeLabel"
        Me.VolumeLabel.Size = New System.Drawing.Size(42, 13)
        Me.VolumeLabel.TabIndex = 10
        Me.VolumeLabel.Text = "Volume"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(533, 19)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(87, 23)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Подписаться"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1051, 522)
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
        Me.Name = "Form1"
        Me.Text = "Form1"
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

End Class
