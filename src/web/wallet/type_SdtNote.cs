/*
				   File: type_SdtNote
			Description: Note
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
	[XmlRoot(ElementName="Note")]
	[XmlType(TypeName="Note" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtNote : GxUserType
	{
		public SdtNote( )
		{
			/* Constructor for serialization */
			gxTv_SdtNote_Description = "";

			gxTv_SdtNote_Created = (DateTime)(DateTime.MinValue);

			gxTv_SdtNote_Notetext = "";

		}

		public SdtNote(IGxContext context)
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



			AddObjectProperty("NoteText", gxTpr_Notetext, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Description")]
		[XmlElement(ElementName="Description")]
		public string gxTpr_Description
		{
			get {
				return gxTv_SdtNote_Description; 
			}
			set {
				gxTv_SdtNote_Description = value;
				SetDirty("Description");
			}
		}



		[SoapElement(ElementName="Created")]
		[XmlElement(ElementName="Created" , IsNullable=true)]
		public string gxTpr_Created_Nullable
		{
			get {
				if ( gxTv_SdtNote_Created == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtNote_Created).value ;
			}
			set {
				gxTv_SdtNote_Created = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Created
		{
			get {
				return gxTv_SdtNote_Created; 
			}
			set {
				gxTv_SdtNote_Created = value;
				SetDirty("Created");
			}
		}



		[SoapElement(ElementName="NoteText")]
		[XmlElement(ElementName="NoteText")]
		public string gxTpr_Notetext
		{
			get {
				return gxTv_SdtNote_Notetext; 
			}
			set {
				gxTv_SdtNote_Notetext = value;
				SetDirty("Notetext");
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
			gxTv_SdtNote_Description = "";
			gxTv_SdtNote_Created = (DateTime)(DateTime.MinValue);
			gxTv_SdtNote_Notetext = "";
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

		protected string gxTv_SdtNote_Description;
		 

		protected DateTime gxTv_SdtNote_Created;
		 

		protected string gxTv_SdtNote_Notetext;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"Note", Namespace="distributedcryptography")]
	public class SdtNote_RESTInterface : GxGenericCollectionItem<SdtNote>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtNote_RESTInterface( ) : base()
		{	
		}

		public SdtNote_RESTInterface( SdtNote psdt ) : base(psdt)
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

		[JsonPropertyName("NoteText")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="NoteText", Order=2)]
		public  string gxTpr_Notetext
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Notetext);

			}
			set { 
				 sdt.gxTpr_Notetext = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtNote sdt
		{
			get { 
				return (SdtNote)Sdt;
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
				sdt = new SdtNote() ;
			}
		}
	}
	#endregion
}