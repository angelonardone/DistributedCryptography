/*
				   File: type_SdtWalletAddressItem
			Description: WalletAddressItem
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="WalletAddressItem")]
	[XmlType(TypeName="WalletAddressItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtWalletAddressItem : GxUserType
	{
		public SdtWalletAddressItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtWalletAddressItem_Chain = "";

			gxTv_SdtWalletAddressItem_Path = "";

			gxTv_SdtWalletAddressItem_Address = "";

			gxTv_SdtWalletAddressItem_Status = "";

			gxTv_SdtWalletAddressItem_Label = "";

		}

		public SdtWalletAddressItem(IGxContext context)
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
			AddObjectProperty("chainIndex", gxTpr_Chainindex, false);


			AddObjectProperty("chain", gxTpr_Chain, false);


			AddObjectProperty("addressIndex", gxTpr_Addressindex, false);


			AddObjectProperty("path", gxTpr_Path, false);


			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("status", gxTpr_Status, false);


			AddObjectProperty("balance", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Balance, 16, 8)), false);


			AddObjectProperty("received", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Received, 16, 8)), false);


			AddObjectProperty("transactions", gxTpr_Transactions, false);


			AddObjectProperty("label", gxTpr_Label, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="chainIndex")]
		[XmlElement(ElementName="chainIndex")]
		public short gxTpr_Chainindex
		{
			get {
				return gxTv_SdtWalletAddressItem_Chainindex; 
			}
			set {
				gxTv_SdtWalletAddressItem_Chainindex = value;
				SetDirty("Chainindex");
			}
		}




		[SoapElement(ElementName="chain")]
		[XmlElement(ElementName="chain")]
		public string gxTpr_Chain
		{
			get {
				return gxTv_SdtWalletAddressItem_Chain; 
			}
			set {
				gxTv_SdtWalletAddressItem_Chain = value;
				SetDirty("Chain");
			}
		}




		[SoapElement(ElementName="addressIndex")]
		[XmlElement(ElementName="addressIndex")]
		public long gxTpr_Addressindex
		{
			get {
				return gxTv_SdtWalletAddressItem_Addressindex; 
			}
			set {
				gxTv_SdtWalletAddressItem_Addressindex = value;
				SetDirty("Addressindex");
			}
		}




		[SoapElement(ElementName="path")]
		[XmlElement(ElementName="path")]
		public string gxTpr_Path
		{
			get {
				return gxTv_SdtWalletAddressItem_Path; 
			}
			set {
				gxTv_SdtWalletAddressItem_Path = value;
				SetDirty("Path");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtWalletAddressItem_Address; 
			}
			set {
				gxTv_SdtWalletAddressItem_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="status")]
		[XmlElement(ElementName="status")]
		public string gxTpr_Status
		{
			get {
				return gxTv_SdtWalletAddressItem_Status; 
			}
			set {
				gxTv_SdtWalletAddressItem_Status = value;
				SetDirty("Status");
			}
		}



		[SoapElement(ElementName="balance")]
		[XmlElement(ElementName="balance")]
		public string gxTpr_Balance_double
		{
			get {
				return Convert.ToString(gxTv_SdtWalletAddressItem_Balance, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtWalletAddressItem_Balance = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Balance
		{
			get {
				return gxTv_SdtWalletAddressItem_Balance; 
			}
			set {
				gxTv_SdtWalletAddressItem_Balance = value;
				SetDirty("Balance");
			}
		}



		[SoapElement(ElementName="received")]
		[XmlElement(ElementName="received")]
		public string gxTpr_Received_double
		{
			get {
				return Convert.ToString(gxTv_SdtWalletAddressItem_Received, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtWalletAddressItem_Received = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Received
		{
			get {
				return gxTv_SdtWalletAddressItem_Received; 
			}
			set {
				gxTv_SdtWalletAddressItem_Received = value;
				SetDirty("Received");
			}
		}




		[SoapElement(ElementName="transactions")]
		[XmlElement(ElementName="transactions")]
		public long gxTpr_Transactions
		{
			get {
				return gxTv_SdtWalletAddressItem_Transactions; 
			}
			set {
				gxTv_SdtWalletAddressItem_Transactions = value;
				SetDirty("Transactions");
			}
		}




		[SoapElement(ElementName="label")]
		[XmlElement(ElementName="label")]
		public string gxTpr_Label
		{
			get {
				return gxTv_SdtWalletAddressItem_Label; 
			}
			set {
				gxTv_SdtWalletAddressItem_Label = value;
				SetDirty("Label");
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
			gxTv_SdtWalletAddressItem_Chain = "";

			gxTv_SdtWalletAddressItem_Path = "";
			gxTv_SdtWalletAddressItem_Address = "";
			gxTv_SdtWalletAddressItem_Status = "";



			gxTv_SdtWalletAddressItem_Label = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected short gxTv_SdtWalletAddressItem_Chainindex;
		 

		protected string gxTv_SdtWalletAddressItem_Chain;
		 

		protected long gxTv_SdtWalletAddressItem_Addressindex;
		 

		protected string gxTv_SdtWalletAddressItem_Path;
		 

		protected string gxTv_SdtWalletAddressItem_Address;
		 

		protected string gxTv_SdtWalletAddressItem_Status;
		 

		protected decimal gxTv_SdtWalletAddressItem_Balance;
		 

		protected decimal gxTv_SdtWalletAddressItem_Received;
		 

		protected long gxTv_SdtWalletAddressItem_Transactions;
		 

		protected string gxTv_SdtWalletAddressItem_Label;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"WalletAddressItem", Namespace="distributedcryptography")]
	public class SdtWalletAddressItem_RESTInterface : GxGenericCollectionItem<SdtWalletAddressItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWalletAddressItem_RESTInterface( ) : base()
		{	
		}

		public SdtWalletAddressItem_RESTInterface( SdtWalletAddressItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("chainIndex")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="chainIndex", Order=0)]
		public short gxTpr_Chainindex
		{
			get { 
				return sdt.gxTpr_Chainindex;

			}
			set { 
				sdt.gxTpr_Chainindex = value;
			}
		}

		[JsonPropertyName("chain")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="chain", Order=1)]
		public  string gxTpr_Chain
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Chain);

			}
			set { 
				 sdt.gxTpr_Chain = value;
			}
		}

		[JsonPropertyName("addressIndex")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="addressIndex", Order=2)]
		public  string gxTpr_Addressindex
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Addressindex, 10, 0));

			}
			set { 
				sdt.gxTpr_Addressindex = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("path")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="path", Order=3)]
		public  string gxTpr_Path
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Path);

			}
			set { 
				 sdt.gxTpr_Path = value;
			}
		}

		[JsonPropertyName("address")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="address", Order=4)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[JsonPropertyName("status")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="status", Order=5)]
		public  string gxTpr_Status
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Status);

			}
			set { 
				 sdt.gxTpr_Status = value;
			}
		}

		[JsonPropertyName("balance")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="balance", Order=6)]
		public  string gxTpr_Balance
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Balance, 16, 8));

			}
			set { 
				sdt.gxTpr_Balance =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("received")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="received", Order=7)]
		public  string gxTpr_Received
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Received, 16, 8));

			}
			set { 
				sdt.gxTpr_Received =  NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("transactions")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="transactions", Order=8)]
		public  string gxTpr_Transactions
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Transactions, 10, 0));

			}
			set { 
				sdt.gxTpr_Transactions = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("label")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="label", Order=9)]
		public  string gxTpr_Label
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Label);

			}
			set { 
				 sdt.gxTpr_Label = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtWalletAddressItem sdt
		{
			get { 
				return (SdtWalletAddressItem)Sdt;
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
				sdt = new SdtWalletAddressItem() ;
			}
		}
	}
	#endregion
}