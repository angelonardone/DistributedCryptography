/*
				   File: type_SdtCodex32Mask
			Description: Codex32Mask
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

namespace GeneXus.Programs.distributedcryptographylib
{
	[XmlRoot(ElementName="Codex32Mask")]
	[XmlType(TypeName="Codex32Mask" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtCodex32Mask : GxUserType
	{
		public SdtCodex32Mask( )
		{
			/* Constructor for serialization */
			gxTv_SdtCodex32Mask_Error = "";

			gxTv_SdtCodex32Mask_Pad = "";

			gxTv_SdtCodex32Mask_Masked = "";

		}

		public SdtCodex32Mask(IGxContext context)
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


			AddObjectProperty("pad", gxTpr_Pad, false);


			AddObjectProperty("masked", gxTpr_Masked, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtCodex32Mask_Success; 
			}
			set {
				gxTv_SdtCodex32Mask_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtCodex32Mask_Error; 
			}
			set {
				gxTv_SdtCodex32Mask_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="pad")]
		[XmlElement(ElementName="pad")]
		public string gxTpr_Pad
		{
			get {
				return gxTv_SdtCodex32Mask_Pad; 
			}
			set {
				gxTv_SdtCodex32Mask_Pad = value;
				SetDirty("Pad");
			}
		}




		[SoapElement(ElementName="masked")]
		[XmlElement(ElementName="masked")]
		public string gxTpr_Masked
		{
			get {
				return gxTv_SdtCodex32Mask_Masked; 
			}
			set {
				gxTv_SdtCodex32Mask_Masked = value;
				SetDirty("Masked");
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
			gxTv_SdtCodex32Mask_Error = "";
			gxTv_SdtCodex32Mask_Pad = "";
			gxTv_SdtCodex32Mask_Masked = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtCodex32Mask_Success;
		 

		protected string gxTv_SdtCodex32Mask_Error;
		 

		protected string gxTv_SdtCodex32Mask_Pad;
		 

		protected string gxTv_SdtCodex32Mask_Masked;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Codex32Mask", Namespace="distributedcryptography")]
	public class SdtCodex32Mask_RESTInterface : GxGenericCollectionItem<SdtCodex32Mask>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtCodex32Mask_RESTInterface( ) : base()
		{	
		}

		public SdtCodex32Mask_RESTInterface( SdtCodex32Mask psdt ) : base(psdt)
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

		[JsonPropertyName("pad")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="pad", Order=2)]
		public  string gxTpr_Pad
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Pad);

			}
			set { 
				 sdt.gxTpr_Pad = value;
			}
		}

		[JsonPropertyName("masked")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="masked", Order=3)]
		public  string gxTpr_Masked
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Masked);

			}
			set { 
				 sdt.gxTpr_Masked = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtCodex32Mask sdt
		{
			get { 
				return (SdtCodex32Mask)Sdt;
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
				sdt = new SdtCodex32Mask() ;
			}
		}
	}
	#endregion
}