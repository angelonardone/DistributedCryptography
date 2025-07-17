/*
				   File: type_SdtGroup_SDT
			Description: Group_SDT
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

using GeneXus.Programs;
using GeneXus.Programs.wallet;
namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="Group_SDT")]
	[XmlType(TypeName="Group_SDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroup_SDT : GxUserType
	{
		public SdtGroup_SDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroup_SDT_Groupname = "";

			gxTv_SdtGroup_SDT_Encpassword = "";

			gxTv_SdtGroup_SDT_Cleartextshare = "";

			gxTv_SdtGroup_SDT_Encryptedtextshare = "";

			gxTv_SdtGroup_SDT_Extpubkeymultisigreceiving = "";

			gxTv_SdtGroup_SDT_Extpubkeymultisigchange = "";

			gxTv_SdtGroup_SDT_Extpubkeytimebountyreceiving = "";

		}

		public SdtGroup_SDT(IGxContext context)
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


			AddObjectProperty("groupType", gxTpr_Grouptype, false);


			AddObjectProperty("groupName", gxTpr_Groupname, false);


			AddObjectProperty("amIgroupOwner", gxTpr_Amigroupowner, false);


			AddObjectProperty("isActive", gxTpr_Isactive, false);


			AddObjectProperty("minimumShares", gxTpr_Minimumshares, false);


			AddObjectProperty("encPassword", gxTpr_Encpassword, false);


			AddObjectProperty("clearTextShare", gxTpr_Cleartextshare, false);


			AddObjectProperty("encryptedTextShare", gxTpr_Encryptedtextshare, false);


			AddObjectProperty("numOfSharesReached", gxTpr_Numofsharesreached, false);


			AddObjectProperty("extPubKeyMultiSigReceiving", gxTpr_Extpubkeymultisigreceiving, false);


			AddObjectProperty("extPubKeyMultiSigChange", gxTpr_Extpubkeymultisigchange, false);


			AddObjectProperty("subGroupType", gxTpr_Subgrouptype, false);


			AddObjectProperty("bountyGroupId", gxTpr_Bountygroupid, false);


			AddObjectProperty("dataGroupId", gxTpr_Datagroupid, false);


			AddObjectProperty("extPubKeyTimeBountyReceiving", gxTpr_Extpubkeytimebountyreceiving, false);

			if (gxTv_SdtGroup_SDT_Timeconstrain != null)
			{
				AddObjectProperty("TimeConstrain", gxTv_SdtGroup_SDT_Timeconstrain, false);
			}
			if (gxTv_SdtGroup_SDT_Contact != null)
			{
				AddObjectProperty("Contact", gxTv_SdtGroup_SDT_Contact, false);
			}
			if (gxTv_SdtGroup_SDT_Othergroup != null)
			{
				AddObjectProperty("otherGroup", gxTv_SdtGroup_SDT_Othergroup, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="groupId")]
		[XmlElement(ElementName="groupId")]
		public Guid gxTpr_Groupid
		{
			get {
				return gxTv_SdtGroup_SDT_Groupid; 
			}
			set {
				gxTv_SdtGroup_SDT_Groupid = value;
				SetDirty("Groupid");
			}
		}




		[SoapElement(ElementName="groupType")]
		[XmlElement(ElementName="groupType")]
		public short gxTpr_Grouptype
		{
			get {
				return gxTv_SdtGroup_SDT_Grouptype; 
			}
			set {
				gxTv_SdtGroup_SDT_Grouptype = value;
				SetDirty("Grouptype");
			}
		}




		[SoapElement(ElementName="groupName")]
		[XmlElement(ElementName="groupName")]
		public string gxTpr_Groupname
		{
			get {
				return gxTv_SdtGroup_SDT_Groupname; 
			}
			set {
				gxTv_SdtGroup_SDT_Groupname = value;
				SetDirty("Groupname");
			}
		}




		[SoapElement(ElementName="amIgroupOwner")]
		[XmlElement(ElementName="amIgroupOwner")]
		public bool gxTpr_Amigroupowner
		{
			get {
				return gxTv_SdtGroup_SDT_Amigroupowner; 
			}
			set {
				gxTv_SdtGroup_SDT_Amigroupowner = value;
				SetDirty("Amigroupowner");
			}
		}




		[SoapElement(ElementName="isActive")]
		[XmlElement(ElementName="isActive")]
		public bool gxTpr_Isactive
		{
			get {
				return gxTv_SdtGroup_SDT_Isactive; 
			}
			set {
				gxTv_SdtGroup_SDT_Isactive = value;
				SetDirty("Isactive");
			}
		}




		[SoapElement(ElementName="minimumShares")]
		[XmlElement(ElementName="minimumShares")]
		public short gxTpr_Minimumshares
		{
			get {
				return gxTv_SdtGroup_SDT_Minimumshares; 
			}
			set {
				gxTv_SdtGroup_SDT_Minimumshares = value;
				SetDirty("Minimumshares");
			}
		}




		[SoapElement(ElementName="encPassword")]
		[XmlElement(ElementName="encPassword")]
		public string gxTpr_Encpassword
		{
			get {
				return gxTv_SdtGroup_SDT_Encpassword; 
			}
			set {
				gxTv_SdtGroup_SDT_Encpassword = value;
				SetDirty("Encpassword");
			}
		}




		[SoapElement(ElementName="clearTextShare")]
		[XmlElement(ElementName="clearTextShare")]
		public string gxTpr_Cleartextshare
		{
			get {
				return gxTv_SdtGroup_SDT_Cleartextshare; 
			}
			set {
				gxTv_SdtGroup_SDT_Cleartextshare = value;
				SetDirty("Cleartextshare");
			}
		}




		[SoapElement(ElementName="encryptedTextShare")]
		[XmlElement(ElementName="encryptedTextShare")]
		public string gxTpr_Encryptedtextshare
		{
			get {
				return gxTv_SdtGroup_SDT_Encryptedtextshare; 
			}
			set {
				gxTv_SdtGroup_SDT_Encryptedtextshare = value;
				SetDirty("Encryptedtextshare");
			}
		}




		[SoapElement(ElementName="numOfSharesReached")]
		[XmlElement(ElementName="numOfSharesReached")]
		public bool gxTpr_Numofsharesreached
		{
			get {
				return gxTv_SdtGroup_SDT_Numofsharesreached; 
			}
			set {
				gxTv_SdtGroup_SDT_Numofsharesreached = value;
				SetDirty("Numofsharesreached");
			}
		}




		[SoapElement(ElementName="extPubKeyMultiSigReceiving")]
		[XmlElement(ElementName="extPubKeyMultiSigReceiving")]
		public string gxTpr_Extpubkeymultisigreceiving
		{
			get {
				return gxTv_SdtGroup_SDT_Extpubkeymultisigreceiving; 
			}
			set {
				gxTv_SdtGroup_SDT_Extpubkeymultisigreceiving = value;
				SetDirty("Extpubkeymultisigreceiving");
			}
		}




		[SoapElement(ElementName="extPubKeyMultiSigChange")]
		[XmlElement(ElementName="extPubKeyMultiSigChange")]
		public string gxTpr_Extpubkeymultisigchange
		{
			get {
				return gxTv_SdtGroup_SDT_Extpubkeymultisigchange; 
			}
			set {
				gxTv_SdtGroup_SDT_Extpubkeymultisigchange = value;
				SetDirty("Extpubkeymultisigchange");
			}
		}




		[SoapElement(ElementName="subGroupType")]
		[XmlElement(ElementName="subGroupType")]
		public short gxTpr_Subgrouptype
		{
			get {
				return gxTv_SdtGroup_SDT_Subgrouptype; 
			}
			set {
				gxTv_SdtGroup_SDT_Subgrouptype = value;
				SetDirty("Subgrouptype");
			}
		}




		[SoapElement(ElementName="bountyGroupId")]
		[XmlElement(ElementName="bountyGroupId")]
		public Guid gxTpr_Bountygroupid
		{
			get {
				return gxTv_SdtGroup_SDT_Bountygroupid; 
			}
			set {
				gxTv_SdtGroup_SDT_Bountygroupid = value;
				SetDirty("Bountygroupid");
			}
		}




		[SoapElement(ElementName="dataGroupId")]
		[XmlElement(ElementName="dataGroupId")]
		public Guid gxTpr_Datagroupid
		{
			get {
				return gxTv_SdtGroup_SDT_Datagroupid; 
			}
			set {
				gxTv_SdtGroup_SDT_Datagroupid = value;
				SetDirty("Datagroupid");
			}
		}




		[SoapElement(ElementName="extPubKeyTimeBountyReceiving")]
		[XmlElement(ElementName="extPubKeyTimeBountyReceiving")]
		public string gxTpr_Extpubkeytimebountyreceiving
		{
			get {
				return gxTv_SdtGroup_SDT_Extpubkeytimebountyreceiving; 
			}
			set {
				gxTv_SdtGroup_SDT_Extpubkeytimebountyreceiving = value;
				SetDirty("Extpubkeytimebountyreceiving");
			}
		}




		[SoapElement(ElementName="TimeConstrain" )]
		[XmlArray(ElementName="TimeConstrain"  )]
		[XmlArrayItemAttribute(ElementName="TimeConstrainItem" , IsNullable=false )]
		public GXBaseCollection<SdtGroup_SDT_TimeConstrainItem> gxTpr_Timeconstrain
		{
			get {
				if ( gxTv_SdtGroup_SDT_Timeconstrain == null )
				{
					gxTv_SdtGroup_SDT_Timeconstrain = new GXBaseCollection<SdtGroup_SDT_TimeConstrainItem>( context, "Group_SDT.TimeConstrainItem", "");
				}
				SetDirty("Timeconstrain");
				return gxTv_SdtGroup_SDT_Timeconstrain;
			}
			set {
				gxTv_SdtGroup_SDT_Timeconstrain_N = false;
				gxTv_SdtGroup_SDT_Timeconstrain = value;
				SetDirty("Timeconstrain");
			}
		}

		public void gxTv_SdtGroup_SDT_Timeconstrain_SetNull()
		{
			gxTv_SdtGroup_SDT_Timeconstrain_N = true;
			gxTv_SdtGroup_SDT_Timeconstrain = null;
		}

		public bool gxTv_SdtGroup_SDT_Timeconstrain_IsNull()
		{
			return gxTv_SdtGroup_SDT_Timeconstrain == null;
		}
		public bool ShouldSerializegxTpr_Timeconstrain_GxSimpleCollection_Json()
		{
			return gxTv_SdtGroup_SDT_Timeconstrain != null && gxTv_SdtGroup_SDT_Timeconstrain.Count > 0;

		}



		[SoapElement(ElementName="Contact" )]
		[XmlArray(ElementName="Contact"  )]
		[XmlArrayItemAttribute(ElementName="ContactItem" , IsNullable=false )]
		public GXBaseCollection<SdtGroup_SDT_ContactItem> gxTpr_Contact
		{
			get {
				if ( gxTv_SdtGroup_SDT_Contact == null )
				{
					gxTv_SdtGroup_SDT_Contact = new GXBaseCollection<SdtGroup_SDT_ContactItem>( context, "Group_SDT.ContactItem", "");
				}
				SetDirty("Contact");
				return gxTv_SdtGroup_SDT_Contact;
			}
			set {
				gxTv_SdtGroup_SDT_Contact_N = false;
				gxTv_SdtGroup_SDT_Contact = value;
				SetDirty("Contact");
			}
		}

		public void gxTv_SdtGroup_SDT_Contact_SetNull()
		{
			gxTv_SdtGroup_SDT_Contact_N = true;
			gxTv_SdtGroup_SDT_Contact = null;
		}

		public bool gxTv_SdtGroup_SDT_Contact_IsNull()
		{
			return gxTv_SdtGroup_SDT_Contact == null;
		}
		public bool ShouldSerializegxTpr_Contact_GxSimpleCollection_Json()
		{
			return gxTv_SdtGroup_SDT_Contact != null && gxTv_SdtGroup_SDT_Contact.Count > 0;

		}


		[SoapElement(ElementName="otherGroup" )]
		[XmlElement(ElementName="otherGroup" )]
		public SdtGroup_SDT_otherGroup gxTpr_Othergroup
		{
			get {
				if ( gxTv_SdtGroup_SDT_Othergroup == null )
				{
					gxTv_SdtGroup_SDT_Othergroup = new SdtGroup_SDT_otherGroup(context);
				}
				gxTv_SdtGroup_SDT_Othergroup_N = false;
				SetDirty("Othergroup");
				return gxTv_SdtGroup_SDT_Othergroup;
			}
			set {
				gxTv_SdtGroup_SDT_Othergroup_N = false;
				gxTv_SdtGroup_SDT_Othergroup = value;
				SetDirty("Othergroup");
			}

		}

		public void gxTv_SdtGroup_SDT_Othergroup_SetNull()
		{
			gxTv_SdtGroup_SDT_Othergroup_N = true;
			gxTv_SdtGroup_SDT_Othergroup = null;
		}

		public bool gxTv_SdtGroup_SDT_Othergroup_IsNull()
		{
			return gxTv_SdtGroup_SDT_Othergroup == null;
		}
		public bool ShouldSerializegxTpr_Othergroup_Json()
		{
				return (gxTv_SdtGroup_SDT_Othergroup != null && gxTv_SdtGroup_SDT_Othergroup.ShouldSerializeSdtJson());

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
			gxTv_SdtGroup_SDT_Groupname = "";



			gxTv_SdtGroup_SDT_Encpassword = "";
			gxTv_SdtGroup_SDT_Cleartextshare = "";
			gxTv_SdtGroup_SDT_Encryptedtextshare = "";

			gxTv_SdtGroup_SDT_Extpubkeymultisigreceiving = "";
			gxTv_SdtGroup_SDT_Extpubkeymultisigchange = "";



			gxTv_SdtGroup_SDT_Extpubkeytimebountyreceiving = "";

			gxTv_SdtGroup_SDT_Timeconstrain_N = true;


			gxTv_SdtGroup_SDT_Contact_N = true;


			gxTv_SdtGroup_SDT_Othergroup_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtGroup_SDT_Groupid;
		 

		protected short gxTv_SdtGroup_SDT_Grouptype;
		 

		protected string gxTv_SdtGroup_SDT_Groupname;
		 

		protected bool gxTv_SdtGroup_SDT_Amigroupowner;
		 

		protected bool gxTv_SdtGroup_SDT_Isactive;
		 

		protected short gxTv_SdtGroup_SDT_Minimumshares;
		 

		protected string gxTv_SdtGroup_SDT_Encpassword;
		 

		protected string gxTv_SdtGroup_SDT_Cleartextshare;
		 

		protected string gxTv_SdtGroup_SDT_Encryptedtextshare;
		 

		protected bool gxTv_SdtGroup_SDT_Numofsharesreached;
		 

		protected string gxTv_SdtGroup_SDT_Extpubkeymultisigreceiving;
		 

		protected string gxTv_SdtGroup_SDT_Extpubkeymultisigchange;
		 

		protected short gxTv_SdtGroup_SDT_Subgrouptype;
		 

		protected Guid gxTv_SdtGroup_SDT_Bountygroupid;
		 

		protected Guid gxTv_SdtGroup_SDT_Datagroupid;
		 

		protected string gxTv_SdtGroup_SDT_Extpubkeytimebountyreceiving;
		 
		protected bool gxTv_SdtGroup_SDT_Timeconstrain_N;
		protected GXBaseCollection<SdtGroup_SDT_TimeConstrainItem> gxTv_SdtGroup_SDT_Timeconstrain = null; 

		protected bool gxTv_SdtGroup_SDT_Contact_N;
		protected GXBaseCollection<SdtGroup_SDT_ContactItem> gxTv_SdtGroup_SDT_Contact = null; 

		protected bool gxTv_SdtGroup_SDT_Othergroup_N;
		protected SdtGroup_SDT_otherGroup gxTv_SdtGroup_SDT_Othergroup = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Group_SDT", Namespace="distributedcryptography")]
	public class SdtGroup_SDT_RESTInterface : GxGenericCollectionItem<SdtGroup_SDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroup_SDT_RESTInterface( ) : base()
		{	
		}

		public SdtGroup_SDT_RESTInterface( SdtGroup_SDT psdt ) : base(psdt)
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

		[JsonPropertyName("groupType")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="groupType", Order=1)]
		public short gxTpr_Grouptype
		{
			get { 
				return sdt.gxTpr_Grouptype;

			}
			set { 
				sdt.gxTpr_Grouptype = value;
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

		[JsonPropertyName("amIgroupOwner")]
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="amIgroupOwner", Order=3)]
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
		[JsonPropertyOrder(4)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isActive", Order=4)]
		public bool gxTpr_Isactive
		{
			get { 
				return sdt.gxTpr_Isactive;

			}
			set { 
				sdt.gxTpr_Isactive = value;
			}
		}

		[JsonPropertyName("minimumShares")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="minimumShares", Order=5)]
		public short gxTpr_Minimumshares
		{
			get { 
				return sdt.gxTpr_Minimumshares;

			}
			set { 
				sdt.gxTpr_Minimumshares = value;
			}
		}

		[JsonPropertyName("encPassword")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="encPassword", Order=6)]
		public  string gxTpr_Encpassword
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encpassword);

			}
			set { 
				 sdt.gxTpr_Encpassword = value;
			}
		}

		[JsonPropertyName("clearTextShare")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="clearTextShare", Order=7)]
		public  string gxTpr_Cleartextshare
		{
			get { 
				return sdt.gxTpr_Cleartextshare;

			}
			set { 
				 sdt.gxTpr_Cleartextshare = value;
			}
		}

		[JsonPropertyName("encryptedTextShare")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="encryptedTextShare", Order=8)]
		public  string gxTpr_Encryptedtextshare
		{
			get { 
				return sdt.gxTpr_Encryptedtextshare;

			}
			set { 
				 sdt.gxTpr_Encryptedtextshare = value;
			}
		}

		[JsonPropertyName("numOfSharesReached")]
		[JsonPropertyOrder(9)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="numOfSharesReached", Order=9)]
		public bool gxTpr_Numofsharesreached
		{
			get { 
				return sdt.gxTpr_Numofsharesreached;

			}
			set { 
				sdt.gxTpr_Numofsharesreached = value;
			}
		}

		[JsonPropertyName("extPubKeyMultiSigReceiving")]
		[JsonPropertyOrder(10)]
		[DataMember(Name="extPubKeyMultiSigReceiving", Order=10)]
		public  string gxTpr_Extpubkeymultisigreceiving
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeymultisigreceiving);

			}
			set { 
				 sdt.gxTpr_Extpubkeymultisigreceiving = value;
			}
		}

		[JsonPropertyName("extPubKeyMultiSigChange")]
		[JsonPropertyOrder(11)]
		[DataMember(Name="extPubKeyMultiSigChange", Order=11)]
		public  string gxTpr_Extpubkeymultisigchange
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeymultisigchange);

			}
			set { 
				 sdt.gxTpr_Extpubkeymultisigchange = value;
			}
		}

		[JsonPropertyName("subGroupType")]
		[JsonPropertyOrder(12)]
		[DataMember(Name="subGroupType", Order=12)]
		public short gxTpr_Subgrouptype
		{
			get { 
				return sdt.gxTpr_Subgrouptype;

			}
			set { 
				sdt.gxTpr_Subgrouptype = value;
			}
		}

		[JsonPropertyName("bountyGroupId")]
		[JsonPropertyOrder(13)]
		[DataMember(Name="bountyGroupId", Order=13)]
		public Guid gxTpr_Bountygroupid
		{
			get { 
				return sdt.gxTpr_Bountygroupid;

			}
			set { 
				sdt.gxTpr_Bountygroupid = value;
			}
		}

		[JsonPropertyName("dataGroupId")]
		[JsonPropertyOrder(14)]
		[DataMember(Name="dataGroupId", Order=14)]
		public Guid gxTpr_Datagroupid
		{
			get { 
				return sdt.gxTpr_Datagroupid;

			}
			set { 
				sdt.gxTpr_Datagroupid = value;
			}
		}

		[JsonPropertyName("extPubKeyTimeBountyReceiving")]
		[JsonPropertyOrder(15)]
		[DataMember(Name="extPubKeyTimeBountyReceiving", Order=15)]
		public  string gxTpr_Extpubkeytimebountyreceiving
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeytimebountyreceiving);

			}
			set { 
				 sdt.gxTpr_Extpubkeytimebountyreceiving = value;
			}
		}

		[JsonPropertyName("TimeConstrain")]
		[JsonPropertyOrder(16)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="TimeConstrain", Order=16, EmitDefaultValue=false)]
		public GxGenericCollection<SdtGroup_SDT_TimeConstrainItem_RESTInterface> gxTpr_Timeconstrain
		{
			get {
				if (sdt.ShouldSerializegxTpr_Timeconstrain_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtGroup_SDT_TimeConstrainItem_RESTInterface>(sdt.gxTpr_Timeconstrain);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Timeconstrain);
			}
		}

		[JsonPropertyName("Contact")]
		[JsonPropertyOrder(17)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Contact", Order=17, EmitDefaultValue=false)]
		public GxGenericCollection<SdtGroup_SDT_ContactItem_RESTInterface> gxTpr_Contact
		{
			get {
				if (sdt.ShouldSerializegxTpr_Contact_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtGroup_SDT_ContactItem_RESTInterface>(sdt.gxTpr_Contact);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Contact);
			}
		}

		[JsonPropertyName("otherGroup")]
		[JsonPropertyOrder(18)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="otherGroup", Order=18, EmitDefaultValue=false)]
		public SdtGroup_SDT_otherGroup_RESTInterface gxTpr_Othergroup
		{
			get {
				if (sdt.ShouldSerializegxTpr_Othergroup_Json())
					return new SdtGroup_SDT_otherGroup_RESTInterface(sdt.gxTpr_Othergroup);
				else
					return null;

			}

			set {
				sdt.gxTpr_Othergroup = value.sdt;
			}

		}


		#endregion
		[JsonIgnore]
		public SdtGroup_SDT sdt
		{
			get { 
				return (SdtGroup_SDT)Sdt;
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
				sdt = new SdtGroup_SDT() ;
			}
		}
	}
	#endregion
}