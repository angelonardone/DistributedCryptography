/*
				   File: type_SdtdeleteGroup__postOutput
			Description: deleteGroup__postOutput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.12.186073
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
	[XmlRoot(ElementName="deleteGroup__postOutput")]
	[XmlType(TypeName="deleteGroup__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtdeleteGroup__postOutput : GxUserType
	{
		public SdtdeleteGroup__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtdeleteGroup__postOutput_Error = "";

		}

		public SdtdeleteGroup__postOutput(IGxContext context)
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
			AddObjectProperty("deletedGroupId", gxTpr_Deletedgroupid, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="deletedGroupId")]
		[XmlElement(ElementName="deletedGroupId")]
		public Guid gxTpr_Deletedgroupid
		{
			get {
				return gxTv_SdtdeleteGroup__postOutput_Deletedgroupid; 
			}
			set {
				gxTv_SdtdeleteGroup__postOutput_Deletedgroupid = value;
				SetDirty("Deletedgroupid");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtdeleteGroup__postOutput_Error; 
			}
			set {
				gxTv_SdtdeleteGroup__postOutput_Error = value;
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
			gxTv_SdtdeleteGroup__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtdeleteGroup__postOutput_Deletedgroupid;
		 

		protected string gxTv_SdtdeleteGroup__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"deleteGroup__postOutput", Namespace="distributedcryptography")]
	public class SdtdeleteGroup__postOutput_RESTInterface : GxGenericCollectionItem<SdtdeleteGroup__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtdeleteGroup__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtdeleteGroup__postOutput_RESTInterface( SdtdeleteGroup__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("deletedGroupId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="deletedGroupId", Order=0)]
		public Guid gxTpr_Deletedgroupid
		{
			get { 
				return sdt.gxTpr_Deletedgroupid;

			}
			set { 
				sdt.gxTpr_Deletedgroupid = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
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
		[JsonIgnore]
		public SdtdeleteGroup__postOutput sdt
		{
			get { 
				return (SdtdeleteGroup__postOutput)Sdt;
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
				sdt = new SdtdeleteGroup__postOutput() ;
			}
		}
	}
	#endregion
}