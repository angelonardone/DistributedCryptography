/*
				   File: type_SdtExternalUserPublic
			Description: ExternalUserPublic
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

namespace GeneXus.Programs.distcrypt
{
	[XmlRoot(ElementName="ExternalUserPublic")]
	[XmlType(TypeName="ExternalUserPublic" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtExternalUserPublic : GxUserType
	{
		public SdtExternalUserPublic( )
		{
			/* Constructor for serialization */
			gxTv_SdtExternalUserPublic_Userinfo_N = true;

		}

		public SdtExternalUserPublic(IGxContext context)
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
			AddObjectProperty("isLoggedIn", gxTpr_Isloggedin, false);

			if (gxTv_SdtExternalUserPublic_Userinfo != null)
			{
				AddObjectProperty("UserInfo", gxTv_SdtExternalUserPublic_Userinfo, false);
			}
			if (gxTv_SdtExternalUserPublic_Keyinfo != null)
			{
				AddObjectProperty("KeyInfo", gxTv_SdtExternalUserPublic_Keyinfo, false);
			}
			if (gxTv_SdtExternalUserPublic_Chatkeyinfo != null)
			{
				AddObjectProperty("ChatKeyInfo", gxTv_SdtExternalUserPublic_Chatkeyinfo, false);
			}
			if (gxTv_SdtExternalUserPublic_Groupskeyinfo != null)
			{
				AddObjectProperty("GroupsKeyInfo", gxTv_SdtExternalUserPublic_Groupskeyinfo, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="isLoggedIn")]
		[XmlElement(ElementName="isLoggedIn")]
		public bool gxTpr_Isloggedin
		{
			get {
				return gxTv_SdtExternalUserPublic_Isloggedin; 
			}
			set {
				gxTv_SdtExternalUserPublic_Isloggedin = value;
				SetDirty("Isloggedin");
			}
		}



		[SoapElement(ElementName="UserInfo")]
		[XmlElement(ElementName="UserInfo")]
		public GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT gxTpr_Userinfo
		{
			get {
				if ( gxTv_SdtExternalUserPublic_Userinfo == null )
				{
					gxTv_SdtExternalUserPublic_Userinfo = new GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT(context);
					SetDirty("Userinfo");
				}
				return gxTv_SdtExternalUserPublic_Userinfo; 
			}
			set {
				gxTv_SdtExternalUserPublic_Userinfo = value;
				SetDirty("Userinfo");
			}
		}
		public void gxTv_SdtExternalUserPublic_Userinfo_SetNull()
		{
			gxTv_SdtExternalUserPublic_Userinfo_N = true;
			gxTv_SdtExternalUserPublic_Userinfo = null;
		}

		public bool gxTv_SdtExternalUserPublic_Userinfo_IsNull()
		{
			return gxTv_SdtExternalUserPublic_Userinfo == null;
		}
		public bool ShouldSerializegxTpr_Userinfo_Json()
		{
			return gxTv_SdtExternalUserPublic_Userinfo != null;

		}

		[SoapElement(ElementName="KeyInfo" )]
		[XmlElement(ElementName="KeyInfo" )]
		public SdtExternalUserPublic_KeyInfo gxTpr_Keyinfo
		{
			get {
				if ( gxTv_SdtExternalUserPublic_Keyinfo == null )
				{
					gxTv_SdtExternalUserPublic_Keyinfo = new SdtExternalUserPublic_KeyInfo(context);
				}
				gxTv_SdtExternalUserPublic_Keyinfo_N = false;
				SetDirty("Keyinfo");
				return gxTv_SdtExternalUserPublic_Keyinfo;
			}
			set {
				gxTv_SdtExternalUserPublic_Keyinfo_N = false;
				gxTv_SdtExternalUserPublic_Keyinfo = value;
				SetDirty("Keyinfo");
			}

		}

		public void gxTv_SdtExternalUserPublic_Keyinfo_SetNull()
		{
			gxTv_SdtExternalUserPublic_Keyinfo_N = true;
			gxTv_SdtExternalUserPublic_Keyinfo = null;
		}

		public bool gxTv_SdtExternalUserPublic_Keyinfo_IsNull()
		{
			return gxTv_SdtExternalUserPublic_Keyinfo == null;
		}
		public bool ShouldSerializegxTpr_Keyinfo_Json()
		{
				return (gxTv_SdtExternalUserPublic_Keyinfo != null && gxTv_SdtExternalUserPublic_Keyinfo.ShouldSerializeSdtJson());

		}


		[SoapElement(ElementName="ChatKeyInfo" )]
		[XmlElement(ElementName="ChatKeyInfo" )]
		public SdtExternalUserPublic_ChatKeyInfo gxTpr_Chatkeyinfo
		{
			get {
				if ( gxTv_SdtExternalUserPublic_Chatkeyinfo == null )
				{
					gxTv_SdtExternalUserPublic_Chatkeyinfo = new SdtExternalUserPublic_ChatKeyInfo(context);
				}
				gxTv_SdtExternalUserPublic_Chatkeyinfo_N = false;
				SetDirty("Chatkeyinfo");
				return gxTv_SdtExternalUserPublic_Chatkeyinfo;
			}
			set {
				gxTv_SdtExternalUserPublic_Chatkeyinfo_N = false;
				gxTv_SdtExternalUserPublic_Chatkeyinfo = value;
				SetDirty("Chatkeyinfo");
			}

		}

		public void gxTv_SdtExternalUserPublic_Chatkeyinfo_SetNull()
		{
			gxTv_SdtExternalUserPublic_Chatkeyinfo_N = true;
			gxTv_SdtExternalUserPublic_Chatkeyinfo = null;
		}

		public bool gxTv_SdtExternalUserPublic_Chatkeyinfo_IsNull()
		{
			return gxTv_SdtExternalUserPublic_Chatkeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Chatkeyinfo_Json()
		{
				return (gxTv_SdtExternalUserPublic_Chatkeyinfo != null && gxTv_SdtExternalUserPublic_Chatkeyinfo.ShouldSerializeSdtJson());

		}


		[SoapElement(ElementName="GroupsKeyInfo" )]
		[XmlElement(ElementName="GroupsKeyInfo" )]
		public SdtExternalUserPublic_GroupsKeyInfo gxTpr_Groupskeyinfo
		{
			get {
				if ( gxTv_SdtExternalUserPublic_Groupskeyinfo == null )
				{
					gxTv_SdtExternalUserPublic_Groupskeyinfo = new SdtExternalUserPublic_GroupsKeyInfo(context);
				}
				gxTv_SdtExternalUserPublic_Groupskeyinfo_N = false;
				SetDirty("Groupskeyinfo");
				return gxTv_SdtExternalUserPublic_Groupskeyinfo;
			}
			set {
				gxTv_SdtExternalUserPublic_Groupskeyinfo_N = false;
				gxTv_SdtExternalUserPublic_Groupskeyinfo = value;
				SetDirty("Groupskeyinfo");
			}

		}

		public void gxTv_SdtExternalUserPublic_Groupskeyinfo_SetNull()
		{
			gxTv_SdtExternalUserPublic_Groupskeyinfo_N = true;
			gxTv_SdtExternalUserPublic_Groupskeyinfo = null;
		}

		public bool gxTv_SdtExternalUserPublic_Groupskeyinfo_IsNull()
		{
			return gxTv_SdtExternalUserPublic_Groupskeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Groupskeyinfo_Json()
		{
				return (gxTv_SdtExternalUserPublic_Groupskeyinfo != null && gxTv_SdtExternalUserPublic_Groupskeyinfo.ShouldSerializeSdtJson());

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
			gxTv_SdtExternalUserPublic_Userinfo_N = true;


			gxTv_SdtExternalUserPublic_Keyinfo_N = true;


			gxTv_SdtExternalUserPublic_Chatkeyinfo_N = true;


			gxTv_SdtExternalUserPublic_Groupskeyinfo_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtExternalUserPublic_Isloggedin;
		 

		protected GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT gxTv_SdtExternalUserPublic_Userinfo = null;
		protected bool gxTv_SdtExternalUserPublic_Userinfo_N;
		 
		protected bool gxTv_SdtExternalUserPublic_Keyinfo_N;
		protected SdtExternalUserPublic_KeyInfo gxTv_SdtExternalUserPublic_Keyinfo = null; 

		protected bool gxTv_SdtExternalUserPublic_Chatkeyinfo_N;
		protected SdtExternalUserPublic_ChatKeyInfo gxTv_SdtExternalUserPublic_Chatkeyinfo = null; 

		protected bool gxTv_SdtExternalUserPublic_Groupskeyinfo_N;
		protected SdtExternalUserPublic_GroupsKeyInfo gxTv_SdtExternalUserPublic_Groupskeyinfo = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ExternalUserPublic", Namespace="distributedcryptography")]
	public class SdtExternalUserPublic_RESTInterface : GxGenericCollectionItem<SdtExternalUserPublic>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtExternalUserPublic_RESTInterface( ) : base()
		{	
		}

		public SdtExternalUserPublic_RESTInterface( SdtExternalUserPublic psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("isLoggedIn")]
		[JsonPropertyOrder(0)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isLoggedIn", Order=0)]
		public bool gxTpr_Isloggedin
		{
			get { 
				return sdt.gxTpr_Isloggedin;

			}
			set { 
				sdt.gxTpr_Isloggedin = value;
			}
		}

		[JsonPropertyName("UserInfo")]
		[JsonPropertyOrder(1)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="UserInfo", Order=1, EmitDefaultValue=false)]
		public GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT_RESTInterface gxTpr_Userinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Userinfo_Json())
					return new GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT_RESTInterface(sdt.gxTpr_Userinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Userinfo = value.sdt;
			}
		}

		[JsonPropertyName("KeyInfo")]
		[JsonPropertyOrder(2)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="KeyInfo", Order=2, EmitDefaultValue=false)]
		public SdtExternalUserPublic_KeyInfo_RESTInterface gxTpr_Keyinfo
		{
			get {
				if (sdt.ShouldSerializegxTpr_Keyinfo_Json())
					return new SdtExternalUserPublic_KeyInfo_RESTInterface(sdt.gxTpr_Keyinfo);
				else
					return null;

			}

			set {
				sdt.gxTpr_Keyinfo = value.sdt;
			}

		}

		[JsonPropertyName("ChatKeyInfo")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="ChatKeyInfo", Order=3, EmitDefaultValue=false)]
		public SdtExternalUserPublic_ChatKeyInfo_RESTInterface gxTpr_Chatkeyinfo
		{
			get {
				if (sdt.ShouldSerializegxTpr_Chatkeyinfo_Json())
					return new SdtExternalUserPublic_ChatKeyInfo_RESTInterface(sdt.gxTpr_Chatkeyinfo);
				else
					return null;

			}

			set {
				sdt.gxTpr_Chatkeyinfo = value.sdt;
			}

		}

		[JsonPropertyName("GroupsKeyInfo")]
		[JsonPropertyOrder(4)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="GroupsKeyInfo", Order=4, EmitDefaultValue=false)]
		public SdtExternalUserPublic_GroupsKeyInfo_RESTInterface gxTpr_Groupskeyinfo
		{
			get {
				if (sdt.ShouldSerializegxTpr_Groupskeyinfo_Json())
					return new SdtExternalUserPublic_GroupsKeyInfo_RESTInterface(sdt.gxTpr_Groupskeyinfo);
				else
					return null;

			}

			set {
				sdt.gxTpr_Groupskeyinfo = value.sdt;
			}

		}


		#endregion
		[JsonIgnore]
		public SdtExternalUserPublic sdt
		{
			get { 
				return (SdtExternalUserPublic)Sdt;
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
				sdt = new SdtExternalUserPublic() ;
			}
		}
	}
	#endregion
}