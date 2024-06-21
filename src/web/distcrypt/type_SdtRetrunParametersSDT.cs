/*
				   File: type_SdtRetrunParametersSDT
			Description: RetrunParametersSDT
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
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
namespace GeneXus.Programs.distcrypt
{
	[XmlRoot(ElementName="RetrunParametersSDT")]
	[XmlType(TypeName="RetrunParametersSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtRetrunParametersSDT : GxUserType
	{
		public SdtRetrunParametersSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtRetrunParametersSDT_Returnurl = "";

			gxTv_SdtRetrunParametersSDT_Externaltoken = "";

		}

		public SdtRetrunParametersSDT(IGxContext context)
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
			AddObjectProperty("ReturnURL", gxTpr_Returnurl, false);


			AddObjectProperty("ExternalToken", gxTpr_Externaltoken, false);

			if (gxTv_SdtRetrunParametersSDT_Userinfo != null)
			{
				AddObjectProperty("UserInfo", gxTv_SdtRetrunParametersSDT_Userinfo, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="ReturnURL")]
		[XmlElement(ElementName="ReturnURL")]
		public string gxTpr_Returnurl
		{
			get {
				return gxTv_SdtRetrunParametersSDT_Returnurl; 
			}
			set {
				gxTv_SdtRetrunParametersSDT_Returnurl = value;
				SetDirty("Returnurl");
			}
		}




		[SoapElement(ElementName="ExternalToken")]
		[XmlElement(ElementName="ExternalToken")]
		public string gxTpr_Externaltoken
		{
			get {
				return gxTv_SdtRetrunParametersSDT_Externaltoken; 
			}
			set {
				gxTv_SdtRetrunParametersSDT_Externaltoken = value;
				SetDirty("Externaltoken");
			}
		}



		[SoapElement(ElementName="UserInfo")]
		[XmlElement(ElementName="UserInfo")]
		public GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT gxTpr_Userinfo
		{
			get {
				if ( gxTv_SdtRetrunParametersSDT_Userinfo == null )
				{
					gxTv_SdtRetrunParametersSDT_Userinfo = new GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT(context);
				}
				return gxTv_SdtRetrunParametersSDT_Userinfo; 
			}
			set {
				gxTv_SdtRetrunParametersSDT_Userinfo = value;
				SetDirty("Userinfo");
			}
		}
		public void gxTv_SdtRetrunParametersSDT_Userinfo_SetNull()
		{
			gxTv_SdtRetrunParametersSDT_Userinfo_N = true;
			gxTv_SdtRetrunParametersSDT_Userinfo = null;
		}

		public bool gxTv_SdtRetrunParametersSDT_Userinfo_IsNull()
		{
			return gxTv_SdtRetrunParametersSDT_Userinfo == null;
		}
		public bool ShouldSerializegxTpr_Userinfo_Json()
		{
			return gxTv_SdtRetrunParametersSDT_Userinfo != null;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtRetrunParametersSDT_Returnurl = "";
			gxTv_SdtRetrunParametersSDT_Externaltoken = "";

			gxTv_SdtRetrunParametersSDT_Userinfo_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtRetrunParametersSDT_Returnurl;
		 

		protected string gxTv_SdtRetrunParametersSDT_Externaltoken;
		 

		protected GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT gxTv_SdtRetrunParametersSDT_Userinfo = null;
		protected bool gxTv_SdtRetrunParametersSDT_Userinfo_N;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"RetrunParametersSDT", Namespace="distributedcryptography")]
	public class SdtRetrunParametersSDT_RESTInterface : GxGenericCollectionItem<SdtRetrunParametersSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtRetrunParametersSDT_RESTInterface( ) : base()
		{	
		}

		public SdtRetrunParametersSDT_RESTInterface( SdtRetrunParametersSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="ReturnURL", Order=0)]
		public  string gxTpr_Returnurl
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Returnurl);

			}
			set { 
				 sdt.gxTpr_Returnurl = value;
			}
		}

		[DataMember(Name="ExternalToken", Order=1)]
		public  string gxTpr_Externaltoken
		{
			get { 
				return sdt.gxTpr_Externaltoken;

			}
			set { 
				 sdt.gxTpr_Externaltoken = value;
			}
		}

		[DataMember(Name="UserInfo", Order=2, EmitDefaultValue=false)]
		public GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT_RESTInterface gxTpr_Userinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Userinfo_Json())
					return new GeneXus.Programs.distcrypt.sso.SdtGAMGAMRemoteUserSDT_RESTInterface(sdt.gxTpr_Userinfo);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Userinfo = value.sdt;
			}
		}


		#endregion

		public SdtRetrunParametersSDT sdt
		{
			get { 
				return (SdtRetrunParametersSDT)Sdt;
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
				sdt = new SdtRetrunParametersSDT() ;
			}
		}
	}
	#endregion
}