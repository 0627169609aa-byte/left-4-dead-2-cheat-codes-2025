```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4DCheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private const int TimerInterval = 1000; // 1 second
        private Timer processCheckTimer;
        private Process gameProcess;

        public MainForm()
        {
            InitializeComponent();
            InitializeProcessCheckTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Cheat App";
            this.Size = new System.Drawing.Size(400, 300);
            Button attachButton = new Button() { Text = "Attach to Game", Location = new System.Drawing.Point(50, 50) };
            attachButton.Click += AttachButton_Click;

            Button someCheatButton = new Button() { Text = "Enable Cheat", Location = new System.Drawing.Point(50, 100) };
            someCheatButton.Click += SomeCheatButton_Click;

            this.Controls.Add(attachButton);
            this.Controls.Add(someCheatButton);
        }

        private void InitializeProcessCheckTimer()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = TimerInterval;
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            gameProcess = Process.GetProcessesByName(GameProcessName).FirstOrDefault();
            if (gameProcess == null)
            {
                MessageBox.Show("Game not running!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                processCheckTimer.Stop();
            }
        }

        private void AttachButton_Click(object sender, EventArgs e)
        {
            gameProcess = Process.GetProcessesByName(GameProcessName).FirstOrDefault();
            if (gameProcess != null)
            {
                MessageBox.Show("Attached to Left 4 Dead 2!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Game not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SomeCheatButton_Click(object sender, EventArgs e)
        {
            if (gameProcess != null)
            {
                // Here you would implement code to manipulate memory or send commands to the game.
                MessageBox.Show("Cheat enabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please attach to the game first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
```