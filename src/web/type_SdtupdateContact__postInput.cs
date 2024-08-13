/*
				   File: type_SdtupdateContact__postInput
			Description: updateContact__postInput
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="updateContact__postInput")]
	[XmlType(TypeName="updateContact__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtupdateContact__postInput : GxUserType
	{
		public SdtupdateContact__postInput( )
		{
			/* Constructor for serialization */
			gxTv_SdtupdateContact__postInput_Encryptedkey = "";

			gxTv_SdtupdateContact__postInput_Iv = "";

			gxTv_SdtupdateContact__postInput_Contactencrypted = "";

		}

		public SdtupdateContact__postInput(IGxContext context)
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
			AddObjectProperty("ContactId", gxTpr_Contactid, false);


			AddObjectProperty("EncryptedKey", gxTpr_Encryptedkey, false);


			AddObjectProperty("IV", gxTpr_Iv, false);


			AddObjectProperty("ContactEncrypted", gxTpr_Contactencrypted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ContactId")]
		[XmlElement(ElementName="ContactId")]
		public Guid gxTpr_Contactid
		{
			get {
				return gxTv_SdtupdateContact__postInput_Contactid; 
			}
			set {
				gxTv_SdtupdateContact__postInput_Contactid = value;
				SetDirty("Contactid");
			}
		}




		[SoapElement(ElementName="EncryptedKey")]
		[XmlElement(ElementName="EncryptedKey")]
		public string gxTpr_Encryptedkey
		{
			get {
				return gxTv_SdtupdateContact__postInput_Encryptedkey; 
			}
			set {
				gxTv_SdtupdateContact__postInput_Encryptedkey = value;
				SetDirty("Encryptedkey");
			}
		}




		[SoapElement(ElementName="IV")]
		[XmlElement(ElementName="IV")]
		public string gxTpr_Iv
		{
			get {
				return gxTv_SdtupdateContact__postInput_Iv; 
			}
			set {
				gxTv_SdtupdateContact__postInput_Iv = value;
				SetDirty("Iv");
			}
		}




		[SoapElement(ElementName="ContactEncrypted")]
		[XmlElement(ElementName="ContactEncrypted")]
		public string gxTpr_Contactencrypted
		{
			get {
				return gxTv_SdtupdateContact__postInput_Contactencrypted; 
			}
			set {
				gxTv_SdtupdateContact__postInput_Contactencrypted = value;
				SetDirty("Contactencrypted");
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
			gxTv_SdtupdateContact__postInput_Encryptedkey = "";
			gxTv_SdtupdateContact__postInput_Iv = "";
			gxTv_SdtupdateContact__postInput_Contactencrypted = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtupdateContact__postInput_Contactid;
		 

		protected string gxTv_SdtupdateContact__postInput_Encryptedkey;
		 

		protected string gxTv_SdtupdateContact__postInput_Iv;
		 

		protected string gxTv_SdtupdateContact__postInput_Contactencrypted;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"updateContact__postInput", Namespace="distributedcryptography")]
	public class SdtupdateContact__postInput_RESTInterface : GxGenericCollectionItem<SdtupdateContact__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtupdateContact__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtupdateContact__postInput_RESTInterface( SdtupdateContact__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="ContactId", Order=0)]
		public Guid gxTpr_Contactid
		{
			get { 
				return sdt.gxTpr_Contactid;

			}
			set { 
				sdt.gxTpr_Contactid = value;
			}
		}

		[DataMember(Name="EncryptedKey", Order=1)]
		public  string gxTpr_Encryptedkey
		{
			get { 
				return sdt.gxTpr_Encryptedkey;

			}
			set { 
				 sdt.gxTpr_Encryptedkey = value;
			}
		}

		[DataMember(Name="IV", Order=2)]
		public  string gxTpr_Iv
		{
			get { 
				return sdt.gxTpr_Iv;

			}
			set { 
				 sdt.gxTpr_Iv = value;
			}
		}

		[DataMember(Name="ContactEncrypted", Order=3)]
		public  string gxTpr_Contactencrypted
		{
			get { 
				return sdt.gxTpr_Contactencrypted;

			}
			set { 
				 sdt.gxTpr_Contactencrypted = value;
			}
		}


		#endregion

		public SdtupdateContact__postInput sdt
		{
			get { 
				return (SdtupdateContact__postInput)Sdt;
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
				sdt = new SdtupdateContact__postInput() ;
			}
		}
	}
	#endregion
}