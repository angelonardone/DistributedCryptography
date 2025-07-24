/*
				   File: type_SdttestECCencryptANDdecryptUnitTestSDT
			Description: testECCencryptANDdecryptUnitTestSDT
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
	[XmlRoot(ElementName="testECCencryptANDdecryptUnitTestSDT")]
	[XmlType(TypeName="testECCencryptANDdecryptUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdttestECCencryptANDdecryptUnitTestSDT : GxUserType
	{
		public SdttestECCencryptANDdecryptUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Testcaseid = "";

			gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Originalcleartext = "";

			gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Msgisverified = "";

		}

		public SdttestECCencryptANDdecryptUnitTestSDT(IGxContext context)
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


			AddObjectProperty("originalClearText", gxTpr_Originalcleartext, false);


			AddObjectProperty("isVerified", gxTpr_Isverified, false);


			AddObjectProperty("ExpectedisVerified", gxTpr_Expectedisverified, false);


			AddObjectProperty("MsgisVerified", gxTpr_Msgisverified, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TestCaseId")]
		[XmlElement(ElementName="TestCaseId")]
		public string gxTpr_Testcaseid
		{
			get {
				return gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="originalClearText")]
		[XmlElement(ElementName="originalClearText")]
		public string gxTpr_Originalcleartext
		{
			get {
				return gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Originalcleartext; 
			}
			set {
				gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Originalcleartext = value;
				SetDirty("Originalcleartext");
			}
		}




		[SoapElement(ElementName="isVerified")]
		[XmlElement(ElementName="isVerified")]
		public bool gxTpr_Isverified
		{
			get {
				return gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Isverified; 
			}
			set {
				gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Isverified = value;
				SetDirty("Isverified");
			}
		}




		[SoapElement(ElementName="ExpectedisVerified")]
		[XmlElement(ElementName="ExpectedisVerified")]
		public bool gxTpr_Expectedisverified
		{
			get {
				return gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Expectedisverified; 
			}
			set {
				gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Expectedisverified = value;
				SetDirty("Expectedisverified");
			}
		}




		[SoapElement(ElementName="MsgisVerified")]
		[XmlElement(ElementName="MsgisVerified")]
		public string gxTpr_Msgisverified
		{
			get {
				return gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Msgisverified; 
			}
			set {
				gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Msgisverified = value;
				SetDirty("Msgisverified");
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
			gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Testcaseid = "";
			gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Originalcleartext = "";


			gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Msgisverified = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Testcaseid;
		 

		protected string gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Originalcleartext;
		 

		protected bool gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Isverified;
		 

		protected bool gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Expectedisverified;
		 

		protected string gxTv_SdttestECCencryptANDdecryptUnitTestSDT_Msgisverified;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"testECCencryptANDdecryptUnitTestSDT", Namespace="distributedcryptography")]
	public class SdttestECCencryptANDdecryptUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdttestECCencryptANDdecryptUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdttestECCencryptANDdecryptUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdttestECCencryptANDdecryptUnitTestSDT_RESTInterface( SdttestECCencryptANDdecryptUnitTestSDT psdt ) : base(psdt)
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

		[JsonPropertyName("originalClearText")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="originalClearText", Order=1)]
		public  string gxTpr_Originalcleartext
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Originalcleartext);

			}
			set { 
				 sdt.gxTpr_Originalcleartext = value;
			}
		}

		[JsonPropertyName("isVerified")]
		[JsonPropertyOrder(2)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isVerified", Order=2)]
		public bool gxTpr_Isverified
		{
			get { 
				return sdt.gxTpr_Isverified;

			}
			set { 
				sdt.gxTpr_Isverified = value;
			}
		}

		[JsonPropertyName("ExpectedisVerified")]
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="ExpectedisVerified", Order=3)]
		public bool gxTpr_Expectedisverified
		{
			get { 
				return sdt.gxTpr_Expectedisverified;

			}
			set { 
				sdt.gxTpr_Expectedisverified = value;
			}
		}

		[JsonPropertyName("MsgisVerified")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="MsgisVerified", Order=4)]
		public  string gxTpr_Msgisverified
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgisverified);

			}
			set { 
				 sdt.gxTpr_Msgisverified = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdttestECCencryptANDdecryptUnitTestSDT sdt
		{
			get { 
				return (SdttestECCencryptANDdecryptUnitTestSDT)Sdt;
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
				sdt = new SdttestECCencryptANDdecryptUnitTestSDT() ;
			}
		}
	}
	#endregion
}