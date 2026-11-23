/*
				   File: type_SdtRubyChildMnemonic
			Description: RubyChildMnemonic
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="RubyChildMnemonic")]
	[XmlType(TypeName="RubyChildMnemonic" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRubyChildMnemonic : GxUserType
	{
		public SdtRubyChildMnemonic( )
		{
			/* Constructor for serialization */
			gxTv_SdtRubyChildMnemonic_Error = "";

			gxTv_SdtRubyChildMnemonic_Mnemonic = "";


		}

		public SdtRubyChildMnemonic(IGxContext context)
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


			AddObjectProperty("mnemonic", gxTpr_Mnemonic, false);


			AddObjectProperty("cancelled", gxTpr_Cancelled, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtRubyChildMnemonic_Success; 
			}
			set {
				gxTv_SdtRubyChildMnemonic_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtRubyChildMnemonic_Error; 
			}
			set {
				gxTv_SdtRubyChildMnemonic_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="mnemonic")]
		[XmlElement(ElementName="mnemonic")]
		public string gxTpr_Mnemonic
		{
			get {
				return gxTv_SdtRubyChildMnemonic_Mnemonic; 
			}
			set {
				gxTv_SdtRubyChildMnemonic_Mnemonic = value;
				SetDirty("Mnemonic");
			}
		}




		[SoapElement(ElementName="cancelled")]
		[XmlElement(ElementName="cancelled")]
		public bool gxTpr_Cancelled
		{
			get {
				return gxTv_SdtRubyChildMnemonic_Cancelled; 
			}
			set {
				gxTv_SdtRubyChildMnemonic_Cancelled = value;
				SetDirty("Cancelled");
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
			gxTv_SdtRubyChildMnemonic_Error = "";
			gxTv_SdtRubyChildMnemonic_Mnemonic = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtRubyChildMnemonic_Success;
		 

		protected string gxTv_SdtRubyChildMnemonic_Error;
		 

		protected string gxTv_SdtRubyChildMnemonic_Mnemonic;
		 

		protected bool gxTv_SdtRubyChildMnemonic_Cancelled;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"RubyChildMnemonic", Namespace="distributedcryptography")]
	public class SdtRubyChildMnemonic_RESTInterface : GxGenericCollectionItem<SdtRubyChildMnemonic>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRubyChildMnemonic_RESTInterface( ) : base()
		{	
		}

		public SdtRubyChildMnemonic_RESTInterface( SdtRubyChildMnemonic psdt ) : base(psdt)
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

		[JsonPropertyName("mnemonic")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="mnemonic", Order=2)]
		public  string gxTpr_Mnemonic
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Mnemonic);

			}
			set { 
				 sdt.gxTpr_Mnemonic = value;
			}
		}

		[JsonPropertyName("cancelled")]
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="cancelled", Order=3)]
		public bool gxTpr_Cancelled
		{
			get { 
				return sdt.gxTpr_Cancelled;

			}
			set { 
				sdt.gxTpr_Cancelled = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtRubyChildMnemonic sdt
		{
			get { 
				return (SdtRubyChildMnemonic)Sdt;
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
				sdt = new SdtRubyChildMnemonic() ;
			}
		}
	}
	#endregion
}