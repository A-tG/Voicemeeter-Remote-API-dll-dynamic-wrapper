﻿using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitDevicesEnumerator()
        {
            GetReadyDelegate(ref m_input_getDeviceNumber, "VBVMR_Input_GetDeviceNumber");
            GetReadyDelegate(ref m_output_getDeviceNumber, "VBVMR_Output_GetDeviceNumber");
            GetReadyDelegate(ref m_output_getDeviceDescA, "VBVMR_Output_GetDeviceDescA");
            GetReadyDelegate(ref m_output_getDeviceDescW, "VBVMR_Output_GetDeviceDescW");
            GetReadyDelegate(ref m_input_getDeviceDescA, "VBVMR_Input_GetDeviceDescA");
            GetReadyDelegate(ref m_input_getDeviceDescW, "VBVMR_Input_GetDeviceDescW");
        }

        private delegate Int32 Common_GetDeviceNumber();
        private Common_GetDeviceNumber m_input_getDeviceNumber, m_output_getDeviceNumber;
        /// <summary>
        ///     Get number of Audio Output Devices available on the system
        /// </summary>
        /// <returns>
        ///     Return number of devices found.
        /// </returns>
        public Int32 GetOutputDevicesNumber()
        {
            return m_output_getDeviceNumber();
        }
        /// <summary>
        ///     Get number of Audio Input Devices available on the system
        /// </summary>
        /// <returns>
        ///     Return number of devices found.
        /// </returns>
        public Int32 GetInputDevicesNumber()
        {
            return m_input_getDeviceNumber();
        }

        private delegate Int32 Common_GetDeviceDesc(Int32 index, out Int32 type, IntPtr deviceNamePtr, IntPtr hardwareIdPtr);
        private Common_GetDeviceDesc m_output_getDeviceDescA, m_output_getDeviceDescW, m_input_getDeviceDescA, m_input_getDeviceDescW;
        unsafe private Int32 Legacy_GetDeviceDescription(Int32 index, out Int32 type, out string deviceName, out string hardwareID, Common_GetDeviceDesc getDeviceFunc)
        {
            // 256 char characters minimum according to DLL documentation
            const int len = 256 + 1;
            byte* deviceNamePtr = stackalloc byte[len];
            byte* hardwareIdPtr = stackalloc byte[len];

            var resp = getDeviceFunc(index, out type, (IntPtr)deviceNamePtr, (IntPtr)hardwareIdPtr);

            deviceName = Marshal.PtrToStringAnsi((IntPtr)deviceNamePtr) ?? string.Empty;
            hardwareID = Marshal.PtrToStringAnsi((IntPtr)hardwareIdPtr) ?? string.Empty;

            return resp;
        }
        unsafe private Int32 GetDeviceDescription(Int32 index, out Int32 type, out string deviceName, out string hardwareID, Common_GetDeviceDesc getDeviceFunc)
        {
            // 256 wchar characters minimum according to DLL documentation
            const int len = 256 + 1; // bugged if addional space is not added
            char* deviceNamePtr = stackalloc char[len];
            char* hardwareIdPtr = stackalloc char[len];

            var resp = getDeviceFunc(index, out type, (IntPtr)deviceNamePtr, (IntPtr)hardwareIdPtr);
            deviceName = new string(deviceNamePtr);
            hardwareID = new string(hardwareIdPtr);

            return resp;
        }


        /// <summary>
        ///     Get name and hardware ID (ASCII) of the output device according index
        /// </summary>
        /// <param name="index">zero based index</param>
        /// <param name="type">Variable receiving the type</param>
        /// <param name="deviceName">Variable receiving the the device name</param>
        /// <param name="hardwareID">Variable receiving the the hardware ID</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        /// </returns>
        public Int32 Legacy_GetOutputDeviceDescriptor(Int32 index, out Int32 type, out string deviceName, out string hardwareID)
        {
            return Legacy_GetDeviceDescription(index, out type, out deviceName, out hardwareID, m_output_getDeviceDescA);
        }

        /// <summary>
        ///     Get name and hardware ID of the output device according index
        /// </summary>
        /// <param name="index">zero based index</param>
        /// <param name="type">Variable receiving the type</param>
        /// <param name="deviceName">Variable receiving the the device name</param>
        /// <param name="hardwareID">Variable receiving the the hardware ID</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        /// </returns>
        public Int32 GetOutputDeviceDescriptor(Int32 index, out Int32 type, out string deviceName, out string hardwareID)
        {
            return GetDeviceDescription(index, out type, out deviceName, out hardwareID, m_output_getDeviceDescW);
        }

        /// <summary>
        ///     Get name and hardware ID (ASCII) of the input device according index
        /// </summary>
        /// <param name="index">zero based index</param>
        /// <param name="type">Variable receiving the type</param>
        /// <param name="deviceName">Variable receiving the the device name</param>
        /// <param name="hardwareID">Variable receiving the the hardware ID</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        /// </returns>
        public Int32 Legacy_GetInputDeviceDescriptor(Int32 index, out Int32 type, out string deviceName, out string hardwareID)
        {
            return Legacy_GetDeviceDescription(index, out type, out deviceName, out hardwareID, m_input_getDeviceDescA);
        }

        /// <summary>
        ///     Get name and hardware ID of the input device according index
        /// </summary>
        /// <param name="index">zero based index</param>
        /// <param name="type">Variable receiving the type</param>
        /// <param name="deviceName">Variable receiving the the device name</param>
        /// <param name="hardwareID">Variable receiving the the hardware ID</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        /// </returns>
        public Int32 GetInputDeviceDescriptor(Int32 index, out Int32 type, out string deviceName, out string hardwareID)
        {
            return GetDeviceDescription(index, out type, out deviceName, out hardwareID, m_input_getDeviceDescW);
        }
    }
}