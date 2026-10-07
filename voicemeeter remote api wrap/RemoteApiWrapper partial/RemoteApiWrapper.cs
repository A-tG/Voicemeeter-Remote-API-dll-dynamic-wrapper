using System;

namespace AtgDev.Voicemeeter
{
    using AtgDev.Utils.Native;
    using System.Reflection.Emit;
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

        unsafe static internal void CopyStrToAsciiBuff(string str, byte* toBuff)
        {
            var len = str.Length;
#if NET8_0_OR_GREATER
            Ascii.FromUtf16(str.AsSpan(), new Span<byte>(toBuff, len), out int _);
#else
            fixed (char* c = str)
            {
                Encoding.ASCII.GetBytes(c, len, toBuff, len);
            }
#endif
            toBuff[len] = 0;
        }

        unsafe static internal void CopyStrToWcharBuff(string str, char* toBuff)
        {
            var len = str.Length;
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            str.AsSpan().CopyTo(new Span<char>(toBuff, len));
#else
            var size = sizeof(char);
            fixed (char* c = str)
            {
                Buffer.MemoryCopy(c, toBuff, size * (len + 1), size * len);
            }
#endif
            toBuff[len] = '\0';
        }

        /// <exception cref="ArgumentException">if paramName length more than <inheritdoc cref="ParameterMaxLength" path="/summary"/> (to limit stack allocation)</exception>
        internal static int CheckAndGetParameterNameLength(string param)
        {
            var len = param.Length;
            if (len > ParameterMaxLength)
            {
                throw new ArgumentException("parameter name's length must not exceed " + ParameterMaxLength);
            }
            return len;
        }

        /// <inheritdoc cref="CheckAndGetParameterNameLength"/>
        internal static int CheckAndGetValueLenght(string val)
        {
            return CheckAndGetParameterNameLength(val);
        }
    }
}
