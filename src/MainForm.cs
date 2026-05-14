```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2CheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private Process gameProcess;
        private Timer processCheckTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 2000; // Check every 2 seconds
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
            CheckForGameProcess();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckForGameProcess();
        }

        private void CheckForGameProcess()
        {
            gameProcess = Process.GetProcessesByName(GameProcessName).Length > 0 ? 
                          Process.GetProcessesByName(GameProcessName)[0] : null;

            if (gameProcess != null)
            {
                lblStatus.Text = "Game is running";
                btnEnableCheat.Enabled = true;
            }
            else
            {
                lblStatus.Text = "Game is not running";
                btnEnableCheat.Enabled = false;
            }
        }

        private void btnEnableCheat_Click(object sender, EventArgs e)
        {
            if (gameProcess != null)
            {
                // Code to enable cheat (implement your cheat logic here)
                MessageBox.Show("Cheat enabled for Left 4 Dead 2!");
            }
        }

        private void btnDisableCheat_Click(object sender, EventArgs e)
        {
            if (gameProcess != null)
            {
                // Code to disable cheat (implement your cheat logic here)
                MessageBox.Show("Cheat disabled.");
            }
        }

        private void InitializeComponent()
        {
            this.btnEnableCheat = new System.Windows.Forms.Button();
            this.btnDisableCheat = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnableCheat
            // 
            this.btnEnableCheat.Location = new System.Drawing.Point(12, 12);
            this.btnEnableCheat.Name = "btnEnableCheat";
            this.btnEnableCheat.Size = new System.Drawing.Size(120, 23);
            this.btnEnableCheat.TabIndex = 0;
            this.btnEnableCheat.Text = "Enable Cheat";
            this.btnEnableCheat.UseVisualStyleBackColor = true;
            this.btnEnableCheat.Click += new System.EventHandler(this.btnEnableCheat_Click);
            // 
            // btnDisableCheat
            // 
            this.btnDisableCheat.Location = new System.Drawing.Point(12, 41);
            this.btnDisableCheat.Name = "btnDisableCheat";
            this.btnDisableCheat.Size = new System.Drawing.Size(120, 23);
            this.btnDisableCheat.TabIndex = 1;
            this.btnDisableCheat.Text = "Disable Cheat";
            this.btn