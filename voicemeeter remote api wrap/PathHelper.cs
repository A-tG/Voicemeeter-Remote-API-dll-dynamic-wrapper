using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace AtgDev.Voicemeeter.Utils
{
    public static class PathHelper
    {
        private const string VmKey = "VB:Voicemeeter {17359A74-1236-5467}";
        private const string regkeyHead = @"HKEY_LOCAL_MACHINE\SOFTWARE\";
        private const string regKeyTail = @"Microsoft\Windows\CurrentVersion\Uninstall\" + VmKey;
        private const string regKeyMiddle = @"WOW6432Node\";
        private const string valueName = "UninstallString";
        private const string VmSubPath = @"VB\Voicemeeter";

        private static string GetDllName()
        {
            string name = "VoicemeeterRemote";
            if (Environment.Is64BitProcess)
            {
                name += "64";
            }
            return name + ".dll";
        }

        private static string GetProgramFolderFromEnvVars()
        {
            var envVars = new string[] { "ProgramFiles(x86)", "ProgramFiles", "ProgramW6432" };
            foreach (var v in envVars)
            {
                string pfPath = Environment.GetEnvironmentVariable(v);
                if (string.IsNullOrWhiteSpace(pfPath)) continue;

                var vmPath = Path.Combine(pfPath, VmSubPath);
                if (Directory.Exists(vmPath)) return vmPath;
            }
            return "";
        }

        /// <exception cref="DirectoryNotFoundException">Thrown when cannot find Voicemeeter folder from registry or in Program Files</exception>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="PathTooLongException"/>
        public static string GetProgramFolder()
        {
            var path = GetProgramFolderFromEnvVars();
            if (!string.IsNullOrWhiteSpace(path)) return path;

            var regKey = regkeyHead + regKeyTail;
            var result = Registry.GetValue(regKey, valueName, null);
            if (result != null) return Path.GetDirectoryName((string)result);

            // try to search in WOW6432Node node
            regKey = regkeyHead + regKeyMiddle + regKeyTail;
            result = Registry.GetValue(regKey, valueName, null);
            if (result != null) return Path.GetDirectoryName((string)result);

            throw new DirectoryNotFoundException($"Error reading registry path: {regKey}\nAnd unable to find Voicemeeter in Program Files");
        }

#if (NET5_0_OR_GREATER || NETCOREAPP3_0_OR_GREATER)
        /// <exception cref="DirectoryNotFoundException">Thrown when cannot find Voicemeeter registry key</exception>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="PlatformNotSupportedException">Thrown when cannot get API's dll on current platform (OS)</exception>
        public static string GetDllPath()
        {
            var result = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                result = Path.Combine(GetProgramFolder(), GetDllName());
            } else
            {
                throw new PlatformNotSupportedException("Cannot get Voicemeeter API dll path on current OS");
            }
            return result;
        }
#else
        /// <exception cref="DirectoryNotFoundException">Thrown when cannot find Voicemeeter registry key</exception>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="PathTooLongException"/>
        public static string GetDllPath()
        {
            return Path.Combine(GetProgramFolder(), GetDllName());
        }
#endif

        public static bool TryGetDllPath(ref string path)
        {
            var result = false;
            try
            {
                path = GetDllPath();
                result = true;
            } catch {}
            return result;
        }

        public static bool TryGetProgramFolder(ref string path)
        {
            var result = false;
            try
            {
                path = GetProgramFolder();
                result = true;
            } catch { }
            return result;
        }
    }
}
