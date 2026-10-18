/*
				   File: type_SdtServerParameters
			Description: ServerParameters
				 Author: Nemo 🐠 for C# (.NET) version 18.0.16.189595
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
using System.Text.Json.Serialization;

using GeneXus.Programs;

namespace GeneXus.Programs.distcrypt
{
	[XmlRoot(ElementName="ServerParameters")]
	[XmlType(TypeName="ServerParameters" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtServerParameters : GxUserType
	{
		public SdtServerParameters( )
		{
			/* Constructor for serialization */
			gxTv_SdtServerParameters_Serverhost = "";


		}

		public SdtServerParameters(IGxContext context)
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
			AddObjectProperty("ServerHost", gxTpr_Serverhost, false);


			AddObjectProperty("ServerPort", gxTpr_Serverport, false);


			AddObjectProperty("ServerSecure", gxTpr_Serversecure, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ServerHost")]
		[XmlElement(ElementName="ServerHost")]
		public string gxTpr_Serverhost
		{
			get {
				return gxTv_SdtServerParameters_Serverhost; 
			}
			set {
				gxTv_SdtServerParameters_Serverhost = value;
				SetDirty("Serverhost");
			}
		}




		[SoapElement(ElementName="ServerPort")]
		[XmlElement(ElementName="ServerPort")]
		public int gxTpr_Serverport
		{
			get {
				return gxTv_SdtServerParameters_Serverport; 
			}
			set {
				gxTv_SdtServerParameters_Serverport = value;
				SetDirty("Serverport");
			}
		}




		[SoapElement(ElementName="ServerSecure")]
		[XmlElement(ElementName="ServerSecure")]
		public bool gxTpr_Serversecure
		{
			get {
				return gxTv_SdtServerParameters_Serversecure; 
			}
			set {
				gxTv_SdtServerParameters_Serversecure = value;
				SetDirty("Serversecure");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Static Type Properties

		[XmlIgnore]
		private static GXTypeInfo _typeProps;
		protected override GXTypeInfo TypeInfo { get { return _typeProps; } set { _typeProps = value; } }

		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtServerParameters_Serverhost = "";


			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtServerParameters_Serverhost;
		 

		protected int gxTv_SdtServerParameters_Serverport;
		 

		protected bool gxTv_SdtServerParameters_Serversecure;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ServerParameters", Namespace="distributedcryptography")]
	public class SdtServerParameters_RESTInterface : GxGenericCollectionItem<SdtServerParameters>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtServerParameters_RESTInterface( ) : base()
		{	
		}

		public SdtServerParameters_RESTInterface( SdtServerParameters psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("ServerHost")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="ServerHost", Order=0)]
		public  string gxTpr_Serverhost
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Serverhost);

			}
			set { 
				 sdt.gxTpr_Serverhost = value;
			}
		}

		[JsonPropertyName("ServerPort")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="ServerPort", Order=1)]
		public int gxTpr_Serverport
		{
			get { 
				return sdt.gxTpr_Serverport;

			}
			set { 
				sdt.gxTpr_Serverport = value;
			}
		}

		[JsonPropertyName("ServerSecure")]
		[JsonPropertyOrder(2)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="ServerSecure", Order=2)]
		public bool gxTpr_Serversecure
		{
			get { 
				return sdt.gxTpr_Serversecure;

			}
			set { 
				sdt.gxTpr_Serversecure = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtServerParameters sdt
		{
			get { 
				return (SdtServerParameters)Sdt;
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
				sdt = new SdtServerParameters() ;
			}
		}
	}
	#endregion
}