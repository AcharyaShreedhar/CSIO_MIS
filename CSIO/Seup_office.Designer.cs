
namespace CSIO
{
    partial class Seup_office
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
            this.province = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.district_combo = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.office_name = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.office_engname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Fax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Phone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ministry = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.department = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.faat = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // province
            // 
            this.province.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.province.Enabled = false;
            this.province.Font = new System.Drawing.Font("Kalimati", 12F);
            this.province.ForeColor = System.Drawing.Color.White;
            this.province.FormattingEnabled = true;
            this.province.Items.AddRange(new object[] {
            "प्रदेश  न. १",
            "प्रदेश न. २",
            "बाग्मती",
            "गण्डकी",
            "लुम्बिनी",
            "कर्णाली",
            "सुदुरपश्चिम"});
            this.province.Location = new System.Drawing.Point(164, 60);
            this.province.Margin = new System.Windows.Forms.Padding(6);
            this.province.Name = "province";
            this.province.Size = new System.Drawing.Size(126, 35);
            this.province.TabIndex = 50;
            this.province.SelectedIndexChanged += new System.EventHandler(this.province_SelectedIndexChanged);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.label51.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(124, 66);
            this.label51.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(37, 18);
            this.label51.TabIndex = 52;
            this.label51.Text = "प्रदेश:";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // district_combo
            // 
            this.district_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.district_combo.Font = new System.Drawing.Font("Kalimati", 12F);
            this.district_combo.FormattingEnabled = true;
            this.district_combo.Location = new System.Drawing.Point(346, 60);
            this.district_combo.Margin = new System.Windows.Forms.Padding(6);
            this.district_combo.Name = "district_combo";
            this.district_combo.Size = new System.Drawing.Size(186, 35);
            this.district_combo.TabIndex = 49;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(292, 68);
            this.label42.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(42, 18);
            this.label42.TabIndex = 51;
            this.label42.Text = "जिल्ला:";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(13, 66);
            this.name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(102, 18);
            this.name.TabIndex = 48;
            this.name.Text = "कार्यालयको ठेगाना:";
            this.name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // office_name
            // 
            this.office_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.office_name.BackColor = System.Drawing.Color.White;
            this.office_name.Font = new System.Drawing.Font("Kalimati", 12F);
            this.office_name.Location = new System.Drawing.Point(149, 197);
            this.office_name.Margin = new System.Windows.Forms.Padding(6);
            this.office_name.Name = "office_name";
            this.office_name.Size = new System.Drawing.Size(383, 34);
            this.office_name.TabIndex = 53;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label24.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(46, 205);
            this.label24.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(91, 18);
            this.label24.TabIndex = 54;
            this.label24.Text = "कार्यालयको नाम:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // office_engname
            // 
            this.office_engname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.office_engname.BackColor = System.Drawing.Color.White;
            this.office_engname.Font = new System.Drawing.Font("Kalimati", 12F);
            this.office_engname.Location = new System.Drawing.Point(149, 243);
            this.office_engname.Margin = new System.Windows.Forms.Padding(6);
            this.office_engname.Name = "office_engname";
            this.office_engname.Size = new System.Drawing.Size(383, 34);
            this.office_engname.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(-3, 251);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 18);
            this.label1.TabIndex = 56;
            this.label1.Text = "कार्यालयको नाम(अंग्रेजीमा):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Fax
            // 
            this.Fax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fax.BackColor = System.Drawing.Color.White;
            this.Fax.Font = new System.Drawing.Font("Kalimati", 12F);
            this.Fax.Location = new System.Drawing.Point(149, 335);
            this.Fax.Margin = new System.Windows.Forms.Padding(6);
            this.Fax.Name = "Fax";
            this.Fax.Size = new System.Drawing.Size(383, 34);
            this.Fax.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(77, 344);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 60;
            this.label2.Text = "फ्याक्स नं.:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Phone
            // 
            this.Phone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Phone.BackColor = System.Drawing.Color.White;
            this.Phone.Font = new System.Drawing.Font("Kalimati", 12F);
            this.Phone.Location = new System.Drawing.Point(149, 289);
            this.Phone.Margin = new System.Windows.Forms.Padding(6);
            this.Phone.Name = "Phone";
            this.Phone.Size = new System.Drawing.Size(383, 34);
            this.Phone.TabIndex = 57;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(88, 297);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 18);
            this.label3.TabIndex = 58;
            this.label3.Text = "फोन नं.:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ministry
            // 
            this.ministry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ministry.BackColor = System.Drawing.Color.White;
            this.ministry.Font = new System.Drawing.Font("Kalimati", 12F);
            this.ministry.Location = new System.Drawing.Point(149, 107);
            this.ministry.Margin = new System.Windows.Forms.Padding(6);
            this.ministry.Name = "ministry";
            this.ministry.Size = new System.Drawing.Size(383, 34);
            this.ministry.TabIndex = 63;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(85, 116);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 18);
            this.label4.TabIndex = 64;
            this.label4.Text = "मन्त्रालय:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // email
            // 
            this.email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.email.BackColor = System.Drawing.Color.White;
            this.email.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email.Location = new System.Drawing.Point(149, 379);
            this.email.Margin = new System.Windows.Forms.Padding(6);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(383, 26);
            this.email.TabIndex = 61;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(66, 387);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 18);
            this.label5.TabIndex = 62;
            this.label5.Text = "इमेल ठेगाना:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // department
            // 
            this.department.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.department.BackColor = System.Drawing.Color.White;
            this.department.Font = new System.Drawing.Font("Kalimati", 12F);
            this.department.Location = new System.Drawing.Point(149, 153);
            this.department.Margin = new System.Windows.Forms.Padding(6);
            this.department.Name = "department";
            this.department.Size = new System.Drawing.Size(383, 34);
            this.department.TabIndex = 65;
            this.department.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(69, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 18);
            this.label6.TabIndex = 66;
            this.label6.Text = "निर्देशनालय:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button12.Font = new System.Drawing.Font("Kalimati", 11.25F);
            this.button12.ForeColor = System.Drawing.Color.Black;
            this.button12.Image = global::CSIO.Properties.Resources.save_icon_24;
            this.button12.Location = new System.Drawing.Point(412, 440);
            this.button12.Margin = new System.Windows.Forms.Padding(6);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(131, 37);
            this.button12.TabIndex = 67;
            this.button12.Text = " सेभ गर्नुहोस";
            this.button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.faat);
            this.groupBox1.Controls.Add(this.department);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ministry);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.email);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Fax);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Phone);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.office_engname);
            this.groupBox1.Controls.Add(this.office_name);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.province);
            this.groupBox1.Controls.Add(this.label51);
            this.groupBox1.Controls.Add(this.district_combo);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Location = new System.Drawing.Point(11, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 429);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "कार्यालयको बिवरण";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(306, 444);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(49, 27);
            this.dataGridView1.TabIndex = 69;
            this.dataGridView1.Visible = false;
            // 
            // faat
            // 
            this.faat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.faat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faat.FormattingEnabled = true;
            this.faat.Items.AddRange(new object[] {
            "Operator",
            "Administrator"});
            this.faat.Location = new System.Drawing.Point(164, 23);
            this.faat.Name = "faat";
            this.faat.Size = new System.Drawing.Size(368, 28);
            this.faat.TabIndex = 70;
            this.faat.SelectedIndexChanged += new System.EventHandler(this.faat_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(25, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(133, 26);
            this.label15.TabIndex = 71;
            this.label15.Text = "उद्योग/बाणिज्य:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Seup_office
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(596, 483);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Kalimati", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Seup_office";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "कार्यालयको बिवरण";
            this.Load += new System.EventHandler(this.Seup_office_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox province;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ComboBox district_combo;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.TextBox office_name;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox office_engname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Fax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Phone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ministry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox department;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox faat;
        private System.Windows.Forms.Label label15;
    }
}