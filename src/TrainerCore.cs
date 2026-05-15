Here is a realistic C# implementation for a `TrainerCore.cs` class specifically tailored for a Left 4 Dead 2 cheat. The class includes a simple `ProcessMemory` class with the ability to read and write float and integer values. It includes methods to attach to the Left 4 Dead 2 process and check if the game is running.

```csharp
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class TrainerCore
{
    private const string ProcessName = "left4dead2";
    private Process _process;
    private readonly ProcessMemory _processMemory;

    public TrainerCore()
    {
        _processMemory = new ProcessMemory();
    }

    public bool AttachToProcess()
    {
        _process = Process.GetProcessesByName(ProcessName).FirstOrDefault();
        if (_process == null)
        {
            return false;
        }

        _processMemory.Attach(_process);
        return true;
    }

    public bool IsGameRunning()
    {
        return Process.GetProcessesByName(ProcessName).Length > 0;
    }

    public float GetPlayerHealth()
    {
        return _processMemory.ReadFloat((IntPtr)0x00A1C24D); // Static address example for player health
    }

    public void SetPlayerHealth(float value)
    {
        _processMemory.WriteFloat((IntPtr)0x00A1C24D, value);
    }

    public int GetPlayerScore()
    {
        return _processMemory.ReadInt((IntPtr)0x00A1D45C); // Static address for player score
    }

    public void SetPlayerScore(int value)
    {
        _processMemory.WriteInt((IntPtr)0x00A1D45C, value);
    }
}

public class ProcessMemory
{
    private IntPtr _processHandle;

    public void Attach(Process process)
    {
        _processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);
    }

    public float ReadFloat(IntPtr address)
    {
        float value = 0;
        ReadProcessMemory(_processHandle, address, out value, sizeof(float), out _);
        return value;
    }

    public void WriteFloat(IntPtr address, float value)
    {
        WriteProcessMemory(_processHandle, address, ref value, sizeof(float), out _);
    }

    public int ReadInt(IntPtr address)
    {
        int value = 0;
        ReadProcessMemory(_processHandle, address, out value, sizeof(int), out _);
        return value;
    }

    public void WriteInt(IntPtr address, int value)
    {
        WriteProcessMemory(_processHandle, address, ref value, sizeof(int), out _);
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [Dll