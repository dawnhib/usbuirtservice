using System;
using System.Collections.Generic;
using System.Text;

using UsbUirt;

namespace UsbUirtService
{
    internal static class UsbUirtManager
    {
        //internal static UsbUirtManager Instance;

        static UsbUirt.Controller _controller;

        /// <summary>
        /// Initializes the UsbUirt controller instance
        /// </summary>
        static void init()
        {
            try
            {
                if (UsbUirt.Controller.DriverVersion != 0x0100)
                {
                    Console.WriteLine("ERROR: Invalid uuirtdrv version!\n");
                    return;
                }
                _controller = new UsbUirt.Controller();
                //_uirtController.Received += new UsbUirt.Controller.ReceivedEventHandler(Uirt_Received);
            }
            catch //(Exception exc)
            {
                throw;
            }
        }

        //private UsbUirtManager()
        //{

        //}

        //public UsbUirtManager Init()
        //{
        //    if (UsbUirtManager.Instance == null)
        //    {
        //        UsbUirtManager.Instance = new UsbUirtManager();
        //    }
        //}
        public static void Transmit(Transmit transmit) {
            UsbUirtManager.Transmit(
                transmit.Body.irCode,
                transmit.Body.codeFormat,
                transmit.Body.repeatCount,
                transmit.Body.inactivityWaitTime);
        }

        /// <summary>
        /// Transmits the IR Code synchronously (blocking)
        /// </summary>
        public static void Transmit(string irCode, 
            UsbUirt.CodeFormat codeFormat, 
            int repeatCount, 
            TimeSpan inactivityWaitTime)
        {
            init();

            if (inactivityWaitTime == null)
            {
                inactivityWaitTime = TimeSpan.Zero;
            }

            _controller.Transmit(irCode, codeFormat, repeatCount, inactivityWaitTime);

        }

        public static void TransmitAsync(string irCode, UsbUirt.CodeFormat codeFormat, int repeatCount, TimeSpan inactivityWaitTime)
        {
            _controller.TransmitCompleted += new UsbUirt.Controller.TransmitCompletedEventHandler(Uirt_TransmitCompleted);

            try
            {
                init();

                if (inactivityWaitTime == null)
                {
                    inactivityWaitTime = TimeSpan.Zero;
                }

                _controller.Transmit(irCode, codeFormat, repeatCount, inactivityWaitTime);
            }
            catch 
            {
                throw;
            }
            finally
            {
                _controller.TransmitCompleted -= new UsbUirt.Controller.TransmitCompletedEventHandler(Uirt_TransmitCompleted);
            }
        }

        #region UsbUirt Event Handlers

        //private static void Uirt_Received(object sender, UsbUirt.ReceivedEventArgs e)
        //{

        //}

        private static void Uirt_TransmitCompleted(object sender, UsbUirt.TransmitCompletedEventArgs e)
        {

        }

        #endregion
    }
}
