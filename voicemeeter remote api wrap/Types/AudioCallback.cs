using System;
using System.Runtime.InteropServices;

namespace AtgDev.Voicemeeter.Types.AudioCallback
{
    [StructLayout(LayoutKind.Sequential)]
    struct AudioInfo
    {
        public Int32 samplerate;
        public Int32 samplesPerFrame;
    }

    /// <summary>
    ///     Struct used inside Audio Callback for x86 API
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    unsafe struct AudioBuffer32
    {
        public Int32 samplerate;
        public Int32 samplesPerFrame;
        public Int32 inputsNumber;
        public Int32 outputsNumber;
        /// <summary>
        ///     Array (length=inputsNumber) of pointers to frames (array with length=samplesPerFrame)<br/>
        ///     Example:
        ///     <code>
        ///         var buffer = audioBufferPointer->inBufferP;
        ///         var frame = (Single*)buffer[channelIndex];<br/>
        ///         Single val = frame[sampleIndex];
        ///     </code>
        /// </summary>
        public fixed UInt32 inBufferP[128];
        ///<summary>
        ///     Array (length=outputsNumber) of pointers to frames (array with length=samplesPerFrame)<br/>
        ///     Example:
        ///     <code>
        ///         var buffer = audioBufferPointer->outBufferP;
        ///         var frame = (Single*)buffer[channelIndex];<br/>
        ///         frame[sampleIndex] = someFloatValue;
        ///     </code>
        /// </summary>
        public fixed UInt32 outBufferP[128];
    }

    /// <summary>
    ///     Struct used inside Audio Callback for x64 API 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    unsafe struct AudioBuffer64
    {
        public Int32 samplerate;
        public Int32 samplesPerFrame;
        public Int32 inputsNumber;
        public Int32 outputsNumber;
        /// <summary>
        ///     Array (length=inputsNumber) of pointers to frames (array with length=samplesPerFrame)<br/>
        ///     Example:
        ///     <code>
        ///         var buffer = audioBufferPointer->inBufferP;
        ///         var frame = (Single*)buffer[channelIndex];<br/>
        ///         Single val = frame[sampleIndex];
        ///     </code>
        /// </summary>
        public fixed UInt64 inBufferP[128];
        ///<summary>
        ///     Array (length=outputsNumber) of pointers to frames (array with length=samplesPerFrame)<br/>
        ///     Example:
        ///     <code>
        ///         var buffer = audioBufferPointer->outBufferP;
        ///         var frame = (Single*)buffer[channelIndex];<br/>
        ///         frame[sampleIndex] = someFloatValue;
        ///     </code>
        /// </summary>
        public fixed UInt64 outBufferP[128];
    }

    enum Command
    {
        Starting = 1,
        Ending,
        Change,
        BufferIn = 10,
        BufferOut,
        BufferMain = 20
    }

    enum Mode
    {
        Inputs = 1,
        Outputs,
        Main = 4,
        All = 4
    }

    /// <summary>
    ///     VB-AUDIO Callback is called for different task to Initialize, perform and end your process.<br/>
    ///     VB-AUDIO Callback is part of single TIME CRITICAL Thread.<br/>
    ///     VB-AUDIO Callback is non re-entrant (cannot be called while in process)<br/>
    ///     VB-AUDIO Callback is supposed to be REAL TIME when called to process buffer.<br/>
    ///     (it means that the process has to be performed as fast as possible, waiting cycles are forbidden.
    ///     do not use O/S synchronization object, even Critical_Section can generate waiting cycle. Do not use
    ///     system functions that can generate waiting cycle like display, disk or communication functions for example).
    /// </summary>
    /// <param name="customDataP">User pointer given on callback registration.</param>
    /// <param name="command">Reason why the callback is called.</param>
    /// <param name="callbackDataP">Pointer on structure, depending on Command/>.</param>
    /// <param name="addData">additional data, unused</param>
    /// <returns>
    ///     0: always 0 (unused).<br/>
    /// </returns>
    unsafe delegate Int32 Callback(void* customDataP, Command command, void* callbackDataP, Int32 addData);
}
