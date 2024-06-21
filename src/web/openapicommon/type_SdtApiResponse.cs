/*
				   File: type_SdtApiResponse
			Description: ApiResponse
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
namespace GeneXus.Programs.openapicommon
{
	[XmlRoot(ElementName="ApiResponse")]
	[XmlType(TypeName="ApiResponse" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtApiResponse : GxUserType
	{
		public SdtApiResponse( )
		{
			/* Constructor for serialization */
			gxTv_SdtApiResponse_Content = "";

			gxTv_SdtApiResponse_Errormessage = "";

		}

		public SdtApiResponse(IGxContext context)
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
			AddObjectProperty("StatusCode", gxTpr_Statuscode, false);


			AddObjectProperty("Content", gxTpr_Content, false);


			AddObjectProperty("ErrorCode", gxTpr_Errorcode, false);


			AddObjectProperty("ErrorMessage", gxTpr_Errormessage, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="StatusCode")]
		[XmlElement(ElementName="StatusCode")]
		public int gxTpr_Statuscode
		{
			get {
				return gxTv_SdtApiResponse_Statuscode; 
			}
			set {
				gxTv_SdtApiResponse_Statuscode = value;
				SetDirty("Statuscode");
			}
		}




		[SoapElement(ElementName="Content")]
		[XmlElement(ElementName="Content")]
		public string gxTpr_Content
		{
			get {
				return gxTv_SdtApiResponse_Content; 
			}
			set {
				gxTv_SdtApiResponse_Content = value;
				SetDirty("Content");
			}
		}




		[SoapElement(ElementName="ErrorCode")]
		[XmlElement(ElementName="ErrorCode")]
		public short gxTpr_Errorcode
		{
			get {
				return gxTv_SdtApiResponse_Errorcode; 
			}
			set {
				gxTv_SdtApiResponse_Errorcode = value;
				SetDirty("Errorcode");
			}
		}




		[SoapElement(ElementName="ErrorMessage")]
		[XmlElement(ElementName="ErrorMessage")]
		public string gxTpr_Errormessage
		{
			get {
				return gxTv_SdtApiResponse_Errormessage; 
			}
			set {
				gxTv_SdtApiResponse_Errormessage = value;
				SetDirty("Errormessage");
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
			gxTv_SdtApiResponse_Content = "";

			gxTv_SdtApiResponse_Errormessage = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected int gxTv_SdtApiResponse_Statuscode;
		 

		protected string gxTv_SdtApiResponse_Content;
		 

		protected short gxTv_SdtApiResponse_Errorcode;
		 

		protected string gxTv_SdtApiResponse_Errormessage;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"ApiResponse", Namespace="distributedcryptography")]
	public class SdtApiResponse_RESTInterface : GxGenericCollectionItem<SdtApiResponse>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtApiResponse_RESTInterface( ) : base()
		{	
		}

		public SdtApiResponse_RESTInterface( SdtApiResponse psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="StatusCode", Order=0)]
		public  string gxTpr_Statuscode
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Statuscode, 8, 0));

			}
			set { 
				sdt.gxTpr_Statuscode = (int) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="Content", Order=1)]
		public  string gxTpr_Content
		{
			get { 
				return sdt.gxTpr_Content;

			}
			set { 
				 sdt.gxTpr_Content = value;
			}
		}

		[DataMember(Name="ErrorCode", Order=2)]
		public short gxTpr_Errorcode
		{
			get { 
				return sdt.gxTpr_Errorcode;

			}
			set { 
				sdt.gxTpr_Errorcode = value;
			}
		}

		[DataMember(Name="ErrorMessage", Order=3)]
		public  string gxTpr_Errormessage
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Errormessage);

			}
			set { 
				 sdt.gxTpr_Errormessage = value;
			}
		}


		#endregion

		public SdtApiResponse sdt
		{
			get { 
				return (SdtApiResponse)Sdt;
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
				sdt = new SdtApiResponse() ;
			}
		}
	}
	#endregion
}