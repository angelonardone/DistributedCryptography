/*
				   File: type_SdtGroupListItem
			Description: GroupListItem
				 Author: Nemo 🐠 for C# (.NET) version 18.0.16.189595
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

using GeneXus.Programs;
using GeneXus.Programs.wallet;

namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="GroupListItem")]
	[XmlType(TypeName="GroupListItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupListItem : GxUserType
	{
		public SdtGroupListItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroupListItem_Groupname = "";


		}

		public SdtGroupListItem(IGxContext context)
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
			AddObjectProperty("groupId", gxTpr_Groupid, false);


			AddObjectProperty("referenceGroupId", gxTpr_Referencegroupid, false);


			AddObjectProperty("groupName", gxTpr_Groupname, false);


			AddObjectProperty("groupType", gxTpr_Grouptype, false);


			AddObjectProperty("subGroupType", gxTpr_Subgrouptype, false);


			AddObjectProperty("amIgroupOwner", gxTpr_Amigroupowner, false);


			AddObjectProperty("isActive", gxTpr_Isactive, false);


			AddObjectProperty("bountyGroupId", gxTpr_Bountygroupid, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="groupId")]
		[XmlElement(ElementName="groupId")]
		public Guid gxTpr_Groupid
		{
			get {
				return gxTv_SdtGroupListItem_Groupid; 
			}
			set {
				gxTv_SdtGroupListItem_Groupid = value;
				SetDirty("Groupid");
			}
		}




		[SoapElement(ElementName="referenceGroupId")]
		[XmlElement(ElementName="referenceGroupId")]
		public Guid gxTpr_Referencegroupid
		{
			get {
				return gxTv_SdtGroupListItem_Referencegroupid; 
			}
			set {
				gxTv_SdtGroupListItem_Referencegroupid = value;
				SetDirty("Referencegroupid");
			}
		}




		[SoapElement(ElementName="groupName")]
		[XmlElement(ElementName="groupName")]
		public string gxTpr_Groupname
		{
			get {
				return gxTv_SdtGroupListItem_Groupname; 
			}
			set {
				gxTv_SdtGroupListItem_Groupname = value;
				SetDirty("Groupname");
			}
		}




		[SoapElement(ElementName="groupType")]
		[XmlElement(ElementName="groupType")]
		public short gxTpr_Grouptype
		{
			get {
				return gxTv_SdtGroupListItem_Grouptype; 
			}
			set {
				gxTv_SdtGroupListItem_Grouptype = value;
				SetDirty("Grouptype");
			}
		}




		[SoapElement(ElementName="subGroupType")]
		[XmlElement(ElementName="subGroupType")]
		public short gxTpr_Subgrouptype
		{
			get {
				return gxTv_SdtGroupListItem_Subgrouptype; 
			}
			set {
				gxTv_SdtGroupListItem_Subgrouptype = value;
				SetDirty("Subgrouptype");
			}
		}




		[SoapElement(ElementName="amIgroupOwner")]
		[XmlElement(ElementName="amIgroupOwner")]
		public bool gxTpr_Amigroupowner
		{
			get {
				return gxTv_SdtGroupListItem_Amigroupowner; 
			}
			set {
				gxTv_SdtGroupListItem_Amigroupowner = value;
				SetDirty("Amigroupowner");
			}
		}




		[SoapElement(ElementName="isActive")]
		[XmlElement(ElementName="isActive")]
		public bool gxTpr_Isactive
		{
			get {
				return gxTv_SdtGroupListItem_Isactive; 
			}
			set {
				gxTv_SdtGroupListItem_Isactive = value;
				SetDirty("Isactive");
			}
		}




		[SoapElement(ElementName="bountyGroupId")]
		[XmlElement(ElementName="bountyGroupId")]
		public Guid gxTpr_Bountygroupid
		{
			get {
				return gxTv_SdtGroupListItem_Bountygroupid; 
			}
			set {
				gxTv_SdtGroupListItem_Bountygroupid = value;
				SetDirty("Bountygroupid");
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
			gxTv_SdtGroupListItem_Groupname = "";





			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtGroupListItem_Groupid;
		 

		protected Guid gxTv_SdtGroupListItem_Referencegroupid;
		 

		protected string gxTv_SdtGroupListItem_Groupname;
		 

		protected short gxTv_SdtGroupListItem_Grouptype;
		 

		protected short gxTv_SdtGroupListItem_Subgrouptype;
		 

		protected bool gxTv_SdtGroupListItem_Amigroupowner;
		 

		protected bool gxTv_SdtGroupListItem_Isactive;
		 

		protected Guid gxTv_SdtGroupListItem_Bountygroupid;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GroupListItem", Namespace="distributedcryptography")]
	public class SdtGroupListItem_RESTInterface : GxGenericCollectionItem<SdtGroupListItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupListItem_RESTInterface( ) : base()
		{	
		}

		public SdtGroupListItem_RESTInterface( SdtGroupListItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("groupId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="groupId", Order=0)]
		public Guid gxTpr_Groupid
		{
			get { 
				return sdt.gxTpr_Groupid;

			}
			set { 
				sdt.gxTpr_Groupid = value;
			}
		}

		[JsonPropertyName("referenceGroupId")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="referenceGroupId", Order=1)]
		public Guid gxTpr_Referencegroupid
		{
			get { 
				return sdt.gxTpr_Referencegroupid;

			}
			set { 
				sdt.gxTpr_Referencegroupid = value;
			}
		}

		[JsonPropertyName("groupName")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="groupName", Order=2)]
		public  string gxTpr_Groupname
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Groupname);

			}
			set { 
				 sdt.gxTpr_Groupname = value;
			}
		}

		[JsonPropertyName("groupType")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="groupType", Order=3)]
		public short gxTpr_Grouptype
		{
			get { 
				return sdt.gxTpr_Grouptype;

			}
			set { 
				sdt.gxTpr_Grouptype = value;
			}
		}

		[JsonPropertyName("subGroupType")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="subGroupType", Order=4)]
		public short gxTpr_Subgrouptype
		{
			get { 
				return sdt.gxTpr_Subgrouptype;

			}
			set { 
				sdt.gxTpr_Subgrouptype = value;
			}
		}

		[JsonPropertyName("amIgroupOwner")]
		[JsonPropertyOrder(5)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="amIgroupOwner", Order=5)]
		public bool gxTpr_Amigroupowner
		{
			get { 
				return sdt.gxTpr_Amigroupowner;

			}
			set { 
				sdt.gxTpr_Amigroupowner = value;
			}
		}

		[JsonPropertyName("isActive")]
		[JsonPropertyOrder(6)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isActive", Order=6)]
		public bool gxTpr_Isactive
		{
			get { 
				return sdt.gxTpr_Isactive;

			}
			set { 
				sdt.gxTpr_Isactive = value;
			}
		}

		[JsonPropertyName("bountyGroupId")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="bountyGroupId", Order=7)]
		public Guid gxTpr_Bountygroupid
		{
			get { 
				return sdt.gxTpr_Bountygroupid;

			}
			set { 
				sdt.gxTpr_Bountygroupid = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroupListItem sdt
		{
			get { 
				return (SdtGroupListItem)Sdt;
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
				sdt = new SdtGroupListItem() ;
			}
		}
	}
	#endregion
}