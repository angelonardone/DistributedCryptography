/*
				   File: type_SdtKeyCreate
			Description: KeyCreate
				 Author: Nemo 🐠 for C# (.NET) version 18.0.12.186073
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
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="KeyCreate")]
	[XmlType(TypeName="KeyCreate" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtKeyCreate : GxUserType
	{
		public SdtKeyCreate( )
		{
			/* Constructor for serialization */
			gxTv_SdtKeyCreate_Createtext = "";

			gxTv_SdtKeyCreate_Networktype = "";


		}

		public SdtKeyCreate(IGxContext context)
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
			AddObjectProperty("CreateKeyType", gxTpr_Createkeytype, false);


			AddObjectProperty("CreateText", gxTpr_Createtext, false);


			AddObjectProperty("NetworkType", gxTpr_Networktype, false);


			AddObjectProperty("AddressType", gxTpr_Addresstype, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="CreateKeyType")]
		[XmlElement(ElementName="CreateKeyType")]
		public short gxTpr_Createkeytype
		{
			get {
				return gxTv_SdtKeyCreate_Createkeytype; 
			}
			set {
				gxTv_SdtKeyCreate_Createkeytype = value;
				SetDirty("Createkeytype");
			}
		}




		[SoapElement(ElementName="CreateText")]
		[XmlElement(ElementName="CreateText")]
		public string gxTpr_Createtext
		{
			get {
				return gxTv_SdtKeyCreate_Createtext; 
			}
			set {
				gxTv_SdtKeyCreate_Createtext = value;
				SetDirty("Createtext");
			}
		}




		[SoapElement(ElementName="NetworkType")]
		[XmlElement(ElementName="NetworkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtKeyCreate_Networktype; 
			}
			set {
				gxTv_SdtKeyCreate_Networktype = value;
				SetDirty("Networktype");
			}
		}




		[SoapElement(ElementName="AddressType")]
		[XmlElement(ElementName="AddressType")]
		public short gxTpr_Addresstype
		{
			get {
				return gxTv_SdtKeyCreate_Addresstype; 
			}
			set {
				gxTv_SdtKeyCreate_Addresstype = value;
				SetDirty("Addresstype");
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
			gxTv_SdtKeyCreate_Createtext = "";
			gxTv_SdtKeyCreate_Networktype = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected short gxTv_SdtKeyCreate_Createkeytype;
		 

		protected string gxTv_SdtKeyCreate_Createtext;
		 

		protected string gxTv_SdtKeyCreate_Networktype;
		 

		protected short gxTv_SdtKeyCreate_Addresstype;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"KeyCreate", Namespace="distributedcryptography")]
	public class SdtKeyCreate_RESTInterface : GxGenericCollectionItem<SdtKeyCreate>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtKeyCreate_RESTInterface( ) : base()
		{	
		}

		public SdtKeyCreate_RESTInterface( SdtKeyCreate psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("CreateKeyType")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="CreateKeyType", Order=0)]
		public short gxTpr_Createkeytype
		{
			get { 
				return sdt.gxTpr_Createkeytype;

			}
			set { 
				sdt.gxTpr_Createkeytype = value;
			}
		}

		[JsonPropertyName("CreateText")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="CreateText", Order=1)]
		public  string gxTpr_Createtext
		{
			get { 
				return sdt.gxTpr_Createtext;

			}
			set { 
				 sdt.gxTpr_Createtext = value;
			}
		}

		[JsonPropertyName("NetworkType")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="NetworkType", Order=2)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}

		[JsonPropertyName("AddressType")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="AddressType", Order=3)]
		public short gxTpr_Addresstype
		{
			get { 
				return sdt.gxTpr_Addresstype;

			}
			set { 
				sdt.gxTpr_Addresstype = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtKeyCreate sdt
		{
			get { 
				return (SdtKeyCreate)Sdt;
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
				sdt = new SdtKeyCreate() ;
			}
		}
	}
	#endregion
}