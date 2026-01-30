/*
				   File: type_SdtExternalUser
			Description: ExternalUser
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

namespace GeneXus.Programs.distcrypt
{
	[XmlRoot(ElementName="ExternalUser")]
	[XmlType(TypeName="ExternalUser" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtExternalUser : GxUserType
	{
		public SdtExternalUser( )
		{
			/* Constructor for serialization */
			gxTv_SdtExternalUser_Externaltoken = "";

			gxTv_SdtExternalUser_Userinfo_N = true;

			gxTv_SdtExternalUser_Keyinfo_N = true;

			gxTv_SdtExternalUser_Chatkeyinfo_N = true;

			gxTv_SdtExternalUser_Groupskeyinfo_N = true;

		}

		public SdtExternalUser(IGxContext context)
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
			AddObjectProperty("ExternalToken", gxTpr_Externaltoken, false);

			if (gxTv_SdtExternalUser_Userinfo != null)
			{
				AddObjectProperty("UserInfo", gxTv_SdtExternalUser_Userinfo, false);
			}
			if (gxTv_SdtExternalUser_Keyinfo != null)
			{
				AddObjectProperty("KeyInfo", gxTv_SdtExternalUser_Keyinfo, false);
			}
			if (gxTv_SdtExternalUser_Chatkeyinfo != null)
			{
				AddObjectProperty("ChatKeyInfo", gxTv_SdtExternalUser_Chatkeyinfo, false);
			}
			if (gxTv_SdtExternalUser_Groupskeyinfo != null)
			{
				AddObjectProperty("GroupsKeyInfo", gxTv_SdtExternalUser_Groupskeyinfo, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ExternalToken")]
		[XmlElement(ElementName="ExternalToken")]
		public string gxTpr_Externaltoken
		{
			get {
				return gxTv_SdtExternalUser_Externaltoken; 
			}
			set {
				gxTv_SdtExternalUser_Externaltoken = value;
				SetDirty("Externaltoken");
			}
		}



		[SoapElement(ElementName="UserInfo")]
		[XmlElement(ElementName="UserInfo")]
		public GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT gxTpr_Userinfo
		{
			get {
				if ( gxTv_SdtExternalUser_Userinfo == null )
				{
					gxTv_SdtExternalUser_Userinfo = new GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT(context);
					SetDirty("Userinfo");
				}
				return gxTv_SdtExternalUser_Userinfo; 
			}
			set {
				gxTv_SdtExternalUser_Userinfo = value;
				SetDirty("Userinfo");
			}
		}
		public void gxTv_SdtExternalUser_Userinfo_SetNull()
		{
			gxTv_SdtExternalUser_Userinfo_N = true;
			gxTv_SdtExternalUser_Userinfo = null;
		}

		public bool gxTv_SdtExternalUser_Userinfo_IsNull()
		{
			return gxTv_SdtExternalUser_Userinfo == null;
		}
		public bool ShouldSerializegxTpr_Userinfo_Json()
		{
			return gxTv_SdtExternalUser_Userinfo != null;

		}

		[SoapElement(ElementName="KeyInfo")]
		[XmlElement(ElementName="KeyInfo")]
		public GeneXus.Programs.nbitcoin.SdtKeyInfo gxTpr_Keyinfo
		{
			get {
				if ( gxTv_SdtExternalUser_Keyinfo == null )
				{
					gxTv_SdtExternalUser_Keyinfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
					SetDirty("Keyinfo");
				}
				return gxTv_SdtExternalUser_Keyinfo; 
			}
			set {
				gxTv_SdtExternalUser_Keyinfo = value;
				SetDirty("Keyinfo");
			}
		}
		public void gxTv_SdtExternalUser_Keyinfo_SetNull()
		{
			gxTv_SdtExternalUser_Keyinfo_N = true;
			gxTv_SdtExternalUser_Keyinfo = null;
		}

		public bool gxTv_SdtExternalUser_Keyinfo_IsNull()
		{
			return gxTv_SdtExternalUser_Keyinfo == null;
		}
		public bool ShouldSerializegxTpr_Keyinfo_Json()
		{
			return gxTv_SdtExternalUser_Keyinfo != null;

		}

		[SoapElement(ElementName="ChatKeyInfo")]
		[XmlElement(ElementName="ChatKeyInfo")]
		public GeneXus.Programs.nbitcoin.SdtKeyInfo gxTpr_Chatkeyinfo
		{
			get {
				if ( gxTv_SdtExternalUser_Chatkeyinfo == null )
				{
					gxTv_SdtExternalUser_Chatkeyinfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
					SetDirty("Chatkeyinfo");
				}
				return gxTv_SdtExternalUser_Chatkeyinfo; 
			}
			set {
				gxTv_SdtExternalUser_Chatkeyinfo = value;
				SetDirty("Chatkeyinfo");
			}
		}
		public void gxTv_SdtExternalUser_Chatkeyinfo_SetNull()
		{
			gxTv_SdtExternalUser_Chatkeyinfo_N = true;
			gxTv_SdtExternalUser_Chatkeyinfo = null;
		}

		public bool gxTv_SdtExternalUser_Chatkeyinfo_IsNull()
		{
			return gxTv_SdtExternalUser_Chatkeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Chatkeyinfo_Json()
		{
			return gxTv_SdtExternalUser_Chatkeyinfo != null;

		}

		[SoapElement(ElementName="GroupsKeyInfo")]
		[XmlElement(ElementName="GroupsKeyInfo")]
		public GeneXus.Programs.nbitcoin.SdtKeyInfo gxTpr_Groupskeyinfo
		{
			get {
				if ( gxTv_SdtExternalUser_Groupskeyinfo == null )
				{
					gxTv_SdtExternalUser_Groupskeyinfo = new GeneXus.Programs.nbitcoin.SdtKeyInfo(context);
					SetDirty("Groupskeyinfo");
				}
				return gxTv_SdtExternalUser_Groupskeyinfo; 
			}
			set {
				gxTv_SdtExternalUser_Groupskeyinfo = value;
				SetDirty("Groupskeyinfo");
			}
		}
		public void gxTv_SdtExternalUser_Groupskeyinfo_SetNull()
		{
			gxTv_SdtExternalUser_Groupskeyinfo_N = true;
			gxTv_SdtExternalUser_Groupskeyinfo = null;
		}

		public bool gxTv_SdtExternalUser_Groupskeyinfo_IsNull()
		{
			return gxTv_SdtExternalUser_Groupskeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Groupskeyinfo_Json()
		{
			return gxTv_SdtExternalUser_Groupskeyinfo != null;

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
			gxTv_SdtExternalUser_Externaltoken = "";

			gxTv_SdtExternalUser_Userinfo_N = true;


			gxTv_SdtExternalUser_Keyinfo_N = true;


			gxTv_SdtExternalUser_Chatkeyinfo_N = true;


			gxTv_SdtExternalUser_Groupskeyinfo_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtExternalUser_Externaltoken;
		 

		protected GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT gxTv_SdtExternalUser_Userinfo = null;
		protected bool gxTv_SdtExternalUser_Userinfo_N;
		 

		protected GeneXus.Programs.nbitcoin.SdtKeyInfo gxTv_SdtExternalUser_Keyinfo = null;
		protected bool gxTv_SdtExternalUser_Keyinfo_N;
		 

		protected GeneXus.Programs.nbitcoin.SdtKeyInfo gxTv_SdtExternalUser_Chatkeyinfo = null;
		protected bool gxTv_SdtExternalUser_Chatkeyinfo_N;
		 

		protected GeneXus.Programs.nbitcoin.SdtKeyInfo gxTv_SdtExternalUser_Groupskeyinfo = null;
		protected bool gxTv_SdtExternalUser_Groupskeyinfo_N;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ExternalUser", Namespace="distributedcryptography")]
	public class SdtExternalUser_RESTInterface : GxGenericCollectionItem<SdtExternalUser>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtExternalUser_RESTInterface( ) : base()
		{	
		}

		public SdtExternalUser_RESTInterface( SdtExternalUser psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("ExternalToken")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="ExternalToken", Order=0)]
		public  string gxTpr_Externaltoken
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Externaltoken);

			}
			set { 
				 sdt.gxTpr_Externaltoken = value;
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
		public GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface gxTpr_Keyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Keyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface(sdt.gxTpr_Keyinfo);
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
		public GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface gxTpr_Chatkeyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Chatkeyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface(sdt.gxTpr_Chatkeyinfo);
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
		public GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface gxTpr_Groupskeyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Groupskeyinfo_Json())
					return new GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface(sdt.gxTpr_Groupskeyinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Groupskeyinfo = value.sdt;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtExternalUser sdt
		{
			get { 
				return (SdtExternalUser)Sdt;
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
				sdt = new SdtExternalUser() ;
			}
		}
	}
	#endregion
}