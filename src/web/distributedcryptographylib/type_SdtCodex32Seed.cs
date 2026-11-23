/*
				   File: type_SdtCodex32Seed
			Description: Codex32Seed
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
	[XmlRoot(ElementName="Codex32Seed")]
	[XmlType(TypeName="Codex32Seed" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtCodex32Seed : GxUserType
	{
		public SdtCodex32Seed( )
		{
			/* Constructor for serialization */
			gxTv_SdtCodex32Seed_Error = "";

			gxTv_SdtCodex32Seed_Seed = "";

		}

		public SdtCodex32Seed(IGxContext context)
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


			AddObjectProperty("seed", gxTpr_Seed, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtCodex32Seed_Success; 
			}
			set {
				gxTv_SdtCodex32Seed_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtCodex32Seed_Error; 
			}
			set {
				gxTv_SdtCodex32Seed_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="seed")]
		[XmlElement(ElementName="seed")]
		public string gxTpr_Seed
		{
			get {
				return gxTv_SdtCodex32Seed_Seed; 
			}
			set {
				gxTv_SdtCodex32Seed_Seed = value;
				SetDirty("Seed");
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
			gxTv_SdtCodex32Seed_Error = "";
			gxTv_SdtCodex32Seed_Seed = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtCodex32Seed_Success;
		 

		protected string gxTv_SdtCodex32Seed_Error;
		 

		protected string gxTv_SdtCodex32Seed_Seed;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Codex32Seed", Namespace="distributedcryptography")]
	public class SdtCodex32Seed_RESTInterface : GxGenericCollectionItem<SdtCodex32Seed>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtCodex32Seed_RESTInterface( ) : base()
		{	
		}

		public SdtCodex32Seed_RESTInterface( SdtCodex32Seed psdt ) : base(psdt)
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

		[JsonPropertyName("seed")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="seed", Order=2)]
		public  string gxTpr_Seed
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Seed);

			}
			set { 
				 sdt.gxTpr_Seed = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtCodex32Seed sdt
		{
			get { 
				return (SdtCodex32Seed)Sdt;
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
				sdt = new SdtCodex32Seed() ;
			}
		}
	}
	#endregion
}