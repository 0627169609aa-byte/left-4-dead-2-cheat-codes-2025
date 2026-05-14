```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class TrainerCore
{
    private const string ProcessName = "left4dead2";
    private Process process;
    private IntPtr processHandle;

    // Static addresses
    private static readonly int playerHealthAddress = 0x01234567; // Replace with actual address
    private static readonly int playerAmmoAddress = 0x01234568; // Replace with actual address

    public bool AttachToProcess()
    {
        process = Process.GetProcessesByName(ProcessName).FirstOrDefault();

        if (process == null)
        {
            Console.WriteLine("Game not running.");
            return false;
        }

        processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
        return processHandle != IntPtr.Zero;
    }

    public bool IsGameRunning()
    {
        return process != null && !process.HasExited;
    }

    public float ReadFloat(int address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
        return BitConverter.ToSingle(buffer, 0);
    }

    public void WriteFloat(int address, float value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
    }

    public int ReadInt(int address)
    {
        byte[] buffer = new byte[4];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(int address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    [Flags]
    private enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF
    }
}
```

**Note:** The provided address values are placeholders and should be replaced with the actual memory addresses relevant to the game. Additionally, please ensure you comply with all