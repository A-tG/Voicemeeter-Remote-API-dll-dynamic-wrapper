using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_GetParameterStringW(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_GetParameterStringW m_getParameterStringW;
        /// <summary>
        ///     Get parameter value (UTF-16 value)
        /// </summary>
        /// <param name="strVal">The string variable receiving the wanted value. (UTF-16)</param>
        /// <inheritdoc cref="GetParameter(string, out Single)"/>
        unsafe public Int32 GetParameter(string paramName, out string strVal)
        {
            var len = CheckAndGetParameterNameLength(paramName);

            byte* paramNameBuff = stackalloc byte[len + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            return GetParameter((IntPtr)paramNameBuff, out strVal);
        }

        /// <inheritdoc cref="GetParameter(IntPtr, IntPtr)"/>
        /// <inheritdoc cref="GetParameter(string, out string)"/>
        unsafe public Int32 GetParameter(IntPtr paramNamePtr, out string strVal)
        {
            char* strValBuff = stackalloc char[512];
            var res = GetParameter(paramNamePtr, (IntPtr)strValBuff);
            strVal = new string(strValBuff);
            return res;
        }

        /// <inheritdoc cref="GetParameter(IntPtr, IntPtr)"/>
        /// <inheritdoc cref="GetParameter(string, out string)"/>
        unsafe public Int32 GetParameter(string paramName, IntPtr strValPtr)
        {
            var len = CheckAndGetParameterNameLength(paramName);

            byte* paramNameBuff = stackalloc byte[len + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            var res = GetParameter((IntPtr)paramNameBuff, strValPtr);
            return res;
        }

        /// <summary>
        ///     Alternative low-level method for pre allocated buffers. For maximum performance
        /// </summary>
        /// <param name="paramNamePtr">Buffer pointer (null terminated ASCII) with the name of the parameter 
        /// (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strValPtr">Buffer pointer receiving the wanted value, 512 size (512 * 2 bytes)</param>
        /// <inheritdoc cref="GetParameter(string, out Single)"/>
        public Int32 GetParameter(IntPtr paramNamePtr, IntPtr strValPtr)
        {
            return m_getParameterStringW(paramNamePtr, strValPtr);
        }
    }
}