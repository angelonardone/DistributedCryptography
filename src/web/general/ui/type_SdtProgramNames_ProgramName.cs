/*
				   File: type_SdtProgramNames_ProgramName
			Description: ProgramNames
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

using GeneXus.Programs;
using GeneXus.Programs.general;
namespace GeneXus.Programs.general.ui
{
	[XmlRoot(ElementName="ProgramName")]
	[XmlType(TypeName="ProgramName" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtProgramNames_ProgramName : GxUserType
	{
		public SdtProgramNames_ProgramName( )
		{
			/* Constructor for serialization */
			gxTv_SdtProgramNames_ProgramName_Name = "";

			gxTv_SdtProgramNames_ProgramName_Description = "";

			gxTv_SdtProgramNames_ProgramName_Link = "";

		}

		public SdtProgramNames_ProgramName(IGxContext context)
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
			AddObjectProperty("Name", gxTpr_Name, false);


			AddObjectProperty("Description", gxTpr_Description, false);


			AddObjectProperty("Link", gxTpr_Link, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Name")]
		[XmlElement(ElementName="Name")]
		public string gxTpr_Name
		{
			get {
				return gxTv_SdtProgramNames_ProgramName_Name; 
			}
			set {
				gxTv_SdtProgramNames_ProgramName_Name = value;
				SetDirty("Name");
			}
		}




		[SoapElement(ElementName="Description")]
		[XmlElement(ElementName="Description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtProgramNames_ProgramName_Description; 
			}
			set {
				gxTv_SdtProgramNames_ProgramName_Description = value;
				SetDirty("Description");
			}
		}




		[SoapElement(ElementName="Link")]
		[XmlElement(ElementName="Link")]
		public string gxTpr_Link
		{
			get {
				return gxTv_SdtProgramNames_ProgramName_Link; 
			}
			set {
				gxTv_SdtProgramNames_ProgramName_Link = value;
				SetDirty("Link");
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
			gxTv_SdtProgramNames_ProgramName_Name = "";
			gxTv_SdtProgramNames_ProgramName_Description = "";
			gxTv_SdtProgramNames_ProgramName_Link = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtProgramNames_ProgramName_Name;
		 

		protected string gxTv_SdtProgramNames_ProgramName_Description;
		 

		protected string gxTv_SdtProgramNames_ProgramName_Link;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"ProgramName", Namespace="distributedcryptography")]
	public class SdtProgramNames_ProgramName_RESTInterface : GxGenericCollectionItem<SdtProgramNames_ProgramName>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtProgramNames_ProgramName_RESTInterface( ) : base()
		{	
		}

		public SdtProgramNames_ProgramName_RESTInterface( SdtProgramNames_ProgramName psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Name")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Name", Order=0)]
		public  string gxTpr_Name
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Name);

			}
			set { 
				 sdt.gxTpr_Name = value;
			}
		}

		[JsonPropertyName("Description")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Description", Order=1)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[JsonPropertyName("Link")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="Link", Order=2)]
		public  string gxTpr_Link
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Link);

			}
			set { 
				 sdt.gxTpr_Link = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtProgramNames_ProgramName sdt
		{
			get { 
				return (SdtProgramNames_ProgramName)Sdt;
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
				sdt = new SdtProgramNames_ProgramName() ;
			}
		}
	}
	#endregion
}