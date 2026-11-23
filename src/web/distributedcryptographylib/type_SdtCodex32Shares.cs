/*
				   File: type_SdtCodex32Shares
			Description: Codex32Shares
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
	[XmlRoot(ElementName="Codex32Shares")]
	[XmlType(TypeName="Codex32Shares" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtCodex32Shares : GxUserType
	{
		public SdtCodex32Shares( )
		{
			/* Constructor for serialization */
			gxTv_SdtCodex32Shares_Error = "";

			gxTv_SdtCodex32Shares_Identifier = "";

		}

		public SdtCodex32Shares(IGxContext context)
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


			AddObjectProperty("identifier", gxTpr_Identifier, false);

			if (gxTv_SdtCodex32Shares_Shares != null)
			{
				AddObjectProperty("shares", gxTv_SdtCodex32Shares_Shares, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtCodex32Shares_Success; 
			}
			set {
				gxTv_SdtCodex32Shares_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtCodex32Shares_Error; 
			}
			set {
				gxTv_SdtCodex32Shares_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="identifier")]
		[XmlElement(ElementName="identifier")]
		public string gxTpr_Identifier
		{
			get {
				return gxTv_SdtCodex32Shares_Identifier; 
			}
			set {
				gxTv_SdtCodex32Shares_Identifier = value;
				SetDirty("Identifier");
			}
		}




		[SoapElement(ElementName="shares" )]
		[XmlArray(ElementName="shares"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GxSimpleCollection<string> gxTpr_Shares_GxSimpleCollection
		{
			get {
				if ( gxTv_SdtCodex32Shares_Shares == null )
				{
					gxTv_SdtCodex32Shares_Shares = new GxSimpleCollection<string>( );
				}
				return gxTv_SdtCodex32Shares_Shares;
			}
			set {
				gxTv_SdtCodex32Shares_Shares_N = false;
				gxTv_SdtCodex32Shares_Shares = value;
			}
		}

		[XmlIgnore]
		public GxSimpleCollection<string> gxTpr_Shares
		{
			get {
				if ( gxTv_SdtCodex32Shares_Shares == null )
				{
					gxTv_SdtCodex32Shares_Shares = new GxSimpleCollection<string>();
				}
				gxTv_SdtCodex32Shares_Shares_N = false;
				SetDirty("Shares");
				return gxTv_SdtCodex32Shares_Shares ;
			}
			set {
				gxTv_SdtCodex32Shares_Shares_N = false;
				gxTv_SdtCodex32Shares_Shares = value;
				SetDirty("Shares");
			}
		}

		public void gxTv_SdtCodex32Shares_Shares_SetNull()
		{
			gxTv_SdtCodex32Shares_Shares_N = true;
			gxTv_SdtCodex32Shares_Shares = null;
		}

		public bool gxTv_SdtCodex32Shares_Shares_IsNull()
		{
			return gxTv_SdtCodex32Shares_Shares == null;
		}
		public bool ShouldSerializegxTpr_Shares_GxSimpleCollection_Json()
		{
			return gxTv_SdtCodex32Shares_Shares != null && gxTv_SdtCodex32Shares_Shares.Count > 0;

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
			gxTv_SdtCodex32Shares_Error = "";
			gxTv_SdtCodex32Shares_Identifier = "";

			gxTv_SdtCodex32Shares_Shares_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtCodex32Shares_Success;
		 

		protected string gxTv_SdtCodex32Shares_Error;
		 

		protected string gxTv_SdtCodex32Shares_Identifier;
		 
		protected bool gxTv_SdtCodex32Shares_Shares_N;
		protected GxSimpleCollection<string> gxTv_SdtCodex32Shares_Shares = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Codex32Shares", Namespace="distributedcryptography")]
	public class SdtCodex32Shares_RESTInterface : GxGenericCollectionItem<SdtCodex32Shares>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtCodex32Shares_RESTInterface( ) : base()
		{	
		}

		public SdtCodex32Shares_RESTInterface( SdtCodex32Shares psdt ) : base(psdt)
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

		[JsonPropertyName("identifier")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="identifier", Order=2)]
		public  string gxTpr_Identifier
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Identifier);

			}
			set { 
				 sdt.gxTpr_Identifier = value;
			}
		}

		[JsonPropertyName("shares")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="shares", Order=3, EmitDefaultValue=false)]
		public  GxSimpleCollection<string> gxTpr_Shares
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Shares_GxSimpleCollection_Json())
					return sdt.gxTpr_Shares;
				else
					return null;

			}
			set { 
				sdt.gxTpr_Shares = value ;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtCodex32Shares sdt
		{
			get { 
				return (SdtCodex32Shares)Sdt;
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
				sdt = new SdtCodex32Shares() ;
			}
		}
	}
	#endregion
}