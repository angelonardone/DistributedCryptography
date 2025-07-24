/*
				   File: type_SdtelectrumRespGetTransactionId
			Description: electrumRespGetTransactionId
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186676
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
	[XmlRoot(ElementName="electrumRespGetTransactionId")]
	[XmlType(TypeName="electrumRespGetTransactionId" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespGetTransactionId : GxUserType
	{
		public SdtelectrumRespGetTransactionId( )
		{
			/* Constructor for serialization */
			gxTv_SdtelectrumRespGetTransactionId_Id = "";

			gxTv_SdtelectrumRespGetTransactionId_Jsonrpc = "";

		}

		public SdtelectrumRespGetTransactionId(IGxContext context)
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

			if (gxTv_SdtelectrumRespGetTransactionId_Result != null)
			{
				AddObjectProperty("result", gxTv_SdtelectrumRespGetTransactionId_Result, false);
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
				return gxTv_SdtelectrumRespGetTransactionId_Id; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="jsonrpc")]
		[XmlElement(ElementName="jsonrpc")]
		public string gxTpr_Jsonrpc
		{
			get {
				return gxTv_SdtelectrumRespGetTransactionId_Jsonrpc; 
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_Jsonrpc = value;
				SetDirty("Jsonrpc");
			}
		}



		[SoapElement(ElementName="result" )]
		[XmlElement(ElementName="result" )]
		public SdtelectrumRespGetTransactionId_result gxTpr_Result
		{
			get {
				if ( gxTv_SdtelectrumRespGetTransactionId_Result == null )
				{
					gxTv_SdtelectrumRespGetTransactionId_Result = new SdtelectrumRespGetTransactionId_result(context);
				}
				gxTv_SdtelectrumRespGetTransactionId_Result_N = false;
				SetDirty("Result");
				return gxTv_SdtelectrumRespGetTransactionId_Result;
			}
			set {
				gxTv_SdtelectrumRespGetTransactionId_Result_N = false;
				gxTv_SdtelectrumRespGetTransactionId_Result = value;
				SetDirty("Result");
			}

		}

		public void gxTv_SdtelectrumRespGetTransactionId_Result_SetNull()
		{
			gxTv_SdtelectrumRespGetTransactionId_Result_N = true;
			gxTv_SdtelectrumRespGetTransactionId_Result = null;
		}

		public bool gxTv_SdtelectrumRespGetTransactionId_Result_IsNull()
		{
			return gxTv_SdtelectrumRespGetTransactionId_Result == null;
		}
		public bool ShouldSerializegxTpr_Result_Json()
		{
				return (gxTv_SdtelectrumRespGetTransactionId_Result != null && gxTv_SdtelectrumRespGetTransactionId_Result.ShouldSerializeSdtJson());

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
			gxTv_SdtelectrumRespGetTransactionId_Id = "";
			gxTv_SdtelectrumRespGetTransactionId_Jsonrpc = "";

			gxTv_SdtelectrumRespGetTransactionId_Result_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtelectrumRespGetTransactionId_Id;
		 

		protected string gxTv_SdtelectrumRespGetTransactionId_Jsonrpc;
		 
		protected bool gxTv_SdtelectrumRespGetTransactionId_Result_N;
		protected SdtelectrumRespGetTransactionId_result gxTv_SdtelectrumRespGetTransactionId_Result = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"electrumRespGetTransactionId", Namespace="distributedcryptography")]
	public class SdtelectrumRespGetTransactionId_RESTInterface : GxGenericCollectionItem<SdtelectrumRespGetTransactionId>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespGetTransactionId_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespGetTransactionId_RESTInterface( SdtelectrumRespGetTransactionId psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("id")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="id", Order=0)]
		public  string gxTpr_Id
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Id);

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
				return StringUtil.RTrim( sdt.gxTpr_Jsonrpc);

			}
			set { 
				 sdt.gxTpr_Jsonrpc = value;
			}
		}

		[JsonPropertyName("result")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="result", Order=2, EmitDefaultValue=false)]
		public SdtelectrumRespGetTransactionId_result_RESTInterface gxTpr_Result
		{
			get {
				if (sdt.ShouldSerializegxTpr_Result_Json())
					return new SdtelectrumRespGetTransactionId_result_RESTInterface(sdt.gxTpr_Result);
				else
					return null;

			}

			set {
				sdt.gxTpr_Result = value.sdt;
			}

		}


		#endregion
		[JsonIgnore]
		public SdtelectrumRespGetTransactionId sdt
		{
			get { 
				return (SdtelectrumRespGetTransactionId)Sdt;
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
				sdt = new SdtelectrumRespGetTransactionId() ;
			}
		}
	}
	#endregion
}