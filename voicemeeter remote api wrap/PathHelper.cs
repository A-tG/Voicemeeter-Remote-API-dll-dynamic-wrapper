using System;
using System.IO;
using Microsoft.Win32;

namespace AtgDev.Voicemeeter.Utils
{
    static class PathHelper
    {
        private const string VmKey = "VB:Voicemeeter {17359A74-1236-5467}";
        private const string regkeyHead = @"HKEY_LOCAL_MACHINE\SOFTWARE\";
        private const string regKeyTail = @"Microsoft\Windows\CurrentVersion\Uninstall\" + VmKey;
        private const string regKeyMiddle = @"WOW6432Node\";
        private const string valueName = "UninstallString";

        private static string GetDllName()
        {
            string result = "VoicemeeterRemote";
            if (Environment.Is64BitOperatingSystem)
            {
                result += "64";
            }
            return result + ".dll";
        }

        /// <exception cref="DirectoryNotFoundException">Thrown when cannot find Voicemeeter registry key</exception>
        public static string GetProgramFolder()
        {
            var regKey = regkeyHead + regKeyTail;
            var result = Registry.GetValue(regKey, valueName, null);
            if (result == null)
            {
                // try to search in WOW6432Node node
                regKey = regkeyHead + regKeyMiddle + regKeyTail;
                result = Registry.GetValue(regKey, valueName, null);
                if (result == null)
                {
                    throw new DirectoryNotFoundException($"Error reading registry path: {regKey}");
                }
            }
            return Path.GetDirectoryName((string)result);
        }

        public static string GetDllPath()
        {
            return Path.Combine(GetProgramFolder(), GetDllName());
        }
    }
}
