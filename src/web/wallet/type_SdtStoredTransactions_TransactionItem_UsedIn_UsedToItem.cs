/*
				   File: type_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem
			Description: UsedTo
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="StoredTransactions.TransactionItem.UsedIn.UsedToItem")]
	[XmlType(TypeName="StoredTransactions.TransactionItem.UsedIn.UsedToItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem : GxUserType
	{
		public SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Scriptpubkey_address = "";


		}

		public SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem(IGxContext context)
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
			AddObjectProperty("scriptPubKey_address", gxTpr_Scriptpubkey_address, false);


			AddObjectProperty("n", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_N, 18, 0)), false);


			AddObjectProperty("value", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Value, 16, 8)), false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="scriptPubKey_address")]
		[XmlElement(ElementName="scriptPubKey_address")]
		public string gxTpr_Scriptpubkey_address
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Scriptpubkey_address; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Scriptpubkey_address = value;
				SetDirty("Scriptpubkey_address");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public long gxTpr_N
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_N; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_N = value;
				SetDirty("N");
			}
		}



		[SoapElement(ElementName="value")]
		[XmlElement(ElementName="value")]
		public string gxTpr_Value_double
		{
			get {
				return Convert.ToString(gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Value, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Value = NumberUtil.Val(value);
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Value
		{
			get {
				return gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Value; 
			}
			set {
				gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Value = value;
				SetDirty("Value");
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
			gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Scriptpubkey_address = "";


			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Scriptpubkey_address;
		 

		protected long gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_N;
		 

		protected decimal gxTv_SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_Value;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"StoredTransactions.TransactionItem.UsedIn.UsedToItem", Namespace="distributedcryptography")]
	public class SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_RESTInterface : GxGenericCollectionItem<SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_RESTInterface( ) : base()
		{	
		}

		public SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem_RESTInterface( SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("scriptPubKey_address")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="scriptPubKey_address", Order=0)]
		public  string gxTpr_Scriptpubkey_address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Scriptpubkey_address);

			}
			set { 
				 sdt.gxTpr_Scriptpubkey_address = value;
			}
		}

		[JsonPropertyName("n")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="n", Order=1)]
		public  string gxTpr_N
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_N, 18, 0));

			}
			set { 
				sdt.gxTpr_N = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("value")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="value", Order=2)]
		public  string gxTpr_Value
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Value, 16, 8));

			}
			set { 
				sdt.gxTpr_Value =  NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem sdt
		{
			get { 
				return (SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem)Sdt;
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
				sdt = new SdtStoredTransactions_TransactionItem_UsedIn_UsedToItem() ;
			}
		}
	}
	#endregion
}