/*
				   File: type_SdtSDT_Json_Enc
			Description: SDT_Json_Enc
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

using GeneXus.Programs;
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="SDT_Json_Enc")]
	[XmlType(TypeName="SDT_Json_Enc" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_Json_Enc : GxUserType
	{
		public SdtSDT_Json_Enc( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_Json_Enc_Encryptedkey = "";

			gxTv_SdtSDT_Json_Enc_Iv = "";

			gxTv_SdtSDT_Json_Enc_Encryptedtext = "";

		}

		public SdtSDT_Json_Enc(IGxContext context)
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


			AddObjectProperty("IV", gxTpr_Iv, false);


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
				return gxTv_SdtSDT_Json_Enc_Encryptedkey; 
			}
			set {
				gxTv_SdtSDT_Json_Enc_Encryptedkey = value;
				SetDirty("Encryptedkey");
			}
		}




		[SoapElement(ElementName="IV")]
		[XmlElement(ElementName="IV")]
		public string gxTpr_Iv
		{
			get {
				return gxTv_SdtSDT_Json_Enc_Iv; 
			}
			set {
				gxTv_SdtSDT_Json_Enc_Iv = value;
				SetDirty("Iv");
			}
		}




		[SoapElement(ElementName="encryptedText")]
		[XmlElement(ElementName="encryptedText")]
		public string gxTpr_Encryptedtext
		{
			get {
				return gxTv_SdtSDT_Json_Enc_Encryptedtext; 
			}
			set {
				gxTv_SdtSDT_Json_Enc_Encryptedtext = value;
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
			gxTv_SdtSDT_Json_Enc_Encryptedkey = "";
			gxTv_SdtSDT_Json_Enc_Iv = "";
			gxTv_SdtSDT_Json_Enc_Encryptedtext = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDT_Json_Enc_Encryptedkey;
		 

		protected string gxTv_SdtSDT_Json_Enc_Iv;
		 

		protected string gxTv_SdtSDT_Json_Enc_Encryptedtext;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"SDT_Json_Enc", Namespace="distributedcryptography")]
	public class SdtSDT_Json_Enc_RESTInterface : GxGenericCollectionItem<SdtSDT_Json_Enc>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_Json_Enc_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_Json_Enc_RESTInterface( SdtSDT_Json_Enc psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
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

		[DataMember(Name="IV", Order=1)]
		public  string gxTpr_Iv
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Iv);

			}
			set { 
				 sdt.gxTpr_Iv = value;
			}
		}

		[DataMember(Name="encryptedText", Order=2)]
		public  string gxTpr_Encryptedtext
		{
			get { 
				return sdt.gxTpr_Encryptedtext;

			}
			set { 
				 sdt.gxTpr_Encryptedtext = value;
			}
		}


		#endregion

		public SdtSDT_Json_Enc sdt
		{
			get { 
				return (SdtSDT_Json_Enc)Sdt;
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
				sdt = new SdtSDT_Json_Enc() ;
			}
		}
	}
	#endregion
}