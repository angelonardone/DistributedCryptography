/*
				   File: type_SdtRubySeedSplit
			Description: RubySeedSplit
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
	[XmlRoot(ElementName="RubySeedSplit")]
	[XmlType(TypeName="RubySeedSplit" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRubySeedSplit : GxUserType
	{
		public SdtRubySeedSplit( )
		{
			/* Constructor for serialization */
			gxTv_SdtRubySeedSplit_Error = "";

			gxTv_SdtRubySeedSplit_Masked = "";

		}

		public SdtRubySeedSplit(IGxContext context)
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
			AddObjectProperty("success", gxTpr_Success, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("cancelled", gxTpr_Cancelled, false);


			AddObjectProperty("masked", gxTpr_Masked, false);

			if (gxTv_SdtRubySeedSplit_Contacts != null)
			{
				AddObjectProperty("contacts", gxTv_SdtRubySeedSplit_Contacts, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtRubySeedSplit_Success; 
			}
			set {
				gxTv_SdtRubySeedSplit_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtRubySeedSplit_Error; 
			}
			set {
				gxTv_SdtRubySeedSplit_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="cancelled")]
		[XmlElement(ElementName="cancelled")]
		public bool gxTpr_Cancelled
		{
			get {
				return gxTv_SdtRubySeedSplit_Cancelled; 
			}
			set {
				gxTv_SdtRubySeedSplit_Cancelled = value;
				SetDirty("Cancelled");
			}
		}




		[SoapElement(ElementName="masked")]
		[XmlElement(ElementName="masked")]
		public string gxTpr_Masked
		{
			get {
				return gxTv_SdtRubySeedSplit_Masked; 
			}
			set {
				gxTv_SdtRubySeedSplit_Masked = value;
				SetDirty("Masked");
			}
		}




		[SoapElement(ElementName="contacts" )]
		[XmlArray(ElementName="contacts"  )]
		[XmlArrayItemAttribute(ElementName="contactsItem" , IsNullable=false )]
		public GXBaseCollection<SdtRubySeedSplit_contactsItem> gxTpr_Contacts
		{
			get {
				if ( gxTv_SdtRubySeedSplit_Contacts == null )
				{
					gxTv_SdtRubySeedSplit_Contacts = new GXBaseCollection<SdtRubySeedSplit_contactsItem>( context, "RubySeedSplit.contactsItem", "");
				}
				SetDirty("Contacts");
				return gxTv_SdtRubySeedSplit_Contacts;
			}
			set {
				gxTv_SdtRubySeedSplit_Contacts_N = false;
				gxTv_SdtRubySeedSplit_Contacts = value;
				SetDirty("Contacts");
			}
		}

		public void gxTv_SdtRubySeedSplit_Contacts_SetNull()
		{
			gxTv_SdtRubySeedSplit_Contacts_N = true;
			gxTv_SdtRubySeedSplit_Contacts = null;
		}

		public bool gxTv_SdtRubySeedSplit_Contacts_IsNull()
		{
			return gxTv_SdtRubySeedSplit_Contacts == null;
		}
		public bool ShouldSerializegxTpr_Contacts_GxSimpleCollection_Json()
		{
			return gxTv_SdtRubySeedSplit_Contacts != null && gxTv_SdtRubySeedSplit_Contacts.Count > 0;

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
			gxTv_SdtRubySeedSplit_Error = "";

			gxTv_SdtRubySeedSplit_Masked = "";

			gxTv_SdtRubySeedSplit_Contacts_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtRubySeedSplit_Success;
		 

		protected string gxTv_SdtRubySeedSplit_Error;
		 

		protected bool gxTv_SdtRubySeedSplit_Cancelled;
		 

		protected string gxTv_SdtRubySeedSplit_Masked;
		 
		protected bool gxTv_SdtRubySeedSplit_Contacts_N;
		protected GXBaseCollection<SdtRubySeedSplit_contactsItem> gxTv_SdtRubySeedSplit_Contacts = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"RubySeedSplit", Namespace="distributedcryptography")]
	public class SdtRubySeedSplit_RESTInterface : GxGenericCollectionItem<SdtRubySeedSplit>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRubySeedSplit_RESTInterface( ) : base()
		{	
		}

		public SdtRubySeedSplit_RESTInterface( SdtRubySeedSplit psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("success")]
		[JsonPropertyOrder(0)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="success", Order=0)]
		public bool gxTpr_Success
		{
			get { 
				return sdt.gxTpr_Success;

			}
			set { 
				sdt.gxTpr_Success = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[JsonPropertyName("cancelled")]
		[JsonPropertyOrder(2)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="cancelled", Order=2)]
		public bool gxTpr_Cancelled
		{
			get { 
				return sdt.gxTpr_Cancelled;

			}
			set { 
				sdt.gxTpr_Cancelled = value;
			}
		}

		[JsonPropertyName("masked")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="masked", Order=3)]
		public  string gxTpr_Masked
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Masked);

			}
			set { 
				 sdt.gxTpr_Masked = value;
			}
		}

		[JsonPropertyName("contacts")]
		[JsonPropertyOrder(4)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="contacts", Order=4, EmitDefaultValue=false)]
		public GxGenericCollection<SdtRubySeedSplit_contactsItem_RESTInterface> gxTpr_Contacts
		{
			get {
				if (sdt.ShouldSerializegxTpr_Contacts_GxSimpleCollection_Json())
					return new GxGenericCollection<SdtRubySeedSplit_contactsItem_RESTInterface>(sdt.gxTpr_Contacts);
				else
					return null;

			}
			set {
				value.LoadCollection(sdt.gxTpr_Contacts);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtRubySeedSplit sdt
		{
			get { 
				return (SdtRubySeedSplit)Sdt;
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
				sdt = new SdtRubySeedSplit() ;
			}
		}
	}
	#endregion
}