using System;
using System.Runtime.CompilerServices;
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
#if NET5_0_OR_GREATER
        [SkipLocalsInit]
#endif
        unsafe public Int32 Legacy_SetParameter(string paramName, string strVal)
        {
            byte* paramNameBuff = stackalloc byte[CheckAndGetParameterNameLength(paramName) + 1];
            byte* strValBuff = stackalloc byte[CheckAndGetValueLenght(strVal) + 1];
            CopyStrToAsciiBuff(paramName, paramNameBuff);
            CopyStrToAsciiBuff(strVal, strValBuff);

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
