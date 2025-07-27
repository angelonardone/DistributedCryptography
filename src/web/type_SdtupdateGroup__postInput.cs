/*
				   File: type_SdtupdateGroup__postInput
			Description: updateGroup__postInput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186676
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
	[XmlRoot(ElementName="updateGroup__postInput")]
	[XmlType(TypeName="updateGroup__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtupdateGroup__postInput : GxUserType
	{
		public SdtupdateGroup__postInput( )
		{
			/* Constructor for serialization */
			gxTv_SdtupdateGroup__postInput_Groupencryptedkey = "";

			gxTv_SdtupdateGroup__postInput_Groupencrypted = "";

		}

		public SdtupdateGroup__postInput(IGxContext context)
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


			AddObjectProperty("GroupEncryptedKey", gxTpr_Groupencryptedkey, false);


			AddObjectProperty("GroupEncrypted", gxTpr_Groupencrypted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="GroupId")]
		[XmlElement(ElementName="GroupId")]
		public Guid gxTpr_Groupid
		{
			get {
				return gxTv_SdtupdateGroup__postInput_Groupid; 
			}
			set {
				gxTv_SdtupdateGroup__postInput_Groupid = value;
				SetDirty("Groupid");
			}
		}




		[SoapElement(ElementName="GroupEncryptedKey")]
		[XmlElement(ElementName="GroupEncryptedKey")]
		public string gxTpr_Groupencryptedkey
		{
			get {
				return gxTv_SdtupdateGroup__postInput_Groupencryptedkey; 
			}
			set {
				gxTv_SdtupdateGroup__postInput_Groupencryptedkey = value;
				SetDirty("Groupencryptedkey");
			}
		}




		[SoapElement(ElementName="GroupEncrypted")]
		[XmlElement(ElementName="GroupEncrypted")]
		public string gxTpr_Groupencrypted
		{
			get {
				return gxTv_SdtupdateGroup__postInput_Groupencrypted; 
			}
			set {
				gxTv_SdtupdateGroup__postInput_Groupencrypted = value;
				SetDirty("Groupencrypted");
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
			gxTv_SdtupdateGroup__postInput_Groupencryptedkey = "";
			gxTv_SdtupdateGroup__postInput_Groupencrypted = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtupdateGroup__postInput_Groupid;
		 

		protected string gxTv_SdtupdateGroup__postInput_Groupencryptedkey;
		 

		protected string gxTv_SdtupdateGroup__postInput_Groupencrypted;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"updateGroup__postInput", Namespace="distributedcryptography")]
	public class SdtupdateGroup__postInput_RESTInterface : GxGenericCollectionItem<SdtupdateGroup__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtupdateGroup__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtupdateGroup__postInput_RESTInterface( SdtupdateGroup__postInput psdt ) : base(psdt)
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

		[JsonPropertyName("GroupEncryptedKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="GroupEncryptedKey", Order=1)]
		public  string gxTpr_Groupencryptedkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Groupencryptedkey);

			}
			set { 
				 sdt.gxTpr_Groupencryptedkey = value;
			}
		}

		[JsonPropertyName("GroupEncrypted")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="GroupEncrypted", Order=2)]
		public  string gxTpr_Groupencrypted
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Groupencrypted);

			}
			set { 
				 sdt.gxTpr_Groupencrypted = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtupdateGroup__postInput sdt
		{
			get { 
				return (SdtupdateGroup__postInput)Sdt;
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
				sdt = new SdtupdateGroup__postInput() ;
			}
		}
	}
	#endregion
}