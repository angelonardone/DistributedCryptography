/*
				   File: type_SdtLegacyFinalResult
			Description: LegacyFinalResult
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
using GeneXus.Programs.wallet;

namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="LegacyFinalResult")]
	[XmlType(TypeName="LegacyFinalResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtLegacyFinalResult : GxUserType
	{
		public SdtLegacyFinalResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtLegacyFinalResult_Error = "";

			gxTv_SdtLegacyFinalResult_Txhex = "";

			gxTv_SdtLegacyFinalResult_Txid = "";

		}

		public SdtLegacyFinalResult(IGxContext context)
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
			AddObjectProperty("success", gxTpr_Success, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("txHex", gxTpr_Txhex, false);


			AddObjectProperty("txId", gxTpr_Txid, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtLegacyFinalResult_Success; 
			}
			set {
				gxTv_SdtLegacyFinalResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtLegacyFinalResult_Error; 
			}
			set {
				gxTv_SdtLegacyFinalResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="txHex")]
		[XmlElement(ElementName="txHex")]
		public string gxTpr_Txhex
		{
			get {
				return gxTv_SdtLegacyFinalResult_Txhex; 
			}
			set {
				gxTv_SdtLegacyFinalResult_Txhex = value;
				SetDirty("Txhex");
			}
		}




		[SoapElement(ElementName="txId")]
		[XmlElement(ElementName="txId")]
		public string gxTpr_Txid
		{
			get {
				return gxTv_SdtLegacyFinalResult_Txid; 
			}
			set {
				gxTv_SdtLegacyFinalResult_Txid = value;
				SetDirty("Txid");
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
			gxTv_SdtLegacyFinalResult_Error = "";
			gxTv_SdtLegacyFinalResult_Txhex = "";
			gxTv_SdtLegacyFinalResult_Txid = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtLegacyFinalResult_Success;
		 

		protected string gxTv_SdtLegacyFinalResult_Error;
		 

		protected string gxTv_SdtLegacyFinalResult_Txhex;
		 

		protected string gxTv_SdtLegacyFinalResult_Txid;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"LegacyFinalResult", Namespace="distributedcryptography")]
	public class SdtLegacyFinalResult_RESTInterface : GxGenericCollectionItem<SdtLegacyFinalResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtLegacyFinalResult_RESTInterface( ) : base()
		{	
		}

		public SdtLegacyFinalResult_RESTInterface( SdtLegacyFinalResult psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("success")]
		[JsonPropertyOrder(0)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="success", Order=0)]
		public bool gxTpr_Success
		{
			get { 
				return sdt.gxTpr_Success;

			}
			set { 
				sdt.gxTpr_Success = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[JsonPropertyName("txHex")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="txHex", Order=2)]
		public  string gxTpr_Txhex
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Txhex);

			}
			set { 
				 sdt.gxTpr_Txhex = value;
			}
		}

		[JsonPropertyName("txId")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="txId", Order=3)]
		public  string gxTpr_Txid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Txid);

			}
			set { 
				 sdt.gxTpr_Txid = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtLegacyFinalResult sdt
		{
			get { 
				return (SdtLegacyFinalResult)Sdt;
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
				sdt = new SdtLegacyFinalResult() ;
			}
		}
	}
	#endregion
}