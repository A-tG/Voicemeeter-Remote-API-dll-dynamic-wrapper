using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_SetParameterStringA(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_SetParameterStringA m_setParameterStringA;
        /// <summary>
        ///     Set parameter value. (ASCII value)
        /// </summary>
        /// <param name="strVal">The variable containing the new value (ASCII).</param>
        /// <inheritdoc cref="SetParameter(string, Single)"/>
        unsafe public Int32 Legacy_SetParameter(string paramName, string strVal)
        {
            var paramLen = CheckAndGetParameterNameLength(paramName);
            var valLen = CheckGetValueLenght(strVal);

            byte* paramNameBuff = stackalloc byte[paramLen + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);
            byte* strValBuff = stackalloc byte[valLen + 1];
            CopyStrToByteStrBuff(strVal, strValBuff);

            return m_setParameterStringA((IntPtr)paramNameBuff, (IntPtr)strValBuff);
        }

        private delegate Int32 VBVMR_SetParameters(IntPtr scriptPtr);
        private VBVMR_SetParameters m_setParameters;
        /// <summary>
        ///     Set one or several parameters by a script (&lt; 48 kB). (ASCII)
        /// </summary>
        /// <inheritdoc cref="SetParameters(string)"/>
        public Int32 Legacy_SetParameters(string script)
        {
            var scriptPtr = Marshal.StringToHGlobalAnsi(script);
            var res = m_setParameters(scriptPtr);
            Marshal.FreeHGlobal(scriptPtr);

            return res;
        }
    }
}
