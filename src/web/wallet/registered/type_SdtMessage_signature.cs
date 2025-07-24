/*
				   File: type_SdtMessage_signature
			Description: Message_signature
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
using GeneXus.Programs.wallet;
namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="Message_signature")]
	[XmlType(TypeName="Message_signature" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMessage_signature : GxUserType
	{
		public SdtMessage_signature( )
		{
			/* Constructor for serialization */
			gxTv_SdtMessage_signature_Username = "";

			gxTv_SdtMessage_signature_Pubkey = "";

			gxTv_SdtMessage_signature_Newpubkey = "";

			gxTv_SdtMessage_signature_Grouppubkey = "";

			gxTv_SdtMessage_signature_Signature = "";

		}

		public SdtMessage_signature(IGxContext context)
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
			AddObjectProperty("UserName", gxTpr_Username, false);


			AddObjectProperty("PubKey", gxTpr_Pubkey, false);


			AddObjectProperty("NewPubKey", gxTpr_Newpubkey, false);


			AddObjectProperty("GroupPubKey", gxTpr_Grouppubkey, false);


			AddObjectProperty("Signature", gxTpr_Signature, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="UserName")]
		[XmlElement(ElementName="UserName")]
		public string gxTpr_Username
		{
			get {
				return gxTv_SdtMessage_signature_Username; 
			}
			set {
				gxTv_SdtMessage_signature_Username = value;
				SetDirty("Username");
			}
		}




		[SoapElement(ElementName="PubKey")]
		[XmlElement(ElementName="PubKey")]
		public string gxTpr_Pubkey
		{
			get {
				return gxTv_SdtMessage_signature_Pubkey; 
			}
			set {
				gxTv_SdtMessage_signature_Pubkey = value;
				SetDirty("Pubkey");
			}
		}




		[SoapElement(ElementName="NewPubKey")]
		[XmlElement(ElementName="NewPubKey")]
		public string gxTpr_Newpubkey
		{
			get {
				return gxTv_SdtMessage_signature_Newpubkey; 
			}
			set {
				gxTv_SdtMessage_signature_Newpubkey = value;
				SetDirty("Newpubkey");
			}
		}




		[SoapElement(ElementName="GroupPubKey")]
		[XmlElement(ElementName="GroupPubKey")]
		public string gxTpr_Grouppubkey
		{
			get {
				return gxTv_SdtMessage_signature_Grouppubkey; 
			}
			set {
				gxTv_SdtMessage_signature_Grouppubkey = value;
				SetDirty("Grouppubkey");
			}
		}




		[SoapElement(ElementName="Signature")]
		[XmlElement(ElementName="Signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtMessage_signature_Signature; 
			}
			set {
				gxTv_SdtMessage_signature_Signature = value;
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
			gxTv_SdtMessage_signature_Username = "";
			gxTv_SdtMessage_signature_Pubkey = "";
			gxTv_SdtMessage_signature_Newpubkey = "";
			gxTv_SdtMessage_signature_Grouppubkey = "";
			gxTv_SdtMessage_signature_Signature = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtMessage_signature_Username;
		 

		protected string gxTv_SdtMessage_signature_Pubkey;
		 

		protected string gxTv_SdtMessage_signature_Newpubkey;
		 

		protected string gxTv_SdtMessage_signature_Grouppubkey;
		 

		protected string gxTv_SdtMessage_signature_Signature;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Message_signature", Namespace="distributedcryptography")]
	public class SdtMessage_signature_RESTInterface : GxGenericCollectionItem<SdtMessage_signature>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMessage_signature_RESTInterface( ) : base()
		{	
		}

		public SdtMessage_signature_RESTInterface( SdtMessage_signature psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("UserName")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="UserName", Order=0)]
		public  string gxTpr_Username
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Username);

			}
			set { 
				 sdt.gxTpr_Username = value;
			}
		}

		[JsonPropertyName("PubKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="PubKey", Order=1)]
		public  string gxTpr_Pubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Pubkey);

			}
			set { 
				 sdt.gxTpr_Pubkey = value;
			}
		}

		[JsonPropertyName("NewPubKey")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="NewPubKey", Order=2)]
		public  string gxTpr_Newpubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Newpubkey);

			}
			set { 
				 sdt.gxTpr_Newpubkey = value;
			}
		}

		[JsonPropertyName("GroupPubKey")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="GroupPubKey", Order=3)]
		public  string gxTpr_Grouppubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Grouppubkey);

			}
			set { 
				 sdt.gxTpr_Grouppubkey = value;
			}
		}

		[JsonPropertyName("Signature")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Signature", Order=4)]
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
		public SdtMessage_signature sdt
		{
			get { 
				return (SdtMessage_signature)Sdt;
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
				sdt = new SdtMessage_signature() ;
			}
		}
	}
	#endregion
}