/*
				   File: type_SdtGAMGAMRemoteTokenSDT
			Description: GAMGAMRemoteTokenSDT
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
using GeneXus.Programs.distcrypt;
namespace GeneXus.Programs.distcrypt.sso
{
	[XmlRoot(ElementName="GAMGAMRemoteTokenSDT")]
	[XmlType(TypeName="GAMGAMRemoteTokenSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGAMGAMRemoteTokenSDT : GxUserType
	{
		public SdtGAMGAMRemoteTokenSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtGAMGAMRemoteTokenSDT_Access_token = "";

			gxTv_SdtGAMGAMRemoteTokenSDT_Token_type = "";

			gxTv_SdtGAMGAMRemoteTokenSDT_Expires_in = "";

		}

		public SdtGAMGAMRemoteTokenSDT(IGxContext context)
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
			AddObjectProperty("access_token", gxTpr_Access_token, false);


			AddObjectProperty("token_type", gxTpr_Token_type, false);


			AddObjectProperty("expires_in", gxTpr_Expires_in, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="access_token")]
		[XmlElement(ElementName="access_token")]
		public string gxTpr_Access_token
		{
			get {
				return gxTv_SdtGAMGAMRemoteTokenSDT_Access_token; 
			}
			set {
				gxTv_SdtGAMGAMRemoteTokenSDT_Access_token = value;
				SetDirty("Access_token");
			}
		}




		[SoapElement(ElementName="token_type")]
		[XmlElement(ElementName="token_type")]
		public string gxTpr_Token_type
		{
			get {
				return gxTv_SdtGAMGAMRemoteTokenSDT_Token_type; 
			}
			set {
				gxTv_SdtGAMGAMRemoteTokenSDT_Token_type = value;
				SetDirty("Token_type");
			}
		}




		[SoapElement(ElementName="expires_in")]
		[XmlElement(ElementName="expires_in")]
		public string gxTpr_Expires_in
		{
			get {
				return gxTv_SdtGAMGAMRemoteTokenSDT_Expires_in; 
			}
			set {
				gxTv_SdtGAMGAMRemoteTokenSDT_Expires_in = value;
				SetDirty("Expires_in");
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
			gxTv_SdtGAMGAMRemoteTokenSDT_Access_token = "";
			gxTv_SdtGAMGAMRemoteTokenSDT_Token_type = "";
			gxTv_SdtGAMGAMRemoteTokenSDT_Expires_in = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtGAMGAMRemoteTokenSDT_Access_token;
		 

		protected string gxTv_SdtGAMGAMRemoteTokenSDT_Token_type;
		 

		protected string gxTv_SdtGAMGAMRemoteTokenSDT_Expires_in;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GAMGAMRemoteTokenSDT", Namespace="distributedcryptography")]
	public class SdtGAMGAMRemoteTokenSDT_RESTInterface : GxGenericCollectionItem<SdtGAMGAMRemoteTokenSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGAMGAMRemoteTokenSDT_RESTInterface( ) : base()
		{	
		}

		public SdtGAMGAMRemoteTokenSDT_RESTInterface( SdtGAMGAMRemoteTokenSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="access_token", Order=0)]
		public  string gxTpr_Access_token
		{
			get { 
				return sdt.gxTpr_Access_token;

			}
			set { 
				 sdt.gxTpr_Access_token = value;
			}
		}

		[DataMember(Name="token_type", Order=1)]
		public  string gxTpr_Token_type
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Token_type);

			}
			set { 
				 sdt.gxTpr_Token_type = value;
			}
		}

		[DataMember(Name="expires_in", Order=2)]
		public  string gxTpr_Expires_in
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expires_in);

			}
			set { 
				 sdt.gxTpr_Expires_in = value;
			}
		}


		#endregion

		public SdtGAMGAMRemoteTokenSDT sdt
		{
			get { 
				return (SdtGAMGAMRemoteTokenSDT)Sdt;
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
				sdt = new SdtGAMGAMRemoteTokenSDT() ;
			}
		}
	}
	#endregion
}