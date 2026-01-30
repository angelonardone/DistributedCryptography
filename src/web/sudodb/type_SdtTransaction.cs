/*
				   File: type_SdtTransaction
			Description: Transaction
				 Author: Nemo 🐠 for C# (.NET) version 18.0.14.187820
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

namespace GeneXus.Programs.sudodb
{
	[XmlRoot(ElementName="Transaction")]
	[XmlType(TypeName="Transaction" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtTransaction : GxUserType
	{
		public SdtTransaction( )
		{
			/* Constructor for serialization */
			gxTv_SdtTransaction_Transactionid = "";

			gxTv_SdtTransaction_Blockid = "";

			gxTv_SdtTransaction_Blockdatetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtTransaction_Hex = "";

		}

		public SdtTransaction(IGxContext context)
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
			AddObjectProperty("TransactionId", gxTpr_Transactionid, false);


			AddObjectProperty("BlockId", gxTpr_Blockid, false);


			AddObjectProperty("Confirmations", gxTpr_Confirmations, false);


			datetime_STZ = gxTpr_Blockdatetime;
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
			AddObjectProperty("BlockDateTime", sDateCnv, false);



			AddObjectProperty("Hex", gxTpr_Hex, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TransactionId")]
		[XmlElement(ElementName="TransactionId")]
		public string gxTpr_Transactionid
		{
			get {
				return gxTv_SdtTransaction_Transactionid; 
			}
			set {
				gxTv_SdtTransaction_Transactionid = value;
				SetDirty("Transactionid");
			}
		}




		[SoapElement(ElementName="BlockId")]
		[XmlElement(ElementName="BlockId")]
		public string gxTpr_Blockid
		{
			get {
				return gxTv_SdtTransaction_Blockid; 
			}
			set {
				gxTv_SdtTransaction_Blockid = value;
				SetDirty("Blockid");
			}
		}




		[SoapElement(ElementName="Confirmations")]
		[XmlElement(ElementName="Confirmations")]
		public long gxTpr_Confirmations
		{
			get {
				return gxTv_SdtTransaction_Confirmations; 
			}
			set {
				gxTv_SdtTransaction_Confirmations = value;
				SetDirty("Confirmations");
			}
		}



		[SoapElement(ElementName="BlockDateTime")]
		[XmlElement(ElementName="BlockDateTime" , IsNullable=true)]
		public string gxTpr_Blockdatetime_Nullable
		{
			get {
				if ( gxTv_SdtTransaction_Blockdatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtTransaction_Blockdatetime).value ;
			}
			set {
				gxTv_SdtTransaction_Blockdatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Blockdatetime
		{
			get {
				return gxTv_SdtTransaction_Blockdatetime; 
			}
			set {
				gxTv_SdtTransaction_Blockdatetime = value;
				SetDirty("Blockdatetime");
			}
		}



		[SoapElement(ElementName="Hex")]
		[XmlElement(ElementName="Hex")]
		public string gxTpr_Hex
		{
			get {
				return gxTv_SdtTransaction_Hex; 
			}
			set {
				gxTv_SdtTransaction_Hex = value;
				SetDirty("Hex");
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
			gxTv_SdtTransaction_Transactionid = "";
			gxTv_SdtTransaction_Blockid = "";

			gxTv_SdtTransaction_Blockdatetime = (DateTime)(DateTime.MinValue);
			gxTv_SdtTransaction_Hex = "";
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

		protected string gxTv_SdtTransaction_Transactionid;
		 

		protected string gxTv_SdtTransaction_Blockid;
		 

		protected long gxTv_SdtTransaction_Confirmations;
		 

		protected DateTime gxTv_SdtTransaction_Blockdatetime;
		 

		protected string gxTv_SdtTransaction_Hex;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Transaction", Namespace="distributedcryptography")]
	public class SdtTransaction_RESTInterface : GxGenericCollectionItem<SdtTransaction>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtTransaction_RESTInterface( ) : base()
		{	
		}

		public SdtTransaction_RESTInterface( SdtTransaction psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TransactionId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="TransactionId", Order=0)]
		public  string gxTpr_Transactionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Transactionid);

			}
			set { 
				 sdt.gxTpr_Transactionid = value;
			}
		}

		[JsonPropertyName("BlockId")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="BlockId", Order=1)]
		public  string gxTpr_Blockid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Blockid);

			}
			set { 
				 sdt.gxTpr_Blockid = value;
			}
		}

		[JsonPropertyName("Confirmations")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="Confirmations", Order=2)]
		public  string gxTpr_Confirmations
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Confirmations, 10, 0));

			}
			set { 
				sdt.gxTpr_Confirmations = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("BlockDateTime")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="BlockDateTime", Order=3)]
		public  string gxTpr_Blockdatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Blockdatetime,context);

			}
			set { 
				sdt.gxTpr_Blockdatetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("Hex")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Hex", Order=4)]
		public  string gxTpr_Hex
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Hex);

			}
			set { 
				 sdt.gxTpr_Hex = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtTransaction sdt
		{
			get { 
				return (SdtTransaction)Sdt;
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
				sdt = new SdtTransaction() ;
			}
		}
	}
	#endregion
}