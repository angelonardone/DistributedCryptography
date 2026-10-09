/*
				   File: type_SdtExchangeFile
			Description: ExchangeFile
				 Author: Nemo 🐠 for C# (.NET) version 18.0.16.189595
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
	[XmlRoot(ElementName="ExchangeFile")]
	[XmlType(TypeName="ExchangeFile" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtExchangeFile : GxUserType
	{
		public SdtExchangeFile( )
		{
			/* Constructor for serialization */
			gxTv_SdtExchangeFile_Filename = "";

			gxTv_SdtExchangeFile_Filesize = "";

			gxTv_SdtExchangeFile_Modified = (DateTime)(DateTime.MinValue);

		}

		public SdtExchangeFile(IGxContext context)
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


			AddObjectProperty("FileSize", gxTpr_Filesize, false);


			datetime_STZ = gxTpr_Modified;
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
			AddObjectProperty("Modified", sDateCnv, false);


			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="FileName")]
		[XmlElement(ElementName="FileName")]
		public string gxTpr_Filename
		{
			get {
				return gxTv_SdtExchangeFile_Filename; 
			}
			set {
				gxTv_SdtExchangeFile_Filename = value;
				SetDirty("Filename");
			}
		}




		[SoapElement(ElementName="FileSize")]
		[XmlElement(ElementName="FileSize")]
		public string gxTpr_Filesize
		{
			get {
				return gxTv_SdtExchangeFile_Filesize; 
			}
			set {
				gxTv_SdtExchangeFile_Filesize = value;
				SetDirty("Filesize");
			}
		}



		[SoapElement(ElementName="Modified")]
		[XmlElement(ElementName="Modified" , IsNullable=true)]
		public string gxTpr_Modified_Nullable
		{
			get {
				if ( gxTv_SdtExchangeFile_Modified == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtExchangeFile_Modified).value ;
			}
			set {
				gxTv_SdtExchangeFile_Modified = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Modified
		{
			get {
				return gxTv_SdtExchangeFile_Modified; 
			}
			set {
				gxTv_SdtExchangeFile_Modified = value;
				SetDirty("Modified");
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
			gxTv_SdtExchangeFile_Filename = "";
			gxTv_SdtExchangeFile_Filesize = "";
			gxTv_SdtExchangeFile_Modified = (DateTime)(DateTime.MinValue);
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

		protected string gxTv_SdtExchangeFile_Filename;
		 

		protected string gxTv_SdtExchangeFile_Filesize;
		 

		protected DateTime gxTv_SdtExchangeFile_Modified;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ExchangeFile", Namespace="distributedcryptography")]
	public class SdtExchangeFile_RESTInterface : GxGenericCollectionItem<SdtExchangeFile>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtExchangeFile_RESTInterface( ) : base()
		{	
		}

		public SdtExchangeFile_RESTInterface( SdtExchangeFile psdt ) : base(psdt)
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

		[JsonPropertyName("FileSize")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="FileSize", Order=1)]
		public  string gxTpr_Filesize
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Filesize);

			}
			set { 
				 sdt.gxTpr_Filesize = value;
			}
		}

		[JsonPropertyName("Modified")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="Modified", Order=2)]
		public  string gxTpr_Modified
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Modified,context);

			}
			set { 
				sdt.gxTpr_Modified = DateTimeUtil.CToT2(value,context);
			}
		}


		#endregion
		[JsonIgnore]
		public SdtExchangeFile sdt
		{
			get { 
				return (SdtExchangeFile)Sdt;
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
				sdt = new SdtExchangeFile() ;
			}
		}
	}
	#endregion
}