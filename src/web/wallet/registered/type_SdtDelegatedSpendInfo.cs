/*
				   File: type_SdtDelegatedSpendInfo
			Description: DelegatedSpendInfo
				 Author: Nemo 🐠 for C# (.NET) version 18.0.16.189595
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
	[XmlRoot(ElementName="DelegatedSpendInfo")]
	[XmlType(TypeName="DelegatedSpendInfo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDelegatedSpendInfo : GxUserType
	{
		public SdtDelegatedSpendInfo( )
		{
			/* Constructor for serialization */
			gxTv_SdtDelegatedSpendInfo_Error = "";

			gxTv_SdtDelegatedSpendInfo_Sendto = "";

			gxTv_SdtDelegatedSpendInfo_Changeto = "";


		}

		public SdtDelegatedSpendInfo(IGxContext context)
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
			AddObjectProperty("success", gxTpr_Success, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("required", gxTpr_Required, false);


			AddObjectProperty("signatures", gxTpr_Signatures, false);


			AddObjectProperty("complete", gxTpr_Complete, false);


			AddObjectProperty("nextIsLast", gxTpr_Nextislast, false);


			AddObjectProperty("iSigned", gxTpr_Isigned, false);


			AddObjectProperty("sendTo", gxTpr_Sendto, false);


			AddObjectProperty("changeTo", gxTpr_Changeto, false);


			AddObjectProperty("amountBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Amountbtc, 16, 8)), false);


			AddObjectProperty("totalInBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Totalinbtc, 16, 8)), false);

			if (gxTv_SdtDelegatedSpendInfo_Levels != null)
			{
				AddObjectProperty("levels", gxTv_SdtDelegatedSpendInfo_Levels, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Success; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Error; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="required")]
		[XmlElement(ElementName="required")]
		public short gxTpr_Required
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Required; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Required = value;
				SetDirty("Required");
			}
		}




		[SoapElement(ElementName="signatures")]
		[XmlElement(ElementName="signatures")]
		public short gxTpr_Signatures
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Signatures; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Signatures = value;
				SetDirty("Signatures");
			}
		}




		[SoapElement(ElementName="complete")]
		[XmlElement(ElementName="complete")]
		public bool gxTpr_Complete
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Complete; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Complete = value;
				SetDirty("Complete");
			}
		}




		[SoapElement(ElementName="nextIsLast")]
		[XmlElement(ElementName="nextIsLast")]
		public bool gxTpr_Nextislast
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Nextislast; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Nextislast = value;
				SetDirty("Nextislast");
			}
		}




		[SoapElement(ElementName="iSigned")]
		[XmlElement(ElementName="iSigned")]
		public bool gxTpr_Isigned
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Isigned; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Isigned = value;
				SetDirty("Isigned");
			}
		}




		[SoapElement(ElementName="sendTo")]
		[XmlElement(ElementName="sendTo")]
		public string gxTpr_Sendto
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Sendto; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Sendto = value;
				SetDirty("Sendto");
			}
		}




		[SoapElement(ElementName="changeTo")]
		[XmlElement(ElementName="changeTo")]
		public string gxTpr_Changeto
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Changeto; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Changeto = value;
				SetDirty("Changeto");
			}
		}



		[SoapElement(ElementName="amountBtc")]
		[XmlElement(ElementName="amountBtc")]
		public string gxTpr_Amountbtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtDelegatedSpendInfo_Amountbtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Amountbtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Amountbtc
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Amountbtc; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Amountbtc = value;
				SetDirty("Amountbtc");
			}
		}



		[SoapElement(ElementName="totalInBtc")]
		[XmlElement(ElementName="totalInBtc")]
		public string gxTpr_Totalinbtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtDelegatedSpendInfo_Totalinbtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Totalinbtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Totalinbtc
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_Totalinbtc; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Totalinbtc = value;
				SetDirty("Totalinbtc");
			}
		}




		[SoapElement(ElementName="levels" )]
		[XmlArray(ElementName="levels"  )]
		[XmlArrayItemAttribute(ElementName="levelsItem" , IsNullable=false )]
		public GXBaseCollection<SdtDelegatedSpendInfo_levelsItem> gxTpr_Levels
		{
			get {
				if ( gxTv_SdtDelegatedSpendInfo_Levels == null )
				{
					gxTv_SdtDelegatedSpendInfo_Levels = new GXBaseCollection<SdtDelegatedSpendInfo_levelsItem>( context, "DelegatedSpendInfo.levelsItem", "");
				}
				SetDirty("Levels");
				return gxTv_SdtDelegatedSpendInfo_Levels;
			}
			set {
				gxTv_SdtDelegatedSpendInfo_Levels_N = false;
				gxTv_SdtDelegatedSpendInfo_Levels = value;
				SetDirty("Levels");
			}
		}

		public void gxTv_SdtDelegatedSpendInfo_Levels_SetNull()
		{
			gxTv_SdtDelegatedSpendInfo_Levels_N = true;
			gxTv_SdtDelegatedSpendInfo_Levels = null;
		}

		public bool gxTv_SdtDelegatedSpendInfo_Levels_IsNull()
		{
			return gxTv_SdtDelegatedSpendInfo_Levels == null;
		}
		public bool ShouldSerializegxTpr_Levels_GxSimpleCollection_Json()
		{
			return gxTv_SdtDelegatedSpendInfo_Levels != null && gxTv_SdtDelegatedSpendInfo_Levels.Count > 0;

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
			gxTv_SdtDelegatedSpendInfo_Error = "";





			gxTv_SdtDelegatedSpendInfo_Sendto = "";
			gxTv_SdtDelegatedSpendInfo_Changeto = "";



			gxTv_SdtDelegatedSpendInfo_Levels_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtDelegatedSpendInfo_Success;
		 

		protected string gxTv_SdtDelegatedSpendInfo_Error;
		 

		protected short gxTv_SdtDelegatedSpendInfo_Required;
		 

		protected short gxTv_SdtDelegatedSpendInfo_Signatures;
		 

		protected bool gxTv_SdtDelegatedSpendInfo_Complete;
		 

		protected bool gxTv_SdtDelegatedSpendInfo_Nextislast;
		 

		protected bool gxTv_SdtDelegatedSpendInfo_Isigned;
		 

		protected string gxTv_SdtDelegatedSpendInfo_Sendto;
		 

		protected string gxTv_SdtDelegatedSpendInfo_Changeto;
		 

		protected decimal gxTv_SdtDelegatedSpendInfo_Amountbtc;
		 

		protected decimal gxTv_SdtDelegatedSpendInfo_Totalinbtc;
		 
		protected bool gxTv_SdtDelegatedSpendInfo_Levels_N;
		protected GXBaseCollection<SdtDelegatedSpendInfo_levelsItem> gxTv_SdtDelegatedSpendInfo_Levels = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"DelegatedSpendInfo", Namespace="distributedcryptography")]
	public class SdtDelegatedSpendInfo_RESTInterface : GxGenericCollectionItem<SdtDelegatedSpendInfo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDelegatedSpendInfo_RESTInterface( ) : base()
		{	
		}

		public SdtDelegatedSpendInfo_RESTInterface( SdtDelegatedSpendInfo psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("success")]
		[JsonPropertyOrder(0)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="success", Order=0)]
		public bool gxTpr_Success
		{
			get { 
				return sdt.gxTpr_Success;

			}
			set { 
				sdt.gxTpr_Success = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[JsonPropertyName("required")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="required", Order=2)]
		public short gxTpr_Required
		{
			get { 
				return sdt.gxTpr_Required;

			}
			set { 
				sdt.gxTpr_Required = value;
			}
		}

		[JsonPropertyName("signatures")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="signatures", Order=3)]
		public short gxTpr_Signatures
		{
			get { 
				return sdt.gxTpr_Signatures;

			}
			set { 
				sdt.gxTpr_Signatures = value;
			}
		}

		[JsonPropertyName("complete")]
		[JsonPropertyOrder(4)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="complete", Order=4)]
		public bool gxTpr_Complete
		{
			get { 
				return sdt.gxTpr_Complete;

			}
			set { 
				sdt.gxTpr_Complete = value;
			}
		}

		[JsonPropertyName("nextIsLast")]
		[JsonPropertyOrder(5)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="nextIsLast", Order=5)]
		public bool gxTpr_Nextislast
		{
			get { 
				return sdt.gxTpr_Nextislast;

			}
			set { 
				sdt.gxTpr_Nextislast = value;
			}
		}

		[JsonPropertyName("iSigned")]
		[JsonPropertyOrder(6)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="iSigned", Order=6)]
		public bool gxTpr_Isigned
		{
			get { 
				return sdt.gxTpr_Isigned;

			}
			set { 
				sdt.gxTpr_Isigned = value;
			}
		}

		[JsonPropertyName("sendTo")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="sendTo", Order=7)]
		public  string gxTpr_Sendto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sendto);

			}
			set { 
				 sdt.gxTpr_Sendto = value;
			}
		}

		[JsonPropertyName("changeTo")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="changeTo", Order=8)]
		public  string gxTpr_Changeto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Changeto);

			}
			set { 
				 sdt.gxTpr_Changeto = value;
			}
		}

		[JsonPropertyName("amountBtc")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="amountBtc", Order=9)]
		public  string gxTpr_Amountbtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Amountbtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Amountbtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("totalInBtc")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="totalInBtc", Order=10)]
		public  string gxTpr_Totalinbtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Totalinbtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Totalinbtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("levels")]
		[JsonPropertyOrder(11)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="levels", Order=11, EmitDefaultValue=false)]
		public GxGenericCollection<SdtDelegatedSpendInfo_levelsItem_RESTInterface> gxTpr_Levels
		{
			get {
				if (sdt.ShouldSerializegxTpr_Levels_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtDelegatedSpendInfo_levelsItem_RESTInterface>(sdt.gxTpr_Levels);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Levels);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtDelegatedSpendInfo sdt
		{
			get { 
				return (SdtDelegatedSpendInfo)Sdt;
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
				sdt = new SdtDelegatedSpendInfo() ;
			}
		}
	}
	#endregion
}