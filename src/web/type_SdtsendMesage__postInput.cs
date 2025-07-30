/*
				   File: type_SdtsendMesage__postInput
			Description: sendMesage__postInput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="sendMesage__postInput")]
	[XmlType(TypeName="sendMesage__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtsendMesage__postInput : GxUserType
	{
		public SdtsendMesage__postInput( )
		{
			/* Constructor for serialization */
			gxTv_SdtsendMesage__postInput_Username = "";

			gxTv_SdtsendMesage__postInput_Messageencryptedkey = "";

			gxTv_SdtsendMesage__postInput_Messageencrypted = "";

		}

		public SdtsendMesage__postInput(IGxContext context)
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
			AddObjectProperty("UserName", gxTpr_Username, false);


			AddObjectProperty("MessageEncryptedKey", gxTpr_Messageencryptedkey, false);


			AddObjectProperty("MessageEncrypted", gxTpr_Messageencrypted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="UserName")]
		[XmlElement(ElementName="UserName")]
		public string gxTpr_Username
		{
			get {
				return gxTv_SdtsendMesage__postInput_Username; 
			}
			set {
				gxTv_SdtsendMesage__postInput_Username = value;
				SetDirty("Username");
			}
		}




		[SoapElement(ElementName="MessageEncryptedKey")]
		[XmlElement(ElementName="MessageEncryptedKey")]
		public string gxTpr_Messageencryptedkey
		{
			get {
				return gxTv_SdtsendMesage__postInput_Messageencryptedkey; 
			}
			set {
				gxTv_SdtsendMesage__postInput_Messageencryptedkey = value;
				SetDirty("Messageencryptedkey");
			}
		}




		[SoapElement(ElementName="MessageEncrypted")]
		[XmlElement(ElementName="MessageEncrypted")]
		public string gxTpr_Messageencrypted
		{
			get {
				return gxTv_SdtsendMesage__postInput_Messageencrypted; 
			}
			set {
				gxTv_SdtsendMesage__postInput_Messageencrypted = value;
				SetDirty("Messageencrypted");
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
			gxTv_SdtsendMesage__postInput_Username = "";
			gxTv_SdtsendMesage__postInput_Messageencryptedkey = "";
			gxTv_SdtsendMesage__postInput_Messageencrypted = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtsendMesage__postInput_Username;
		 

		protected string gxTv_SdtsendMesage__postInput_Messageencryptedkey;
		 

		protected string gxTv_SdtsendMesage__postInput_Messageencrypted;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"sendMesage__postInput", Namespace="distributedcryptography")]
	public class SdtsendMesage__postInput_RESTInterface : GxGenericCollectionItem<SdtsendMesage__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtsendMesage__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtsendMesage__postInput_RESTInterface( SdtsendMesage__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("UserName")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="UserName", Order=0)]
		public  string gxTpr_Username
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Username);

			}
			set { 
				 sdt.gxTpr_Username = value;
			}
		}

		[JsonPropertyName("MessageEncryptedKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="MessageEncryptedKey", Order=1)]
		public  string gxTpr_Messageencryptedkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Messageencryptedkey);

			}
			set { 
				 sdt.gxTpr_Messageencryptedkey = value;
			}
		}

		[JsonPropertyName("MessageEncrypted")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="MessageEncrypted", Order=2)]
		public  string gxTpr_Messageencrypted
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Messageencrypted);

			}
			set { 
				 sdt.gxTpr_Messageencrypted = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtsendMesage__postInput sdt
		{
			get { 
				return (SdtsendMesage__postInput)Sdt;
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
				sdt = new SdtsendMesage__postInput() ;
			}
		}
	}
	#endregion
}