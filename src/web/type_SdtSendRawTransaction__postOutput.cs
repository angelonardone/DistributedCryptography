/*
				   File: type_SdtSendRawTransaction__postOutput
			Description: SendRawTransaction__postOutput
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
	[XmlRoot(ElementName="SendRawTransaction__postOutput")]
	[XmlType(TypeName="SendRawTransaction__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSendRawTransaction__postOutput : GxUserType
	{
		public SdtSendRawTransaction__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtSendRawTransaction__postOutput_Error = "";

		}

		public SdtSendRawTransaction__postOutput(IGxContext context)
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
			if (gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result != null)
			{
				AddObjectProperty("sdt_sendBlock_result", gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result, false);
			}

			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="sdt_sendBlock_result")]
		[XmlElement(ElementName="sdt_sendBlock_result")]
		public GeneXus.Programs.SdtGxExplorer_SDT_sendBlock_result gxTpr_Sdt_sendblock_result
		{
			get {
				if ( gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result == null )
				{
					gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result = new GeneXus.Programs.SdtGxExplorer_SDT_sendBlock_result(context);
				}
				return gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result; 
			}
			set {
				gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result = value;
				SetDirty("Sdt_sendblock_result");
			}
		}
		public void gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result_SetNull()
		{
			gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result_N = true;
			gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result = null;
		}

		public bool gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result_IsNull()
		{
			return gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result == null;
		}
		public bool ShouldSerializegxTpr_Sdt_sendblock_result_Json()
		{
			return gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result != null;

		}


		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtSendRawTransaction__postOutput_Error; 
			}
			set {
				gxTv_SdtSendRawTransaction__postOutput_Error = value;
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
			gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result_N = true;

			gxTv_SdtSendRawTransaction__postOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected GeneXus.Programs.SdtGxExplorer_SDT_sendBlock_result gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result = null;
		protected bool gxTv_SdtSendRawTransaction__postOutput_Sdt_sendblock_result_N;
		 

		protected string gxTv_SdtSendRawTransaction__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"SendRawTransaction__postOutput", Namespace="distributedcryptography")]
	public class SdtSendRawTransaction__postOutput_RESTInterface : GxGenericCollectionItem<SdtSendRawTransaction__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSendRawTransaction__postOutput_RESTInterface( ) : base()
		{	
		}

		public SdtSendRawTransaction__postOutput_RESTInterface( SdtSendRawTransaction__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="sdt_sendBlock_result", Order=0, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtGxExplorer_SDT_sendBlock_result_RESTInterface gxTpr_Sdt_sendblock_result
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Sdt_sendblock_result_Json())
					return new GeneXus.Programs.SdtGxExplorer_SDT_sendBlock_result_RESTInterface(sdt.gxTpr_Sdt_sendblock_result);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Sdt_sendblock_result = value.sdt;
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

		public SdtSendRawTransaction__postOutput sdt
		{
			get { 
				return (SdtSendRawTransaction__postOutput)Sdt;
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
				sdt = new SdtSendRawTransaction__postOutput() ;
			}
		}
	}
	#endregion
}