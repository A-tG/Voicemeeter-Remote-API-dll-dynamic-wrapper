using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitDevicesEnumerator()
        {
            GetReadyDelegate(ref m_output_getDeviceNumber);
            GetReadyDelegate(ref m_output_getDeviceDescA);
            GetReadyDelegate(ref m_output_getDeviceDescW);
            GetReadyDelegate(ref m_input_getDeviceNumber);
            GetReadyDelegate(ref m_input_getDeviceDescA);
            GetReadyDelegate(ref m_input_getDeviceDescW);
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

        private delegate Int32 VBVMR_Output_GetDeviceDescA(Int32 index, out Int32 type, IntPtr deviceNamePtr, IntPtr hardwareIdPtr);
        private VBVMR_Output_GetDeviceDescA m_output_getDeviceDescA;
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
            // 256 characters minimum according to DLL documentation
            const int len = 256;
            var deviceNamePtr = Marshal.AllocHGlobal(len);
            var hardwareIdPtr = Marshal.AllocHGlobal(len);

            var resp = m_output_getDeviceDescA(index, out type, deviceNamePtr, hardwareIdPtr);
            deviceName = Marshal.PtrToStringAnsi(deviceNamePtr) ?? "";
            hardwareID = Marshal.PtrToStringAnsi(hardwareIdPtr) ?? "";

            Marshal.FreeHGlobal(deviceNamePtr);
            Marshal.FreeHGlobal(hardwareIdPtr);
            return resp;
        }

        private delegate Int32 VBVMR_Output_GetDeviceDescW(Int32 index, out Int32 type, IntPtr deviceNamePtr, IntPtr hardwareIdPtr);
        private VBVMR_Output_GetDeviceDescW m_output_getDeviceDescW;
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
            // 256 characters minimum according to DLL documentation
            const int len = 256 * 2;
            var deviceNamePtr = Marshal.AllocHGlobal(len);
            var hardwareIdPtr = Marshal.AllocHGlobal(len);

            var resp = m_output_getDeviceDescW(index, out type, deviceNamePtr, hardwareIdPtr);
            deviceName = Marshal.PtrToStringUni(deviceNamePtr) ?? "";
            hardwareID = Marshal.PtrToStringUni(hardwareIdPtr) ?? "";

            Marshal.FreeHGlobal(deviceNamePtr);
            Marshal.FreeHGlobal(hardwareIdPtr);
            return resp;
        }

        private delegate Int32 VBVMR_Input_GetDeviceNumber();
        private VBVMR_Input_GetDeviceNumber m_input_getDeviceNumber;
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

        private delegate Int32 VBVMR_Input_GetDeviceDescA(Int32 index, out Int32 type, IntPtr deviceNamePtr, IntPtr hardwareIdPtr);
        private VBVMR_Input_GetDeviceDescA m_input_getDeviceDescA;
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
            // 256 characters minimum according to DLL documentation
            const int len = 256;
            var deviceNamePtr = Marshal.AllocHGlobal(len);
            var hardwareIdPtr = Marshal.AllocHGlobal(len);

            var resp = m_input_getDeviceDescA(index, out type, deviceNamePtr, hardwareIdPtr);
            deviceName = Marshal.PtrToStringAnsi(deviceNamePtr) ?? "";
            hardwareID = Marshal.PtrToStringAnsi(hardwareIdPtr) ?? "";

            Marshal.FreeHGlobal(deviceNamePtr);
            Marshal.FreeHGlobal(hardwareIdPtr);

            return resp;
        }

        private delegate Int32 VBVMR_Input_GetDeviceDescW(Int32 index, out Int32 type, IntPtr deviceNamePtr, IntPtr hardwareIdPtr);
        private VBVMR_Input_GetDeviceDescW m_input_getDeviceDescW;
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
            // 256 (16 bits each) characters minimum according to DLL documentation
            const int len = 256 * 2;
            var deviceNamePtr = Marshal.AllocHGlobal(len);
            var hardwareIdPtr = Marshal.AllocHGlobal(len);

            var resp = m_input_getDeviceDescW(index, out type, deviceNamePtr, hardwareIdPtr);
            deviceName = Marshal.PtrToStringUni(deviceNamePtr) ?? "";
            hardwareID = Marshal.PtrToStringUni(hardwareIdPtr) ?? "";

            Marshal.FreeHGlobal(deviceNamePtr);
            Marshal.FreeHGlobal(hardwareIdPtr);

            return resp;
        }
    }
}