/*
				   File: type_SdtVaultContact
			Description: VaultContact
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

namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="VaultContact")]
	[XmlType(TypeName="VaultContact" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtVaultContact : GxUserType
	{
		public SdtVaultContact( )
		{
			/* Constructor for serialization */
			gxTv_SdtVaultContact_Contactprivatename = "";

		}

		public SdtVaultContact(IGxContext context)
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
			AddObjectProperty("contactId", gxTpr_Contactid, false);


			AddObjectProperty("contactPrivateName", gxTpr_Contactprivatename, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="contactId")]
		[XmlElement(ElementName="contactId")]
		public Guid gxTpr_Contactid
		{
			get {
				return gxTv_SdtVaultContact_Contactid; 
			}
			set {
				gxTv_SdtVaultContact_Contactid = value;
				SetDirty("Contactid");
			}
		}




		[SoapElement(ElementName="contactPrivateName")]
		[XmlElement(ElementName="contactPrivateName")]
		public string gxTpr_Contactprivatename
		{
			get {
				return gxTv_SdtVaultContact_Contactprivatename; 
			}
			set {
				gxTv_SdtVaultContact_Contactprivatename = value;
				SetDirty("Contactprivatename");
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
			gxTv_SdtVaultContact_Contactprivatename = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtVaultContact_Contactid;
		 

		protected string gxTv_SdtVaultContact_Contactprivatename;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"VaultContact", Namespace="distributedcryptography")]
	public class SdtVaultContact_RESTInterface : GxGenericCollectionItem<SdtVaultContact>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtVaultContact_RESTInterface( ) : base()
		{	
		}

		public SdtVaultContact_RESTInterface( SdtVaultContact psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("contactId")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="contactId", Order=0)]
		public Guid gxTpr_Contactid
		{
			get { 
				return sdt.gxTpr_Contactid;

			}
			set { 
				sdt.gxTpr_Contactid = value;
			}
		}

		[JsonPropertyName("contactPrivateName")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="contactPrivateName", Order=1)]
		public  string gxTpr_Contactprivatename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Contactprivatename);

			}
			set { 
				 sdt.gxTpr_Contactprivatename = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtVaultContact sdt
		{
			get { 
				return (SdtVaultContact)Sdt;
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
				sdt = new SdtVaultContact() ;
			}
		}
	}
	#endregion
}