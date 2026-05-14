Here is a basic implementation of a `TrainerCore.cs` for Left 4 Dead 2, including the `ProcessMemory` class with methods for reading and writing float and integer values. Note that this code is for educational purposes only and should not be used for cheating in games.

```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class ProcessMemory
{
    private IntPtr processHandle;
    private Process targetProcess;

    // Example static addresses (these need to be correct based on the game's memory structure)
    private const int HealthAddress = 0x00ABCD; // Replace with actual address
    private const int AmmoAddress = 0x00EFAB;  // Replace with actual address

    public bool AttachToProcess(string processName)
    {
        targetProcess = Process.GetProcessesByName(processName)[0];
        if (targetProcess == null) return false;

        processHandle = OpenProcess(ProcessAccessFlags.All, false, targetProcess.Id);
        return processHandle != IntPtr.Zero;
    }

    public bool IsGameRunning(string processName)
    {
        return Process.GetProcessesByName(processName).Length > 0;
    }

    public float ReadFloat(int address)
    {
        byte[] buffer = new byte[sizeof(float)];
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
        byte[] buffer = new byte[sizeof(int)];
        ReadProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
        return BitConverter.ToInt32(buffer, 0);
    }

    public void WriteInt(int address, int value)
    {
        byte[] buffer = BitConverter.GetBytes(value);
        WriteProcessMemory(processHandle, (IntPtr)address, buffer, buffer.Length, out _);
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    [Flags]
    private enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,