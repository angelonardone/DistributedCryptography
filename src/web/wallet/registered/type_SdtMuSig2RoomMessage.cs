/*
				   File: type_SdtMuSig2RoomMessage
			Description: MuSig2RoomMessage
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
	[XmlRoot(ElementName="MuSig2RoomMessage")]
	[XmlType(TypeName="MuSig2RoomMessage" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtMuSig2RoomMessage : GxUserType
	{
		public SdtMuSig2RoomMessage( )
		{
			/* Constructor for serialization */
			gxTv_SdtMuSig2RoomMessage_Fromusername = "";

			gxTv_SdtMuSig2RoomMessage_Kind = "";

			gxTv_SdtMuSig2RoomMessage_Data = "";

			gxTv_SdtMuSig2RoomMessage_Signature = "";

		}

		public SdtMuSig2RoomMessage(IGxContext context)
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


			AddObjectProperty("roomId", gxTpr_Roomid, false);


			AddObjectProperty("fromUserName", gxTpr_Fromusername, false);


			AddObjectProperty("dateTimeUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Datetimeunix, 18, 0)), false);


			AddObjectProperty("kind", gxTpr_Kind, false);


			AddObjectProperty("data", gxTpr_Data, false);


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
				return gxTv_SdtMuSig2RoomMessage_Id; 
			}
			set {
				gxTv_SdtMuSig2RoomMessage_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="referenceGroupId")]
		[XmlElement(ElementName="referenceGroupId")]
		public Guid gxTpr_Referencegroupid
		{
			get {
				return gxTv_SdtMuSig2RoomMessage_Referencegroupid; 
			}
			set {
				gxTv_SdtMuSig2RoomMessage_Referencegroupid = value;
				SetDirty("Referencegroupid");
			}
		}




		[SoapElement(ElementName="roomId")]
		[XmlElement(ElementName="roomId")]
		public Guid gxTpr_Roomid
		{
			get {
				return gxTv_SdtMuSig2RoomMessage_Roomid; 
			}
			set {
				gxTv_SdtMuSig2RoomMessage_Roomid = value;
				SetDirty("Roomid");
			}
		}




		[SoapElement(ElementName="fromUserName")]
		[XmlElement(ElementName="fromUserName")]
		public string gxTpr_Fromusername
		{
			get {
				return gxTv_SdtMuSig2RoomMessage_Fromusername; 
			}
			set {
				gxTv_SdtMuSig2RoomMessage_Fromusername = value;
				SetDirty("Fromusername");
			}
		}




		[SoapElement(ElementName="dateTimeUnix")]
		[XmlElement(ElementName="dateTimeUnix")]
		public long gxTpr_Datetimeunix
		{
			get {
				return gxTv_SdtMuSig2RoomMessage_Datetimeunix; 
			}
			set {
				gxTv_SdtMuSig2RoomMessage_Datetimeunix = value;
				SetDirty("Datetimeunix");
			}
		}




		[SoapElement(ElementName="kind")]
		[XmlElement(ElementName="kind")]
		public string gxTpr_Kind
		{
			get {
				return gxTv_SdtMuSig2RoomMessage_Kind; 
			}
			set {
				gxTv_SdtMuSig2RoomMessage_Kind = value;
				SetDirty("Kind");
			}
		}




		[SoapElement(ElementName="data")]
		[XmlElement(ElementName="data")]
		public string gxTpr_Data
		{
			get {
				return gxTv_SdtMuSig2RoomMessage_Data; 
			}
			set {
				gxTv_SdtMuSig2RoomMessage_Data = value;
				SetDirty("Data");
			}
		}




		[SoapElement(ElementName="signature")]
		[XmlElement(ElementName="signature")]
		public string gxTpr_Signature
		{
			get {
				return gxTv_SdtMuSig2RoomMessage_Signature; 
			}
			set {
				gxTv_SdtMuSig2RoomMessage_Signature = value;
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
			gxTv_SdtMuSig2RoomMessage_Fromusername = "";

			gxTv_SdtMuSig2RoomMessage_Kind = "";
			gxTv_SdtMuSig2RoomMessage_Data = "";
			gxTv_SdtMuSig2RoomMessage_Signature = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtMuSig2RoomMessage_Id;
		 

		protected Guid gxTv_SdtMuSig2RoomMessage_Referencegroupid;
		 

		protected Guid gxTv_SdtMuSig2RoomMessage_Roomid;
		 

		protected string gxTv_SdtMuSig2RoomMessage_Fromusername;
		 

		protected long gxTv_SdtMuSig2RoomMessage_Datetimeunix;
		 

		protected string gxTv_SdtMuSig2RoomMessage_Kind;
		 

		protected string gxTv_SdtMuSig2RoomMessage_Data;
		 

		protected string gxTv_SdtMuSig2RoomMessage_Signature;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"MuSig2RoomMessage", Namespace="distributedcryptography")]
	public class SdtMuSig2RoomMessage_RESTInterface : GxGenericCollectionItem<SdtMuSig2RoomMessage>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtMuSig2RoomMessage_RESTInterface( ) : base()
		{	
		}

		public SdtMuSig2RoomMessage_RESTInterface( SdtMuSig2RoomMessage psdt ) : base(psdt)
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

		[JsonPropertyName("roomId")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="roomId", Order=2)]
		public Guid gxTpr_Roomid
		{
			get { 
				return sdt.gxTpr_Roomid;

			}
			set { 
				sdt.gxTpr_Roomid = value;
			}
		}

		[JsonPropertyName("fromUserName")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="fromUserName", Order=3)]
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
		[JsonPropertyOrder(4)]
		[DataMember(Name="dateTimeUnix", Order=4)]
		public  string gxTpr_Datetimeunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Datetimeunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Datetimeunix = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("kind")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="kind", Order=5)]
		public  string gxTpr_Kind
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Kind);

			}
			set { 
				 sdt.gxTpr_Kind = value;
			}
		}

		[JsonPropertyName("data")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="data", Order=6)]
		public  string gxTpr_Data
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Data);

			}
			set { 
				 sdt.gxTpr_Data = value;
			}
		}

		[JsonPropertyName("signature")]
		[JsonPropertyOrder(7)]
		[DataMember(Name="signature", Order=7)]
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
		public SdtMuSig2RoomMessage sdt
		{
			get { 
				return (SdtMuSig2RoomMessage)Sdt;
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
				sdt = new SdtMuSig2RoomMessage() ;
			}
		}
	}
	#endregion
}