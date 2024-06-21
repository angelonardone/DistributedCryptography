/*
				   File: type_SdtcreateContact__postInput
			Description: createContact__postInput
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
	[XmlRoot(ElementName="createContact__postInput")]
	[XmlType(TypeName="createContact__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtcreateContact__postInput : GxUserType
	{
		public SdtcreateContact__postInput( )
		{
			/* Constructor for serialization */
			gxTv_SdtcreateContact__postInput_Encryptedkey = "";

			gxTv_SdtcreateContact__postInput_Iv = "";

			gxTv_SdtcreateContact__postInput_Contactencrypted = "";

		}

		public SdtcreateContact__postInput(IGxContext context)
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
			AddObjectProperty("EncryptedKey", gxTpr_Encryptedkey, false);


			AddObjectProperty("IV", gxTpr_Iv, false);


			AddObjectProperty("ContactEncrypted", gxTpr_Contactencrypted, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="EncryptedKey")]
		[XmlElement(ElementName="EncryptedKey")]
		public string gxTpr_Encryptedkey
		{
			get {
				return gxTv_SdtcreateContact__postInput_Encryptedkey; 
			}
			set {
				gxTv_SdtcreateContact__postInput_Encryptedkey = value;
				SetDirty("Encryptedkey");
			}
		}




		[SoapElement(ElementName="IV")]
		[XmlElement(ElementName="IV")]
		public string gxTpr_Iv
		{
			get {
				return gxTv_SdtcreateContact__postInput_Iv; 
			}
			set {
				gxTv_SdtcreateContact__postInput_Iv = value;
				SetDirty("Iv");
			}
		}




		[SoapElement(ElementName="ContactEncrypted")]
		[XmlElement(ElementName="ContactEncrypted")]
		public string gxTpr_Contactencrypted
		{
			get {
				return gxTv_SdtcreateContact__postInput_Contactencrypted; 
			}
			set {
				gxTv_SdtcreateContact__postInput_Contactencrypted = value;
				SetDirty("Contactencrypted");
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
			gxTv_SdtcreateContact__postInput_Encryptedkey = "";
			gxTv_SdtcreateContact__postInput_Iv = "";
			gxTv_SdtcreateContact__postInput_Contactencrypted = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtcreateContact__postInput_Encryptedkey;
		 

		protected string gxTv_SdtcreateContact__postInput_Iv;
		 

		protected string gxTv_SdtcreateContact__postInput_Contactencrypted;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"createContact__postInput", Namespace="distributedcryptography")]
	public class SdtcreateContact__postInput_RESTInterface : GxGenericCollectionItem<SdtcreateContact__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtcreateContact__postInput_RESTInterface( ) : base()
		{	
		}

		public SdtcreateContact__postInput_RESTInterface( SdtcreateContact__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="EncryptedKey", Order=0)]
		public  string gxTpr_Encryptedkey
		{
			get { 
				return sdt.gxTpr_Encryptedkey;

			}
			set { 
				 sdt.gxTpr_Encryptedkey = value;
			}
		}

		[DataMember(Name="IV", Order=1)]
		public  string gxTpr_Iv
		{
			get { 
				return sdt.gxTpr_Iv;

			}
			set { 
				 sdt.gxTpr_Iv = value;
			}
		}

		[DataMember(Name="ContactEncrypted", Order=2)]
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

		public SdtcreateContact__postInput sdt
		{
			get { 
				return (SdtcreateContact__postInput)Sdt;
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
				sdt = new SdtcreateContact__postInput() ;
			}
		}
	}
	#endregion
}