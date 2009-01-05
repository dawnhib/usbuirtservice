using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;
using W3C.Soap;
using System.Threading;

namespace UsbUirtService
{
    [Contract(Contract.Identifier)]
    [DisplayName("UsbUirtService")]
    [Description("Service interface for the USB-UIRT transceiver")]
    [ServicePort()]
    class UsbUirtService : DsspServiceBase
    {
        [ServiceState]
        UsbUirtServiceState _state = new UsbUirtServiceState();

        [ServicePort("/UsbUirtService", AllowMultipleInstances = true)]
        UsbUirtServiceOperations _mainPort = new UsbUirtServiceOperations();

        public UsbUirtService(DsspServiceCreationPort creationPort)
            : base(creationPort)
        {
        }

        protected override void Start()
        {

            // 
            // Add service specific initialization here
            // 

            base.Start();
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public virtual IEnumerator<ITask> TransmitHandler(Transmit transmit)
        {
            LogInfo( transmit.Body.ToString() );

            try
            {
                UsbUirtManager.Transmit(transmit);

                // update state
                //
                _state.HasException = false;
                _state.LastException = null;
            }
            catch (Exception exc)
            {
                // update state
                //
                _state.HasException = true;
                _state.LastException = exc;
            }

            transmit.ResponsePort.Post(DefaultSubmitResponseType.Instance);

            yield break;
        }

        ///// <summary>
        ///// Increment Tick Handler
        ///// </summary>
        ///// <param name="incrementTick">Not used</param>
        ///// <returns></returns>
        //[ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        //public IEnumerator<ITask> IncrementTickHandler(IncrementTick incrementTick)
        //{
        //    // Only update the state here because this is an Exclusive handler
        //    _state.Ticks++;
        //    LogInfo("Tick: " + _state.Ticks);
        //    incrementTick.ResponsePort.Post(DefaultUpdateResponseType.Instance);
        //    yield break;
        //}

    }
}


