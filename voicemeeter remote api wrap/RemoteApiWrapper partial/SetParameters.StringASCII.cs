﻿using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_SetParameterStringA(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_SetParameterStringA m_setParameterStringA;
        /// <summary>
        ///     Set parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see parameters name table)</param>
        /// <param name="strVal">The variable containing the new value (ASCII).</param>
        /// <inheritdoc cref="SetParameter(string, Single)" path="/returns"/>
        /// <inheritdoc cref="SetParameter(string, Single)" path="/exception"/>
        unsafe public Int32 Legacy_SetParameter(string paramName, string strVal)
        {
            var paramLen = CheckGetParameterNameLength(paramName);
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
        ///     Set one or several parameters by a script (&lt; 48 kB).
        /// </summary>
        /// <param name="script">
        ///     String giving the script (ASCII)<br/>                  
        ///     (script allows to change several parameters in the same time - SYNCHRO).<br/>                  
        ///     Possible Instuction separators: ',' ';' or '\n'(CR)<br/> 
        ///     EXAMPLE:<br/>
        ///     <c>                
        ///         "Strip[0].gain = -6.0<br/>                 
        ///         Strip[0].A1 = 0<br/>                      
        ///         Strip[0].B1 = 1<br/>                      
        ///         Strip[1].gain = -6.0<br/>                      
        ///         Strip[2].gain = 0.0 <br/>                     
        ///         Strip[3].name = "Skype Caller" "
        ///     </c>
        /// </param>
        /// <inheritdoc cref="SetParameters(string)" path="/returns"/>
        public Int32 Legacy_SetParameters(string script)
        {
            var scriptPtr = Marshal.StringToHGlobalAnsi(script);
            var res = m_setParameters(scriptPtr);
            Marshal.FreeHGlobal(scriptPtr);

            return res;
        }
    }
}