/*
				   File: type_SdtelectrumRespGetHistory_resultItem
			Description: result
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
namespace GeneXus.Programs.electrum
{
	[XmlRoot(ElementName="electrumRespGetHistory.resultItem")]
	[XmlType(TypeName="electrumRespGetHistory.resultItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtelectrumRespGetHistory_resultItem : GxUserType
	{
		public SdtelectrumRespGetHistory_resultItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtelectrumRespGetHistory_resultItem_Tx_hash = "";


		}

		public SdtelectrumRespGetHistory_resultItem(IGxContext context)
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
			AddObjectProperty("height", gxTpr_Height, false);


			AddObjectProperty("tx_hash", gxTpr_Tx_hash, false);


			AddObjectProperty("fee", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Fee, 16, 0)), false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="height")]
		[XmlElement(ElementName="height")]
		public long gxTpr_Height
		{
			get {
				return gxTv_SdtelectrumRespGetHistory_resultItem_Height; 
			}
			set {
				gxTv_SdtelectrumRespGetHistory_resultItem_Height = value;
				SetDirty("Height");
			}
		}




		[SoapElement(ElementName="tx_hash")]
		[XmlElement(ElementName="tx_hash")]
		public string gxTpr_Tx_hash
		{
			get {
				return gxTv_SdtelectrumRespGetHistory_resultItem_Tx_hash; 
			}
			set {
				gxTv_SdtelectrumRespGetHistory_resultItem_Tx_hash = value;
				SetDirty("Tx_hash");
			}
		}




		[SoapElement(ElementName="fee")]
		[XmlElement(ElementName="fee")]
		public long gxTpr_Fee
		{
			get {
				return gxTv_SdtelectrumRespGetHistory_resultItem_Fee; 
			}
			set {
				gxTv_SdtelectrumRespGetHistory_resultItem_Fee = value;
				SetDirty("Fee");
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
			gxTv_SdtelectrumRespGetHistory_resultItem_Tx_hash = "";

			return  ;
		}



		#endregion

		#region Declaration

		protected long gxTv_SdtelectrumRespGetHistory_resultItem_Height;
		 

		protected string gxTv_SdtelectrumRespGetHistory_resultItem_Tx_hash;
		 

		protected long gxTv_SdtelectrumRespGetHistory_resultItem_Fee;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"electrumRespGetHistory.resultItem", Namespace="distributedcryptography")]
	public class SdtelectrumRespGetHistory_resultItem_RESTInterface : GxGenericCollectionItem<SdtelectrumRespGetHistory_resultItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtelectrumRespGetHistory_resultItem_RESTInterface( ) : base()
		{	
		}

		public SdtelectrumRespGetHistory_resultItem_RESTInterface( SdtelectrumRespGetHistory_resultItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("height")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="height", Order=0)]
		public  string gxTpr_Height
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Height, 10, 0));

			}
			set { 
				sdt.gxTpr_Height = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("tx_hash")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="tx_hash", Order=1)]
		public  string gxTpr_Tx_hash
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Tx_hash);

			}
			set { 
				 sdt.gxTpr_Tx_hash = value;
			}
		}

		[JsonPropertyName("fee")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="fee", Order=2)]
		public  string gxTpr_Fee
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Fee, 16, 0));

			}
			set { 
				sdt.gxTpr_Fee = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtelectrumRespGetHistory_resultItem sdt
		{
			get { 
				return (SdtelectrumRespGetHistory_resultItem)Sdt;
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
				sdt = new SdtelectrumRespGetHistory_resultItem() ;
			}
		}
	}
	#endregion
}