/*
				   File: type_SdtEstimateSmartFee__getOutput
			Description: EstimateSmartFee__getOutput
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
	[XmlRoot(ElementName="EstimateSmartFee__getOutput")]
	[XmlType(TypeName="EstimateSmartFee__getOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtEstimateSmartFee__getOutput : GxUserType
	{
		public SdtEstimateSmartFee__getOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtEstimateSmartFee__getOutput_Error = "";

		}

		public SdtEstimateSmartFee__getOutput(IGxContext context)
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
			if (gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result != null)
			{
				AddObjectProperty("sdt_estimateSmartFee_result", gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result, false);
			}

			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="sdt_estimateSmartFee_result")]
		[XmlElement(ElementName="sdt_estimateSmartFee_result")]
		public GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result gxTpr_Sdt_estimatesmartfee_result
		{
			get {
				if ( gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result == null )
				{
					gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result = new GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result(context);
				}
				return gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result; 
			}
			set {
				gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result = value;
				SetDirty("Sdt_estimatesmartfee_result");
			}
		}
		public void gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result_SetNull()
		{
			gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result_N = true;
			gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result = null;
		}

		public bool gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result_IsNull()
		{
			return gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result == null;
		}
		public bool ShouldSerializegxTpr_Sdt_estimatesmartfee_result_Json()
		{
			return gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result != null;

		}


		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtEstimateSmartFee__getOutput_Error; 
			}
			set {
				gxTv_SdtEstimateSmartFee__getOutput_Error = value;
				SetDirty("Error");
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
			gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result_N = true;

			gxTv_SdtEstimateSmartFee__getOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result = null;
		protected bool gxTv_SdtEstimateSmartFee__getOutput_Sdt_estimatesmartfee_result_N;
		 

		protected string gxTv_SdtEstimateSmartFee__getOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"EstimateSmartFee__getOutput", Namespace="distributedcryptography")]
	public class SdtEstimateSmartFee__getOutput_RESTInterface : GxGenericCollectionItem<SdtEstimateSmartFee__getOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtEstimateSmartFee__getOutput_RESTInterface( ) : base()
		{	
		}

		public SdtEstimateSmartFee__getOutput_RESTInterface( SdtEstimateSmartFee__getOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="sdt_estimateSmartFee_result", Order=0, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result_RESTInterface gxTpr_Sdt_estimatesmartfee_result
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Sdt_estimatesmartfee_result_Json())
					return new GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result_RESTInterface(sdt.gxTpr_Sdt_estimatesmartfee_result);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Sdt_estimatesmartfee_result = value.sdt;
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

		public SdtEstimateSmartFee__getOutput sdt
		{
			get { 
				return (SdtEstimateSmartFee__getOutput)Sdt;
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
				sdt = new SdtEstimateSmartFee__getOutput() ;
			}
		}
	}
	#endregion
}