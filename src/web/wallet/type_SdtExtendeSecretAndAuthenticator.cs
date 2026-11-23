/*
				   File: type_SdtExtendeSecretAndAuthenticator
			Description: ExtendeSecretAndAuthenticator
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="ExtendeSecretAndAuthenticator")]
	[XmlType(TypeName="ExtendeSecretAndAuthenticator" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtExtendeSecretAndAuthenticator : GxUserType
	{
		public SdtExtendeSecretAndAuthenticator( )
		{
			/* Constructor for serialization */
			gxTv_SdtExtendeSecretAndAuthenticator_Authenticatorbase32 = "";

			gxTv_SdtExtendeSecretAndAuthenticator_Networktype = "";

			gxTv_SdtExtendeSecretAndAuthenticator_Oraclekey = "";

			gxTv_SdtExtendeSecretAndAuthenticator_Seed = "";

		}

		public SdtExtendeSecretAndAuthenticator(IGxContext context)
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
			AddObjectProperty("AuthenticatorBase32", gxTpr_Authenticatorbase32, false);


			AddObjectProperty("NetworkType", gxTpr_Networktype, false);


			AddObjectProperty("OracleKey", gxTpr_Oraclekey, false);


			AddObjectProperty("Seed", gxTpr_Seed, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="AuthenticatorBase32")]
		[XmlElement(ElementName="AuthenticatorBase32")]
		public string gxTpr_Authenticatorbase32
		{
			get {
				return gxTv_SdtExtendeSecretAndAuthenticator_Authenticatorbase32; 
			}
			set {
				gxTv_SdtExtendeSecretAndAuthenticator_Authenticatorbase32 = value;
				SetDirty("Authenticatorbase32");
			}
		}




		[SoapElement(ElementName="NetworkType")]
		[XmlElement(ElementName="NetworkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtExtendeSecretAndAuthenticator_Networktype; 
			}
			set {
				gxTv_SdtExtendeSecretAndAuthenticator_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="OracleKey")]
		[XmlElement(ElementName="OracleKey")]
		public string gxTpr_Oraclekey
		{
			get {
				return gxTv_SdtExtendeSecretAndAuthenticator_Oraclekey; 
			}
			set {
				gxTv_SdtExtendeSecretAndAuthenticator_Oraclekey = value;
				SetDirty("Oraclekey");
			}
		}




		[SoapElement(ElementName="Seed")]
		[XmlElement(ElementName="Seed")]
		public string gxTpr_Seed
		{
			get {
				return gxTv_SdtExtendeSecretAndAuthenticator_Seed; 
			}
			set {
				gxTv_SdtExtendeSecretAndAuthenticator_Seed = value;
				SetDirty("Seed");
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
			gxTv_SdtExtendeSecretAndAuthenticator_Authenticatorbase32 = "";
			gxTv_SdtExtendeSecretAndAuthenticator_Networktype = "";
			gxTv_SdtExtendeSecretAndAuthenticator_Oraclekey = "";
			gxTv_SdtExtendeSecretAndAuthenticator_Seed = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtExtendeSecretAndAuthenticator_Authenticatorbase32;
		 

		protected string gxTv_SdtExtendeSecretAndAuthenticator_Networktype;
		 

		protected string gxTv_SdtExtendeSecretAndAuthenticator_Oraclekey;
		 

		protected string gxTv_SdtExtendeSecretAndAuthenticator_Seed;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ExtendeSecretAndAuthenticator", Namespace="distributedcryptography")]
	public class SdtExtendeSecretAndAuthenticator_RESTInterface : GxGenericCollectionItem<SdtExtendeSecretAndAuthenticator>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtExtendeSecretAndAuthenticator_RESTInterface( ) : base()
		{	
		}

		public SdtExtendeSecretAndAuthenticator_RESTInterface( SdtExtendeSecretAndAuthenticator psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("AuthenticatorBase32")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="AuthenticatorBase32", Order=0)]
		public  string gxTpr_Authenticatorbase32
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Authenticatorbase32);

			}
			set { 
				 sdt.gxTpr_Authenticatorbase32 = value;
			}
		}

		[JsonPropertyName("NetworkType")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="NetworkType", Order=1)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[JsonPropertyName("OracleKey")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="OracleKey", Order=2)]
		public  string gxTpr_Oraclekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Oraclekey);

			}
			set { 
				 sdt.gxTpr_Oraclekey = value;
			}
		}

		[JsonPropertyName("Seed")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="Seed", Order=3)]
		public  string gxTpr_Seed
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Seed);

			}
			set { 
				 sdt.gxTpr_Seed = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtExtendeSecretAndAuthenticator sdt
		{
			get { 
				return (SdtExtendeSecretAndAuthenticator)Sdt;
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
				sdt = new SdtExtendeSecretAndAuthenticator() ;
			}
		}
	}
	#endregion
}