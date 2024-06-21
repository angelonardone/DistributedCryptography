/*
				   File: type_Sdtrpc_ScanTxOutSet_response
			Description: rpc_ScanTxOutSet_response
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
	[XmlRoot(ElementName="rpc_ScanTxOutSet_response")]
	[XmlType(TypeName="rpc_ScanTxOutSet_response" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdtrpc_ScanTxOutSet_response : GxUserType
	{
		public Sdtrpc_ScanTxOutSet_response( )
		{
			/* Constructor for serialization */
		}

		public Sdtrpc_ScanTxOutSet_response(IGxContext context)
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
			if (gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal != null)
			{
				AddObjectProperty("addressSubTotal", gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="addressSubTotal" )]
		[XmlArray(ElementName="addressSubTotal"  )]
		[XmlArrayItemAttribute(ElementName="addressSubTotalItem" , IsNullable=false )]
		public GXBaseCollection<Sdtrpc_ScanTxOutSet_response_addressSubTotalItem> gxTpr_Addresssubtotal
		{
			get {
				if ( gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal == null )
				{
					gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal = new GXBaseCollection<Sdtrpc_ScanTxOutSet_response_addressSubTotalItem>( context, "rpc_ScanTxOutSet_response.addressSubTotalItem", "");
				}
				return gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal;
			}
			set {
				gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal_N = false;
				gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal = value;
				SetDirty("Addresssubtotal");
			}
		}

		public void gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal_SetNull()
		{
			gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal_N = true;
			gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal = null;
		}

		public bool gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal_IsNull()
		{
			return gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal == null;
		}
		public bool ShouldSerializegxTpr_Addresssubtotal_GxSimpleCollection_Json()
		{
			return gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal != null && gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal.Count > 0;

		}


		public override bool ShouldSerializeSdtJson()
		{
			return (
				ShouldSerializegxTpr_Addresssubtotal_GxSimpleCollection_Json() || 
				false);
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal_N;
		protected GXBaseCollection<Sdtrpc_ScanTxOutSet_response_addressSubTotalItem> gxTv_Sdtrpc_ScanTxOutSet_response_Addresssubtotal = null; 



		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"rpc_ScanTxOutSet_response", Namespace="distributedcryptography")]
	public class Sdtrpc_ScanTxOutSet_response_RESTInterface : GxGenericCollectionItem<Sdtrpc_ScanTxOutSet_response>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdtrpc_ScanTxOutSet_response_RESTInterface( ) : base()
		{	
		}

		public Sdtrpc_ScanTxOutSet_response_RESTInterface( Sdtrpc_ScanTxOutSet_response psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="addressSubTotal", Order=0, EmitDefaultValue=false)]
		public GxGenericCollection<Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_RESTInterface> gxTpr_Addresssubtotal
		{
			get {
				if (sdt.ShouldSerializegxTpr_Addresssubtotal_GxSimpleCollection_Json())
					return new GxGenericCollection<Sdtrpc_ScanTxOutSet_response_addressSubTotalItem_RESTInterface>(sdt.gxTpr_Addresssubtotal);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Addresssubtotal);
			}
		}


		#endregion

		public Sdtrpc_ScanTxOutSet_response sdt
		{
			get { 
				return (Sdtrpc_ScanTxOutSet_response)Sdt;
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
				sdt = new Sdtrpc_ScanTxOutSet_response() ;
			}
		}
	}
	#endregion
}