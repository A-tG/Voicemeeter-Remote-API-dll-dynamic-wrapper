using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitSetParameters()
        {
            m_setParameterFloat = GetReadyDelegate<VBVMR_SetParameterFloat>();
            m_setParameterStringA = GetReadyDelegate<VBVMR_SetParameterStringA>();
            m_setParameterStringW = GetReadyDelegate<VBVMR_SetParameterStringW>();
            m_setParameters = GetReadyDelegate<VBVMR_SetParameters>();
            m_setParametersW = GetReadyDelegate<VBVMR_SetParametersW>();
        }

        private delegate Int32 VBVMR_SetParameterFloat([MarshalAs(UnmanagedType.LPStr)] string paramName, Single val);
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
            return m_setParameterFloat(paramName, val);
        }

        private delegate Int32 VBVMR_SetParameterStringA(
            [MarshalAs(UnmanagedType.LPStr)] string paramName,
            [MarshalAs(UnmanagedType.LPStr)] string strVal
        );
        private VBVMR_SetParameterStringA m_setParameterStringA;
        /// <summary>
        ///     Get parameter value.
        /// </summary>
        /// <param name="paramName">The name of the parameter (see parameters name table)</param>
        /// <param name="strVal">The variable containing the new value (ANSI).</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        /// </returns>
        public Int32 Legacy_SetParameter(string paramName, string strVal)
        {
            return m_setParameterStringA(paramName, strVal);
        }

        private delegate Int32 VBVMR_SetParameterStringW(
            [MarshalAs(UnmanagedType.LPStr)] string paramName,
            [MarshalAs(UnmanagedType.LPWStr)] string strVal
        );
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
            return m_setParameterStringW(paramName, strVal);
        }

        private delegate Int32 VBVMR_SetParameters([MarshalAs(UnmanagedType.LPStr)] string script);
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
            return m_setParameters(script);
        }

        private delegate Int32 VBVMR_SetParametersW([MarshalAs(UnmanagedType.LPWStr)] string script);
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
            return m_setParametersW(script);
        }
    }
}
