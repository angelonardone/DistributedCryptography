/*
				   File: type_SdtStoredTransactions
			Description: StoredTransactions
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
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="StoredTransactions")]
	[XmlType(TypeName="StoredTransactions" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtStoredTransactions : GxUserType
	{
		public SdtStoredTransactions( )
		{
			/* Constructor for serialization */
		}

		public SdtStoredTransactions(IGxContext context)
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
			if (gxTv_SdtStoredTransactions_Transaction != null)
			{
				AddObjectProperty("Transaction", gxTv_SdtStoredTransactions_Transaction, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Transaction" )]
		[XmlArray(ElementName="Transaction"  )]
		[XmlArrayItemAttribute(ElementName="TransactionItem" , IsNullable=false )]
		public GXBaseCollection<SdtStoredTransactions_TransactionItem> gxTpr_Transaction
		{
			get {
				if ( gxTv_SdtStoredTransactions_Transaction == null )
				{
					gxTv_SdtStoredTransactions_Transaction = new GXBaseCollection<SdtStoredTransactions_TransactionItem>( context, "StoredTransactions.TransactionItem", "");
				}
				return gxTv_SdtStoredTransactions_Transaction;
			}
			set {
				gxTv_SdtStoredTransactions_Transaction_N = false;
				gxTv_SdtStoredTransactions_Transaction = value;
				SetDirty("Transaction");
			}
		}

		public void gxTv_SdtStoredTransactions_Transaction_SetNull()
		{
			gxTv_SdtStoredTransactions_Transaction_N = true;
			gxTv_SdtStoredTransactions_Transaction = null;
		}

		public bool gxTv_SdtStoredTransactions_Transaction_IsNull()
		{
			return gxTv_SdtStoredTransactions_Transaction == null;
		}
		public bool ShouldSerializegxTpr_Transaction_GxSimpleCollection_Json()
		{
			return gxTv_SdtStoredTransactions_Transaction != null && gxTv_SdtStoredTransactions_Transaction.Count > 0;

		}


		public override bool ShouldSerializeSdtJson()
		{
			return (
				ShouldSerializegxTpr_Transaction_GxSimpleCollection_Json() || 
				false);
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtStoredTransactions_Transaction_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtStoredTransactions_Transaction_N;
		protected GXBaseCollection<SdtStoredTransactions_TransactionItem> gxTv_SdtStoredTransactions_Transaction = null; 



		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"StoredTransactions", Namespace="distributedcryptography")]
	public class SdtStoredTransactions_RESTInterface : GxGenericCollectionItem<SdtStoredTransactions>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtStoredTransactions_RESTInterface( ) : base()
		{	
		}

		public SdtStoredTransactions_RESTInterface( SdtStoredTransactions psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Transaction", Order=0, EmitDefaultValue=false)]
		public GxGenericCollection<SdtStoredTransactions_TransactionItem_RESTInterface> gxTpr_Transaction
		{
			get {
				if (sdt.ShouldSerializegxTpr_Transaction_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtStoredTransactions_TransactionItem_RESTInterface>(sdt.gxTpr_Transaction);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Transaction);
			}
		}


		#endregion

		public SdtStoredTransactions sdt
		{
			get { 
				return (SdtStoredTransactions)Sdt;
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
				sdt = new SdtStoredTransactions() ;
			}
		}
	}
	#endregion
}