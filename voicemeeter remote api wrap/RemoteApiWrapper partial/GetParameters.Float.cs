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
        /// <inheritdoc cref="CheckAndGetParameterNameLength(string)" path="/exception"/>
        unsafe public Int32 GetParameter(string paramName, out Single val)
        {
            var len = CheckAndGetParameterNameLength(paramName);

            byte* paramNameBuff = stackalloc byte[len + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            // (!)For some reason casting to IntPtr variant of function is always bit faster than just unsafe pointer
            return GetParameter((IntPtr)paramNameBuff, out val);
        }

        /// <inheritdoc cref="GetParameter(IntPtr, IntPtr)"/>
        /// <inheritdoc cref="GetParameter(string, out Single)"/>
        public Int32 GetParameter(IntPtr paramNamePtr, out Single val)
        {
            return m_getParameterFloat(paramNamePtr, out val);
        }
    }
}
