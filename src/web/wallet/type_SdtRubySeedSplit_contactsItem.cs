/*
				   File: type_SdtRubySeedSplit_contactsItem
			Description: contacts
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
	[XmlRoot(ElementName="RubySeedSplit.contactsItem")]
	[XmlType(TypeName="RubySeedSplit.contactsItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRubySeedSplit_contactsItem : GxUserType
	{
		public SdtRubySeedSplit_contactsItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtRubySeedSplit_contactsItem_Encryptedkey = "";

			gxTv_SdtRubySeedSplit_contactsItem_Encryptedtext = "";

		}

		public SdtRubySeedSplit_contactsItem(IGxContext context)
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
			AddObjectProperty("encryptedKey", gxTpr_Encryptedkey, false);


			AddObjectProperty("encryptedText", gxTpr_Encryptedtext, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="encryptedKey")]
		[XmlElement(ElementName="encryptedKey")]
		public string gxTpr_Encryptedkey
		{
			get {
				return gxTv_SdtRubySeedSplit_contactsItem_Encryptedkey; 
			}
			set {
				gxTv_SdtRubySeedSplit_contactsItem_Encryptedkey = value;
				SetDirty("Encryptedkey");
			}
		}




		[SoapElement(ElementName="encryptedText")]
		[XmlElement(ElementName="encryptedText")]
		public string gxTpr_Encryptedtext
		{
			get {
				return gxTv_SdtRubySeedSplit_contactsItem_Encryptedtext; 
			}
			set {
				gxTv_SdtRubySeedSplit_contactsItem_Encryptedtext = value;
				SetDirty("Encryptedtext");
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
			gxTv_SdtRubySeedSplit_contactsItem_Encryptedkey = "";
			gxTv_SdtRubySeedSplit_contactsItem_Encryptedtext = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtRubySeedSplit_contactsItem_Encryptedkey;
		 

		protected string gxTv_SdtRubySeedSplit_contactsItem_Encryptedtext;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"RubySeedSplit.contactsItem", Namespace="distributedcryptography")]
	public class SdtRubySeedSplit_contactsItem_RESTInterface : GxGenericCollectionItem<SdtRubySeedSplit_contactsItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRubySeedSplit_contactsItem_RESTInterface( ) : base()
		{	
		}

		public SdtRubySeedSplit_contactsItem_RESTInterface( SdtRubySeedSplit_contactsItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("encryptedKey")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="encryptedKey", Order=0)]
		public  string gxTpr_Encryptedkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedkey);

			}
			set { 
				 sdt.gxTpr_Encryptedkey = value;
			}
		}

		[JsonPropertyName("encryptedText")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="encryptedText", Order=1)]
		public  string gxTpr_Encryptedtext
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedtext);

			}
			set { 
				 sdt.gxTpr_Encryptedtext = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtRubySeedSplit_contactsItem sdt
		{
			get { 
				return (SdtRubySeedSplit_contactsItem)Sdt;
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
				sdt = new SdtRubySeedSplit_contactsItem() ;
			}
		}
	}
	#endregion
}