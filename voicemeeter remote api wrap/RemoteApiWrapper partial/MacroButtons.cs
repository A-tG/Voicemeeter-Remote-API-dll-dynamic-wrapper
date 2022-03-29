using System;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitMacroButtons()
        {
            TryGetReadyDelegate(ref m_macroButtonIsDirty);
            TryGetReadyDelegate(ref m_macroButtonGetStatus);
            TryGetReadyDelegate(ref m_MacroButtonSetStatus);
        }

        private delegate Int32 VBVMR_MacroButton_IsDirty();
        private VBVMR_MacroButton_IsDirty m_macroButtonIsDirty;
        /// <summary>
        ///     Added in 3.0.1.4 / 2.0.5.4 / 1.0.7.4 <br/> 
        ///     Check if Macro Buttons states changed.<br/>   
        ///     Call this function periodically (typically every 50 or 500ms) to know if something happen on MacroButton states.<br/>
        ///     (this function must be called from one thread only)<br/>
        /// </summary>
        /// <returns>
        ///      0: no new status.br/>
        ///     >0: last nu logical button status changed.br/>
        ///     -1: error (unexpected)<br/>
        ///     -2: no server.<br/>
        ///     <inheritdoc cref="ProcedureNotImportedErrorCode" path="/summary"/>
        /// </returns>
        public Int32 MacroButtonIsDirty()
        {
            if (m_macroButtonIsDirty is null) return ProcedureNotImportedErrorCode;

            return m_macroButtonIsDirty();
        }

        private delegate Int32 VBVMR_MacroButton_GetStatus(Int32 buttonIndex, out Single val, Int32 mode);
        private VBVMR_MacroButton_GetStatus m_macroButtonGetStatus;
        /// <summary>
        ///     Added in 3.0.1.4 / 2.0.5.4 / 1.0.7.4 <br/> 
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
        ///     <inheritdoc cref="ProcedureNotImportedErrorCode" path="/summary"/>
        /// </returns>
        public Int32 MacroButtonGetStatus(Int32 buttonIndex, out Single val, Int32 mode)
        {
            if (m_macroButtonGetStatus is null)
            {
                val = 0;
                return ProcedureNotImportedErrorCode;
            }
            return m_macroButtonGetStatus(buttonIndex, out val, mode);
        }

        private delegate Int32 VBVMR_MacroButton_SetStatus(Int32 buttonIndex, Single val, Int32 mode);
        private VBVMR_MacroButton_SetStatus m_MacroButtonSetStatus;
        /// <summary>
        ///     Added in 3.0.1.4 / 2.0.5.4 / 1.0.7.4 <br/> 
        ///     Set current button value.
        /// </summary>
        /// <param name="val">Variable giving the status (0.0 = OFF / 1.0 = ON).</param>
        /// <param name="mode">Define what kind of value you want to write/modify</param>
        /// <inheritdoc cref="MacroButtonGetStatus(int, out float, int)"/>
        public Int32 MacroButtonSetStatus(Int32 buttonIndex, Single val, Int32 mode)
        {
            if (m_MacroButtonSetStatus is null) return ProcedureNotImportedErrorCode;

            return m_MacroButtonSetStatus(buttonIndex, val, mode);
        }
    }
}
