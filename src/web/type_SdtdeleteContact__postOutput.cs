/*
				   File: type_SdtdeleteContact__postOutput
			Description: deleteContact__postOutput
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
	[XmlRoot(ElementName="deleteContact__postOutput")]
	[XmlType(TypeName="deleteContact__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtdeleteContact__postOutput : GxUserType
	{
		public SdtdeleteContact__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtdeleteContact__postOutput_Error = "";

		}

		public SdtdeleteContact__postOutput(IGxContext context)
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
			AddObjectProperty("deletedContactId", gxTpr_Deletedcontactid, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="deletedContactId")]
		[XmlElement(ElementName="deletedContactId")]
		public Guid gxTpr_Deletedcontactid
		{
			get {
				return gxTv_SdtdeleteContact__postOutput_Deletedcontactid; 
			}
			set {
				gxTv_SdtdeleteContact__postOutput_Deletedcontactid = value;
				SetDirty("Deletedcontactid");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtdeleteContact__postOutput_Error; 
			}
			set {
				gxTv_SdtdeleteContact__postOutput_Error = value;
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
			gxTv_SdtdeleteContact__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtdeleteContact__postOutput_Deletedcontactid;
		 

		protected string gxTv_SdtdeleteContact__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"deleteContact__postOutput", Namespace="distributedcryptography")]
	public class SdtdeleteContact__postOutput_RESTInterface : GxGenericCollectionItem<SdtdeleteContact__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtdeleteContact__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtdeleteContact__postOutput_RESTInterface( SdtdeleteContact__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="deletedContactId", Order=0)]
		public Guid gxTpr_Deletedcontactid
		{
			get { 
				return sdt.gxTpr_Deletedcontactid;

			}
			set { 
				sdt.gxTpr_Deletedcontactid = value;
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

		public SdtdeleteContact__postOutput sdt
		{
			get { 
				return (SdtdeleteContact__postOutput)Sdt;
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
				sdt = new SdtdeleteContact__postOutput() ;
			}
		}
	}
	#endregion
}