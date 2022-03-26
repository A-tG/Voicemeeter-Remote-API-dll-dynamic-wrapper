﻿using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitSetParameters()
        {
            GetReadyDelegate(ref m_setParameterFloat);
            GetReadyDelegate(ref m_setParameterStringA);
            GetReadyDelegate(ref m_setParameterStringW);
            GetReadyDelegate(ref m_setParameters);
            GetReadyDelegate(ref m_setParametersW);
        }

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
        public Int32 SetParameter(string paramName, Single val)
        {
            var paramNamePtr = Marshal.StringToHGlobalAnsi(paramName);
            var res = m_setParameterFloat(paramNamePtr, val);
            Marshal.FreeHGlobal(paramNamePtr);

            return res;
        }

        /// <summary>
        ///     Set a single parameter. Alternative low-level, faster method.
        /// </summary>
        /// <param name="paramNamePtr">Buffer pointer (null terminated ASCII) with the name of the parameter 
        /// (see VoicemeeterRemoteAPI parameters name table)</param>
        /// <param name="val">The variable containing the new value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        public Int32 SetParameter(IntPtr paramNamePtr, Single val)
        {
            return m_setParameterFloat(paramNamePtr, val);
        }

        private delegate Int32 VBVMR_SetParameterStringA(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_SetParameterStringA m_setParameterStringA;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see parameters name table)</param>
        /// <param name="strVal">The variable containing the new value (ASCII).</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        public Int32 Legacy_SetParameter(string paramName, string strVal)
        {
            var paramNamePtr = Marshal.StringToHGlobalAnsi(paramName);
            var strValPtr = Marshal.StringToHGlobalAnsi(strVal);

            var resp = m_setParameterStringA(paramNamePtr, strValPtr);

            Marshal.FreeHGlobal(paramNamePtr);
            Marshal.FreeHGlobal(strValPtr);

            return resp;
        }

        private delegate Int32 VBVMR_SetParameterStringW(IntPtr paramNamePtr, IntPtr strValPtr);
        private VBVMR_SetParameterStringW m_setParameterStringW;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see parameters name table)</param>
        /// <param name="strVal">The variable containing the new value. (UTF-16)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        public Int32 SetParameter(string paramName, string strVal)
        {
            var paramNamePtr = Marshal.StringToHGlobalAnsi(paramName);
            var strValPtr = Marshal.StringToHGlobalUni(strVal);

            var resp = m_setParameterStringW(paramNamePtr, strValPtr);

            Marshal.FreeHGlobal(paramNamePtr);
            Marshal.FreeHGlobal(strValPtr);

            return resp;
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
        /// <returns>
        ///     0: OK (no error).<br/> 
        ///		>0: number of line causing script error.<br/> 
        ///		-1: error<br/> 
        ///		-2: no server<br/> 
        ///		-3: unexpected error<br/> 
        ///		-4: unexpected error<br/> 
        /// </returns>
        public Int32 Legacy_SetParameters(string script)
        {
            var scriptPtr = Marshal.StringToHGlobalAnsi(script);
            var res = m_setParameters(scriptPtr);
            Marshal.FreeHGlobal(scriptPtr);

            return res;
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
        ///     Buffer pointer of the
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
