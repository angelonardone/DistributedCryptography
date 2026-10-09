/*
				   File: type_SdtLegacyPsbtResult
			Description: LegacyPsbtResult
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
	[XmlRoot(ElementName="LegacyPsbtResult")]
	[XmlType(TypeName="LegacyPsbtResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtLegacyPsbtResult : GxUserType
	{
		public SdtLegacyPsbtResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtLegacyPsbtResult_Error = "";

			gxTv_SdtLegacyPsbtResult_Psbt = "";

		}

		public SdtLegacyPsbtResult(IGxContext context)
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


			AddObjectProperty("psbt", gxTpr_Psbt, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtLegacyPsbtResult_Success; 
			}
			set {
				gxTv_SdtLegacyPsbtResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtLegacyPsbtResult_Error; 
			}
			set {
				gxTv_SdtLegacyPsbtResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="psbt")]
		[XmlElement(ElementName="psbt")]
		public string gxTpr_Psbt
		{
			get {
				return gxTv_SdtLegacyPsbtResult_Psbt; 
			}
			set {
				gxTv_SdtLegacyPsbtResult_Psbt = value;
				SetDirty("Psbt");
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
			gxTv_SdtLegacyPsbtResult_Error = "";
			gxTv_SdtLegacyPsbtResult_Psbt = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtLegacyPsbtResult_Success;
		 

		protected string gxTv_SdtLegacyPsbtResult_Error;
		 

		protected string gxTv_SdtLegacyPsbtResult_Psbt;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"LegacyPsbtResult", Namespace="distributedcryptography")]
	public class SdtLegacyPsbtResult_RESTInterface : GxGenericCollectionItem<SdtLegacyPsbtResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtLegacyPsbtResult_RESTInterface( ) : base()
		{	
		}

		public SdtLegacyPsbtResult_RESTInterface( SdtLegacyPsbtResult psdt ) : base(psdt)
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

		[JsonPropertyName("psbt")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="psbt", Order=2)]
		public  string gxTpr_Psbt
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Psbt);

			}
			set { 
				 sdt.gxTpr_Psbt = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtLegacyPsbtResult sdt
		{
			get { 
				return (SdtLegacyPsbtResult)Sdt;
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
				sdt = new SdtLegacyPsbtResult() ;
			}
		}
	}
	#endregion
}