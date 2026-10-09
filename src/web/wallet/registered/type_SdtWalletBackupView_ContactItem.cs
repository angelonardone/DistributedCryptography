/*
				   File: type_SdtWalletBackupView_ContactItem
			Description: Contact
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
	[XmlRoot(ElementName="WalletBackupView.ContactItem")]
	[XmlType(TypeName="WalletBackupView.ContactItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtWalletBackupView_ContactItem : GxUserType
	{
		public SdtWalletBackupView_ContactItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtWalletBackupView_ContactItem_Contactprivatename = "";

			gxTv_SdtWalletBackupView_ContactItem_Contactusername = "";

			gxTv_SdtWalletBackupView_ContactItem_Contactuserpubkey = "";

			gxTv_SdtWalletBackupView_ContactItem_Contactinvitationsent = (DateTime)(DateTime.MinValue);

			gxTv_SdtWalletBackupView_ContactItem_Contactinvitacionaccepted = (DateTime)(DateTime.MinValue);

			gxTv_SdtWalletBackupView_ContactItem_Restoresigneddatetime = (DateTime)(DateTime.MinValue);

		}

		public SdtWalletBackupView_ContactItem(IGxContext context)
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
			AddObjectProperty("contactId", gxTpr_Contactid, false);


			AddObjectProperty("numShares", gxTpr_Numshares, false);


			AddObjectProperty("contactPrivateName", gxTpr_Contactprivatename, false);


			AddObjectProperty("contactUserName", gxTpr_Contactusername, false);


			AddObjectProperty("contactUserPubKey", gxTpr_Contactuserpubkey, false);


			datetime_STZ = gxTpr_Contactinvitationsent;
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
			AddObjectProperty("contactInvitationSent", sDateCnv, false);



			datetime_STZ = gxTpr_Contactinvitacionaccepted;
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
			AddObjectProperty("contactInvitacionAccepted", sDateCnv, false);



			AddObjectProperty("contactInviSent", gxTpr_Contactinvisent, false);


			datetime_STZ = gxTpr_Restoresigneddatetime;
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
			AddObjectProperty("restoreSignedDateTime", sDateCnv, false);


			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="contactId")]
		[XmlElement(ElementName="contactId")]
		public Guid gxTpr_Contactid
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Contactid; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactid = value;
				SetDirty("Contactid");
			}
		}




		[SoapElement(ElementName="numShares")]
		[XmlElement(ElementName="numShares")]
		public short gxTpr_Numshares
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Numshares; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Numshares = value;
				SetDirty("Numshares");
			}
		}




		[SoapElement(ElementName="contactPrivateName")]
		[XmlElement(ElementName="contactPrivateName")]
		public string gxTpr_Contactprivatename
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Contactprivatename; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactprivatename = value;
				SetDirty("Contactprivatename");
			}
		}




		[SoapElement(ElementName="contactUserName")]
		[XmlElement(ElementName="contactUserName")]
		public string gxTpr_Contactusername
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Contactusername; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactusername = value;
				SetDirty("Contactusername");
			}
		}




		[SoapElement(ElementName="contactUserPubKey")]
		[XmlElement(ElementName="contactUserPubKey")]
		public string gxTpr_Contactuserpubkey
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Contactuserpubkey; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactuserpubkey = value;
				SetDirty("Contactuserpubkey");
			}
		}



		[SoapElement(ElementName="contactInvitationSent")]
		[XmlElement(ElementName="contactInvitationSent" , IsNullable=true)]
		public string gxTpr_Contactinvitationsent_Nullable
		{
			get {
				if ( gxTv_SdtWalletBackupView_ContactItem_Contactinvitationsent == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtWalletBackupView_ContactItem_Contactinvitationsent).value ;
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactinvitationsent = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Contactinvitationsent
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Contactinvitationsent; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactinvitationsent = value;
				SetDirty("Contactinvitationsent");
			}
		}


		[SoapElement(ElementName="contactInvitacionAccepted")]
		[XmlElement(ElementName="contactInvitacionAccepted" , IsNullable=true)]
		public string gxTpr_Contactinvitacionaccepted_Nullable
		{
			get {
				if ( gxTv_SdtWalletBackupView_ContactItem_Contactinvitacionaccepted == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtWalletBackupView_ContactItem_Contactinvitacionaccepted).value ;
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactinvitacionaccepted = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Contactinvitacionaccepted
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Contactinvitacionaccepted; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactinvitacionaccepted = value;
				SetDirty("Contactinvitacionaccepted");
			}
		}



		[SoapElement(ElementName="contactInviSent")]
		[XmlElement(ElementName="contactInviSent")]
		public bool gxTpr_Contactinvisent
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Contactinvisent; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Contactinvisent = value;
				SetDirty("Contactinvisent");
			}
		}



		[SoapElement(ElementName="restoreSignedDateTime")]
		[XmlElement(ElementName="restoreSignedDateTime" , IsNullable=true)]
		public string gxTpr_Restoresigneddatetime_Nullable
		{
			get {
				if ( gxTv_SdtWalletBackupView_ContactItem_Restoresigneddatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtWalletBackupView_ContactItem_Restoresigneddatetime).value ;
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Restoresigneddatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Restoresigneddatetime
		{
			get {
				return gxTv_SdtWalletBackupView_ContactItem_Restoresigneddatetime; 
			}
			set {
				gxTv_SdtWalletBackupView_ContactItem_Restoresigneddatetime = value;
				SetDirty("Restoresigneddatetime");
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
			gxTv_SdtWalletBackupView_ContactItem_Contactprivatename = "";
			gxTv_SdtWalletBackupView_ContactItem_Contactusername = "";
			gxTv_SdtWalletBackupView_ContactItem_Contactuserpubkey = "";
			gxTv_SdtWalletBackupView_ContactItem_Contactinvitationsent = (DateTime)(DateTime.MinValue);
			gxTv_SdtWalletBackupView_ContactItem_Contactinvitacionaccepted = (DateTime)(DateTime.MinValue);

			gxTv_SdtWalletBackupView_ContactItem_Restoresigneddatetime = (DateTime)(DateTime.MinValue);
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

		protected Guid gxTv_SdtWalletBackupView_ContactItem_Contactid;
		 

		protected short gxTv_SdtWalletBackupView_ContactItem_Numshares;
		 

		protected string gxTv_SdtWalletBackupView_ContactItem_Contactprivatename;
		 

		protected string gxTv_SdtWalletBackupView_ContactItem_Contactusername;
		 

		protected string gxTv_SdtWalletBackupView_ContactItem_Contactuserpubkey;
		 

		protected DateTime gxTv_SdtWalletBackupView_ContactItem_Contactinvitationsent;
		 

		protected DateTime gxTv_SdtWalletBackupView_ContactItem_Contactinvitacionaccepted;
		 

		protected bool gxTv_SdtWalletBackupView_ContactItem_Contactinvisent;
		 

		protected DateTime gxTv_SdtWalletBackupView_ContactItem_Restoresigneddatetime;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"WalletBackupView.ContactItem", Namespace="distributedcryptography")]
	public class SdtWalletBackupView_ContactItem_RESTInterface : GxGenericCollectionItem<SdtWalletBackupView_ContactItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtWalletBackupView_ContactItem_RESTInterface( ) : base()
		{	
		}

		public SdtWalletBackupView_ContactItem_RESTInterface( SdtWalletBackupView_ContactItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("contactId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="contactId", Order=0)]
		public Guid gxTpr_Contactid
		{
			get { 
				return sdt.gxTpr_Contactid;

			}
			set { 
				sdt.gxTpr_Contactid = value;
			}
		}

		[JsonPropertyName("numShares")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="numShares", Order=1)]
		public short gxTpr_Numshares
		{
			get { 
				return sdt.gxTpr_Numshares;

			}
			set { 
				sdt.gxTpr_Numshares = value;
			}
		}

		[JsonPropertyName("contactPrivateName")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="contactPrivateName", Order=2)]
		public  string gxTpr_Contactprivatename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactprivatename);

			}
			set { 
				 sdt.gxTpr_Contactprivatename = value;
			}
		}

		[JsonPropertyName("contactUserName")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="contactUserName", Order=3)]
		public  string gxTpr_Contactusername
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactusername);

			}
			set { 
				 sdt.gxTpr_Contactusername = value;
			}
		}

		[JsonPropertyName("contactUserPubKey")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="contactUserPubKey", Order=4)]
		public  string gxTpr_Contactuserpubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactuserpubkey);

			}
			set { 
				 sdt.gxTpr_Contactuserpubkey = value;
			}
		}

		[JsonPropertyName("contactInvitationSent")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="contactInvitationSent", Order=5)]
		public  string gxTpr_Contactinvitationsent
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Contactinvitationsent,context);

			}
			set { 
				sdt.gxTpr_Contactinvitationsent = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("contactInvitacionAccepted")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="contactInvitacionAccepted", Order=6)]
		public  string gxTpr_Contactinvitacionaccepted
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Contactinvitacionaccepted,context);

			}
			set { 
				sdt.gxTpr_Contactinvitacionaccepted = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("contactInviSent")]
		[JsonPropertyOrder(7)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="contactInviSent", Order=7)]
		public bool gxTpr_Contactinvisent
		{
			get { 
				return sdt.gxTpr_Contactinvisent;

			}
			set { 
				sdt.gxTpr_Contactinvisent = value;
			}
		}

		[JsonPropertyName("restoreSignedDateTime")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="restoreSignedDateTime", Order=8)]
		public  string gxTpr_Restoresigneddatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Restoresigneddatetime,context);

			}
			set { 
				sdt.gxTpr_Restoresigneddatetime = DateTimeUtil.CToT2(value,context);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtWalletBackupView_ContactItem sdt
		{
			get { 
				return (SdtWalletBackupView_ContactItem)Sdt;
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
				sdt = new SdtWalletBackupView_ContactItem() ;
			}
		}
	}
	#endregion
}