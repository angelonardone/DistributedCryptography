/*
				   File: type_SdtExternalUserPublic_ChatKeyInfo
			Description: ChatKeyInfo
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
	[XmlRoot(ElementName="ExternalUserPublic.ChatKeyInfo")]
	[XmlType(TypeName="ExternalUserPublic.ChatKeyInfo" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtExternalUserPublic_ChatKeyInfo : GxUserType
	{
		public SdtExternalUserPublic_ChatKeyInfo( )
		{
			/* Constructor for serialization */
			gxTv_SdtExternalUserPublic_ChatKeyInfo_Publickey = "";

		}

		public SdtExternalUserPublic_ChatKeyInfo(IGxContext context)
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
			AddObjectProperty("PublicKey", gxTpr_Publickey, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="PublicKey")]
		[XmlElement(ElementName="PublicKey")]
		public string gxTpr_Publickey
		{
			get {
				return gxTv_SdtExternalUserPublic_ChatKeyInfo_Publickey; 
			}
			set {
				gxTv_SdtExternalUserPublic_ChatKeyInfo_Publickey = value;
				SetDirty("Publickey");
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
			gxTv_SdtExternalUserPublic_ChatKeyInfo_Publickey = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtExternalUserPublic_ChatKeyInfo_Publickey;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ExternalUserPublic.ChatKeyInfo", Namespace="distributedcryptography")]
	public class SdtExternalUserPublic_ChatKeyInfo_RESTInterface : GxGenericCollectionItem<SdtExternalUserPublic_ChatKeyInfo>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtExternalUserPublic_ChatKeyInfo_RESTInterface( ) : base()
		{	
		}

		public SdtExternalUserPublic_ChatKeyInfo_RESTInterface( SdtExternalUserPublic_ChatKeyInfo psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("PublicKey")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="PublicKey", Order=0)]
		public  string gxTpr_Publickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickey);

			}
			set { 
				 sdt.gxTpr_Publickey = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtExternalUserPublic_ChatKeyInfo sdt
		{
			get { 
				return (SdtExternalUserPublic_ChatKeyInfo)Sdt;
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
				sdt = new SdtExternalUserPublic_ChatKeyInfo() ;
			}
		}
	}
	#endregion
}