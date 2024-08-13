/*
				   File: type_SdtgetMesssages__getOutput
			Description: getMesssages__getOutput
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
	[XmlRoot(ElementName="getMesssages__getOutput")]
	[XmlType(TypeName="getMesssages__getOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtgetMesssages__getOutput : GxUserType
	{
		public SdtgetMesssages__getOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtgetMesssages__getOutput_Error = "";

		}

		public SdtgetMesssages__getOutput(IGxContext context)
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
			if (gxTv_SdtgetMesssages__getOutput_Mesages != null)
			{
				AddObjectProperty("mesages", gxTv_SdtgetMesssages__getOutput_Mesages, false);
			}

			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="mesages")]
		[XmlElement(ElementName="mesages")]
		public GeneXus.Programs.SdtDesktopApp_services_SDT_Messages gxTpr_Mesages
		{
			get {
				if ( gxTv_SdtgetMesssages__getOutput_Mesages == null )
				{
					gxTv_SdtgetMesssages__getOutput_Mesages = new GeneXus.Programs.SdtDesktopApp_services_SDT_Messages(context);
				}
				return gxTv_SdtgetMesssages__getOutput_Mesages; 
			}
			set {
				gxTv_SdtgetMesssages__getOutput_Mesages = value;
				SetDirty("Mesages");
			}
		}
		public void gxTv_SdtgetMesssages__getOutput_Mesages_SetNull()
		{
			gxTv_SdtgetMesssages__getOutput_Mesages_N = true;
			gxTv_SdtgetMesssages__getOutput_Mesages = null;
		}

		public bool gxTv_SdtgetMesssages__getOutput_Mesages_IsNull()
		{
			return gxTv_SdtgetMesssages__getOutput_Mesages == null;
		}
		public bool ShouldSerializegxTpr_Mesages_Json()
		{
			return gxTv_SdtgetMesssages__getOutput_Mesages != null;

		}


		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtgetMesssages__getOutput_Error; 
			}
			set {
				gxTv_SdtgetMesssages__getOutput_Error = value;
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
			gxTv_SdtgetMesssages__getOutput_Mesages_N = true;

			gxTv_SdtgetMesssages__getOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected GeneXus.Programs.SdtDesktopApp_services_SDT_Messages gxTv_SdtgetMesssages__getOutput_Mesages = null;
		protected bool gxTv_SdtgetMesssages__getOutput_Mesages_N;
		 

		protected string gxTv_SdtgetMesssages__getOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"getMesssages__getOutput", Namespace="distributedcryptography")]
	public class SdtgetMesssages__getOutput_RESTInterface : GxGenericCollectionItem<SdtgetMesssages__getOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtgetMesssages__getOutput_RESTInterface( ) : base()
		{	
		}

		public SdtgetMesssages__getOutput_RESTInterface( SdtgetMesssages__getOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="mesages", Order=0, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_RESTInterface gxTpr_Mesages
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Mesages_Json())
					return new GeneXus.Programs.SdtDesktopApp_services_SDT_Messages_RESTInterface(sdt.gxTpr_Mesages);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Mesages = value.sdt;
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

		public SdtgetMesssages__getOutput sdt
		{
			get { 
				return (SdtgetMesssages__getOutput)Sdt;
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
				sdt = new SdtgetMesssages__getOutput() ;
			}
		}
	}
	#endregion
}