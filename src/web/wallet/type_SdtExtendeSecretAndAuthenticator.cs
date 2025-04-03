/*
				   File: type_SdtExtendeSecretAndAuthenticator
			Description: ExtendeSecretAndAuthenticator
				 Author: Nemo 🐠 for C# (.NET) version 18.0.12.186073
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
			gxTv_SdtExtendeSecretAndAuthenticator_Extencryptedsecret = "";

			gxTv_SdtExtendeSecretAndAuthenticator_Authenticatorbase32 = "";

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
			AddObjectProperty("ExtEncryptedSecret", gxTpr_Extencryptedsecret, false);


			AddObjectProperty("AuthenticatorBase32", gxTpr_Authenticatorbase32, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ExtEncryptedSecret")]
		[XmlElement(ElementName="ExtEncryptedSecret")]
		public string gxTpr_Extencryptedsecret
		{
			get {
				return gxTv_SdtExtendeSecretAndAuthenticator_Extencryptedsecret; 
			}
			set {
				gxTv_SdtExtendeSecretAndAuthenticator_Extencryptedsecret = value;
				SetDirty("Extencryptedsecret");
			}
		}




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
			gxTv_SdtExtendeSecretAndAuthenticator_Extencryptedsecret = "";
			gxTv_SdtExtendeSecretAndAuthenticator_Authenticatorbase32 = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtExtendeSecretAndAuthenticator_Extencryptedsecret;
		 

		protected string gxTv_SdtExtendeSecretAndAuthenticator_Authenticatorbase32;
		 


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
		[JsonPropertyName("ExtEncryptedSecret")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="ExtEncryptedSecret", Order=0)]
		public  string gxTpr_Extencryptedsecret
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extencryptedsecret);

			}
			set { 
				 sdt.gxTpr_Extencryptedsecret = value;
			}
		}

		[JsonPropertyName("AuthenticatorBase32")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="AuthenticatorBase32", Order=1)]
		public  string gxTpr_Authenticatorbase32
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Authenticatorbase32);

			}
			set { 
				 sdt.gxTpr_Authenticatorbase32 = value;
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