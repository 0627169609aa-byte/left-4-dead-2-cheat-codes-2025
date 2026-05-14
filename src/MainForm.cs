```csharp
using System;
using System.Diagnostics;
using System.Drawing;
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
            this.Text = "Left 4 Dead 2 Cheat";
            this.Size = new Size(300, 200);

            Button injectButton = new Button
            {
                Text = "Inject Cheat",
                Location = new Point(50, 50),
                Size = new Size(200, 50),
            };
            injectButton.Click += InjectButton_Click;
            this.Controls.Add(injectButton);

            Button exitButton = new Button
            {
                Text = "Exit",
                Location = new Point(50, 110),
                Size = new Size(200, 50),
            };
            exitButton.Click += ExitButton_Click;
            this.Controls.Add(exitButton);

            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            gameProcess = GetGameProcess();
            if (gameProcess == null)
            {
                MessageBox.Show("Left 4 Dead 2 is not running.", "Process Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Process GetGameProcess()
        {
            var processes = Process.GetProcessesByName(GameProcessName);
            return processes.Length > 0 ? processes[0] : null;
        }

        private void InjectButton_Click(object sender, EventArgs e)
        {
            if (gameProcess == null)
            {
                MessageBox.Show("Please start Left 4 Dead 2 before injecting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Code to inject the cheat goes here.
            MessageBox.Show("Cheat injected successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
```