using System;

namespace AtgDev.Voicemeeter
{
    using AtgDev.Utils.Native;

    /// <summary>
    ///     <para>Voicemeeter Remote API</para>
    ///     Wrapper for the library that allows communication with Voicemeeter Applications
    /// </summary>
    public partial class RemoteApiWrapper : DllWrapperBase
    {
        // shortcut for <inheritdoc> 
        /// <summary>-100: procedure was not successfully imported</summary>
        public const int ProcedureNotImportedErrorCode = -100;

        /// <summary>512</summary>
        private const int ParameterMaxLength = 512; // to limit stackalloc inside GetParameter(), SetParameter()

        public RemoteApiWrapper(string dllPath) : base(dllPath) 
        {
            InitProcedures();
        }


        private void InitProcedures()
        {
            InitLogin();
            InitGeneralInformation();
            InitGetParameters();
            InitGetLevels();
            InitSetParameters();
            InitDevicesEnumerator();
            InitAudioCallback();
            InitMacroButtons();
        }

        unsafe internal void CopyCharStrBuffToByteStrBuff(char* frombuff, byte* toBuff, int lenWithNull)
        {
            var lenWithouNull = lenWithNull - 1;
            for (int i = 0; i < lenWithouNull; i++)
            {
                toBuff[i] = (byte)frombuff[i];
            }
            if (lenWithouNull > 0)
            {
                toBuff[lenWithouNull] = 0; // add null character
            }
        }

        unsafe internal void CopyStrToByteStrBuff(string str, byte* toBuff)
        {
            fixed (char* c = str)
            {
                CopyCharStrBuffToByteStrBuff(c, toBuff, str.Length + 1); // to account null character
            }
        }

        /// <exception cref="ArgumentException">if paramName length more than <inheritdoc cref="ParameterMaxLength" path="//summary"/> (to limit stack allocation)</exception>
        internal int CheckGetParameterNameLength(string param)
        {
            var len = param.Length;
            if (len > ParameterMaxLength)
            {
                throw new ArgumentException("parameter name's length must not exceed " + ParameterMaxLength);
            }
            return len;
        }

        /// <inheritdoc cref="CheckGetParameterNameLength"/>
        internal int CheckGetValueLenght(string val)
        {
            return CheckGetParameterNameLength(val);
        }
    }
}
