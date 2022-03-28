using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private delegate Int32 VBVMR_SetParameterStringW(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_SetParameterStringW m_setParameterStringW;
        /// <summary>
        ///     Set parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see parameters name table)</param>
        /// <param name="strVal">The variable containing the new value. (UTF-16)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        /// <exception cref="ArgumentException">if paramName or strVal length more than 512 (to limit stack allocation)</exception>
        unsafe public Int32 SetParameter(string paramName, string strVal)
        {
            var paramLen = paramName.Length;
            var valLen = strVal.Length;
            if (paramLen > 512) throw new ArgumentException("parameter name's length must not exceed 512");
            if (valLen > 512) throw new ArgumentException("value name's length must not exceed 512");

            byte* paramNameBuff = stackalloc byte[paramLen + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            return SetParameter((IntPtr)paramNameBuff, strVal);
        }

        /// <summary>
        ///     Set parameter value.
        /// </summary>
        /// <param name="paramNamePtr">Pointer to the string (null terminated ASCII) with the name of the parameter (see parameters name table)</param>
        /// <param name="strVal">The variable containing the new value. (UTF-16)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        unsafe public Int32 SetParameter(IntPtr paramNamePtr, string strVal)
        {
            fixed (char* strValBuff = strVal)
            {
                return m_setParameterStringW(paramNamePtr, (IntPtr)strValBuff);
            }
        }

        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramNamePtr">Pointer to the string (null terminated ASCII) with the name of the parameter (see parameters name table)</param>
        /// <param name="strValPtr">Buffer pointer containing the new value. (null terminated UTF-16)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        public Int32 SetParameter(IntPtr paramNamePtr, IntPtr strValPtr)
        {
            return m_setParameterStringW(paramNamePtr, strValPtr);
        }

        private delegate Int32 VBVMR_SetParametersW(IntPtr scriptPtr);
        private VBVMR_SetParametersW m_setParametersW;
        /// <summary>
        ///     Set one or several parameters by a script (&lt; 48 kB).
        /// </summary>
        /// <param name="script">
        ///     String giving the script (UTF16)<br/>                  
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
        /// <returns>
        ///     0: OK (no error).<br/> 
        ///		>0: number of line causing script error.<br/> 
        ///		-1: error<br/> 
        ///		-2: no server<br/> 
        ///		-3: unexpected error<br/> 
        ///		-4: unexpected error<br/> 
        /// </returns>
        public Int32 SetParameters(string script)
        {
            var scriptPtr = Marshal.StringToHGlobalUni(script);
            var res = m_setParametersW(scriptPtr);
            Marshal.FreeHGlobal(scriptPtr);

            return res;
        }

        /// <summary>
        ///     Set one or several parameters by a script (&lt; 48 kB). Alternative low-level, faster method.
        /// </summary>
        /// <param name="scriptPtr">
        ///     Buffer pointer to the
        ///     string giving the script (null terminated UTF-16)<br/>                  
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
        /// <returns>
        ///     0: OK (no error).<br/> 
        ///		>0: number of line causing script error.<br/> 
        ///		-1: error<br/> 
        ///		-2: no server<br/> 
        ///		-3: unexpected error<br/> 
        ///		-4: unexpected error<br/> 
        /// </returns>
        public Int32 SetParameters(IntPtr scriptPtr)
        {
            return m_setParametersW(scriptPtr);
        }
    }
}
