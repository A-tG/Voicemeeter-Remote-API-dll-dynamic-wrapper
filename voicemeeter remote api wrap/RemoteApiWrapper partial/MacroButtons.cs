using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitMacroButtons()
        {
            m_macroButtonIsDirty = GetReadyDelegate<VBVMR_MacroButton_IsDirty>();
            m_macroButtonGetStatus = GetReadyDelegate<VBVMR_MacroButton_GetStatus>();
            m_MacroButtonSetStatus = GetReadyDelegate<VBVMR_MacroButton_SetStatus>();
        }

        public enum MacrobuttonMode
        {
            /// <summary>PUSH or RELEASE state</summary>
            Default = 0,
            /// <summary>change Displayed State only</summary>
            State = 2,
            /// <summary>change Trigger State</summary>
            Trigger = 3
        }

        private delegate Int32 VBVMR_MacroButton_IsDirty();
        private VBVMR_MacroButton_IsDirty m_macroButtonIsDirty;
        /// <summary>
        ///     Check if Macro Buttons states changed.<br/>   
        ///     Call this function periodically (typically every 50 or 500ms) to know if something happen on MacroButton states.<br/>
        ///     (this function must be called from one thread only)<br/>
        /// </summary>
        /// <returns>
        ///      0: no new status.br/>
        ///     >0: last nu logical button status changed.br/>
        ///     -1: error (unexpected)br/>
        ///     -2: no server.
        /// </returns>
        public Int32 MacroButtonIsDirty()
        {
            return m_macroButtonIsDirty();
        }

        private delegate Int32 VBVMR_MacroButton_GetStatus(Int32 buttonIndex, out Single val, MacrobuttonMode mode);
        private VBVMR_MacroButton_GetStatus m_macroButtonGetStatus;
        /// <summary>
        ///     Get current status of a given button.
        /// </summary>
        /// <param name="buttonIndex">button index: 0 to 79</param>
        /// <param name="val">Variable receiving the wanted value (0.0 = OFF / 1.0 = ON)</param>
        /// <param name="mode">Define what kind of value you want to read</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public Int32 MacroButtonGetStatus(Int32 buttonIndex, out Single val, MacrobuttonMode mode)
        {
            return m_macroButtonGetStatus(buttonIndex, out val, mode);
        }

        private delegate Int32 VBVMR_MacroButton_SetStatus(Int32 buttonIndex, Single val, MacrobuttonMode mode);
        private VBVMR_MacroButton_SetStatus m_MacroButtonSetStatus;
        /// <summary>
        ///     Set current button value.
        /// </summary>
        /// <param name="buttonIndex">button index: 0 to 79</param>
        /// <param name="val">Value giving the status (0.0 = OFF / 1.0 = ON).</param>
        /// <param name="mode">define what kind of value you want to write/modify</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     -3: unknown parameter<br/>
        ///     -5: structure mismatch<br/>
        /// </returns>
        public Int32 MacroButtonSetStatus(Int32 buttonIndex, Single val, MacrobuttonMode mode)
        {
            return m_MacroButtonSetStatus(buttonIndex, val, mode);
        }
    }
}
