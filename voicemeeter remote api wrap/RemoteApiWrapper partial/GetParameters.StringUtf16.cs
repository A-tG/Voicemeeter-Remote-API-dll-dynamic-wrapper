using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_GetParameterStringW(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_GetParameterStringW m_getParameterStringW;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strVal">The string variable receiving the wanted value. (UTF-16)</param>
        /// <returns>
        ///     <inheritdoc cref="GetParameter(string, out Single)" path="/returns"/>
        /// </returns>
        /// <exception cref="ArgumentException">if paramName length more than 512 (to limit stack allocation)</exception>
        unsafe public Int32 GetParameter(string paramName, out string strVal)
        {
            var len = CheckGetParameterNameLength(paramName);

            byte* paramNameBuff = stackalloc byte[len + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            return GetParameter((IntPtr)paramNameBuff, out strVal);
        }

        /// <summary>
        ///     Get parameter value. Alternative low-level, faster method.
        /// </summary>
        /// <param name="paramNamePtr">Buffer pointer (null terminated ASCII) with the name of the parameter 
        /// (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strVal">The string variable receiving the wanted value. (UTF-16)</param>
        /// <returns>
        ///     <inheritdoc cref="GetParameter(string, out Single)" path="/returns"/>
        /// </returns>
        unsafe public Int32 GetParameter(IntPtr paramNamePtr, out string strVal)
        {
            char* strValBuff = stackalloc char[512];
            var res = GetParameter(paramNamePtr, (IntPtr)strValBuff);
            strVal = new string(strValBuff);
            return res;
        }

        /// <summary>
        ///     Get parameter value. Alternative low-level, faster method.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strValPtr">Buffer pointer receiving the wanted value, 512 size (512 * 2 bytes)</param>
        /// <returns>
        ///     <inheritdoc cref="GetParameter(string, out Single)" path="/returns"/>
        /// </returns>
        /// <exception cref="ArgumentException">if paramName length more than 512 (to limit stack allocation)</exception>
        unsafe public Int32 GetParameter(string paramName, IntPtr strValPtr)
        {
            var len = CheckGetParameterNameLength(paramName);

            byte* paramNameBuff = stackalloc byte[len + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            var res = GetParameter((IntPtr)paramNameBuff, strValPtr);
            return res;
        }

        /// <summary>
        ///     Get parameter value. Alternative low-level, faster method.
        ///     Except 2-3x faster execution time with preallocated paramBuffPtr and strBuffPtr
        /// </summary>
        /// <param name="paramNamePtr">Buffer pointer (null terminated ASCII) with the name of the parameter 
        /// (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strValPtr">Buffer pointer receiving the wanted value, 512 size (512 * 2 bytes)</param>
        /// <returns>
        ///     <inheritdoc cref="GetParameter(string, out Single)" path="/returns"/>
        /// </returns>
        public Int32 GetParameter(IntPtr paramNamePtr, IntPtr strValPtr)
        {
            return m_getParameterStringW(paramNamePtr, strValPtr);
        }
    }
}