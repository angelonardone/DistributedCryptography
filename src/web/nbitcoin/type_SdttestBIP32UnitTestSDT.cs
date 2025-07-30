/*
				   File: type_SdttestBIP32UnitTestSDT
			Description: testBIP32UnitTestSDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
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
	[XmlRoot(ElementName="testBIP32UnitTestSDT")]
	[XmlType(TypeName="testBIP32UnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdttestBIP32UnitTestSDT : GxUserType
	{
		public SdttestBIP32UnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdttestBIP32UnitTestSDT_Testcaseid = "";

			gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in_N = true;

			gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out_N = true;

			gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out_N = true;

			gxTv_SdttestBIP32UnitTestSDT_Msgtest_bip32_out = "";

			gxTv_SdttestBIP32UnitTestSDT_Error = "";

			gxTv_SdttestBIP32UnitTestSDT_Expectederror = "";

			gxTv_SdttestBIP32UnitTestSDT_Msgerror = "";

		}

		public SdttestBIP32UnitTestSDT(IGxContext context)
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

			if (gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in != null)
			{
				AddObjectProperty("test_BIP32_in", gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in, false);
			}
			if (gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out != null)
			{
				AddObjectProperty("test_BIP32_out", gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out, false);
			}
			if (gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out != null)
			{
				AddObjectProperty("Expectedtest_BIP32_out", gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out, false);
			}

			AddObjectProperty("Msgtest_BIP32_out", gxTpr_Msgtest_bip32_out, false);


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
				return gxTv_SdttestBIP32UnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdttestBIP32UnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}



		[SoapElement(ElementName="test_BIP32_in")]
		[XmlElement(ElementName="test_BIP32_in")]
		public GeneXus.Programs.nbitcoin.Sdttest_BIP32_in gxTpr_Test_bip32_in
		{
			get {
				if ( gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in == null )
				{
					gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in = new GeneXus.Programs.nbitcoin.Sdttest_BIP32_in(context);
					SetDirty("Test_bip32_in");
				}
				return gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in; 
			}
			set {
				gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in = value;
				SetDirty("Test_bip32_in");
			}
		}
		public void gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in_SetNull()
		{
			gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in_N = true;
			gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in = null;
		}

		public bool gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in_IsNull()
		{
			return gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in == null;
		}
		public bool ShouldSerializegxTpr_Test_bip32_in_Json()
		{
			return gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in != null;

		}

		[SoapElement(ElementName="test_BIP32_out")]
		[XmlElement(ElementName="test_BIP32_out")]
		public GeneXus.Programs.nbitcoin.Sdttest_BIP32_out gxTpr_Test_bip32_out
		{
			get {
				if ( gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out == null )
				{
					gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out = new GeneXus.Programs.nbitcoin.Sdttest_BIP32_out(context);
					SetDirty("Test_bip32_out");
				}
				return gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out; 
			}
			set {
				gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out = value;
				SetDirty("Test_bip32_out");
			}
		}
		public void gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out_SetNull()
		{
			gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out_N = true;
			gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out = null;
		}

		public bool gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out_IsNull()
		{
			return gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out == null;
		}
		public bool ShouldSerializegxTpr_Test_bip32_out_Json()
		{
			return gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out != null;

		}

		[SoapElement(ElementName="Expectedtest_BIP32_out")]
		[XmlElement(ElementName="Expectedtest_BIP32_out")]
		public GeneXus.Programs.nbitcoin.Sdttest_BIP32_out gxTpr_Expectedtest_bip32_out
		{
			get {
				if ( gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out == null )
				{
					gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out = new GeneXus.Programs.nbitcoin.Sdttest_BIP32_out(context);
					SetDirty("Expectedtest_bip32_out");
				}
				return gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out; 
			}
			set {
				gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out = value;
				SetDirty("Expectedtest_bip32_out");
			}
		}
		public void gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out_SetNull()
		{
			gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out_N = true;
			gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out = null;
		}

		public bool gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out_IsNull()
		{
			return gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out == null;
		}
		public bool ShouldSerializegxTpr_Expectedtest_bip32_out_Json()
		{
			return gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out != null;

		}


		[SoapElement(ElementName="Msgtest_BIP32_out")]
		[XmlElement(ElementName="Msgtest_BIP32_out")]
		public string gxTpr_Msgtest_bip32_out
		{
			get {
				return gxTv_SdttestBIP32UnitTestSDT_Msgtest_bip32_out; 
			}
			set {
				gxTv_SdttestBIP32UnitTestSDT_Msgtest_bip32_out = value;
				SetDirty("Msgtest_bip32_out");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdttestBIP32UnitTestSDT_Error; 
			}
			set {
				gxTv_SdttestBIP32UnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdttestBIP32UnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdttestBIP32UnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdttestBIP32UnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdttestBIP32UnitTestSDT_Msgerror = value;
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
			gxTv_SdttestBIP32UnitTestSDT_Testcaseid = "";

			gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in_N = true;


			gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out_N = true;


			gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out_N = true;

			gxTv_SdttestBIP32UnitTestSDT_Msgtest_bip32_out = "";
			gxTv_SdttestBIP32UnitTestSDT_Error = "";
			gxTv_SdttestBIP32UnitTestSDT_Expectederror = "";
			gxTv_SdttestBIP32UnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdttestBIP32UnitTestSDT_Testcaseid;
		 

		protected GeneXus.Programs.nbitcoin.Sdttest_BIP32_in gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in = null;
		protected bool gxTv_SdttestBIP32UnitTestSDT_Test_bip32_in_N;
		 

		protected GeneXus.Programs.nbitcoin.Sdttest_BIP32_out gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out = null;
		protected bool gxTv_SdttestBIP32UnitTestSDT_Test_bip32_out_N;
		 

		protected GeneXus.Programs.nbitcoin.Sdttest_BIP32_out gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out = null;
		protected bool gxTv_SdttestBIP32UnitTestSDT_Expectedtest_bip32_out_N;
		 

		protected string gxTv_SdttestBIP32UnitTestSDT_Msgtest_bip32_out;
		 

		protected string gxTv_SdttestBIP32UnitTestSDT_Error;
		 

		protected string gxTv_SdttestBIP32UnitTestSDT_Expectederror;
		 

		protected string gxTv_SdttestBIP32UnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"testBIP32UnitTestSDT", Namespace="distributedcryptography")]
	public class SdttestBIP32UnitTestSDT_RESTInterface : GxGenericCollectionItem<SdttestBIP32UnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdttestBIP32UnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdttestBIP32UnitTestSDT_RESTInterface( SdttestBIP32UnitTestSDT psdt ) : base(psdt)
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

		[JsonPropertyName("test_BIP32_in")]
		[JsonPropertyOrder(1)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="test_BIP32_in", Order=1, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.Sdttest_BIP32_in_RESTInterface gxTpr_Test_bip32_in
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Test_bip32_in_Json())
					return new GeneXus.Programs.nbitcoin.Sdttest_BIP32_in_RESTInterface(sdt.gxTpr_Test_bip32_in);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Test_bip32_in = value.sdt;
			}
		}

		[JsonPropertyName("test_BIP32_out")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="test_BIP32_out", Order=2, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.Sdttest_BIP32_out_RESTInterface gxTpr_Test_bip32_out
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Test_bip32_out_Json())
					return new GeneXus.Programs.nbitcoin.Sdttest_BIP32_out_RESTInterface(sdt.gxTpr_Test_bip32_out);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Test_bip32_out = value.sdt;
			}
		}

		[JsonPropertyName("Expectedtest_BIP32_out")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Expectedtest_BIP32_out", Order=3, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.Sdttest_BIP32_out_RESTInterface gxTpr_Expectedtest_bip32_out
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedtest_bip32_out_Json())
					return new GeneXus.Programs.nbitcoin.Sdttest_BIP32_out_RESTInterface(sdt.gxTpr_Expectedtest_bip32_out);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Expectedtest_bip32_out = value.sdt;
			}
		}

		[JsonPropertyName("Msgtest_BIP32_out")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Msgtest_BIP32_out", Order=4)]
		public  string gxTpr_Msgtest_bip32_out
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgtest_bip32_out);

			}
			set { 
				 sdt.gxTpr_Msgtest_bip32_out = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="error", Order=5)]
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
		[JsonPropertyOrder(6)]
		[DataMember(Name="Expectederror", Order=6)]
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
		[JsonPropertyOrder(7)]
		[DataMember(Name="Msgerror", Order=7)]
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
		public SdttestBIP32UnitTestSDT sdt
		{
			get { 
				return (SdttestBIP32UnitTestSDT)Sdt;
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
				sdt = new SdttestBIP32UnitTestSDT() ;
			}
		}
	}
	#endregion
}