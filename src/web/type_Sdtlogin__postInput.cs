/*
				   File: type_Sdtlogin__postInput
			Description: login__postInput
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="login__postInput")]
	[XmlType(TypeName="login__postInput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdtlogin__postInput : GxUserType
	{
		public Sdtlogin__postInput( )
		{
			/* Constructor for serialization */
			gxTv_Sdtlogin__postInput_Username = "";

			gxTv_Sdtlogin__postInput_Challenge = "";

			gxTv_Sdtlogin__postInput_Signature = "";

			gxTv_Sdtlogin__postInput_Userpubkey = "";

		}

		public Sdtlogin__postInput(IGxContext context)
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
			AddObjectProperty("UserName", gxTpr_Username, false);


			AddObjectProperty("Challenge", gxTpr_Challenge, false);


			AddObjectProperty("Signature", gxTpr_Signature, false);


			AddObjectProperty("UserPubKey", gxTpr_Userpubkey, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="UserName")]
		[XmlElement(ElementName="UserName")]
		public string gxTpr_Username
		{
			get {
				return gxTv_Sdtlogin__postInput_Username; 
			}
			set {
				gxTv_Sdtlogin__postInput_Username = value;
				SetDirty("Username");
			}
		}




		[SoapElement(ElementName="Challenge")]
		[XmlElement(ElementName="Challenge")]
		public string gxTpr_Challenge
		{
			get {
				return gxTv_Sdtlogin__postInput_Challenge; 
			}
			set {
				gxTv_Sdtlogin__postInput_Challenge = value;
				SetDirty("Challenge");
			}
		}




		[SoapElement(ElementName="Signature")]
		[XmlElement(ElementName="Signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_Sdtlogin__postInput_Signature; 
			}
			set {
				gxTv_Sdtlogin__postInput_Signature = value;
				SetDirty("Signature");
			}
		}




		[SoapElement(ElementName="UserPubKey")]
		[XmlElement(ElementName="UserPubKey")]
		public string gxTpr_Userpubkey
		{
			get {
				return gxTv_Sdtlogin__postInput_Userpubkey; 
			}
			set {
				gxTv_Sdtlogin__postInput_Userpubkey = value;
				SetDirty("Userpubkey");
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
			gxTv_Sdtlogin__postInput_Username = "";
			gxTv_Sdtlogin__postInput_Challenge = "";
			gxTv_Sdtlogin__postInput_Signature = "";
			gxTv_Sdtlogin__postInput_Userpubkey = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_Sdtlogin__postInput_Username;
		 

		protected string gxTv_Sdtlogin__postInput_Challenge;
		 

		protected string gxTv_Sdtlogin__postInput_Signature;
		 

		protected string gxTv_Sdtlogin__postInput_Userpubkey;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"login__postInput", Namespace="distributedcryptography")]
	public class Sdtlogin__postInput_RESTInterface : GxGenericCollectionItem<Sdtlogin__postInput>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdtlogin__postInput_RESTInterface( ) : base()
		{	
		}

		public Sdtlogin__postInput_RESTInterface( Sdtlogin__postInput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("UserName")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="UserName", Order=0)]
		public  string gxTpr_Username
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Username);

			}
			set { 
				 sdt.gxTpr_Username = value;
			}
		}

		[JsonPropertyName("Challenge")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Challenge", Order=1)]
		public  string gxTpr_Challenge
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Challenge);

			}
			set { 
				 sdt.gxTpr_Challenge = value;
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

		[JsonPropertyName("UserPubKey")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="UserPubKey", Order=3)]
		public  string gxTpr_Userpubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Userpubkey);

			}
			set { 
				 sdt.gxTpr_Userpubkey = value;
			}
		}


		#endregion
		[JsonIgnore]
		public Sdtlogin__postInput sdt
		{
			get { 
				return (Sdtlogin__postInput)Sdt;
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
				sdt = new Sdtlogin__postInput() ;
			}
		}
	}
	#endregion
}