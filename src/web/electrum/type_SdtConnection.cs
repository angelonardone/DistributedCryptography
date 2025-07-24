/*
				   File: type_SdtConnection
			Description: Connection
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186676
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
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="Connection")]
	[XmlType(TypeName="Connection" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtConnection : GxUserType
	{
		public SdtConnection( )
		{
			/* Constructor for serialization */
			gxTv_SdtConnection_Connectiontype = "";

		}

		public SdtConnection(IGxContext context)
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
			AddObjectProperty("ConnectionId", gxTpr_Connectionid, false);


			AddObjectProperty("ConnectionType", gxTpr_Connectiontype, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ConnectionId")]
		[XmlElement(ElementName="ConnectionId")]
		public Guid gxTpr_Connectionid
		{
			get {
				return gxTv_SdtConnection_Connectionid; 
			}
			set {
				gxTv_SdtConnection_Connectionid = value;
				SetDirty("Connectionid");
			}
		}




		[SoapElement(ElementName="ConnectionType")]
		[XmlElement(ElementName="ConnectionType")]
		public string gxTpr_Connectiontype
		{
			get {
				return gxTv_SdtConnection_Connectiontype; 
			}
			set {
				gxTv_SdtConnection_Connectiontype = value;
				SetDirty("Connectiontype");
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
			gxTv_SdtConnection_Connectiontype = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtConnection_Connectionid;
		 

		protected string gxTv_SdtConnection_Connectiontype;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Connection", Namespace="distributedcryptography")]
	public class SdtConnection_RESTInterface : GxGenericCollectionItem<SdtConnection>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtConnection_RESTInterface( ) : base()
		{	
		}

		public SdtConnection_RESTInterface( SdtConnection psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("ConnectionId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="ConnectionId", Order=0)]
		public Guid gxTpr_Connectionid
		{
			get { 
				return sdt.gxTpr_Connectionid;

			}
			set { 
				sdt.gxTpr_Connectionid = value;
			}
		}

		[JsonPropertyName("ConnectionType")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="ConnectionType", Order=1)]
		public  string gxTpr_Connectiontype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Connectiontype);

			}
			set { 
				 sdt.gxTpr_Connectiontype = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtConnection sdt
		{
			get { 
				return (SdtConnection)Sdt;
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
				sdt = new SdtConnection() ;
			}
		}
	}
	#endregion
}