/*
				   File: type_SdtgetUserGroups__getOutput
			Description: getUserGroups__getOutput
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186702
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
using System.Text.Json.Serialization;


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="getUserGroups__getOutput")]
	[XmlType(TypeName="getUserGroups__getOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtgetUserGroups__getOutput : GxUserType
	{
		public SdtgetUserGroups__getOutput( )
		{
			/* Constructor for serialization */
			gxTv_SdtgetUserGroups__getOutput_Groups_N = true;

			gxTv_SdtgetUserGroups__getOutput_Error = "";

		}

		public SdtgetUserGroups__getOutput(IGxContext context)
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
			if (gxTv_SdtgetUserGroups__getOutput_Groups != null)
			{
				AddObjectProperty("groups", gxTv_SdtgetUserGroups__getOutput_Groups, false);
			}

			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="groups")]
		[XmlElement(ElementName="groups")]
		public GeneXus.Programs.SdtDesktopApp_services_SDT_Groups gxTpr_Groups
		{
			get {
				if ( gxTv_SdtgetUserGroups__getOutput_Groups == null )
				{
					gxTv_SdtgetUserGroups__getOutput_Groups = new GeneXus.Programs.SdtDesktopApp_services_SDT_Groups(context);
					SetDirty("Groups");
				}
				return gxTv_SdtgetUserGroups__getOutput_Groups; 
			}
			set {
				gxTv_SdtgetUserGroups__getOutput_Groups = value;
				SetDirty("Groups");
			}
		}
		public void gxTv_SdtgetUserGroups__getOutput_Groups_SetNull()
		{
			gxTv_SdtgetUserGroups__getOutput_Groups_N = true;
			gxTv_SdtgetUserGroups__getOutput_Groups = null;
		}

		public bool gxTv_SdtgetUserGroups__getOutput_Groups_IsNull()
		{
			return gxTv_SdtgetUserGroups__getOutput_Groups == null;
		}
		public bool ShouldSerializegxTpr_Groups_Json()
		{
			return gxTv_SdtgetUserGroups__getOutput_Groups != null;

		}


		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtgetUserGroups__getOutput_Error; 
			}
			set {
				gxTv_SdtgetUserGroups__getOutput_Error = value;
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
			gxTv_SdtgetUserGroups__getOutput_Groups_N = true;

			gxTv_SdtgetUserGroups__getOutput_Error = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected GeneXus.Programs.SdtDesktopApp_services_SDT_Groups gxTv_SdtgetUserGroups__getOutput_Groups = null;
		protected bool gxTv_SdtgetUserGroups__getOutput_Groups_N;
		 

		protected string gxTv_SdtgetUserGroups__getOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"getUserGroups__getOutput", Namespace="distributedcryptography")]
	public class SdtgetUserGroups__getOutput_RESTInterface : GxGenericCollectionItem<SdtgetUserGroups__getOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtgetUserGroups__getOutput_RESTInterface( ) : base()
		{	
		}

		public SdtgetUserGroups__getOutput_RESTInterface( SdtgetUserGroups__getOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("groups")]
		[JsonPropertyOrder(0)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="groups", Order=0, EmitDefaultValue=false)]
		public GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_RESTInterface gxTpr_Groups
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Groups_Json())
					return new GeneXus.Programs.SdtDesktopApp_services_SDT_Groups_RESTInterface(sdt.gxTpr_Groups);
				else
					return null;

			}
			set { 
				sdt.gxTpr_Groups = value.sdt;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtgetUserGroups__getOutput sdt
		{
			get { 
				return (SdtgetUserGroups__getOutput)Sdt;
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
				sdt = new SdtgetUserGroups__getOutput() ;
			}
		}
	}
	#endregion
}