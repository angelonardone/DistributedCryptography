/*
				   File: type_SdtDelegatedSpendInfo_levelsItem
			Description: levels
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
	[XmlRoot(ElementName="DelegatedSpendInfo.levelsItem")]
	[XmlType(TypeName="DelegatedSpendInfo.levelsItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDelegatedSpendInfo_levelsItem : GxUserType
	{
		public SdtDelegatedSpendInfo_levelsItem( )
		{
			/* Constructor for serialization */
		}

		public SdtDelegatedSpendInfo_levelsItem(IGxContext context)
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
			AddObjectProperty("percent", gxTpr_Percent, false);


			AddObjectProperty("feeBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Feebtc, 16, 8)), false);


			AddObjectProperty("amountBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Amountbtc, 16, 8)), false);


			AddObjectProperty("changeBtc", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Changebtc, 16, 8)), false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="percent")]
		[XmlElement(ElementName="percent")]
		public short gxTpr_Percent
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_levelsItem_Percent; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_levelsItem_Percent = value;
				SetDirty("Percent");
			}
		}



		[SoapElement(ElementName="feeBtc")]
		[XmlElement(ElementName="feeBtc")]
		public string gxTpr_Feebtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtDelegatedSpendInfo_levelsItem_Feebtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtDelegatedSpendInfo_levelsItem_Feebtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Feebtc
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_levelsItem_Feebtc; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_levelsItem_Feebtc = value;
				SetDirty("Feebtc");
			}
		}



		[SoapElement(ElementName="amountBtc")]
		[XmlElement(ElementName="amountBtc")]
		public string gxTpr_Amountbtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtDelegatedSpendInfo_levelsItem_Amountbtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtDelegatedSpendInfo_levelsItem_Amountbtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Amountbtc
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_levelsItem_Amountbtc; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_levelsItem_Amountbtc = value;
				SetDirty("Amountbtc");
			}
		}



		[SoapElement(ElementName="changeBtc")]
		[XmlElement(ElementName="changeBtc")]
		public string gxTpr_Changebtc_double
		{
			get {
				return Convert.ToString(gxTv_SdtDelegatedSpendInfo_levelsItem_Changebtc, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtDelegatedSpendInfo_levelsItem_Changebtc = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Changebtc
		{
			get {
				return gxTv_SdtDelegatedSpendInfo_levelsItem_Changebtc; 
			}
			set {
				gxTv_SdtDelegatedSpendInfo_levelsItem_Changebtc = value;
				SetDirty("Changebtc");
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
			return  ;
		}



		#endregion

		#region Declaration

		protected short gxTv_SdtDelegatedSpendInfo_levelsItem_Percent;
		 

		protected decimal gxTv_SdtDelegatedSpendInfo_levelsItem_Feebtc;
		 

		protected decimal gxTv_SdtDelegatedSpendInfo_levelsItem_Amountbtc;
		 

		protected decimal gxTv_SdtDelegatedSpendInfo_levelsItem_Changebtc;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"DelegatedSpendInfo.levelsItem", Namespace="distributedcryptography")]
	public class SdtDelegatedSpendInfo_levelsItem_RESTInterface : GxGenericCollectionItem<SdtDelegatedSpendInfo_levelsItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDelegatedSpendInfo_levelsItem_RESTInterface( ) : base()
		{	
		}

		public SdtDelegatedSpendInfo_levelsItem_RESTInterface( SdtDelegatedSpendInfo_levelsItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("percent")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="percent", Order=0)]
		public short gxTpr_Percent
		{
			get { 
				return sdt.gxTpr_Percent;

			}
			set { 
				sdt.gxTpr_Percent = value;
			}
		}

		[JsonPropertyName("feeBtc")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="feeBtc", Order=1)]
		public  string gxTpr_Feebtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Feebtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Feebtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("amountBtc")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="amountBtc", Order=2)]
		public  string gxTpr_Amountbtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Amountbtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Amountbtc =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("changeBtc")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="changeBtc", Order=3)]
		public  string gxTpr_Changebtc
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Changebtc, 16, 8));

			}
			set { 
				sdt.gxTpr_Changebtc =  NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtDelegatedSpendInfo_levelsItem sdt
		{
			get { 
				return (SdtDelegatedSpendInfo_levelsItem)Sdt;
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
				sdt = new SdtDelegatedSpendInfo_levelsItem() ;
			}
		}
	}
	#endregion
}