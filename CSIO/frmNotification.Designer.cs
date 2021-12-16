
namespace CSIO
{
    partial class frmNotification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelNotiDetail = new System.Windows.Forms.Panel();
            this.btnNotiRefresh = new System.Windows.Forms.Button();
            this.btnNotiClose = new System.Windows.Forms.Button();
            this.LnkSyncDist = new System.Windows.Forms.LinkLabel();
            this.dgvNoti = new System.Windows.Forms.DataGridView();
            this.col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelNoti = new System.Windows.Forms.Panel();
            this.labelNotiCount = new System.Windows.Forms.Label();
            this.lalbeNotiBack = new System.Windows.Forms.Label();
            this.panelSyncDist = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCloseSyncDist = new System.Windows.Forms.Button();
            this.dgvSyncDist = new System.Windows.Forms.DataGridView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panelNotiDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoti)).BeginInit();
            this.panelNoti.SuspendLayout();
            this.panelSyncDist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSyncDist)).BeginInit();
            this.SuspendLayout();
            // 
            // panelNotiDetail
            // 
            this.panelNotiDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNotiDetail.BackColor = System.Drawing.Color.LightCyan;
            this.panelNotiDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNotiDetail.Controls.Add(this.linkLabel1);
            this.panelNotiDetail.Controls.Add(this.btnNotiRefresh);
            this.panelNotiDetail.Controls.Add(this.btnNotiClose);
            this.panelNotiDetail.Controls.Add(this.LnkSyncDist);
            this.panelNotiDetail.Controls.Add(this.dgvNoti);
            this.panelNotiDetail.Controls.Add(this.label3);
            this.panelNotiDetail.Controls.Add(this.labelTitle);
            this.panelNotiDetail.Location = new System.Drawing.Point(4, 91);
            this.panelNotiDetail.Name = "panelNotiDetail";
            this.panelNotiDetail.Size = new System.Drawing.Size(567, 432);
            this.panelNotiDetail.TabIndex = 20;
            // 
            // btnNotiRefresh
            // 
            this.btnNotiRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotiRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNotiRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNotiRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnNotiRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnNotiRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNotiRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotiRefresh.Font = new System.Drawing.Font("Noto Sans", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotiRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnNotiRefresh.Image = global::CSIO.Properties.Resources.refresh4_24;
            this.btnNotiRefresh.Location = new System.Drawing.Point(496, 4);
            this.btnNotiRefresh.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.btnNotiRefresh.Name = "btnNotiRefresh";
            this.btnNotiRefresh.Size = new System.Drawing.Size(30, 29);
            this.btnNotiRefresh.TabIndex = 146;
            this.btnNotiRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNotiRefresh.UseVisualStyleBackColor = false;
            this.btnNotiRefresh.Click += new System.EventHandler(this.labelNotiRefresh_Click);
            // 
            // btnNotiClose
            // 
            this.btnNotiClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotiClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNotiClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNotiClose.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnNotiClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnNotiClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNotiClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotiClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotiClose.ForeColor = System.Drawing.Color.Red;
            this.btnNotiClose.Location = new System.Drawing.Point(531, 4);
            this.btnNotiClose.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.btnNotiClose.Name = "btnNotiClose";
            this.btnNotiClose.Size = new System.Drawing.Size(30, 29);
            this.btnNotiClose.TabIndex = 145;
            this.btnNotiClose.Text = "X";
            this.btnNotiClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNotiClose.UseVisualStyleBackColor = false;
            this.btnNotiClose.Click += new System.EventHandler(this.labelNotiClose_Click);
            // 
            // LnkSyncDist
            // 
            this.LnkSyncDist.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.LnkSyncDist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LnkSyncDist.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LnkSyncDist.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.LnkSyncDist.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.LnkSyncDist.Location = new System.Drawing.Point(123, 311);
            this.LnkSyncDist.Name = "LnkSyncDist";
            this.LnkSyncDist.Size = new System.Drawing.Size(293, 35);
            this.LnkSyncDist.TabIndex = 6;
            this.LnkSyncDist.TabStop = true;
            this.LnkSyncDist.Text = "जिल्ला अनुसार सिन्क्रोनाइज विवरण हेर्नुहोस्";
            this.LnkSyncDist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LnkSyncDist.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkSyncDist_LinkClicked);
            // 
            // dgvNoti
            // 
            this.dgvNoti.AllowUserToAddRows = false;
            this.dgvNoti.AllowUserToDeleteRows = false;
            this.dgvNoti.AllowUserToOrderColumns = true;
            this.dgvNoti.AllowUserToResizeColumns = false;
            this.dgvNoti.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvNoti.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNoti.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNoti.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNoti.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvNoti.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dgvNoti.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNoti.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvNoti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNoti.ColumnHeadersVisible = false;
            this.dgvNoti.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1,
            this.Col2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNoti.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNoti.Location = new System.Drawing.Point(5, 38);
            this.dgvNoti.Name = "dgvNoti";
            this.dgvNoti.ReadOnly = true;
            this.dgvNoti.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvNoti.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNoti.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvNoti.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNoti.Size = new System.Drawing.Size(557, 254);
            this.dgvNoti.TabIndex = 5;
            this.dgvNoti.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNoti_CellContentClick);
            this.dgvNoti.Leave += new System.EventHandler(this.dgvNoti_Leave);
            // 
            // col1
            // 
            this.col1.HeaderText = "Column1";
            this.col1.Name = "col1";
            this.col1.ReadOnly = true;
            // 
            // Col2
            // 
            this.Col2.HeaderText = "Col2";
            this.Col2.Name = "Col2";
            this.Col2.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.PaleTurquoise;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(0, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(565, 10);
            this.label3.TabIndex = 2;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.PaleTurquoise;
            this.labelTitle.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTitle.Location = new System.Drawing.Point(4, 4);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelTitle.Size = new System.Drawing.Size(486, 28);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "सूचना (Notification)";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelNoti
            // 
            this.panelNoti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNoti.BackColor = System.Drawing.Color.Honeydew;
            this.panelNoti.Controls.Add(this.labelNotiCount);
            this.panelNoti.Controls.Add(this.lalbeNotiBack);
            this.panelNoti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelNoti.Location = new System.Drawing.Point(499, 12);
            this.panelNoti.Name = "panelNoti";
            this.panelNoti.Size = new System.Drawing.Size(71, 73);
            this.panelNoti.TabIndex = 19;
            this.panelNoti.Click += new System.EventHandler(this.panelNoti_Click);
            // 
            // labelNotiCount
            // 
            this.labelNotiCount.BackColor = System.Drawing.Color.Red;
            this.labelNotiCount.Font = new System.Drawing.Font("Noto Sans", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelNotiCount.ForeColor = System.Drawing.Color.White;
            this.labelNotiCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelNotiCount.Location = new System.Drawing.Point(30, 10);
            this.labelNotiCount.Name = "labelNotiCount";
            this.labelNotiCount.Size = new System.Drawing.Size(35, 29);
            this.labelNotiCount.TabIndex = 16;
            this.labelNotiCount.Text = "९९९";
            this.labelNotiCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNotiCount.Click += new System.EventHandler(this.panelNoti_Click);
            // 
            // lalbeNotiBack
            // 
            this.lalbeNotiBack.BackColor = System.Drawing.Color.Transparent;
            this.lalbeNotiBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 80.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lalbeNotiBack.ForeColor = System.Drawing.Color.Red;
            this.lalbeNotiBack.Image = global::CSIO.Properties.Resources.bell_icon_64;
            this.lalbeNotiBack.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lalbeNotiBack.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lalbeNotiBack.Location = new System.Drawing.Point(-3, -43);
            this.lalbeNotiBack.Name = "lalbeNotiBack";
            this.lalbeNotiBack.Size = new System.Drawing.Size(113, 112);
            this.lalbeNotiBack.TabIndex = 15;
            this.lalbeNotiBack.Text = "●";
            this.lalbeNotiBack.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lalbeNotiBack.Click += new System.EventHandler(this.panelNoti_Click);
            // 
            // panelSyncDist
            // 
            this.panelSyncDist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSyncDist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelSyncDist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSyncDist.Controls.Add(this.label1);
            this.panelSyncDist.Controls.Add(this.btnCloseSyncDist);
            this.panelSyncDist.Controls.Add(this.dgvSyncDist);
            this.panelSyncDist.Location = new System.Drawing.Point(4, 93);
            this.panelSyncDist.Name = "panelSyncDist";
            this.panelSyncDist.Size = new System.Drawing.Size(567, 345);
            this.panelSyncDist.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.label1.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(527, 28);
            this.label1.TabIndex = 147;
            this.label1.Text = "जिल्लाअनुसार सिन्क्रोनाइज र सेटिङ अपडेट विवरण";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnCloseSyncDist
            // 
            this.btnCloseSyncDist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseSyncDist.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCloseSyncDist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCloseSyncDist.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnCloseSyncDist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnCloseSyncDist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCloseSyncDist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseSyncDist.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseSyncDist.ForeColor = System.Drawing.Color.Red;
            this.btnCloseSyncDist.Location = new System.Drawing.Point(536, 2);
            this.btnCloseSyncDist.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.btnCloseSyncDist.Name = "btnCloseSyncDist";
            this.btnCloseSyncDist.Size = new System.Drawing.Size(25, 27);
            this.btnCloseSyncDist.TabIndex = 146;
            this.btnCloseSyncDist.Text = "X";
            this.btnCloseSyncDist.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCloseSyncDist.UseVisualStyleBackColor = false;
            this.btnCloseSyncDist.Click += new System.EventHandler(this.btnCloseSyncDist_Click);
            // 
            // dgvSyncDist
            // 
            this.dgvSyncDist.AllowUserToAddRows = false;
            this.dgvSyncDist.AllowUserToDeleteRows = false;
            this.dgvSyncDist.AllowUserToResizeColumns = false;
            this.dgvSyncDist.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvSyncDist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSyncDist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSyncDist.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSyncDist.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dgvSyncDist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSyncDist.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSyncDist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSyncDist.ColumnHeadersHeight = 30;
            this.dgvSyncDist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Noto Sans", 11F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSyncDist.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSyncDist.EnableHeadersVisualStyles = false;
            this.dgvSyncDist.Location = new System.Drawing.Point(3, 33);
            this.dgvSyncDist.Name = "dgvSyncDist";
            this.dgvSyncDist.ReadOnly = true;
            this.dgvSyncDist.RowHeadersVisible = false;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSyncDist.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSyncDist.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Noto Sans", 11F);
            this.dgvSyncDist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSyncDist.Size = new System.Drawing.Size(559, 305);
            this.dgvSyncDist.TabIndex = 5;
            this.dgvSyncDist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSyncDist_CellContentClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.linkLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkLabel1.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.linkLabel1.Location = new System.Drawing.Point(123, 349);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(293, 35);
            this.linkLabel1.TabIndex = 147;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "पूर्ण सिन्‌क्रोनाइज गर्नुहोस";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // frmNotification
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(575, 522);
            this.ControlBox = false;
            this.Controls.Add(this.panelNoti);
            this.Controls.Add(this.panelNotiDetail);
            this.Controls.Add(this.panelSyncDist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNotification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form5";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.frmNotification_Load);
            this.Leave += new System.EventHandler(this.frmNotification_Leave);
            this.panelNotiDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoti)).EndInit();
            this.panelNoti.ResumeLayout(false);
            this.panelSyncDist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSyncDist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNotiDetail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelNoti;
        private System.Windows.Forms.Label labelNotiCount;
        private System.Windows.Forms.Label lalbeNotiBack;
        private System.Windows.Forms.DataGridView dgvNoti;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1;
        private System.Windows.Forms.DataGridViewButtonColumn Col2;
        private System.Windows.Forms.Button btnNotiClose;
        private System.Windows.Forms.Button btnNotiRefresh;
        private System.Windows.Forms.Panel panelSyncDist;
        private System.Windows.Forms.DataGridView dgvSyncDist;
        private System.Windows.Forms.LinkLabel LnkSyncDist;
        private System.Windows.Forms.Button btnCloseSyncDist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}