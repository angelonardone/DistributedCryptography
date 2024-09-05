/*
				   File: type_SdtWallet
			Description: Wallet
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
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
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="Wallet")]
	[XmlType(TypeName="Wallet" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtWallet : GxUserType
	{
		public SdtWallet( )
		{
			/* Constructor for serialization */
			gxTv_SdtWallet_Walletname = "";

			gxTv_SdtWallet_Wallettype = "";

			gxTv_SdtWallet_Encryptedsecret = "";

			gxTv_SdtWallet_Extencryptedsecret = "";

			gxTv_SdtWallet_Networktype = "";

			gxTv_SdtWallet_Walletfilename = "";

			gxTv_SdtWallet_Walletbasedirectory = "";

		}

		public SdtWallet(IGxContext context)
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
			AddObjectProperty("WalletName", gxTpr_Walletname, false);


			AddObjectProperty("WalletType", gxTpr_Wallettype, false);


			AddObjectProperty("EncryptedSecret", gxTpr_Encryptedsecret, false);


			AddObjectProperty("ExtEncryptedSecret", gxTpr_Extencryptedsecret, false);


			AddObjectProperty("useAuthenticator", gxTpr_Useauthenticator, false);


			AddObjectProperty("NetworkType", gxTpr_Networktype, false);


			AddObjectProperty("WalletFileName", gxTpr_Walletfilename, false);


			AddObjectProperty("WalletBaseDirectory", gxTpr_Walletbasedirectory, false);


			AddObjectProperty("WalletReadBalanceOnStart", gxTpr_Walletreadbalanceonstart, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="WalletName")]
		[XmlElement(ElementName="WalletName")]
		public string gxTpr_Walletname
		{
			get {
				return gxTv_SdtWallet_Walletname; 
			}
			set {
				gxTv_SdtWallet_Walletname = value;
				SetDirty("Walletname");
			}
		}




		[SoapElement(ElementName="WalletType")]
		[XmlElement(ElementName="WalletType")]
		public string gxTpr_Wallettype
		{
			get {
				return gxTv_SdtWallet_Wallettype; 
			}
			set {
				gxTv_SdtWallet_Wallettype = value;
				SetDirty("Wallettype");
			}
		}




		[SoapElement(ElementName="EncryptedSecret")]
		[XmlElement(ElementName="EncryptedSecret")]
		public string gxTpr_Encryptedsecret
		{
			get {
				return gxTv_SdtWallet_Encryptedsecret; 
			}
			set {
				gxTv_SdtWallet_Encryptedsecret = value;
				SetDirty("Encryptedsecret");
			}
		}




		[SoapElement(ElementName="ExtEncryptedSecret")]
		[XmlElement(ElementName="ExtEncryptedSecret")]
		public string gxTpr_Extencryptedsecret
		{
			get {
				return gxTv_SdtWallet_Extencryptedsecret; 
			}
			set {
				gxTv_SdtWallet_Extencryptedsecret = value;
				SetDirty("Extencryptedsecret");
			}
		}




		[SoapElement(ElementName="useAuthenticator")]
		[XmlElement(ElementName="useAuthenticator")]
		public bool gxTpr_Useauthenticator
		{
			get {
				return gxTv_SdtWallet_Useauthenticator; 
			}
			set {
				gxTv_SdtWallet_Useauthenticator = value;
				SetDirty("Useauthenticator");
			}
		}




		[SoapElement(ElementName="NetworkType")]
		[XmlElement(ElementName="NetworkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtWallet_Networktype; 
			}
			set {
				gxTv_SdtWallet_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="WalletFileName")]
		[XmlElement(ElementName="WalletFileName")]
		public string gxTpr_Walletfilename
		{
			get {
				return gxTv_SdtWallet_Walletfilename; 
			}
			set {
				gxTv_SdtWallet_Walletfilename = value;
				SetDirty("Walletfilename");
			}
		}




		[SoapElement(ElementName="WalletBaseDirectory")]
		[XmlElement(ElementName="WalletBaseDirectory")]
		public string gxTpr_Walletbasedirectory
		{
			get {
				return gxTv_SdtWallet_Walletbasedirectory; 
			}
			set {
				gxTv_SdtWallet_Walletbasedirectory = value;
				SetDirty("Walletbasedirectory");
			}
		}




		[SoapElement(ElementName="WalletReadBalanceOnStart")]
		[XmlElement(ElementName="WalletReadBalanceOnStart")]
		public bool gxTpr_Walletreadbalanceonstart
		{
			get {
				return gxTv_SdtWallet_Walletreadbalanceonstart; 
			}
			set {
				gxTv_SdtWallet_Walletreadbalanceonstart = value;
				SetDirty("Walletreadbalanceonstart");
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
			gxTv_SdtWallet_Walletname = "";
			gxTv_SdtWallet_Wallettype = "";
			gxTv_SdtWallet_Encryptedsecret = "";
			gxTv_SdtWallet_Extencryptedsecret = "";

			gxTv_SdtWallet_Networktype = "";
			gxTv_SdtWallet_Walletfilename = "";
			gxTv_SdtWallet_Walletbasedirectory = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtWallet_Walletname;
		 

		protected string gxTv_SdtWallet_Wallettype;
		 

		protected string gxTv_SdtWallet_Encryptedsecret;
		 

		protected string gxTv_SdtWallet_Extencryptedsecret;
		 

		protected bool gxTv_SdtWallet_Useauthenticator;
		 

		protected string gxTv_SdtWallet_Networktype;
		 

		protected string gxTv_SdtWallet_Walletfilename;
		 

		protected string gxTv_SdtWallet_Walletbasedirectory;
		 

		protected bool gxTv_SdtWallet_Walletreadbalanceonstart;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Wallet", Namespace="distributedcryptography")]
	public class SdtWallet_RESTInterface : GxGenericCollectionItem<SdtWallet>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWallet_RESTInterface( ) : base()
		{	
		}

		public SdtWallet_RESTInterface( SdtWallet psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="WalletName", Order=0)]
		public  string gxTpr_Walletname
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Walletname);

			}
			set { 
				 sdt.gxTpr_Walletname = value;
			}
		}

		[DataMember(Name="WalletType", Order=1)]
		public  string gxTpr_Wallettype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Wallettype);

			}
			set { 
				 sdt.gxTpr_Wallettype = value;
			}
		}

		[DataMember(Name="EncryptedSecret", Order=2)]
		public  string gxTpr_Encryptedsecret
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedsecret);

			}
			set { 
				 sdt.gxTpr_Encryptedsecret = value;
			}
		}

		[DataMember(Name="ExtEncryptedSecret", Order=3)]
		public  string gxTpr_Extencryptedsecret
		{
			get { 
				return sdt.gxTpr_Extencryptedsecret;

			}
			set { 
				 sdt.gxTpr_Extencryptedsecret = value;
			}
		}

		[DataMember(Name="useAuthenticator", Order=4)]
		public bool gxTpr_Useauthenticator
		{
			get { 
				return sdt.gxTpr_Useauthenticator;

			}
			set { 
				sdt.gxTpr_Useauthenticator = value;
			}
		}

		[DataMember(Name="NetworkType", Order=5)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[DataMember(Name="WalletFileName", Order=6)]
		public  string gxTpr_Walletfilename
		{
			get { 
				return sdt.gxTpr_Walletfilename;

			}
			set { 
				 sdt.gxTpr_Walletfilename = value;
			}
		}

		[DataMember(Name="WalletBaseDirectory", Order=7)]
		public  string gxTpr_Walletbasedirectory
		{
			get { 
				return sdt.gxTpr_Walletbasedirectory;

			}
			set { 
				 sdt.gxTpr_Walletbasedirectory = value;
			}
		}

		[DataMember(Name="WalletReadBalanceOnStart", Order=8)]
		public bool gxTpr_Walletreadbalanceonstart
		{
			get { 
				return sdt.gxTpr_Walletreadbalanceonstart;

			}
			set { 
				sdt.gxTpr_Walletreadbalanceonstart = value;
			}
		}


		#endregion

		public SdtWallet sdt
		{
			get { 
				return (SdtWallet)Sdt;
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
				sdt = new SdtWallet() ;
			}
		}
	}
	#endregion
}