/*
				   File: type_SdtGxExplorer_SDT_estimateSmartFee_result
			Description: GxExplorer_SDT_estimateSmartFee_result
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
	[XmlRoot(ElementName="GxExplorer_SDT_estimateSmartFee_result")]
	[XmlType(TypeName="GxExplorer_SDT_estimateSmartFee_result" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxExplorer_SDT_estimateSmartFee_result : GxUserType
	{
		public SdtGxExplorer_SDT_estimateSmartFee_result( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Id = "";

		}

		public SdtGxExplorer_SDT_estimateSmartFee_result(IGxContext context)
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
			AddObjectProperty("id", gxTpr_Id, false);

			if (gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result != null)
			{
				AddObjectProperty("result", gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="id")]
		[XmlElement(ElementName="id")]
		public string gxTpr_Id
		{
			get {
				return gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Id; 
			}
			set {
				gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Id = value;
				SetDirty("Id");
			}
		}



		[SoapElement(ElementName="result")]
		[XmlElement(ElementName="result")]
		public GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result_result gxTpr_Result
		{
			get {
				if ( gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result == null )
				{
					gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result = new GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result_result(context);
				}
				return gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result; 
			}
			set {
				gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result = value;
				SetDirty("Result");
			}
		}
		public void gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result_SetNull()
		{
			gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result_N = true;
			gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result = null;
		}

		public bool gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result_IsNull()
		{
			return gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result == null;
		}
		public bool ShouldSerializegxTpr_Result_Json()
		{
			return gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result != null;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Id = "";

			gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Id;
		 

		protected GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result_result gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result = null;
		protected bool gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_Result_N;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"GxExplorer_SDT_estimateSmartFee_result", Namespace="distributedcryptography")]
	public class SdtGxExplorer_SDT_estimateSmartFee_result_RESTInterface : GxGenericCollectionItem<SdtGxExplorer_SDT_estimateSmartFee_result>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxExplorer_SDT_estimateSmartFee_result_RESTInterface( ) : base()
		{	
		}

		public SdtGxExplorer_SDT_estimateSmartFee_result_RESTInterface( SdtGxExplorer_SDT_estimateSmartFee_result psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="id", Order=0)]
		public  string gxTpr_Id
		{
			get { 
				return sdt.gxTpr_Id;

			}
			set { 
				 sdt.gxTpr_Id = value;
			}
		}

		[DataMember(Name="result", Order=1, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result_result_RESTInterface gxTpr_Result
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Result_Json())
					return new GeneXus.Programs.SdtGxExplorer_SDT_estimateSmartFee_result_result_RESTInterface(sdt.gxTpr_Result);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Result = value.sdt;
			}
		}


		#endregion

		public SdtGxExplorer_SDT_estimateSmartFee_result sdt
		{
			get { 
				return (SdtGxExplorer_SDT_estimateSmartFee_result)Sdt;
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
				sdt = new SdtGxExplorer_SDT_estimateSmartFee_result() ;
			}
		}
	}
	#endregion
}