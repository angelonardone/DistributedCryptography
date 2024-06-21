/*
				   File: type_SdtprocessRecivedFromNostrTestSDT
			Description: processRecivedFromNostrTestSDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="processRecivedFromNostrTestSDT")]
	[XmlType(TypeName="processRecivedFromNostrTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtprocessRecivedFromNostrTestSDT : GxUserType
	{
		public SdtprocessRecivedFromNostrTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtprocessRecivedFromNostrTestSDT_Testcaseid = "";

			gxTv_SdtprocessRecivedFromNostrTestSDT_Msgrecfromnostr = "";

		}

		public SdtprocessRecivedFromNostrTestSDT(IGxContext context)
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
			AddObjectProperty("TestCaseId", gxTpr_Testcaseid, false);

			if (gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo != null)
			{
				AddObjectProperty("NotificationInfo", gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo, false);
			}
			if (gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr != null)
			{
				AddObjectProperty("recFromNostr", gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr, false);
			}
			if (gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr != null)
			{
				AddObjectProperty("ExpectedrecFromNostr", gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr, false);
			}

			AddObjectProperty("MsgrecFromNostr", gxTpr_Msgrecfromnostr, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TestCaseId")]
		[XmlElement(ElementName="TestCaseId")]
		public string gxTpr_Testcaseid
		{
			get {
				return gxTv_SdtprocessRecivedFromNostrTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtprocessRecivedFromNostrTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}



		[SoapElement(ElementName="NotificationInfo")]
		[XmlElement(ElementName="NotificationInfo")]
		public GeneXus.Core.genexus.server.SdtNotificationInfo gxTpr_Notificationinfo
		{
			get {
				if ( gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo == null )
				{
					gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo = new GeneXus.Core.genexus.server.SdtNotificationInfo(context);
				}
				return gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo; 
			}
			set {
				gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo = value;
				SetDirty("Notificationinfo");
			}
		}
		public void gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo_SetNull()
		{
			gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo_N = true;
			gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo = null;
		}

		public bool gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo_IsNull()
		{
			return gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo == null;
		}
		public bool ShouldSerializegxTpr_Notificationinfo_Json()
		{
			return gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo != null;

		}

		[SoapElement(ElementName="recFromNostr")]
		[XmlElement(ElementName="recFromNostr")]
		public GeneXus.Programs.nostr.SdtRecFromNostr gxTpr_Recfromnostr
		{
			get {
				if ( gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr == null )
				{
					gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr = new GeneXus.Programs.nostr.SdtRecFromNostr(context);
				}
				return gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr; 
			}
			set {
				gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr = value;
				SetDirty("Recfromnostr");
			}
		}
		public void gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr_SetNull()
		{
			gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr_N = true;
			gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr = null;
		}

		public bool gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr_IsNull()
		{
			return gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr == null;
		}
		public bool ShouldSerializegxTpr_Recfromnostr_Json()
		{
			return gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr != null;

		}

		[SoapElement(ElementName="ExpectedrecFromNostr")]
		[XmlElement(ElementName="ExpectedrecFromNostr")]
		public GeneXus.Programs.nostr.SdtRecFromNostr gxTpr_Expectedrecfromnostr
		{
			get {
				if ( gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr == null )
				{
					gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr = new GeneXus.Programs.nostr.SdtRecFromNostr(context);
				}
				return gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr; 
			}
			set {
				gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr = value;
				SetDirty("Expectedrecfromnostr");
			}
		}
		public void gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr_SetNull()
		{
			gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr_N = true;
			gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr = null;
		}

		public bool gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr_IsNull()
		{
			return gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr == null;
		}
		public bool ShouldSerializegxTpr_Expectedrecfromnostr_Json()
		{
			return gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr != null;

		}


		[SoapElement(ElementName="MsgrecFromNostr")]
		[XmlElement(ElementName="MsgrecFromNostr")]
		public string gxTpr_Msgrecfromnostr
		{
			get {
				return gxTv_SdtprocessRecivedFromNostrTestSDT_Msgrecfromnostr; 
			}
			set {
				gxTv_SdtprocessRecivedFromNostrTestSDT_Msgrecfromnostr = value;
				SetDirty("Msgrecfromnostr");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtprocessRecivedFromNostrTestSDT_Testcaseid = "";

			gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo_N = true;


			gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr_N = true;


			gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr_N = true;

			gxTv_SdtprocessRecivedFromNostrTestSDT_Msgrecfromnostr = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtprocessRecivedFromNostrTestSDT_Testcaseid;
		 

		protected GeneXus.Core.genexus.server.SdtNotificationInfo gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo = null;
		protected bool gxTv_SdtprocessRecivedFromNostrTestSDT_Notificationinfo_N;
		 

		protected GeneXus.Programs.nostr.SdtRecFromNostr gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr = null;
		protected bool gxTv_SdtprocessRecivedFromNostrTestSDT_Recfromnostr_N;
		 

		protected GeneXus.Programs.nostr.SdtRecFromNostr gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr = null;
		protected bool gxTv_SdtprocessRecivedFromNostrTestSDT_Expectedrecfromnostr_N;
		 

		protected string gxTv_SdtprocessRecivedFromNostrTestSDT_Msgrecfromnostr;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"processRecivedFromNostrTestSDT", Namespace="distributedcryptography")]
	public class SdtprocessRecivedFromNostrTestSDT_RESTInterface : GxGenericCollectionItem<SdtprocessRecivedFromNostrTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtprocessRecivedFromNostrTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtprocessRecivedFromNostrTestSDT_RESTInterface( SdtprocessRecivedFromNostrTestSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="TestCaseId", Order=0)]
		public  string gxTpr_Testcaseid
		{
			get { 
				return sdt.gxTpr_Testcaseid;

			}
			set { 
				 sdt.gxTpr_Testcaseid = value;
			}
		}

		[DataMember(Name="NotificationInfo", Order=1, EmitDefaultValue=false)]
		public GeneXus.Core.genexus.server.SdtNotificationInfo_RESTInterface gxTpr_Notificationinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Notificationinfo_Json())
					return new GeneXus.Core.genexus.server.SdtNotificationInfo_RESTInterface(sdt.gxTpr_Notificationinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Notificationinfo = value.sdt;
			}
		}

		[DataMember(Name="recFromNostr", Order=2, EmitDefaultValue=false)]
		public GeneXus.Programs.nostr.SdtRecFromNostr_RESTInterface gxTpr_Recfromnostr
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Recfromnostr_Json())
					return new GeneXus.Programs.nostr.SdtRecFromNostr_RESTInterface(sdt.gxTpr_Recfromnostr);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Recfromnostr = value.sdt;
			}
		}

		[DataMember(Name="ExpectedrecFromNostr", Order=3, EmitDefaultValue=false)]
		public GeneXus.Programs.nostr.SdtRecFromNostr_RESTInterface gxTpr_Expectedrecfromnostr
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedrecfromnostr_Json())
					return new GeneXus.Programs.nostr.SdtRecFromNostr_RESTInterface(sdt.gxTpr_Expectedrecfromnostr);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Expectedrecfromnostr = value.sdt;
			}
		}

		[DataMember(Name="MsgrecFromNostr", Order=4)]
		public  string gxTpr_Msgrecfromnostr
		{
			get { 
				return sdt.gxTpr_Msgrecfromnostr;

			}
			set { 
				 sdt.gxTpr_Msgrecfromnostr = value;
			}
		}


		#endregion

		public SdtprocessRecivedFromNostrTestSDT sdt
		{
			get { 
				return (SdtprocessRecivedFromNostrTestSDT)Sdt;
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
				sdt = new SdtprocessRecivedFromNostrTestSDT() ;
			}
		}
	}
	#endregion
}