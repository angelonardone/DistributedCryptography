/*
				   File: type_SdtderiveAddresssTimeBountyTestSDT
			Description: deriveAddresssTimeBountyTestSDT
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
using GeneXus.Programs.wallet;
namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="deriveAddresssTimeBountyTestSDT")]
	[XmlType(TypeName="deriveAddresssTimeBountyTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtderiveAddresssTimeBountyTestSDT : GxUserType
	{
		public SdtderiveAddresssTimeBountyTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Testcaseid = "";

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions_N = true;

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt_N = true;

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Networktype = "";

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Secret = "";

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgsdt_addressess = "";

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Error = "";

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectederror = "";

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgerror = "";

		}

		public SdtderiveAddresssTimeBountyTestSDT(IGxContext context)
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

			if (gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions != null)
			{
				AddObjectProperty("StoredTransactions", gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions, false);
			}
			if (gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt != null)
			{
				AddObjectProperty("group_sdt", gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt, false);
			}

			AddObjectProperty("networkType", gxTpr_Networktype, false);


			AddObjectProperty("cuantity", gxTpr_Cuantity, false);


			AddObjectProperty("generatedType", gxTpr_Generatedtype, false);


			AddObjectProperty("secret", gxTpr_Secret, false);

			if (gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess != null)
			{
				AddObjectProperty("sdt_addressess", gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess, false);
			}
			if (gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess != null)
			{
				AddObjectProperty("Expectedsdt_addressess", gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess, false);
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
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}



		[SoapElement(ElementName="StoredTransactions")]
		[XmlElement(ElementName="StoredTransactions")]
		public GeneXus.Programs.wallet.SdtStoredTransactions gxTpr_Storedtransactions
		{
			get {
				if ( gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions == null )
				{
					gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions = new GeneXus.Programs.wallet.SdtStoredTransactions(context);
					SetDirty("Storedtransactions");
				}
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions = value;
				SetDirty("Storedtransactions");
			}
		}
		public void gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions_SetNull()
		{
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions_N = true;
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions = null;
		}

		public bool gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions_IsNull()
		{
			return gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions == null;
		}
		public bool ShouldSerializegxTpr_Storedtransactions_Json()
		{
			return gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions != null;

		}

		[SoapElement(ElementName="group_sdt")]
		[XmlElement(ElementName="group_sdt")]
		public GeneXus.Programs.wallet.registered.SdtGroup_SDT gxTpr_Group_sdt
		{
			get {
				if ( gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt == null )
				{
					gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt = new GeneXus.Programs.wallet.registered.SdtGroup_SDT(context);
					SetDirty("Group_sdt");
				}
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt = value;
				SetDirty("Group_sdt");
			}
		}
		public void gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt_SetNull()
		{
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt_N = true;
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt = null;
		}

		public bool gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt_IsNull()
		{
			return gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt == null;
		}
		public bool ShouldSerializegxTpr_Group_sdt_Json()
		{
			return gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt != null;

		}


		[SoapElement(ElementName="networkType")]
		[XmlElement(ElementName="networkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Networktype; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="cuantity")]
		[XmlElement(ElementName="cuantity")]
		public short gxTpr_Cuantity
		{
			get {
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Cuantity; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Cuantity = value;
				SetDirty("Cuantity");
			}
		}




		[SoapElement(ElementName="generatedType")]
		[XmlElement(ElementName="generatedType")]
		public short gxTpr_Generatedtype
		{
			get {
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Generatedtype; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Generatedtype = value;
				SetDirty("Generatedtype");
			}
		}




		[SoapElement(ElementName="secret")]
		[XmlElement(ElementName="secret")]
		public string gxTpr_Secret
		{
			get {
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Secret; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Secret = value;
				SetDirty("Secret");
			}
		}




		[SoapElement(ElementName="sdt_addressess" )]
		[XmlArray(ElementName="sdt_addressess"  )]
		[XmlArrayItemAttribute(ElementName="SDT_AddressessItem" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTpr_Sdt_addressess_GXBaseCollection
		{
			get {
				if ( gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess == null )
				{
					gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_Addressess", "");
				}
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess;
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess_N = false;
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTpr_Sdt_addressess
		{
			get {
				if ( gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess == null )
				{
					gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_Addressess", "");
				}
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess_N = false;
				SetDirty("Sdt_addressess");
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess ;
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess_N = false;
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess = value;
				SetDirty("Sdt_addressess");
			}
		}

		public void gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess_SetNull()
		{
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess_N = true;
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess = null;
		}

		public bool gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess_IsNull()
		{
			return gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess == null;
		}
		public bool ShouldSerializegxTpr_Sdt_addressess_GXBaseCollection_Json()
		{
			return gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess != null && gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess.Count > 0;

		}


		[SoapElement(ElementName="Expectedsdt_addressess" )]
		[XmlArray(ElementName="Expectedsdt_addressess"  )]
		[XmlArrayItemAttribute(ElementName="SDT_AddressessItem" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTpr_Expectedsdt_addressess_GXBaseCollection
		{
			get {
				if ( gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess == null )
				{
					gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_Addressess", "");
				}
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess;
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess_N = false;
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTpr_Expectedsdt_addressess
		{
			get {
				if ( gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess == null )
				{
					gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem>( context, "SDT_Addressess", "");
				}
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess_N = false;
				SetDirty("Expectedsdt_addressess");
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess ;
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess_N = false;
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess = value;
				SetDirty("Expectedsdt_addressess");
			}
		}

		public void gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess_SetNull()
		{
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess_N = true;
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess = null;
		}

		public bool gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess_IsNull()
		{
			return gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess == null;
		}
		public bool ShouldSerializegxTpr_Expectedsdt_addressess_GXBaseCollection_Json()
		{
			return gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess != null && gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess.Count > 0;

		}


		[SoapElement(ElementName="Msgsdt_addressess")]
		[XmlElement(ElementName="Msgsdt_addressess")]
		public string gxTpr_Msgsdt_addressess
		{
			get {
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgsdt_addressess; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgsdt_addressess = value;
				SetDirty("Msgsdt_addressess");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Error; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgerror = value;
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
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Testcaseid = "";

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions_N = true;


			gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt_N = true;

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Networktype = "";


			gxTv_SdtderiveAddresssTimeBountyTestSDT_Secret = "";

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess_N = true;


			gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess_N = true;

			gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgsdt_addressess = "";
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Error = "";
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectederror = "";
			gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtderiveAddresssTimeBountyTestSDT_Testcaseid;
		 

		protected GeneXus.Programs.wallet.SdtStoredTransactions gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions = null;
		protected bool gxTv_SdtderiveAddresssTimeBountyTestSDT_Storedtransactions_N;
		 

		protected GeneXus.Programs.wallet.registered.SdtGroup_SDT gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt = null;
		protected bool gxTv_SdtderiveAddresssTimeBountyTestSDT_Group_sdt_N;
		 

		protected string gxTv_SdtderiveAddresssTimeBountyTestSDT_Networktype;
		 

		protected short gxTv_SdtderiveAddresssTimeBountyTestSDT_Cuantity;
		 

		protected short gxTv_SdtderiveAddresssTimeBountyTestSDT_Generatedtype;
		 

		protected string gxTv_SdtderiveAddresssTimeBountyTestSDT_Secret;
		 
		protected bool gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess_N;
		protected GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTv_SdtderiveAddresssTimeBountyTestSDT_Sdt_addressess = null;  
		protected bool gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess_N;
		protected GXBaseCollection<GeneXus.Programs.nbitcoin.SdtSDT_Addressess_SDT_AddressessItem> gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectedsdt_addressess = null;  

		protected string gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgsdt_addressess;
		 

		protected string gxTv_SdtderiveAddresssTimeBountyTestSDT_Error;
		 

		protected string gxTv_SdtderiveAddresssTimeBountyTestSDT_Expectederror;
		 

		protected string gxTv_SdtderiveAddresssTimeBountyTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"deriveAddresssTimeBountyTestSDT", Namespace="distributedcryptography")]
	public class SdtderiveAddresssTimeBountyTestSDT_RESTInterface : GxGenericCollectionItem<SdtderiveAddresssTimeBountyTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtderiveAddresssTimeBountyTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtderiveAddresssTimeBountyTestSDT_RESTInterface( SdtderiveAddresssTimeBountyTestSDT psdt ) : base(psdt)
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

		[JsonPropertyName("StoredTransactions")]
		[JsonPropertyOrder(1)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="StoredTransactions", Order=1, EmitDefaultValue=false)]
		public GeneXus.Programs.wallet.SdtStoredTransactions_RESTInterface gxTpr_Storedtransactions
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Storedtransactions_Json())
					return new GeneXus.Programs.wallet.SdtStoredTransactions_RESTInterface(sdt.gxTpr_Storedtransactions);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Storedtransactions = value.sdt;
			}
		}

		[JsonPropertyName("group_sdt")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="group_sdt", Order=2, EmitDefaultValue=false)]
		public GeneXus.Programs.wallet.registered.SdtGroup_SDT_RESTInterface gxTpr_Group_sdt
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Group_sdt_Json())
					return new GeneXus.Programs.wallet.registered.SdtGroup_SDT_RESTInterface(sdt.gxTpr_Group_sdt);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Group_sdt = value.sdt;
			}
		}

		[JsonPropertyName("networkType")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="networkType", Order=3)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[JsonPropertyName("cuantity")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="cuantity", Order=4)]
		public short gxTpr_Cuantity
		{
			get { 
				return sdt.gxTpr_Cuantity;

			}
			set { 
				sdt.gxTpr_Cuantity = value;
			}
		}

		[JsonPropertyName("generatedType")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="generatedType", Order=5)]
		public short gxTpr_Generatedtype
		{
			get { 
				return sdt.gxTpr_Generatedtype;

			}
			set { 
				sdt.gxTpr_Generatedtype = value;
			}
		}

		[JsonPropertyName("secret")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="secret", Order=6)]
		public  string gxTpr_Secret
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Secret);

			}
			set { 
				 sdt.gxTpr_Secret = value;
			}
		}

		[JsonPropertyName("sdt_addressess")]
		[JsonPropertyOrder(7)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="sdt_addressess", Order=7, EmitDefaultValue=false)]
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

		[JsonPropertyName("Expectedsdt_addressess")]
		[JsonPropertyOrder(8)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Expectedsdt_addressess", Order=8, EmitDefaultValue=false)]
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

		[JsonPropertyName("Msgsdt_addressess")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="Msgsdt_addressess", Order=9)]
		public  string gxTpr_Msgsdt_addressess
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msgsdt_addressess);

			}
			set { 
				 sdt.gxTpr_Msgsdt_addressess = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="error", Order=10)]
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
		[JsonPropertyOrder(11)]
		[DataMember(Name="Expectederror", Order=11)]
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
		[JsonPropertyOrder(12)]
		[DataMember(Name="Msgerror", Order=12)]
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
		public SdtderiveAddresssTimeBountyTestSDT sdt
		{
			get { 
				return (SdtderiveAddresssTimeBountyTestSDT)Sdt;
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
				sdt = new SdtderiveAddresssTimeBountyTestSDT() ;
			}
		}
	}
	#endregion
}