/*
				   File: type_SdttestSLIP0132UnitTestSDT
			Description: testSLIP0132UnitTestSDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.14.187820
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
	[XmlRoot(ElementName="testSLIP0132UnitTestSDT")]
	[XmlType(TypeName="testSLIP0132UnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdttestSLIP0132UnitTestSDT : GxUserType
	{
		public SdttestSLIP0132UnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdttestSLIP0132UnitTestSDT_Testcaseid = "";

			gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in_N = true;

			gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out_N = true;

			gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out_N = true;

			gxTv_SdttestSLIP0132UnitTestSDT_Msgtest_slip0132_out = "";

			gxTv_SdttestSLIP0132UnitTestSDT_Error = "";

			gxTv_SdttestSLIP0132UnitTestSDT_Expectederror = "";

			gxTv_SdttestSLIP0132UnitTestSDT_Msgerror = "";

		}

		public SdttestSLIP0132UnitTestSDT(IGxContext context)
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

			if (gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in != null)
			{
				AddObjectProperty("test_SLIP0132_in", gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in, false);
			}
			if (gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out != null)
			{
				AddObjectProperty("test_SLIP0132_out", gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out, false);
			}
			if (gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out != null)
			{
				AddObjectProperty("Expectedtest_SLIP0132_out", gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out, false);
			}

			AddObjectProperty("Msgtest_SLIP0132_out", gxTpr_Msgtest_slip0132_out, false);


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
				return gxTv_SdttestSLIP0132UnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdttestSLIP0132UnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}



		[SoapElement(ElementName="test_SLIP0132_in")]
		[XmlElement(ElementName="test_SLIP0132_in")]
		public GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_in gxTpr_Test_slip0132_in
		{
			get {
				if ( gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in == null )
				{
					gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in = new GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_in(context);
					SetDirty("Test_slip0132_in");
				}
				return gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in; 
			}
			set {
				gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in = value;
				SetDirty("Test_slip0132_in");
			}
		}
		public void gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in_SetNull()
		{
			gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in_N = true;
			gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in = null;
		}

		public bool gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in_IsNull()
		{
			return gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in == null;
		}
		public bool ShouldSerializegxTpr_Test_slip0132_in_Json()
		{
			return gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in != null;

		}

		[SoapElement(ElementName="test_SLIP0132_out")]
		[XmlElement(ElementName="test_SLIP0132_out")]
		public GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out gxTpr_Test_slip0132_out
		{
			get {
				if ( gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out == null )
				{
					gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out = new GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out(context);
					SetDirty("Test_slip0132_out");
				}
				return gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out; 
			}
			set {
				gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out = value;
				SetDirty("Test_slip0132_out");
			}
		}
		public void gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out_SetNull()
		{
			gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out_N = true;
			gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out = null;
		}

		public bool gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out_IsNull()
		{
			return gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out == null;
		}
		public bool ShouldSerializegxTpr_Test_slip0132_out_Json()
		{
			return gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out != null;

		}

		[SoapElement(ElementName="Expectedtest_SLIP0132_out")]
		[XmlElement(ElementName="Expectedtest_SLIP0132_out")]
		public GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out gxTpr_Expectedtest_slip0132_out
		{
			get {
				if ( gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out == null )
				{
					gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out = new GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out(context);
					SetDirty("Expectedtest_slip0132_out");
				}
				return gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out; 
			}
			set {
				gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out = value;
				SetDirty("Expectedtest_slip0132_out");
			}
		}
		public void gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out_SetNull()
		{
			gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out_N = true;
			gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out = null;
		}

		public bool gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out_IsNull()
		{
			return gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out == null;
		}
		public bool ShouldSerializegxTpr_Expectedtest_slip0132_out_Json()
		{
			return gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out != null;

		}


		[SoapElement(ElementName="Msgtest_SLIP0132_out")]
		[XmlElement(ElementName="Msgtest_SLIP0132_out")]
		public string gxTpr_Msgtest_slip0132_out
		{
			get {
				return gxTv_SdttestSLIP0132UnitTestSDT_Msgtest_slip0132_out; 
			}
			set {
				gxTv_SdttestSLIP0132UnitTestSDT_Msgtest_slip0132_out = value;
				SetDirty("Msgtest_slip0132_out");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdttestSLIP0132UnitTestSDT_Error; 
			}
			set {
				gxTv_SdttestSLIP0132UnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdttestSLIP0132UnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdttestSLIP0132UnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdttestSLIP0132UnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdttestSLIP0132UnitTestSDT_Msgerror = value;
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
			gxTv_SdttestSLIP0132UnitTestSDT_Testcaseid = "";

			gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in_N = true;


			gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out_N = true;


			gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out_N = true;

			gxTv_SdttestSLIP0132UnitTestSDT_Msgtest_slip0132_out = "";
			gxTv_SdttestSLIP0132UnitTestSDT_Error = "";
			gxTv_SdttestSLIP0132UnitTestSDT_Expectederror = "";
			gxTv_SdttestSLIP0132UnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdttestSLIP0132UnitTestSDT_Testcaseid;
		 

		protected GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_in gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in = null;
		protected bool gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_in_N;
		 

		protected GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out = null;
		protected bool gxTv_SdttestSLIP0132UnitTestSDT_Test_slip0132_out_N;
		 

		protected GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out = null;
		protected bool gxTv_SdttestSLIP0132UnitTestSDT_Expectedtest_slip0132_out_N;
		 

		protected string gxTv_SdttestSLIP0132UnitTestSDT_Msgtest_slip0132_out;
		 

		protected string gxTv_SdttestSLIP0132UnitTestSDT_Error;
		 

		protected string gxTv_SdttestSLIP0132UnitTestSDT_Expectederror;
		 

		protected string gxTv_SdttestSLIP0132UnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"testSLIP0132UnitTestSDT", Namespace="distributedcryptography")]
	public class SdttestSLIP0132UnitTestSDT_RESTInterface : GxGenericCollectionItem<SdttestSLIP0132UnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdttestSLIP0132UnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdttestSLIP0132UnitTestSDT_RESTInterface( SdttestSLIP0132UnitTestSDT psdt ) : base(psdt)
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

		[JsonPropertyName("test_SLIP0132_in")]
		[JsonPropertyOrder(1)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="test_SLIP0132_in", Order=1, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_in_RESTInterface gxTpr_Test_slip0132_in
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Test_slip0132_in_Json())
					return new GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_in_RESTInterface(sdt.gxTpr_Test_slip0132_in);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Test_slip0132_in = value.sdt;
			}
		}

		[JsonPropertyName("test_SLIP0132_out")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="test_SLIP0132_out", Order=2, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out_RESTInterface gxTpr_Test_slip0132_out
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Test_slip0132_out_Json())
					return new GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out_RESTInterface(sdt.gxTpr_Test_slip0132_out);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Test_slip0132_out = value.sdt;
			}
		}

		[JsonPropertyName("Expectedtest_SLIP0132_out")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Expectedtest_SLIP0132_out", Order=3, EmitDefaultValue=false)]
		public GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out_RESTInterface gxTpr_Expectedtest_slip0132_out
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedtest_slip0132_out_Json())
					return new GeneXus.Programs.nbitcoin.Sdttest_SLIP0132_out_RESTInterface(sdt.gxTpr_Expectedtest_slip0132_out);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Expectedtest_slip0132_out = value.sdt;
			}
		}

		[JsonPropertyName("Msgtest_SLIP0132_out")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Msgtest_SLIP0132_out", Order=4)]
		public  string gxTpr_Msgtest_slip0132_out
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgtest_slip0132_out);

			}
			set { 
				 sdt.gxTpr_Msgtest_slip0132_out = value;
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
		public SdttestSLIP0132UnitTestSDT sdt
		{
			get { 
				return (SdttestSLIP0132UnitTestSDT)Sdt;
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
				sdt = new SdttestSLIP0132UnitTestSDT() ;
			}
		}
	}
	#endregion
}