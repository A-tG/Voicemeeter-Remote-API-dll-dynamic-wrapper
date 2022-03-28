using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitGetParameters()
        {
            GetReadyDelegate(ref m_isParametersDirty);
            GetReadyDelegate(ref m_getParameterFloat);
            GetReadyDelegate(ref m_getParameterStringA);
            GetReadyDelegate(ref m_getParameterStringW);
        }

        private delegate Int32 VBVMR_IsParametersDirty();
        private VBVMR_IsParametersDirty m_isParametersDirty;
        /// <summary>
        ///     Check if parameters have changed.<br/>
        ///     Call this function periodically (typically every 10 or 20ms).
        /// </summary>
        /// <returns>
        ///     0: no new paramters.<br/>
        ///     1: New parameters -> update your display.<br/>
        ///     -1: error (unexpected)<br/>
        ///     -2: no server.<br/>
        /// </returns>
        public Int32 IsParametersDirty()
        {
            return m_isParametersDirty();
        }
    }
}
