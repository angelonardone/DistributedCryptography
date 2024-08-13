/*
				   File: type_SdtGroup_SDT
			Description: Group_SDT
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

			gxTv_SdtGroup_SDT_Extpubkeymultisigreceiving = "";

			gxTv_SdtGroup_SDT_Extpubkeymultisigchange = "";

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


			AddObjectProperty("numOfSharesReached", gxTpr_Numofsharesreached, false);


			AddObjectProperty("extPubKeyMultiSigReceiving", gxTpr_Extpubkeymultisigreceiving, false);


			AddObjectProperty("extPubKeyMultiSigChange", gxTpr_Extpubkeymultisigchange, false);

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

			gxTv_SdtGroup_SDT_Extpubkeymultisigreceiving = "";
			gxTv_SdtGroup_SDT_Extpubkeymultisigchange = "";

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
		 

		protected bool gxTv_SdtGroup_SDT_Numofsharesreached;
		 

		protected string gxTv_SdtGroup_SDT_Extpubkeymultisigreceiving;
		 

		protected string gxTv_SdtGroup_SDT_Extpubkeymultisigchange;
		 
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

		[DataMember(Name="numOfSharesReached", Order=8)]
		public bool gxTpr_Numofsharesreached
		{
			get { 
				return sdt.gxTpr_Numofsharesreached;

			}
			set { 
				sdt.gxTpr_Numofsharesreached = value;
			}
		}

		[DataMember(Name="extPubKeyMultiSigReceiving", Order=9)]
		public  string gxTpr_Extpubkeymultisigreceiving
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeymultisigreceiving);

			}
			set { 
				 sdt.gxTpr_Extpubkeymultisigreceiving = value;
			}
		}

		[DataMember(Name="extPubKeyMultiSigChange", Order=10)]
		public  string gxTpr_Extpubkeymultisigchange
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extpubkeymultisigchange);

			}
			set { 
				 sdt.gxTpr_Extpubkeymultisigchange = value;
			}
		}

		[DataMember(Name="Contact", Order=11, EmitDefaultValue=false)]
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

		[DataMember(Name="otherGroup", Order=12, EmitDefaultValue=false)]
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