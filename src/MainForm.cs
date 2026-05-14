```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Left4Dead2CheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private const int RefreshInterval = 1000; // 1 second

        private Timer processCheckTimer;
        private Process gameProcess;

        public MainForm()
        {
            InitializeComponent();
            InitializeAdditionalComponents();
        }

        private void InitializeAdditionalComponents()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = RefreshInterval;
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            gameProcess = Process.GetProcessesByName(GameProcessName).FirstOrDefault();
            if (gameProcess != null)
            {
                lblStatus.Text = "Game Running";
                btnActivateCheat.Enabled = true;
            }
            else
            {
                lblStatus.Text = "Game Not Running";
                btnActivateCheat.Enabled = false;
            }
        }

        private void btnActivateCheat_Click(object sender, EventArgs e)
        {
            if (gameProcess != null)
            {
                // Insert cheat activation code here
                MessageBox.Show("Cheat Activated!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Game must be running to activate cheats.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            processCheckTimer.Stop();
            processCheckTimer.Dispose();
        }

        private void InitializeComponent()
        {
            this.lblStatus = new Label();
            this.btnActivateCheat = new Button();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 0;
            // 
            // btnActivateCheat
            // 
            this.btnActivateCheat.Location = new System.Drawing.Point(12, 35);
            this.btnActivateCheat.Name = "btnActivateCheat";
            this.btnActivateCheat.Size = new System.Drawing.Size(120, 23);
            this.btnActivateCheat.TabIndex = 1;
            this.btnActivateCheat.Text = "Activate Cheat";
            this.btnActivateCheat.UseVisualStyleBackColor = true;
            this.btnActivateCheat.Click += new EventHandler(this.btnActivateCheat_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 61);
            this.Controls.Add(this.btnActivateCheat);
            this.Controls.Add(this.lblStatus);
            this.Name = "MainForm";