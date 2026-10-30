/*
				   File: type_SdtNotificationTarget
			Description: NotificationTarget
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
	[XmlRoot(ElementName="NotificationTarget")]
	[XmlType(TypeName="NotificationTarget" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtNotificationTarget : GxUserType
	{
		public SdtNotificationTarget( )
		{
			/* Constructor for serialization */
			gxTv_SdtNotificationTarget_Kind = "";

			gxTv_SdtNotificationTarget_Datetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtNotificationTarget_Toasttype = "";

			gxTv_SdtNotificationTarget_Title = "";

			gxTv_SdtNotificationTarget_Text = "";


		}

		public SdtNotificationTarget(IGxContext context)
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
			AddObjectProperty("index", gxTpr_Index, false);


			AddObjectProperty("kind", gxTpr_Kind, false);


			AddObjectProperty("contactId", gxTpr_Contactid, false);


			AddObjectProperty("groupId", gxTpr_Groupid, false);


			datetime_STZ = gxTpr_Datetime;
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
			AddObjectProperty("dateTime", sDateCnv, false);



			AddObjectProperty("toastType", gxTpr_Toasttype, false);


			AddObjectProperty("title", gxTpr_Title, false);


			AddObjectProperty("text", gxTpr_Text, false);


			AddObjectProperty("isRead", gxTpr_Isread, false);


			AddObjectProperty("referenceGroupId", gxTpr_Referencegroupid, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="index")]
		[XmlElement(ElementName="index")]
		public int gxTpr_Index
		{
			get {
				return gxTv_SdtNotificationTarget_Index; 
			}
			set {
				gxTv_SdtNotificationTarget_Index = value;
				SetDirty("Index");
			}
		}




		[SoapElement(ElementName="kind")]
		[XmlElement(ElementName="kind")]
		public string gxTpr_Kind
		{
			get {
				return gxTv_SdtNotificationTarget_Kind; 
			}
			set {
				gxTv_SdtNotificationTarget_Kind = value;
				SetDirty("Kind");
			}
		}




		[SoapElement(ElementName="contactId")]
		[XmlElement(ElementName="contactId")]
		public Guid gxTpr_Contactid
		{
			get {
				return gxTv_SdtNotificationTarget_Contactid; 
			}
			set {
				gxTv_SdtNotificationTarget_Contactid = value;
				SetDirty("Contactid");
			}
		}




		[SoapElement(ElementName="groupId")]
		[XmlElement(ElementName="groupId")]
		public Guid gxTpr_Groupid
		{
			get {
				return gxTv_SdtNotificationTarget_Groupid; 
			}
			set {
				gxTv_SdtNotificationTarget_Groupid = value;
				SetDirty("Groupid");
			}
		}



		[SoapElement(ElementName="dateTime")]
		[XmlElement(ElementName="dateTime" , IsNullable=true)]
		public string gxTpr_Datetime_Nullable
		{
			get {
				if ( gxTv_SdtNotificationTarget_Datetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtNotificationTarget_Datetime).value ;
			}
			set {
				gxTv_SdtNotificationTarget_Datetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Datetime
		{
			get {
				return gxTv_SdtNotificationTarget_Datetime; 
			}
			set {
				gxTv_SdtNotificationTarget_Datetime = value;
				SetDirty("Datetime");
			}
		}



		[SoapElement(ElementName="toastType")]
		[XmlElement(ElementName="toastType")]
		public string gxTpr_Toasttype
		{
			get {
				return gxTv_SdtNotificationTarget_Toasttype; 
			}
			set {
				gxTv_SdtNotificationTarget_Toasttype = value;
				SetDirty("Toasttype");
			}
		}




		[SoapElement(ElementName="title")]
		[XmlElement(ElementName="title")]
		public string gxTpr_Title
		{
			get {
				return gxTv_SdtNotificationTarget_Title; 
			}
			set {
				gxTv_SdtNotificationTarget_Title = value;
				SetDirty("Title");
			}
		}




		[SoapElement(ElementName="text")]
		[XmlElement(ElementName="text")]
		public string gxTpr_Text
		{
			get {
				return gxTv_SdtNotificationTarget_Text; 
			}
			set {
				gxTv_SdtNotificationTarget_Text = value;
				SetDirty("Text");
			}
		}




		[SoapElement(ElementName="isRead")]
		[XmlElement(ElementName="isRead")]
		public bool gxTpr_Isread
		{
			get {
				return gxTv_SdtNotificationTarget_Isread; 
			}
			set {
				gxTv_SdtNotificationTarget_Isread = value;
				SetDirty("Isread");
			}
		}




		[SoapElement(ElementName="referenceGroupId")]
		[XmlElement(ElementName="referenceGroupId")]
		public Guid gxTpr_Referencegroupid
		{
			get {
				return gxTv_SdtNotificationTarget_Referencegroupid; 
			}
			set {
				gxTv_SdtNotificationTarget_Referencegroupid = value;
				SetDirty("Referencegroupid");
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
			gxTv_SdtNotificationTarget_Kind = "";


			gxTv_SdtNotificationTarget_Datetime = (DateTime)(DateTime.MinValue);
			gxTv_SdtNotificationTarget_Toasttype = "";
			gxTv_SdtNotificationTarget_Title = "";
			gxTv_SdtNotificationTarget_Text = "";


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

		protected int gxTv_SdtNotificationTarget_Index;
		 

		protected string gxTv_SdtNotificationTarget_Kind;
		 

		protected Guid gxTv_SdtNotificationTarget_Contactid;
		 

		protected Guid gxTv_SdtNotificationTarget_Groupid;
		 

		protected DateTime gxTv_SdtNotificationTarget_Datetime;
		 

		protected string gxTv_SdtNotificationTarget_Toasttype;
		 

		protected string gxTv_SdtNotificationTarget_Title;
		 

		protected string gxTv_SdtNotificationTarget_Text;
		 

		protected bool gxTv_SdtNotificationTarget_Isread;
		 

		protected Guid gxTv_SdtNotificationTarget_Referencegroupid;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"NotificationTarget", Namespace="distributedcryptography")]
	public class SdtNotificationTarget_RESTInterface : GxGenericCollectionItem<SdtNotificationTarget>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtNotificationTarget_RESTInterface( ) : base()
		{	
		}

		public SdtNotificationTarget_RESTInterface( SdtNotificationTarget psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("index")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="index", Order=0)]
		public  string gxTpr_Index
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Index, 8, 0));

			}
			set { 
				sdt.gxTpr_Index = (int) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("kind")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="kind", Order=1)]
		public  string gxTpr_Kind
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Kind);

			}
			set { 
				 sdt.gxTpr_Kind = value;
			}
		}

		[JsonPropertyName("contactId")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="contactId", Order=2)]
		public Guid gxTpr_Contactid
		{
			get { 
				return sdt.gxTpr_Contactid;

			}
			set { 
				sdt.gxTpr_Contactid = value;
			}
		}

		[JsonPropertyName("groupId")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="groupId", Order=3)]
		public Guid gxTpr_Groupid
		{
			get { 
				return sdt.gxTpr_Groupid;

			}
			set { 
				sdt.gxTpr_Groupid = value;
			}
		}

		[JsonPropertyName("dateTime")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="dateTime", Order=4)]
		public  string gxTpr_Datetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Datetime,context);

			}
			set { 
				sdt.gxTpr_Datetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("toastType")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="toastType", Order=5)]
		public  string gxTpr_Toasttype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Toasttype);

			}
			set { 
				 sdt.gxTpr_Toasttype = value;
			}
		}

		[JsonPropertyName("title")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="title", Order=6)]
		public  string gxTpr_Title
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Title);

			}
			set { 
				 sdt.gxTpr_Title = value;
			}
		}

		[JsonPropertyName("text")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="text", Order=7)]
		public  string gxTpr_Text
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Text);

			}
			set { 
				 sdt.gxTpr_Text = value;
			}
		}

		[JsonPropertyName("isRead")]
		[JsonPropertyOrder(8)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isRead", Order=8)]
		public bool gxTpr_Isread
		{
			get { 
				return sdt.gxTpr_Isread;

			}
			set { 
				sdt.gxTpr_Isread = value;
			}
		}

		[JsonPropertyName("referenceGroupId")]
		[JsonPropertyOrder(9)]
		[DataMember(Name="referenceGroupId", Order=9)]
		public Guid gxTpr_Referencegroupid
		{
			get { 
				return sdt.gxTpr_Referencegroupid;

			}
			set { 
				sdt.gxTpr_Referencegroupid = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtNotificationTarget sdt
		{
			get { 
				return (SdtNotificationTarget)Sdt;
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
				sdt = new SdtNotificationTarget() ;
			}
		}
	}
	#endregion
}