/*
				   File: type_SdtKeyInfo
			Description: KeyInfo
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

using GeneXus.Programs;
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="KeyInfo")]
	[XmlType(TypeName="KeyInfo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtKeyInfo : GxUserType
	{
		public SdtKeyInfo( )
		{
			/* Constructor for serialization */
			gxTv_SdtKeyInfo_Privatekey = "";

			gxTv_SdtKeyInfo_Publickey = "";

			gxTv_SdtKeyInfo_Taprootpubkey = "";

			gxTv_SdtKeyInfo_Publickeyhash = "";

			gxTv_SdtKeyInfo_Scriptpublickey = "";

			gxTv_SdtKeyInfo_Address = "";

			gxTv_SdtKeyInfo_Wif = "";

			gxTv_SdtKeyInfo_Encryptedwif = "";

		}

		public SdtKeyInfo(IGxContext context)
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
			AddObjectProperty("PrivateKey", gxTpr_Privatekey, false);


			AddObjectProperty("PublicKey", gxTpr_Publickey, false);


			AddObjectProperty("TaprootPubKey", gxTpr_Taprootpubkey, false);


			AddObjectProperty("PublicKeyHash", gxTpr_Publickeyhash, false);


			AddObjectProperty("ScriptPublicKey", gxTpr_Scriptpublickey, false);


			AddObjectProperty("Address", gxTpr_Address, false);


			AddObjectProperty("WIF", gxTpr_Wif, false);


			AddObjectProperty("encryptedWIF", gxTpr_Encryptedwif, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="PrivateKey")]
		[XmlElement(ElementName="PrivateKey")]
		public string gxTpr_Privatekey
		{
			get {
				return gxTv_SdtKeyInfo_Privatekey; 
			}
			set {
				gxTv_SdtKeyInfo_Privatekey = value;
				SetDirty("Privatekey");
			}
		}




		[SoapElement(ElementName="PublicKey")]
		[XmlElement(ElementName="PublicKey")]
		public string gxTpr_Publickey
		{
			get {
				return gxTv_SdtKeyInfo_Publickey; 
			}
			set {
				gxTv_SdtKeyInfo_Publickey = value;
				SetDirty("Publickey");
			}
		}




		[SoapElement(ElementName="TaprootPubKey")]
		[XmlElement(ElementName="TaprootPubKey")]
		public string gxTpr_Taprootpubkey
		{
			get {
				return gxTv_SdtKeyInfo_Taprootpubkey; 
			}
			set {
				gxTv_SdtKeyInfo_Taprootpubkey = value;
				SetDirty("Taprootpubkey");
			}
		}




		[SoapElement(ElementName="PublicKeyHash")]
		[XmlElement(ElementName="PublicKeyHash")]
		public string gxTpr_Publickeyhash
		{
			get {
				return gxTv_SdtKeyInfo_Publickeyhash; 
			}
			set {
				gxTv_SdtKeyInfo_Publickeyhash = value;
				SetDirty("Publickeyhash");
			}
		}




		[SoapElement(ElementName="ScriptPublicKey")]
		[XmlElement(ElementName="ScriptPublicKey")]
		public string gxTpr_Scriptpublickey
		{
			get {
				return gxTv_SdtKeyInfo_Scriptpublickey; 
			}
			set {
				gxTv_SdtKeyInfo_Scriptpublickey = value;
				SetDirty("Scriptpublickey");
			}
		}




		[SoapElement(ElementName="Address")]
		[XmlElement(ElementName="Address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtKeyInfo_Address; 
			}
			set {
				gxTv_SdtKeyInfo_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="WIF")]
		[XmlElement(ElementName="WIF")]
		public string gxTpr_Wif
		{
			get {
				return gxTv_SdtKeyInfo_Wif; 
			}
			set {
				gxTv_SdtKeyInfo_Wif = value;
				SetDirty("Wif");
			}
		}




		[SoapElement(ElementName="encryptedWIF")]
		[XmlElement(ElementName="encryptedWIF")]
		public string gxTpr_Encryptedwif
		{
			get {
				return gxTv_SdtKeyInfo_Encryptedwif; 
			}
			set {
				gxTv_SdtKeyInfo_Encryptedwif = value;
				SetDirty("Encryptedwif");
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
			gxTv_SdtKeyInfo_Privatekey = "";
			gxTv_SdtKeyInfo_Publickey = "";
			gxTv_SdtKeyInfo_Taprootpubkey = "";
			gxTv_SdtKeyInfo_Publickeyhash = "";
			gxTv_SdtKeyInfo_Scriptpublickey = "";
			gxTv_SdtKeyInfo_Address = "";
			gxTv_SdtKeyInfo_Wif = "";
			gxTv_SdtKeyInfo_Encryptedwif = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtKeyInfo_Privatekey;
		 

		protected string gxTv_SdtKeyInfo_Publickey;
		 

		protected string gxTv_SdtKeyInfo_Taprootpubkey;
		 

		protected string gxTv_SdtKeyInfo_Publickeyhash;
		 

		protected string gxTv_SdtKeyInfo_Scriptpublickey;
		 

		protected string gxTv_SdtKeyInfo_Address;
		 

		protected string gxTv_SdtKeyInfo_Wif;
		 

		protected string gxTv_SdtKeyInfo_Encryptedwif;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"KeyInfo", Namespace="distributedcryptography")]
	public class SdtKeyInfo_RESTInterface : GxGenericCollectionItem<SdtKeyInfo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtKeyInfo_RESTInterface( ) : base()
		{	
		}

		public SdtKeyInfo_RESTInterface( SdtKeyInfo psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="PrivateKey", Order=0)]
		public  string gxTpr_Privatekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Privatekey);

			}
			set { 
				 sdt.gxTpr_Privatekey = value;
			}
		}

		[DataMember(Name="PublicKey", Order=1)]
		public  string gxTpr_Publickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickey);

			}
			set { 
				 sdt.gxTpr_Publickey = value;
			}
		}

		[DataMember(Name="TaprootPubKey", Order=2)]
		public  string gxTpr_Taprootpubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Taprootpubkey);

			}
			set { 
				 sdt.gxTpr_Taprootpubkey = value;
			}
		}

		[DataMember(Name="PublicKeyHash", Order=3)]
		public  string gxTpr_Publickeyhash
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickeyhash);

			}
			set { 
				 sdt.gxTpr_Publickeyhash = value;
			}
		}

		[DataMember(Name="ScriptPublicKey", Order=4)]
		public  string gxTpr_Scriptpublickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Scriptpublickey);

			}
			set { 
				 sdt.gxTpr_Scriptpublickey = value;
			}
		}

		[DataMember(Name="Address", Order=5)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[DataMember(Name="WIF", Order=6)]
		public  string gxTpr_Wif
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Wif);

			}
			set { 
				 sdt.gxTpr_Wif = value;
			}
		}

		[DataMember(Name="encryptedWIF", Order=7)]
		public  string gxTpr_Encryptedwif
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedwif);

			}
			set { 
				 sdt.gxTpr_Encryptedwif = value;
			}
		}


		#endregion

		public SdtKeyInfo sdt
		{
			get { 
				return (SdtKeyInfo)Sdt;
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
				sdt = new SdtKeyInfo() ;
			}
		}
	}
	#endregion
}