using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitSetParameters()
        {
            m_setParameterFloat = GetReadyDelegate<VBVMR_SetParameterFloat>();
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        // VBVMR_SetParameterFloat(char * szParamName, float Value);
        private delegate Int32 VBVMR_SetParameterFloat([MarshalAs(UnmanagedType.LPStr)] string paramName, Single val);
        private VBVMR_SetParameterFloat m_setParameterFloat;
        /// <summary>
        ///     Set a single parameter.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="val">The variable containing the new value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        public Int32 SetParameterFloat(string paramName, Single val)
        {
            return m_setParameterFloat(paramName, val);
        }

        // VBVMR_SetParameterStringA()

        // VBVMR_SetParameters()
    }
}
