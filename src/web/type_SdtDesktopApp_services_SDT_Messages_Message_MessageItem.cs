/*
				   File: type_SdtDesktopApp_services_SDT_Messages_Message_MessageItem
			Description: DesktopApp_services_SDT_Messages_Message_MessageItem
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
	[XmlRoot(ElementName="DesktopApp_services_SDT_Messages_Message_MessageItem")]
	[XmlType(TypeName="DesktopApp_services_SDT_Messages_Message_MessageItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDesktopApp_services_SDT_Messages_Message_MessageItem : GxUserType
	{
		public SdtDesktopApp_services_SDT_Messages_Message_MessageItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencryptedkey = "";

			gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageiv = "";

			gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencrypted = "";

		}

		public SdtDesktopApp_services_SDT_Messages_Message_MessageItem(IGxContext context)
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
			AddObjectProperty("MessageId", gxTpr_Messageid, false);


			AddObjectProperty("MessageEncryptedKey", gxTpr_Messageencryptedkey, false);


			AddObjectProperty("MessageIV", gxTpr_Messageiv, false);


			AddObjectProperty("MessageEncrypted", gxTpr_Messageencrypted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="MessageId")]
		[XmlElement(ElementName="MessageId")]
		public Guid gxTpr_Messageid
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageid; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageid = value;
				SetDirty("Messageid");
			}
		}




		[SoapElement(ElementName="MessageEncryptedKey")]
		[XmlElement(ElementName="MessageEncryptedKey")]
		public string gxTpr_Messageencryptedkey
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencryptedkey; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencryptedkey = value;
				SetDirty("Messageencryptedkey");
			}
		}




		[SoapElement(ElementName="MessageIV")]
		[XmlElement(ElementName="MessageIV")]
		public string gxTpr_Messageiv
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageiv; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageiv = value;
				SetDirty("Messageiv");
			}
		}




		[SoapElement(ElementName="MessageEncrypted")]
		[XmlElement(ElementName="MessageEncrypted")]
		public string gxTpr_Messageencrypted
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencrypted; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencrypted = value;
				SetDirty("Messageencrypted");
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
			gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencryptedkey = "";
			gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageiv = "";
			gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencrypted = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageid;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencryptedkey;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageiv;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Messages_Message_MessageItem_Messageencrypted;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"DesktopApp_services_SDT_Messages_Message_MessageItem", Namespace="distributedcryptography")]
	public class SdtDesktopApp_services_SDT_Messages_Message_MessageItem_RESTInterface : GxGenericCollectionItem<SdtDesktopApp_services_SDT_Messages_Message_MessageItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDesktopApp_services_SDT_Messages_Message_MessageItem_RESTInterface( ) : base()
		{	
		}

		public SdtDesktopApp_services_SDT_Messages_Message_MessageItem_RESTInterface( SdtDesktopApp_services_SDT_Messages_Message_MessageItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="MessageId", Order=0)]
		public Guid gxTpr_Messageid
		{
			get { 
				return sdt.gxTpr_Messageid;

			}
			set { 
				sdt.gxTpr_Messageid = value;
			}
		}

		[DataMember(Name="MessageEncryptedKey", Order=1)]
		public  string gxTpr_Messageencryptedkey
		{
			get { 
				return sdt.gxTpr_Messageencryptedkey;

			}
			set { 
				 sdt.gxTpr_Messageencryptedkey = value;
			}
		}

		[DataMember(Name="MessageIV", Order=2)]
		public  string gxTpr_Messageiv
		{
			get { 
				return sdt.gxTpr_Messageiv;

			}
			set { 
				 sdt.gxTpr_Messageiv = value;
			}
		}

		[DataMember(Name="MessageEncrypted", Order=3)]
		public  string gxTpr_Messageencrypted
		{
			get { 
				return sdt.gxTpr_Messageencrypted;

			}
			set { 
				 sdt.gxTpr_Messageencrypted = value;
			}
		}


		#endregion

		public SdtDesktopApp_services_SDT_Messages_Message_MessageItem sdt
		{
			get { 
				return (SdtDesktopApp_services_SDT_Messages_Message_MessageItem)Sdt;
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
				sdt = new SdtDesktopApp_services_SDT_Messages_Message_MessageItem() ;
			}
		}
	}
	#endregion
}