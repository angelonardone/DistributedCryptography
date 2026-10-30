/*
				   File: type_SdtGroupChatLine
			Description: GroupChatLine
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
using GeneXus.Programs.wallet;

namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="GroupChatLine")]
	[XmlType(TypeName="GroupChatLine" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupChatLine : GxUserType
	{
		public SdtGroupChatLine( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroupChatLine_Datetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtGroupChatLine_Who = "";

			gxTv_SdtGroupChatLine_Text = "";


		}

		public SdtGroupChatLine(IGxContext context)
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
			datetime_STZ = gxTpr_Datetime;
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
			AddObjectProperty("dateTime", sDateCnv, false);



			AddObjectProperty("who", gxTpr_Who, false);


			AddObjectProperty("text", gxTpr_Text, false);


			AddObjectProperty("mine", gxTpr_Mine, false);


			AddObjectProperty("colorIndex", gxTpr_Colorindex, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="dateTime")]
		[XmlElement(ElementName="dateTime" , IsNullable=true)]
		public string gxTpr_Datetime_Nullable
		{
			get {
				if ( gxTv_SdtGroupChatLine_Datetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGroupChatLine_Datetime).value ;
			}
			set {
				gxTv_SdtGroupChatLine_Datetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Datetime
		{
			get {
				return gxTv_SdtGroupChatLine_Datetime; 
			}
			set {
				gxTv_SdtGroupChatLine_Datetime = value;
				SetDirty("Datetime");
			}
		}



		[SoapElement(ElementName="who")]
		[XmlElement(ElementName="who")]
		public string gxTpr_Who
		{
			get {
				return gxTv_SdtGroupChatLine_Who; 
			}
			set {
				gxTv_SdtGroupChatLine_Who = value;
				SetDirty("Who");
			}
		}




		[SoapElement(ElementName="text")]
		[XmlElement(ElementName="text")]
		public string gxTpr_Text
		{
			get {
				return gxTv_SdtGroupChatLine_Text; 
			}
			set {
				gxTv_SdtGroupChatLine_Text = value;
				SetDirty("Text");
			}
		}




		[SoapElement(ElementName="mine")]
		[XmlElement(ElementName="mine")]
		public bool gxTpr_Mine
		{
			get {
				return gxTv_SdtGroupChatLine_Mine; 
			}
			set {
				gxTv_SdtGroupChatLine_Mine = value;
				SetDirty("Mine");
			}
		}




		[SoapElement(ElementName="colorIndex")]
		[XmlElement(ElementName="colorIndex")]
		public short gxTpr_Colorindex
		{
			get {
				return gxTv_SdtGroupChatLine_Colorindex; 
			}
			set {
				gxTv_SdtGroupChatLine_Colorindex = value;
				SetDirty("Colorindex");
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
			gxTv_SdtGroupChatLine_Datetime = (DateTime)(DateTime.MinValue);
			gxTv_SdtGroupChatLine_Who = "";
			gxTv_SdtGroupChatLine_Text = "";


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

		protected DateTime gxTv_SdtGroupChatLine_Datetime;
		 

		protected string gxTv_SdtGroupChatLine_Who;
		 

		protected string gxTv_SdtGroupChatLine_Text;
		 

		protected bool gxTv_SdtGroupChatLine_Mine;
		 

		protected short gxTv_SdtGroupChatLine_Colorindex;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GroupChatLine", Namespace="distributedcryptography")]
	public class SdtGroupChatLine_RESTInterface : GxGenericCollectionItem<SdtGroupChatLine>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupChatLine_RESTInterface( ) : base()
		{	
		}

		public SdtGroupChatLine_RESTInterface( SdtGroupChatLine psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("dateTime")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="dateTime", Order=0)]
		public  string gxTpr_Datetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Datetime,context);

			}
			set { 
				sdt.gxTpr_Datetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("who")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="who", Order=1)]
		public  string gxTpr_Who
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Who);

			}
			set { 
				 sdt.gxTpr_Who = value;
			}
		}

		[JsonPropertyName("text")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="text", Order=2)]
		public  string gxTpr_Text
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Text);

			}
			set { 
				 sdt.gxTpr_Text = value;
			}
		}

		[JsonPropertyName("mine")]
		[JsonPropertyOrder(3)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="mine", Order=3)]
		public bool gxTpr_Mine
		{
			get { 
				return sdt.gxTpr_Mine;

			}
			set { 
				sdt.gxTpr_Mine = value;
			}
		}

		[JsonPropertyName("colorIndex")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="colorIndex", Order=4)]
		public short gxTpr_Colorindex
		{
			get { 
				return sdt.gxTpr_Colorindex;

			}
			set { 
				sdt.gxTpr_Colorindex = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroupChatLine sdt
		{
			get { 
				return (SdtGroupChatLine)Sdt;
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
				sdt = new SdtGroupChatLine() ;
			}
		}
	}
	#endregion
}