/*
				   File: type_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem
			Description: addressSubTotal
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
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

using GeneXus.Programs;
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="rpc_ScanTxOutSet_response.addressSubTotalItem")]
	[XmlType(TypeName="rpc_ScanTxOutSet_response.addressSubTotalItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdtrpc_ScanTxOutSet_response_addressSubTotalItem : GxUserType
	{
		public Sdtrpc_ScanTxOutSet_response_addressSubTotalItem( )
		{
			/* Constructor for serialization */
			gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Address = "";

		}

		public Sdtrpc_ScanTxOutSet_response_addressSubTotalItem(IGxContext context)
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
			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("amount", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Amount, 16, 8)), false);


			AddObjectProperty("blockHigh", gxTpr_Blockhigh, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Address; 
			}
			set {
				gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Address = value;
				SetDirty("Address");
			}
		}



		[SoapElement(ElementName="amount")]
		[XmlElement(ElementName="amount")]
		public string gxTpr_Amount_double
		{
			get {
				return Convert.ToString(gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Amount, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Amount = (decimal)(Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Amount
		{
			get {
				return gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Amount; 
			}
			set {
				gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Amount = value;
				SetDirty("Amount");
			}
		}




		[SoapElement(ElementName="blockHigh")]
		[XmlElement(ElementName="blockHigh")]
		public long gxTpr_Blockhigh
		{
			get {
				return gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Blockhigh; 
			}
			set {
				gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Blockhigh = value;
				SetDirty("Blockhigh");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Address = "";


			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Address;
		 

		protected decimal gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Amount;
		 

		protected long gxTv_Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_Blockhigh;
		 


		#endregion
	}
	#region Rest interface
	[DataContract(Name=@"rpc_ScanTxOutSet_response.addressSubTotalItem", Namespace="distributedcryptography")]
	public class Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_RESTInterface : GxGenericCollectionItem<Sdtrpc_ScanTxOutSet_response_addressSubTotalItem>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_RESTInterface( ) : base()
		{	
		}

		public Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_RESTInterface( Sdtrpc_ScanTxOutSet_response_addressSubTotalItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="address", Order=0)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[DataMember(Name="amount", Order=1)]
		public  string gxTpr_Amount
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Amount, 16, 8));

			}
			set { 
				sdt.gxTpr_Amount =  NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="blockHigh", Order=2)]
		public  string gxTpr_Blockhigh
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Blockhigh, 10, 0));

			}
			set { 
				sdt.gxTpr_Blockhigh = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion

		public Sdtrpc_ScanTxOutSet_response_addressSubTotalItem sdt
		{
			get { 
				return (Sdtrpc_ScanTxOutSet_response_addressSubTotalItem)Sdt;
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
				sdt = new Sdtrpc_ScanTxOutSet_response_addressSubTotalItem() ;
			}
		}
	}
	#endregion
}