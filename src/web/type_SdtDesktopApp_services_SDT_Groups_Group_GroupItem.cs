/*
				   File: type_SdtDesktopApp_services_SDT_Groups_Group_GroupItem
			Description: DesktopApp_services_SDT_Groups_Group_GroupItem
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
	[XmlRoot(ElementName="DesktopApp_services_SDT_Groups_Group_GroupItem")]
	[XmlType(TypeName="DesktopApp_services_SDT_Groups_Group_GroupItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDesktopApp_services_SDT_Groups_Group_GroupItem : GxUserType
	{
		public SdtDesktopApp_services_SDT_Groups_Group_GroupItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencryptedkey = "";

			gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupiv = "";

			gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencrypted = "";

		}

		public SdtDesktopApp_services_SDT_Groups_Group_GroupItem(IGxContext context)
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


			AddObjectProperty("GroupEncryptedKey", gxTpr_Groupencryptedkey, false);


			AddObjectProperty("GroupIV", gxTpr_Groupiv, false);


			AddObjectProperty("GroupEncrypted", gxTpr_Groupencrypted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="GroupId")]
		[XmlElement(ElementName="GroupId")]
		public Guid gxTpr_Groupid
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupid; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupid = value;
				SetDirty("Groupid");
			}
		}




		[SoapElement(ElementName="GroupEncryptedKey")]
		[XmlElement(ElementName="GroupEncryptedKey")]
		public string gxTpr_Groupencryptedkey
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencryptedkey; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencryptedkey = value;
				SetDirty("Groupencryptedkey");
			}
		}




		[SoapElement(ElementName="GroupIV")]
		[XmlElement(ElementName="GroupIV")]
		public string gxTpr_Groupiv
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupiv; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupiv = value;
				SetDirty("Groupiv");
			}
		}




		[SoapElement(ElementName="GroupEncrypted")]
		[XmlElement(ElementName="GroupEncrypted")]
		public string gxTpr_Groupencrypted
		{
			get {
				return gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencrypted; 
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencrypted = value;
				SetDirty("Groupencrypted");
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
			gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencryptedkey = "";
			gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupiv = "";
			gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencrypted = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupid;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencryptedkey;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupiv;
		 

		protected string gxTv_SdtDesktopApp_services_SDT_Groups_Group_GroupItem_Groupencrypted;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"DesktopApp_services_SDT_Groups_Group_GroupItem", Namespace="distributedcryptography")]
	public class SdtDesktopApp_services_SDT_Groups_Group_GroupItem_RESTInterface : GxGenericCollectionItem<SdtDesktopApp_services_SDT_Groups_Group_GroupItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDesktopApp_services_SDT_Groups_Group_GroupItem_RESTInterface( ) : base()
		{	
		}

		public SdtDesktopApp_services_SDT_Groups_Group_GroupItem_RESTInterface( SdtDesktopApp_services_SDT_Groups_Group_GroupItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("GroupId")]
		[JsonPropertyOrder(0)]
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

		[JsonPropertyName("GroupEncryptedKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="GroupEncryptedKey", Order=1)]
		public  string gxTpr_Groupencryptedkey
		{
			get { 
				return sdt.gxTpr_Groupencryptedkey;

			}
			set { 
				 sdt.gxTpr_Groupencryptedkey = value;
			}
		}

		[JsonPropertyName("GroupIV")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="GroupIV", Order=2)]
		public  string gxTpr_Groupiv
		{
			get { 
				return sdt.gxTpr_Groupiv;

			}
			set { 
				 sdt.gxTpr_Groupiv = value;
			}
		}

		[JsonPropertyName("GroupEncrypted")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="GroupEncrypted", Order=3)]
		public  string gxTpr_Groupencrypted
		{
			get { 
				return sdt.gxTpr_Groupencrypted;

			}
			set { 
				 sdt.gxTpr_Groupencrypted = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtDesktopApp_services_SDT_Groups_Group_GroupItem sdt
		{
			get { 
				return (SdtDesktopApp_services_SDT_Groups_Group_GroupItem)Sdt;
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
				sdt = new SdtDesktopApp_services_SDT_Groups_Group_GroupItem() ;
			}
		}
	}
	#endregion
}