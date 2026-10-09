/*
				   File: type_SdtLegacyUtxo
			Description: LegacyUtxo
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
	[XmlRoot(ElementName="LegacyUtxo")]
	[XmlType(TypeName="LegacyUtxo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtLegacyUtxo : GxUserType
	{
		public SdtLegacyUtxo( )
		{
			/* Constructor for serialization */
			gxTv_SdtLegacyUtxo_Txid = "";

			gxTv_SdtLegacyUtxo_Amountbtc = "";

		}

		public SdtLegacyUtxo(IGxContext context)
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

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TxId")]
		[XmlElement(ElementName="TxId")]
		public string gxTpr_Txid
		{
			get {
				return gxTv_SdtLegacyUtxo_Txid; 
			}
			set {
				gxTv_SdtLegacyUtxo_Txid = value;
				SetDirty("Txid");
			}
		}




		[SoapElement(ElementName="Vout")]
		[XmlElement(ElementName="Vout")]
		public int gxTpr_Vout
		{
			get {
				return gxTv_SdtLegacyUtxo_Vout; 
			}
			set {
				gxTv_SdtLegacyUtxo_Vout = value;
				SetDirty("Vout");
			}
		}




		[SoapElement(ElementName="AmountBtc")]
		[XmlElement(ElementName="AmountBtc")]
		public string gxTpr_Amountbtc
		{
			get {
				return gxTv_SdtLegacyUtxo_Amountbtc; 
			}
			set {
				gxTv_SdtLegacyUtxo_Amountbtc = value;
				SetDirty("Amountbtc");
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
			gxTv_SdtLegacyUtxo_Txid = "";

			gxTv_SdtLegacyUtxo_Amountbtc = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtLegacyUtxo_Txid;
		 

		protected int gxTv_SdtLegacyUtxo_Vout;
		 

		protected string gxTv_SdtLegacyUtxo_Amountbtc;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"LegacyUtxo", Namespace="distributedcryptography")]
	public class SdtLegacyUtxo_RESTInterface : GxGenericCollectionItem<SdtLegacyUtxo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtLegacyUtxo_RESTInterface( ) : base()
		{	
		}

		public SdtLegacyUtxo_RESTInterface( SdtLegacyUtxo psdt ) : base(psdt)
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
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Vout, 8, 0));

			}
			set { 
				sdt.gxTpr_Vout = (int) NumberUtil.Val( value, ".");
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


		#endregion
		[JsonIgnore]
		public SdtLegacyUtxo sdt
		{
			get { 
				return (SdtLegacyUtxo)Sdt;
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
				sdt = new SdtLegacyUtxo() ;
			}
		}
	}
	#endregion
}