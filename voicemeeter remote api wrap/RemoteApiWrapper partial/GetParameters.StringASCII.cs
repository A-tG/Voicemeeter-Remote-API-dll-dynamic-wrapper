using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_GetParameterStringA(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_GetParameterStringA m_getParameterStringA;
        /// <summary>
        ///     Get parameter value (ASCII value)
        /// </summary>
        /// <param name="strVal">The string variable receiving the wanted value. (ASCII)</param>
        /// <inheritdoc cref="GetParameter(string, out Single)"/>
#if NET5_0_OR_GREATER
        [SkipLocalsInit]
#endif
        unsafe public Int32 Legacy_GetParameter(string paramName, out string strVal)
        {
            byte* paramNameBuff = stackalloc byte[CheckAndGetParameterNameLength(paramName) + 1];
            CopyStrToAsciiBuff(paramName, paramNameBuff);

            byte* strValBuff = stackalloc byte[512];
            strValBuff[0] = 0;
            var strPtr = (IntPtr)strValBuff;

            var res = m_getParameterStringA((IntPtr)paramNameBuff, strPtr);
            strVal = Marshal.PtrToStringAnsi(strPtr);
            return res;
        }
    }
}