/*
				   File: type_SdtcreateContact__postOutput
			Description: createContact__postOutput
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="createContact__postOutput")]
	[XmlType(TypeName="createContact__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtcreateContact__postOutput : GxUserType
	{
		public SdtcreateContact__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtcreateContact__postOutput_Error = "";

		}

		public SdtcreateContact__postOutput(IGxContext context)
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
			AddObjectProperty("ContactId", gxTpr_Contactid, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ContactId")]
		[XmlElement(ElementName="ContactId")]
		public Guid gxTpr_Contactid
		{
			get {
				return gxTv_SdtcreateContact__postOutput_Contactid; 
			}
			set {
				gxTv_SdtcreateContact__postOutput_Contactid = value;
				SetDirty("Contactid");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtcreateContact__postOutput_Error; 
			}
			set {
				gxTv_SdtcreateContact__postOutput_Error = value;
				SetDirty("Error");
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
			gxTv_SdtcreateContact__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtcreateContact__postOutput_Contactid;
		 

		protected string gxTv_SdtcreateContact__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"createContact__postOutput", Namespace="distributedcryptography")]
	public class SdtcreateContact__postOutput_RESTInterface : GxGenericCollectionItem<SdtcreateContact__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtcreateContact__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtcreateContact__postOutput_RESTInterface( SdtcreateContact__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("ContactId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="ContactId", Order=0)]
		public Guid gxTpr_Contactid
		{
			get { 
				return sdt.gxTpr_Contactid;

			}
			set { 
				sdt.gxTpr_Contactid = value;
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


		#endregion
		[JsonIgnore]
		public SdtcreateContact__postOutput sdt
		{
			get { 
				return (SdtcreateContact__postOutput)Sdt;
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
				sdt = new SdtcreateContact__postOutput() ;
			}
		}
	}
	#endregion
}