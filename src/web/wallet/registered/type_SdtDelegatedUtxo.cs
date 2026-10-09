/*
				   File: type_SdtDelegatedUtxo
			Description: DelegatedUtxo
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
	[XmlRoot(ElementName="DelegatedUtxo")]
	[XmlType(TypeName="DelegatedUtxo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDelegatedUtxo : GxUserType
	{
		public SdtDelegatedUtxo( )
		{
			/* Constructor for serialization */
			gxTv_SdtDelegatedUtxo_Txid = "";

			gxTv_SdtDelegatedUtxo_Amountbtc = "";

			gxTv_SdtDelegatedUtxo_Address = "";

		}

		public SdtDelegatedUtxo(IGxContext context)
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
			AddObjectProperty("TxId", gxTpr_Txid, false);


			AddObjectProperty("Vout", gxTpr_Vout, false);


			AddObjectProperty("AmountBtc", gxTpr_Amountbtc, false);


			AddObjectProperty("Sequence", gxTpr_Sequence, false);


			AddObjectProperty("IsChange", gxTpr_Ischange, false);


			AddObjectProperty("Address", gxTpr_Address, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TxId")]
		[XmlElement(ElementName="TxId")]
		public string gxTpr_Txid
		{
			get {
				return gxTv_SdtDelegatedUtxo_Txid; 
			}
			set {
				gxTv_SdtDelegatedUtxo_Txid = value;
				SetDirty("Txid");
			}
		}




		[SoapElement(ElementName="Vout")]
		[XmlElement(ElementName="Vout")]
		public long gxTpr_Vout
		{
			get {
				return gxTv_SdtDelegatedUtxo_Vout; 
			}
			set {
				gxTv_SdtDelegatedUtxo_Vout = value;
				SetDirty("Vout");
			}
		}




		[SoapElement(ElementName="AmountBtc")]
		[XmlElement(ElementName="AmountBtc")]
		public string gxTpr_Amountbtc
		{
			get {
				return gxTv_SdtDelegatedUtxo_Amountbtc; 
			}
			set {
				gxTv_SdtDelegatedUtxo_Amountbtc = value;
				SetDirty("Amountbtc");
			}
		}




		[SoapElement(ElementName="Sequence")]
		[XmlElement(ElementName="Sequence")]
		public long gxTpr_Sequence
		{
			get {
				return gxTv_SdtDelegatedUtxo_Sequence; 
			}
			set {
				gxTv_SdtDelegatedUtxo_Sequence = value;
				SetDirty("Sequence");
			}
		}




		[SoapElement(ElementName="IsChange")]
		[XmlElement(ElementName="IsChange")]
		public bool gxTpr_Ischange
		{
			get {
				return gxTv_SdtDelegatedUtxo_Ischange; 
			}
			set {
				gxTv_SdtDelegatedUtxo_Ischange = value;
				SetDirty("Ischange");
			}
		}




		[SoapElement(ElementName="Address")]
		[XmlElement(ElementName="Address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtDelegatedUtxo_Address; 
			}
			set {
				gxTv_SdtDelegatedUtxo_Address = value;
				SetDirty("Address");
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
			gxTv_SdtDelegatedUtxo_Txid = "";

			gxTv_SdtDelegatedUtxo_Amountbtc = "";


			gxTv_SdtDelegatedUtxo_Address = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtDelegatedUtxo_Txid;
		 

		protected long gxTv_SdtDelegatedUtxo_Vout;
		 

		protected string gxTv_SdtDelegatedUtxo_Amountbtc;
		 

		protected long gxTv_SdtDelegatedUtxo_Sequence;
		 

		protected bool gxTv_SdtDelegatedUtxo_Ischange;
		 

		protected string gxTv_SdtDelegatedUtxo_Address;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"DelegatedUtxo", Namespace="distributedcryptography")]
	public class SdtDelegatedUtxo_RESTInterface : GxGenericCollectionItem<SdtDelegatedUtxo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDelegatedUtxo_RESTInterface( ) : base()
		{	
		}

		public SdtDelegatedUtxo_RESTInterface( SdtDelegatedUtxo psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("TxId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="TxId", Order=0)]
		public  string gxTpr_Txid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Txid);

			}
			set { 
				 sdt.gxTpr_Txid = value;
			}
		}

		[JsonPropertyName("Vout")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Vout", Order=1)]
		public  string gxTpr_Vout
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Vout, 10, 0));

			}
			set { 
				sdt.gxTpr_Vout = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("AmountBtc")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="AmountBtc", Order=2)]
		public  string gxTpr_Amountbtc
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Amountbtc);

			}
			set { 
				 sdt.gxTpr_Amountbtc = value;
			}
		}

		[JsonPropertyName("Sequence")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="Sequence", Order=3)]
		public  string gxTpr_Sequence
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Sequence, 10, 0));

			}
			set { 
				sdt.gxTpr_Sequence = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("IsChange")]
		[JsonPropertyOrder(4)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="IsChange", Order=4)]
		public bool gxTpr_Ischange
		{
			get { 
				return sdt.gxTpr_Ischange;

			}
			set { 
				sdt.gxTpr_Ischange = value;
			}
		}

		[JsonPropertyName("Address")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="Address", Order=5)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtDelegatedUtxo sdt
		{
			get { 
				return (SdtDelegatedUtxo)Sdt;
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
				sdt = new SdtDelegatedUtxo() ;
			}
		}
	}
	#endregion
}