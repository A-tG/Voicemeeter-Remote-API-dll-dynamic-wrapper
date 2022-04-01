using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_SetParameterFloat(IntPtr paramNamePtr, Single val);
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
        /// <inheritdoc cref="CheckAndGetParameterNameLength(string)" path="/exception"/>
        unsafe public Int32 SetParameter(string paramName, Single val)
        {
            byte* paramNameBuff = stackalloc byte[CheckAndGetParameterNameLength(paramName) + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            return SetParameter((IntPtr)paramNameBuff, val);
        }

        /// <inheritdoc cref="SetParameter(IntPtr, IntPtr)"/>
        /// <inheritdoc cref="SetParameter(string, Single)"/>
        public Int32 SetParameter(IntPtr paramNamePtr, Single val)
        {
            return m_setParameterFloat(paramNamePtr, val);
        }
    }
}
