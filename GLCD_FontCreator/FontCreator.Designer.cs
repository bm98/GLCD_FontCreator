namespace GLCD_FontCreator
{
  partial class FontCreator
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if ( disposing && ( components != null ) ) {
        components.Dispose( );
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontCreator));
      this.fDlg = new System.Windows.Forms.FontDialog();
      this.btNewFont = new System.Windows.Forms.Button();
      this.lbFontProps = new System.Windows.Forms.ListBox();
      this.txMyText = new System.Windows.Forms.TextBox();
      this.txFirstChar = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txLastChar = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txCharCount = new System.Windows.Forms.TextBox();
      this.txFirstCharASC = new System.Windows.Forms.TextBox();
      this.txLastCharASC = new System.Windows.Forms.TextBox();
      this.txTargetSize = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.btOptimizeFont = new System.Windows.Forms.Button();
      this.btSaveFontAs = new System.Windows.Forms.Button();
      this.cbxRemoveTop = new System.Windows.Forms.CheckBox();
      this.cbxRemoveBottom = new System.Windows.Forms.CheckBox();
      this.txFinalSize = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txFontName = new System.Windows.Forms.TextBox();
      this.btToTest = new System.Windows.Forms.Button();
      this.hScrTargetHeight = new System.Windows.Forms.HScrollBar();
      this.btClearTest = new System.Windows.Forms.Button();
      this.sfDlg = new System.Windows.Forms.SaveFileDialog();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbTrimMinimum = new System.Windows.Forms.RadioButton();
      this.rbTrimMono = new System.Windows.Forms.RadioButton();
      this.rbNoTrim = new System.Windows.Forms.RadioButton();
      this.label7 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.txTxFont = new System.Windows.Forms.TextBox();
      this.comFileFormat = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.menu = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.addFontfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ofDlg = new System.Windows.Forms.OpenFileDialog();
      this.fbDlg = new System.Windows.Forms.FolderBrowserDialog();
      this.lblDirty = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.menu.SuspendLayout();
      this.SuspendLayout();
      // 
      // fDlg
      // 
      this.fDlg.AllowVectorFonts = false;
      this.fDlg.AllowVerticalFonts = false;
      this.fDlg.FontMustExist = true;
      // 
      // btNewFont
      // 
      this.btNewFont.Location = new System.Drawing.Point(6, 39);
      this.btNewFont.Name = "btNewFont";
      this.btNewFont.Size = new System.Drawing.Size(98, 28);
      this.btNewFont.TabIndex = 4;
      this.btNewFont.Text = "Load Font...";
      this.btNewFont.UseVisualStyleBackColor = true;
      this.btNewFont.Click += new System.EventHandler(this.btNewFont_Click);
      // 
      // lbFontProps
      // 
      this.lbFontProps.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbFontProps.FormattingEnabled = true;
      this.lbFontProps.Location = new System.Drawing.Point(427, 41);
      this.lbFontProps.Name = "lbFontProps";
      this.lbFontProps.Size = new System.Drawing.Size(609, 160);
      this.lbFontProps.TabIndex = 2;
      this.lbFontProps.TabStop = false;
      // 
      // txMyText
      // 
      this.txMyText.BackColor = System.Drawing.Color.YellowGreen;
      this.txMyText.Location = new System.Drawing.Point(75, 71);
      this.txMyText.Name = "txMyText";
      this.txMyText.Size = new System.Drawing.Size(253, 22);
      this.txMyText.TabIndex = 6;
      this.txMyText.TextChanged += new System.EventHandler(this.txMyText_TextChanged);
      // 
      // txFirstChar
      // 
      this.txFirstChar.BackColor = System.Drawing.Color.YellowGreen;
      this.txFirstChar.Location = new System.Drawing.Point(75, 15);
      this.txFirstChar.MaxLength = 1;
      this.txFirstChar.Name = "txFirstChar";
      this.txFirstChar.Size = new System.Drawing.Size(48, 22);
      this.txFirstChar.TabIndex = 0;
      this.txFirstChar.Text = "%";
      this.txFirstChar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txFirstChar.TextChanged += new System.EventHandler(this.txFirstChar_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "First char:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(173, 18);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(57, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Last Char:";
      // 
      // txLastChar
      // 
      this.txLastChar.BackColor = System.Drawing.Color.YellowGreen;
      this.txLastChar.Location = new System.Drawing.Point(236, 15);
      this.txLastChar.MaxLength = 1;
      this.txLastChar.Name = "txLastChar";
      this.txLastChar.Size = new System.Drawing.Size(48, 22);
      this.txLastChar.TabIndex = 1;
      this.txLastChar.Text = "9";
      this.txLastChar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txLastChar.TextChanged += new System.EventHandler(this.txLastChar_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(173, 46);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(69, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Char Count:";
      // 
      // txCharCount
      // 
      this.txCharCount.Location = new System.Drawing.Point(290, 43);
      this.txCharCount.Name = "txCharCount";
      this.txCharCount.ReadOnly = true;
      this.txCharCount.Size = new System.Drawing.Size(38, 22);
      this.txCharCount.TabIndex = 6;
      this.txCharCount.TabStop = false;
      this.txCharCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // txFirstCharASC
      // 
      this.txFirstCharASC.Location = new System.Drawing.Point(129, 15);
      this.txFirstCharASC.Name = "txFirstCharASC";
      this.txFirstCharASC.ReadOnly = true;
      this.txFirstCharASC.Size = new System.Drawing.Size(38, 22);
      this.txFirstCharASC.TabIndex = 6;
      this.txFirstCharASC.TabStop = false;
      this.txFirstCharASC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // txLastCharASC
      // 
      this.txLastCharASC.Location = new System.Drawing.Point(290, 15);
      this.txLastCharASC.Name = "txLastCharASC";
      this.txLastCharASC.ReadOnly = true;
      this.txLastCharASC.Size = new System.Drawing.Size(38, 22);
      this.txLastCharASC.TabIndex = 6;
      this.txLastCharASC.TabStop = false;
      this.txLastCharASC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // txTargetSize
      // 
      this.txTargetSize.BackColor = System.Drawing.Color.YellowGreen;
      this.txTargetSize.Location = new System.Drawing.Point(123, 15);
      this.txTargetSize.MaxLength = 3;
      this.txTargetSize.Name = "txTargetSize";
      this.txTargetSize.ReadOnly = true;
      this.txTargetSize.Size = new System.Drawing.Size(48, 22);
      this.txTargetSize.TabIndex = 3;
      this.txTargetSize.TabStop = false;
      this.txTargetSize.Text = "16";
      this.txTargetSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(7, 18);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(104, 13);
      this.label4.TabIndex = 5;
      this.label4.Text = "Target Height [pix]:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(5, 74);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(64, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Test String:";
      // 
      // btOptimizeFont
      // 
      this.btOptimizeFont.Location = new System.Drawing.Point(8, 73);
      this.btOptimizeFont.Name = "btOptimizeFont";
      this.btOptimizeFont.Size = new System.Drawing.Size(98, 28);
      this.btOptimizeFont.TabIndex = 5;
      this.btOptimizeFont.Text = "Optimize Font";
      this.btOptimizeFont.UseVisualStyleBackColor = true;
      this.btOptimizeFont.Click += new System.EventHandler(this.btOptimizeFont_Click);
      // 
      // btSaveFontAs
      // 
      this.btSaveFontAs.Location = new System.Drawing.Point(429, 229);
      this.btSaveFontAs.Name = "btSaveFontAs";
      this.btSaveFontAs.Size = new System.Drawing.Size(98, 28);
      this.btSaveFontAs.TabIndex = 13;
      this.btSaveFontAs.Text = "Save Font...";
      this.btSaveFontAs.UseVisualStyleBackColor = true;
      this.btSaveFontAs.Click += new System.EventHandler(this.btSaveFontAs_Click);
      // 
      // cbxRemoveTop
      // 
      this.cbxRemoveTop.AutoSize = true;
      this.cbxRemoveTop.Checked = true;
      this.cbxRemoveTop.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbxRemoveTop.Location = new System.Drawing.Point(187, 16);
      this.cbxRemoveTop.Name = "cbxRemoveTop";
      this.cbxRemoveTop.Size = new System.Drawing.Size(88, 17);
      this.cbxRemoveTop.TabIndex = 8;
      this.cbxRemoveTop.Text = "Remove Top";
      this.cbxRemoveTop.UseVisualStyleBackColor = true;
      // 
      // cbxRemoveBottom
      // 
      this.cbxRemoveBottom.AutoSize = true;
      this.cbxRemoveBottom.Checked = true;
      this.cbxRemoveBottom.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbxRemoveBottom.Location = new System.Drawing.Point(187, 39);
      this.cbxRemoveBottom.Name = "cbxRemoveBottom";
      this.cbxRemoveBottom.Size = new System.Drawing.Size(107, 17);
      this.cbxRemoveBottom.TabIndex = 9;
      this.cbxRemoveBottom.Text = "Remove Bottom";
      this.cbxRemoveBottom.UseVisualStyleBackColor = true;
      // 
      // txFinalSize
      // 
      this.txFinalSize.Location = new System.Drawing.Point(223, 77);
      this.txFinalSize.Name = "txFinalSize";
      this.txFinalSize.ReadOnly = true;
      this.txFinalSize.Size = new System.Drawing.Size(58, 22);
      this.txFinalSize.TabIndex = 13;
      this.txFinalSize.TabStop = false;
      this.txFinalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(126, 81);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(91, 13);
      this.label6.TabIndex = 7;
      this.label6.Text = "Final Size (WxH):";
      // 
      // txFontName
      // 
      this.txFontName.BackColor = System.Drawing.SystemColors.Control;
      this.txFontName.Location = new System.Drawing.Point(533, 234);
      this.txFontName.Name = "txFontName";
      this.txFontName.ReadOnly = true;
      this.txFontName.Size = new System.Drawing.Size(503, 22);
      this.txFontName.TabIndex = 3;
      this.txFontName.TabStop = false;
      this.txFontName.TextChanged += new System.EventHandler(this.txMyText_TextChanged);
      // 
      // btToTest
      // 
      this.btToTest.Location = new System.Drawing.Point(334, 15);
      this.btToTest.Name = "btToTest";
      this.btToTest.Size = new System.Drawing.Size(61, 22);
      this.btToTest.TabIndex = 2;
      this.btToTest.Text = "to Test";
      this.btToTest.UseVisualStyleBackColor = true;
      this.btToTest.Click += new System.EventHandler(this.btToTest_Click);
      // 
      // hScrTargetHeight
      // 
      this.hScrTargetHeight.LargeChange = 1;
      this.hScrTargetHeight.Location = new System.Drawing.Point(123, 38);
      this.hScrTargetHeight.Minimum = 2;
      this.hScrTargetHeight.Name = "hScrTargetHeight";
      this.hScrTargetHeight.Size = new System.Drawing.Size(48, 19);
      this.hScrTargetHeight.TabIndex = 3;
      this.hScrTargetHeight.Value = 16;
      this.hScrTargetHeight.ValueChanged += new System.EventHandler(this.hScrTargetHeight_ValueChanged);
      // 
      // btClearTest
      // 
      this.btClearTest.Location = new System.Drawing.Point(334, 69);
      this.btClearTest.Name = "btClearTest";
      this.btClearTest.Size = new System.Drawing.Size(61, 22);
      this.btClearTest.TabIndex = 7;
      this.btClearTest.Text = "Clear";
      this.btClearTest.UseVisualStyleBackColor = true;
      this.btClearTest.Click += new System.EventHandler(this.btClearTest_Click);
      // 
      // sfDlg
      // 
      this.sfDlg.DefaultExt = "h";
      this.sfDlg.Filter = "Header files|*.h|All files|*.*";
      this.sfDlg.SupportMultiDottedExtensions = true;
      this.sfDlg.Title = "Save Font Header File";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lblDirty);
      this.groupBox1.Controls.Add(this.rbTrimMinimum);
      this.groupBox1.Controls.Add(this.rbTrimMono);
      this.groupBox1.Controls.Add(this.rbNoTrim);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.hScrTargetHeight);
      this.groupBox1.Controls.Add(this.txTargetSize);
      this.groupBox1.Controls.Add(this.btNewFont);
      this.groupBox1.Controls.Add(this.btOptimizeFont);
      this.groupBox1.Controls.Add(this.txFinalSize);
      this.groupBox1.Controls.Add(this.cbxRemoveTop);
      this.groupBox1.Controls.Add(this.cbxRemoveBottom);
      this.groupBox1.Location = new System.Drawing.Point(12, 149);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(409, 107);
      this.groupBox1.TabIndex = 14;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Optimize Font";
      // 
      // rbTrimMinimum
      // 
      this.rbTrimMinimum.AutoSize = true;
      this.rbTrimMinimum.Checked = true;
      this.rbTrimMinimum.Location = new System.Drawing.Point(304, 62);
      this.rbTrimMinimum.Name = "rbTrimMinimum";
      this.rbTrimMinimum.Size = new System.Drawing.Size(97, 17);
      this.rbTrimMinimum.TabIndex = 12;
      this.rbTrimMinimum.TabStop = true;
      this.rbTrimMinimum.Text = "Trim Minimum";
      this.rbTrimMinimum.UseVisualStyleBackColor = true;
      // 
      // rbTrimMono
      // 
      this.rbTrimMono.AutoSize = true;
      this.rbTrimMono.Location = new System.Drawing.Point(304, 39);
      this.rbTrimMono.Name = "rbTrimMono";
      this.rbTrimMono.Size = new System.Drawing.Size(80, 17);
      this.rbTrimMono.TabIndex = 11;
      this.rbTrimMono.Text = "Trim Mono";
      this.rbTrimMono.UseVisualStyleBackColor = true;
      // 
      // rbNoTrim
      // 
      this.rbNoTrim.AutoSize = true;
      this.rbNoTrim.Location = new System.Drawing.Point(304, 16);
      this.rbNoTrim.Name = "rbNoTrim";
      this.rbNoTrim.Size = new System.Drawing.Size(64, 17);
      this.rbNoTrim.TabIndex = 10;
      this.rbNoTrim.Text = "No Trim";
      this.rbNoTrim.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(313, 82);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(80, 13);
      this.label7.TabIndex = 7;
      this.label7.Text = ".. while saving";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.txMyText);
      this.groupBox2.Controls.Add(this.btClearTest);
      this.groupBox2.Controls.Add(this.txFirstChar);
      this.groupBox2.Controls.Add(this.btToTest);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.txLastChar);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.txCharCount);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.txLastCharASC);
      this.groupBox2.Controls.Add(this.txFirstCharASC);
      this.groupBox2.Location = new System.Drawing.Point(12, 33);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(409, 100);
      this.groupBox2.TabIndex = 15;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Character Set Selection";
      // 
      // txTxFont
      // 
      this.txTxFont.BackColor = System.Drawing.Color.White;
      this.txTxFont.Location = new System.Drawing.Point(12, 263);
      this.txTxFont.Multiline = true;
      this.txTxFont.Name = "txTxFont";
      this.txTxFont.ReadOnly = true;
      this.txTxFont.Size = new System.Drawing.Size(1033, 227);
      this.txTxFont.TabIndex = 16;
      this.txTxFont.TabStop = false;
      // 
      // comFileFormat
      // 
      this.comFileFormat.FormattingEnabled = true;
      this.comFileFormat.Location = new System.Drawing.Point(533, 207);
      this.comFileFormat.Name = "comFileFormat";
      this.comFileFormat.Size = new System.Drawing.Size(289, 21);
      this.comFileFormat.TabIndex = 17;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(434, 210);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(93, 13);
      this.label8.TabIndex = 18;
      this.label8.Text = "Save File Format:";
      // 
      // menu
      // 
      this.menu.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
      this.menu.Location = new System.Drawing.Point(0, 0);
      this.menu.Name = "menu";
      this.menu.Size = new System.Drawing.Size(1057, 24);
      this.menu.TabIndex = 19;
      this.menu.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFontToolStripMenuItem,
            this.addFontfilesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // loadFontToolStripMenuItem
      // 
      this.loadFontToolStripMenuItem.Name = "loadFontToolStripMenuItem";
      this.loadFontToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
      this.loadFontToolStripMenuItem.Text = "Load Font...";
      this.loadFontToolStripMenuItem.Click += new System.EventHandler(this.loadFontToolStripMenuItem_Click);
      // 
      // addFontfilesToolStripMenuItem
      // 
      this.addFontfilesToolStripMenuItem.Name = "addFontfilesToolStripMenuItem";
      this.addFontfilesToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
      this.addFontfilesToolStripMenuItem.Text = "Add Fontfiles...";
      this.addFontfilesToolStripMenuItem.Click += new System.EventHandler(this.addFontfilesToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 22);
      this.toolStripMenuItem1.Text = "Add Font Directory...";
      this.toolStripMenuItem1.Click += new System.EventHandler(this.AddFontDirectorytoolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
      this.saveAsToolStripMenuItem.Text = "Save as...";
      this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // ofDlg
      // 
      this.ofDlg.DefaultExt = "ttf";
      this.ofDlg.Filter = "Font files|*.ttf|All files|*.*";
      this.ofDlg.Multiselect = true;
      this.ofDlg.SupportMultiDottedExtensions = true;
      this.ofDlg.Title = "Load private font files";
      // 
      // fbDlg
      // 
      this.fbDlg.ShowNewFolderButton = false;
      // 
      // lblDirty
      // 
      this.lblDirty.AutoSize = true;
      this.lblDirty.BackColor = System.Drawing.Color.OrangeRed;
      this.lblDirty.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDirty.Location = new System.Drawing.Point(108, 81);
      this.lblDirty.Name = "lblDirty";
      this.lblDirty.Size = new System.Drawing.Size(11, 13);
      this.lblDirty.TabIndex = 16;
      this.lblDirty.Text = "!";
      // 
      // FontCreator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1057, 498);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.comFileFormat);
      this.Controls.Add(this.txTxFont);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btSaveFontAs);
      this.Controls.Add(this.txFontName);
      this.Controls.Add(this.lbFontProps);
      this.Controls.Add(this.menu);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menu;
      this.Name = "FontCreator";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "GLCD FontCreator";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.menu.ResumeLayout(false);
      this.menu.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.FontDialog fDlg;
    private System.Windows.Forms.Button btNewFont;
    private System.Windows.Forms.ListBox lbFontProps;
    private System.Windows.Forms.TextBox txMyText;
    private System.Windows.Forms.TextBox txFirstChar;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txLastChar;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txCharCount;
    private System.Windows.Forms.TextBox txFirstCharASC;
    private System.Windows.Forms.TextBox txLastCharASC;
    private System.Windows.Forms.TextBox txTargetSize;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btOptimizeFont;
    private System.Windows.Forms.Button btSaveFontAs;
    private System.Windows.Forms.CheckBox cbxRemoveTop;
    private System.Windows.Forms.CheckBox cbxRemoveBottom;
    private System.Windows.Forms.TextBox txFinalSize;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txFontName;
    private System.Windows.Forms.Button btToTest;
    private System.Windows.Forms.HScrollBar hScrTargetHeight;
    private System.Windows.Forms.Button btClearTest;
    private System.Windows.Forms.SaveFileDialog sfDlg;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rbTrimMinimum;
    private System.Windows.Forms.RadioButton rbTrimMono;
    private System.Windows.Forms.RadioButton rbNoTrim;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txTxFont;
    private System.Windows.Forms.ComboBox comFileFormat;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.MenuStrip menu;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem loadFontToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addFontfilesToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog ofDlg;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.FolderBrowserDialog fbDlg;
    private System.Windows.Forms.Label lblDirty;
  }
}

