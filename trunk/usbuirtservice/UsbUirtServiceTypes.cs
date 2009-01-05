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
	public sealed class Contract
	{
		[DataMember]
		public const string Identifier = "http://schemas.tempuri.org/2009/01/usbuirtservice.html";
	}
	
	[DataContract]
	public class UsbUirtServiceState
	{
        public bool HasException { get; internal set; }
        public Exception LastException { get; internal set; }
	}
	
	[ServicePort]
    public class UsbUirtServiceOperations : PortSet<DsspDefaultLookup, DsspDefaultDrop, Get, Transmit>
	{
	}
	
	public class Get : Get<GetRequestType, PortSet<UsbUirtServiceState, Fault>>
	{
		public Get()
		{
		}
		
		public Get(GetRequestType body)
			: base(body)
		{
		}
		
		public Get(GetRequestType body, PortSet<UsbUirtServiceState, Fault> responsePort)
			: base(body, responsePort)
		{
		}
	}
}


