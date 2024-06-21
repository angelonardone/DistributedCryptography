/*
				   File: type_SdtcreateGroup__postOutput
			Description: createGroup__postOutput
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
	[XmlRoot(ElementName="createGroup__postOutput")]
	[XmlType(TypeName="createGroup__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtcreateGroup__postOutput : GxUserType
	{
		public SdtcreateGroup__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtcreateGroup__postOutput_Error = "";

		}

		public SdtcreateGroup__postOutput(IGxContext context)
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
			AddObjectProperty("GroupId", gxTpr_Groupid, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="GroupId")]
		[XmlElement(ElementName="GroupId")]
		public Guid gxTpr_Groupid
		{
			get {
				return gxTv_SdtcreateGroup__postOutput_Groupid; 
			}
			set {
				gxTv_SdtcreateGroup__postOutput_Groupid = value;
				SetDirty("Groupid");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtcreateGroup__postOutput_Error; 
			}
			set {
				gxTv_SdtcreateGroup__postOutput_Error = value;
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
			gxTv_SdtcreateGroup__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtcreateGroup__postOutput_Groupid;
		 

		protected string gxTv_SdtcreateGroup__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"createGroup__postOutput", Namespace="distributedcryptography")]
	public class SdtcreateGroup__postOutput_RESTInterface : GxGenericCollectionItem<SdtcreateGroup__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtcreateGroup__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtcreateGroup__postOutput_RESTInterface( SdtcreateGroup__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="GroupId", Order=0)]
		public Guid gxTpr_Groupid
		{
			get { 
				return sdt.gxTpr_Groupid;

			}
			set { 
				sdt.gxTpr_Groupid = value;
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

		public SdtcreateGroup__postOutput sdt
		{
			get { 
				return (SdtcreateGroup__postOutput)Sdt;
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
				sdt = new SdtcreateGroup__postOutput() ;
			}
		}
	}
	#endregion
}