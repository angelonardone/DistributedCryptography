/*
				   File: type_SdtSDT_Rest_Parameters_Item
			Description: SDT_Rest_Parameters
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="Item")]
	[XmlType(TypeName="Item" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_Rest_Parameters_Item : GxUserType
	{
		public SdtSDT_Rest_Parameters_Item( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_Rest_Parameters_Item_Hostname = "";

			gxTv_SdtSDT_Rest_Parameters_Item_Restpath = "";

			gxTv_SdtSDT_Rest_Parameters_Item_Networktype = "";

		}

		public SdtSDT_Rest_Parameters_Item(IGxContext context)
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
			AddObjectProperty("HostName", gxTpr_Hostname, false);


			AddObjectProperty("Port", gxTpr_Port, false);


			AddObjectProperty("Secure", gxTpr_Secure, false);


			AddObjectProperty("RestPath", gxTpr_Restpath, false);


			AddObjectProperty("NetworkType", gxTpr_Networktype, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="HostName")]
		[XmlElement(ElementName="HostName")]
		public string gxTpr_Hostname
		{
			get {
				return gxTv_SdtSDT_Rest_Parameters_Item_Hostname; 
			}
			set {
				gxTv_SdtSDT_Rest_Parameters_Item_Hostname = value;
				SetDirty("Hostname");
			}
		}




		[SoapElement(ElementName="Port")]
		[XmlElement(ElementName="Port")]
		public short gxTpr_Port
		{
			get {
				return gxTv_SdtSDT_Rest_Parameters_Item_Port; 
			}
			set {
				gxTv_SdtSDT_Rest_Parameters_Item_Port = value;
				SetDirty("Port");
			}
		}




		[SoapElement(ElementName="Secure")]
		[XmlElement(ElementName="Secure")]
		public bool gxTpr_Secure
		{
			get {
				return gxTv_SdtSDT_Rest_Parameters_Item_Secure; 
			}
			set {
				gxTv_SdtSDT_Rest_Parameters_Item_Secure = value;
				SetDirty("Secure");
			}
		}




		[SoapElement(ElementName="RestPath")]
		[XmlElement(ElementName="RestPath")]
		public string gxTpr_Restpath
		{
			get {
				return gxTv_SdtSDT_Rest_Parameters_Item_Restpath; 
			}
			set {
				gxTv_SdtSDT_Rest_Parameters_Item_Restpath = value;
				SetDirty("Restpath");
			}
		}




		[SoapElement(ElementName="NetworkType")]
		[XmlElement(ElementName="NetworkType")]
		public string gxTpr_Networktype
		{
			get {
				return gxTv_SdtSDT_Rest_Parameters_Item_Networktype; 
			}
			set {
				gxTv_SdtSDT_Rest_Parameters_Item_Networktype = value;
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
			gxTv_SdtSDT_Rest_Parameters_Item_Hostname = "";


			gxTv_SdtSDT_Rest_Parameters_Item_Restpath = "";
			gxTv_SdtSDT_Rest_Parameters_Item_Networktype = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtSDT_Rest_Parameters_Item_Hostname;
		 

		protected short gxTv_SdtSDT_Rest_Parameters_Item_Port;
		 

		protected bool gxTv_SdtSDT_Rest_Parameters_Item_Secure;
		 

		protected string gxTv_SdtSDT_Rest_Parameters_Item_Restpath;
		 

		protected string gxTv_SdtSDT_Rest_Parameters_Item_Networktype;
		 


		#endregion
	}
	#region Rest interface
	[DataContract(Name=@"Item", Namespace="distributedcryptography")]
	public class SdtSDT_Rest_Parameters_Item_RESTInterface : GxGenericCollectionItem<SdtSDT_Rest_Parameters_Item>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_Rest_Parameters_Item_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_Rest_Parameters_Item_RESTInterface( SdtSDT_Rest_Parameters_Item psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="HostName", Order=0)]
		public  string gxTpr_Hostname
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Hostname);

			}
			set { 
				 sdt.gxTpr_Hostname = value;
			}
		}

		[DataMember(Name="Port", Order=1)]
		public short gxTpr_Port
		{
			get { 
				return sdt.gxTpr_Port;

			}
			set { 
				sdt.gxTpr_Port = value;
			}
		}

		[DataMember(Name="Secure", Order=2)]
		public bool gxTpr_Secure
		{
			get { 
				return sdt.gxTpr_Secure;

			}
			set { 
				sdt.gxTpr_Secure = value;
			}
		}

		[DataMember(Name="RestPath", Order=3)]
		public  string gxTpr_Restpath
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Restpath);

			}
			set { 
				 sdt.gxTpr_Restpath = value;
			}
		}

		[DataMember(Name="NetworkType", Order=4)]
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

		public SdtSDT_Rest_Parameters_Item sdt
		{
			get { 
				return (SdtSDT_Rest_Parameters_Item)Sdt;
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
				sdt = new SdtSDT_Rest_Parameters_Item() ;
			}
		}
	}
	#endregion
}