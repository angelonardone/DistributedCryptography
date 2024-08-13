/*
				   File: type_SdtresultSendRawTransaction
			Description: resultSendRawTransaction
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
	[XmlRoot(ElementName="resultSendRawTransaction")]
	[XmlType(TypeName="resultSendRawTransaction" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtresultSendRawTransaction : GxUserType
	{
		public SdtresultSendRawTransaction( )
		{
			/* Constructor for serialization */
			gxTv_SdtresultSendRawTransaction_Result = "";

			gxTv_SdtresultSendRawTransaction_Error = "";

			gxTv_SdtresultSendRawTransaction_Id = "";

		}

		public SdtresultSendRawTransaction(IGxContext context)
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
			AddObjectProperty("result", gxTpr_Result, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("id", gxTpr_Id, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="result")]
		[XmlElement(ElementName="result")]
		public string gxTpr_Result
		{
			get {
				return gxTv_SdtresultSendRawTransaction_Result; 
			}
			set {
				gxTv_SdtresultSendRawTransaction_Result = value;
				SetDirty("Result");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtresultSendRawTransaction_Error; 
			}
			set {
				gxTv_SdtresultSendRawTransaction_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="id")]
		[XmlElement(ElementName="id")]
		public string gxTpr_Id
		{
			get {
				return gxTv_SdtresultSendRawTransaction_Id; 
			}
			set {
				gxTv_SdtresultSendRawTransaction_Id = value;
				SetDirty("Id");
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
			gxTv_SdtresultSendRawTransaction_Result = "";
			gxTv_SdtresultSendRawTransaction_Error = "";
			gxTv_SdtresultSendRawTransaction_Id = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtresultSendRawTransaction_Result;
		 

		protected string gxTv_SdtresultSendRawTransaction_Error;
		 

		protected string gxTv_SdtresultSendRawTransaction_Id;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"resultSendRawTransaction", Namespace="distributedcryptography")]
	public class SdtresultSendRawTransaction_RESTInterface : GxGenericCollectionItem<SdtresultSendRawTransaction>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtresultSendRawTransaction_RESTInterface( ) : base()
		{	
		}

		public SdtresultSendRawTransaction_RESTInterface( SdtresultSendRawTransaction psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="result", Order=0)]
		public  string gxTpr_Result
		{
			get { 
				return sdt.gxTpr_Result;

			}
			set { 
				 sdt.gxTpr_Result = value;
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

		[DataMember(Name="id", Order=2)]
		public  string gxTpr_Id
		{
			get { 
				return sdt.gxTpr_Id;

			}
			set { 
				 sdt.gxTpr_Id = value;
			}
		}


		#endregion

		public SdtresultSendRawTransaction sdt
		{
			get { 
				return (SdtresultSendRawTransaction)Sdt;
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
				sdt = new SdtresultSendRawTransaction() ;
			}
		}
	}
	#endregion
}