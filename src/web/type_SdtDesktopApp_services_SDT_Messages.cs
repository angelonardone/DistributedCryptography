/*
				   File: type_SdtDesktopApp_services_SDT_Messages
			Description: DesktopApp_services_SDT_Messages
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
	[XmlRoot(ElementName="DesktopApp_services_SDT_Messages")]
	[XmlType(TypeName="DesktopApp_services_SDT_Messages" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDesktopApp_services_SDT_Messages : GxUserType
	{
		public SdtDesktopApp_services_SDT_Messages( )
		{
			/* Constructor for serialization */
		}

		public SdtDesktopApp_services_SDT_Messages(IGxContext context)
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
			if (gxTv_SdtDesktopApp_services_SDT_Messages_Message != null)
			{
				AddObjectProperty("Message", gxTv_SdtDesktopApp_services_SDT_Messages_Message, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Message" )]
		[XmlArray(ElementName="Message"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_Message_MessageItem> gxTpr_Message_GXBaseCollection
		{
			get {
				if ( gxTv_SdtDesktopApp_services_SDT_Messages_Message == null )
				{
					gxTv_SdtDesktopApp_services_SDT_Messages_Message = new GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_Message_MessageItem>( context, "DesktopApp_services_SDT_Messages_Message_MessageItem", "");
				}
				return gxTv_SdtDesktopApp_services_SDT_Messages_Message;
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Messages_Message_N = false;
				gxTv_SdtDesktopApp_services_SDT_Messages_Message = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_Message_MessageItem> gxTpr_Message
		{
			get {
				if ( gxTv_SdtDesktopApp_services_SDT_Messages_Message == null )
				{
					gxTv_SdtDesktopApp_services_SDT_Messages_Message = new GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_Message_MessageItem>( context, "DesktopApp_services_SDT_Messages_Message_MessageItem", "");
				}
				gxTv_SdtDesktopApp_services_SDT_Messages_Message_N = false;
				return gxTv_SdtDesktopApp_services_SDT_Messages_Message ;
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Messages_Message_N = false;
				gxTv_SdtDesktopApp_services_SDT_Messages_Message = value;
				SetDirty("Message");
			}
		}

		public void gxTv_SdtDesktopApp_services_SDT_Messages_Message_SetNull()
		{
			gxTv_SdtDesktopApp_services_SDT_Messages_Message_N = true;
			gxTv_SdtDesktopApp_services_SDT_Messages_Message = null;
		}

		public bool gxTv_SdtDesktopApp_services_SDT_Messages_Message_IsNull()
		{
			return gxTv_SdtDesktopApp_services_SDT_Messages_Message == null;
		}
		public bool ShouldSerializegxTpr_Message_GXBaseCollection_Json()
		{
			return gxTv_SdtDesktopApp_services_SDT_Messages_Message != null && gxTv_SdtDesktopApp_services_SDT_Messages_Message.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Message_GXBaseCollection_Json()||  
				false);
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtDesktopApp_services_SDT_Messages_Message_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtDesktopApp_services_SDT_Messages_Message_N;
		protected GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_Message_MessageItem> gxTv_SdtDesktopApp_services_SDT_Messages_Message = null;  


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"DesktopApp_services_SDT_Messages", Namespace="distributedcryptography")]
	public class SdtDesktopApp_services_SDT_Messages_RESTInterface : GxGenericCollectionItem<SdtDesktopApp_services_SDT_Messages>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDesktopApp_services_SDT_Messages_RESTInterface( ) : base()
		{	
		}

		public SdtDesktopApp_services_SDT_Messages_RESTInterface( SdtDesktopApp_services_SDT_Messages psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="Message", Order=0, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_Message_MessageItem_RESTInterface> gxTpr_Message
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Message_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_Message_MessageItem_RESTInterface>(sdt.gxTpr_Message);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Message);
			}
		}


		#endregion

		public SdtDesktopApp_services_SDT_Messages sdt
		{
			get { 
				return (SdtDesktopApp_services_SDT_Messages)Sdt;
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
				sdt = new SdtDesktopApp_services_SDT_Messages() ;
			}
		}
	}
	#endregion
}