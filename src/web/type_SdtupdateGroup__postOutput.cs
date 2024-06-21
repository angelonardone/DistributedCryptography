/*
				   File: type_SdtupdateGroup__postOutput
			Description: updateGroup__postOutput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
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
	[XmlRoot(ElementName="updateGroup__postOutput")]
	[XmlType(TypeName="updateGroup__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtupdateGroup__postOutput : GxUserType
	{
		public SdtupdateGroup__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtupdateGroup__postOutput_Error = "";

		}

		public SdtupdateGroup__postOutput(IGxContext context)
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
			AddObjectProperty("updatedGroupId", gxTpr_Updatedgroupid, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="updatedGroupId")]
		[XmlElement(ElementName="updatedGroupId")]
		public Guid gxTpr_Updatedgroupid
		{
			get {
				return gxTv_SdtupdateGroup__postOutput_Updatedgroupid; 
			}
			set {
				gxTv_SdtupdateGroup__postOutput_Updatedgroupid = value;
				SetDirty("Updatedgroupid");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtupdateGroup__postOutput_Error; 
			}
			set {
				gxTv_SdtupdateGroup__postOutput_Error = value;
				SetDirty("Error");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtupdateGroup__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtupdateGroup__postOutput_Updatedgroupid;
		 

		protected string gxTv_SdtupdateGroup__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"updateGroup__postOutput", Namespace="distributedcryptography")]
	public class SdtupdateGroup__postOutput_RESTInterface : GxGenericCollectionItem<SdtupdateGroup__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtupdateGroup__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtupdateGroup__postOutput_RESTInterface( SdtupdateGroup__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="updatedGroupId", Order=0)]
		public Guid gxTpr_Updatedgroupid
		{
			get { 
				return sdt.gxTpr_Updatedgroupid;

			}
			set { 
				sdt.gxTpr_Updatedgroupid = value;
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

		public SdtupdateGroup__postOutput sdt
		{
			get { 
				return (SdtupdateGroup__postOutput)Sdt;
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
				sdt = new SdtupdateGroup__postOutput() ;
			}
		}
	}
	#endregion
}