/*
				   File: type_SdtFromPassphraseCode
			Description: FromPassphraseCode
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

namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="FromPassphraseCode")]
	[XmlType(TypeName="FromPassphraseCode" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtFromPassphraseCode : GxUserType
	{
		public SdtFromPassphraseCode( )
		{
			/* Constructor for serialization */
			gxTv_SdtFromPassphraseCode_Address = "";

			gxTv_SdtFromPassphraseCode_Encriptedkey = "";

			gxTv_SdtFromPassphraseCode_Confirmationcode = "";

		}

		public SdtFromPassphraseCode(IGxContext context)
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
			AddObjectProperty("Address", gxTpr_Address, false);


			AddObjectProperty("EncriptedKey", gxTpr_Encriptedkey, false);


			AddObjectProperty("ConfirmationCode", gxTpr_Confirmationcode, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Address")]
		[XmlElement(ElementName="Address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtFromPassphraseCode_Address; 
			}
			set {
				gxTv_SdtFromPassphraseCode_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="EncriptedKey")]
		[XmlElement(ElementName="EncriptedKey")]
		public string gxTpr_Encriptedkey
		{
			get {
				return gxTv_SdtFromPassphraseCode_Encriptedkey; 
			}
			set {
				gxTv_SdtFromPassphraseCode_Encriptedkey = value;
				SetDirty("Encriptedkey");
			}
		}




		[SoapElement(ElementName="ConfirmationCode")]
		[XmlElement(ElementName="ConfirmationCode")]
		public string gxTpr_Confirmationcode
		{
			get {
				return gxTv_SdtFromPassphraseCode_Confirmationcode; 
			}
			set {
				gxTv_SdtFromPassphraseCode_Confirmationcode = value;
				SetDirty("Confirmationcode");
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
			gxTv_SdtFromPassphraseCode_Address = "";
			gxTv_SdtFromPassphraseCode_Encriptedkey = "";
			gxTv_SdtFromPassphraseCode_Confirmationcode = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtFromPassphraseCode_Address;
		 

		protected string gxTv_SdtFromPassphraseCode_Encriptedkey;
		 

		protected string gxTv_SdtFromPassphraseCode_Confirmationcode;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"FromPassphraseCode", Namespace="distributedcryptography")]
	public class SdtFromPassphraseCode_RESTInterface : GxGenericCollectionItem<SdtFromPassphraseCode>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtFromPassphraseCode_RESTInterface( ) : base()
		{	
		}

		public SdtFromPassphraseCode_RESTInterface( SdtFromPassphraseCode psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Address")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Address", Order=0)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[JsonPropertyName("EncriptedKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="EncriptedKey", Order=1)]
		public  string gxTpr_Encriptedkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encriptedkey);

			}
			set { 
				 sdt.gxTpr_Encriptedkey = value;
			}
		}

		[JsonPropertyName("ConfirmationCode")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="ConfirmationCode", Order=2)]
		public  string gxTpr_Confirmationcode
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Confirmationcode);

			}
			set { 
				 sdt.gxTpr_Confirmationcode = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtFromPassphraseCode sdt
		{
			get { 
				return (SdtFromPassphraseCode)Sdt;
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
				sdt = new SdtFromPassphraseCode() ;
			}
		}
	}
	#endregion
}