/*
				   File: type_SdtWalletInfo
			Description: WalletInfo
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
	[XmlRoot(ElementName="WalletInfo")]
	[XmlType(TypeName="WalletInfo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtWalletInfo : GxUserType
	{
		public SdtWalletInfo( )
		{
			/* Constructor for serialization */
			gxTv_SdtWalletInfo_Walletname = "";

			gxTv_SdtWalletInfo_Wallettype = "";

			gxTv_SdtWalletInfo_Networktype = "";


		}

		public SdtWalletInfo(IGxContext context)
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


			AddObjectProperty("useAuthenticator", gxTpr_Useauthenticator, false);


			AddObjectProperty("NetworkType", gxTpr_Networktype, false);


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
				return gxTv_SdtWalletInfo_Walletname; 
			}
			set {
				gxTv_SdtWalletInfo_Walletname = value;
				SetDirty("Walletname");
			}
		}




		[SoapElement(ElementName="WalletType")]
		[XmlElement(ElementName="WalletType")]
		public string gxTpr_Wallettype
		{
			get {
				return gxTv_SdtWalletInfo_Wallettype; 
			}
			set {
				gxTv_SdtWalletInfo_Wallettype = value;
				SetDirty("Wallettype");
			}
		}




		[SoapElement(ElementName="useAuthenticator")]
		[XmlElement(ElementName="useAuthenticator")]
		public bool gxTpr_Useauthenticator
		{
			get {
				return gxTv_SdtWalletInfo_Useauthenticator; 
			}
			set {
				gxTv_SdtWalletInfo_Useauthenticator = value;
				SetDirty("Useauthenticator");
			}
		}




		[SoapElement(ElementName="NetworkType")]
		[XmlElement(ElementName="NetworkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtWalletInfo_Networktype; 
			}
			set {
				gxTv_SdtWalletInfo_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="WalletReadBalanceOnStart")]
		[XmlElement(ElementName="WalletReadBalanceOnStart")]
		public bool gxTpr_Walletreadbalanceonstart
		{
			get {
				return gxTv_SdtWalletInfo_Walletreadbalanceonstart; 
			}
			set {
				gxTv_SdtWalletInfo_Walletreadbalanceonstart = value;
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
			gxTv_SdtWalletInfo_Walletname = "";
			gxTv_SdtWalletInfo_Wallettype = "";

			gxTv_SdtWalletInfo_Networktype = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtWalletInfo_Walletname;
		 

		protected string gxTv_SdtWalletInfo_Wallettype;
		 

		protected bool gxTv_SdtWalletInfo_Useauthenticator;
		 

		protected string gxTv_SdtWalletInfo_Networktype;
		 

		protected bool gxTv_SdtWalletInfo_Walletreadbalanceonstart;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"WalletInfo", Namespace="distributedcryptography")]
	public class SdtWalletInfo_RESTInterface : GxGenericCollectionItem<SdtWalletInfo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWalletInfo_RESTInterface( ) : base()
		{	
		}

		public SdtWalletInfo_RESTInterface( SdtWalletInfo psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("WalletName")]
		[JsonPropertyOrder(0)]
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

		[JsonPropertyName("WalletType")]
		[JsonPropertyOrder(1)]
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

		[JsonPropertyName("useAuthenticator")]
		[JsonPropertyOrder(2)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="useAuthenticator", Order=2)]
		public bool gxTpr_Useauthenticator
		{
			get { 
				return sdt.gxTpr_Useauthenticator;

			}
			set { 
				sdt.gxTpr_Useauthenticator = value;
			}
		}

		[JsonPropertyName("NetworkType")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="NetworkType", Order=3)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[JsonPropertyName("WalletReadBalanceOnStart")]
		[JsonPropertyOrder(4)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="WalletReadBalanceOnStart", Order=4)]
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
		[JsonIgnore]
		public SdtWalletInfo sdt
		{
			get { 
				return (SdtWalletInfo)Sdt;
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
				sdt = new SdtWalletInfo() ;
			}
		}
	}
	#endregion
}