/*
				   File: type_SdtgetUserContacts__getOutput
			Description: getUserContacts__getOutput
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
	[XmlRoot(ElementName="getUserContacts__getOutput")]
	[XmlType(TypeName="getUserContacts__getOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtgetUserContacts__getOutput : GxUserType
	{
		public SdtgetUserContacts__getOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtgetUserContacts__getOutput_Contacts_N = true;

			gxTv_SdtgetUserContacts__getOutput_Error = "";

		}

		public SdtgetUserContacts__getOutput(IGxContext context)
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
			if (gxTv_SdtgetUserContacts__getOutput_Contacts != null)
			{
				AddObjectProperty("contacts", gxTv_SdtgetUserContacts__getOutput_Contacts, false);
			}

			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="contacts")]
		[XmlElement(ElementName="contacts")]
		public GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts gxTpr_Contacts
		{
			get {
				if ( gxTv_SdtgetUserContacts__getOutput_Contacts == null )
				{
					gxTv_SdtgetUserContacts__getOutput_Contacts = new GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts(context);
					SetDirty("Contacts");
				}
				return gxTv_SdtgetUserContacts__getOutput_Contacts; 
			}
			set {
				gxTv_SdtgetUserContacts__getOutput_Contacts = value;
				SetDirty("Contacts");
			}
		}
		public void gxTv_SdtgetUserContacts__getOutput_Contacts_SetNull()
		{
			gxTv_SdtgetUserContacts__getOutput_Contacts_N = true;
			gxTv_SdtgetUserContacts__getOutput_Contacts = null;
		}

		public bool gxTv_SdtgetUserContacts__getOutput_Contacts_IsNull()
		{
			return gxTv_SdtgetUserContacts__getOutput_Contacts == null;
		}
		public bool ShouldSerializegxTpr_Contacts_Json()
		{
			return gxTv_SdtgetUserContacts__getOutput_Contacts != null;

		}


		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtgetUserContacts__getOutput_Error; 
			}
			set {
				gxTv_SdtgetUserContacts__getOutput_Error = value;
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
			gxTv_SdtgetUserContacts__getOutput_Contacts_N = true;

			gxTv_SdtgetUserContacts__getOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts gxTv_SdtgetUserContacts__getOutput_Contacts = null;
		protected bool gxTv_SdtgetUserContacts__getOutput_Contacts_N;
		 

		protected string gxTv_SdtgetUserContacts__getOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"getUserContacts__getOutput", Namespace="distributedcryptography")]
	public class SdtgetUserContacts__getOutput_RESTInterface : GxGenericCollectionItem<SdtgetUserContacts__getOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtgetUserContacts__getOutput_RESTInterface( ) : base()
		{	
		}

		public SdtgetUserContacts__getOutput_RESTInterface( SdtgetUserContacts__getOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("contacts")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="contacts", Order=0, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_RESTInterface gxTpr_Contacts
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Contacts_Json())
					return new GeneXus.Programs.SdtDesktopApp_services_SDT_Contacts_RESTInterface(sdt.gxTpr_Contacts);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Contacts = value.sdt;
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
		public SdtgetUserContacts__getOutput sdt
		{
			get { 
				return (SdtgetUserContacts__getOutput)Sdt;
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
				sdt = new SdtgetUserContacts__getOutput() ;
			}
		}
	}
	#endregion
}