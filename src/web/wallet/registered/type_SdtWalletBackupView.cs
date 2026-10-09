/*
				   File: type_SdtWalletBackupView
			Description: WalletBackupView
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
	[XmlRoot(ElementName="WalletBackupView")]
	[XmlType(TypeName="WalletBackupView" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtWalletBackupView : GxUserType
	{
		public SdtWalletBackupView( )
		{
			/* Constructor for serialization */
			gxTv_SdtWalletBackupView_Groupname = "";

			gxTv_SdtWalletBackupView_Restorestoppeddatetime = (DateTime)(DateTime.MinValue);

		}

		public SdtWalletBackupView(IGxContext context)
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


			AddObjectProperty("groupName", gxTpr_Groupname, false);


			AddObjectProperty("amIgroupOwner", gxTpr_Amigroupowner, false);


			AddObjectProperty("isActive", gxTpr_Isactive, false);


			AddObjectProperty("minimumShares", gxTpr_Minimumshares, false);


			AddObjectProperty("numOfSharesReached", gxTpr_Numofsharesreached, false);


			AddObjectProperty("restoreStopped", gxTpr_Restorestopped, false);


			datetime_STZ = gxTpr_Restorestoppeddatetime;
			sDateCnv = "";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Year(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("0000", 1, 4-StringUtil.Len( sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim( StringUtil.Str((decimal)(DateTimeUtil.Month(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Day(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "T";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Hour(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Minute(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Second(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			AddObjectProperty("restoreStoppedDateTime", sDateCnv, false);


			if (gxTv_SdtWalletBackupView_Contact != null)
			{
				AddObjectProperty("Contact", gxTv_SdtWalletBackupView_Contact, false);
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
				return gxTv_SdtWalletBackupView_Groupid; 
			}
			set {
				gxTv_SdtWalletBackupView_Groupid = value;
				SetDirty("Groupid");
			}
		}




		[SoapElement(ElementName="groupName")]
		[XmlElement(ElementName="groupName")]
		public string gxTpr_Groupname
		{
			get {
				return gxTv_SdtWalletBackupView_Groupname; 
			}
			set {
				gxTv_SdtWalletBackupView_Groupname = value;
				SetDirty("Groupname");
			}
		}




		[SoapElement(ElementName="amIgroupOwner")]
		[XmlElement(ElementName="amIgroupOwner")]
		public bool gxTpr_Amigroupowner
		{
			get {
				return gxTv_SdtWalletBackupView_Amigroupowner; 
			}
			set {
				gxTv_SdtWalletBackupView_Amigroupowner = value;
				SetDirty("Amigroupowner");
			}
		}




		[SoapElement(ElementName="isActive")]
		[XmlElement(ElementName="isActive")]
		public bool gxTpr_Isactive
		{
			get {
				return gxTv_SdtWalletBackupView_Isactive; 
			}
			set {
				gxTv_SdtWalletBackupView_Isactive = value;
				SetDirty("Isactive");
			}
		}




		[SoapElement(ElementName="minimumShares")]
		[XmlElement(ElementName="minimumShares")]
		public short gxTpr_Minimumshares
		{
			get {
				return gxTv_SdtWalletBackupView_Minimumshares; 
			}
			set {
				gxTv_SdtWalletBackupView_Minimumshares = value;
				SetDirty("Minimumshares");
			}
		}




		[SoapElement(ElementName="numOfSharesReached")]
		[XmlElement(ElementName="numOfSharesReached")]
		public bool gxTpr_Numofsharesreached
		{
			get {
				return gxTv_SdtWalletBackupView_Numofsharesreached; 
			}
			set {
				gxTv_SdtWalletBackupView_Numofsharesreached = value;
				SetDirty("Numofsharesreached");
			}
		}




		[SoapElement(ElementName="restoreStopped")]
		[XmlElement(ElementName="restoreStopped")]
		public bool gxTpr_Restorestopped
		{
			get {
				return gxTv_SdtWalletBackupView_Restorestopped; 
			}
			set {
				gxTv_SdtWalletBackupView_Restorestopped = value;
				SetDirty("Restorestopped");
			}
		}



		[SoapElement(ElementName="restoreStoppedDateTime")]
		[XmlElement(ElementName="restoreStoppedDateTime" , IsNullable=true)]
		public string gxTpr_Restorestoppeddatetime_Nullable
		{
			get {
				if ( gxTv_SdtWalletBackupView_Restorestoppeddatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtWalletBackupView_Restorestoppeddatetime).value ;
			}
			set {
				gxTv_SdtWalletBackupView_Restorestoppeddatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Restorestoppeddatetime
		{
			get {
				return gxTv_SdtWalletBackupView_Restorestoppeddatetime; 
			}
			set {
				gxTv_SdtWalletBackupView_Restorestoppeddatetime = value;
				SetDirty("Restorestoppeddatetime");
			}
		}



		[SoapElement(ElementName="Contact" )]
		[XmlArray(ElementName="Contact"  )]
		[XmlArrayItemAttribute(ElementName="ContactItem" , IsNullable=false )]
		public GXBaseCollection<SdtWalletBackupView_ContactItem> gxTpr_Contact
		{
			get {
				if ( gxTv_SdtWalletBackupView_Contact == null )
				{
					gxTv_SdtWalletBackupView_Contact = new GXBaseCollection<SdtWalletBackupView_ContactItem>( context, "WalletBackupView.ContactItem", "");
				}
				SetDirty("Contact");
				return gxTv_SdtWalletBackupView_Contact;
			}
			set {
				gxTv_SdtWalletBackupView_Contact_N = false;
				gxTv_SdtWalletBackupView_Contact = value;
				SetDirty("Contact");
			}
		}

		public void gxTv_SdtWalletBackupView_Contact_SetNull()
		{
			gxTv_SdtWalletBackupView_Contact_N = true;
			gxTv_SdtWalletBackupView_Contact = null;
		}

		public bool gxTv_SdtWalletBackupView_Contact_IsNull()
		{
			return gxTv_SdtWalletBackupView_Contact == null;
		}
		public bool ShouldSerializegxTpr_Contact_GxSimpleCollection_Json()
		{
			return gxTv_SdtWalletBackupView_Contact != null && gxTv_SdtWalletBackupView_Contact.Count > 0;

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
			gxTv_SdtWalletBackupView_Groupname = "";





			gxTv_SdtWalletBackupView_Restorestoppeddatetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtWalletBackupView_Contact_N = true;

			datetime_STZ = (DateTime)(DateTime.MinValue);
			sDateCnv = "";
			sNumToPad = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string sDateCnv ;
		protected string sNumToPad ;
		protected DateTime datetime_STZ ;

		protected Guid gxTv_SdtWalletBackupView_Groupid;
		 

		protected string gxTv_SdtWalletBackupView_Groupname;
		 

		protected bool gxTv_SdtWalletBackupView_Amigroupowner;
		 

		protected bool gxTv_SdtWalletBackupView_Isactive;
		 

		protected short gxTv_SdtWalletBackupView_Minimumshares;
		 

		protected bool gxTv_SdtWalletBackupView_Numofsharesreached;
		 

		protected bool gxTv_SdtWalletBackupView_Restorestopped;
		 

		protected DateTime gxTv_SdtWalletBackupView_Restorestoppeddatetime;
		 
		protected bool gxTv_SdtWalletBackupView_Contact_N;
		protected GXBaseCollection<SdtWalletBackupView_ContactItem> gxTv_SdtWalletBackupView_Contact = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"WalletBackupView", Namespace="distributedcryptography")]
	public class SdtWalletBackupView_RESTInterface : GxGenericCollectionItem<SdtWalletBackupView>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWalletBackupView_RESTInterface( ) : base()
		{	
		}

		public SdtWalletBackupView_RESTInterface( SdtWalletBackupView psdt ) : base(psdt)
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

		[JsonPropertyName("groupName")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="groupName", Order=1)]
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
		[JsonPropertyOrder(2)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="amIgroupOwner", Order=2)]
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
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isActive", Order=3)]
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
		[JsonPropertyOrder(4)]
		[DataMember(Name="minimumShares", Order=4)]
		public short gxTpr_Minimumshares
		{
			get { 
				return sdt.gxTpr_Minimumshares;

			}
			set { 
				sdt.gxTpr_Minimumshares = value;
			}
		}

		[JsonPropertyName("numOfSharesReached")]
		[JsonPropertyOrder(5)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="numOfSharesReached", Order=5)]
		public bool gxTpr_Numofsharesreached
		{
			get { 
				return sdt.gxTpr_Numofsharesreached;

			}
			set { 
				sdt.gxTpr_Numofsharesreached = value;
			}
		}

		[JsonPropertyName("restoreStopped")]
		[JsonPropertyOrder(6)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="restoreStopped", Order=6)]
		public bool gxTpr_Restorestopped
		{
			get { 
				return sdt.gxTpr_Restorestopped;

			}
			set { 
				sdt.gxTpr_Restorestopped = value;
			}
		}

		[JsonPropertyName("restoreStoppedDateTime")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="restoreStoppedDateTime", Order=7)]
		public  string gxTpr_Restorestoppeddatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Restorestoppeddatetime,context);

			}
			set { 
				sdt.gxTpr_Restorestoppeddatetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("Contact")]
		[JsonPropertyOrder(8)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="Contact", Order=8, EmitDefaultValue=false)]
		public GxGenericCollection<SdtWalletBackupView_ContactItem_RESTInterface> gxTpr_Contact
		{
			get {
				if (sdt.ShouldSerializegxTpr_Contact_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtWalletBackupView_ContactItem_RESTInterface>(sdt.gxTpr_Contact);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Contact);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtWalletBackupView sdt
		{
			get { 
				return (SdtWalletBackupView)Sdt;
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
				sdt = new SdtWalletBackupView() ;
			}
		}
	}
	#endregion
}