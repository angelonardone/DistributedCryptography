/*
				   File: type_SdtECCverifyMsgUnitTestSDT
			Description: ECCverifyMsgUnitTestSDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.12.186073
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
	[XmlRoot(ElementName="ECCverifyMsgUnitTestSDT")]
	[XmlType(TypeName="ECCverifyMsgUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtECCverifyMsgUnitTestSDT : GxUserType
	{
		public SdtECCverifyMsgUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtECCverifyMsgUnitTestSDT_Testcaseid = "";

			gxTv_SdtECCverifyMsgUnitTestSDT_Pubkey = "";

			gxTv_SdtECCverifyMsgUnitTestSDT_Message = "";

			gxTv_SdtECCverifyMsgUnitTestSDT_Signature = "";

			gxTv_SdtECCverifyMsgUnitTestSDT_Msgverified = "";

			gxTv_SdtECCverifyMsgUnitTestSDT_Error = "";

			gxTv_SdtECCverifyMsgUnitTestSDT_Expectederror = "";

			gxTv_SdtECCverifyMsgUnitTestSDT_Msgerror = "";

		}

		public SdtECCverifyMsgUnitTestSDT(IGxContext context)
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


			AddObjectProperty("pubKey", gxTpr_Pubkey, false);


			AddObjectProperty("message", gxTpr_Message, false);


			AddObjectProperty("signature", gxTpr_Signature, false);


			AddObjectProperty("verified", gxTpr_Verified, false);


			AddObjectProperty("Expectedverified", gxTpr_Expectedverified, false);


			AddObjectProperty("Msgverified", gxTpr_Msgverified, false);


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
				return gxTv_SdtECCverifyMsgUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="pubKey")]
		[XmlElement(ElementName="pubKey")]
		public string gxTpr_Pubkey
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Pubkey; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Pubkey = value;
				SetDirty("Pubkey");
			}
		}




		[SoapElement(ElementName="message")]
		[XmlElement(ElementName="message")]
		public string gxTpr_Message
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Message; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Message = value;
				SetDirty("Message");
			}
		}




		[SoapElement(ElementName="signature")]
		[XmlElement(ElementName="signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Signature; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Signature = value;
				SetDirty("Signature");
			}
		}




		[SoapElement(ElementName="verified")]
		[XmlElement(ElementName="verified")]
		public bool gxTpr_Verified
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Verified; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Verified = value;
				SetDirty("Verified");
			}
		}




		[SoapElement(ElementName="Expectedverified")]
		[XmlElement(ElementName="Expectedverified")]
		public bool gxTpr_Expectedverified
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Expectedverified; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Expectedverified = value;
				SetDirty("Expectedverified");
			}
		}




		[SoapElement(ElementName="Msgverified")]
		[XmlElement(ElementName="Msgverified")]
		public string gxTpr_Msgverified
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Msgverified; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Msgverified = value;
				SetDirty("Msgverified");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtECCverifyMsgUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtECCverifyMsgUnitTestSDT_Msgerror = value;
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
			gxTv_SdtECCverifyMsgUnitTestSDT_Testcaseid = "";
			gxTv_SdtECCverifyMsgUnitTestSDT_Pubkey = "";
			gxTv_SdtECCverifyMsgUnitTestSDT_Message = "";
			gxTv_SdtECCverifyMsgUnitTestSDT_Signature = "";


			gxTv_SdtECCverifyMsgUnitTestSDT_Msgverified = "";
			gxTv_SdtECCverifyMsgUnitTestSDT_Error = "";
			gxTv_SdtECCverifyMsgUnitTestSDT_Expectederror = "";
			gxTv_SdtECCverifyMsgUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtECCverifyMsgUnitTestSDT_Testcaseid;
		 

		protected string gxTv_SdtECCverifyMsgUnitTestSDT_Pubkey;
		 

		protected string gxTv_SdtECCverifyMsgUnitTestSDT_Message;
		 

		protected string gxTv_SdtECCverifyMsgUnitTestSDT_Signature;
		 

		protected bool gxTv_SdtECCverifyMsgUnitTestSDT_Verified;
		 

		protected bool gxTv_SdtECCverifyMsgUnitTestSDT_Expectedverified;
		 

		protected string gxTv_SdtECCverifyMsgUnitTestSDT_Msgverified;
		 

		protected string gxTv_SdtECCverifyMsgUnitTestSDT_Error;
		 

		protected string gxTv_SdtECCverifyMsgUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtECCverifyMsgUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ECCverifyMsgUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtECCverifyMsgUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtECCverifyMsgUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtECCverifyMsgUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtECCverifyMsgUnitTestSDT_RESTInterface( SdtECCverifyMsgUnitTestSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TestCaseId")]
		[JsonPropertyOrder(0)]
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

		[JsonPropertyName("pubKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="pubKey", Order=1)]
		public  string gxTpr_Pubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Pubkey);

			}
			set { 
				 sdt.gxTpr_Pubkey = value;
			}
		}

		[JsonPropertyName("message")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="message", Order=2)]
		public  string gxTpr_Message
		{
			get { 
				return sdt.gxTpr_Message;

			}
			set { 
				 sdt.gxTpr_Message = value;
			}
		}

		[JsonPropertyName("signature")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="signature", Order=3)]
		public  string gxTpr_Signature
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signature);

			}
			set { 
				 sdt.gxTpr_Signature = value;
			}
		}

		[JsonPropertyName("verified")]
		[JsonPropertyOrder(4)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="verified", Order=4)]
		public bool gxTpr_Verified
		{
			get { 
				return sdt.gxTpr_Verified;

			}
			set { 
				sdt.gxTpr_Verified = value;
			}
		}

		[JsonPropertyName("Expectedverified")]
		[JsonPropertyOrder(5)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="Expectedverified", Order=5)]
		public bool gxTpr_Expectedverified
		{
			get { 
				return sdt.gxTpr_Expectedverified;

			}
			set { 
				sdt.gxTpr_Expectedverified = value;
			}
		}

		[JsonPropertyName("Msgverified")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="Msgverified", Order=6)]
		public  string gxTpr_Msgverified
		{
			get { 
				return sdt.gxTpr_Msgverified;

			}
			set { 
				 sdt.gxTpr_Msgverified = value;
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
				return sdt.gxTpr_Msgerror;

			}
			set { 
				 sdt.gxTpr_Msgerror = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtECCverifyMsgUnitTestSDT sdt
		{
			get { 
				return (SdtECCverifyMsgUnitTestSDT)Sdt;
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
				sdt = new SdtECCverifyMsgUnitTestSDT() ;
			}
		}
	}
	#endregion
}