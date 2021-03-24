using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AtgDev.Voicemeeter
{
    using Types.AudioCallback;

    partial class RemoteApiWrapper
    {
        private void InitAudioCallback()
        {
            unsafe
            {
                m_audioCallbackRegister = GetReadyDelegate<VBVMR_AudioCallbackRegister>();
            }
            m_audioCallbackStart = GetReadyDelegate<VBVMR_AudioCallbackStart>();
            m_audioCallbackStop = GetReadyDelegate<VBVMR_AudioCallbackStop>();
            m_audioCallbackUnregister = GetReadyDelegate<VBVMR_AudioCallbackUnregister>();
        }

        unsafe private delegate Int32 VBVMR_AudioCallbackRegister(
            Mode mode,
            Callback callback,
            void* customDataP,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder ClientName
        );
        unsafe private VBVMR_AudioCallbackRegister m_audioCallbackRegister;
        /// <summary>
        ///     register your audio callback function to receive real time audio buffer
        ///     it's possible to register up to 3x different Audio Callback in the same application or in 
		///	    3x different applications.In the same application, this is possible because Voicemeeter
        ///     provides 3 kind of audio Streams:<br/>
		///		    - AUDIO INPUT INSERT(to process all Voicemeeter inputs as insert)<br/>
		///		    - AUDIO OUTPUT INSERT(to process all Voicemeeter BUS outputs as insert)<br/>
		///		    - ALL AUDIO I/O(to process all Voicemeeter i/o).<br/>
		///	    Note: a single callback can be used to receive the 3 possible audio streams.
        /// </summary>
        /// <param name="mode">callback type</param>
        /// <param name="customDataP">Pointer that will be passed in callback first argument</param>
        /// <param name="ClientName">Name of the application registering the Callback. / Name of the application already registered. (max 64 characters)</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     1: callback already registered (by another application).<br/>
        /// </returns>
        unsafe public Int32 AudioCallbackRegister(Mode mode, Callback callback, void* customDataP, ref string ClientName)
        {
            const int maxLen = 64;
            var len = Math.Min(ClientName.Length, maxLen);
            var name = new StringBuilder(ClientName, 0, len, maxLen);
            var resp = m_audioCallbackRegister(mode, callback, customDataP, name);
            ClientName = name.ToString();
            return resp;
        }

        private delegate Int32 VBVMR_AudioCallbackStart();
        private VBVMR_AudioCallbackStart m_audioCallbackStart;
        /// <summary>
        ///     Start Audio processing
        /// </summary>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no callback registred.<br/>
        /// </returns>
        public Int32 AudioCallbackStart()
        {
            return m_audioCallbackStart();
        }

        private delegate Int32 VBVMR_AudioCallbackStop();
        private VBVMR_AudioCallbackStop m_audioCallbackStop;
        /// <summary>
        ///     Stop Audio processing
        /// </summary>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no callback registred.<br/>
        /// </returns>
        public Int32 AudioCallbackStop()
        {
            return m_audioCallbackStop();
        }

        private delegate Int32 VBVMR_AudioCallbackUnregister();
        private VBVMR_AudioCallbackUnregister m_audioCallbackUnregister;
        /// <summary>
        ///     unregister your callback to release voicemeeter virtual driver
        ///     (this function will automatically call VBVMR_AudioCallbackStop() function)
        /// </summary>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     1: callback already unregistered.<br/>
        /// </returns>
        public Int32 AudioCallbackUnregister()
        {
            return m_audioCallbackUnregister();
        }
    }
}
