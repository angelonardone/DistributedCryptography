/*
				   File: type_SdtSDT_message
			Description: SDT_message
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
namespace GeneXus.Programs.nostr
{
	[XmlRoot(ElementName="SDT_message")]
	[XmlType(TypeName="SDT_message" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_message : GxUserType
	{
		public SdtSDT_message( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_message_Message = "";

		}

		public SdtSDT_message(IGxContext context)
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
			AddObjectProperty("Id", gxTpr_Id, false);


			AddObjectProperty("MessageType", gxTpr_Messagetype, false);


			AddObjectProperty("dateTimeUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Datetimeunix, 18, 0)), false);


			AddObjectProperty("Message", gxTpr_Message, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Id")]
		[XmlElement(ElementName="Id")]
		public Guid gxTpr_Id
		{
			get {
				return gxTv_SdtSDT_message_Id; 
			}
			set {
				gxTv_SdtSDT_message_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="MessageType")]
		[XmlElement(ElementName="MessageType")]
		public int gxTpr_Messagetype
		{
			get {
				return gxTv_SdtSDT_message_Messagetype; 
			}
			set {
				gxTv_SdtSDT_message_Messagetype = value;
				SetDirty("Messagetype");
			}
		}




		[SoapElement(ElementName="dateTimeUnix")]
		[XmlElement(ElementName="dateTimeUnix")]
		public long gxTpr_Datetimeunix
		{
			get {
				return gxTv_SdtSDT_message_Datetimeunix; 
			}
			set {
				gxTv_SdtSDT_message_Datetimeunix = value;
				SetDirty("Datetimeunix");
			}
		}




		[SoapElement(ElementName="Message")]
		[XmlElement(ElementName="Message")]
		public string gxTpr_Message
		{
			get {
				return gxTv_SdtSDT_message_Message; 
			}
			set {
				gxTv_SdtSDT_message_Message = value;
				SetDirty("Message");
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
			gxTv_SdtSDT_message_Message = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtSDT_message_Id;
		 

		protected int gxTv_SdtSDT_message_Messagetype;
		 

		protected long gxTv_SdtSDT_message_Datetimeunix;
		 

		protected string gxTv_SdtSDT_message_Message;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"SDT_message", Namespace="distributedcryptography")]
	public class SdtSDT_message_RESTInterface : GxGenericCollectionItem<SdtSDT_message>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_message_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_message_RESTInterface( SdtSDT_message psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Id")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Id", Order=0)]
		public Guid gxTpr_Id
		{
			get { 
				return sdt.gxTpr_Id;

			}
			set { 
				sdt.gxTpr_Id = value;
			}
		}

		[JsonPropertyName("MessageType")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="MessageType", Order=1)]
		public int gxTpr_Messagetype
		{
			get { 
				return sdt.gxTpr_Messagetype;

			}
			set { 
				sdt.gxTpr_Messagetype = value;
			}
		}

		[JsonPropertyName("dateTimeUnix")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="dateTimeUnix", Order=2)]
		public  string gxTpr_Datetimeunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Datetimeunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Datetimeunix = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("Message")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="Message", Order=3)]
		public  string gxTpr_Message
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Message);

			}
			set { 
				 sdt.gxTpr_Message = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtSDT_message sdt
		{
			get { 
				return (SdtSDT_message)Sdt;
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
				sdt = new SdtSDT_message() ;
			}
		}
	}
	#endregion
}