/*
				   File: type_SdtGxExplorer_SDT_estimateSmartFee_result_result
			Description: GxExplorer_SDT_estimateSmartFee_result_result
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
	[XmlRoot(ElementName="GxExplorer_SDT_estimateSmartFee_result_result")]
	[XmlType(TypeName="GxExplorer_SDT_estimateSmartFee_result_result" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxExplorer_SDT_estimateSmartFee_result_result : GxUserType
	{
		public SdtGxExplorer_SDT_estimateSmartFee_result_result( )
		{
			/* Constructor for serialization */
		}

		public SdtGxExplorer_SDT_estimateSmartFee_result_result(IGxContext context)
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
			AddObjectProperty("feerate", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Feerate, 16, 8)), false);


			AddObjectProperty("blocks", gxTpr_Blocks, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="feerate")]
		[XmlElement(ElementName="feerate")]
		public string gxTpr_Feerate_double
		{
			get {
				return Convert.ToString(gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_result_Feerate, System.Globalization.CultureInfo.InvariantCulture);
			}
			set {
				gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_result_Feerate = (decimal)(Convert.ToDecimal(value, System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		[XmlIgnore]
		public decimal gxTpr_Feerate
		{
			get {
				return gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_result_Feerate; 
			}
			set {
				gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_result_Feerate = value;
				SetDirty("Feerate");
			}
		}




		[SoapElement(ElementName="blocks")]
		[XmlElement(ElementName="blocks")]
		public long gxTpr_Blocks
		{
			get {
				return gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_result_Blocks; 
			}
			set {
				gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_result_Blocks = value;
				SetDirty("Blocks");
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
			return  ;
		}



		#endregion

		#region Declaration

		protected decimal gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_result_Feerate;
		 

		protected long gxTv_SdtGxExplorer_SDT_estimateSmartFee_result_result_Blocks;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"GxExplorer_SDT_estimateSmartFee_result_result", Namespace="distributedcryptography")]
	public class SdtGxExplorer_SDT_estimateSmartFee_result_result_RESTInterface : GxGenericCollectionItem<SdtGxExplorer_SDT_estimateSmartFee_result_result>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxExplorer_SDT_estimateSmartFee_result_result_RESTInterface( ) : base()
		{	
		}

		public SdtGxExplorer_SDT_estimateSmartFee_result_result_RESTInterface( SdtGxExplorer_SDT_estimateSmartFee_result_result psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="feerate", Order=0)]
		public  string gxTpr_Feerate
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str(  sdt.gxTpr_Feerate, 16, 8));

			}
			set { 
				sdt.gxTpr_Feerate =  NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="blocks", Order=1)]
		public  string gxTpr_Blocks
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Blocks, 10, 0));

			}
			set { 
				sdt.gxTpr_Blocks = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion

		public SdtGxExplorer_SDT_estimateSmartFee_result_result sdt
		{
			get { 
				return (SdtGxExplorer_SDT_estimateSmartFee_result_result)Sdt;
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
				sdt = new SdtGxExplorer_SDT_estimateSmartFee_result_result() ;
			}
		}
	}
	#endregion
}