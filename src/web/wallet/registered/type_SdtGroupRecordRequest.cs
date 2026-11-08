/*
				   File: type_SdtGroupRecordRequest
			Description: GroupRecordRequest
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
	[XmlRoot(ElementName="GroupRecordRequest")]
	[XmlType(TypeName="GroupRecordRequest" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupRecordRequest : GxUserType
	{
		public SdtGroupRecordRequest( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroupRecordRequest_Kind = "";

			gxTv_SdtGroupRecordRequest_Recordkey = "";

			gxTv_SdtGroupRecordRequest_Encryptedrecord = "";

			gxTv_SdtGroupRecordRequest_Accesstoken = "";

			gxTv_SdtGroupRecordRequest_Editsecret = "";

		}

		public SdtGroupRecordRequest(IGxContext context)
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


			AddObjectProperty("Kind", gxTpr_Kind, false);


			AddObjectProperty("RecordKey", gxTpr_Recordkey, false);


			AddObjectProperty("EncryptedRecord", gxTpr_Encryptedrecord, false);


			AddObjectProperty("Version", gxTpr_Version, false);


			AddObjectProperty("Deleted", gxTpr_Deleted, false);


			AddObjectProperty("AccessToken", gxTpr_Accesstoken, false);


			AddObjectProperty("EditSecret", gxTpr_Editsecret, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="GroupId")]
		[XmlElement(ElementName="GroupId")]
		public Guid gxTpr_Groupid
		{
			get {
				return gxTv_SdtGroupRecordRequest_Groupid; 
			}
			set {
				gxTv_SdtGroupRecordRequest_Groupid = value;
				SetDirty("Groupid");
			}
		}




		[SoapElement(ElementName="Kind")]
		[XmlElement(ElementName="Kind")]
		public string gxTpr_Kind
		{
			get {
				return gxTv_SdtGroupRecordRequest_Kind; 
			}
			set {
				gxTv_SdtGroupRecordRequest_Kind = value;
				SetDirty("Kind");
			}
		}




		[SoapElement(ElementName="RecordKey")]
		[XmlElement(ElementName="RecordKey")]
		public string gxTpr_Recordkey
		{
			get {
				return gxTv_SdtGroupRecordRequest_Recordkey; 
			}
			set {
				gxTv_SdtGroupRecordRequest_Recordkey = value;
				SetDirty("Recordkey");
			}
		}




		[SoapElement(ElementName="EncryptedRecord")]
		[XmlElement(ElementName="EncryptedRecord")]
		public string gxTpr_Encryptedrecord
		{
			get {
				return gxTv_SdtGroupRecordRequest_Encryptedrecord; 
			}
			set {
				gxTv_SdtGroupRecordRequest_Encryptedrecord = value;
				SetDirty("Encryptedrecord");
			}
		}




		[SoapElement(ElementName="Version")]
		[XmlElement(ElementName="Version")]
		public long gxTpr_Version
		{
			get {
				return gxTv_SdtGroupRecordRequest_Version; 
			}
			set {
				gxTv_SdtGroupRecordRequest_Version = value;
				SetDirty("Version");
			}
		}




		[SoapElement(ElementName="Deleted")]
		[XmlElement(ElementName="Deleted")]
		public bool gxTpr_Deleted
		{
			get {
				return gxTv_SdtGroupRecordRequest_Deleted; 
			}
			set {
				gxTv_SdtGroupRecordRequest_Deleted = value;
				SetDirty("Deleted");
			}
		}




		[SoapElement(ElementName="AccessToken")]
		[XmlElement(ElementName="AccessToken")]
		public string gxTpr_Accesstoken
		{
			get {
				return gxTv_SdtGroupRecordRequest_Accesstoken; 
			}
			set {
				gxTv_SdtGroupRecordRequest_Accesstoken = value;
				SetDirty("Accesstoken");
			}
		}




		[SoapElement(ElementName="EditSecret")]
		[XmlElement(ElementName="EditSecret")]
		public string gxTpr_Editsecret
		{
			get {
				return gxTv_SdtGroupRecordRequest_Editsecret; 
			}
			set {
				gxTv_SdtGroupRecordRequest_Editsecret = value;
				SetDirty("Editsecret");
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
			gxTv_SdtGroupRecordRequest_Kind = "";
			gxTv_SdtGroupRecordRequest_Recordkey = "";
			gxTv_SdtGroupRecordRequest_Encryptedrecord = "";


			gxTv_SdtGroupRecordRequest_Accesstoken = "";
			gxTv_SdtGroupRecordRequest_Editsecret = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtGroupRecordRequest_Groupid;
		 

		protected string gxTv_SdtGroupRecordRequest_Kind;
		 

		protected string gxTv_SdtGroupRecordRequest_Recordkey;
		 

		protected string gxTv_SdtGroupRecordRequest_Encryptedrecord;
		 

		protected long gxTv_SdtGroupRecordRequest_Version;
		 

		protected bool gxTv_SdtGroupRecordRequest_Deleted;
		 

		protected string gxTv_SdtGroupRecordRequest_Accesstoken;
		 

		protected string gxTv_SdtGroupRecordRequest_Editsecret;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GroupRecordRequest", Namespace="distributedcryptography")]
	public class SdtGroupRecordRequest_RESTInterface : GxGenericCollectionItem<SdtGroupRecordRequest>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupRecordRequest_RESTInterface( ) : base()
		{	
		}

		public SdtGroupRecordRequest_RESTInterface( SdtGroupRecordRequest psdt ) : base(psdt)
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

		[JsonPropertyName("Kind")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Kind", Order=1)]
		public  string gxTpr_Kind
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Kind);

			}
			set { 
				 sdt.gxTpr_Kind = value;
			}
		}

		[JsonPropertyName("RecordKey")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="RecordKey", Order=2)]
		public  string gxTpr_Recordkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Recordkey);

			}
			set { 
				 sdt.gxTpr_Recordkey = value;
			}
		}

		[JsonPropertyName("EncryptedRecord")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="EncryptedRecord", Order=3)]
		public  string gxTpr_Encryptedrecord
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedrecord);

			}
			set { 
				 sdt.gxTpr_Encryptedrecord = value;
			}
		}

		[JsonPropertyName("Version")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="Version", Order=4)]
		public  string gxTpr_Version
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Version, 10, 0));

			}
			set { 
				sdt.gxTpr_Version = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("Deleted")]
		[JsonPropertyOrder(5)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="Deleted", Order=5)]
		public bool gxTpr_Deleted
		{
			get { 
				return sdt.gxTpr_Deleted;

			}
			set { 
				sdt.gxTpr_Deleted = value;
			}
		}

		[JsonPropertyName("AccessToken")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="AccessToken", Order=6)]
		public  string gxTpr_Accesstoken
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Accesstoken);

			}
			set { 
				 sdt.gxTpr_Accesstoken = value;
			}
		}

		[JsonPropertyName("EditSecret")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="EditSecret", Order=7)]
		public  string gxTpr_Editsecret
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Editsecret);

			}
			set { 
				 sdt.gxTpr_Editsecret = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroupRecordRequest sdt
		{
			get { 
				return (SdtGroupRecordRequest)Sdt;
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
				sdt = new SdtGroupRecordRequest() ;
			}
		}
	}
	#endregion
}