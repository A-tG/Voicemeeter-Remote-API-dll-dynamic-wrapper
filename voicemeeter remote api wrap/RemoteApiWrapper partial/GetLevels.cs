using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitGetLevels()
        {
            m_getLevel = GetReadyDelegate<VBVMR_GetLevel>();
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

        // long __stdcall VBVMR_GetMidiMessage(unsigned char *pMIDIBuffer, long nbByteMax);
    }
}
