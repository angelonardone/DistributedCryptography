/*
				   File: type_SdtMuSig2VsizeResult
			Description: MuSig2VsizeResult
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
	[XmlRoot(ElementName="MuSig2VsizeResult")]
	[XmlType(TypeName="MuSig2VsizeResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2VsizeResult : GxUserType
	{
		public SdtMuSig2VsizeResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2VsizeResult_Error = "";


		}

		public SdtMuSig2VsizeResult(IGxContext context)
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


			AddObjectProperty("vsize", gxTpr_Vsize, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtMuSig2VsizeResult_Success; 
			}
			set {
				gxTv_SdtMuSig2VsizeResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtMuSig2VsizeResult_Error; 
			}
			set {
				gxTv_SdtMuSig2VsizeResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="vsize")]
		[XmlElement(ElementName="vsize")]
		public long gxTpr_Vsize
		{
			get {
				return gxTv_SdtMuSig2VsizeResult_Vsize; 
			}
			set {
				gxTv_SdtMuSig2VsizeResult_Vsize = value;
				SetDirty("Vsize");
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
			gxTv_SdtMuSig2VsizeResult_Error = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtMuSig2VsizeResult_Success;
		 

		protected string gxTv_SdtMuSig2VsizeResult_Error;
		 

		protected long gxTv_SdtMuSig2VsizeResult_Vsize;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSig2VsizeResult", Namespace="distributedcryptography")]
	public class SdtMuSig2VsizeResult_RESTInterface : GxGenericCollectionItem<SdtMuSig2VsizeResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2VsizeResult_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2VsizeResult_RESTInterface( SdtMuSig2VsizeResult psdt ) : base(psdt)
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

		[JsonPropertyName("vsize")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="vsize", Order=2)]
		public  string gxTpr_Vsize
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Vsize, 10, 0));

			}
			set { 
				sdt.gxTpr_Vsize = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMuSig2VsizeResult sdt
		{
			get { 
				return (SdtMuSig2VsizeResult)Sdt;
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
				sdt = new SdtMuSig2VsizeResult() ;
			}
		}
	}
	#endregion
}