/*
				   File: type_SdtMuSig2RoomLine
			Description: MuSig2RoomLine
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
	[XmlRoot(ElementName="MuSig2RoomLine")]
	[XmlType(TypeName="MuSig2RoomLine" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2RoomLine : GxUserType
	{
		public SdtMuSig2RoomLine( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2RoomLine_Created = "";

			gxTv_SdtMuSig2RoomLine_Openedby = "";

			gxTv_SdtMuSig2RoomLine_Description = "";

			gxTv_SdtMuSig2RoomLine_Amounttext = "";

			gxTv_SdtMuSig2RoomLine_Sendto = "";

			gxTv_SdtMuSig2RoomLine_Statetext = "";

			gxTv_SdtMuSig2RoomLine_People = "";

			gxTv_SdtMuSig2RoomLine_Waiting = "";

			gxTv_SdtMuSig2RoomLine_Chatlog = "";

			gxTv_SdtMuSig2RoomLine_Stateshort = "";

		}

		public SdtMuSig2RoomLine(IGxContext context)
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
			AddObjectProperty("roomId", gxTpr_Roomid, false);


			AddObjectProperty("created", gxTpr_Created, false);


			AddObjectProperty("openedBy", gxTpr_Openedby, false);


			AddObjectProperty("description", gxTpr_Description, false);


			AddObjectProperty("amountText", gxTpr_Amounttext, false);


			AddObjectProperty("sendTo", gxTpr_Sendto, false);


			AddObjectProperty("stateText", gxTpr_Statetext, false);


			AddObjectProperty("people", gxTpr_People, false);


			AddObjectProperty("canHere", gxTpr_Canhere, false);


			AddObjectProperty("canSignNow", gxTpr_Cansignnow, false);


			AddObjectProperty("canSign", gxTpr_Cansign, false);


			AddObjectProperty("canAbort", gxTpr_Canabort, false);


			AddObjectProperty("waiting", gxTpr_Waiting, false);


			AddObjectProperty("isOpen", gxTpr_Isopen, false);


			AddObjectProperty("chatLog", gxTpr_Chatlog, false);


			AddObjectProperty("stateShort", gxTpr_Stateshort, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="roomId")]
		[XmlElement(ElementName="roomId")]
		public Guid gxTpr_Roomid
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Roomid; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Roomid = value;
				SetDirty("Roomid");
			}
		}




		[SoapElement(ElementName="created")]
		[XmlElement(ElementName="created")]
		public string gxTpr_Created
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Created; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Created = value;
				SetDirty("Created");
			}
		}




		[SoapElement(ElementName="openedBy")]
		[XmlElement(ElementName="openedBy")]
		public string gxTpr_Openedby
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Openedby; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Openedby = value;
				SetDirty("Openedby");
			}
		}




		[SoapElement(ElementName="description")]
		[XmlElement(ElementName="description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Description; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="amountText")]
		[XmlElement(ElementName="amountText")]
		public string gxTpr_Amounttext
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Amounttext; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Amounttext = value;
				SetDirty("Amounttext");
			}
		}




		[SoapElement(ElementName="sendTo")]
		[XmlElement(ElementName="sendTo")]
		public string gxTpr_Sendto
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Sendto; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Sendto = value;
				SetDirty("Sendto");
			}
		}




		[SoapElement(ElementName="stateText")]
		[XmlElement(ElementName="stateText")]
		public string gxTpr_Statetext
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Statetext; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Statetext = value;
				SetDirty("Statetext");
			}
		}




		[SoapElement(ElementName="people")]
		[XmlElement(ElementName="people")]
		public string gxTpr_People
		{
			get {
				return gxTv_SdtMuSig2RoomLine_People; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_People = value;
				SetDirty("People");
			}
		}




		[SoapElement(ElementName="canHere")]
		[XmlElement(ElementName="canHere")]
		public bool gxTpr_Canhere
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Canhere; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Canhere = value;
				SetDirty("Canhere");
			}
		}




		[SoapElement(ElementName="canSignNow")]
		[XmlElement(ElementName="canSignNow")]
		public bool gxTpr_Cansignnow
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Cansignnow; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Cansignnow = value;
				SetDirty("Cansignnow");
			}
		}




		[SoapElement(ElementName="canSign")]
		[XmlElement(ElementName="canSign")]
		public bool gxTpr_Cansign
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Cansign; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Cansign = value;
				SetDirty("Cansign");
			}
		}




		[SoapElement(ElementName="canAbort")]
		[XmlElement(ElementName="canAbort")]
		public bool gxTpr_Canabort
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Canabort; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Canabort = value;
				SetDirty("Canabort");
			}
		}




		[SoapElement(ElementName="waiting")]
		[XmlElement(ElementName="waiting")]
		public string gxTpr_Waiting
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Waiting; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Waiting = value;
				SetDirty("Waiting");
			}
		}




		[SoapElement(ElementName="isOpen")]
		[XmlElement(ElementName="isOpen")]
		public bool gxTpr_Isopen
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Isopen; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Isopen = value;
				SetDirty("Isopen");
			}
		}




		[SoapElement(ElementName="chatLog")]
		[XmlElement(ElementName="chatLog")]
		public string gxTpr_Chatlog
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Chatlog; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Chatlog = value;
				SetDirty("Chatlog");
			}
		}




		[SoapElement(ElementName="stateShort")]
		[XmlElement(ElementName="stateShort")]
		public string gxTpr_Stateshort
		{
			get {
				return gxTv_SdtMuSig2RoomLine_Stateshort; 
			}
			set {
				gxTv_SdtMuSig2RoomLine_Stateshort = value;
				SetDirty("Stateshort");
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
			gxTv_SdtMuSig2RoomLine_Created = "";
			gxTv_SdtMuSig2RoomLine_Openedby = "";
			gxTv_SdtMuSig2RoomLine_Description = "";
			gxTv_SdtMuSig2RoomLine_Amounttext = "";
			gxTv_SdtMuSig2RoomLine_Sendto = "";
			gxTv_SdtMuSig2RoomLine_Statetext = "";
			gxTv_SdtMuSig2RoomLine_People = "";




			gxTv_SdtMuSig2RoomLine_Waiting = "";

			gxTv_SdtMuSig2RoomLine_Chatlog = "";
			gxTv_SdtMuSig2RoomLine_Stateshort = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtMuSig2RoomLine_Roomid;
		 

		protected string gxTv_SdtMuSig2RoomLine_Created;
		 

		protected string gxTv_SdtMuSig2RoomLine_Openedby;
		 

		protected string gxTv_SdtMuSig2RoomLine_Description;
		 

		protected string gxTv_SdtMuSig2RoomLine_Amounttext;
		 

		protected string gxTv_SdtMuSig2RoomLine_Sendto;
		 

		protected string gxTv_SdtMuSig2RoomLine_Statetext;
		 

		protected string gxTv_SdtMuSig2RoomLine_People;
		 

		protected bool gxTv_SdtMuSig2RoomLine_Canhere;
		 

		protected bool gxTv_SdtMuSig2RoomLine_Cansignnow;
		 

		protected bool gxTv_SdtMuSig2RoomLine_Cansign;
		 

		protected bool gxTv_SdtMuSig2RoomLine_Canabort;
		 

		protected string gxTv_SdtMuSig2RoomLine_Waiting;
		 

		protected bool gxTv_SdtMuSig2RoomLine_Isopen;
		 

		protected string gxTv_SdtMuSig2RoomLine_Chatlog;
		 

		protected string gxTv_SdtMuSig2RoomLine_Stateshort;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSig2RoomLine", Namespace="distributedcryptography")]
	public class SdtMuSig2RoomLine_RESTInterface : GxGenericCollectionItem<SdtMuSig2RoomLine>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2RoomLine_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2RoomLine_RESTInterface( SdtMuSig2RoomLine psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("roomId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="roomId", Order=0)]
		public Guid gxTpr_Roomid
		{
			get { 
				return sdt.gxTpr_Roomid;

			}
			set { 
				sdt.gxTpr_Roomid = value;
			}
		}

		[JsonPropertyName("created")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="created", Order=1)]
		public  string gxTpr_Created
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Created);

			}
			set { 
				 sdt.gxTpr_Created = value;
			}
		}

		[JsonPropertyName("openedBy")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="openedBy", Order=2)]
		public  string gxTpr_Openedby
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Openedby);

			}
			set { 
				 sdt.gxTpr_Openedby = value;
			}
		}

		[JsonPropertyName("description")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="description", Order=3)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[JsonPropertyName("amountText")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="amountText", Order=4)]
		public  string gxTpr_Amounttext
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Amounttext);

			}
			set { 
				 sdt.gxTpr_Amounttext = value;
			}
		}

		[JsonPropertyName("sendTo")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="sendTo", Order=5)]
		public  string gxTpr_Sendto
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Sendto);

			}
			set { 
				 sdt.gxTpr_Sendto = value;
			}
		}

		[JsonPropertyName("stateText")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="stateText", Order=6)]
		public  string gxTpr_Statetext
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Statetext);

			}
			set { 
				 sdt.gxTpr_Statetext = value;
			}
		}

		[JsonPropertyName("people")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="people", Order=7)]
		public  string gxTpr_People
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_People);

			}
			set { 
				 sdt.gxTpr_People = value;
			}
		}

		[JsonPropertyName("canHere")]
		[JsonPropertyOrder(8)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="canHere", Order=8)]
		public bool gxTpr_Canhere
		{
			get { 
				return sdt.gxTpr_Canhere;

			}
			set { 
				sdt.gxTpr_Canhere = value;
			}
		}

		[JsonPropertyName("canSignNow")]
		[JsonPropertyOrder(9)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="canSignNow", Order=9)]
		public bool gxTpr_Cansignnow
		{
			get { 
				return sdt.gxTpr_Cansignnow;

			}
			set { 
				sdt.gxTpr_Cansignnow = value;
			}
		}

		[JsonPropertyName("canSign")]
		[JsonPropertyOrder(10)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="canSign", Order=10)]
		public bool gxTpr_Cansign
		{
			get { 
				return sdt.gxTpr_Cansign;

			}
			set { 
				sdt.gxTpr_Cansign = value;
			}
		}

		[JsonPropertyName("canAbort")]
		[JsonPropertyOrder(11)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="canAbort", Order=11)]
		public bool gxTpr_Canabort
		{
			get { 
				return sdt.gxTpr_Canabort;

			}
			set { 
				sdt.gxTpr_Canabort = value;
			}
		}

		[JsonPropertyName("waiting")]
		[JsonPropertyOrder(12)]
		[DataMember(Name="waiting", Order=12)]
		public  string gxTpr_Waiting
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Waiting);

			}
			set { 
				 sdt.gxTpr_Waiting = value;
			}
		}

		[JsonPropertyName("isOpen")]
		[JsonPropertyOrder(13)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isOpen", Order=13)]
		public bool gxTpr_Isopen
		{
			get { 
				return sdt.gxTpr_Isopen;

			}
			set { 
				sdt.gxTpr_Isopen = value;
			}
		}

		[JsonPropertyName("chatLog")]
		[JsonPropertyOrder(14)]
		[DataMember(Name="chatLog", Order=14)]
		public  string gxTpr_Chatlog
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Chatlog);

			}
			set { 
				 sdt.gxTpr_Chatlog = value;
			}
		}

		[JsonPropertyName("stateShort")]
		[JsonPropertyOrder(15)]
		[DataMember(Name="stateShort", Order=15)]
		public  string gxTpr_Stateshort
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Stateshort);

			}
			set { 
				 sdt.gxTpr_Stateshort = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtMuSig2RoomLine sdt
		{
			get { 
				return (SdtMuSig2RoomLine)Sdt;
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
				sdt = new SdtMuSig2RoomLine() ;
			}
		}
	}
	#endregion
}