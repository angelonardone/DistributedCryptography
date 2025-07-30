/*
				   File: type_SdtContact_SDT
			Description: Contact_SDT
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

using GeneXus.Programs;
using GeneXus.Programs.wallet;
namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="Contact_SDT")]
	[XmlType(TypeName="Contact_SDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtContact_SDT : GxUserType
	{
		public SdtContact_SDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtContact_SDT_Username = "";

			gxTv_SdtContact_SDT_Userpubkey = "";

			gxTv_SdtContact_SDT_Messagepubkey = "";

			gxTv_SdtContact_SDT_Grouppubkey = "";

			gxTv_SdtContact_SDT_Userprivatename = "";

			gxTv_SdtContact_SDT_Invitationsent = (DateTime)(DateTime.MinValue);

			gxTv_SdtContact_SDT_Invitacionaccepted = (DateTime)(DateTime.MinValue);


		}

		public SdtContact_SDT(IGxContext context)
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
			AddObjectProperty("contactrId", gxTpr_Contactrid, false);


			AddObjectProperty("userName", gxTpr_Username, false);


			AddObjectProperty("userPubKey", gxTpr_Userpubkey, false);


			AddObjectProperty("messagePubKey", gxTpr_Messagepubkey, false);


			AddObjectProperty("groupPubKey", gxTpr_Grouppubkey, false);


			AddObjectProperty("userPrivateName", gxTpr_Userprivatename, false);


			datetime_STZ = gxTpr_Invitationsent;
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
			AddObjectProperty("invitationSent", sDateCnv, false);



			datetime_STZ = gxTpr_Invitacionaccepted;
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
			AddObjectProperty("invitacionAccepted", sDateCnv, false);



			AddObjectProperty("invitationDeclined", gxTpr_Invitationdeclined, false);


			AddObjectProperty("sent", gxTpr_Sent, false);


			AddObjectProperty("received", gxTpr_Received, false);


			AddObjectProperty("edited", gxTpr_Edited, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="contactrId")]
		[XmlElement(ElementName="contactrId")]
		public Guid gxTpr_Contactrid
		{
			get {
				return gxTv_SdtContact_SDT_Contactrid; 
			}
			set {
				gxTv_SdtContact_SDT_Contactrid = value;
				SetDirty("Contactrid");
			}
		}




		[SoapElement(ElementName="userName")]
		[XmlElement(ElementName="userName")]
		public string gxTpr_Username
		{
			get {
				return gxTv_SdtContact_SDT_Username; 
			}
			set {
				gxTv_SdtContact_SDT_Username = value;
				SetDirty("Username");
			}
		}




		[SoapElement(ElementName="userPubKey")]
		[XmlElement(ElementName="userPubKey")]
		public string gxTpr_Userpubkey
		{
			get {
				return gxTv_SdtContact_SDT_Userpubkey; 
			}
			set {
				gxTv_SdtContact_SDT_Userpubkey = value;
				SetDirty("Userpubkey");
			}
		}




		[SoapElement(ElementName="messagePubKey")]
		[XmlElement(ElementName="messagePubKey")]
		public string gxTpr_Messagepubkey
		{
			get {
				return gxTv_SdtContact_SDT_Messagepubkey; 
			}
			set {
				gxTv_SdtContact_SDT_Messagepubkey = value;
				SetDirty("Messagepubkey");
			}
		}




		[SoapElement(ElementName="groupPubKey")]
		[XmlElement(ElementName="groupPubKey")]
		public string gxTpr_Grouppubkey
		{
			get {
				return gxTv_SdtContact_SDT_Grouppubkey; 
			}
			set {
				gxTv_SdtContact_SDT_Grouppubkey = value;
				SetDirty("Grouppubkey");
			}
		}




		[SoapElement(ElementName="userPrivateName")]
		[XmlElement(ElementName="userPrivateName")]
		public string gxTpr_Userprivatename
		{
			get {
				return gxTv_SdtContact_SDT_Userprivatename; 
			}
			set {
				gxTv_SdtContact_SDT_Userprivatename = value;
				SetDirty("Userprivatename");
			}
		}



		[SoapElement(ElementName="invitationSent")]
		[XmlElement(ElementName="invitationSent" , IsNullable=true)]
		public string gxTpr_Invitationsent_Nullable
		{
			get {
				if ( gxTv_SdtContact_SDT_Invitationsent == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtContact_SDT_Invitationsent).value ;
			}
			set {
				gxTv_SdtContact_SDT_Invitationsent = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Invitationsent
		{
			get {
				return gxTv_SdtContact_SDT_Invitationsent; 
			}
			set {
				gxTv_SdtContact_SDT_Invitationsent = value;
				SetDirty("Invitationsent");
			}
		}


		[SoapElement(ElementName="invitacionAccepted")]
		[XmlElement(ElementName="invitacionAccepted" , IsNullable=true)]
		public string gxTpr_Invitacionaccepted_Nullable
		{
			get {
				if ( gxTv_SdtContact_SDT_Invitacionaccepted == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtContact_SDT_Invitacionaccepted).value ;
			}
			set {
				gxTv_SdtContact_SDT_Invitacionaccepted = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Invitacionaccepted
		{
			get {
				return gxTv_SdtContact_SDT_Invitacionaccepted; 
			}
			set {
				gxTv_SdtContact_SDT_Invitacionaccepted = value;
				SetDirty("Invitacionaccepted");
			}
		}



		[SoapElement(ElementName="invitationDeclined")]
		[XmlElement(ElementName="invitationDeclined")]
		public bool gxTpr_Invitationdeclined
		{
			get {
				return gxTv_SdtContact_SDT_Invitationdeclined; 
			}
			set {
				gxTv_SdtContact_SDT_Invitationdeclined = value;
				SetDirty("Invitationdeclined");
			}
		}




		[SoapElement(ElementName="sent")]
		[XmlElement(ElementName="sent")]
		public bool gxTpr_Sent
		{
			get {
				return gxTv_SdtContact_SDT_Sent; 
			}
			set {
				gxTv_SdtContact_SDT_Sent = value;
				SetDirty("Sent");
			}
		}




		[SoapElement(ElementName="received")]
		[XmlElement(ElementName="received")]
		public bool gxTpr_Received
		{
			get {
				return gxTv_SdtContact_SDT_Received; 
			}
			set {
				gxTv_SdtContact_SDT_Received = value;
				SetDirty("Received");
			}
		}




		[SoapElement(ElementName="edited")]
		[XmlElement(ElementName="edited")]
		public bool gxTpr_Edited
		{
			get {
				return gxTv_SdtContact_SDT_Edited; 
			}
			set {
				gxTv_SdtContact_SDT_Edited = value;
				SetDirty("Edited");
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
			gxTv_SdtContact_SDT_Username = "";
			gxTv_SdtContact_SDT_Userpubkey = "";
			gxTv_SdtContact_SDT_Messagepubkey = "";
			gxTv_SdtContact_SDT_Grouppubkey = "";
			gxTv_SdtContact_SDT_Userprivatename = "";
			gxTv_SdtContact_SDT_Invitationsent = (DateTime)(DateTime.MinValue);
			gxTv_SdtContact_SDT_Invitacionaccepted = (DateTime)(DateTime.MinValue);




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

		protected Guid gxTv_SdtContact_SDT_Contactrid;
		 

		protected string gxTv_SdtContact_SDT_Username;
		 

		protected string gxTv_SdtContact_SDT_Userpubkey;
		 

		protected string gxTv_SdtContact_SDT_Messagepubkey;
		 

		protected string gxTv_SdtContact_SDT_Grouppubkey;
		 

		protected string gxTv_SdtContact_SDT_Userprivatename;
		 

		protected DateTime gxTv_SdtContact_SDT_Invitationsent;
		 

		protected DateTime gxTv_SdtContact_SDT_Invitacionaccepted;
		 

		protected bool gxTv_SdtContact_SDT_Invitationdeclined;
		 

		protected bool gxTv_SdtContact_SDT_Sent;
		 

		protected bool gxTv_SdtContact_SDT_Received;
		 

		protected bool gxTv_SdtContact_SDT_Edited;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Contact_SDT", Namespace="distributedcryptography")]
	public class SdtContact_SDT_RESTInterface : GxGenericCollectionItem<SdtContact_SDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtContact_SDT_RESTInterface( ) : base()
		{	
		}

		public SdtContact_SDT_RESTInterface( SdtContact_SDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("contactrId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="contactrId", Order=0)]
		public Guid gxTpr_Contactrid
		{
			get { 
				return sdt.gxTpr_Contactrid;

			}
			set { 
				sdt.gxTpr_Contactrid = value;
			}
		}

		[JsonPropertyName("userName")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="userName", Order=1)]
		public  string gxTpr_Username
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Username);

			}
			set { 
				 sdt.gxTpr_Username = value;
			}
		}

		[JsonPropertyName("userPubKey")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="userPubKey", Order=2)]
		public  string gxTpr_Userpubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Userpubkey);

			}
			set { 
				 sdt.gxTpr_Userpubkey = value;
			}
		}

		[JsonPropertyName("messagePubKey")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="messagePubKey", Order=3)]
		public  string gxTpr_Messagepubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Messagepubkey);

			}
			set { 
				 sdt.gxTpr_Messagepubkey = value;
			}
		}

		[JsonPropertyName("groupPubKey")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="groupPubKey", Order=4)]
		public  string gxTpr_Grouppubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Grouppubkey);

			}
			set { 
				 sdt.gxTpr_Grouppubkey = value;
			}
		}

		[JsonPropertyName("userPrivateName")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="userPrivateName", Order=5)]
		public  string gxTpr_Userprivatename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Userprivatename);

			}
			set { 
				 sdt.gxTpr_Userprivatename = value;
			}
		}

		[JsonPropertyName("invitationSent")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="invitationSent", Order=6)]
		public  string gxTpr_Invitationsent
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Invitationsent,context);

			}
			set { 
				sdt.gxTpr_Invitationsent = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("invitacionAccepted")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="invitacionAccepted", Order=7)]
		public  string gxTpr_Invitacionaccepted
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Invitacionaccepted,context);

			}
			set { 
				sdt.gxTpr_Invitacionaccepted = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("invitationDeclined")]
		[JsonPropertyOrder(8)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="invitationDeclined", Order=8)]
		public bool gxTpr_Invitationdeclined
		{
			get { 
				return sdt.gxTpr_Invitationdeclined;

			}
			set { 
				sdt.gxTpr_Invitationdeclined = value;
			}
		}

		[JsonPropertyName("sent")]
		[JsonPropertyOrder(9)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="sent", Order=9)]
		public bool gxTpr_Sent
		{
			get { 
				return sdt.gxTpr_Sent;

			}
			set { 
				sdt.gxTpr_Sent = value;
			}
		}

		[JsonPropertyName("received")]
		[JsonPropertyOrder(10)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="received", Order=10)]
		public bool gxTpr_Received
		{
			get { 
				return sdt.gxTpr_Received;

			}
			set { 
				sdt.gxTpr_Received = value;
			}
		}

		[JsonPropertyName("edited")]
		[JsonPropertyOrder(11)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="edited", Order=11)]
		public bool gxTpr_Edited
		{
			get { 
				return sdt.gxTpr_Edited;

			}
			set { 
				sdt.gxTpr_Edited = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtContact_SDT sdt
		{
			get { 
				return (SdtContact_SDT)Sdt;
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
				sdt = new SdtContact_SDT() ;
			}
		}
	}
	#endregion
}