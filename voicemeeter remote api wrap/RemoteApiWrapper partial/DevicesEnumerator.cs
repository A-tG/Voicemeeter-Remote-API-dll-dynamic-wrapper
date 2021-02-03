﻿using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitDevicesEnumerator()
        {
            m_output_getDeviceNumber = GetReadyDelegate<VBVMR_Output_GetDeviceNumber>();
            m_output_getDeviceDescA = GetReadyDelegate<VBVMR_Output_GetDeviceDescA>();
        }

        private delegate Int32 VBVMR_Output_GetDeviceNumber();
        private VBVMR_Output_GetDeviceNumber m_output_getDeviceNumber;
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

        
        private delegate Int32 VBVMR_Output_GetDeviceDescA(
            Int32 index, 
            out Int32 type,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder deviceName,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder hardwareID
        );
        private VBVMR_Output_GetDeviceDescA m_output_getDeviceDescA;
        /// <summary>
        ///     Get name and hardware ID of the device according index
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
            var devideNameSB = new StringBuilder(256);
            var hardwareIDsb = new StringBuilder(256);
            var resp = m_output_getDeviceDescA(index, out type, devideNameSB, hardwareIDsb);
            deviceName = devideNameSB.ToString();
            hardwareID = hardwareIDsb.ToString();
            return resp;
        }

        // long __stdcall VBVMR_Input_GetDeviceNumber(void);

        // long __stdcall VBVMR_Input_GetDeviceDescA(long zindex, long * nType, char * szDeviceName, char * szHardwareId);
    }
}