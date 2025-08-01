/*
				   File: type_SdtelectrumRespBroadcastTran
			Description: electrumRespBroadcastTran
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

using GeneXus.Programs;
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="electrumRespBroadcastTran")]
	[XmlType(TypeName="electrumRespBroadcastTran" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespBroadcastTran : GxUserType
	{
		public SdtelectrumRespBroadcastTran( )
		{
			/* Constructor for serialization */
			gxTv_SdtelectrumRespBroadcastTran_Id = "";

			gxTv_SdtelectrumRespBroadcastTran_Jsonrpc = "";

			gxTv_SdtelectrumRespBroadcastTran_Result = "";

		}

		public SdtelectrumRespBroadcastTran(IGxContext context)
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


			AddObjectProperty("result", gxTpr_Result, false);

			if (gxTv_SdtelectrumRespBroadcastTran_Error != null)
			{
				AddObjectProperty("error", gxTv_SdtelectrumRespBroadcastTran_Error, false);
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
				return gxTv_SdtelectrumRespBroadcastTran_Id; 
			}
			set {
				gxTv_SdtelectrumRespBroadcastTran_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="jsonrpc")]
		[XmlElement(ElementName="jsonrpc")]
		public string gxTpr_Jsonrpc
		{
			get {
				return gxTv_SdtelectrumRespBroadcastTran_Jsonrpc; 
			}
			set {
				gxTv_SdtelectrumRespBroadcastTran_Jsonrpc = value;
				SetDirty("Jsonrpc");
			}
		}




		[SoapElement(ElementName="result")]
		[XmlElement(ElementName="result")]
		public string gxTpr_Result
		{
			get {
				return gxTv_SdtelectrumRespBroadcastTran_Result; 
			}
			set {
				gxTv_SdtelectrumRespBroadcastTran_Result = value;
				SetDirty("Result");
			}
		}



		[SoapElement(ElementName="error" )]
		[XmlElement(ElementName="error" )]
		public SdtelectrumRespBroadcastTran_error gxTpr_Error
		{
			get {
				if ( gxTv_SdtelectrumRespBroadcastTran_Error == null )
				{
					gxTv_SdtelectrumRespBroadcastTran_Error = new SdtelectrumRespBroadcastTran_error(context);
				}
				gxTv_SdtelectrumRespBroadcastTran_Error_N = false;
				SetDirty("Error");
				return gxTv_SdtelectrumRespBroadcastTran_Error;
			}
			set {
				gxTv_SdtelectrumRespBroadcastTran_Error_N = false;
				gxTv_SdtelectrumRespBroadcastTran_Error = value;
				SetDirty("Error");
			}

		}

		public void gxTv_SdtelectrumRespBroadcastTran_Error_SetNull()
		{
			gxTv_SdtelectrumRespBroadcastTran_Error_N = true;
			gxTv_SdtelectrumRespBroadcastTran_Error = null;
		}

		public bool gxTv_SdtelectrumRespBroadcastTran_Error_IsNull()
		{
			return gxTv_SdtelectrumRespBroadcastTran_Error == null;
		}
		public bool ShouldSerializegxTpr_Error_Json()
		{
				return (gxTv_SdtelectrumRespBroadcastTran_Error != null && gxTv_SdtelectrumRespBroadcastTran_Error.ShouldSerializeSdtJson());

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
			gxTv_SdtelectrumRespBroadcastTran_Id = "";
			gxTv_SdtelectrumRespBroadcastTran_Jsonrpc = "";
			gxTv_SdtelectrumRespBroadcastTran_Result = "";

			gxTv_SdtelectrumRespBroadcastTran_Error_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtelectrumRespBroadcastTran_Id;
		 

		protected string gxTv_SdtelectrumRespBroadcastTran_Jsonrpc;
		 

		protected string gxTv_SdtelectrumRespBroadcastTran_Result;
		 
		protected bool gxTv_SdtelectrumRespBroadcastTran_Error_N;
		protected SdtelectrumRespBroadcastTran_error gxTv_SdtelectrumRespBroadcastTran_Error = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"electrumRespBroadcastTran", Namespace="distributedcryptography")]
	public class SdtelectrumRespBroadcastTran_RESTInterface : GxGenericCollectionItem<SdtelectrumRespBroadcastTran>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespBroadcastTran_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespBroadcastTran_RESTInterface( SdtelectrumRespBroadcastTran psdt ) : base(psdt)
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
		[DataMember(Name="result", Order=2)]
		public  string gxTpr_Result
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Result);

			}
			set { 
				 sdt.gxTpr_Result = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="error", Order=3, EmitDefaultValue=false)]
		public SdtelectrumRespBroadcastTran_error_RESTInterface gxTpr_Error
		{
			get {
				if (sdt.ShouldSerializegxTpr_Error_Json())
					return new SdtelectrumRespBroadcastTran_error_RESTInterface(sdt.gxTpr_Error);
				else
					return null;

			}

			set {
				sdt.gxTpr_Error = value.sdt;
			}

		}


		#endregion
		[JsonIgnore]
		public SdtelectrumRespBroadcastTran sdt
		{
			get { 
				return (SdtelectrumRespBroadcastTran)Sdt;
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
				sdt = new SdtelectrumRespBroadcastTran() ;
			}
		}
	}
	#endregion
}