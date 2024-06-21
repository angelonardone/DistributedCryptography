/*
				   File: type_SdtSendRawTransaction__postInput
			Description: SendRawTransaction__postInput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
		   Program type: Callable routine
			  Main DBMS: 
*/
using System;
using System.Collections;
using GeneXus.Utils;
using GeneXus.Resources;
using GeneXus.Application;
using GeneXus.Metadata;
using GeneXus.Cryptography;
using GeneXus.Encryption;
using GeneXus.Http.Client;
using GeneXus.Http.Server;
using System.Reflection;
using System.Xml.Serialization;
using System.Runtime.Serialization;


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="SendRawTransaction__postInput")]
	[XmlType(TypeName="SendRawTransaction__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSendRawTransaction__postInput : GxUserType
	{
		public SdtSendRawTransaction__postInput( )
		{
			/* Constructor for serialization */
			gxTv_SdtSendRawTransaction__postInput_Hextransaction = "";

		}

		public SdtSendRawTransaction__postInput(IGxContext context)
		{
			this.context = context;	
			initialize();
		}

		#region Json
		private static Hashtable mapper;
		public override string JsonMap(string value)
		{
			if (mapper == null)
			{
				mapper = new Hashtable();
			}
			return (string)mapper[value]; ;
		}

		public override void ToJSON()
		{
			ToJSON(true) ;
			return;
		}

		public override void ToJSON(bool includeState)
		{
			AddObjectProperty("hexTransaction", gxTpr_Hextransaction, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="hexTransaction")]
		[XmlElement(ElementName="hexTransaction")]
		public string gxTpr_Hextransaction
		{
			get {
				return gxTv_SdtSendRawTransaction__postInput_Hextransaction; 
			}
			set {
				gxTv_SdtSendRawTransaction__postInput_Hextransaction = value;
				SetDirty("Hextransaction");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtSendRawTransaction__postInput_Hextransaction = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSendRawTransaction__postInput_Hextransaction;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"SendRawTransaction__postInput", Namespace="distributedcryptography")]
	public class SdtSendRawTransaction__postInput_RESTInterface : GxGenericCollectionItem<SdtSendRawTransaction__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSendRawTransaction__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtSendRawTransaction__postInput_RESTInterface( SdtSendRawTransaction__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="hexTransaction", Order=0)]
		public  string gxTpr_Hextransaction
		{
			get { 
				return sdt.gxTpr_Hextransaction;

			}
			set { 
				 sdt.gxTpr_Hextransaction = value;
			}
		}


		#endregion

		public SdtSendRawTransaction__postInput sdt
		{
			get { 
				return (SdtSendRawTransaction__postInput)Sdt;
			}
			set { 
				Sdt = value;
			}
		}

		[OnDeserializing]
		void checkSdt( StreamingContext ctx )
		{
			if ( sdt == null )
			{
				sdt = new SdtSendRawTransaction__postInput() ;
			}
		}
	}
	#endregion
}