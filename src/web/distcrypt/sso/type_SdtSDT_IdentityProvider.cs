/*
				   File: type_SdtSDT_IdentityProvider
			Description: SDT_IdentityProvider
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
using GeneXus.Programs.distcrypt;
namespace GeneXus.Programs.distcrypt.sso
{
	[XmlRoot(ElementName="SDT_IdentityProvider")]
	[XmlType(TypeName="SDT_IdentityProvider" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_IdentityProvider : GxUserType
	{
		public SdtSDT_IdentityProvider( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_IdentityProvider_Identityproviderhost = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderapplication = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderclientid = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderclientsecret = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderredirurl = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderdynamiccallprocedure = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderrepository = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderencryptionkey = "";

		}

		public SdtSDT_IdentityProvider(IGxContext context)
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
			AddObjectProperty("IdentityProviderId", gxTpr_Identityproviderid, false);


			AddObjectProperty("IdentityProviderHost", gxTpr_Identityproviderhost, false);


			AddObjectProperty("IdentityProviderPort", gxTpr_Identityproviderport, false);


			AddObjectProperty("IdentityProviderApplication", gxTpr_Identityproviderapplication, false);


			AddObjectProperty("IdentityProviderClientId", gxTpr_Identityproviderclientid, false);


			AddObjectProperty("IdentityProviderClientSecret", gxTpr_Identityproviderclientsecret, false);


			AddObjectProperty("IdentityProviderSecure", gxTpr_Identityprovidersecure, false);


			AddObjectProperty("IdentityProviderRedirUrl", gxTpr_Identityproviderredirurl, false);


			AddObjectProperty("IdentityProviderDynamicCallProcedure", gxTpr_Identityproviderdynamiccallprocedure, false);


			AddObjectProperty("IdentityProviderEncryptParameter", gxTpr_Identityproviderencryptparameter, false);


			AddObjectProperty("IdentityProviderRepository", gxTpr_Identityproviderrepository, false);


			AddObjectProperty("IdentityProviderEncryptionKey", gxTpr_Identityproviderencryptionkey, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="IdentityProviderId")]
		[XmlElement(ElementName="IdentityProviderId")]
		public short gxTpr_Identityproviderid
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderid; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderid = value;
				SetDirty("Identityproviderid");
			}
		}




		[SoapElement(ElementName="IdentityProviderHost")]
		[XmlElement(ElementName="IdentityProviderHost")]
		public string gxTpr_Identityproviderhost
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderhost; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderhost = value;
				SetDirty("Identityproviderhost");
			}
		}




		[SoapElement(ElementName="IdentityProviderPort")]
		[XmlElement(ElementName="IdentityProviderPort")]
		public short gxTpr_Identityproviderport
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderport; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderport = value;
				SetDirty("Identityproviderport");
			}
		}




		[SoapElement(ElementName="IdentityProviderApplication")]
		[XmlElement(ElementName="IdentityProviderApplication")]
		public string gxTpr_Identityproviderapplication
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderapplication; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderapplication = value;
				SetDirty("Identityproviderapplication");
			}
		}




		[SoapElement(ElementName="IdentityProviderClientId")]
		[XmlElement(ElementName="IdentityProviderClientId")]
		public string gxTpr_Identityproviderclientid
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderclientid; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderclientid = value;
				SetDirty("Identityproviderclientid");
			}
		}




		[SoapElement(ElementName="IdentityProviderClientSecret")]
		[XmlElement(ElementName="IdentityProviderClientSecret")]
		public string gxTpr_Identityproviderclientsecret
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderclientsecret; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderclientsecret = value;
				SetDirty("Identityproviderclientsecret");
			}
		}




		[SoapElement(ElementName="IdentityProviderSecure")]
		[XmlElement(ElementName="IdentityProviderSecure")]
		public bool gxTpr_Identityprovidersecure
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityprovidersecure; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityprovidersecure = value;
				SetDirty("Identityprovidersecure");
			}
		}




		[SoapElement(ElementName="IdentityProviderRedirUrl")]
		[XmlElement(ElementName="IdentityProviderRedirUrl")]
		public string gxTpr_Identityproviderredirurl
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderredirurl; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderredirurl = value;
				SetDirty("Identityproviderredirurl");
			}
		}




		[SoapElement(ElementName="IdentityProviderDynamicCallProcedure")]
		[XmlElement(ElementName="IdentityProviderDynamicCallProcedure")]
		public string gxTpr_Identityproviderdynamiccallprocedure
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderdynamiccallprocedure; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderdynamiccallprocedure = value;
				SetDirty("Identityproviderdynamiccallprocedure");
			}
		}




		[SoapElement(ElementName="IdentityProviderEncryptParameter")]
		[XmlElement(ElementName="IdentityProviderEncryptParameter")]
		public bool gxTpr_Identityproviderencryptparameter
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderencryptparameter; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderencryptparameter = value;
				SetDirty("Identityproviderencryptparameter");
			}
		}




		[SoapElement(ElementName="IdentityProviderRepository")]
		[XmlElement(ElementName="IdentityProviderRepository")]
		public string gxTpr_Identityproviderrepository
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderrepository; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderrepository = value;
				SetDirty("Identityproviderrepository");
			}
		}




		[SoapElement(ElementName="IdentityProviderEncryptionKey")]
		[XmlElement(ElementName="IdentityProviderEncryptionKey")]
		public string gxTpr_Identityproviderencryptionkey
		{
			get {
				return gxTv_SdtSDT_IdentityProvider_Identityproviderencryptionkey; 
			}
			set {
				gxTv_SdtSDT_IdentityProvider_Identityproviderencryptionkey = value;
				SetDirty("Identityproviderencryptionkey");
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
			gxTv_SdtSDT_IdentityProvider_Identityproviderhost = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderapplication = "";
			gxTv_SdtSDT_IdentityProvider_Identityproviderclientid = "";
			gxTv_SdtSDT_IdentityProvider_Identityproviderclientsecret = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderredirurl = "";
			gxTv_SdtSDT_IdentityProvider_Identityproviderdynamiccallprocedure = "";

			gxTv_SdtSDT_IdentityProvider_Identityproviderrepository = "";
			gxTv_SdtSDT_IdentityProvider_Identityproviderencryptionkey = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected short gxTv_SdtSDT_IdentityProvider_Identityproviderid;
		 

		protected string gxTv_SdtSDT_IdentityProvider_Identityproviderhost;
		 

		protected short gxTv_SdtSDT_IdentityProvider_Identityproviderport;
		 

		protected string gxTv_SdtSDT_IdentityProvider_Identityproviderapplication;
		 

		protected string gxTv_SdtSDT_IdentityProvider_Identityproviderclientid;
		 

		protected string gxTv_SdtSDT_IdentityProvider_Identityproviderclientsecret;
		 

		protected bool gxTv_SdtSDT_IdentityProvider_Identityprovidersecure;
		 

		protected string gxTv_SdtSDT_IdentityProvider_Identityproviderredirurl;
		 

		protected string gxTv_SdtSDT_IdentityProvider_Identityproviderdynamiccallprocedure;
		 

		protected bool gxTv_SdtSDT_IdentityProvider_Identityproviderencryptparameter;
		 

		protected string gxTv_SdtSDT_IdentityProvider_Identityproviderrepository;
		 

		protected string gxTv_SdtSDT_IdentityProvider_Identityproviderencryptionkey;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"SDT_IdentityProvider", Namespace="distributedcryptography")]
	public class SdtSDT_IdentityProvider_RESTInterface : GxGenericCollectionItem<SdtSDT_IdentityProvider>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_IdentityProvider_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_IdentityProvider_RESTInterface( SdtSDT_IdentityProvider psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="IdentityProviderId", Order=0)]
		public short gxTpr_Identityproviderid
		{
			get { 
				return sdt.gxTpr_Identityproviderid;

			}
			set { 
				sdt.gxTpr_Identityproviderid = value;
			}
		}

		[DataMember(Name="IdentityProviderHost", Order=1)]
		public  string gxTpr_Identityproviderhost
		{
			get { 
				return sdt.gxTpr_Identityproviderhost;

			}
			set { 
				 sdt.gxTpr_Identityproviderhost = value;
			}
		}

		[DataMember(Name="IdentityProviderPort", Order=2)]
		public short gxTpr_Identityproviderport
		{
			get { 
				return sdt.gxTpr_Identityproviderport;

			}
			set { 
				sdt.gxTpr_Identityproviderport = value;
			}
		}

		[DataMember(Name="IdentityProviderApplication", Order=3)]
		public  string gxTpr_Identityproviderapplication
		{
			get { 
				return sdt.gxTpr_Identityproviderapplication;

			}
			set { 
				 sdt.gxTpr_Identityproviderapplication = value;
			}
		}

		[DataMember(Name="IdentityProviderClientId", Order=4)]
		public  string gxTpr_Identityproviderclientid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Identityproviderclientid);

			}
			set { 
				 sdt.gxTpr_Identityproviderclientid = value;
			}
		}

		[DataMember(Name="IdentityProviderClientSecret", Order=5)]
		public  string gxTpr_Identityproviderclientsecret
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Identityproviderclientsecret);

			}
			set { 
				 sdt.gxTpr_Identityproviderclientsecret = value;
			}
		}

		[DataMember(Name="IdentityProviderSecure", Order=6)]
		public bool gxTpr_Identityprovidersecure
		{
			get { 
				return sdt.gxTpr_Identityprovidersecure;

			}
			set { 
				sdt.gxTpr_Identityprovidersecure = value;
			}
		}

		[DataMember(Name="IdentityProviderRedirUrl", Order=7)]
		public  string gxTpr_Identityproviderredirurl
		{
			get { 
				return sdt.gxTpr_Identityproviderredirurl;

			}
			set { 
				 sdt.gxTpr_Identityproviderredirurl = value;
			}
		}

		[DataMember(Name="IdentityProviderDynamicCallProcedure", Order=8)]
		public  string gxTpr_Identityproviderdynamiccallprocedure
		{
			get { 
				return sdt.gxTpr_Identityproviderdynamiccallprocedure;

			}
			set { 
				 sdt.gxTpr_Identityproviderdynamiccallprocedure = value;
			}
		}

		[DataMember(Name="IdentityProviderEncryptParameter", Order=9)]
		public bool gxTpr_Identityproviderencryptparameter
		{
			get { 
				return sdt.gxTpr_Identityproviderencryptparameter;

			}
			set { 
				sdt.gxTpr_Identityproviderencryptparameter = value;
			}
		}

		[DataMember(Name="IdentityProviderRepository", Order=10)]
		public  string gxTpr_Identityproviderrepository
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Identityproviderrepository);

			}
			set { 
				 sdt.gxTpr_Identityproviderrepository = value;
			}
		}

		[DataMember(Name="IdentityProviderEncryptionKey", Order=11)]
		public  string gxTpr_Identityproviderencryptionkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Identityproviderencryptionkey);

			}
			set { 
				 sdt.gxTpr_Identityproviderencryptionkey = value;
			}
		}


		#endregion

		public SdtSDT_IdentityProvider sdt
		{
			get { 
				return (SdtSDT_IdentityProvider)Sdt;
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
				sdt = new SdtSDT_IdentityProvider() ;
			}
		}
	}
	#endregion
}