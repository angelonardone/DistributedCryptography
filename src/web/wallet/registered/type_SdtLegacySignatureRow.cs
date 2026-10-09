/*
				   File: type_SdtLegacySignatureRow
			Description: LegacySignatureRow
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
	[XmlRoot(ElementName="LegacySignatureRow")]
	[XmlType(TypeName="LegacySignatureRow" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtLegacySignatureRow : GxUserType
	{
		public SdtLegacySignatureRow( )
		{
			/* Constructor for serialization */
			gxTv_SdtLegacySignatureRow_Description = "";

			gxTv_SdtLegacySignatureRow_Signeddatetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtLegacySignatureRow_Senderusername = "";

			gxTv_SdtLegacySignatureRow_Sendto = "";


		}

		public SdtLegacySignatureRow(IGxContext context)
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
			AddObjectProperty("id", gxTpr_Id, false);


			AddObjectProperty("description", gxTpr_Description, false);


			datetime_STZ = gxTpr_Signeddatetime;
			sDateCnv = "";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Year(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("0000", 1, 4-StringUtil.Len( sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim( StringUtil.Str((decimal)(DateTimeUtil.Month(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Day(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "T";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Hour(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Minute(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Second(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			AddObjectProperty("signedDateTime", sDateCnv, false);



			AddObjectProperty("senderUserName", gxTpr_Senderusername, false);


			AddObjectProperty("compleated", gxTpr_Compleated, false);


			AddObjectProperty("sendCoins", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Sendcoins, 16, 8)), false);


			AddObjectProperty("sendTo", gxTpr_Sendto, false);


			AddObjectProperty("canSign", gxTpr_Cansign, false);


			AddObjectProperty("rowIndex", gxTpr_Rowindex, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="id")]
		[XmlElement(ElementName="id")]
		public Guid gxTpr_Id
		{
			get {
				return gxTv_SdtLegacySignatureRow_Id; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="description")]
		[XmlElement(ElementName="description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtLegacySignatureRow_Description; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Description = value;
				SetDirty("Description");
			}
		}



		[SoapElement(ElementName="signedDateTime")]
		[XmlElement(ElementName="signedDateTime" , IsNullable=true)]
		public string gxTpr_Signeddatetime_Nullable
		{
			get {
				if ( gxTv_SdtLegacySignatureRow_Signeddatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtLegacySignatureRow_Signeddatetime).value ;
			}
			set {
				gxTv_SdtLegacySignatureRow_Signeddatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Signeddatetime
		{
			get {
				return gxTv_SdtLegacySignatureRow_Signeddatetime; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Signeddatetime = value;
				SetDirty("Signeddatetime");
			}
		}



		[SoapElement(ElementName="senderUserName")]
		[XmlElement(ElementName="senderUserName")]
		public string gxTpr_Senderusername
		{
			get {
				return gxTv_SdtLegacySignatureRow_Senderusername; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Senderusername = value;
				SetDirty("Senderusername");
			}
		}




		[SoapElement(ElementName="compleated")]
		[XmlElement(ElementName="compleated")]
		public bool gxTpr_Compleated
		{
			get {
				return gxTv_SdtLegacySignatureRow_Compleated; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Compleated = value;
				SetDirty("Compleated");
			}
		}



		[SoapElement(ElementName="sendCoins")]
		[XmlElement(ElementName="sendCoins")]
		public string gxTpr_Sendcoins_double
		{
			get {
				return Convert.ToString(gxTv_SdtLegacySignatureRow_Sendcoins, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtLegacySignatureRow_Sendcoins = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Sendcoins
		{
			get {
				return gxTv_SdtLegacySignatureRow_Sendcoins; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Sendcoins = value;
				SetDirty("Sendcoins");
			}
		}




		[SoapElement(ElementName="sendTo")]
		[XmlElement(ElementName="sendTo")]
		public string gxTpr_Sendto
		{
			get {
				return gxTv_SdtLegacySignatureRow_Sendto; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Sendto = value;
				SetDirty("Sendto");
			}
		}




		[SoapElement(ElementName="canSign")]
		[XmlElement(ElementName="canSign")]
		public bool gxTpr_Cansign
		{
			get {
				return gxTv_SdtLegacySignatureRow_Cansign; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Cansign = value;
				SetDirty("Cansign");
			}
		}




		[SoapElement(ElementName="rowIndex")]
		[XmlElement(ElementName="rowIndex")]
		public short gxTpr_Rowindex
		{
			get {
				return gxTv_SdtLegacySignatureRow_Rowindex; 
			}
			set {
				gxTv_SdtLegacySignatureRow_Rowindex = value;
				SetDirty("Rowindex");
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
			gxTv_SdtLegacySignatureRow_Description = "";
			gxTv_SdtLegacySignatureRow_Signeddatetime = (DateTime)(DateTime.MinValue);
			gxTv_SdtLegacySignatureRow_Senderusername = "";


			gxTv_SdtLegacySignatureRow_Sendto = "";


			datetime_STZ = (DateTime)(DateTime.MinValue);
			sDateCnv = "";
			sNumToPad = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string sDateCnv ;
		protected string sNumToPad ;
		protected DateTime datetime_STZ ;

		protected Guid gxTv_SdtLegacySignatureRow_Id;
		 

		protected string gxTv_SdtLegacySignatureRow_Description;
		 

		protected DateTime gxTv_SdtLegacySignatureRow_Signeddatetime;
		 

		protected string gxTv_SdtLegacySignatureRow_Senderusername;
		 

		protected bool gxTv_SdtLegacySignatureRow_Compleated;
		 

		protected decimal gxTv_SdtLegacySignatureRow_Sendcoins;
		 

		protected string gxTv_SdtLegacySignatureRow_Sendto;
		 

		protected bool gxTv_SdtLegacySignatureRow_Cansign;
		 

		protected short gxTv_SdtLegacySignatureRow_Rowindex;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"LegacySignatureRow", Namespace="distributedcryptography")]
	public class SdtLegacySignatureRow_RESTInterface : GxGenericCollectionItem<SdtLegacySignatureRow>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtLegacySignatureRow_RESTInterface( ) : base()
		{	
		}

		public SdtLegacySignatureRow_RESTInterface( SdtLegacySignatureRow psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("id")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="id", Order=0)]
		public Guid gxTpr_Id
		{
			get { 
				return sdt.gxTpr_Id;

			}
			set { 
				sdt.gxTpr_Id = value;
			}
		}

		[JsonPropertyName("description")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="description", Order=1)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[JsonPropertyName("signedDateTime")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="signedDateTime", Order=2)]
		public  string gxTpr_Signeddatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Signeddatetime,context);

			}
			set { 
				sdt.gxTpr_Signeddatetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("senderUserName")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="senderUserName", Order=3)]
		public  string gxTpr_Senderusername
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Senderusername);

			}
			set { 
				 sdt.gxTpr_Senderusername = value;
			}
		}

		[JsonPropertyName("compleated")]
		[JsonPropertyOrder(4)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="compleated", Order=4)]
		public bool gxTpr_Compleated
		{
			get { 
				return sdt.gxTpr_Compleated;

			}
			set { 
				sdt.gxTpr_Compleated = value;
			}
		}

		[JsonPropertyName("sendCoins")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="sendCoins", Order=5)]
		public  string gxTpr_Sendcoins
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Sendcoins, 16, 8));

			}
			set { 
				sdt.gxTpr_Sendcoins =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("sendTo")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="sendTo", Order=6)]
		public  string gxTpr_Sendto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sendto);

			}
			set { 
				 sdt.gxTpr_Sendto = value;
			}
		}

		[JsonPropertyName("canSign")]
		[JsonPropertyOrder(7)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="canSign", Order=7)]
		public bool gxTpr_Cansign
		{
			get { 
				return sdt.gxTpr_Cansign;

			}
			set { 
				sdt.gxTpr_Cansign = value;
			}
		}

		[JsonPropertyName("rowIndex")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="rowIndex", Order=8)]
		public short gxTpr_Rowindex
		{
			get { 
				return sdt.gxTpr_Rowindex;

			}
			set { 
				sdt.gxTpr_Rowindex = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtLegacySignatureRow sdt
		{
			get { 
				return (SdtLegacySignatureRow)Sdt;
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
				sdt = new SdtLegacySignatureRow() ;
			}
		}
	}
	#endregion
}