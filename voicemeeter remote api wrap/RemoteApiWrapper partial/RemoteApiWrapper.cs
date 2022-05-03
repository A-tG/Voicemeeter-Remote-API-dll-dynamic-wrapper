using System;

namespace AtgDev.Voicemeeter
{
    using AtgDev.Utils.Native;
    using System.Text;

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

        unsafe internal void CopyCharStrBuffToAsciiBuff(char* frombuff, byte* toBuff, int lenWithNull)
        {
            var lenWithouNull = lenWithNull - 1;
            ASCIIEncoding.ASCII.GetBytes(frombuff, lenWithouNull, toBuff, lenWithouNull);
            if (lenWithouNull >= 0)
            {
                toBuff[lenWithouNull] = 0; // add null character
            }
        }

        unsafe internal void CopyStrToAsciiBuff(string str, byte* toBuff)
        {
            fixed (char* c = str)
            {
                CopyCharStrBuffToAsciiBuff(c, toBuff, str.Length + 1); // to account null character
            }
        }

        /// <exception cref="ArgumentException">if paramName length more than <inheritdoc cref="ParameterMaxLength" path="/summary"/> (to limit stack allocation)</exception>
        internal int CheckAndGetParameterNameLength(string param)
        {
            var len = param.Length;
            if (len > ParameterMaxLength)
            {
                throw new ArgumentException("parameter name's length must not exceed " + ParameterMaxLength);
            }
            return len;
        }

        /// <inheritdoc cref="CheckAndGetParameterNameLength"/>
        internal int CheckAndGetValueLenght(string val)
        {
            return CheckAndGetParameterNameLength(val);
        }
    }
}
