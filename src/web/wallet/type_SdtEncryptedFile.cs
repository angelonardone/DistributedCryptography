/*
				   File: type_SdtEncryptedFile
			Description: EncryptedFile
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
namespace GeneXus.Programs.wallet
{
	[XmlRoot(ElementName="EncryptedFile")]
	[XmlType(TypeName="EncryptedFile" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtEncryptedFile : GxUserType
	{
		public SdtEncryptedFile( )
		{
			/* Constructor for serialization */
			gxTv_SdtEncryptedFile_Filename = "";

			gxTv_SdtEncryptedFile_Create = (DateTime)(DateTime.MinValue);

			gxTv_SdtEncryptedFile_Fullfilename = "";

			gxTv_SdtEncryptedFile_Encryptedkey = "";

			gxTv_SdtEncryptedFile_Iv = "";

		}

		public SdtEncryptedFile(IGxContext context)
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
			AddObjectProperty("FileName", gxTpr_Filename, false);


			datetime_STZ = gxTpr_Create;
			sDateCnv = "";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Year(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("0000", 1, 4-StringUtil.Len( sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim( StringUtil.Str((decimal)(DateTimeUtil.Month(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Day(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "T";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Hour(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Minute(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + ":";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Second(datetime_STZ)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			AddObjectProperty("Create", sDateCnv, false);



			AddObjectProperty("FullFileName", gxTpr_Fullfilename, false);


			AddObjectProperty("EncryptedKey", gxTpr_Encryptedkey, false);


			AddObjectProperty("IV", gxTpr_Iv, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="FileName")]
		[XmlElement(ElementName="FileName")]
		public string gxTpr_Filename
		{
			get {
				return gxTv_SdtEncryptedFile_Filename; 
			}
			set {
				gxTv_SdtEncryptedFile_Filename = value;
				SetDirty("Filename");
			}
		}



		[SoapElement(ElementName="Create")]
		[XmlElement(ElementName="Create" , IsNullable=true)]
		public string gxTpr_Create_Nullable
		{
			get {
				if ( gxTv_SdtEncryptedFile_Create == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtEncryptedFile_Create).value ;
			}
			set {
				gxTv_SdtEncryptedFile_Create = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Create
		{
			get {
				return gxTv_SdtEncryptedFile_Create; 
			}
			set {
				gxTv_SdtEncryptedFile_Create = value;
				SetDirty("Create");
			}
		}



		[SoapElement(ElementName="FullFileName")]
		[XmlElement(ElementName="FullFileName")]
		public string gxTpr_Fullfilename
		{
			get {
				return gxTv_SdtEncryptedFile_Fullfilename; 
			}
			set {
				gxTv_SdtEncryptedFile_Fullfilename = value;
				SetDirty("Fullfilename");
			}
		}




		[SoapElement(ElementName="EncryptedKey")]
		[XmlElement(ElementName="EncryptedKey")]
		public string gxTpr_Encryptedkey
		{
			get {
				return gxTv_SdtEncryptedFile_Encryptedkey; 
			}
			set {
				gxTv_SdtEncryptedFile_Encryptedkey = value;
				SetDirty("Encryptedkey");
			}
		}




		[SoapElement(ElementName="IV")]
		[XmlElement(ElementName="IV")]
		public string gxTpr_Iv
		{
			get {
				return gxTv_SdtEncryptedFile_Iv; 
			}
			set {
				gxTv_SdtEncryptedFile_Iv = value;
				SetDirty("Iv");
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
			gxTv_SdtEncryptedFile_Filename = "";
			gxTv_SdtEncryptedFile_Create = (DateTime)(DateTime.MinValue);
			gxTv_SdtEncryptedFile_Fullfilename = "";
			gxTv_SdtEncryptedFile_Encryptedkey = "";
			gxTv_SdtEncryptedFile_Iv = "";
			datetime_STZ = (DateTime)(DateTime.MinValue);
			sDateCnv = "";
			sNumToPad = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string sDateCnv ;
		protected string sNumToPad ;
		protected DateTime datetime_STZ ;

		protected string gxTv_SdtEncryptedFile_Filename;
		 

		protected DateTime gxTv_SdtEncryptedFile_Create;
		 

		protected string gxTv_SdtEncryptedFile_Fullfilename;
		 

		protected string gxTv_SdtEncryptedFile_Encryptedkey;
		 

		protected string gxTv_SdtEncryptedFile_Iv;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"EncryptedFile", Namespace="distributedcryptography")]
	public class SdtEncryptedFile_RESTInterface : GxGenericCollectionItem<SdtEncryptedFile>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtEncryptedFile_RESTInterface( ) : base()
		{	
		}

		public SdtEncryptedFile_RESTInterface( SdtEncryptedFile psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("FileName")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="FileName", Order=0)]
		public  string gxTpr_Filename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Filename);

			}
			set { 
				 sdt.gxTpr_Filename = value;
			}
		}

		[JsonPropertyName("Create")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Create", Order=1)]
		public  string gxTpr_Create
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Create,context);

			}
			set { 
				sdt.gxTpr_Create = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("FullFileName")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="FullFileName", Order=2)]
		public  string gxTpr_Fullfilename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Fullfilename);

			}
			set { 
				 sdt.gxTpr_Fullfilename = value;
			}
		}

		[JsonPropertyName("EncryptedKey")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="EncryptedKey", Order=3)]
		public  string gxTpr_Encryptedkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedkey);

			}
			set { 
				 sdt.gxTpr_Encryptedkey = value;
			}
		}

		[JsonPropertyName("IV")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="IV", Order=4)]
		public  string gxTpr_Iv
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Iv);

			}
			set { 
				 sdt.gxTpr_Iv = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtEncryptedFile sdt
		{
			get { 
				return (SdtEncryptedFile)Sdt;
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
				sdt = new SdtEncryptedFile() ;
			}
		}
	}
	#endregion
}