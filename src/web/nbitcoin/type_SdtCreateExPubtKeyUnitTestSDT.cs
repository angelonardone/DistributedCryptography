/*
				   File: type_SdtCreateExPubtKeyUnitTestSDT
			Description: CreateExPubtKeyUnitTestSDT
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
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="CreateExPubtKeyUnitTestSDT")]
	[XmlType(TypeName="CreateExPubtKeyUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtCreateExPubtKeyUnitTestSDT : GxUserType
	{
		public SdtCreateExPubtKeyUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Testcaseid = "";

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Extendedpublickey = "";

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Networktype = "";

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Keypath = "";

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo_N = true;

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo_N = true;

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgkeyinfo = "";

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Error = "";

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectederror = "";

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgerror = "";

		}

		public SdtCreateExPubtKeyUnitTestSDT(IGxContext context)
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


			AddObjectProperty("ExtendedPublicKey", gxTpr_Extendedpublickey, false);


			AddObjectProperty("networkType", gxTpr_Networktype, false);


			AddObjectProperty("keyPath", gxTpr_Keypath, false);

			if (gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo != null)
			{
				AddObjectProperty("keyInfo", gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo, false);
			}
			if (gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo != null)
			{
				AddObjectProperty("ExpectedkeyInfo", gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo, false);
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
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="ExtendedPublicKey")]
		[XmlElement(ElementName="ExtendedPublicKey")]
		public string gxTpr_Extendedpublickey
		{
			get {
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Extendedpublickey; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Extendedpublickey = value;
				SetDirty("Extendedpublickey");
			}
		}




		[SoapElement(ElementName="networkType")]
		[XmlElement(ElementName="networkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Networktype; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="keyPath")]
		[XmlElement(ElementName="keyPath")]
		public string gxTpr_Keypath
		{
			get {
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Keypath; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Keypath = value;
				SetDirty("Keypath");
			}
		}



		[SoapElement(ElementName="keyInfo")]
		[XmlElement(ElementName="keyInfo")]
		public GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo gxTpr_Keyinfo
		{
			get {
				if ( gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo == null )
				{
					gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
					SetDirty("Keyinfo");
				}
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo = value;
				SetDirty("Keyinfo");
			}
		}
		public void gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo_SetNull()
		{
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo_N = true;
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo = null;
		}

		public bool gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo_IsNull()
		{
			return gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo == null;
		}
		public bool ShouldSerializegxTpr_Keyinfo_Json()
		{
			return gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo != null;

		}

		[SoapElement(ElementName="ExpectedkeyInfo")]
		[XmlElement(ElementName="ExpectedkeyInfo")]
		public GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo gxTpr_Expectedkeyinfo
		{
			get {
				if ( gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo == null )
				{
					gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo = new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo(context);
					SetDirty("Expectedkeyinfo");
				}
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo = value;
				SetDirty("Expectedkeyinfo");
			}
		}
		public void gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo_SetNull()
		{
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo_N = true;
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo = null;
		}

		public bool gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo_IsNull()
		{
			return gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Expectedkeyinfo_Json()
		{
			return gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo != null;

		}


		[SoapElement(ElementName="MsgkeyInfo")]
		[XmlElement(ElementName="MsgkeyInfo")]
		public string gxTpr_Msgkeyinfo
		{
			get {
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgkeyinfo; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgkeyinfo = value;
				SetDirty("Msgkeyinfo");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgerror = value;
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
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Testcaseid = "";
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Extendedpublickey = "";
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Networktype = "";
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Keypath = "";

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo_N = true;


			gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo_N = true;

			gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgkeyinfo = "";
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Error = "";
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectederror = "";
			gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtCreateExPubtKeyUnitTestSDT_Testcaseid;
		 

		protected string gxTv_SdtCreateExPubtKeyUnitTestSDT_Extendedpublickey;
		 

		protected string gxTv_SdtCreateExPubtKeyUnitTestSDT_Networktype;
		 

		protected string gxTv_SdtCreateExPubtKeyUnitTestSDT_Keypath;
		 

		protected GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo = null;
		protected bool gxTv_SdtCreateExPubtKeyUnitTestSDT_Keyinfo_N;
		 

		protected GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo = null;
		protected bool gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectedkeyinfo_N;
		 

		protected string gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgkeyinfo;
		 

		protected string gxTv_SdtCreateExPubtKeyUnitTestSDT_Error;
		 

		protected string gxTv_SdtCreateExPubtKeyUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtCreateExPubtKeyUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"CreateExPubtKeyUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtCreateExPubtKeyUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtCreateExPubtKeyUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtCreateExPubtKeyUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtCreateExPubtKeyUnitTestSDT_RESTInterface( SdtCreateExPubtKeyUnitTestSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TestCaseId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="TestCaseId", Order=0)]
		public  string gxTpr_Testcaseid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Testcaseid);

			}
			set { 
				 sdt.gxTpr_Testcaseid = value;
			}
		}

		[JsonPropertyName("ExtendedPublicKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="ExtendedPublicKey", Order=1)]
		public  string gxTpr_Extendedpublickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extendedpublickey);

			}
			set { 
				 sdt.gxTpr_Extendedpublickey = value;
			}
		}

		[JsonPropertyName("networkType")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="networkType", Order=2)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[JsonPropertyName("keyPath")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="keyPath", Order=3)]
		public  string gxTpr_Keypath
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Keypath);

			}
			set { 
				 sdt.gxTpr_Keypath = value;
			}
		}

		[JsonPropertyName("keyInfo")]
		[JsonPropertyOrder(4)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="keyInfo", Order=4, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo_RESTInterface gxTpr_Keyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Keyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo_RESTInterface(sdt.gxTpr_Keyinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Keyinfo = value.sdt;
			}
		}

		[JsonPropertyName("ExpectedkeyInfo")]
		[JsonPropertyOrder(5)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="ExpectedkeyInfo", Order=5, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo_RESTInterface gxTpr_Expectedkeyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedkeyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtExtPubKeyInfo_RESTInterface(sdt.gxTpr_Expectedkeyinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Expectedkeyinfo = value.sdt;
			}
		}

		[JsonPropertyName("MsgkeyInfo")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="MsgkeyInfo", Order=6)]
		public  string gxTpr_Msgkeyinfo
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgkeyinfo);

			}
			set { 
				 sdt.gxTpr_Msgkeyinfo = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="error", Order=7)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[JsonPropertyName("Expectederror")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="Expectederror", Order=8)]
		public  string gxTpr_Expectederror
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectederror);

			}
			set { 
				 sdt.gxTpr_Expectederror = value;
			}
		}

		[JsonPropertyName("Msgerror")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="Msgerror", Order=9)]
		public  string gxTpr_Msgerror
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgerror);

			}
			set { 
				 sdt.gxTpr_Msgerror = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtCreateExPubtKeyUnitTestSDT sdt
		{
			get { 
				return (SdtCreateExPubtKeyUnitTestSDT)Sdt;
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
				sdt = new SdtCreateExPubtKeyUnitTestSDT() ;
			}
		}
	}
	#endregion
}