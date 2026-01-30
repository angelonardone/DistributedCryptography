/*
				   File: type_Sdtparse_serialized_extended_keyUnitTestSDT
			Description: parse_serialized_extended_keyUnitTestSDT
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
	[XmlRoot(ElementName="parse_serialized_extended_keyUnitTestSDT")]
	[XmlType(TypeName="parse_serialized_extended_keyUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdtparse_serialized_extended_keyUnitTestSDT : GxUserType
	{
		public Sdtparse_serialized_extended_keyUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Testcaseid = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_In_extended_key = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Out_extended_key = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedout_extended_key = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgout_extended_key = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Networktype = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectednetworktype = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgnetworktype = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Extendedkeytype = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedextendedkeytype = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgextendedkeytype = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Error = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectederror = "";

			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgerror = "";

		}

		public Sdtparse_serialized_extended_keyUnitTestSDT(IGxContext context)
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


			AddObjectProperty("in_extended_key", gxTpr_In_extended_key, false);


			AddObjectProperty("out_extended_key", gxTpr_Out_extended_key, false);


			AddObjectProperty("Expectedout_extended_key", gxTpr_Expectedout_extended_key, false);


			AddObjectProperty("Msgout_extended_key", gxTpr_Msgout_extended_key, false);


			AddObjectProperty("networkType", gxTpr_Networktype, false);


			AddObjectProperty("ExpectednetworkType", gxTpr_Expectednetworktype, false);


			AddObjectProperty("MsgnetworkType", gxTpr_Msgnetworktype, false);


			AddObjectProperty("extendedKeyType", gxTpr_Extendedkeytype, false);


			AddObjectProperty("ExpectedextendedKeyType", gxTpr_Expectedextendedkeytype, false);


			AddObjectProperty("MsgextendedKeyType", gxTpr_Msgextendedkeytype, false);


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
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="in_extended_key")]
		[XmlElement(ElementName="in_extended_key")]
		public string gxTpr_In_extended_key
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_In_extended_key; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_In_extended_key = value;
				SetDirty("In_extended_key");
			}
		}




		[SoapElement(ElementName="out_extended_key")]
		[XmlElement(ElementName="out_extended_key")]
		public string gxTpr_Out_extended_key
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Out_extended_key; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Out_extended_key = value;
				SetDirty("Out_extended_key");
			}
		}




		[SoapElement(ElementName="Expectedout_extended_key")]
		[XmlElement(ElementName="Expectedout_extended_key")]
		public string gxTpr_Expectedout_extended_key
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedout_extended_key; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedout_extended_key = value;
				SetDirty("Expectedout_extended_key");
			}
		}




		[SoapElement(ElementName="Msgout_extended_key")]
		[XmlElement(ElementName="Msgout_extended_key")]
		public string gxTpr_Msgout_extended_key
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgout_extended_key; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgout_extended_key = value;
				SetDirty("Msgout_extended_key");
			}
		}




		[SoapElement(ElementName="networkType")]
		[XmlElement(ElementName="networkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Networktype; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="ExpectednetworkType")]
		[XmlElement(ElementName="ExpectednetworkType")]
		public string gxTpr_Expectednetworktype
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectednetworktype; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectednetworktype = value;
				SetDirty("Expectednetworktype");
			}
		}




		[SoapElement(ElementName="MsgnetworkType")]
		[XmlElement(ElementName="MsgnetworkType")]
		public string gxTpr_Msgnetworktype
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgnetworktype; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgnetworktype = value;
				SetDirty("Msgnetworktype");
			}
		}




		[SoapElement(ElementName="extendedKeyType")]
		[XmlElement(ElementName="extendedKeyType")]
		public string gxTpr_Extendedkeytype
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Extendedkeytype; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Extendedkeytype = value;
				SetDirty("Extendedkeytype");
			}
		}




		[SoapElement(ElementName="ExpectedextendedKeyType")]
		[XmlElement(ElementName="ExpectedextendedKeyType")]
		public string gxTpr_Expectedextendedkeytype
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedextendedkeytype; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedextendedkeytype = value;
				SetDirty("Expectedextendedkeytype");
			}
		}




		[SoapElement(ElementName="MsgextendedKeyType")]
		[XmlElement(ElementName="MsgextendedKeyType")]
		public string gxTpr_Msgextendedkeytype
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgextendedkeytype; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgextendedkeytype = value;
				SetDirty("Msgextendedkeytype");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Error; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgerror = value;
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
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Testcaseid = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_In_extended_key = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Out_extended_key = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedout_extended_key = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgout_extended_key = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Networktype = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectednetworktype = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgnetworktype = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Extendedkeytype = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedextendedkeytype = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgextendedkeytype = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Error = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectederror = "";
			gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Testcaseid;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_In_extended_key;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Out_extended_key;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedout_extended_key;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgout_extended_key;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Networktype;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectednetworktype;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgnetworktype;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Extendedkeytype;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectedextendedkeytype;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgextendedkeytype;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Error;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Expectederror;
		 

		protected string gxTv_Sdtparse_serialized_extended_keyUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"parse_serialized_extended_keyUnitTestSDT", Namespace="distributedcryptography")]
	public class Sdtparse_serialized_extended_keyUnitTestSDT_RESTInterface : GxGenericCollectionItem<Sdtparse_serialized_extended_keyUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdtparse_serialized_extended_keyUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public Sdtparse_serialized_extended_keyUnitTestSDT_RESTInterface( Sdtparse_serialized_extended_keyUnitTestSDT psdt ) : base(psdt)
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

		[JsonPropertyName("in_extended_key")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="in_extended_key", Order=1)]
		public  string gxTpr_In_extended_key
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_In_extended_key);

			}
			set { 
				 sdt.gxTpr_In_extended_key = value;
			}
		}

		[JsonPropertyName("out_extended_key")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="out_extended_key", Order=2)]
		public  string gxTpr_Out_extended_key
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Out_extended_key);

			}
			set { 
				 sdt.gxTpr_Out_extended_key = value;
			}
		}

		[JsonPropertyName("Expectedout_extended_key")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="Expectedout_extended_key", Order=3)]
		public  string gxTpr_Expectedout_extended_key
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectedout_extended_key);

			}
			set { 
				 sdt.gxTpr_Expectedout_extended_key = value;
			}
		}

		[JsonPropertyName("Msgout_extended_key")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Msgout_extended_key", Order=4)]
		public  string gxTpr_Msgout_extended_key
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgout_extended_key);

			}
			set { 
				 sdt.gxTpr_Msgout_extended_key = value;
			}
		}

		[JsonPropertyName("networkType")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="networkType", Order=5)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[JsonPropertyName("ExpectednetworkType")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="ExpectednetworkType", Order=6)]
		public  string gxTpr_Expectednetworktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectednetworktype);

			}
			set { 
				 sdt.gxTpr_Expectednetworktype = value;
			}
		}

		[JsonPropertyName("MsgnetworkType")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="MsgnetworkType", Order=7)]
		public  string gxTpr_Msgnetworktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgnetworktype);

			}
			set { 
				 sdt.gxTpr_Msgnetworktype = value;
			}
		}

		[JsonPropertyName("extendedKeyType")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="extendedKeyType", Order=8)]
		public  string gxTpr_Extendedkeytype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extendedkeytype);

			}
			set { 
				 sdt.gxTpr_Extendedkeytype = value;
			}
		}

		[JsonPropertyName("ExpectedextendedKeyType")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="ExpectedextendedKeyType", Order=9)]
		public  string gxTpr_Expectedextendedkeytype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectedextendedkeytype);

			}
			set { 
				 sdt.gxTpr_Expectedextendedkeytype = value;
			}
		}

		[JsonPropertyName("MsgextendedKeyType")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="MsgextendedKeyType", Order=10)]
		public  string gxTpr_Msgextendedkeytype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgextendedkeytype);

			}
			set { 
				 sdt.gxTpr_Msgextendedkeytype = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="error", Order=11)]
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
		[JsonPropertyOrder(12)]
		[DataMember(Name="Expectederror", Order=12)]
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
		[JsonPropertyOrder(13)]
		[DataMember(Name="Msgerror", Order=13)]
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
		public Sdtparse_serialized_extended_keyUnitTestSDT sdt
		{
			get { 
				return (Sdtparse_serialized_extended_keyUnitTestSDT)Sdt;
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
				sdt = new Sdtparse_serialized_extended_keyUnitTestSDT() ;
			}
		}
	}
	#endregion
}