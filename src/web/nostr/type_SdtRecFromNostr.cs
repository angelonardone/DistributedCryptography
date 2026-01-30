/*
				   File: type_SdtRecFromNostr
			Description: RecFromNostr
				 Author: Nemo 🐠 for C# (.NET) version 18.0.14.187820
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

namespace GeneXus.Programs.nostr
{
	[XmlRoot(ElementName="RecFromNostr")]
	[XmlType(TypeName="RecFromNostr" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRecFromNostr : GxUserType
	{
		public SdtRecFromNostr( )
		{
			/* Constructor for serialization */
			gxTv_SdtRecFromNostr_Responsetype = "";

			gxTv_SdtRecFromNostr_Subscription_id = "";

			gxTv_SdtRecFromNostr_Event_id = "";

			gxTv_SdtRecFromNostr_Messge = "";

			gxTv_SdtRecFromNostr_Event_N = true;

		}

		public SdtRecFromNostr(IGxContext context)
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
			AddObjectProperty("responseType", gxTpr_Responsetype, false);


			AddObjectProperty("subscription_id", gxTpr_Subscription_id, false);


			AddObjectProperty("event_id", gxTpr_Event_id, false);


			AddObjectProperty("event_accepted", gxTpr_Event_accepted, false);


			AddObjectProperty("messge", gxTpr_Messge, false);

			if (gxTv_SdtRecFromNostr_Event != null)
			{
				AddObjectProperty("event", gxTv_SdtRecFromNostr_Event, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="responseType")]
		[XmlElement(ElementName="responseType")]
		public string gxTpr_Responsetype
		{
			get {
				return gxTv_SdtRecFromNostr_Responsetype; 
			}
			set {
				gxTv_SdtRecFromNostr_Responsetype = value;
				SetDirty("Responsetype");
			}
		}




		[SoapElement(ElementName="subscription_id")]
		[XmlElement(ElementName="subscription_id")]
		public string gxTpr_Subscription_id
		{
			get {
				return gxTv_SdtRecFromNostr_Subscription_id; 
			}
			set {
				gxTv_SdtRecFromNostr_Subscription_id = value;
				SetDirty("Subscription_id");
			}
		}




		[SoapElement(ElementName="event_id")]
		[XmlElement(ElementName="event_id")]
		public string gxTpr_Event_id
		{
			get {
				return gxTv_SdtRecFromNostr_Event_id; 
			}
			set {
				gxTv_SdtRecFromNostr_Event_id = value;
				SetDirty("Event_id");
			}
		}




		[SoapElement(ElementName="event_accepted")]
		[XmlElement(ElementName="event_accepted")]
		public bool gxTpr_Event_accepted
		{
			get {
				return gxTv_SdtRecFromNostr_Event_accepted; 
			}
			set {
				gxTv_SdtRecFromNostr_Event_accepted = value;
				SetDirty("Event_accepted");
			}
		}




		[SoapElement(ElementName="messge")]
		[XmlElement(ElementName="messge")]
		public string gxTpr_Messge
		{
			get {
				return gxTv_SdtRecFromNostr_Messge; 
			}
			set {
				gxTv_SdtRecFromNostr_Messge = value;
				SetDirty("Messge");
			}
		}



		[SoapElement(ElementName="event")]
		[XmlElement(ElementName="event")]
		public GeneXus.Programs.nostr.SdtEvent gxTpr_Event
		{
			get {
				if ( gxTv_SdtRecFromNostr_Event == null )
				{
					gxTv_SdtRecFromNostr_Event = new GeneXus.Programs.nostr.SdtEvent(context);
					SetDirty("Event");
				}
				return gxTv_SdtRecFromNostr_Event; 
			}
			set {
				gxTv_SdtRecFromNostr_Event = value;
				SetDirty("Event");
			}
		}
		public void gxTv_SdtRecFromNostr_Event_SetNull()
		{
			gxTv_SdtRecFromNostr_Event_N = true;
			gxTv_SdtRecFromNostr_Event = null;
		}

		public bool gxTv_SdtRecFromNostr_Event_IsNull()
		{
			return gxTv_SdtRecFromNostr_Event == null;
		}
		public bool ShouldSerializegxTpr_Event_Json()
		{
			return gxTv_SdtRecFromNostr_Event != null;

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
			gxTv_SdtRecFromNostr_Responsetype = "";
			gxTv_SdtRecFromNostr_Subscription_id = "";
			gxTv_SdtRecFromNostr_Event_id = "";

			gxTv_SdtRecFromNostr_Messge = "";

			gxTv_SdtRecFromNostr_Event_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtRecFromNostr_Responsetype;
		 

		protected string gxTv_SdtRecFromNostr_Subscription_id;
		 

		protected string gxTv_SdtRecFromNostr_Event_id;
		 

		protected bool gxTv_SdtRecFromNostr_Event_accepted;
		 

		protected string gxTv_SdtRecFromNostr_Messge;
		 

		protected GeneXus.Programs.nostr.SdtEvent gxTv_SdtRecFromNostr_Event = null;
		protected bool gxTv_SdtRecFromNostr_Event_N;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"RecFromNostr", Namespace="distributedcryptography")]
	public class SdtRecFromNostr_RESTInterface : GxGenericCollectionItem<SdtRecFromNostr>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRecFromNostr_RESTInterface( ) : base()
		{	
		}

		public SdtRecFromNostr_RESTInterface( SdtRecFromNostr psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("responseType")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="responseType", Order=0)]
		public  string gxTpr_Responsetype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Responsetype);

			}
			set { 
				 sdt.gxTpr_Responsetype = value;
			}
		}

		[JsonPropertyName("subscription_id")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="subscription_id", Order=1)]
		public  string gxTpr_Subscription_id
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Subscription_id);

			}
			set { 
				 sdt.gxTpr_Subscription_id = value;
			}
		}

		[JsonPropertyName("event_id")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="event_id", Order=2)]
		public  string gxTpr_Event_id
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Event_id);

			}
			set { 
				 sdt.gxTpr_Event_id = value;
			}
		}

		[JsonPropertyName("event_accepted")]
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="event_accepted", Order=3)]
		public bool gxTpr_Event_accepted
		{
			get { 
				return sdt.gxTpr_Event_accepted;

			}
			set { 
				sdt.gxTpr_Event_accepted = value;
			}
		}

		[JsonPropertyName("messge")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="messge", Order=4)]
		public  string gxTpr_Messge
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Messge);

			}
			set { 
				 sdt.gxTpr_Messge = value;
			}
		}

		[JsonPropertyName("event")]
		[JsonPropertyOrder(5)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="event", Order=5, EmitDefaultValue=false)]
		public GeneXus.Programs.nostr.SdtEvent_RESTInterface gxTpr_Event
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Event_Json())
					return new GeneXus.Programs.nostr.SdtEvent_RESTInterface(sdt.gxTpr_Event);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Event = value.sdt;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtRecFromNostr sdt
		{
			get { 
				return (SdtRecFromNostr)Sdt;
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
				sdt = new SdtRecFromNostr() ;
			}
		}
	}
	#endregion
}