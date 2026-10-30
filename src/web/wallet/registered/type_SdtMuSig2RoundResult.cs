/*
				   File: type_SdtMuSig2RoundResult
			Description: MuSig2RoundResult
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
	[XmlRoot(ElementName="MuSig2RoundResult")]
	[XmlType(TypeName="MuSig2RoundResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2RoundResult : GxUserType
	{
		public SdtMuSig2RoundResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2RoundResult_Error = "";

			gxTv_SdtMuSig2RoundResult_Sessionid = "";

			gxTv_SdtMuSig2RoundResult_Signer = "";

			gxTv_SdtMuSig2RoundResult_Publicnonces = "";

			gxTv_SdtMuSig2RoundResult_Partialsignatures = "";

		}

		public SdtMuSig2RoundResult(IGxContext context)
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


			AddObjectProperty("sessionId", gxTpr_Sessionid, false);


			AddObjectProperty("signer", gxTpr_Signer, false);


			AddObjectProperty("publicNonces", gxTpr_Publicnonces, false);


			AddObjectProperty("partialSignatures", gxTpr_Partialsignatures, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtMuSig2RoundResult_Success; 
			}
			set {
				gxTv_SdtMuSig2RoundResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtMuSig2RoundResult_Error; 
			}
			set {
				gxTv_SdtMuSig2RoundResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="sessionId")]
		[XmlElement(ElementName="sessionId")]
		public string gxTpr_Sessionid
		{
			get {
				return gxTv_SdtMuSig2RoundResult_Sessionid; 
			}
			set {
				gxTv_SdtMuSig2RoundResult_Sessionid = value;
				SetDirty("Sessionid");
			}
		}




		[SoapElement(ElementName="signer")]
		[XmlElement(ElementName="signer")]
		public string gxTpr_Signer
		{
			get {
				return gxTv_SdtMuSig2RoundResult_Signer; 
			}
			set {
				gxTv_SdtMuSig2RoundResult_Signer = value;
				SetDirty("Signer");
			}
		}




		[SoapElement(ElementName="publicNonces")]
		[XmlElement(ElementName="publicNonces")]
		public string gxTpr_Publicnonces
		{
			get {
				return gxTv_SdtMuSig2RoundResult_Publicnonces; 
			}
			set {
				gxTv_SdtMuSig2RoundResult_Publicnonces = value;
				SetDirty("Publicnonces");
			}
		}




		[SoapElement(ElementName="partialSignatures")]
		[XmlElement(ElementName="partialSignatures")]
		public string gxTpr_Partialsignatures
		{
			get {
				return gxTv_SdtMuSig2RoundResult_Partialsignatures; 
			}
			set {
				gxTv_SdtMuSig2RoundResult_Partialsignatures = value;
				SetDirty("Partialsignatures");
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
			gxTv_SdtMuSig2RoundResult_Error = "";
			gxTv_SdtMuSig2RoundResult_Sessionid = "";
			gxTv_SdtMuSig2RoundResult_Signer = "";
			gxTv_SdtMuSig2RoundResult_Publicnonces = "";
			gxTv_SdtMuSig2RoundResult_Partialsignatures = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtMuSig2RoundResult_Success;
		 

		protected string gxTv_SdtMuSig2RoundResult_Error;
		 

		protected string gxTv_SdtMuSig2RoundResult_Sessionid;
		 

		protected string gxTv_SdtMuSig2RoundResult_Signer;
		 

		protected string gxTv_SdtMuSig2RoundResult_Publicnonces;
		 

		protected string gxTv_SdtMuSig2RoundResult_Partialsignatures;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSig2RoundResult", Namespace="distributedcryptography")]
	public class SdtMuSig2RoundResult_RESTInterface : GxGenericCollectionItem<SdtMuSig2RoundResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2RoundResult_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2RoundResult_RESTInterface( SdtMuSig2RoundResult psdt ) : base(psdt)
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

		[JsonPropertyName("sessionId")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="sessionId", Order=2)]
		public  string gxTpr_Sessionid
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sessionid);

			}
			set { 
				 sdt.gxTpr_Sessionid = value;
			}
		}

		[JsonPropertyName("signer")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="signer", Order=3)]
		public  string gxTpr_Signer
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signer);

			}
			set { 
				 sdt.gxTpr_Signer = value;
			}
		}

		[JsonPropertyName("publicNonces")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="publicNonces", Order=4)]
		public  string gxTpr_Publicnonces
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publicnonces);

			}
			set { 
				 sdt.gxTpr_Publicnonces = value;
			}
		}

		[JsonPropertyName("partialSignatures")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="partialSignatures", Order=5)]
		public  string gxTpr_Partialsignatures
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Partialsignatures);

			}
			set { 
				 sdt.gxTpr_Partialsignatures = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMuSig2RoundResult sdt
		{
			get { 
				return (SdtMuSig2RoundResult)Sdt;
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
				sdt = new SdtMuSig2RoundResult() ;
			}
		}
	}
	#endregion
}