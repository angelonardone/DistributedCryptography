/*
				   File: type_SdtisAddressValidUnitTestSDT
			Description: isAddressValidUnitTestSDT
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
	[XmlRoot(ElementName="isAddressValidUnitTestSDT")]
	[XmlType(TypeName="isAddressValidUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtisAddressValidUnitTestSDT : GxUserType
	{
		public SdtisAddressValidUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtisAddressValidUnitTestSDT_Testcaseid = "";

			gxTv_SdtisAddressValidUnitTestSDT_Address = "";

			gxTv_SdtisAddressValidUnitTestSDT_Networktype = "";

			gxTv_SdtisAddressValidUnitTestSDT_Error = "";

			gxTv_SdtisAddressValidUnitTestSDT_Expectederror = "";

			gxTv_SdtisAddressValidUnitTestSDT_Msgerror = "";

		}

		public SdtisAddressValidUnitTestSDT(IGxContext context)
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
				return gxTv_SdtisAddressValidUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtisAddressValidUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtisAddressValidUnitTestSDT_Address; 
			}
			set {
				gxTv_SdtisAddressValidUnitTestSDT_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="networkType")]
		[XmlElement(ElementName="networkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtisAddressValidUnitTestSDT_Networktype; 
			}
			set {
				gxTv_SdtisAddressValidUnitTestSDT_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtisAddressValidUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtisAddressValidUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtisAddressValidUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtisAddressValidUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtisAddressValidUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtisAddressValidUnitTestSDT_Msgerror = value;
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
			gxTv_SdtisAddressValidUnitTestSDT_Testcaseid = "";
			gxTv_SdtisAddressValidUnitTestSDT_Address = "";
			gxTv_SdtisAddressValidUnitTestSDT_Networktype = "";
			gxTv_SdtisAddressValidUnitTestSDT_Error = "";
			gxTv_SdtisAddressValidUnitTestSDT_Expectederror = "";
			gxTv_SdtisAddressValidUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtisAddressValidUnitTestSDT_Testcaseid;
		 

		protected string gxTv_SdtisAddressValidUnitTestSDT_Address;
		 

		protected string gxTv_SdtisAddressValidUnitTestSDT_Networktype;
		 

		protected string gxTv_SdtisAddressValidUnitTestSDT_Error;
		 

		protected string gxTv_SdtisAddressValidUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtisAddressValidUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"isAddressValidUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtisAddressValidUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtisAddressValidUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtisAddressValidUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtisAddressValidUnitTestSDT_RESTInterface( SdtisAddressValidUnitTestSDT psdt ) : base(psdt)
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

		[DataMember(Name="error", Order=3)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[DataMember(Name="Expectederror", Order=4)]
		public  string gxTpr_Expectederror
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectederror);

			}
			set { 
				 sdt.gxTpr_Expectederror = value;
			}
		}

		[DataMember(Name="Msgerror", Order=5)]
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

		public SdtisAddressValidUnitTestSDT sdt
		{
			get { 
				return (SdtisAddressValidUnitTestSDT)Sdt;
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
				sdt = new SdtisAddressValidUnitTestSDT() ;
			}
		}
	}
	#endregion
}