/*
				   File: type_SdtdeleteMesage__postOutput
			Description: deleteMesage__postOutput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.14.187820
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
	[XmlRoot(ElementName="deleteMesage__postOutput")]
	[XmlType(TypeName="deleteMesage__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtdeleteMesage__postOutput : GxUserType
	{
		public SdtdeleteMesage__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtdeleteMesage__postOutput_Error = "";

		}

		public SdtdeleteMesage__postOutput(IGxContext context)
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
			AddObjectProperty("deleteMessageId", gxTpr_Deletemessageid, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="deleteMessageId")]
		[XmlElement(ElementName="deleteMessageId")]
		public Guid gxTpr_Deletemessageid
		{
			get {
				return gxTv_SdtdeleteMesage__postOutput_Deletemessageid; 
			}
			set {
				gxTv_SdtdeleteMesage__postOutput_Deletemessageid = value;
				SetDirty("Deletemessageid");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtdeleteMesage__postOutput_Error; 
			}
			set {
				gxTv_SdtdeleteMesage__postOutput_Error = value;
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
			gxTv_SdtdeleteMesage__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtdeleteMesage__postOutput_Deletemessageid;
		 

		protected string gxTv_SdtdeleteMesage__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"deleteMesage__postOutput", Namespace="distributedcryptography")]
	public class SdtdeleteMesage__postOutput_RESTInterface : GxGenericCollectionItem<SdtdeleteMesage__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtdeleteMesage__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtdeleteMesage__postOutput_RESTInterface( SdtdeleteMesage__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("deleteMessageId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="deleteMessageId", Order=0)]
		public Guid gxTpr_Deletemessageid
		{
			get { 
				return sdt.gxTpr_Deletemessageid;

			}
			set { 
				sdt.gxTpr_Deletemessageid = value;
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
		public SdtdeleteMesage__postOutput sdt
		{
			get { 
				return (SdtdeleteMesage__postOutput)Sdt;
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
				sdt = new SdtdeleteMesage__postOutput() ;
			}
		}
	}
	#endregion
}