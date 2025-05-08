/*
				   File: type_SdtECCsignMsgUnitTestSDT
			Description: ECCsignMsgUnitTestSDT
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
	[XmlRoot(ElementName="ECCsignMsgUnitTestSDT")]
	[XmlType(TypeName="ECCsignMsgUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtECCsignMsgUnitTestSDT : GxUserType
	{
		public SdtECCsignMsgUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtECCsignMsgUnitTestSDT_Testcaseid = "";

			gxTv_SdtECCsignMsgUnitTestSDT_Privatekey = "";

			gxTv_SdtECCsignMsgUnitTestSDT_Message = "";

			gxTv_SdtECCsignMsgUnitTestSDT_Signature = "";

			gxTv_SdtECCsignMsgUnitTestSDT_Expectedsignature = "";

			gxTv_SdtECCsignMsgUnitTestSDT_Msgsignature = "";

			gxTv_SdtECCsignMsgUnitTestSDT_Error = "";

			gxTv_SdtECCsignMsgUnitTestSDT_Expectederror = "";

			gxTv_SdtECCsignMsgUnitTestSDT_Msgerror = "";

		}

		public SdtECCsignMsgUnitTestSDT(IGxContext context)
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


			AddObjectProperty("privateKey", gxTpr_Privatekey, false);


			AddObjectProperty("message", gxTpr_Message, false);


			AddObjectProperty("signature", gxTpr_Signature, false);


			AddObjectProperty("Expectedsignature", gxTpr_Expectedsignature, false);


			AddObjectProperty("Msgsignature", gxTpr_Msgsignature, false);


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
				return gxTv_SdtECCsignMsgUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="privateKey")]
		[XmlElement(ElementName="privateKey")]
		public string gxTpr_Privatekey
		{
			get {
				return gxTv_SdtECCsignMsgUnitTestSDT_Privatekey; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Privatekey = value;
				SetDirty("Privatekey");
			}
		}




		[SoapElement(ElementName="message")]
		[XmlElement(ElementName="message")]
		public string gxTpr_Message
		{
			get {
				return gxTv_SdtECCsignMsgUnitTestSDT_Message; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Message = value;
				SetDirty("Message");
			}
		}




		[SoapElement(ElementName="signature")]
		[XmlElement(ElementName="signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtECCsignMsgUnitTestSDT_Signature; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Signature = value;
				SetDirty("Signature");
			}
		}




		[SoapElement(ElementName="Expectedsignature")]
		[XmlElement(ElementName="Expectedsignature")]
		public string gxTpr_Expectedsignature
		{
			get {
				return gxTv_SdtECCsignMsgUnitTestSDT_Expectedsignature; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Expectedsignature = value;
				SetDirty("Expectedsignature");
			}
		}




		[SoapElement(ElementName="Msgsignature")]
		[XmlElement(ElementName="Msgsignature")]
		public string gxTpr_Msgsignature
		{
			get {
				return gxTv_SdtECCsignMsgUnitTestSDT_Msgsignature; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Msgsignature = value;
				SetDirty("Msgsignature");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtECCsignMsgUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtECCsignMsgUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtECCsignMsgUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtECCsignMsgUnitTestSDT_Msgerror = value;
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
			gxTv_SdtECCsignMsgUnitTestSDT_Testcaseid = "";
			gxTv_SdtECCsignMsgUnitTestSDT_Privatekey = "";
			gxTv_SdtECCsignMsgUnitTestSDT_Message = "";
			gxTv_SdtECCsignMsgUnitTestSDT_Signature = "";
			gxTv_SdtECCsignMsgUnitTestSDT_Expectedsignature = "";
			gxTv_SdtECCsignMsgUnitTestSDT_Msgsignature = "";
			gxTv_SdtECCsignMsgUnitTestSDT_Error = "";
			gxTv_SdtECCsignMsgUnitTestSDT_Expectederror = "";
			gxTv_SdtECCsignMsgUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Testcaseid;
		 

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Privatekey;
		 

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Message;
		 

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Signature;
		 

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Expectedsignature;
		 

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Msgsignature;
		 

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Error;
		 

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtECCsignMsgUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ECCsignMsgUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtECCsignMsgUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtECCsignMsgUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtECCsignMsgUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtECCsignMsgUnitTestSDT_RESTInterface( SdtECCsignMsgUnitTestSDT psdt ) : base(psdt)
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

		[JsonPropertyName("privateKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="privateKey", Order=1)]
		public  string gxTpr_Privatekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Privatekey);

			}
			set { 
				 sdt.gxTpr_Privatekey = value;
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

		[JsonPropertyName("Expectedsignature")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Expectedsignature", Order=4)]
		public  string gxTpr_Expectedsignature
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectedsignature);

			}
			set { 
				 sdt.gxTpr_Expectedsignature = value;
			}
		}

		[JsonPropertyName("Msgsignature")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="Msgsignature", Order=5)]
		public  string gxTpr_Msgsignature
		{
			get { 
				return sdt.gxTpr_Msgsignature;

			}
			set { 
				 sdt.gxTpr_Msgsignature = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(6)]
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

		[JsonPropertyName("Expectederror")]
		[JsonPropertyOrder(7)]
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

		[JsonPropertyName("Msgerror")]
		[JsonPropertyOrder(8)]
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
		[JsonIgnore]
		public SdtECCsignMsgUnitTestSDT sdt
		{
			get { 
				return (SdtECCsignMsgUnitTestSDT)Sdt;
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
				sdt = new SdtECCsignMsgUnitTestSDT() ;
			}
		}
	}
	#endregion
}