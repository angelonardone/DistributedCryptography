/*
				   File: type_SdtDesktopApp_services_SDT_Groups
			Description: DesktopApp_services_SDT_Groups
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
	[XmlRoot(ElementName="DesktopApp_services_SDT_Groups")]
	[XmlType(TypeName="DesktopApp_services_SDT_Groups" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDesktopApp_services_SDT_Groups : GxUserType
	{
		public SdtDesktopApp_services_SDT_Groups( )
		{
			/* Constructor for serialization */
		}

		public SdtDesktopApp_services_SDT_Groups(IGxContext context)
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
			if (gxTv_SdtDesktopApp_services_SDT_Groups_Group != null)
			{
				AddObjectProperty("Group", gxTv_SdtDesktopApp_services_SDT_Groups_Group, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Group" )]
		[XmlArray(ElementName="Group"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem> gxTpr_Group_GXBaseCollection
		{
			get {
				if ( gxTv_SdtDesktopApp_services_SDT_Groups_Group == null )
				{
					gxTv_SdtDesktopApp_services_SDT_Groups_Group = new GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem>( context, "DesktopApp_services_SDT_Groups_Group_GroupItem", "");
				}
				return gxTv_SdtDesktopApp_services_SDT_Groups_Group;
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Groups_Group_N = false;
				gxTv_SdtDesktopApp_services_SDT_Groups_Group = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem> gxTpr_Group
		{
			get {
				if ( gxTv_SdtDesktopApp_services_SDT_Groups_Group == null )
				{
					gxTv_SdtDesktopApp_services_SDT_Groups_Group = new GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem>( context, "DesktopApp_services_SDT_Groups_Group_GroupItem", "");
				}
				gxTv_SdtDesktopApp_services_SDT_Groups_Group_N = false;
				SetDirty("Group");
				return gxTv_SdtDesktopApp_services_SDT_Groups_Group ;
			}
			set {
				gxTv_SdtDesktopApp_services_SDT_Groups_Group_N = false;
				gxTv_SdtDesktopApp_services_SDT_Groups_Group = value;
				SetDirty("Group");
			}
		}

		public void gxTv_SdtDesktopApp_services_SDT_Groups_Group_SetNull()
		{
			gxTv_SdtDesktopApp_services_SDT_Groups_Group_N = true;
			gxTv_SdtDesktopApp_services_SDT_Groups_Group = null;
		}

		public bool gxTv_SdtDesktopApp_services_SDT_Groups_Group_IsNull()
		{
			return gxTv_SdtDesktopApp_services_SDT_Groups_Group == null;
		}
		public bool ShouldSerializegxTpr_Group_GXBaseCollection_Json()
		{
			return gxTv_SdtDesktopApp_services_SDT_Groups_Group != null && gxTv_SdtDesktopApp_services_SDT_Groups_Group.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return (
				 ShouldSerializegxTpr_Group_GXBaseCollection_Json()||  
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
			gxTv_SdtDesktopApp_services_SDT_Groups_Group_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtDesktopApp_services_SDT_Groups_Group_N;
		protected GXBaseCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem> gxTv_SdtDesktopApp_services_SDT_Groups_Group = null;  


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"DesktopApp_services_SDT_Groups", Namespace="distributedcryptography")]
	public class SdtDesktopApp_services_SDT_Groups_RESTInterface : GxGenericCollectionItem<SdtDesktopApp_services_SDT_Groups>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDesktopApp_services_SDT_Groups_RESTInterface( ) : base()
		{	
		}

		public SdtDesktopApp_services_SDT_Groups_RESTInterface( SdtDesktopApp_services_SDT_Groups psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Group")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Group", Order=0, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem_RESTInterface> gxTpr_Group
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Group_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem_RESTInterface>(sdt.gxTpr_Group);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Group);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtDesktopApp_services_SDT_Groups sdt
		{
			get { 
				return (SdtDesktopApp_services_SDT_Groups)Sdt;
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
				sdt = new SdtDesktopApp_services_SDT_Groups() ;
			}
		}
	}
	#endregion
}