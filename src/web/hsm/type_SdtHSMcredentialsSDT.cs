/*
				   File: type_SdtHSMcredentialsSDT
			Description: HSMcredentialsSDT
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

namespace GeneXus.Programs.hsm
{
	[XmlRoot(ElementName="HSMcredentialsSDT")]
	[XmlType(TypeName="HSMcredentialsSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtHSMcredentialsSDT : GxUserType
	{
		public SdtHSMcredentialsSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtHSMcredentialsSDT_Authorization = "";

			gxTv_SdtHSMcredentialsSDT_Timestamp = "";

			gxTv_SdtHSMcredentialsSDT_Nonce = "";

			gxTv_SdtHSMcredentialsSDT_Signature = "";

		}

		public SdtHSMcredentialsSDT(IGxContext context)
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
			AddObjectProperty("authorization", gxTpr_Authorization, false);


			AddObjectProperty("timestamp", gxTpr_Timestamp, false);


			AddObjectProperty("nonce", gxTpr_Nonce, false);


			AddObjectProperty("signature", gxTpr_Signature, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="authorization")]
		[XmlElement(ElementName="authorization")]
		public string gxTpr_Authorization
		{
			get {
				return gxTv_SdtHSMcredentialsSDT_Authorization; 
			}
			set {
				gxTv_SdtHSMcredentialsSDT_Authorization = value;
				SetDirty("Authorization");
			}
		}




		[SoapElement(ElementName="timestamp")]
		[XmlElement(ElementName="timestamp")]
		public string gxTpr_Timestamp
		{
			get {
				return gxTv_SdtHSMcredentialsSDT_Timestamp; 
			}
			set {
				gxTv_SdtHSMcredentialsSDT_Timestamp = value;
				SetDirty("Timestamp");
			}
		}




		[SoapElement(ElementName="nonce")]
		[XmlElement(ElementName="nonce")]
		public string gxTpr_Nonce
		{
			get {
				return gxTv_SdtHSMcredentialsSDT_Nonce; 
			}
			set {
				gxTv_SdtHSMcredentialsSDT_Nonce = value;
				SetDirty("Nonce");
			}
		}




		[SoapElement(ElementName="signature")]
		[XmlElement(ElementName="signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtHSMcredentialsSDT_Signature; 
			}
			set {
				gxTv_SdtHSMcredentialsSDT_Signature = value;
				SetDirty("Signature");
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
			gxTv_SdtHSMcredentialsSDT_Authorization = "";
			gxTv_SdtHSMcredentialsSDT_Timestamp = "";
			gxTv_SdtHSMcredentialsSDT_Nonce = "";
			gxTv_SdtHSMcredentialsSDT_Signature = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtHSMcredentialsSDT_Authorization;
		 

		protected string gxTv_SdtHSMcredentialsSDT_Timestamp;
		 

		protected string gxTv_SdtHSMcredentialsSDT_Nonce;
		 

		protected string gxTv_SdtHSMcredentialsSDT_Signature;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"HSMcredentialsSDT", Namespace="distributedcryptography")]
	public class SdtHSMcredentialsSDT_RESTInterface : GxGenericCollectionItem<SdtHSMcredentialsSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtHSMcredentialsSDT_RESTInterface( ) : base()
		{	
		}

		public SdtHSMcredentialsSDT_RESTInterface( SdtHSMcredentialsSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("authorization")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="authorization", Order=0)]
		public  string gxTpr_Authorization
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Authorization);

			}
			set { 
				 sdt.gxTpr_Authorization = value;
			}
		}

		[JsonPropertyName("timestamp")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="timestamp", Order=1)]
		public  string gxTpr_Timestamp
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Timestamp);

			}
			set { 
				 sdt.gxTpr_Timestamp = value;
			}
		}

		[JsonPropertyName("nonce")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="nonce", Order=2)]
		public  string gxTpr_Nonce
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Nonce);

			}
			set { 
				 sdt.gxTpr_Nonce = value;
			}
		}

		[JsonPropertyName("signature")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="signature", Order=3)]
		public  string gxTpr_Signature
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signature);

			}
			set { 
				 sdt.gxTpr_Signature = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtHSMcredentialsSDT sdt
		{
			get { 
				return (SdtHSMcredentialsSDT)Sdt;
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
				sdt = new SdtHSMcredentialsSDT() ;
			}
		}
	}
	#endregion
}