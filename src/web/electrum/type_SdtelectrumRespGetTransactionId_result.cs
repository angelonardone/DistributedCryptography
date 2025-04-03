/*
				   File: type_SdtelectrumRespGetTransactionId_result
			Description: result
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
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="electrumRespGetTransactionId.result")]
	[XmlType(TypeName="electrumRespGetTransactionId.result" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespGetTransactionId_result : GxUserType
	{
		public SdtelectrumRespGetTransactionId_result( )
		{
			/* Constructor for serialization */
			gxTv_SdtelectrumRespGetTransactionId_result_Blockhash = "";

			gxTv_SdtelectrumRespGetTransactionId_result_Hash = "";

			gxTv_SdtelectrumRespGetTransactionId_result_Hex = "";

			gxTv_SdtelectrumRespGetTransactionId_result_Txid = "";


		}

		public SdtelectrumRespGetTransactionId_result(IGxContext context)
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
			AddObjectProperty("blockhash", gxTpr_Blockhash, false);


			AddObjectProperty("blocktime", gxTpr_Blocktime, false);


			AddObjectProperty("confirmations", gxTpr_Confirmations, false);


			AddObjectProperty("hash", gxTpr_Hash, false);


			AddObjectProperty("hex", gxTpr_Hex, false);


			AddObjectProperty("locktime", gxTpr_Locktime, false);


			AddObjectProperty("size", gxTpr_Size, false);


			AddObjectProperty("time", gxTpr_Time, false);


			AddObjectProperty("txid", gxTpr_Txid, false);


			AddObjectProperty("version", gxTpr_Version, false);

			if (gxTv_SdtelectrumRespGetTransactionId_result_Vin != null)
			{
				AddObjectProperty("vin", gxTv_SdtelectrumRespGetTransactionId_result_Vin, false);
			}
			if (gxTv_SdtelectrumRespGetTransactionId_result_Vout != null)
			{
				AddObjectProperty("vout", gxTv_SdtelectrumRespGetTransactionId_result_Vout, false);
			}

			AddObjectProperty("vsize", gxTpr_Vsize, false);


			AddObjectProperty("weight", gxTpr_Weight, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="blockhash")]
		[XmlElement(ElementName="blockhash")]
		public string gxTpr_Blockhash
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Blockhash; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Blockhash = value;
				SetDirty("Blockhash");
			}
		}



		[SoapElement(ElementName="blocktime")]
		[XmlElement(ElementName="blocktime")]
		public string gxTpr_Blocktime_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_Blocktime, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Blocktime = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Blocktime
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Blocktime; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Blocktime = value;
				SetDirty("Blocktime");
			}
		}



		[SoapElement(ElementName="confirmations")]
		[XmlElement(ElementName="confirmations")]
		public string gxTpr_Confirmations_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_Confirmations, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Confirmations = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Confirmations
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Confirmations; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Confirmations = value;
				SetDirty("Confirmations");
			}
		}




		[SoapElement(ElementName="hash")]
		[XmlElement(ElementName="hash")]
		public string gxTpr_Hash
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Hash; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Hash = value;
				SetDirty("Hash");
			}
		}




		[SoapElement(ElementName="hex")]
		[XmlElement(ElementName="hex")]
		public string gxTpr_Hex
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Hex; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Hex = value;
				SetDirty("Hex");
			}
		}



		[SoapElement(ElementName="locktime")]
		[XmlElement(ElementName="locktime")]
		public string gxTpr_Locktime_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_Locktime, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Locktime = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Locktime
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Locktime; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Locktime = value;
				SetDirty("Locktime");
			}
		}



		[SoapElement(ElementName="size")]
		[XmlElement(ElementName="size")]
		public string gxTpr_Size_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_Size, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Size = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Size
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Size; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Size = value;
				SetDirty("Size");
			}
		}



		[SoapElement(ElementName="time")]
		[XmlElement(ElementName="time")]
		public string gxTpr_Time_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_Time, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Time = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Time
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Time; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Time = value;
				SetDirty("Time");
			}
		}




		[SoapElement(ElementName="txid")]
		[XmlElement(ElementName="txid")]
		public string gxTpr_Txid
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Txid; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Txid = value;
				SetDirty("Txid");
			}
		}



		[SoapElement(ElementName="version")]
		[XmlElement(ElementName="version")]
		public string gxTpr_Version_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_Version, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Version = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Version
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Version; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Version = value;
				SetDirty("Version");
			}
		}




		[SoapElement(ElementName="vin" )]
		[XmlArray(ElementName="vin"  )]
		[XmlArrayItemAttribute(ElementName="vinItem" , IsNullable=false )]
		public GXBaseCollection<SdtelectrumRespGetTransactionId_result_vinItem> gxTpr_Vin
		{
			get {
				if ( gxTv_SdtelectrumRespGetTransactionId_result_Vin == null )
				{
					gxTv_SdtelectrumRespGetTransactionId_result_Vin = new GXBaseCollection<SdtelectrumRespGetTransactionId_result_vinItem>( context, "electrumRespGetTransactionId.result.vinItem", "");
				}
				SetDirty("Vin");
				return gxTv_SdtelectrumRespGetTransactionId_result_Vin;
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Vin_N = false;
				gxTv_SdtelectrumRespGetTransactionId_result_Vin = value;
				SetDirty("Vin");
			}
		}

		public void gxTv_SdtelectrumRespGetTransactionId_result_Vin_SetNull()
		{
			gxTv_SdtelectrumRespGetTransactionId_result_Vin_N = true;
			gxTv_SdtelectrumRespGetTransactionId_result_Vin = null;
		}

		public bool gxTv_SdtelectrumRespGetTransactionId_result_Vin_IsNull()
		{
			return gxTv_SdtelectrumRespGetTransactionId_result_Vin == null;
		}
		public bool ShouldSerializegxTpr_Vin_GxSimpleCollection_Json()
		{
			return gxTv_SdtelectrumRespGetTransactionId_result_Vin != null && gxTv_SdtelectrumRespGetTransactionId_result_Vin.Count > 0;

		}



		[SoapElement(ElementName="vout" )]
		[XmlArray(ElementName="vout"  )]
		[XmlArrayItemAttribute(ElementName="voutItem" , IsNullable=false )]
		public GXBaseCollection<SdtelectrumRespGetTransactionId_result_voutItem> gxTpr_Vout
		{
			get {
				if ( gxTv_SdtelectrumRespGetTransactionId_result_Vout == null )
				{
					gxTv_SdtelectrumRespGetTransactionId_result_Vout = new GXBaseCollection<SdtelectrumRespGetTransactionId_result_voutItem>( context, "electrumRespGetTransactionId.result.voutItem", "");
				}
				SetDirty("Vout");
				return gxTv_SdtelectrumRespGetTransactionId_result_Vout;
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Vout_N = false;
				gxTv_SdtelectrumRespGetTransactionId_result_Vout = value;
				SetDirty("Vout");
			}
		}

		public void gxTv_SdtelectrumRespGetTransactionId_result_Vout_SetNull()
		{
			gxTv_SdtelectrumRespGetTransactionId_result_Vout_N = true;
			gxTv_SdtelectrumRespGetTransactionId_result_Vout = null;
		}

		public bool gxTv_SdtelectrumRespGetTransactionId_result_Vout_IsNull()
		{
			return gxTv_SdtelectrumRespGetTransactionId_result_Vout == null;
		}
		public bool ShouldSerializegxTpr_Vout_GxSimpleCollection_Json()
		{
			return gxTv_SdtelectrumRespGetTransactionId_result_Vout != null && gxTv_SdtelectrumRespGetTransactionId_result_Vout.Count > 0;

		}


		[SoapElement(ElementName="vsize")]
		[XmlElement(ElementName="vsize")]
		public string gxTpr_Vsize_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_Vsize, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Vsize = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Vsize
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Vsize; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Vsize = value;
				SetDirty("Vsize");
			}
		}



		[SoapElement(ElementName="weight")]
		[XmlElement(ElementName="weight")]
		public string gxTpr_Weight_double
		{
			get {
				return Convert.ToString(gxTv_SdtelectrumRespGetTransactionId_result_Weight, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Weight = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Weight
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_result_Weight; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_result_Weight = value;
				SetDirty("Weight");
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
			gxTv_SdtelectrumRespGetTransactionId_result_Blockhash = "";


			gxTv_SdtelectrumRespGetTransactionId_result_Hash = "";
			gxTv_SdtelectrumRespGetTransactionId_result_Hex = "";



			gxTv_SdtelectrumRespGetTransactionId_result_Txid = "";


			gxTv_SdtelectrumRespGetTransactionId_result_Vin_N = true;


			gxTv_SdtelectrumRespGetTransactionId_result_Vout_N = true;



			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtelectrumRespGetTransactionId_result_Blockhash;
		 

		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_Blocktime;
		 

		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_Confirmations;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_result_Hash;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_result_Hex;
		 

		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_Locktime;
		 

		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_Size;
		 

		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_Time;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_result_Txid;
		 

		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_Version;
		 
		protected bool gxTv_SdtelectrumRespGetTransactionId_result_Vin_N;
		protected GXBaseCollection<SdtelectrumRespGetTransactionId_result_vinItem> gxTv_SdtelectrumRespGetTransactionId_result_Vin = null; 

		protected bool gxTv_SdtelectrumRespGetTransactionId_result_Vout_N;
		protected GXBaseCollection<SdtelectrumRespGetTransactionId_result_voutItem> gxTv_SdtelectrumRespGetTransactionId_result_Vout = null; 


		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_Vsize;
		 

		protected decimal gxTv_SdtelectrumRespGetTransactionId_result_Weight;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"electrumRespGetTransactionId.result", Namespace="distributedcryptography")]
	public class SdtelectrumRespGetTransactionId_result_RESTInterface : GxGenericCollectionItem<SdtelectrumRespGetTransactionId_result>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespGetTransactionId_result_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespGetTransactionId_result_RESTInterface( SdtelectrumRespGetTransactionId_result psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("blockhash")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="blockhash", Order=0)]
		public  string gxTpr_Blockhash
		{
			get { 
				return sdt.gxTpr_Blockhash;

			}
			set { 
				 sdt.gxTpr_Blockhash = value;
			}
		}

		[JsonPropertyName("blocktime")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="blocktime", Order=1)]
		public  string gxTpr_Blocktime
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Blocktime, 10, 5));

			}
			set { 
				sdt.gxTpr_Blocktime =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("confirmations")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="confirmations", Order=2)]
		public  string gxTpr_Confirmations
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Confirmations, 10, 5));

			}
			set { 
				sdt.gxTpr_Confirmations =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("hash")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="hash", Order=3)]
		public  string gxTpr_Hash
		{
			get { 
				return sdt.gxTpr_Hash;

			}
			set { 
				 sdt.gxTpr_Hash = value;
			}
		}

		[JsonPropertyName("hex")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="hex", Order=4)]
		public  string gxTpr_Hex
		{
			get { 
				return sdt.gxTpr_Hex;

			}
			set { 
				 sdt.gxTpr_Hex = value;
			}
		}

		[JsonPropertyName("locktime")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="locktime", Order=5)]
		public  string gxTpr_Locktime
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Locktime, 10, 5));

			}
			set { 
				sdt.gxTpr_Locktime =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("size")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="size", Order=6)]
		public  string gxTpr_Size
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Size, 10, 5));

			}
			set { 
				sdt.gxTpr_Size =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("time")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="time", Order=7)]
		public  string gxTpr_Time
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Time, 10, 5));

			}
			set { 
				sdt.gxTpr_Time =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("txid")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="txid", Order=8)]
		public  string gxTpr_Txid
		{
			get { 
				return sdt.gxTpr_Txid;

			}
			set { 
				 sdt.gxTpr_Txid = value;
			}
		}

		[JsonPropertyName("version")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="version", Order=9)]
		public  string gxTpr_Version
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Version, 10, 5));

			}
			set { 
				sdt.gxTpr_Version =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("vin")]
		[JsonPropertyOrder(10)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="vin", Order=10, EmitDefaultValue=false)]
		public GxGenericCollection<SdtelectrumRespGetTransactionId_result_vinItem_RESTInterface> gxTpr_Vin
		{
			get {
				if (sdt.ShouldSerializegxTpr_Vin_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtelectrumRespGetTransactionId_result_vinItem_RESTInterface>(sdt.gxTpr_Vin);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Vin);
			}
		}

		[JsonPropertyName("vout")]
		[JsonPropertyOrder(11)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="vout", Order=11, EmitDefaultValue=false)]
		public GxGenericCollection<SdtelectrumRespGetTransactionId_result_voutItem_RESTInterface> gxTpr_Vout
		{
			get {
				if (sdt.ShouldSerializegxTpr_Vout_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtelectrumRespGetTransactionId_result_voutItem_RESTInterface>(sdt.gxTpr_Vout);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Vout);
			}
		}

		[JsonPropertyName("vsize")]
		[JsonPropertyOrder(12)]
		[DataMember(Name="vsize", Order=12)]
		public  string gxTpr_Vsize
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Vsize, 10, 5));

			}
			set { 
				sdt.gxTpr_Vsize =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("weight")]
		[JsonPropertyOrder(13)]
		[DataMember(Name="weight", Order=13)]
		public  string gxTpr_Weight
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Weight, 10, 5));

			}
			set { 
				sdt.gxTpr_Weight =  NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtelectrumRespGetTransactionId_result sdt
		{
			get { 
				return (SdtelectrumRespGetTransactionId_result)Sdt;
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
				sdt = new SdtelectrumRespGetTransactionId_result() ;
			}
		}
	}
	#endregion
}