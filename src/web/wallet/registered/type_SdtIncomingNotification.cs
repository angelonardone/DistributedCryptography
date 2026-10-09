/*
				   File: type_SdtIncomingNotification
			Description: IncomingNotification
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
	[XmlRoot(ElementName="IncomingNotification")]
	[XmlType(TypeName="IncomingNotification" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtIncomingNotification : GxUserType
	{
		public SdtIncomingNotification( )
		{
			/* Constructor for serialization */
			gxTv_SdtIncomingNotification_Toasttype = "";

			gxTv_SdtIncomingNotification_Title = "";

			gxTv_SdtIncomingNotification_Text = "";

		}

		public SdtIncomingNotification(IGxContext context)
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
			AddObjectProperty("toastType", gxTpr_Toasttype, false);


			AddObjectProperty("title", gxTpr_Title, false);


			AddObjectProperty("text", gxTpr_Text, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="toastType")]
		[XmlElement(ElementName="toastType")]
		public string gxTpr_Toasttype
		{
			get {
				return gxTv_SdtIncomingNotification_Toasttype; 
			}
			set {
				gxTv_SdtIncomingNotification_Toasttype = value;
				SetDirty("Toasttype");
			}
		}




		[SoapElement(ElementName="title")]
		[XmlElement(ElementName="title")]
		public string gxTpr_Title
		{
			get {
				return gxTv_SdtIncomingNotification_Title; 
			}
			set {
				gxTv_SdtIncomingNotification_Title = value;
				SetDirty("Title");
			}
		}




		[SoapElement(ElementName="text")]
		[XmlElement(ElementName="text")]
		public string gxTpr_Text
		{
			get {
				return gxTv_SdtIncomingNotification_Text; 
			}
			set {
				gxTv_SdtIncomingNotification_Text = value;
				SetDirty("Text");
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
			gxTv_SdtIncomingNotification_Toasttype = "";
			gxTv_SdtIncomingNotification_Title = "";
			gxTv_SdtIncomingNotification_Text = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtIncomingNotification_Toasttype;
		 

		protected string gxTv_SdtIncomingNotification_Title;
		 

		protected string gxTv_SdtIncomingNotification_Text;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"IncomingNotification", Namespace="distributedcryptography")]
	public class SdtIncomingNotification_RESTInterface : GxGenericCollectionItem<SdtIncomingNotification>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtIncomingNotification_RESTInterface( ) : base()
		{	
		}

		public SdtIncomingNotification_RESTInterface( SdtIncomingNotification psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("toastType")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="toastType", Order=0)]
		public  string gxTpr_Toasttype
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Toasttype);

			}
			set { 
				 sdt.gxTpr_Toasttype = value;
			}
		}

		[JsonPropertyName("title")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="title", Order=1)]
		public  string gxTpr_Title
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Title);

			}
			set { 
				 sdt.gxTpr_Title = value;
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


		#endregion
		[JsonIgnore]
		public SdtIncomingNotification sdt
		{
			get { 
				return (SdtIncomingNotification)Sdt;
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
				sdt = new SdtIncomingNotification() ;
			}
		}
	}
	#endregion
}