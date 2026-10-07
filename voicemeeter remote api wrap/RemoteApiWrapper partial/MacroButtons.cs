using System;
using System.Runtime.CompilerServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitMacroButtons()
        {
            TryGetReadyDelegate(ref m_macroButtonIsDirty);
            TryGetReadyDelegate(ref m_macroButtonGetStatus);
            TryGetReadyDelegate(ref m_MacroButtonSetStatus);
            TryGetReadyDelegate(ref m_setCustomButton);
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

        private delegate Int32 VBVMR_SetCustomButton(Int32 buttonIndex, Int32 type, Int32 state, IntPtr labelPtr, IntPtr hwnd, Int32 command);
        private VBVMR_SetCustomButton m_setCustomButton;
        /// <summary>
        ///     Added in 3.1.2.5 / 2.1.2.5 / 1.1.2.5<br/> 
        ///     Set custom button on Voicemeeter to get Push message and display information. Check vmr_client.c in the official SDK.
        /// </summary>
        /// <param name="buttonIndex">Button index</param>
        /// <param name="type">-1: no button displayed / 0: no position / 1: Push button, / 2: 2x Positions button (Click behavior)</param>
        /// <param name="state">-1: no change, 0: released, 1: pushed</param>
        /// <param name="label">Button label (32 char max)</param>
        /// <param name="hwnd">Windows handle that will receive WM_COMMAND message</param>
        /// <param name="command">Command ID (WPARAM) / LPARAM: button state</param>
        /// <returns>
        ///     0: OK (no error).<br/>
        ///     -1: error<br/>
        ///     -2: no server.<br/>
        ///     <inheritdoc cref="ProcedureNotImportedErrorCode" path="/summary"/>
        /// </returns>
#if NET5_0_OR_GREATER
        [SkipLocalsInit]
#endif
        unsafe public Int32 SetCustomButton(Int32 buttonIndex, Int32 type, Int32 state, string label, IntPtr hwnd, Int32 command)
        {
            if (m_setCustomButton is null) return ProcedureNotImportedErrorCode;

            var len = label.Length;
            if (len > 32) throw new ArgumentOutOfRangeException("label 32 character maximum");

            char* pLabelBuff = stackalloc char[len + 1];
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            label.AsSpan().CopyTo(new Span<char>(pLabelBuff, len));
            pLabelBuff[len] = '\0';
#else
            CopyStrToWcharBuff(label, pLabelBuff);
#endif

            return m_setCustomButton(buttonIndex, type, state, (IntPtr)pLabelBuff, hwnd, command);
        }

        /// <summary>
        ///     Alternative low-level method for pre allocated buffers. For maximum performance
        /// </summary>
        /// <inheritdoc cref="SetCustomButton(Int32, Int32, Int32, string, IntPtr, Int32)"/>
        public Int32 SetCustomButton(Int32 buttonIndex, Int32 type, Int32 state, IntPtr pLabel, IntPtr hwnd, Int32 command)
        {
            if (m_setCustomButton is null) return ProcedureNotImportedErrorCode;

            return m_setCustomButton(buttonIndex, type, state, pLabel, hwnd, command);
        }
    }
}
