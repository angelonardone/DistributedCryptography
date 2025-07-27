/*
				   File: type_SdtMultiSigSignatureData_DataItem
			Description: Data
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
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="MultiSigSignatureData.DataItem")]
	[XmlType(TypeName="MultiSigSignatureData.DataItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMultiSigSignatureData_DataItem : GxUserType
	{
		public SdtMultiSigSignatureData_DataItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtMultiSigSignatureData_DataItem_Transactionid = "";

			gxTv_SdtMultiSigSignatureData_DataItem_Ec_pubkey = "";

			gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination_N = true;

		}

		public SdtMultiSigSignatureData_DataItem(IGxContext context)
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
			AddObjectProperty("i", gxTpr_I, false);


			AddObjectProperty("p", gxTpr_P, false);

			if (gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces != null)
			{
				AddObjectProperty("pubNonces", gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces, false);
			}

			AddObjectProperty("transactionId", gxTpr_Transactionid, false);


			AddObjectProperty("n", gxTpr_N, false);


			AddObjectProperty("Ec_pubKey", gxTpr_Ec_pubkey, false);

			if (gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination != null)
			{
				AddObjectProperty("finalCombination", gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination, false);
			}
			if (gxTv_SdtMultiSigSignatureData_DataItem_Signatures != null)
			{
				AddObjectProperty("signatures", gxTv_SdtMultiSigSignatureData_DataItem_Signatures, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="i")]
		[XmlElement(ElementName="i")]
		public int gxTpr_I
		{
			get {
				return gxTv_SdtMultiSigSignatureData_DataItem_I; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_I = value;
				SetDirty("I");
			}
		}




		[SoapElement(ElementName="p")]
		[XmlElement(ElementName="p")]
		public int gxTpr_P
		{
			get {
				return gxTv_SdtMultiSigSignatureData_DataItem_P; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_P = value;
				SetDirty("P");
			}
		}




		[SoapElement(ElementName="pubNonces" )]
		[XmlArray(ElementName="pubNonces"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GxSimpleCollection<string> gxTpr_Pubnonces_GxSimpleCollection
		{
			get {
				if ( gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces == null )
				{
					gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces = new GxSimpleCollection<string>( );
				}
				return gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces;
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces_N = false;
				gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces = value;
			}
		}

		[XmlIgnore]
		public GxSimpleCollection<string> gxTpr_Pubnonces
		{
			get {
				if ( gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces == null )
				{
					gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces = new GxSimpleCollection<string>();
				}
				gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces_N = false;
				SetDirty("Pubnonces");
				return gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces ;
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces_N = false;
				gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces = value;
				SetDirty("Pubnonces");
			}
		}

		public void gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces_SetNull()
		{
			gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces_N = true;
			gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces = null;
		}

		public bool gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces_IsNull()
		{
			return gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces == null;
		}
		public bool ShouldSerializegxTpr_Pubnonces_GxSimpleCollection_Json()
		{
			return gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces != null && gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces.Count > 0;

		}


		[SoapElement(ElementName="transactionId")]
		[XmlElement(ElementName="transactionId")]
		public string gxTpr_Transactionid
		{
			get {
				return gxTv_SdtMultiSigSignatureData_DataItem_Transactionid; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_Transactionid = value;
				SetDirty("Transactionid");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public long gxTpr_N
		{
			get {
				return gxTv_SdtMultiSigSignatureData_DataItem_N; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_N = value;
				SetDirty("N");
			}
		}




		[SoapElement(ElementName="Ec_pubKey")]
		[XmlElement(ElementName="Ec_pubKey")]
		public string gxTpr_Ec_pubkey
		{
			get {
				return gxTv_SdtMultiSigSignatureData_DataItem_Ec_pubkey; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_Ec_pubkey = value;
				SetDirty("Ec_pubkey");
			}
		}



		[SoapElement(ElementName="finalCombination")]
		[XmlElement(ElementName="finalCombination")]
		public GeneXus.Programs.math.SdtfinalCombination gxTpr_Finalcombination
		{
			get {
				if ( gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination == null )
				{
					gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination = new GeneXus.Programs.math.SdtfinalCombination(context);
					SetDirty("Finalcombination");
				}
				return gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination; 
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination = value;
				SetDirty("Finalcombination");
			}
		}
		public void gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination_SetNull()
		{
			gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination_N = true;
			gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination = null;
		}

		public bool gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination_IsNull()
		{
			return gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination == null;
		}
		public bool ShouldSerializegxTpr_Finalcombination_Json()
		{
			return gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination != null;

		}


		[SoapElement(ElementName="signatures" )]
		[XmlArray(ElementName="signatures"  )]
		[XmlArrayItemAttribute(ElementName="signaturesItem" , IsNullable=false )]
		public GXBaseCollection<SdtMultiSigSignatureData_DataItem_signaturesItem> gxTpr_Signatures
		{
			get {
				if ( gxTv_SdtMultiSigSignatureData_DataItem_Signatures == null )
				{
					gxTv_SdtMultiSigSignatureData_DataItem_Signatures = new GXBaseCollection<SdtMultiSigSignatureData_DataItem_signaturesItem>( context, "MultiSigSignatureData.DataItem.signaturesItem", "");
				}
				SetDirty("Signatures");
				return gxTv_SdtMultiSigSignatureData_DataItem_Signatures;
			}
			set {
				gxTv_SdtMultiSigSignatureData_DataItem_Signatures_N = false;
				gxTv_SdtMultiSigSignatureData_DataItem_Signatures = value;
				SetDirty("Signatures");
			}
		}

		public void gxTv_SdtMultiSigSignatureData_DataItem_Signatures_SetNull()
		{
			gxTv_SdtMultiSigSignatureData_DataItem_Signatures_N = true;
			gxTv_SdtMultiSigSignatureData_DataItem_Signatures = null;
		}

		public bool gxTv_SdtMultiSigSignatureData_DataItem_Signatures_IsNull()
		{
			return gxTv_SdtMultiSigSignatureData_DataItem_Signatures == null;
		}
		public bool ShouldSerializegxTpr_Signatures_GxSimpleCollection_Json()
		{
			return gxTv_SdtMultiSigSignatureData_DataItem_Signatures != null && gxTv_SdtMultiSigSignatureData_DataItem_Signatures.Count > 0;

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
			gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces_N = true;

			gxTv_SdtMultiSigSignatureData_DataItem_Transactionid = "";

			gxTv_SdtMultiSigSignatureData_DataItem_Ec_pubkey = "";

			gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination_N = true;


			gxTv_SdtMultiSigSignatureData_DataItem_Signatures_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected int gxTv_SdtMultiSigSignatureData_DataItem_I;
		 

		protected int gxTv_SdtMultiSigSignatureData_DataItem_P;
		 
		protected bool gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces_N;
		protected GxSimpleCollection<string> gxTv_SdtMultiSigSignatureData_DataItem_Pubnonces = null;  

		protected string gxTv_SdtMultiSigSignatureData_DataItem_Transactionid;
		 

		protected long gxTv_SdtMultiSigSignatureData_DataItem_N;
		 

		protected string gxTv_SdtMultiSigSignatureData_DataItem_Ec_pubkey;
		 

		protected GeneXus.Programs.math.SdtfinalCombination gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination = null;
		protected bool gxTv_SdtMultiSigSignatureData_DataItem_Finalcombination_N;
		 
		protected bool gxTv_SdtMultiSigSignatureData_DataItem_Signatures_N;
		protected GXBaseCollection<SdtMultiSigSignatureData_DataItem_signaturesItem> gxTv_SdtMultiSigSignatureData_DataItem_Signatures = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"MultiSigSignatureData.DataItem", Namespace="distributedcryptography")]
	public class SdtMultiSigSignatureData_DataItem_RESTInterface : GxGenericCollectionItem<SdtMultiSigSignatureData_DataItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMultiSigSignatureData_DataItem_RESTInterface( ) : base()
		{	
		}

		public SdtMultiSigSignatureData_DataItem_RESTInterface( SdtMultiSigSignatureData_DataItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("i")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="i", Order=0)]
		public int gxTpr_I
		{
			get { 
				return sdt.gxTpr_I;

			}
			set { 
				sdt.gxTpr_I = value;
			}
		}

		[JsonPropertyName("p")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="p", Order=1)]
		public int gxTpr_P
		{
			get { 
				return sdt.gxTpr_P;

			}
			set { 
				sdt.gxTpr_P = value;
			}
		}

		[JsonPropertyName("pubNonces")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="pubNonces", Order=2, EmitDefaultValue=false)]
		public  GxSimpleCollection<string> gxTpr_Pubnonces
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Pubnonces_GxSimpleCollection_Json())
					return sdt.gxTpr_Pubnonces;
				else
					return null;

			}
			set { 
				sdt.gxTpr_Pubnonces = value ;
			}
		}

		[JsonPropertyName("transactionId")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="transactionId", Order=3)]
		public  string gxTpr_Transactionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Transactionid);

			}
			set { 
				 sdt.gxTpr_Transactionid = value;
			}
		}

		[JsonPropertyName("n")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="n", Order=4)]
		public  string gxTpr_N
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_N, 10, 0));

			}
			set { 
				sdt.gxTpr_N = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("Ec_pubKey")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="Ec_pubKey", Order=5)]
		public  string gxTpr_Ec_pubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Ec_pubkey);

			}
			set { 
				 sdt.gxTpr_Ec_pubkey = value;
			}
		}

		[JsonPropertyName("finalCombination")]
		[JsonPropertyOrder(6)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="finalCombination", Order=6, EmitDefaultValue=false)]
		public GeneXus.Programs.math.SdtfinalCombination_RESTInterface gxTpr_Finalcombination
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Finalcombination_Json())
					return new GeneXus.Programs.math.SdtfinalCombination_RESTInterface(sdt.gxTpr_Finalcombination);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Finalcombination = value.sdt;
			}
		}

		[JsonPropertyName("signatures")]
		[JsonPropertyOrder(7)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="signatures", Order=7, EmitDefaultValue=false)]
		public GxGenericCollection<SdtMultiSigSignatureData_DataItem_signaturesItem_RESTInterface> gxTpr_Signatures
		{
			get {
				if (sdt.ShouldSerializegxTpr_Signatures_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtMultiSigSignatureData_DataItem_signaturesItem_RESTInterface>(sdt.gxTpr_Signatures);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Signatures);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMultiSigSignatureData_DataItem sdt
		{
			get { 
				return (SdtMultiSigSignatureData_DataItem)Sdt;
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
				sdt = new SdtMultiSigSignatureData_DataItem() ;
			}
		}
	}
	#endregion
}