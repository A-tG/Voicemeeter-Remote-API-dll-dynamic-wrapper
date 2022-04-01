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
        /// <inheritdoc cref="SetParameter(string, Single)" path="/returns"/>
        /// <inheritdoc cref="SetParameter(string, Single)" path="/exception"/>
        unsafe public Int32 SetParameter(string paramName, string strVal)
        {
            byte* paramNameBuff = stackalloc byte[CheckAndGetParameterNameLength(paramName) + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            return SetParameter((IntPtr)paramNameBuff, strVal);
        }

        /// <inheritdoc cref="SetParameter(IntPtr, IntPtr)"/>
        /// <inheritdoc cref="SetParameter(string, string)"/>
        unsafe public Int32 SetParameter(IntPtr paramNamePtr, string strVal)
        {
            fixed (char* strValBuff = strVal)
            {
                return m_setParameterStringW(paramNamePtr, (IntPtr)strValBuff);
            }
        }

        /// <inheritdoc cref="SetParameter(IntPtr, IntPtr)"/>
        /// <inheritdoc cref="SetParameter(string, string)"/>
        unsafe public Int32 SetParameter(string paramName, IntPtr strValPtr)
        {
            byte* paramNameBuff = stackalloc byte[CheckAndGetParameterNameLength(paramName) + 1];
            CopyStrToByteStrBuff(paramName, paramNameBuff);

            return m_setParameterStringW((IntPtr)paramNameBuff, strValPtr);
        }

        /// <summary>
        ///    Alternative low-level method for pre allocated buffers. For maximum performance
        /// </summary>
        /// <param name="paramNamePtr">
        ///     <inheritdoc cref="GetParameter(IntPtr, IntPtr)" path="/param[@name='paramNamePtr']"/>
        /// </param>
        /// <param name="strValPtr">Buffer pointer containing the new value. (C string, null terminated UTF-16)</param>
        /// <inheritdoc cref="SetParameter(string, Single)" path="/returns"/>
        public Int32 SetParameter(IntPtr paramNamePtr, IntPtr strValPtr)
        {
            return m_setParameterStringW(paramNamePtr, strValPtr);
        }

        private delegate Int32 VBVMR_SetParametersW(IntPtr scriptPtr);
        private VBVMR_SetParametersW m_setParametersW;
        /// <summary>
        ///     Set one or several parameters by a script (&lt; 48 kB). (UTF-16)
        /// </summary>
        /// <param name="script">
        ///     String giving the script<br/>                  
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
        unsafe public Int32 SetParameters(string script)
        {
            fixed(char* scriptPtr = script)
            {
                return m_setParametersW((IntPtr)scriptPtr);
            }
        }

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
        /// <inheritdoc cref="SetParameters(string)"/>
        public Int32 SetParameters(IntPtr scriptPtr)
        {
            return m_setParametersW(scriptPtr);
        }
    }
}
