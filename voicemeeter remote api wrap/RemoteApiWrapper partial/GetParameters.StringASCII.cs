using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_GetParameterStringA(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_GetParameterStringA m_getParameterStringA;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="strVal">The string variable receiving the wanted value. (ASCII)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        /// <exception cref="ArgumentException">if paramName length more than 512 (to limit stack allocation)</exception>
        unsafe public Int32 Legacy_GetParameter(string paramName, out string strVal)
        {
            var len = paramName.Length;
            if (len > 512) throw new ArgumentException("parameter name's length must not exceed 512");

            byte* paramNameBuff = stackalloc byte[len + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            char* strValBuff = stackalloc char[512];

            var res = m_getParameterStringA((IntPtr)paramNameBuff, (IntPtr)strValBuff);
            strVal = new string(strValBuff);
            return res;
        }
    }
}