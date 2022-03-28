using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_GetParameterFloat(IntPtr paramNamePtr, out Single value);
        private VBVMR_GetParameterFloat m_getParameterFloat;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="val">The variable receiving the wanted value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        /// <exception cref="ArgumentException">if paramName length more than 512 (to limit stack allocation)</exception>
        unsafe public Int32 GetParameter(string paramName, out Single val)
        {
            var len = paramName.Length;
            if (len > 512) throw new ArgumentException("parameter name's length must not exceed 512");

            byte* paramNameBuff = stackalloc byte[len + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            // (!)For some reason casting to IntPtr variant of function is always bit faster than just unsafe pointer
            return GetParameter((IntPtr)paramNameBuff, out val);
        }

        /// <summary>
        ///     Get parameter value. Alternative low-level, faster method.
        ///     Except ~20% faster execution time with preallocated paramBuffPtr
        /// </summary>
        /// <param name="paramNamePtr">Buffer pointer (null terminated ASCII) with the name of the parameter 
        /// (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="val">The variable receiving the wanted value.</param>
        /// <returns>
        ///     <inheritdoc cref="GetParameter(string, out Single)" path="/returns"/>
        /// </returns>
        public Int32 GetParameter(IntPtr paramNamePtr, out Single val)
        {
            return m_getParameterFloat(paramNamePtr, out val);
        }
    }
}
