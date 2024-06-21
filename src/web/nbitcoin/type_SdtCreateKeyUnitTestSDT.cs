/*
				   File: type_SdtCreateKeyUnitTestSDT
			Description: CreateKeyUnitTestSDT
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
	[XmlRoot(ElementName="CreateKeyUnitTestSDT")]
	[XmlType(TypeName="CreateKeyUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtCreateKeyUnitTestSDT : GxUserType
	{
		public SdtCreateKeyUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtCreateKeyUnitTestSDT_Testcaseid = "";

			gxTv_SdtCreateKeyUnitTestSDT_Password = "";

			gxTv_SdtCreateKeyUnitTestSDT_Msgkeyinfo = "";

			gxTv_SdtCreateKeyUnitTestSDT_Error = "";

			gxTv_SdtCreateKeyUnitTestSDT_Expectederror = "";

			gxTv_SdtCreateKeyUnitTestSDT_Msgerror = "";

		}

		public SdtCreateKeyUnitTestSDT(IGxContext context)
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
			AddObjectProperty("TestCaseId", gxTpr_Testcaseid, false);

			if (gxTv_SdtCreateKeyUnitTestSDT_Keycreate != null)
			{
				AddObjectProperty("keyCreate", gxTv_SdtCreateKeyUnitTestSDT_Keycreate, false);
			}

			AddObjectProperty("password", gxTpr_Password, false);

			if (gxTv_SdtCreateKeyUnitTestSDT_Keyinfo != null)
			{
				AddObjectProperty("keyInfo", gxTv_SdtCreateKeyUnitTestSDT_Keyinfo, false);
			}
			if (gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo != null)
			{
				AddObjectProperty("ExpectedkeyInfo", gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo, false);
			}

			AddObjectProperty("MsgkeyInfo", gxTpr_Msgkeyinfo, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("Expectederror", gxTpr_Expectederror, false);


			AddObjectProperty("Msgerror", gxTpr_Msgerror, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TestCaseId")]
		[XmlElement(ElementName="TestCaseId")]
		public string gxTpr_Testcaseid
		{
			get {
				return gxTv_SdtCreateKeyUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}



		[SoapElement(ElementName="keyCreate")]
		[XmlElement(ElementName="keyCreate")]
		public GeneXus.Programs.nbitcoin.SdtKeyCreate gxTpr_Keycreate
		{
			get {
				if ( gxTv_SdtCreateKeyUnitTestSDT_Keycreate == null )
				{
					gxTv_SdtCreateKeyUnitTestSDT_Keycreate = new GeneXus.Programs.nbitcoin.SdtKeyCreate(context);
				}
				return gxTv_SdtCreateKeyUnitTestSDT_Keycreate; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Keycreate = value;
				SetDirty("Keycreate");
			}
		}
		public void gxTv_SdtCreateKeyUnitTestSDT_Keycreate_SetNull()
		{
			gxTv_SdtCreateKeyUnitTestSDT_Keycreate_N = true;
			gxTv_SdtCreateKeyUnitTestSDT_Keycreate = null;
		}

		public bool gxTv_SdtCreateKeyUnitTestSDT_Keycreate_IsNull()
		{
			return gxTv_SdtCreateKeyUnitTestSDT_Keycreate == null;
		}
		public bool ShouldSerializegxTpr_Keycreate_Json()
		{
			return gxTv_SdtCreateKeyUnitTestSDT_Keycreate != null;

		}


		[SoapElement(ElementName="password")]
		[XmlElement(ElementName="password")]
		public string gxTpr_Password
		{
			get {
				return gxTv_SdtCreateKeyUnitTestSDT_Password; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Password = value;
				SetDirty("Password");
			}
		}



		[SoapElement(ElementName="keyInfo")]
		[XmlElement(ElementName="keyInfo")]
		public GeneXus.Programs.nbitcoin.SdtKeyInfo gxTpr_Keyinfo
		{
			get {
				if ( gxTv_SdtCreateKeyUnitTestSDT_Keyinfo == null )
				{
					gxTv_SdtCreateKeyUnitTestSDT_Keyinfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
				}
				return gxTv_SdtCreateKeyUnitTestSDT_Keyinfo; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Keyinfo = value;
				SetDirty("Keyinfo");
			}
		}
		public void gxTv_SdtCreateKeyUnitTestSDT_Keyinfo_SetNull()
		{
			gxTv_SdtCreateKeyUnitTestSDT_Keyinfo_N = true;
			gxTv_SdtCreateKeyUnitTestSDT_Keyinfo = null;
		}

		public bool gxTv_SdtCreateKeyUnitTestSDT_Keyinfo_IsNull()
		{
			return gxTv_SdtCreateKeyUnitTestSDT_Keyinfo == null;
		}
		public bool ShouldSerializegxTpr_Keyinfo_Json()
		{
			return gxTv_SdtCreateKeyUnitTestSDT_Keyinfo != null;

		}

		[SoapElement(ElementName="ExpectedkeyInfo")]
		[XmlElement(ElementName="ExpectedkeyInfo")]
		public GeneXus.Programs.nbitcoin.SdtKeyInfo gxTpr_Expectedkeyinfo
		{
			get {
				if ( gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo == null )
				{
					gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
				}
				return gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo = value;
				SetDirty("Expectedkeyinfo");
			}
		}
		public void gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo_SetNull()
		{
			gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo_N = true;
			gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo = null;
		}

		public bool gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo_IsNull()
		{
			return gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Expectedkeyinfo_Json()
		{
			return gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo != null;

		}


		[SoapElement(ElementName="MsgkeyInfo")]
		[XmlElement(ElementName="MsgkeyInfo")]
		public string gxTpr_Msgkeyinfo
		{
			get {
				return gxTv_SdtCreateKeyUnitTestSDT_Msgkeyinfo; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Msgkeyinfo = value;
				SetDirty("Msgkeyinfo");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtCreateKeyUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtCreateKeyUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtCreateKeyUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtCreateKeyUnitTestSDT_Msgerror = value;
				SetDirty("Msgerror");
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
			gxTv_SdtCreateKeyUnitTestSDT_Testcaseid = "";

			gxTv_SdtCreateKeyUnitTestSDT_Keycreate_N = true;

			gxTv_SdtCreateKeyUnitTestSDT_Password = "";

			gxTv_SdtCreateKeyUnitTestSDT_Keyinfo_N = true;


			gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo_N = true;

			gxTv_SdtCreateKeyUnitTestSDT_Msgkeyinfo = "";
			gxTv_SdtCreateKeyUnitTestSDT_Error = "";
			gxTv_SdtCreateKeyUnitTestSDT_Expectederror = "";
			gxTv_SdtCreateKeyUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtCreateKeyUnitTestSDT_Testcaseid;
		 

		protected GeneXus.Programs.nbitcoin.SdtKeyCreate gxTv_SdtCreateKeyUnitTestSDT_Keycreate = null;
		protected bool gxTv_SdtCreateKeyUnitTestSDT_Keycreate_N;
		 

		protected string gxTv_SdtCreateKeyUnitTestSDT_Password;
		 

		protected GeneXus.Programs.nbitcoin.SdtKeyInfo gxTv_SdtCreateKeyUnitTestSDT_Keyinfo = null;
		protected bool gxTv_SdtCreateKeyUnitTestSDT_Keyinfo_N;
		 

		protected GeneXus.Programs.nbitcoin.SdtKeyInfo gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo = null;
		protected bool gxTv_SdtCreateKeyUnitTestSDT_Expectedkeyinfo_N;
		 

		protected string gxTv_SdtCreateKeyUnitTestSDT_Msgkeyinfo;
		 

		protected string gxTv_SdtCreateKeyUnitTestSDT_Error;
		 

		protected string gxTv_SdtCreateKeyUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtCreateKeyUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"CreateKeyUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtCreateKeyUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtCreateKeyUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtCreateKeyUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtCreateKeyUnitTestSDT_RESTInterface( SdtCreateKeyUnitTestSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="TestCaseId", Order=0)]
		public  string gxTpr_Testcaseid
		{
			get { 
				return sdt.gxTpr_Testcaseid;

			}
			set { 
				 sdt.gxTpr_Testcaseid = value;
			}
		}

		[DataMember(Name="keyCreate", Order=1, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtKeyCreate_RESTInterface gxTpr_Keycreate
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Keycreate_Json())
					return new GeneXus.Programs.nbitcoin.SdtKeyCreate_RESTInterface(sdt.gxTpr_Keycreate);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Keycreate = value.sdt;
			}
		}

		[DataMember(Name="password", Order=2)]
		public  string gxTpr_Password
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Password);

			}
			set { 
				 sdt.gxTpr_Password = value;
			}
		}

		[DataMember(Name="keyInfo", Order=3, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface gxTpr_Keyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Keyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface(sdt.gxTpr_Keyinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Keyinfo = value.sdt;
			}
		}

		[DataMember(Name="ExpectedkeyInfo", Order=4, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface gxTpr_Expectedkeyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedkeyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface(sdt.gxTpr_Expectedkeyinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Expectedkeyinfo = value.sdt;
			}
		}

		[DataMember(Name="MsgkeyInfo", Order=5)]
		public  string gxTpr_Msgkeyinfo
		{
			get { 
				return sdt.gxTpr_Msgkeyinfo;

			}
			set { 
				 sdt.gxTpr_Msgkeyinfo = value;
			}
		}

		[DataMember(Name="error", Order=6)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[DataMember(Name="Expectederror", Order=7)]
		public  string gxTpr_Expectederror
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectederror);

			}
			set { 
				 sdt.gxTpr_Expectederror = value;
			}
		}

		[DataMember(Name="Msgerror", Order=8)]
		public  string gxTpr_Msgerror
		{
			get { 
				return sdt.gxTpr_Msgerror;

			}
			set { 
				 sdt.gxTpr_Msgerror = value;
			}
		}


		#endregion

		public SdtCreateKeyUnitTestSDT sdt
		{
			get { 
				return (SdtCreateKeyUnitTestSDT)Sdt;
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
				sdt = new SdtCreateKeyUnitTestSDT() ;
			}
		}
	}
	#endregion
}