/*
				   File: type_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem
			Description: DesktopApp_services_SDT_Contacts_Contact_ContactItem
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
	[XmlRoot(ElementName="DesktopApp_services_SDT_Contacts_Contact_ContactItem")]
	[XmlType(TypeName="DesktopApp_services_SDT_Contacts_Contact_ContactItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem : GxUserType
	{
		public SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Encryptedkey = "";

			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Iv = "";

			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Contactencrypted = "";

		}

		public SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem(IGxContext context)
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


			AddObjectProperty("EncryptedKey", gxTpr_Encryptedkey, false);


			AddObjectProperty("IV", gxTpr_Iv, false);


			AddObjectProperty("ContactEncrypted", gxTpr_Contactencrypted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ContactId")]
		[XmlElement(ElementName="ContactId")]
		public Guid gxTpr_Contactid
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Contactid; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Contactid = value;
				SetDirty("Contactid");
			}
		}




		[SoapElement(ElementName="EncryptedKey")]
		[XmlElement(ElementName="EncryptedKey")]
		public string gxTpr_Encryptedkey
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Encryptedkey; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Encryptedkey = value;
				SetDirty("Encryptedkey");
			}
		}




		[SoapElement(ElementName="IV")]
		[XmlElement(ElementName="IV")]
		public string gxTpr_Iv
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Iv; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Iv = value;
				SetDirty("Iv");
			}
		}




		[SoapElement(ElementName="ContactEncrypted")]
		[XmlElement(ElementName="ContactEncrypted")]
		public string gxTpr_Contactencrypted
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Contactencrypted; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Contactencrypted = value;
				SetDirty("Contactencrypted");
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
			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Encryptedkey = "";
			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Iv = "";
			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Contactencrypted = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Contactid;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Encryptedkey;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Iv;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_Contactencrypted;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"DesktopApp_services_SDT_Contacts_Contact_ContactItem", Namespace="distributedcryptography")]
	public class SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_RESTInterface : GxGenericCollectionItem<SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_RESTInterface( ) : base()
		{	
		}

		public SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_RESTInterface( SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
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

		[DataMember(Name="EncryptedKey", Order=1)]
		public  string gxTpr_Encryptedkey
		{
			get { 
				return sdt.gxTpr_Encryptedkey;

			}
			set { 
				 sdt.gxTpr_Encryptedkey = value;
			}
		}

		[DataMember(Name="IV", Order=2)]
		public  string gxTpr_Iv
		{
			get { 
				return sdt.gxTpr_Iv;

			}
			set { 
				 sdt.gxTpr_Iv = value;
			}
		}

		[DataMember(Name="ContactEncrypted", Order=3)]
		public  string gxTpr_Contactencrypted
		{
			get { 
				return sdt.gxTpr_Contactencrypted;

			}
			set { 
				 sdt.gxTpr_Contactencrypted = value;
			}
		}


		#endregion

		public SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem sdt
		{
			get { 
				return (SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem)Sdt;
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
				sdt = new SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem() ;
			}
		}
	}
	#endregion
}