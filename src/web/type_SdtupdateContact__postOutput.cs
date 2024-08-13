/*
				   File: type_SdtupdateContact__postOutput
			Description: updateContact__postOutput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="updateContact__postOutput")]
	[XmlType(TypeName="updateContact__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtupdateContact__postOutput : GxUserType
	{
		public SdtupdateContact__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtupdateContact__postOutput_Error = "";

		}

		public SdtupdateContact__postOutput(IGxContext context)
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
			AddObjectProperty("updatedContactId", gxTpr_Updatedcontactid, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="updatedContactId")]
		[XmlElement(ElementName="updatedContactId")]
		public Guid gxTpr_Updatedcontactid
		{
			get {
				return gxTv_SdtupdateContact__postOutput_Updatedcontactid; 
			}
			set {
				gxTv_SdtupdateContact__postOutput_Updatedcontactid = value;
				SetDirty("Updatedcontactid");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtupdateContact__postOutput_Error; 
			}
			set {
				gxTv_SdtupdateContact__postOutput_Error = value;
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
			gxTv_SdtupdateContact__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtupdateContact__postOutput_Updatedcontactid;
		 

		protected string gxTv_SdtupdateContact__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"updateContact__postOutput", Namespace="distributedcryptography")]
	public class SdtupdateContact__postOutput_RESTInterface : GxGenericCollectionItem<SdtupdateContact__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtupdateContact__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtupdateContact__postOutput_RESTInterface( SdtupdateContact__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="updatedContactId", Order=0)]
		public Guid gxTpr_Updatedcontactid
		{
			get { 
				return sdt.gxTpr_Updatedcontactid;

			}
			set { 
				sdt.gxTpr_Updatedcontactid = value;
			}
		}

		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return sdt.gxTpr_Error;

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}


		#endregion

		public SdtupdateContact__postOutput sdt
		{
			get { 
				return (SdtupdateContact__postOutput)Sdt;
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
				sdt = new SdtupdateContact__postOutput() ;
			}
		}
	}
	#endregion
}