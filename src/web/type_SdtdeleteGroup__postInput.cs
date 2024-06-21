/*
				   File: type_SdtdeleteGroup__postInput
			Description: deleteGroup__postInput
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
	[XmlRoot(ElementName="deleteGroup__postInput")]
	[XmlType(TypeName="deleteGroup__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtdeleteGroup__postInput : GxUserType
	{
		public SdtdeleteGroup__postInput( )
		{
			/* Constructor for serialization */
		}

		public SdtdeleteGroup__postInput(IGxContext context)
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

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="GroupId")]
		[XmlElement(ElementName="GroupId")]
		public Guid gxTpr_Groupid
		{
			get {
				return gxTv_SdtdeleteGroup__postInput_Groupid; 
			}
			set {
				gxTv_SdtdeleteGroup__postInput_Groupid = value;
				SetDirty("Groupid");
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
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtdeleteGroup__postInput_Groupid;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"deleteGroup__postInput", Namespace="distributedcryptography")]
	public class SdtdeleteGroup__postInput_RESTInterface : GxGenericCollectionItem<SdtdeleteGroup__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtdeleteGroup__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtdeleteGroup__postInput_RESTInterface( SdtdeleteGroup__postInput psdt ) : base(psdt)
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


		#endregion

		public SdtdeleteGroup__postInput sdt
		{
			get { 
				return (SdtdeleteGroup__postInput)Sdt;
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
				sdt = new SdtdeleteGroup__postInput() ;
			}
		}
	}
	#endregion
}