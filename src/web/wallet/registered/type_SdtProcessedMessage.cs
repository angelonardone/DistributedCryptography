/*
				   File: type_SdtProcessedMessage
			Description: ProcessedMessage
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
	[XmlRoot(ElementName="ProcessedMessage")]
	[XmlType(TypeName="ProcessedMessage" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtProcessedMessage : GxUserType
	{
		public SdtProcessedMessage( )
		{
			/* Constructor for serialization */
		}

		public SdtProcessedMessage(IGxContext context)
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


			AddObjectProperty("dateTimeUnix", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Datetimeunix, 18, 0)), false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Id")]
		[XmlElement(ElementName="Id")]
		public Guid gxTpr_Id
		{
			get {
				return gxTv_SdtProcessedMessage_Id; 
			}
			set {
				gxTv_SdtProcessedMessage_Id = value;
				SetDirty("Id");
			}
		}




		[SoapElement(ElementName="dateTimeUnix")]
		[XmlElement(ElementName="dateTimeUnix")]
		public long gxTpr_Datetimeunix
		{
			get {
				return gxTv_SdtProcessedMessage_Datetimeunix; 
			}
			set {
				gxTv_SdtProcessedMessage_Datetimeunix = value;
				SetDirty("Datetimeunix");
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
			return  ;
		}



		#endregion

		#region Declaration

		protected Guid gxTv_SdtProcessedMessage_Id;
		 

		protected long gxTv_SdtProcessedMessage_Datetimeunix;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"ProcessedMessage", Namespace="distributedcryptography")]
	public class SdtProcessedMessage_RESTInterface : GxGenericCollectionItem<SdtProcessedMessage>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtProcessedMessage_RESTInterface( ) : base()
		{	
		}

		public SdtProcessedMessage_RESTInterface( SdtProcessedMessage psdt ) : base(psdt)
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

		[JsonPropertyName("dateTimeUnix")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="dateTimeUnix", Order=1)]
		public  string gxTpr_Datetimeunix
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Datetimeunix, 18, 0));

			}
			set { 
				sdt.gxTpr_Datetimeunix = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtProcessedMessage sdt
		{
			get { 
				return (SdtProcessedMessage)Sdt;
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
				sdt = new SdtProcessedMessage() ;
			}
		}
	}
	#endregion
}