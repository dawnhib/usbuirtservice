using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;

using W3C.Soap;


namespace UsbUirtService
{
    // Get<GetRequestType, PortSet<UsbUirtServiceState, Fault>>
    public class Transmit : Submit<TransmitRequest, PortSet<DefaultSubmitResponseType, Fault>>
    {
        public Transmit()
            : base(new TransmitRequest())
        {

        }

        public Transmit(TransmitRequest request)
            : base(request)
        {

        }
    }

    [DataContract]
    public class TransmitRequest
    {
        [DataMember]
        public string irCode { get; set; }

        [DataMember]
        public UsbUirt.CodeFormat codeFormat { get; set; }

        [DataMember]
        public int repeatCount { get; set; }

        [DataMember]
        public TimeSpan inactivityWaitTime { set; get; }

        public override string ToString()
        {

            return string.Format(
                "irCode='{0}';codeFormat={1};repeatCount={2};inactivityWaitTime={3}",
                this.irCode,
                this.codeFormat.ToString(),
                this.repeatCount,
                this.inactivityWaitTime.Ticks
                );
        }
    }
}
