/*
				   File: type_SdtGroupChatMessage
			Description: GroupChatMessage
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
	[XmlRoot(ElementName="GroupChatMessage")]
	[XmlType(TypeName="GroupChatMessage" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupChatMessage : GxUserType
	{
		public SdtGroupChatMessage( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroupChatMessage_Fromusername = "";

			gxTv_SdtGroupChatMessage_Text = "";

			gxTv_SdtGroupChatMessage_Signature = "";

		}

		public SdtGroupChatMessage(IGxContext context)
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


			AddObjectProperty("referenceGroupId", gxTpr_Referencegroupid, false);


			AddObjectProperty("fromUserName", gxTpr_Fromusername, false);


			AddObjectProperty("dateTimeUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Datetimeunix, 18, 0)), false);


			AddObjectProperty("text", gxTpr_Text, false);


			AddObjectProperty("signature", gxTpr_Signature, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Id")]
		[XmlElement(ElementName="Id")]
		public Guid gxTpr_Id
		{
			get {
				return gxTv_SdtGroupChatMessage_Id; 
			}
			set {
				gxTv_SdtGroupChatMessage_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="referenceGroupId")]
		[XmlElement(ElementName="referenceGroupId")]
		public Guid gxTpr_Referencegroupid
		{
			get {
				return gxTv_SdtGroupChatMessage_Referencegroupid; 
			}
			set {
				gxTv_SdtGroupChatMessage_Referencegroupid = value;
				SetDirty("Referencegroupid");
			}
		}




		[SoapElement(ElementName="fromUserName")]
		[XmlElement(ElementName="fromUserName")]
		public string gxTpr_Fromusername
		{
			get {
				return gxTv_SdtGroupChatMessage_Fromusername; 
			}
			set {
				gxTv_SdtGroupChatMessage_Fromusername = value;
				SetDirty("Fromusername");
			}
		}




		[SoapElement(ElementName="dateTimeUnix")]
		[XmlElement(ElementName="dateTimeUnix")]
		public long gxTpr_Datetimeunix
		{
			get {
				return gxTv_SdtGroupChatMessage_Datetimeunix; 
			}
			set {
				gxTv_SdtGroupChatMessage_Datetimeunix = value;
				SetDirty("Datetimeunix");
			}
		}




		[SoapElement(ElementName="text")]
		[XmlElement(ElementName="text")]
		public string gxTpr_Text
		{
			get {
				return gxTv_SdtGroupChatMessage_Text; 
			}
			set {
				gxTv_SdtGroupChatMessage_Text = value;
				SetDirty("Text");
			}
		}




		[SoapElement(ElementName="signature")]
		[XmlElement(ElementName="signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtGroupChatMessage_Signature; 
			}
			set {
				gxTv_SdtGroupChatMessage_Signature = value;
				SetDirty("Signature");
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
			gxTv_SdtGroupChatMessage_Fromusername = "";

			gxTv_SdtGroupChatMessage_Text = "";
			gxTv_SdtGroupChatMessage_Signature = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtGroupChatMessage_Id;
		 

		protected Guid gxTv_SdtGroupChatMessage_Referencegroupid;
		 

		protected string gxTv_SdtGroupChatMessage_Fromusername;
		 

		protected long gxTv_SdtGroupChatMessage_Datetimeunix;
		 

		protected string gxTv_SdtGroupChatMessage_Text;
		 

		protected string gxTv_SdtGroupChatMessage_Signature;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GroupChatMessage", Namespace="distributedcryptography")]
	public class SdtGroupChatMessage_RESTInterface : GxGenericCollectionItem<SdtGroupChatMessage>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupChatMessage_RESTInterface( ) : base()
		{	
		}

		public SdtGroupChatMessage_RESTInterface( SdtGroupChatMessage psdt ) : base(psdt)
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

		[JsonPropertyName("referenceGroupId")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="referenceGroupId", Order=1)]
		public Guid gxTpr_Referencegroupid
		{
			get { 
				return sdt.gxTpr_Referencegroupid;

			}
			set { 
				sdt.gxTpr_Referencegroupid = value;
			}
		}

		[JsonPropertyName("fromUserName")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="fromUserName", Order=2)]
		public  string gxTpr_Fromusername
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Fromusername);

			}
			set { 
				 sdt.gxTpr_Fromusername = value;
			}
		}

		[JsonPropertyName("dateTimeUnix")]
		[JsonPropertyOrder(3)]
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

		[JsonPropertyName("text")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="text", Order=4)]
		public  string gxTpr_Text
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Text);

			}
			set { 
				 sdt.gxTpr_Text = value;
			}
		}

		[JsonPropertyName("signature")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="signature", Order=5)]
		public  string gxTpr_Signature
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Signature);

			}
			set { 
				 sdt.gxTpr_Signature = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroupChatMessage sdt
		{
			get { 
				return (SdtGroupChatMessage)Sdt;
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
				sdt = new SdtGroupChatMessage() ;
			}
		}
	}
	#endregion
}