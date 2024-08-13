/*
				   File: type_SdtGxExplorer_services_resultGetRawTransaction
			Description: GxExplorer_services_resultGetRawTransaction
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GxExplorer_services_resultGetRawTransaction")]
	[XmlType(TypeName="GxExplorer_services_resultGetRawTransaction" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxExplorer_services_resultGetRawTransaction : GxUserType
	{
		public SdtGxExplorer_services_resultGetRawTransaction( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxExplorer_services_resultGetRawTransaction_Hextransaction = "";

			gxTv_SdtGxExplorer_services_resultGetRawTransaction_Error = "";

		}

		public SdtGxExplorer_services_resultGetRawTransaction(IGxContext context)
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
			AddObjectProperty("hexTransaction", gxTpr_Hextransaction, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="hexTransaction")]
		[XmlElement(ElementName="hexTransaction")]
		public string gxTpr_Hextransaction
		{
			get {
				return gxTv_SdtGxExplorer_services_resultGetRawTransaction_Hextransaction; 
			}
			set {
				gxTv_SdtGxExplorer_services_resultGetRawTransaction_Hextransaction = value;
				SetDirty("Hextransaction");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtGxExplorer_services_resultGetRawTransaction_Error; 
			}
			set {
				gxTv_SdtGxExplorer_services_resultGetRawTransaction_Error = value;
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
			gxTv_SdtGxExplorer_services_resultGetRawTransaction_Hextransaction = "";
			gxTv_SdtGxExplorer_services_resultGetRawTransaction_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtGxExplorer_services_resultGetRawTransaction_Hextransaction;
		 

		protected string gxTv_SdtGxExplorer_services_resultGetRawTransaction_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GxExplorer_services_resultGetRawTransaction", Namespace="distributedcryptography")]
	public class SdtGxExplorer_services_resultGetRawTransaction_RESTInterface : GxGenericCollectionItem<SdtGxExplorer_services_resultGetRawTransaction>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxExplorer_services_resultGetRawTransaction_RESTInterface( ) : base()
		{	
		}

		public SdtGxExplorer_services_resultGetRawTransaction_RESTInterface( SdtGxExplorer_services_resultGetRawTransaction psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="hexTransaction", Order=0)]
		public  string gxTpr_Hextransaction
		{
			get { 
				return sdt.gxTpr_Hextransaction;

			}
			set { 
				 sdt.gxTpr_Hextransaction = value;
			}
		}

		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return sdt.gxTpr_Error;

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}


		#endregion

		public SdtGxExplorer_services_resultGetRawTransaction sdt
		{
			get { 
				return (SdtGxExplorer_services_resultGetRawTransaction)Sdt;
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
				sdt = new SdtGxExplorer_services_resultGetRawTransaction() ;
			}
		}
	}
	#endregion
}