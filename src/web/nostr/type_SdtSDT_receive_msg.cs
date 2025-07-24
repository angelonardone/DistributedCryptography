/*
				   File: type_SdtSDT_receive_msg
			Description: SDT_receive_msg
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
namespace GeneXus.Programs.nostr
{
	[XmlRoot(ElementName="SDT_receive_msg")]
	[XmlType(TypeName="SDT_receive_msg" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_receive_msg : GxUserType
	{
		public SdtSDT_receive_msg( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_receive_msg_Topic = "";

			gxTv_SdtSDT_receive_msg_Datetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtSDT_receive_msg_Msessageenc = "";

		}

		public SdtSDT_receive_msg(IGxContext context)
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
			AddObjectProperty("topic", gxTpr_Topic, false);


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



			AddObjectProperty("msessageEnc", gxTpr_Msessageenc, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="topic")]
		[XmlElement(ElementName="topic")]
		public string gxTpr_Topic
		{
			get {
				return gxTv_SdtSDT_receive_msg_Topic; 
			}
			set {
				gxTv_SdtSDT_receive_msg_Topic = value;
				SetDirty("Topic");
			}
		}



		[SoapElement(ElementName="dateTime")]
		[XmlElement(ElementName="dateTime" , IsNullable=true)]
		public string gxTpr_Datetime_Nullable
		{
			get {
				if ( gxTv_SdtSDT_receive_msg_Datetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtSDT_receive_msg_Datetime).value ;
			}
			set {
				gxTv_SdtSDT_receive_msg_Datetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Datetime
		{
			get {
				return gxTv_SdtSDT_receive_msg_Datetime; 
			}
			set {
				gxTv_SdtSDT_receive_msg_Datetime = value;
				SetDirty("Datetime");
			}
		}



		[SoapElement(ElementName="msessageEnc")]
		[XmlElement(ElementName="msessageEnc")]
		public string gxTpr_Msessageenc
		{
			get {
				return gxTv_SdtSDT_receive_msg_Msessageenc; 
			}
			set {
				gxTv_SdtSDT_receive_msg_Msessageenc = value;
				SetDirty("Msessageenc");
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
			gxTv_SdtSDT_receive_msg_Topic = "";
			gxTv_SdtSDT_receive_msg_Datetime = (DateTime)(DateTime.MinValue);
			gxTv_SdtSDT_receive_msg_Msessageenc = "";
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

		protected string gxTv_SdtSDT_receive_msg_Topic;
		 

		protected DateTime gxTv_SdtSDT_receive_msg_Datetime;
		 

		protected string gxTv_SdtSDT_receive_msg_Msessageenc;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"SDT_receive_msg", Namespace="distributedcryptography")]
	public class SdtSDT_receive_msg_RESTInterface : GxGenericCollectionItem<SdtSDT_receive_msg>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_receive_msg_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_receive_msg_RESTInterface( SdtSDT_receive_msg psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("topic")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="topic", Order=0)]
		public  string gxTpr_Topic
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Topic);

			}
			set { 
				 sdt.gxTpr_Topic = value;
			}
		}

		[JsonPropertyName("dateTime")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="dateTime", Order=1)]
		public  string gxTpr_Datetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Datetime,context);

			}
			set { 
				sdt.gxTpr_Datetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("msessageEnc")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="msessageEnc", Order=2)]
		public  string gxTpr_Msessageenc
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Msessageenc);

			}
			set { 
				 sdt.gxTpr_Msessageenc = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtSDT_receive_msg sdt
		{
			get { 
				return (SdtSDT_receive_msg)Sdt;
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
				sdt = new SdtSDT_receive_msg() ;
			}
		}
	}
	#endregion
}