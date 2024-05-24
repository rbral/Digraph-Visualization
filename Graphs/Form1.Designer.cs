namespace Graphs
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbGraph = new System.Windows.Forms.TextBox();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.topSortButton = new System.Windows.Forms.Button();
            this.prillsButton = new System.Windows.Forms.Button();
            this.kruskalsButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbGraph);
            this.groupBox1.Controls.Add(this.tbDatabase);
            this.groupBox1.Controls.Add(this.tbServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(36, 33);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(752, 252);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DataBase";
            // 
            // tbGraph
            // 
            this.tbGraph.Location = new System.Drawing.Point(196, 173);
            this.tbGraph.Margin = new System.Windows.Forms.Padding(6);
            this.tbGraph.Name = "tbGraph";
            this.tbGraph.Size = new System.Drawing.Size(516, 44);
            this.tbGraph.TabIndex = 5;
            this.tbGraph.Text = "Digraph";
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(196, 115);
            this.tbDatabase.Margin = new System.Windows.Forms.Padding(6);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(516, 44);
            this.tbDatabase.TabIndex = 4;
            this.tbDatabase.Text = "Graph";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(196, 58);
            this.tbServer.Margin = new System.Windows.Forms.Padding(6);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(516, 44);
            this.tbServer.TabIndex = 3;
            this.tbServer.Text = "RIVKALAPTOP\\SQLEXPRESS01";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 179);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 37);
            this.label3.TabIndex = 2;
            this.label3.Text = "Graph";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "DataBase";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(813, 211);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 73);
            this.button1.TabIndex = 1;
            this.button1.Text = "Get";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panelGraph.Location = new System.Drawing.Point(36, 296);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(6);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(900, 820);
            this.panelGraph.TabIndex = 2;
            // 
            // topSortButton
            // 
            this.topSortButton.Location = new System.Drawing.Point(989, 296);
            this.topSortButton.Name = "topSortButton";
            this.topSortButton.Size = new System.Drawing.Size(151, 103);
            this.topSortButton.TabIndex = 3;
            this.topSortButton.Text = "Topological Sort";
            this.topSortButton.UseVisualStyleBackColor = true;
            this.topSortButton.Click += new System.EventHandler(this.topSortButton_Click);
            // 
            // prillsButton
            // 
            this.prillsButton.Location = new System.Drawing.Point(989, 699);
            this.prillsButton.Name = "prillsButton";
            this.prillsButton.Size = new System.Drawing.Size(151, 97);
            this.prillsButton.TabIndex = 5;
            this.prillsButton.Text = "Prim\'s MST";
            this.prillsButton.UseVisualStyleBackColor = true;
            this.prillsButton.Click += new System.EventHandler(this.primsMSTButton_Click);
            // 
            // kruskalsButton
            // 
            this.kruskalsButton.Location = new System.Drawing.Point(989, 845);
            this.kruskalsButton.Name = "kruskalsButton";
            this.kruskalsButton.Size = new System.Drawing.Size(151, 97);
            this.kruskalsButton.TabIndex = 6;
            this.kruskalsButton.Text = "Kruskal\'s MST";
            this.kruskalsButton.UseVisualStyleBackColor = true;
            this.kruskalsButton.Click += new System.EventHandler(this.kruskalsMSTButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1227, 1321);
            this.Controls.Add(this.kruskalsButton);
            this.Controls.Add(this.prillsButton);
            this.Controls.Add(this.topSortButton);
            this.Controls.Add(this.panelGraph);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Graph Demo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbGraph;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Button topSortButton;
        private System.Windows.Forms.Button prillsButton;
        private System.Windows.Forms.Button kruskalsButton;
    }
}

