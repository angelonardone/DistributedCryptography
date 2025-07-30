/*
				   File: type_SdtretAddressElectrumFormatUnitTestSDT
			Description: retAddressElectrumFormatUnitTestSDT
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
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="retAddressElectrumFormatUnitTestSDT")]
	[XmlType(TypeName="retAddressElectrumFormatUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtretAddressElectrumFormatUnitTestSDT : GxUserType
	{
		public SdtretAddressElectrumFormatUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Testcaseid = "";

			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Address = "";

			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Networktype = "";

			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Electrumaddress = "";

			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectedelectrumaddress = "";

			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgelectrumaddress = "";

			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Error = "";

			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectederror = "";

			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgerror = "";

		}

		public SdtretAddressElectrumFormatUnitTestSDT(IGxContext context)
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


			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("networkType", gxTpr_Networktype, false);


			AddObjectProperty("electrumAddress", gxTpr_Electrumaddress, false);


			AddObjectProperty("ExpectedelectrumAddress", gxTpr_Expectedelectrumaddress, false);


			AddObjectProperty("MsgelectrumAddress", gxTpr_Msgelectrumaddress, false);


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
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Address; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="networkType")]
		[XmlElement(ElementName="networkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Networktype; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="electrumAddress")]
		[XmlElement(ElementName="electrumAddress")]
		public string gxTpr_Electrumaddress
		{
			get {
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Electrumaddress; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Electrumaddress = value;
				SetDirty("Electrumaddress");
			}
		}




		[SoapElement(ElementName="ExpectedelectrumAddress")]
		[XmlElement(ElementName="ExpectedelectrumAddress")]
		public string gxTpr_Expectedelectrumaddress
		{
			get {
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectedelectrumaddress; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectedelectrumaddress = value;
				SetDirty("Expectedelectrumaddress");
			}
		}




		[SoapElement(ElementName="MsgelectrumAddress")]
		[XmlElement(ElementName="MsgelectrumAddress")]
		public string gxTpr_Msgelectrumaddress
		{
			get {
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgelectrumaddress; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgelectrumaddress = value;
				SetDirty("Msgelectrumaddress");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgerror = value;
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
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Testcaseid = "";
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Address = "";
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Networktype = "";
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Electrumaddress = "";
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectedelectrumaddress = "";
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgelectrumaddress = "";
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Error = "";
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectederror = "";
			gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Testcaseid;
		 

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Address;
		 

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Networktype;
		 

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Electrumaddress;
		 

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectedelectrumaddress;
		 

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgelectrumaddress;
		 

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Error;
		 

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtretAddressElectrumFormatUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"retAddressElectrumFormatUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtretAddressElectrumFormatUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtretAddressElectrumFormatUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtretAddressElectrumFormatUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtretAddressElectrumFormatUnitTestSDT_RESTInterface( SdtretAddressElectrumFormatUnitTestSDT psdt ) : base(psdt)
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

		[JsonPropertyName("address")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="address", Order=1)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
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

		[JsonPropertyName("electrumAddress")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="electrumAddress", Order=3)]
		public  string gxTpr_Electrumaddress
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Electrumaddress);

			}
			set { 
				 sdt.gxTpr_Electrumaddress = value;
			}
		}

		[JsonPropertyName("ExpectedelectrumAddress")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="ExpectedelectrumAddress", Order=4)]
		public  string gxTpr_Expectedelectrumaddress
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectedelectrumaddress);

			}
			set { 
				 sdt.gxTpr_Expectedelectrumaddress = value;
			}
		}

		[JsonPropertyName("MsgelectrumAddress")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="MsgelectrumAddress", Order=5)]
		public  string gxTpr_Msgelectrumaddress
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgelectrumaddress);

			}
			set { 
				 sdt.gxTpr_Msgelectrumaddress = value;
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
				return StringUtil.RTrim( sdt.gxTpr_Msgerror);

			}
			set { 
				 sdt.gxTpr_Msgerror = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtretAddressElectrumFormatUnitTestSDT sdt
		{
			get { 
				return (SdtretAddressElectrumFormatUnitTestSDT)Sdt;
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
				sdt = new SdtretAddressElectrumFormatUnitTestSDT() ;
			}
		}
	}
	#endregion
}