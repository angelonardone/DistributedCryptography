/*
				   File: type_SdtQueuedNostrEvent
			Description: QueuedNostrEvent
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
	[XmlRoot(ElementName="QueuedNostrEvent")]
	[XmlType(TypeName="QueuedNostrEvent" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtQueuedNostrEvent : GxUserType
	{
		public SdtQueuedNostrEvent( )
		{
			/* Constructor for serialization */
			gxTv_SdtQueuedNostrEvent_Notification = "";

		}

		public SdtQueuedNostrEvent(IGxContext context)
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
			AddObjectProperty("createdAt", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Createdat, 18, 0)), false);


			AddObjectProperty("notification", gxTpr_Notification, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="createdAt")]
		[XmlElement(ElementName="createdAt")]
		public long gxTpr_Createdat
		{
			get {
				return gxTv_SdtQueuedNostrEvent_Createdat; 
			}
			set {
				gxTv_SdtQueuedNostrEvent_Createdat = value;
				SetDirty("Createdat");
			}
		}




		[SoapElement(ElementName="notification")]
		[XmlElement(ElementName="notification")]
		public string gxTpr_Notification
		{
			get {
				return gxTv_SdtQueuedNostrEvent_Notification; 
			}
			set {
				gxTv_SdtQueuedNostrEvent_Notification = value;
				SetDirty("Notification");
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
			gxTv_SdtQueuedNostrEvent_Notification = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected long gxTv_SdtQueuedNostrEvent_Createdat;
		 

		protected string gxTv_SdtQueuedNostrEvent_Notification;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"QueuedNostrEvent", Namespace="distributedcryptography")]
	public class SdtQueuedNostrEvent_RESTInterface : GxGenericCollectionItem<SdtQueuedNostrEvent>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtQueuedNostrEvent_RESTInterface( ) : base()
		{	
		}

		public SdtQueuedNostrEvent_RESTInterface( SdtQueuedNostrEvent psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("createdAt")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="createdAt", Order=0)]
		public  string gxTpr_Createdat
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Createdat, 18, 0));

			}
			set { 
				sdt.gxTpr_Createdat = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("notification")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="notification", Order=1)]
		public  string gxTpr_Notification
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Notification);

			}
			set { 
				 sdt.gxTpr_Notification = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtQueuedNostrEvent sdt
		{
			get { 
				return (SdtQueuedNostrEvent)Sdt;
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
				sdt = new SdtQueuedNostrEvent() ;
			}
		}
	}
	#endregion
}