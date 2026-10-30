/*
				   File: type_SdtMuSig2SessionData_InputsItem
			Description: Inputs
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
	[XmlRoot(ElementName="MuSig2SessionData.InputsItem")]
	[XmlType(TypeName="MuSig2SessionData.InputsItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2SessionData_InputsItem : GxUserType
	{
		public SdtMuSig2SessionData_InputsItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2SessionData_InputsItem_Txid = "";

			gxTv_SdtMuSig2SessionData_InputsItem_Amountbtc = "";


		}

		public SdtMuSig2SessionData_InputsItem(IGxContext context)
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

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TxId")]
		[XmlElement(ElementName="TxId")]
		public string gxTpr_Txid
		{
			get {
				return gxTv_SdtMuSig2SessionData_InputsItem_Txid; 
			}
			set {
				gxTv_SdtMuSig2SessionData_InputsItem_Txid = value;
				SetDirty("Txid");
			}
		}




		[SoapElement(ElementName="Vout")]
		[XmlElement(ElementName="Vout")]
		public long gxTpr_Vout
		{
			get {
				return gxTv_SdtMuSig2SessionData_InputsItem_Vout; 
			}
			set {
				gxTv_SdtMuSig2SessionData_InputsItem_Vout = value;
				SetDirty("Vout");
			}
		}




		[SoapElement(ElementName="AmountBtc")]
		[XmlElement(ElementName="AmountBtc")]
		public string gxTpr_Amountbtc
		{
			get {
				return gxTv_SdtMuSig2SessionData_InputsItem_Amountbtc; 
			}
			set {
				gxTv_SdtMuSig2SessionData_InputsItem_Amountbtc = value;
				SetDirty("Amountbtc");
			}
		}




		[SoapElement(ElementName="Sequence")]
		[XmlElement(ElementName="Sequence")]
		public long gxTpr_Sequence
		{
			get {
				return gxTv_SdtMuSig2SessionData_InputsItem_Sequence; 
			}
			set {
				gxTv_SdtMuSig2SessionData_InputsItem_Sequence = value;
				SetDirty("Sequence");
			}
		}




		[SoapElement(ElementName="IsChange")]
		[XmlElement(ElementName="IsChange")]
		public bool gxTpr_Ischange
		{
			get {
				return gxTv_SdtMuSig2SessionData_InputsItem_Ischange; 
			}
			set {
				gxTv_SdtMuSig2SessionData_InputsItem_Ischange = value;
				SetDirty("Ischange");
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
			gxTv_SdtMuSig2SessionData_InputsItem_Txid = "";

			gxTv_SdtMuSig2SessionData_InputsItem_Amountbtc = "";


			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtMuSig2SessionData_InputsItem_Txid;
		 

		protected long gxTv_SdtMuSig2SessionData_InputsItem_Vout;
		 

		protected string gxTv_SdtMuSig2SessionData_InputsItem_Amountbtc;
		 

		protected long gxTv_SdtMuSig2SessionData_InputsItem_Sequence;
		 

		protected bool gxTv_SdtMuSig2SessionData_InputsItem_Ischange;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"MuSig2SessionData.InputsItem", Namespace="distributedcryptography")]
	public class SdtMuSig2SessionData_InputsItem_RESTInterface : GxGenericCollectionItem<SdtMuSig2SessionData_InputsItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2SessionData_InputsItem_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2SessionData_InputsItem_RESTInterface( SdtMuSig2SessionData_InputsItem psdt ) : base(psdt)
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


		#endregion
		[JsonIgnore]
		public SdtMuSig2SessionData_InputsItem sdt
		{
			get { 
				return (SdtMuSig2SessionData_InputsItem)Sdt;
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
				sdt = new SdtMuSig2SessionData_InputsItem() ;
			}
		}
	}
	#endregion
}