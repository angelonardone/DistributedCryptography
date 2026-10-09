/*
				   File: type_SdtDelegatedSpendResult
			Description: DelegatedSpendResult
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
	[XmlRoot(ElementName="DelegatedSpendResult")]
	[XmlType(TypeName="DelegatedSpendResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDelegatedSpendResult : GxUserType
	{
		public SdtDelegatedSpendResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtDelegatedSpendResult_Error = "";

			gxTv_SdtDelegatedSpendResult_Bundle = "";

			gxTv_SdtDelegatedSpendResult_Txhex = "";

			gxTv_SdtDelegatedSpendResult_Txid = "";


		}

		public SdtDelegatedSpendResult(IGxContext context)
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


			AddObjectProperty("bundle", gxTpr_Bundle, false);


			AddObjectProperty("signatures", gxTpr_Signatures, false);


			AddObjectProperty("required", gxTpr_Required, false);


			AddObjectProperty("complete", gxTpr_Complete, false);


			AddObjectProperty("txHex", gxTpr_Txhex, false);


			AddObjectProperty("txId", gxTpr_Txid, false);


			AddObjectProperty("feeBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Feebtc, 16, 8)), false);


			AddObjectProperty("vsize", gxTpr_Vsize, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Success; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Error; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="bundle")]
		[XmlElement(ElementName="bundle")]
		public string gxTpr_Bundle
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Bundle; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Bundle = value;
				SetDirty("Bundle");
			}
		}




		[SoapElement(ElementName="signatures")]
		[XmlElement(ElementName="signatures")]
		public short gxTpr_Signatures
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Signatures; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Signatures = value;
				SetDirty("Signatures");
			}
		}




		[SoapElement(ElementName="required")]
		[XmlElement(ElementName="required")]
		public short gxTpr_Required
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Required; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Required = value;
				SetDirty("Required");
			}
		}




		[SoapElement(ElementName="complete")]
		[XmlElement(ElementName="complete")]
		public bool gxTpr_Complete
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Complete; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Complete = value;
				SetDirty("Complete");
			}
		}




		[SoapElement(ElementName="txHex")]
		[XmlElement(ElementName="txHex")]
		public string gxTpr_Txhex
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Txhex; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Txhex = value;
				SetDirty("Txhex");
			}
		}




		[SoapElement(ElementName="txId")]
		[XmlElement(ElementName="txId")]
		public string gxTpr_Txid
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Txid; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Txid = value;
				SetDirty("Txid");
			}
		}



		[SoapElement(ElementName="feeBtc")]
		[XmlElement(ElementName="feeBtc")]
		public string gxTpr_Feebtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtDelegatedSpendResult_Feebtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtDelegatedSpendResult_Feebtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Feebtc
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Feebtc; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Feebtc = value;
				SetDirty("Feebtc");
			}
		}




		[SoapElement(ElementName="vsize")]
		[XmlElement(ElementName="vsize")]
		public long gxTpr_Vsize
		{
			get {
				return gxTv_SdtDelegatedSpendResult_Vsize; 
			}
			set {
				gxTv_SdtDelegatedSpendResult_Vsize = value;
				SetDirty("Vsize");
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
			gxTv_SdtDelegatedSpendResult_Error = "";
			gxTv_SdtDelegatedSpendResult_Bundle = "";



			gxTv_SdtDelegatedSpendResult_Txhex = "";
			gxTv_SdtDelegatedSpendResult_Txid = "";


			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtDelegatedSpendResult_Success;
		 

		protected string gxTv_SdtDelegatedSpendResult_Error;
		 

		protected string gxTv_SdtDelegatedSpendResult_Bundle;
		 

		protected short gxTv_SdtDelegatedSpendResult_Signatures;
		 

		protected short gxTv_SdtDelegatedSpendResult_Required;
		 

		protected bool gxTv_SdtDelegatedSpendResult_Complete;
		 

		protected string gxTv_SdtDelegatedSpendResult_Txhex;
		 

		protected string gxTv_SdtDelegatedSpendResult_Txid;
		 

		protected decimal gxTv_SdtDelegatedSpendResult_Feebtc;
		 

		protected long gxTv_SdtDelegatedSpendResult_Vsize;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"DelegatedSpendResult", Namespace="distributedcryptography")]
	public class SdtDelegatedSpendResult_RESTInterface : GxGenericCollectionItem<SdtDelegatedSpendResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDelegatedSpendResult_RESTInterface( ) : base()
		{	
		}

		public SdtDelegatedSpendResult_RESTInterface( SdtDelegatedSpendResult psdt ) : base(psdt)
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

		[JsonPropertyName("bundle")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="bundle", Order=2)]
		public  string gxTpr_Bundle
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Bundle);

			}
			set { 
				 sdt.gxTpr_Bundle = value;
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

		[JsonPropertyName("required")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="required", Order=4)]
		public short gxTpr_Required
		{
			get { 
				return sdt.gxTpr_Required;

			}
			set { 
				sdt.gxTpr_Required = value;
			}
		}

		[JsonPropertyName("complete")]
		[JsonPropertyOrder(5)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="complete", Order=5)]
		public bool gxTpr_Complete
		{
			get { 
				return sdt.gxTpr_Complete;

			}
			set { 
				sdt.gxTpr_Complete = value;
			}
		}

		[JsonPropertyName("txHex")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="txHex", Order=6)]
		public  string gxTpr_Txhex
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Txhex);

			}
			set { 
				 sdt.gxTpr_Txhex = value;
			}
		}

		[JsonPropertyName("txId")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="txId", Order=7)]
		public  string gxTpr_Txid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Txid);

			}
			set { 
				 sdt.gxTpr_Txid = value;
			}
		}

		[JsonPropertyName("feeBtc")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="feeBtc", Order=8)]
		public  string gxTpr_Feebtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Feebtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Feebtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("vsize")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="vsize", Order=9)]
		public  string gxTpr_Vsize
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Vsize, 10, 0));

			}
			set { 
				sdt.gxTpr_Vsize = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtDelegatedSpendResult sdt
		{
			get { 
				return (SdtDelegatedSpendResult)Sdt;
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
				sdt = new SdtDelegatedSpendResult() ;
			}
		}
	}
	#endregion
}