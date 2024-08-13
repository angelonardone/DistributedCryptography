/*
				   File: type_SdtSDT_Chat
			Description: SDT_Chat
				 Author: Nemo 🐠 for C# (.NET) version 18.0.10.184260
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

using GeneXus.Programs;
namespace GeneXus.Programs.nostr
{
	[XmlRoot(ElementName="SDT_Chat")]
	[XmlType(TypeName="SDT_Chat" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_Chat : GxUserType
	{
		public SdtSDT_Chat( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_Chat_From = "";

			gxTv_SdtSDT_Chat_To = "";

			gxTv_SdtSDT_Chat_Message = "";

		}

		public SdtSDT_Chat(IGxContext context)
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


			AddObjectProperty("From", gxTpr_From, false);


			AddObjectProperty("To", gxTpr_To, false);


			AddObjectProperty("dateTimeUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Datetimeunix, 18, 0)), false);


			AddObjectProperty("Message", gxTpr_Message, false);


			AddObjectProperty("confirmedDateTimeUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Confirmeddatetimeunix, 18, 0)), false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Id")]
		[XmlElement(ElementName="Id")]
		public Guid gxTpr_Id
		{
			get {
				return gxTv_SdtSDT_Chat_Id; 
			}
			set {
				gxTv_SdtSDT_Chat_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="From")]
		[XmlElement(ElementName="From")]
		public string gxTpr_From
		{
			get {
				return gxTv_SdtSDT_Chat_From; 
			}
			set {
				gxTv_SdtSDT_Chat_From = value;
				SetDirty("From");
			}
		}




		[SoapElement(ElementName="To")]
		[XmlElement(ElementName="To")]
		public string gxTpr_To
		{
			get {
				return gxTv_SdtSDT_Chat_To; 
			}
			set {
				gxTv_SdtSDT_Chat_To = value;
				SetDirty("To");
			}
		}




		[SoapElement(ElementName="dateTimeUnix")]
		[XmlElement(ElementName="dateTimeUnix")]
		public long gxTpr_Datetimeunix
		{
			get {
				return gxTv_SdtSDT_Chat_Datetimeunix; 
			}
			set {
				gxTv_SdtSDT_Chat_Datetimeunix = value;
				SetDirty("Datetimeunix");
			}
		}




		[SoapElement(ElementName="Message")]
		[XmlElement(ElementName="Message")]
		public string gxTpr_Message
		{
			get {
				return gxTv_SdtSDT_Chat_Message; 
			}
			set {
				gxTv_SdtSDT_Chat_Message = value;
				SetDirty("Message");
			}
		}




		[SoapElement(ElementName="confirmedDateTimeUnix")]
		[XmlElement(ElementName="confirmedDateTimeUnix")]
		public long gxTpr_Confirmeddatetimeunix
		{
			get {
				return gxTv_SdtSDT_Chat_Confirmeddatetimeunix; 
			}
			set {
				gxTv_SdtSDT_Chat_Confirmeddatetimeunix = value;
				SetDirty("Confirmeddatetimeunix");
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
			gxTv_SdtSDT_Chat_From = "";
			gxTv_SdtSDT_Chat_To = "";

			gxTv_SdtSDT_Chat_Message = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtSDT_Chat_Id;
		 

		protected string gxTv_SdtSDT_Chat_From;
		 

		protected string gxTv_SdtSDT_Chat_To;
		 

		protected long gxTv_SdtSDT_Chat_Datetimeunix;
		 

		protected string gxTv_SdtSDT_Chat_Message;
		 

		protected long gxTv_SdtSDT_Chat_Confirmeddatetimeunix;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"SDT_Chat", Namespace="distributedcryptography")]
	public class SdtSDT_Chat_RESTInterface : GxGenericCollectionItem<SdtSDT_Chat>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_Chat_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_Chat_RESTInterface( SdtSDT_Chat psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
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

		[DataMember(Name="From", Order=1)]
		public  string gxTpr_From
		{
			get { 
				return sdt.gxTpr_From;

			}
			set { 
				 sdt.gxTpr_From = value;
			}
		}

		[DataMember(Name="To", Order=2)]
		public  string gxTpr_To
		{
			get { 
				return sdt.gxTpr_To;

			}
			set { 
				 sdt.gxTpr_To = value;
			}
		}

		[DataMember(Name="dateTimeUnix", Order=3)]
		public  string gxTpr_Datetimeunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Datetimeunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Datetimeunix = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="Message", Order=4)]
		public  string gxTpr_Message
		{
			get { 
				return sdt.gxTpr_Message;

			}
			set { 
				 sdt.gxTpr_Message = value;
			}
		}

		[DataMember(Name="confirmedDateTimeUnix", Order=5)]
		public  string gxTpr_Confirmeddatetimeunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Confirmeddatetimeunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Confirmeddatetimeunix = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion

		public SdtSDT_Chat sdt
		{
			get { 
				return (SdtSDT_Chat)Sdt;
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
				sdt = new SdtSDT_Chat() ;
			}
		}
	}
	#endregion
}