﻿using System;
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

        private static string GetDllName()
        {
            string name = "VoicemeeterRemote";
            if (Environment.Is64BitProcess)
            {
                name += "64";
            }
            return name + ".dll";
        }

        /// <exception cref="DirectoryNotFoundException">Thrown when cannot find Voicemeeter registry key</exception>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="PathTooLongException"/>
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
