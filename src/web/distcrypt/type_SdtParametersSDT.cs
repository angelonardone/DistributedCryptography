/*
				   File: type_SdtParametersSDT
			Description: ParametersSDT
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

namespace GeneXus.Programs.distcrypt
{
	[XmlRoot(ElementName="ParametersSDT")]
	[XmlType(TypeName="ParametersSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtParametersSDT : GxUserType
	{
		public SdtParametersSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtParametersSDT_Address = "";

			gxTv_SdtParametersSDT_Publickey = "";

			gxTv_SdtParametersSDT_Signature = "";

			gxTv_SdtParametersSDT_Password = "";

			gxTv_SdtParametersSDT_Returnurl = "";

		}

		public SdtParametersSDT(IGxContext context)
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


			AddObjectProperty("PublicKey", gxTpr_Publickey, false);


			AddObjectProperty("Signature", gxTpr_Signature, false);


			AddObjectProperty("Password", gxTpr_Password, false);


			AddObjectProperty("ReturnURL", gxTpr_Returnurl, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Address")]
		[XmlElement(ElementName="Address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtParametersSDT_Address; 
			}
			set {
				gxTv_SdtParametersSDT_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="PublicKey")]
		[XmlElement(ElementName="PublicKey")]
		public string gxTpr_Publickey
		{
			get {
				return gxTv_SdtParametersSDT_Publickey; 
			}
			set {
				gxTv_SdtParametersSDT_Publickey = value;
				SetDirty("Publickey");
			}
		}




		[SoapElement(ElementName="Signature")]
		[XmlElement(ElementName="Signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtParametersSDT_Signature; 
			}
			set {
				gxTv_SdtParametersSDT_Signature = value;
				SetDirty("Signature");
			}
		}




		[SoapElement(ElementName="Password")]
		[XmlElement(ElementName="Password")]
		public string gxTpr_Password
		{
			get {
				return gxTv_SdtParametersSDT_Password; 
			}
			set {
				gxTv_SdtParametersSDT_Password = value;
				SetDirty("Password");
			}
		}




		[SoapElement(ElementName="ReturnURL")]
		[XmlElement(ElementName="ReturnURL")]
		public string gxTpr_Returnurl
		{
			get {
				return gxTv_SdtParametersSDT_Returnurl; 
			}
			set {
				gxTv_SdtParametersSDT_Returnurl = value;
				SetDirty("Returnurl");
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
			gxTv_SdtParametersSDT_Address = "";
			gxTv_SdtParametersSDT_Publickey = "";
			gxTv_SdtParametersSDT_Signature = "";
			gxTv_SdtParametersSDT_Password = "";
			gxTv_SdtParametersSDT_Returnurl = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtParametersSDT_Address;
		 

		protected string gxTv_SdtParametersSDT_Publickey;
		 

		protected string gxTv_SdtParametersSDT_Signature;
		 

		protected string gxTv_SdtParametersSDT_Password;
		 

		protected string gxTv_SdtParametersSDT_Returnurl;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ParametersSDT", Namespace="distributedcryptography")]
	public class SdtParametersSDT_RESTInterface : GxGenericCollectionItem<SdtParametersSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtParametersSDT_RESTInterface( ) : base()
		{	
		}

		public SdtParametersSDT_RESTInterface( SdtParametersSDT psdt ) : base(psdt)
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

		[JsonPropertyName("PublicKey")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="PublicKey", Order=1)]
		public  string gxTpr_Publickey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Publickey);

			}
			set { 
				 sdt.gxTpr_Publickey = value;
			}
		}

		[JsonPropertyName("Signature")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="Signature", Order=2)]
		public  string gxTpr_Signature
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signature);

			}
			set { 
				 sdt.gxTpr_Signature = value;
			}
		}

		[JsonPropertyName("Password")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="Password", Order=3)]
		public  string gxTpr_Password
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Password);

			}
			set { 
				 sdt.gxTpr_Password = value;
			}
		}

		[JsonPropertyName("ReturnURL")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="ReturnURL", Order=4)]
		public  string gxTpr_Returnurl
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Returnurl);

			}
			set { 
				 sdt.gxTpr_Returnurl = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtParametersSDT sdt
		{
			get { 
				return (SdtParametersSDT)Sdt;
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
				sdt = new SdtParametersSDT() ;
			}
		}
	}
	#endregion
}