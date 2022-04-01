using System;

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
        unsafe public Int32 Legacy_GetParameter(string paramName, out string strVal)
        {
            var len = CheckAndGetParameterNameLength(paramName);

            byte* paramNameBuff = stackalloc byte[len + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            char* strValBuff = stackalloc char[512];

            var res = m_getParameterStringA((IntPtr)paramNameBuff, (IntPtr)strValBuff);
            strVal = new string(strValBuff);
            return res;
        }
    }
}