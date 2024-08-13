/*
				   File: type_SdtCreateExtKeyUnitTestSDT
			Description: CreateExtKeyUnitTestSDT
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
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="CreateExtKeyUnitTestSDT")]
	[XmlType(TypeName="CreateExtKeyUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtCreateExtKeyUnitTestSDT : GxUserType
	{
		public SdtCreateExtKeyUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtCreateExtKeyUnitTestSDT_Testcaseid = "";

			gxTv_SdtCreateExtKeyUnitTestSDT_Password = "";

			gxTv_SdtCreateExtKeyUnitTestSDT_Msgkeyinfo = "";

			gxTv_SdtCreateExtKeyUnitTestSDT_Error = "";

			gxTv_SdtCreateExtKeyUnitTestSDT_Expectederror = "";

			gxTv_SdtCreateExtKeyUnitTestSDT_Msgerror = "";

		}

		public SdtCreateExtKeyUnitTestSDT(IGxContext context)
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

			if (gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate != null)
			{
				AddObjectProperty("extKeyCreate", gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate, false);
			}

			AddObjectProperty("password", gxTpr_Password, false);

			if (gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo != null)
			{
				AddObjectProperty("keyInfo", gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo, false);
			}
			if (gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo != null)
			{
				AddObjectProperty("ExpectedkeyInfo", gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo, false);
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
				return gxTv_SdtCreateExtKeyUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}



		[SoapElement(ElementName="extKeyCreate")]
		[XmlElement(ElementName="extKeyCreate")]
		public GeneXus.Programs.nbitcoin.SdtExtKeyCreate gxTpr_Extkeycreate
		{
			get {
				if ( gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate == null )
				{
					gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate = new GeneXus.Programs.nbitcoin.SdtExtKeyCreate(context);
				}
				return gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate = value;
				SetDirty("Extkeycreate");
			}
		}
		public void gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate_SetNull()
		{
			gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate_N = true;
			gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate = null;
		}

		public bool gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate_IsNull()
		{
			return gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate == null;
		}
		public bool ShouldSerializegxTpr_Extkeycreate_Json()
		{
			return gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate != null;

		}


		[SoapElement(ElementName="password")]
		[XmlElement(ElementName="password")]
		public string gxTpr_Password
		{
			get {
				return gxTv_SdtCreateExtKeyUnitTestSDT_Password; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Password = value;
				SetDirty("Password");
			}
		}



		[SoapElement(ElementName="keyInfo")]
		[XmlElement(ElementName="keyInfo")]
		public GeneXus.Programs.nbitcoin.SdtExtKeyInfo gxTpr_Keyinfo
		{
			get {
				if ( gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo == null )
				{
					gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
				}
				return gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo = value;
				SetDirty("Keyinfo");
			}
		}
		public void gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo_SetNull()
		{
			gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo_N = true;
			gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo = null;
		}

		public bool gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo_IsNull()
		{
			return gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo == null;
		}
		public bool ShouldSerializegxTpr_Keyinfo_Json()
		{
			return gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo != null;

		}

		[SoapElement(ElementName="ExpectedkeyInfo")]
		[XmlElement(ElementName="ExpectedkeyInfo")]
		public GeneXus.Programs.nbitcoin.SdtExtKeyInfo gxTpr_Expectedkeyinfo
		{
			get {
				if ( gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo == null )
				{
					gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo = new GeneXus.Programs.nbitcoin.SdtExtKeyInfo(context);
				}
				return gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo = value;
				SetDirty("Expectedkeyinfo");
			}
		}
		public void gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo_SetNull()
		{
			gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo_N = true;
			gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo = null;
		}

		public bool gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo_IsNull()
		{
			return gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Expectedkeyinfo_Json()
		{
			return gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo != null;

		}


		[SoapElement(ElementName="MsgkeyInfo")]
		[XmlElement(ElementName="MsgkeyInfo")]
		public string gxTpr_Msgkeyinfo
		{
			get {
				return gxTv_SdtCreateExtKeyUnitTestSDT_Msgkeyinfo; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Msgkeyinfo = value;
				SetDirty("Msgkeyinfo");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtCreateExtKeyUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtCreateExtKeyUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtCreateExtKeyUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtCreateExtKeyUnitTestSDT_Msgerror = value;
				SetDirty("Msgerror");
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
			gxTv_SdtCreateExtKeyUnitTestSDT_Testcaseid = "";

			gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate_N = true;

			gxTv_SdtCreateExtKeyUnitTestSDT_Password = "";

			gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo_N = true;


			gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo_N = true;

			gxTv_SdtCreateExtKeyUnitTestSDT_Msgkeyinfo = "";
			gxTv_SdtCreateExtKeyUnitTestSDT_Error = "";
			gxTv_SdtCreateExtKeyUnitTestSDT_Expectederror = "";
			gxTv_SdtCreateExtKeyUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtCreateExtKeyUnitTestSDT_Testcaseid;
		 

		protected GeneXus.Programs.nbitcoin.SdtExtKeyCreate gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate = null;
		protected bool gxTv_SdtCreateExtKeyUnitTestSDT_Extkeycreate_N;
		 

		protected string gxTv_SdtCreateExtKeyUnitTestSDT_Password;
		 

		protected GeneXus.Programs.nbitcoin.SdtExtKeyInfo gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo = null;
		protected bool gxTv_SdtCreateExtKeyUnitTestSDT_Keyinfo_N;
		 

		protected GeneXus.Programs.nbitcoin.SdtExtKeyInfo gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo = null;
		protected bool gxTv_SdtCreateExtKeyUnitTestSDT_Expectedkeyinfo_N;
		 

		protected string gxTv_SdtCreateExtKeyUnitTestSDT_Msgkeyinfo;
		 

		protected string gxTv_SdtCreateExtKeyUnitTestSDT_Error;
		 

		protected string gxTv_SdtCreateExtKeyUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtCreateExtKeyUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"CreateExtKeyUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtCreateExtKeyUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtCreateExtKeyUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtCreateExtKeyUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtCreateExtKeyUnitTestSDT_RESTInterface( SdtCreateExtKeyUnitTestSDT psdt ) : base(psdt)
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

		[DataMember(Name="extKeyCreate", Order=1, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtExtKeyCreate_RESTInterface gxTpr_Extkeycreate
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Extkeycreate_Json())
					return new GeneXus.Programs.nbitcoin.SdtExtKeyCreate_RESTInterface(sdt.gxTpr_Extkeycreate);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Extkeycreate = value.sdt;
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
		public GeneXus.Programs.nbitcoin.SdtExtKeyInfo_RESTInterface gxTpr_Keyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Keyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtExtKeyInfo_RESTInterface(sdt.gxTpr_Keyinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Keyinfo = value.sdt;
			}
		}

		[DataMember(Name="ExpectedkeyInfo", Order=4, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtExtKeyInfo_RESTInterface gxTpr_Expectedkeyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedkeyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtExtKeyInfo_RESTInterface(sdt.gxTpr_Expectedkeyinfo);
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

		public SdtCreateExtKeyUnitTestSDT sdt
		{
			get { 
				return (SdtCreateExtKeyUnitTestSDT)Sdt;
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
				sdt = new SdtCreateExtKeyUnitTestSDT() ;
			}
		}
	}
	#endregion
}