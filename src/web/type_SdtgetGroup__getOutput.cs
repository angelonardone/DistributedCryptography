/*
				   File: type_SdtgetGroup__getOutput
			Description: getGroup__getOutput
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
	[XmlRoot(ElementName="getGroup__getOutput")]
	[XmlType(TypeName="getGroup__getOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtgetGroup__getOutput : GxUserType
	{
		public SdtgetGroup__getOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtgetGroup__getOutput_Error = "";

		}

		public SdtgetGroup__getOutput(IGxContext context)
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
			if (gxTv_SdtgetGroup__getOutput_Group != null)
			{
				AddObjectProperty("group", gxTv_SdtgetGroup__getOutput_Group, false);
			}

			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="group")]
		[XmlElement(ElementName="group")]
		public GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem gxTpr_Group
		{
			get {
				if ( gxTv_SdtgetGroup__getOutput_Group == null )
				{
					gxTv_SdtgetGroup__getOutput_Group = new GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem(context);
				}
				return gxTv_SdtgetGroup__getOutput_Group; 
			}
			set {
				gxTv_SdtgetGroup__getOutput_Group = value;
				SetDirty("Group");
			}
		}
		public void gxTv_SdtgetGroup__getOutput_Group_SetNull()
		{
			gxTv_SdtgetGroup__getOutput_Group_N = true;
			gxTv_SdtgetGroup__getOutput_Group = null;
		}

		public bool gxTv_SdtgetGroup__getOutput_Group_IsNull()
		{
			return gxTv_SdtgetGroup__getOutput_Group == null;
		}
		public bool ShouldSerializegxTpr_Group_Json()
		{
			return gxTv_SdtgetGroup__getOutput_Group != null;

		}


		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtgetGroup__getOutput_Error; 
			}
			set {
				gxTv_SdtgetGroup__getOutput_Error = value;
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
			gxTv_SdtgetGroup__getOutput_Group_N = true;

			gxTv_SdtgetGroup__getOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem gxTv_SdtgetGroup__getOutput_Group = null;
		protected bool gxTv_SdtgetGroup__getOutput_Group_N;
		 

		protected string gxTv_SdtgetGroup__getOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"getGroup__getOutput", Namespace="distributedcryptography")]
	public class SdtgetGroup__getOutput_RESTInterface : GxGenericCollectionItem<SdtgetGroup__getOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtgetGroup__getOutput_RESTInterface( ) : base()
		{	
		}

		public SdtgetGroup__getOutput_RESTInterface( SdtgetGroup__getOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="group", Order=0, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem_RESTInterface gxTpr_Group
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Group_Json())
					return new GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_Group_GroupItem_RESTInterface(sdt.gxTpr_Group);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Group = value.sdt;
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

		public SdtgetGroup__getOutput sdt
		{
			get { 
				return (SdtgetGroup__getOutput)Sdt;
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
				sdt = new SdtgetGroup__getOutput() ;
			}
		}
	}
	#endregion
}