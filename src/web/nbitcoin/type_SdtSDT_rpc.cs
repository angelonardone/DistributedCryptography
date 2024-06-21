/*
				   File: type_SdtSDT_rpc
			Description: SDT_rpc
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
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="SDT_rpc")]
	[XmlType(TypeName="SDT_rpc" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_rpc : GxUserType
	{
		public SdtSDT_rpc( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_rpc_Credential = "";

			gxTv_SdtSDT_rpc_Serveruri = "";

			gxTv_SdtSDT_rpc_Networktype = "";

		}

		public SdtSDT_rpc(IGxContext context)
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
			AddObjectProperty("credential", gxTpr_Credential, false);


			AddObjectProperty("serverURI", gxTpr_Serveruri, false);


			AddObjectProperty("networkType", gxTpr_Networktype, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="credential")]
		[XmlElement(ElementName="credential")]
		public string gxTpr_Credential
		{
			get {
				return gxTv_SdtSDT_rpc_Credential; 
			}
			set {
				gxTv_SdtSDT_rpc_Credential = value;
				SetDirty("Credential");
			}
		}




		[SoapElement(ElementName="serverURI")]
		[XmlElement(ElementName="serverURI")]
		public string gxTpr_Serveruri
		{
			get {
				return gxTv_SdtSDT_rpc_Serveruri; 
			}
			set {
				gxTv_SdtSDT_rpc_Serveruri = value;
				SetDirty("Serveruri");
			}
		}




		[SoapElement(ElementName="networkType")]
		[XmlElement(ElementName="networkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtSDT_rpc_Networktype; 
			}
			set {
				gxTv_SdtSDT_rpc_Networktype = value;
				SetDirty("Networktype");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtSDT_rpc_Credential = "";
			gxTv_SdtSDT_rpc_Serveruri = "";
			gxTv_SdtSDT_rpc_Networktype = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDT_rpc_Credential;
		 

		protected string gxTv_SdtSDT_rpc_Serveruri;
		 

		protected string gxTv_SdtSDT_rpc_Networktype;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"SDT_rpc", Namespace="distributedcryptography")]
	public class SdtSDT_rpc_RESTInterface : GxGenericCollectionItem<SdtSDT_rpc>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_rpc_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_rpc_RESTInterface( SdtSDT_rpc psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="credential", Order=0)]
		public  string gxTpr_Credential
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Credential);

			}
			set { 
				 sdt.gxTpr_Credential = value;
			}
		}

		[DataMember(Name="serverURI", Order=1)]
		public  string gxTpr_Serveruri
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Serveruri);

			}
			set { 
				 sdt.gxTpr_Serveruri = value;
			}
		}

		[DataMember(Name="networkType", Order=2)]
		public  string gxTpr_Networktype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Networktype);

			}
			set { 
				 sdt.gxTpr_Networktype = value;
			}
		}


		#endregion

		public SdtSDT_rpc sdt
		{
			get { 
				return (SdtSDT_rpc)Sdt;
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
				sdt = new SdtSDT_rpc() ;
			}
		}
	}
	#endregion
}