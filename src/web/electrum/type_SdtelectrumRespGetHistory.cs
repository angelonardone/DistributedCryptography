/*
				   File: type_SdtelectrumRespGetHistory
			Description: electrumRespGetHistory
				 Author: Nemo 🐠 for C# (.NET) version 18.0.12.186073
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
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="electrumRespGetHistory")]
	[XmlType(TypeName="electrumRespGetHistory" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespGetHistory : GxUserType
	{
		public SdtelectrumRespGetHistory( )
		{
			/* Constructor for serialization */
			gxTv_SdtelectrumRespGetHistory_Id = "";

			gxTv_SdtelectrumRespGetHistory_Jsonrpc = "";

		}

		public SdtelectrumRespGetHistory(IGxContext context)
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
			AddObjectProperty("id", gxTpr_Id, false);


			AddObjectProperty("jsonrpc", gxTpr_Jsonrpc, false);

			if (gxTv_SdtelectrumRespGetHistory_Result != null)
			{
				AddObjectProperty("result", gxTv_SdtelectrumRespGetHistory_Result, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="id")]
		[XmlElement(ElementName="id")]
		public string gxTpr_Id
		{
			get {
				return gxTv_SdtelectrumRespGetHistory_Id; 
			}
			set {
				gxTv_SdtelectrumRespGetHistory_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="jsonrpc")]
		[XmlElement(ElementName="jsonrpc")]
		public string gxTpr_Jsonrpc
		{
			get {
				return gxTv_SdtelectrumRespGetHistory_Jsonrpc; 
			}
			set {
				gxTv_SdtelectrumRespGetHistory_Jsonrpc = value;
				SetDirty("Jsonrpc");
			}
		}




		[SoapElement(ElementName="result" )]
		[XmlArray(ElementName="result"  )]
		[XmlArrayItemAttribute(ElementName="resultItem" , IsNullable=false )]
		public GXBaseCollection<SdtelectrumRespGetHistory_resultItem> gxTpr_Result
		{
			get {
				if ( gxTv_SdtelectrumRespGetHistory_Result == null )
				{
					gxTv_SdtelectrumRespGetHistory_Result = new GXBaseCollection<SdtelectrumRespGetHistory_resultItem>( context, "electrumRespGetHistory.resultItem", "");
				}
				SetDirty("Result");
				return gxTv_SdtelectrumRespGetHistory_Result;
			}
			set {
				gxTv_SdtelectrumRespGetHistory_Result_N = false;
				gxTv_SdtelectrumRespGetHistory_Result = value;
				SetDirty("Result");
			}
		}

		public void gxTv_SdtelectrumRespGetHistory_Result_SetNull()
		{
			gxTv_SdtelectrumRespGetHistory_Result_N = true;
			gxTv_SdtelectrumRespGetHistory_Result = null;
		}

		public bool gxTv_SdtelectrumRespGetHistory_Result_IsNull()
		{
			return gxTv_SdtelectrumRespGetHistory_Result == null;
		}
		public bool ShouldSerializegxTpr_Result_GxSimpleCollection_Json()
		{
			return gxTv_SdtelectrumRespGetHistory_Result != null && gxTv_SdtelectrumRespGetHistory_Result.Count > 0;

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
			gxTv_SdtelectrumRespGetHistory_Id = "";
			gxTv_SdtelectrumRespGetHistory_Jsonrpc = "";

			gxTv_SdtelectrumRespGetHistory_Result_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtelectrumRespGetHistory_Id;
		 

		protected string gxTv_SdtelectrumRespGetHistory_Jsonrpc;
		 
		protected bool gxTv_SdtelectrumRespGetHistory_Result_N;
		protected GXBaseCollection<SdtelectrumRespGetHistory_resultItem> gxTv_SdtelectrumRespGetHistory_Result = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"electrumRespGetHistory", Namespace="distributedcryptography")]
	public class SdtelectrumRespGetHistory_RESTInterface : GxGenericCollectionItem<SdtelectrumRespGetHistory>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespGetHistory_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespGetHistory_RESTInterface( SdtelectrumRespGetHistory psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("id")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="id", Order=0)]
		public  string gxTpr_Id
		{
			get { 
				return sdt.gxTpr_Id;

			}
			set { 
				 sdt.gxTpr_Id = value;
			}
		}

		[JsonPropertyName("jsonrpc")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="jsonrpc", Order=1)]
		public  string gxTpr_Jsonrpc
		{
			get { 
				return sdt.gxTpr_Jsonrpc;

			}
			set { 
				 sdt.gxTpr_Jsonrpc = value;
			}
		}

		[JsonPropertyName("result")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="result", Order=2, EmitDefaultValue=false)]
		public GxGenericCollection<SdtelectrumRespGetHistory_resultItem_RESTInterface> gxTpr_Result
		{
			get {
				if (sdt.ShouldSerializegxTpr_Result_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtelectrumRespGetHistory_resultItem_RESTInterface>(sdt.gxTpr_Result);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Result);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtelectrumRespGetHistory sdt
		{
			get { 
				return (SdtelectrumRespGetHistory)Sdt;
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
				sdt = new SdtelectrumRespGetHistory() ;
			}
		}
	}
	#endregion
}