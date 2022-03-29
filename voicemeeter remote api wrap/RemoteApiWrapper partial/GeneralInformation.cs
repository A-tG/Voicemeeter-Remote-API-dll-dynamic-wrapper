using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitGeneralInformation()
        {
            GetReadyDelegate(ref m_getVoicemeeterType);
            GetReadyDelegate(ref m_getVoicemeeterVersion);
        }

        private delegate Int32 VBVMR_GetVoicemeeterType(out Int32 type);
        private VBVMR_GetVoicemeeterType m_getVoicemeeterType;
        /// <summary>
        ///     Get Voicemeeter Type.
        /// </summary>
        /// <param name="type">The variable receiving the type (1 = Voicemeeter, 2 = Voicemeeter Banana, 3 = Voicemeeter Potato).</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: cannot get client (unexpected)<br/>
        ///     -2: no server.<br/>
        /// </returns>
        public Int32 GetVoicemeeterType(out Int32 type)
        {
            return m_getVoicemeeterType(out type);
        }

        private delegate Int32 VBVMR_GetVoicemeeterVersion(out Int32 ver);
        private VBVMR_GetVoicemeeterVersion m_getVoicemeeterVersion;
        /// <summary>
        ///     Get Voicemeeter Version
        /// </summary>
        /// <param name="ver">
        ///     Variable receiving the version (v1.v2.v3.v4)<br/>
        ///     <c>
        ///          v1 = (version &amp; 0xFF000000)>>24;<br/>
		/// 	        v2 = (version &amp; 0x00FF0000)>>16;<br/>
		/// 	        v3 = (version &amp; 0x0000FF00)>>8;<br/>
		/// 	        v4 = version &amp; 0x000000FF;<br/>
        /// 	   </c>
        /// </param>
        /// <inheritdoc cref="GetVoicemeeterType(out Int32)" path="/returns"/>
        public Int32 GetVoicemeeterVersion(out Int32 ver)
        {
            return m_getVoicemeeterVersion(out ver);
        }
    }
}
