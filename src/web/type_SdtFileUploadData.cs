/*
				   File: type_SdtFileUploadData
			Description: FileUploadData
				 Author: Nemo 🐠 for C# (.NET) version 18.0.14.187820
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
	[XmlRoot(ElementName="FileUploadData")]
	[XmlType(TypeName="FileUploadData" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtFileUploadData : GxUserType
	{
		public SdtFileUploadData( )
		{
			/* Constructor for serialization */
			gxTv_SdtFileUploadData_Fullname = "";

			gxTv_SdtFileUploadData_Name = "";

			gxTv_SdtFileUploadData_Extension = "";

			gxTv_SdtFileUploadData_File = "";

		}

		public SdtFileUploadData(IGxContext context)
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
			AddObjectProperty("FullName", gxTpr_Fullname, false);


			AddObjectProperty("Name", gxTpr_Name, false);


			AddObjectProperty("Extension", gxTpr_Extension, false);


			AddObjectProperty("Size", gxTpr_Size, false);


			AddObjectProperty("File", gxTpr_File, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="FullName")]
		[XmlElement(ElementName="FullName")]
		public string gxTpr_Fullname
		{
			get {
				return gxTv_SdtFileUploadData_Fullname; 
			}
			set {
				gxTv_SdtFileUploadData_Fullname = value;
				SetDirty("Fullname");
			}
		}




		[SoapElement(ElementName="Name")]
		[XmlElement(ElementName="Name")]
		public string gxTpr_Name
		{
			get {
				return gxTv_SdtFileUploadData_Name; 
			}
			set {
				gxTv_SdtFileUploadData_Name = value;
				SetDirty("Name");
			}
		}




		[SoapElement(ElementName="Extension")]
		[XmlElement(ElementName="Extension")]
		public string gxTpr_Extension
		{
			get {
				return gxTv_SdtFileUploadData_Extension; 
			}
			set {
				gxTv_SdtFileUploadData_Extension = value;
				SetDirty("Extension");
			}
		}




		[SoapElement(ElementName="Size")]
		[XmlElement(ElementName="Size")]
		public long gxTpr_Size
		{
			get {
				return gxTv_SdtFileUploadData_Size; 
			}
			set {
				gxTv_SdtFileUploadData_Size = value;
				SetDirty("Size");
			}
		}



		[SoapElement(ElementName="File")]
		[XmlElement(ElementName="File")]
		[GxUpload()]
		public byte[] gxTpr_File_Blob
		{
			get{
				IGxContext context = this.context == null ? new GxContext() : this.context;
				return context.FileToByteArray(gxTv_SdtFileUploadData_File) ;
			}
			set {
				IGxContext context = this.context == null ? new GxContext() : this.context;
				gxTv_SdtFileUploadData_File=context.FileFromByteArray( value) ;
			}
		}[XmlIgnore]
		public string gxTpr_File
		{
			get {
				return gxTv_SdtFileUploadData_File; 
			}
			set {
				gxTv_SdtFileUploadData_File = value;
				SetDirty("File");
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
			gxTv_SdtFileUploadData_Fullname = "";
			gxTv_SdtFileUploadData_Name = "";
			gxTv_SdtFileUploadData_Extension = "";

			gxTv_SdtFileUploadData_File = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtFileUploadData_Fullname;
		 

		protected string gxTv_SdtFileUploadData_Name;
		 

		protected string gxTv_SdtFileUploadData_Extension;
		 

		protected long gxTv_SdtFileUploadData_Size;
		 

		protected string gxTv_SdtFileUploadData_File;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"FileUploadData", Namespace="distributedcryptography")]
	public class SdtFileUploadData_RESTInterface : GxGenericCollectionItem<SdtFileUploadData>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtFileUploadData_RESTInterface( ) : base()
		{	
		}

		public SdtFileUploadData_RESTInterface( SdtFileUploadData psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("FullName")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="FullName", Order=0)]
		public  string gxTpr_Fullname
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Fullname);

			}
			set { 
				 sdt.gxTpr_Fullname = value;
			}
		}

		[JsonPropertyName("Name")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Name", Order=1)]
		public  string gxTpr_Name
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Name);

			}
			set { 
				 sdt.gxTpr_Name = value;
			}
		}

		[JsonPropertyName("Extension")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="Extension", Order=2)]
		public  string gxTpr_Extension
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Extension);

			}
			set { 
				 sdt.gxTpr_Extension = value;
			}
		}

		[JsonPropertyName("Size")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="Size", Order=3)]
		public  string gxTpr_Size
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Size, 10, 0));

			}
			set { 
				sdt.gxTpr_Size = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("File")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="File", Order=4)]
		[GxUpload()]
		public  string gxTpr_File
		{
			get { 
				return PathUtil.RelativePath( sdt.gxTpr_File);

			}
			set { 
				 sdt.gxTpr_File = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtFileUploadData sdt
		{
			get { 
				return (SdtFileUploadData)Sdt;
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
				sdt = new SdtFileUploadData() ;
			}
		}
	}
	#endregion
}