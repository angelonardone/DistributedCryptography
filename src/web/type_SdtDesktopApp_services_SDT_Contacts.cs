/*
				   File: type_SdtDesktopApp_services_SDT_Contacts
			Description: DesktopApp_services_SDT_Contacts
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
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
	[XmlRoot(ElementName="DesktopApp_services_SDT_Contacts")]
	[XmlType(TypeName="DesktopApp_services_SDT_Contacts" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDesktopApp_services_SDT_Contacts : GxUserType
	{
		public SdtDesktopApp_services_SDT_Contacts( )
		{
			/* Constructor for serialization */
		}

		public SdtDesktopApp_services_SDT_Contacts(IGxContext context)
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
			if (gxTv_SdtDesktopApp_services_SDT_Contacts_Contact != null)
			{
				AddObjectProperty("Contact", gxTv_SdtDesktopApp_services_SDT_Contacts_Contact, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Contact" )]
		[XmlArray(ElementName="Contact"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem> gxTpr_Contact_GXBaseCollection
		{
			get {
				if ( gxTv_SdtDesktopApp_services_SDT_Contacts_Contact == null )
				{
					gxTv_SdtDesktopApp_services_SDT_Contacts_Contact = new GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem>( context, "DesktopApp_services_SDT_Contacts_Contact_ContactItem", "");
				}
				return gxTv_SdtDesktopApp_services_SDT_Contacts_Contact;
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_N = false;
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem> gxTpr_Contact
		{
			get {
				if ( gxTv_SdtDesktopApp_services_SDT_Contacts_Contact == null )
				{
					gxTv_SdtDesktopApp_services_SDT_Contacts_Contact = new GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem>( context, "DesktopApp_services_SDT_Contacts_Contact_ContactItem", "");
				}
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_N = false;
				SetDirty("Contact");
				return gxTv_SdtDesktopApp_services_SDT_Contacts_Contact ;
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_N = false;
				gxTv_SdtDesktopApp_services_SDT_Contacts_Contact = value;
				SetDirty("Contact");
			}
		}

		public void gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_SetNull()
		{
			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_N = true;
			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact = null;
		}

		public bool gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_IsNull()
		{
			return gxTv_SdtDesktopApp_services_SDT_Contacts_Contact == null;
		}
		public bool ShouldSerializegxTpr_Contact_GXBaseCollection_Json()
		{
			return gxTv_SdtDesktopApp_services_SDT_Contacts_Contact != null && gxTv_SdtDesktopApp_services_SDT_Contacts_Contact.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Contact_GXBaseCollection_Json()||  
				false);
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
			gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtDesktopApp_services_SDT_Contacts_Contact_N;
		protected GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem> gxTv_SdtDesktopApp_services_SDT_Contacts_Contact = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"DesktopApp_services_SDT_Contacts", Namespace="distributedcryptography")]
	public class SdtDesktopApp_services_SDT_Contacts_RESTInterface : GxGenericCollectionItem<SdtDesktopApp_services_SDT_Contacts>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDesktopApp_services_SDT_Contacts_RESTInterface( ) : base()
		{	
		}

		public SdtDesktopApp_services_SDT_Contacts_RESTInterface( SdtDesktopApp_services_SDT_Contacts psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Contact")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Contact", Order=0, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_RESTInterface> gxTpr_Contact
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Contact_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_Contact_ContactItem_RESTInterface>(sdt.gxTpr_Contact);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Contact);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtDesktopApp_services_SDT_Contacts sdt
		{
			get { 
				return (SdtDesktopApp_services_SDT_Contacts)Sdt;
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
				sdt = new SdtDesktopApp_services_SDT_Contacts() ;
			}
		}
	}
	#endregion
}