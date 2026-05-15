```csharp
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace L4D2CheatApp
{
    public partial class MainForm : Form
    {
        private const string GameProcessName = "left4dead2";
        private const string CheatEnabledText = "Cheat Enabled";
        private const string CheatDisabledText = "Cheat Disabled";

        private Timer processCheckTimer;
        private bool isCheatEnabled = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeProcessCheckTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Left 4 Dead 2 Cheat App";
            this.Size = new System.Drawing.Size(300, 200);
            
            Button toggleCheatButton = new Button
            {
                Text = "Enable Cheat",
                Dock = DockStyle.Fill
            };
            toggleCheatButton.Click += ToggleCheatButton_Click;

            this.Controls.Add(toggleCheatButton);
        }

        private void InitializeProcessCheckTimer()
        {
            processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every second
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ToggleCheatButton_Click(object sender, EventArgs e)
        {
            if (IsGameRunning())
            {
                isCheatEnabled = !isCheatEnabled;

                // Placeholder for actual cheat enabling/disabling logic.
                if (isCheatEnabled)
                {
                    EnableCheat();
                }
                else
                {
                    DisableCheat();
                }

                ((Button)sender).Text = isCheatEnabled ? CheatEnabledText : CheatDisabledText;
            }
            else
            {
                MessageBox.Show("Left 4 Dead 2 is not running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!IsGameRunning())
            {
                ResetCheatState();
            }
        }

        private bool IsGameRunning()
        {
            return Process.GetProcessesByName(GameProcessName).Any();
        }

        private void EnableCheat()
        {
            // Logic to enable the actual cheat
        }

        private void DisableCheat()
        {
            // Logic to disable the actual cheat
        }

        private void ResetCheatState()
        {
            isCheatEnabled = false;
            Controls.OfType<Button>().First().Text = "Enable Cheat";
        }
    }
}
```