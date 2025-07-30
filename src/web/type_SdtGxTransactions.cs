/*
				   File: type_SdtGxTransactions
			Description: GxTransactions
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GxTransactions")]
	[XmlType(TypeName="GxTransactions" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxTransactions : GxUserType
	{
		public SdtGxTransactions( )
		{
			/* Constructor for serialization */
		}

		public SdtGxTransactions(IGxContext context)
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
			if (gxTv_SdtGxTransactions_Transaction != null)
			{
				AddObjectProperty("Transaction", gxTv_SdtGxTransactions_Transaction, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Transaction" )]
		[XmlArray(ElementName="Transaction"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.SdtGxTransactionItem> gxTpr_Transaction_GXBaseCollection
		{
			get {
				if ( gxTv_SdtGxTransactions_Transaction == null )
				{
					gxTv_SdtGxTransactions_Transaction = new GXBaseCollection<GeneXus.Programs.SdtGxTransactionItem>( context, "GxTransactionItem", "");
				}
				return gxTv_SdtGxTransactions_Transaction;
			}
			set {
				gxTv_SdtGxTransactions_Transaction_N = false;
				gxTv_SdtGxTransactions_Transaction = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.SdtGxTransactionItem> gxTpr_Transaction
		{
			get {
				if ( gxTv_SdtGxTransactions_Transaction == null )
				{
					gxTv_SdtGxTransactions_Transaction = new GXBaseCollection<GeneXus.Programs.SdtGxTransactionItem>( context, "GxTransactionItem", "");
				}
				gxTv_SdtGxTransactions_Transaction_N = false;
				SetDirty("Transaction");
				return gxTv_SdtGxTransactions_Transaction ;
			}
			set {
				gxTv_SdtGxTransactions_Transaction_N = false;
				gxTv_SdtGxTransactions_Transaction = value;
				SetDirty("Transaction");
			}
		}

		public void gxTv_SdtGxTransactions_Transaction_SetNull()
		{
			gxTv_SdtGxTransactions_Transaction_N = true;
			gxTv_SdtGxTransactions_Transaction = null;
		}

		public bool gxTv_SdtGxTransactions_Transaction_IsNull()
		{
			return gxTv_SdtGxTransactions_Transaction == null;
		}
		public bool ShouldSerializegxTpr_Transaction_GXBaseCollection_Json()
		{
			return gxTv_SdtGxTransactions_Transaction != null && gxTv_SdtGxTransactions_Transaction.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Transaction_GXBaseCollection_Json()||  
				false);
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
			gxTv_SdtGxTransactions_Transaction_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtGxTransactions_Transaction_N;
		protected GXBaseCollection<GeneXus.Programs.SdtGxTransactionItem> gxTv_SdtGxTransactions_Transaction = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GxTransactions", Namespace="distributedcryptography")]
	public class SdtGxTransactions_RESTInterface : GxGenericCollectionItem<SdtGxTransactions>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxTransactions_RESTInterface( ) : base()
		{	
		}

		public SdtGxTransactions_RESTInterface( SdtGxTransactions psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Transaction")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Transaction", Order=0, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.SdtGxTransactionItem_RESTInterface> gxTpr_Transaction
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Transaction_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.SdtGxTransactionItem_RESTInterface>(sdt.gxTpr_Transaction);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Transaction);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGxTransactions sdt
		{
			get { 
				return (SdtGxTransactions)Sdt;
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
				sdt = new SdtGxTransactions() ;
			}
		}
	}
	#endregion
}