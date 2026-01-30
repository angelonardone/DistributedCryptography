/*
				   File: type_SdtgetUserPubKey__getOutput
			Description: getUserPubKey__getOutput
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="getUserPubKey__getOutput")]
	[XmlType(TypeName="getUserPubKey__getOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtgetUserPubKey__getOutput : GxUserType
	{
		public SdtgetUserPubKey__getOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtgetUserPubKey__getOutput_Userpubkey = "";

			gxTv_SdtgetUserPubKey__getOutput_Error = "";

		}

		public SdtgetUserPubKey__getOutput(IGxContext context)
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
			AddObjectProperty("UserPubKey", gxTpr_Userpubkey, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="UserPubKey")]
		[XmlElement(ElementName="UserPubKey")]
		public string gxTpr_Userpubkey
		{
			get {
				return gxTv_SdtgetUserPubKey__getOutput_Userpubkey; 
			}
			set {
				gxTv_SdtgetUserPubKey__getOutput_Userpubkey = value;
				SetDirty("Userpubkey");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtgetUserPubKey__getOutput_Error; 
			}
			set {
				gxTv_SdtgetUserPubKey__getOutput_Error = value;
				SetDirty("Error");
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
			gxTv_SdtgetUserPubKey__getOutput_Userpubkey = "";
			gxTv_SdtgetUserPubKey__getOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtgetUserPubKey__getOutput_Userpubkey;
		 

		protected string gxTv_SdtgetUserPubKey__getOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"getUserPubKey__getOutput", Namespace="distributedcryptography")]
	public class SdtgetUserPubKey__getOutput_RESTInterface : GxGenericCollectionItem<SdtgetUserPubKey__getOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtgetUserPubKey__getOutput_RESTInterface( ) : base()
		{	
		}

		public SdtgetUserPubKey__getOutput_RESTInterface( SdtgetUserPubKey__getOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("UserPubKey")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="UserPubKey", Order=0)]
		public  string gxTpr_Userpubkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Userpubkey);

			}
			set { 
				 sdt.gxTpr_Userpubkey = value;
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


		#endregion
		[JsonIgnore]
		public SdtgetUserPubKey__getOutput sdt
		{
			get { 
				return (SdtgetUserPubKey__getOutput)Sdt;
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
				sdt = new SdtgetUserPubKey__getOutput() ;
			}
		}
	}
	#endregion
}