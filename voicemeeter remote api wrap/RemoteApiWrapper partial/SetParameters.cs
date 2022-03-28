using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter
{
    partial class RemoteApiWrapper
    {
        private void InitSetParameters()
        {
            GetReadyDelegate(ref m_setParameterFloat);
            GetReadyDelegate(ref m_setParameterStringA);
            GetReadyDelegate(ref m_setParameterStringW);
            GetReadyDelegate(ref m_setParameters);
            GetReadyDelegate(ref m_setParametersW);
        }
    }
}
