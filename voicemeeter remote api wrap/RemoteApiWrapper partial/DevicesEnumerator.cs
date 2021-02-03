using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitDevicesEnumerator()
        {
            m_output_getDeviceNumber = GetReadyDelegate<VBVMR_Output_GetDeviceNumber>();
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

        // long __stdcall VBVMR_Output_GetDeviceDescA(long zindex, long * nType, char * szDeviceName, char * szHardwareId);
        

        // long __stdcall VBVMR_Input_GetDeviceNumber(void);

        // long __stdcall VBVMR_Input_GetDeviceDescA(long zindex, long * nType, char * szDeviceName, char * szHardwareId);
    }
}