/*
				   File: type_SdtserializeEventUnitTestSDT
			Description: serializeEventUnitTestSDT
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
namespace GeneXus.Programs.nostr
{
	[XmlRoot(ElementName="serializeEventUnitTestSDT")]
	[XmlType(TypeName="serializeEventUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtserializeEventUnitTestSDT : GxUserType
	{
		public SdtserializeEventUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtserializeEventUnitTestSDT_Testcaseid = "";

			gxTv_SdtserializeEventUnitTestSDT_Msgnostrevent = "";

			gxTv_SdtserializeEventUnitTestSDT_Privatekey = "";

			gxTv_SdtserializeEventUnitTestSDT_Serialized = "";

			gxTv_SdtserializeEventUnitTestSDT_Expectedserialized = "";

			gxTv_SdtserializeEventUnitTestSDT_Msgserialized = "";

			gxTv_SdtserializeEventUnitTestSDT_Error = "";

			gxTv_SdtserializeEventUnitTestSDT_Expectederror = "";

			gxTv_SdtserializeEventUnitTestSDT_Msgerror = "";

		}

		public SdtserializeEventUnitTestSDT(IGxContext context)
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

			if (gxTv_SdtserializeEventUnitTestSDT_Nostrevent != null)
			{
				AddObjectProperty("nostrEvent", gxTv_SdtserializeEventUnitTestSDT_Nostrevent, false);
			}
			if (gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent != null)
			{
				AddObjectProperty("ExpectednostrEvent", gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent, false);
			}

			AddObjectProperty("MsgnostrEvent", gxTpr_Msgnostrevent, false);


			AddObjectProperty("privateKey", gxTpr_Privatekey, false);


			AddObjectProperty("serialized", gxTpr_Serialized, false);


			AddObjectProperty("Expectedserialized", gxTpr_Expectedserialized, false);


			AddObjectProperty("Msgserialized", gxTpr_Msgserialized, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("Expectederror", gxTpr_Expectederror, false);


			AddObjectProperty("Msgerror", gxTpr_Msgerror, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TestCaseId")]
		[XmlElement(ElementName="TestCaseId")]
		public string gxTpr_Testcaseid
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}



		[SoapElement(ElementName="nostrEvent")]
		[XmlElement(ElementName="nostrEvent")]
		public GeneXus.Programs.nostr.SdtEvent gxTpr_Nostrevent
		{
			get {
				if ( gxTv_SdtserializeEventUnitTestSDT_Nostrevent == null )
				{
					gxTv_SdtserializeEventUnitTestSDT_Nostrevent = new GeneXus.Programs.nostr.SdtEvent(context);
				}
				return gxTv_SdtserializeEventUnitTestSDT_Nostrevent; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Nostrevent = value;
				SetDirty("Nostrevent");
			}
		}
		public void gxTv_SdtserializeEventUnitTestSDT_Nostrevent_SetNull()
		{
			gxTv_SdtserializeEventUnitTestSDT_Nostrevent_N = true;
			gxTv_SdtserializeEventUnitTestSDT_Nostrevent = null;
		}

		public bool gxTv_SdtserializeEventUnitTestSDT_Nostrevent_IsNull()
		{
			return gxTv_SdtserializeEventUnitTestSDT_Nostrevent == null;
		}
		public bool ShouldSerializegxTpr_Nostrevent_Json()
		{
			return gxTv_SdtserializeEventUnitTestSDT_Nostrevent != null;

		}

		[SoapElement(ElementName="ExpectednostrEvent")]
		[XmlElement(ElementName="ExpectednostrEvent")]
		public GeneXus.Programs.nostr.SdtEvent gxTpr_Expectednostrevent
		{
			get {
				if ( gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent == null )
				{
					gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent = new GeneXus.Programs.nostr.SdtEvent(context);
				}
				return gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent = value;
				SetDirty("Expectednostrevent");
			}
		}
		public void gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent_SetNull()
		{
			gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent_N = true;
			gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent = null;
		}

		public bool gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent_IsNull()
		{
			return gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent == null;
		}
		public bool ShouldSerializegxTpr_Expectednostrevent_Json()
		{
			return gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent != null;

		}


		[SoapElement(ElementName="MsgnostrEvent")]
		[XmlElement(ElementName="MsgnostrEvent")]
		public string gxTpr_Msgnostrevent
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Msgnostrevent; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Msgnostrevent = value;
				SetDirty("Msgnostrevent");
			}
		}




		[SoapElement(ElementName="privateKey")]
		[XmlElement(ElementName="privateKey")]
		public string gxTpr_Privatekey
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Privatekey; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Privatekey = value;
				SetDirty("Privatekey");
			}
		}




		[SoapElement(ElementName="serialized")]
		[XmlElement(ElementName="serialized")]
		public string gxTpr_Serialized
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Serialized; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Serialized = value;
				SetDirty("Serialized");
			}
		}




		[SoapElement(ElementName="Expectedserialized")]
		[XmlElement(ElementName="Expectedserialized")]
		public string gxTpr_Expectedserialized
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Expectedserialized; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Expectedserialized = value;
				SetDirty("Expectedserialized");
			}
		}




		[SoapElement(ElementName="Msgserialized")]
		[XmlElement(ElementName="Msgserialized")]
		public string gxTpr_Msgserialized
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Msgserialized; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Msgserialized = value;
				SetDirty("Msgserialized");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtserializeEventUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtserializeEventUnitTestSDT_Msgerror = value;
				SetDirty("Msgerror");
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
			gxTv_SdtserializeEventUnitTestSDT_Testcaseid = "";

			gxTv_SdtserializeEventUnitTestSDT_Nostrevent_N = true;


			gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent_N = true;

			gxTv_SdtserializeEventUnitTestSDT_Msgnostrevent = "";
			gxTv_SdtserializeEventUnitTestSDT_Privatekey = "";
			gxTv_SdtserializeEventUnitTestSDT_Serialized = "";
			gxTv_SdtserializeEventUnitTestSDT_Expectedserialized = "";
			gxTv_SdtserializeEventUnitTestSDT_Msgserialized = "";
			gxTv_SdtserializeEventUnitTestSDT_Error = "";
			gxTv_SdtserializeEventUnitTestSDT_Expectederror = "";
			gxTv_SdtserializeEventUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtserializeEventUnitTestSDT_Testcaseid;
		 

		protected GeneXus.Programs.nostr.SdtEvent gxTv_SdtserializeEventUnitTestSDT_Nostrevent = null;
		protected bool gxTv_SdtserializeEventUnitTestSDT_Nostrevent_N;
		 

		protected GeneXus.Programs.nostr.SdtEvent gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent = null;
		protected bool gxTv_SdtserializeEventUnitTestSDT_Expectednostrevent_N;
		 

		protected string gxTv_SdtserializeEventUnitTestSDT_Msgnostrevent;
		 

		protected string gxTv_SdtserializeEventUnitTestSDT_Privatekey;
		 

		protected string gxTv_SdtserializeEventUnitTestSDT_Serialized;
		 

		protected string gxTv_SdtserializeEventUnitTestSDT_Expectedserialized;
		 

		protected string gxTv_SdtserializeEventUnitTestSDT_Msgserialized;
		 

		protected string gxTv_SdtserializeEventUnitTestSDT_Error;
		 

		protected string gxTv_SdtserializeEventUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtserializeEventUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"serializeEventUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtserializeEventUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtserializeEventUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtserializeEventUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtserializeEventUnitTestSDT_RESTInterface( SdtserializeEventUnitTestSDT psdt ) : base(psdt)
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

		[DataMember(Name="nostrEvent", Order=1, EmitDefaultValue=false)]
		public GeneXus.Programs.nostr.SdtEvent_RESTInterface gxTpr_Nostrevent
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Nostrevent_Json())
					return new GeneXus.Programs.nostr.SdtEvent_RESTInterface(sdt.gxTpr_Nostrevent);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Nostrevent = value.sdt;
			}
		}

		[DataMember(Name="ExpectednostrEvent", Order=2, EmitDefaultValue=false)]
		public GeneXus.Programs.nostr.SdtEvent_RESTInterface gxTpr_Expectednostrevent
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectednostrevent_Json())
					return new GeneXus.Programs.nostr.SdtEvent_RESTInterface(sdt.gxTpr_Expectednostrevent);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Expectednostrevent = value.sdt;
			}
		}

		[DataMember(Name="MsgnostrEvent", Order=3)]
		public  string gxTpr_Msgnostrevent
		{
			get { 
				return sdt.gxTpr_Msgnostrevent;

			}
			set { 
				 sdt.gxTpr_Msgnostrevent = value;
			}
		}

		[DataMember(Name="privateKey", Order=4)]
		public  string gxTpr_Privatekey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Privatekey);

			}
			set { 
				 sdt.gxTpr_Privatekey = value;
			}
		}

		[DataMember(Name="serialized", Order=5)]
		public  string gxTpr_Serialized
		{
			get { 
				return sdt.gxTpr_Serialized;

			}
			set { 
				 sdt.gxTpr_Serialized = value;
			}
		}

		[DataMember(Name="Expectedserialized", Order=6)]
		public  string gxTpr_Expectedserialized
		{
			get { 
				return sdt.gxTpr_Expectedserialized;

			}
			set { 
				 sdt.gxTpr_Expectedserialized = value;
			}
		}

		[DataMember(Name="Msgserialized", Order=7)]
		public  string gxTpr_Msgserialized
		{
			get { 
				return sdt.gxTpr_Msgserialized;

			}
			set { 
				 sdt.gxTpr_Msgserialized = value;
			}
		}

		[DataMember(Name="error", Order=8)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[DataMember(Name="Expectederror", Order=9)]
		public  string gxTpr_Expectederror
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectederror);

			}
			set { 
				 sdt.gxTpr_Expectederror = value;
			}
		}

		[DataMember(Name="Msgerror", Order=10)]
		public  string gxTpr_Msgerror
		{
			get { 
				return sdt.gxTpr_Msgerror;

			}
			set { 
				 sdt.gxTpr_Msgerror = value;
			}
		}


		#endregion

		public SdtserializeEventUnitTestSDT sdt
		{
			get { 
				return (SdtserializeEventUnitTestSDT)Sdt;
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
				sdt = new SdtserializeEventUnitTestSDT() ;
			}
		}
	}
	#endregion
}