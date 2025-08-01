/*
				   File: type_SdtLinkList_LinkItem
			Description: LinkList
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="LinkItem")]
	[XmlType(TypeName="LinkItem" , Namespace="GeneXus" )]
	[Serializable]
	public class SdtLinkList_LinkItem : GxUserType
	{
		public SdtLinkList_LinkItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtLinkList_LinkItem_Caption = "";

			gxTv_SdtLinkList_LinkItem_Url = "";

		}

		public SdtLinkList_LinkItem(IGxContext context)
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
			AddObjectProperty("Caption", gxTpr_Caption, false);


			AddObjectProperty("URL", gxTpr_Url, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Caption")]
		[XmlElement(ElementName="Caption")]
		public string gxTpr_Caption
		{
			get {
				return gxTv_SdtLinkList_LinkItem_Caption; 
			}
			set {
				gxTv_SdtLinkList_LinkItem_Caption = value;
				SetDirty("Caption");
			}
		}




		[SoapElement(ElementName="URL")]
		[XmlElement(ElementName="URL")]
		public string gxTpr_Url
		{
			get {
				return gxTv_SdtLinkList_LinkItem_Url; 
			}
			set {
				gxTv_SdtLinkList_LinkItem_Url = value;
				SetDirty("Url");
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
			gxTv_SdtLinkList_LinkItem_Caption = "";
			gxTv_SdtLinkList_LinkItem_Url = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtLinkList_LinkItem_Caption;
		 

		protected string gxTv_SdtLinkList_LinkItem_Url;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"LinkItem", Namespace="GeneXus")]
	public class SdtLinkList_LinkItem_RESTInterface : GxGenericCollectionItem<SdtLinkList_LinkItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtLinkList_LinkItem_RESTInterface( ) : base()
		{	
		}

		public SdtLinkList_LinkItem_RESTInterface( SdtLinkList_LinkItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Caption")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Caption", Order=0)]
		public  string gxTpr_Caption
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Caption);

			}
			set { 
				 sdt.gxTpr_Caption = value;
			}
		}

		[JsonPropertyName("URL")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="URL", Order=1)]
		public  string gxTpr_Url
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Url);

			}
			set { 
				 sdt.gxTpr_Url = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtLinkList_LinkItem sdt
		{
			get { 
				return (SdtLinkList_LinkItem)Sdt;
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
				sdt = new SdtLinkList_LinkItem() ;
			}
		}
	}
	#endregion
}