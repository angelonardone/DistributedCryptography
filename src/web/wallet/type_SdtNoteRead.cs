/*
				   File: type_SdtNoteRead
			Description: NoteRead
				 Author: Nemo 🐠 for C# (.NET) version 18.0.13.186676
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
	[XmlRoot(ElementName="NoteRead")]
	[XmlType(TypeName="NoteRead" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtNoteRead : GxUserType
	{
		public SdtNoteRead( )
		{
			/* Constructor for serialization */
			gxTv_SdtNoteRead_Description = "";

			gxTv_SdtNoteRead_Created = (DateTime)(DateTime.MinValue);

			gxTv_SdtNoteRead_Notefilename = "";

		}

		public SdtNoteRead(IGxContext context)
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
			AddObjectProperty("Description", gxTpr_Description, false);


			datetime_STZ = gxTpr_Created;
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
			AddObjectProperty("Created", sDateCnv, false);



			AddObjectProperty("NoteFileName", gxTpr_Notefilename, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Description")]
		[XmlElement(ElementName="Description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtNoteRead_Description; 
			}
			set {
				gxTv_SdtNoteRead_Description = value;
				SetDirty("Description");
			}
		}



		[SoapElement(ElementName="Created")]
		[XmlElement(ElementName="Created" , IsNullable=true)]
		public string gxTpr_Created_Nullable
		{
			get {
				if ( gxTv_SdtNoteRead_Created == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtNoteRead_Created).value ;
			}
			set {
				gxTv_SdtNoteRead_Created = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Created
		{
			get {
				return gxTv_SdtNoteRead_Created; 
			}
			set {
				gxTv_SdtNoteRead_Created = value;
				SetDirty("Created");
			}
		}



		[SoapElement(ElementName="NoteFileName")]
		[XmlElement(ElementName="NoteFileName")]
		public string gxTpr_Notefilename
		{
			get {
				return gxTv_SdtNoteRead_Notefilename; 
			}
			set {
				gxTv_SdtNoteRead_Notefilename = value;
				SetDirty("Notefilename");
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
			gxTv_SdtNoteRead_Description = "";
			gxTv_SdtNoteRead_Created = (DateTime)(DateTime.MinValue);
			gxTv_SdtNoteRead_Notefilename = "";
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

		protected string gxTv_SdtNoteRead_Description;
		 

		protected DateTime gxTv_SdtNoteRead_Created;
		 

		protected string gxTv_SdtNoteRead_Notefilename;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"NoteRead", Namespace="distributedcryptography")]
	public class SdtNoteRead_RESTInterface : GxGenericCollectionItem<SdtNoteRead>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtNoteRead_RESTInterface( ) : base()
		{	
		}

		public SdtNoteRead_RESTInterface( SdtNoteRead psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Description")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Description", Order=0)]
		public  string gxTpr_Description
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Description);

			}
			set { 
				 sdt.gxTpr_Description = value;
			}
		}

		[JsonPropertyName("Created")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Created", Order=1)]
		public  string gxTpr_Created
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Created,context);

			}
			set { 
				sdt.gxTpr_Created = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("NoteFileName")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="NoteFileName", Order=2)]
		public  string gxTpr_Notefilename
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Notefilename);

			}
			set { 
				 sdt.gxTpr_Notefilename = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtNoteRead sdt
		{
			get { 
				return (SdtNoteRead)Sdt;
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
				sdt = new SdtNoteRead() ;
			}
		}
	}
	#endregion
}