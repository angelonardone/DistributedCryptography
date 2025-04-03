/*
				   File: type_SdttestCombinationsSDT
			Description: testCombinationsSDT
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
namespace GeneXus.Programs.math
{
	[XmlRoot(ElementName="testCombinationsSDT")]
	[XmlType(TypeName="testCombinationsSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdttestCombinationsSDT : GxUserType
	{
		public SdttestCombinationsSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdttestCombinationsSDT_Testcaseid = "";

			gxTv_SdttestCombinationsSDT_Msgfinalcombinations = "";

		}

		public SdttestCombinationsSDT(IGxContext context)
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

			if (gxTv_SdttestCombinationsSDT_Items != null)
			{
				AddObjectProperty("items", gxTv_SdttestCombinationsSDT_Items, false);
			}

			AddObjectProperty("minSignatures", gxTpr_Minsignatures, false);

			if (gxTv_SdttestCombinationsSDT_Finalcombinations != null)
			{
				AddObjectProperty("finalCombinations", gxTv_SdttestCombinationsSDT_Finalcombinations, false);
			}
			if (gxTv_SdttestCombinationsSDT_Expectedfinalcombinations != null)
			{
				AddObjectProperty("ExpectedfinalCombinations", gxTv_SdttestCombinationsSDT_Expectedfinalcombinations, false);
			}

			AddObjectProperty("MsgfinalCombinations", gxTpr_Msgfinalcombinations, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TestCaseId")]
		[XmlElement(ElementName="TestCaseId")]
		public string gxTpr_Testcaseid
		{
			get {
				return gxTv_SdttestCombinationsSDT_Testcaseid; 
			}
			set {
				gxTv_SdttestCombinationsSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="items" )]
		[XmlArray(ElementName="items"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GxSimpleCollection<string> gxTpr_Items_GxSimpleCollection
		{
			get {
				if ( gxTv_SdttestCombinationsSDT_Items == null )
				{
					gxTv_SdttestCombinationsSDT_Items = new GxSimpleCollection<string>( );
				}
				return gxTv_SdttestCombinationsSDT_Items;
			}
			set {
				gxTv_SdttestCombinationsSDT_Items_N = false;
				gxTv_SdttestCombinationsSDT_Items = value;
			}
		}

		[XmlIgnore]
		public GxSimpleCollection<string> gxTpr_Items
		{
			get {
				if ( gxTv_SdttestCombinationsSDT_Items == null )
				{
					gxTv_SdttestCombinationsSDT_Items = new GxSimpleCollection<string>();
				}
				gxTv_SdttestCombinationsSDT_Items_N = false;
				SetDirty("Items");
				return gxTv_SdttestCombinationsSDT_Items ;
			}
			set {
				gxTv_SdttestCombinationsSDT_Items_N = false;
				gxTv_SdttestCombinationsSDT_Items = value;
				SetDirty("Items");
			}
		}

		public void gxTv_SdttestCombinationsSDT_Items_SetNull()
		{
			gxTv_SdttestCombinationsSDT_Items_N = true;
			gxTv_SdttestCombinationsSDT_Items = null;
		}

		public bool gxTv_SdttestCombinationsSDT_Items_IsNull()
		{
			return gxTv_SdttestCombinationsSDT_Items == null;
		}
		public bool ShouldSerializegxTpr_Items_GxSimpleCollection_Json()
		{
			return gxTv_SdttestCombinationsSDT_Items != null && gxTv_SdttestCombinationsSDT_Items.Count > 0;

		}


		[SoapElement(ElementName="minSignatures")]
		[XmlElement(ElementName="minSignatures")]
		public short gxTpr_Minsignatures
		{
			get {
				return gxTv_SdttestCombinationsSDT_Minsignatures; 
			}
			set {
				gxTv_SdttestCombinationsSDT_Minsignatures = value;
				SetDirty("Minsignatures");
			}
		}




		[SoapElement(ElementName="finalCombinations" )]
		[XmlArray(ElementName="finalCombinations"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> gxTpr_Finalcombinations_GXBaseCollection
		{
			get {
				if ( gxTv_SdttestCombinationsSDT_Finalcombinations == null )
				{
					gxTv_SdttestCombinationsSDT_Finalcombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "");
				}
				return gxTv_SdttestCombinationsSDT_Finalcombinations;
			}
			set {
				gxTv_SdttestCombinationsSDT_Finalcombinations_N = false;
				gxTv_SdttestCombinationsSDT_Finalcombinations = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> gxTpr_Finalcombinations
		{
			get {
				if ( gxTv_SdttestCombinationsSDT_Finalcombinations == null )
				{
					gxTv_SdttestCombinationsSDT_Finalcombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "");
				}
				gxTv_SdttestCombinationsSDT_Finalcombinations_N = false;
				SetDirty("Finalcombinations");
				return gxTv_SdttestCombinationsSDT_Finalcombinations ;
			}
			set {
				gxTv_SdttestCombinationsSDT_Finalcombinations_N = false;
				gxTv_SdttestCombinationsSDT_Finalcombinations = value;
				SetDirty("Finalcombinations");
			}
		}

		public void gxTv_SdttestCombinationsSDT_Finalcombinations_SetNull()
		{
			gxTv_SdttestCombinationsSDT_Finalcombinations_N = true;
			gxTv_SdttestCombinationsSDT_Finalcombinations = null;
		}

		public bool gxTv_SdttestCombinationsSDT_Finalcombinations_IsNull()
		{
			return gxTv_SdttestCombinationsSDT_Finalcombinations == null;
		}
		public bool ShouldSerializegxTpr_Finalcombinations_GXBaseCollection_Json()
		{
			return gxTv_SdttestCombinationsSDT_Finalcombinations != null && gxTv_SdttestCombinationsSDT_Finalcombinations.Count > 0;

		}


		[SoapElement(ElementName="ExpectedfinalCombinations" )]
		[XmlArray(ElementName="ExpectedfinalCombinations"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> gxTpr_Expectedfinalcombinations_GXBaseCollection
		{
			get {
				if ( gxTv_SdttestCombinationsSDT_Expectedfinalcombinations == null )
				{
					gxTv_SdttestCombinationsSDT_Expectedfinalcombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "");
				}
				return gxTv_SdttestCombinationsSDT_Expectedfinalcombinations;
			}
			set {
				gxTv_SdttestCombinationsSDT_Expectedfinalcombinations_N = false;
				gxTv_SdttestCombinationsSDT_Expectedfinalcombinations = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> gxTpr_Expectedfinalcombinations
		{
			get {
				if ( gxTv_SdttestCombinationsSDT_Expectedfinalcombinations == null )
				{
					gxTv_SdttestCombinationsSDT_Expectedfinalcombinations = new GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination>( context, "finalCombination", "");
				}
				gxTv_SdttestCombinationsSDT_Expectedfinalcombinations_N = false;
				SetDirty("Expectedfinalcombinations");
				return gxTv_SdttestCombinationsSDT_Expectedfinalcombinations ;
			}
			set {
				gxTv_SdttestCombinationsSDT_Expectedfinalcombinations_N = false;
				gxTv_SdttestCombinationsSDT_Expectedfinalcombinations = value;
				SetDirty("Expectedfinalcombinations");
			}
		}

		public void gxTv_SdttestCombinationsSDT_Expectedfinalcombinations_SetNull()
		{
			gxTv_SdttestCombinationsSDT_Expectedfinalcombinations_N = true;
			gxTv_SdttestCombinationsSDT_Expectedfinalcombinations = null;
		}

		public bool gxTv_SdttestCombinationsSDT_Expectedfinalcombinations_IsNull()
		{
			return gxTv_SdttestCombinationsSDT_Expectedfinalcombinations == null;
		}
		public bool ShouldSerializegxTpr_Expectedfinalcombinations_GXBaseCollection_Json()
		{
			return gxTv_SdttestCombinationsSDT_Expectedfinalcombinations != null && gxTv_SdttestCombinationsSDT_Expectedfinalcombinations.Count > 0;

		}


		[SoapElement(ElementName="MsgfinalCombinations")]
		[XmlElement(ElementName="MsgfinalCombinations")]
		public string gxTpr_Msgfinalcombinations
		{
			get {
				return gxTv_SdttestCombinationsSDT_Msgfinalcombinations; 
			}
			set {
				gxTv_SdttestCombinationsSDT_Msgfinalcombinations = value;
				SetDirty("Msgfinalcombinations");
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
			gxTv_SdttestCombinationsSDT_Testcaseid = "";

			gxTv_SdttestCombinationsSDT_Items_N = true;



			gxTv_SdttestCombinationsSDT_Finalcombinations_N = true;


			gxTv_SdttestCombinationsSDT_Expectedfinalcombinations_N = true;

			gxTv_SdttestCombinationsSDT_Msgfinalcombinations = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdttestCombinationsSDT_Testcaseid;
		 
		protected bool gxTv_SdttestCombinationsSDT_Items_N;
		protected GxSimpleCollection<string> gxTv_SdttestCombinationsSDT_Items = null;  

		protected short gxTv_SdttestCombinationsSDT_Minsignatures;
		 
		protected bool gxTv_SdttestCombinationsSDT_Finalcombinations_N;
		protected GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> gxTv_SdttestCombinationsSDT_Finalcombinations = null;  
		protected bool gxTv_SdttestCombinationsSDT_Expectedfinalcombinations_N;
		protected GXBaseCollection<GeneXus.Programs.math.SdtfinalCombination> gxTv_SdttestCombinationsSDT_Expectedfinalcombinations = null;  

		protected string gxTv_SdttestCombinationsSDT_Msgfinalcombinations;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"testCombinationsSDT", Namespace="distributedcryptography")]
	public class SdttestCombinationsSDT_RESTInterface : GxGenericCollectionItem<SdttestCombinationsSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdttestCombinationsSDT_RESTInterface( ) : base()
		{	
		}

		public SdttestCombinationsSDT_RESTInterface( SdttestCombinationsSDT psdt ) : base(psdt)
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

		[JsonPropertyName("items")]
		[JsonPropertyOrder(1)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="items", Order=1, EmitDefaultValue=false)]
		public  GxSimpleCollection<string> gxTpr_Items
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Items_GxSimpleCollection_Json())
					return sdt.gxTpr_Items;
				else
					return null;

			}
			set { 
				sdt.gxTpr_Items = value ;
			}
		}

		[JsonPropertyName("minSignatures")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="minSignatures", Order=2)]
		public short gxTpr_Minsignatures
		{
			get { 
				return sdt.gxTpr_Minsignatures;

			}
			set { 
				sdt.gxTpr_Minsignatures = value;
			}
		}

		[JsonPropertyName("finalCombinations")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="finalCombinations", Order=3, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.math.SdtfinalCombination_RESTInterface> gxTpr_Finalcombinations
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Finalcombinations_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.math.SdtfinalCombination_RESTInterface>(sdt.gxTpr_Finalcombinations);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Finalcombinations);
			}
		}

		[JsonPropertyName("ExpectedfinalCombinations")]
		[JsonPropertyOrder(4)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="ExpectedfinalCombinations", Order=4, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.math.SdtfinalCombination_RESTInterface> gxTpr_Expectedfinalcombinations
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedfinalcombinations_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.math.SdtfinalCombination_RESTInterface>(sdt.gxTpr_Expectedfinalcombinations);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Expectedfinalcombinations);
			}
		}

		[JsonPropertyName("MsgfinalCombinations")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="MsgfinalCombinations", Order=5)]
		public  string gxTpr_Msgfinalcombinations
		{
			get { 
				return sdt.gxTpr_Msgfinalcombinations;

			}
			set { 
				 sdt.gxTpr_Msgfinalcombinations = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdttestCombinationsSDT sdt
		{
			get { 
				return (SdttestCombinationsSDT)Sdt;
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
				sdt = new SdttestCombinationsSDT() ;
			}
		}
	}
	#endregion
}