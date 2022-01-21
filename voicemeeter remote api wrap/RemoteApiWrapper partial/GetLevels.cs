using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitGetLevels()
        {
            GetReadyDelegate(ref m_getLevel);
            GetReadyDelegate(ref m_getMidiMessage);
            TryGetReadyDelegate(ref m_sendMidiMessage);
        }

        private delegate Int32 VBVMR_GetLevel(Int32 type, Int32 channel, out Single val);
        private VBVMR_GetLevel m_getLevel;
        /// <summary>
        ///     <para>Get Current levels.</para>
        ///     this function must be called from one thread only
        /// </summary>
        /// <param name="type">
        ///     0= pre fader input levels.<br/>
		///		1= post fader input levels.<br/>
		///		2= post Mute input levels.<br/>
		///		3= output levels.<br/>
        /// </param>
        /// <param name="channel">Audio channel zero based index</param>
        /// <param name="val">The variable receiving the wanted value.</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: no level available<br/>
        ///     -4: out of range<br/>
        /// </returns>
        public Int32 GetLevel(Int32 type, Int32 channel, out Single val)
        {
            return m_getLevel(type, channel, out val);
        }

        private delegate Int32 VBVMR_GetMidiMessage(byte[] midiBuffer, Int32 ByteMax);
        private VBVMR_GetMidiMessage m_getMidiMessage;
        /// <summary>
        ///     <para>Get MIDI message from M.I.D.I. input device used by Voicemeeter M.I.D.I. mapping.</para>
        ///     this function must be called from one thread only
        /// </summary>
        /// <param name="midiBuffer">
        ///     MIDI Buffer array. Expected message size is below 4 bytes, 
        ///     but it's recommended to use larger buffer to receive
        ///     possible multiple MIDI event message in optimal way.
        /// </param>
        /// <param name="bufferSize">
        ///     [Optional] MIDI Buffer size (1024 is recommended)
        /// </param>
        /// <returns>
        ///     >0: number of bytes placed in buffer (2 or 3 byte for usual M.I.D.I. message)<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -5: no MIDI data<br/>
        ///     -6: no MIDI data<br/>
        /// </returns>
        public Int32 GetMidiMessage(out byte[] midiBuffer, int bufferSize = 1024)
        {
            midiBuffer = new byte[bufferSize]; 
            return m_getMidiMessage(midiBuffer, bufferSize);
        }

        private delegate Int32 VBVMR_SendMidiMessage(byte[] midiBuffer, Int32 ByteMax);
        private VBVMR_SendMidiMessage m_sendMidiMessage;
        /// <summary>
        ///     Added in 3.02.2 / 2.0.6.2 / 1.0.8.2
        /// </summary>
        public Int32 SendMidiMessage(byte[] midiBuffer)
        {
            if (m_sendMidiMessage is null) return ProcedureNotImportedErrorCode;

            return m_sendMidiMessage(midiBuffer, midiBuffer.Length);
        }
    }
}
