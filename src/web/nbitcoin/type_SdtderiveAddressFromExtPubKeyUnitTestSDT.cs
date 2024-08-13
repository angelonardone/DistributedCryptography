/*
				   File: type_SdtderiveAddressFromExtPubKeyUnitTestSDT
			Description: deriveAddressFromExtPubKeyUnitTestSDT
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
	[XmlRoot(ElementName="deriveAddressFromExtPubKeyUnitTestSDT")]
	[XmlType(TypeName="deriveAddressFromExtPubKeyUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtderiveAddressFromExtPubKeyUnitTestSDT : GxUserType
	{
		public SdtderiveAddressFromExtPubKeyUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Testcaseid = "";

			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Deserializedextpubkey = "";

			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgsdt_addressess = "";

			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Error = "";

			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectederror = "";

			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgerror = "";

		}

		public SdtderiveAddressFromExtPubKeyUnitTestSDT(IGxContext context)
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


			AddObjectProperty("deserializedExtPubKey", gxTpr_Deserializedextpubkey, false);


			AddObjectProperty("base", gxTpr_Base, false);


			AddObjectProperty("start", gxTpr_Start, false);


			AddObjectProperty("end", gxTpr_End, false);

			if (gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess != null)
			{
				AddObjectProperty("sdt_addressess", gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess, false);
			}
			if (gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess != null)
			{
				AddObjectProperty("Expectedsdt_addressess", gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess, false);
			}

			AddObjectProperty("Msgsdt_addressess", gxTpr_Msgsdt_addressess, false);


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
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="deserializedExtPubKey")]
		[XmlElement(ElementName="deserializedExtPubKey")]
		public string gxTpr_Deserializedextpubkey
		{
			get {
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Deserializedextpubkey; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Deserializedextpubkey = value;
				SetDirty("Deserializedextpubkey");
			}
		}




		[SoapElement(ElementName="base")]
		[XmlElement(ElementName="base")]
		public long gxTpr_Base
		{
			get {
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Base; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Base = value;
				SetDirty("Base");
			}
		}




		[SoapElement(ElementName="start")]
		[XmlElement(ElementName="start")]
		public long gxTpr_Start
		{
			get {
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Start; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Start = value;
				SetDirty("Start");
			}
		}




		[SoapElement(ElementName="end")]
		[XmlElement(ElementName="end")]
		public long gxTpr_End
		{
			get {
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_End; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_End = value;
				SetDirty("End");
			}
		}




		[SoapElement(ElementName="sdt_addressess" )]
		[XmlArray(ElementName="sdt_addressess"  )]
		[XmlArrayItemAttribute(ElementName="SDT_AddressessItem" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTpr_Sdt_addressess_GXBaseCollection
		{
			get {
				if ( gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess == null )
				{
					gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_Addressess", "");
				}
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess;
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess_N = false;
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTpr_Sdt_addressess
		{
			get {
				if ( gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess == null )
				{
					gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_Addressess", "");
				}
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess_N = false;
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess ;
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess_N = false;
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess = value;
				SetDirty("Sdt_addressess");
			}
		}

		public void gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess_SetNull()
		{
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess_N = true;
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess = null;
		}

		public bool gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess_IsNull()
		{
			return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess == null;
		}
		public bool ShouldSerializegxTpr_Sdt_addressess_GXBaseCollection_Json()
		{
			return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess != null && gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess.Count > 0;

		}


		[SoapElement(ElementName="Expectedsdt_addressess" )]
		[XmlArray(ElementName="Expectedsdt_addressess"  )]
		[XmlArrayItemAttribute(ElementName="SDT_AddressessItem" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTpr_Expectedsdt_addressess_GXBaseCollection
		{
			get {
				if ( gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess == null )
				{
					gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_Addressess", "");
				}
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess;
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess_N = false;
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTpr_Expectedsdt_addressess
		{
			get {
				if ( gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess == null )
				{
					gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_Addressess", "");
				}
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess_N = false;
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess ;
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess_N = false;
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess = value;
				SetDirty("Expectedsdt_addressess");
			}
		}

		public void gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess_SetNull()
		{
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess_N = true;
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess = null;
		}

		public bool gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess_IsNull()
		{
			return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess == null;
		}
		public bool ShouldSerializegxTpr_Expectedsdt_addressess_GXBaseCollection_Json()
		{
			return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess != null && gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess.Count > 0;

		}


		[SoapElement(ElementName="Msgsdt_addressess")]
		[XmlElement(ElementName="Msgsdt_addressess")]
		public string gxTpr_Msgsdt_addressess
		{
			get {
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgsdt_addressess; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgsdt_addressess = value;
				SetDirty("Msgsdt_addressess");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgerror = value;
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
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Testcaseid = "";
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Deserializedextpubkey = "";




			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess_N = true;


			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess_N = true;

			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgsdt_addressess = "";
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Error = "";
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectederror = "";
			gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Testcaseid;
		 

		protected string gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Deserializedextpubkey;
		 

		protected long gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Base;
		 

		protected long gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Start;
		 

		protected long gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_End;
		 
		protected bool gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess_N;
		protected GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Sdt_addressess = null;  
		protected bool gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess_N;
		protected GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectedsdt_addressess = null;  

		protected string gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgsdt_addressess;
		 

		protected string gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Error;
		 

		protected string gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtderiveAddressFromExtPubKeyUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"deriveAddressFromExtPubKeyUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtderiveAddressFromExtPubKeyUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtderiveAddressFromExtPubKeyUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtderiveAddressFromExtPubKeyUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtderiveAddressFromExtPubKeyUnitTestSDT_RESTInterface( SdtderiveAddressFromExtPubKeyUnitTestSDT psdt ) : base(psdt)
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

		[DataMember(Name="deserializedExtPubKey", Order=1)]
		public  string gxTpr_Deserializedextpubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Deserializedextpubkey);

			}
			set { 
				 sdt.gxTpr_Deserializedextpubkey = value;
			}
		}

		[DataMember(Name="base", Order=2)]
		public  string gxTpr_Base
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Base, 10, 0));

			}
			set { 
				sdt.gxTpr_Base = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="start", Order=3)]
		public  string gxTpr_Start
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Start, 10, 0));

			}
			set { 
				sdt.gxTpr_Start = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="end", Order=4)]
		public  string gxTpr_End
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_End, 10, 0));

			}
			set { 
				sdt.gxTpr_End = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="sdt_addressess", Order=5, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem_RESTInterface> gxTpr_Sdt_addressess
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Sdt_addressess_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem_RESTInterface>(sdt.gxTpr_Sdt_addressess);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Sdt_addressess);
			}
		}

		[DataMember(Name="Expectedsdt_addressess", Order=6, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem_RESTInterface> gxTpr_Expectedsdt_addressess
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedsdt_addressess_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem_RESTInterface>(sdt.gxTpr_Expectedsdt_addressess);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Expectedsdt_addressess);
			}
		}

		[DataMember(Name="Msgsdt_addressess", Order=7)]
		public  string gxTpr_Msgsdt_addressess
		{
			get { 
				return sdt.gxTpr_Msgsdt_addressess;

			}
			set { 
				 sdt.gxTpr_Msgsdt_addressess = value;
			}
		}

		[DataMember(Name="error", Order=8)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[DataMember(Name="Expectederror", Order=9)]
		public  string gxTpr_Expectederror
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectederror);

			}
			set { 
				 sdt.gxTpr_Expectederror = value;
			}
		}

		[DataMember(Name="Msgerror", Order=10)]
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

		public SdtderiveAddressFromExtPubKeyUnitTestSDT sdt
		{
			get { 
				return (SdtderiveAddressFromExtPubKeyUnitTestSDT)Sdt;
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
				sdt = new SdtderiveAddressFromExtPubKeyUnitTestSDT() ;
			}
		}
	}
	#endregion
}