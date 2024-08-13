/*
				   File: type_SdtGxExplorer_services_TxoutFromAddresses
			Description: GxExplorer_services_TxoutFromAddresses
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GxExplorer_services_TxoutFromAddresses")]
	[XmlType(TypeName="GxExplorer_services_TxoutFromAddresses" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxExplorer_services_TxoutFromAddresses : GxUserType
	{
		public SdtGxExplorer_services_TxoutFromAddresses( )
		{
			/* Constructor for serialization */
		}

		public SdtGxExplorer_services_TxoutFromAddresses(IGxContext context)
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
			if (gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction != null)
			{
				AddObjectProperty("Transaction", gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Transaction" )]
		[XmlArray(ElementName="Transaction"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem> gxTpr_Transaction_GXBaseCollection
		{
			get {
				if ( gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction == null )
				{
					gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction = new GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem>( context, "GxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem", "");
				}
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction;
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_N = false;
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem> gxTpr_Transaction
		{
			get {
				if ( gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction == null )
				{
					gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction = new GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem>( context, "GxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem", "");
				}
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_N = false;
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction ;
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_N = false;
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction = value;
				SetDirty("Transaction");
			}
		}

		public void gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_SetNull()
		{
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_N = true;
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction = null;
		}

		public bool gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_IsNull()
		{
			return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction == null;
		}
		public bool ShouldSerializegxTpr_Transaction_GXBaseCollection_Json()
		{
			return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction != null && gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction.Count > 0;

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
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_N;
		protected GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem> gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GxExplorer_services_TxoutFromAddresses", Namespace="distributedcryptography")]
	public class SdtGxExplorer_services_TxoutFromAddresses_RESTInterface : GxGenericCollectionItem<SdtGxExplorer_services_TxoutFromAddresses>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxExplorer_services_TxoutFromAddresses_RESTInterface( ) : base()
		{	
		}

		public SdtGxExplorer_services_TxoutFromAddresses_RESTInterface( SdtGxExplorer_services_TxoutFromAddresses psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Transaction", Order=0, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_RESTInterface> gxTpr_Transaction
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Transaction_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_TransactionItem_RESTInterface>(sdt.gxTpr_Transaction);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Transaction);
			}
		}


		#endregion

		public SdtGxExplorer_services_TxoutFromAddresses sdt
		{
			get { 
				return (SdtGxExplorer_services_TxoutFromAddresses)Sdt;
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
				sdt = new SdtGxExplorer_services_TxoutFromAddresses() ;
			}
		}
	}
	#endregion
}