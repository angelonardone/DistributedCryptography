/*
				   File: type_SdtGroup_SDT_TimeConstrainItem
			Description: TimeConstrain
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
using GeneXus.Programs.wallet;
namespace GeneXus.Programs.wallet.registered
{
	[XmlRoot(ElementName="Group_SDT.TimeConstrainItem")]
	[XmlType(TypeName="Group_SDT.TimeConstrainItem" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroup_SDT_TimeConstrainItem : GxUserType
	{
		public SdtGroup_SDT_TimeConstrainItem( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroup_SDT_TimeConstrainItem_Address = "";

			gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedsecret = "";

			gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedkey = "";

		}

		public SdtGroup_SDT_TimeConstrainItem(IGxContext context)
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
			AddObjectProperty("sequence", gxTpr_Sequence, false);


			AddObjectProperty("address", gxTpr_Address, false);


			sDateCnv = "";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Year(gxTpr_Date)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("0000", 1, 4-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Month(gxTpr_Date)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			sDateCnv = sDateCnv + "-";
			sNumToPad = StringUtil.Trim(StringUtil.Str((decimal)(DateTimeUtil.Day(gxTpr_Date)), 10, 0));
			sDateCnv = sDateCnv + StringUtil.Substring("00", 1, 2-StringUtil.Len(sNumToPad)) + sNumToPad;
			AddObjectProperty("date", sDateCnv, false);



			AddObjectProperty("encryptedSecret", gxTpr_Encryptedsecret, false);


			AddObjectProperty("encryptedKey", gxTpr_Encryptedkey, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="sequence")]
		[XmlElement(ElementName="sequence")]
		public int gxTpr_Sequence
		{
			get {
				return gxTv_SdtGroup_SDT_TimeConstrainItem_Sequence; 
			}
			set {
				gxTv_SdtGroup_SDT_TimeConstrainItem_Sequence = value;
				SetDirty("Sequence");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtGroup_SDT_TimeConstrainItem_Address; 
			}
			set {
				gxTv_SdtGroup_SDT_TimeConstrainItem_Address = value;
				SetDirty("Address");
			}
		}



		[SoapElement(ElementName="date")]
		[XmlElement(ElementName="date" , IsNullable=true)]
		public string gxTpr_Date_Nullable
		{
			get {
				if ( gxTv_SdtGroup_SDT_TimeConstrainItem_Date == DateTime.MinValue)
					return null;
				return new GxDateString(gxTv_SdtGroup_SDT_TimeConstrainItem_Date).value ;
			}
			set {
				gxTv_SdtGroup_SDT_TimeConstrainItem_Date = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Date
		{
			get {
				return gxTv_SdtGroup_SDT_TimeConstrainItem_Date; 
			}
			set {
				gxTv_SdtGroup_SDT_TimeConstrainItem_Date = value;
				SetDirty("Date");
			}
		}



		[SoapElement(ElementName="encryptedSecret")]
		[XmlElement(ElementName="encryptedSecret")]
		public string gxTpr_Encryptedsecret
		{
			get {
				return gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedsecret; 
			}
			set {
				gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedsecret = value;
				SetDirty("Encryptedsecret");
			}
		}




		[SoapElement(ElementName="encryptedKey")]
		[XmlElement(ElementName="encryptedKey")]
		public string gxTpr_Encryptedkey
		{
			get {
				return gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedkey; 
			}
			set {
				gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedkey = value;
				SetDirty("Encryptedkey");
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
			gxTv_SdtGroup_SDT_TimeConstrainItem_Address = "";

			gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedsecret = "";
			gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedkey = "";
			sDateCnv = "";
			sNumToPad = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string sDateCnv ;
		protected string sNumToPad ;
		protected int gxTv_SdtGroup_SDT_TimeConstrainItem_Sequence;
		 

		protected string gxTv_SdtGroup_SDT_TimeConstrainItem_Address;
		 

		protected DateTime gxTv_SdtGroup_SDT_TimeConstrainItem_Date;
		 

		protected string gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedsecret;
		 

		protected string gxTv_SdtGroup_SDT_TimeConstrainItem_Encryptedkey;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"Group_SDT.TimeConstrainItem", Namespace="distributedcryptography")]
	public class SdtGroup_SDT_TimeConstrainItem_RESTInterface : GxGenericCollectionItem<SdtGroup_SDT_TimeConstrainItem>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroup_SDT_TimeConstrainItem_RESTInterface( ) : base()
		{	
		}

		public SdtGroup_SDT_TimeConstrainItem_RESTInterface( SdtGroup_SDT_TimeConstrainItem psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("sequence")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="sequence", Order=0)]
		public  string gxTpr_Sequence
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Sequence, 8, 0));

			}
			set { 
				sdt.gxTpr_Sequence = (int) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("address")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="address", Order=1)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[JsonPropertyName("date")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="date", Order=2)]
		public  string gxTpr_Date
		{
			get { 
				return DateTimeUtil.DToC2( sdt.gxTpr_Date);

			}
			set { 
				sdt.gxTpr_Date = DateTimeUtil.CToD2(value);
			}
		}

		[JsonPropertyName("encryptedSecret")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="encryptedSecret", Order=3)]
		public  string gxTpr_Encryptedsecret
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedsecret);

			}
			set { 
				 sdt.gxTpr_Encryptedsecret = value;
			}
		}

		[JsonPropertyName("encryptedKey")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="encryptedKey", Order=4)]
		public  string gxTpr_Encryptedkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Encryptedkey);

			}
			set { 
				 sdt.gxTpr_Encryptedkey = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtGroup_SDT_TimeConstrainItem sdt
		{
			get { 
				return (SdtGroup_SDT_TimeConstrainItem)Sdt;
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
				sdt = new SdtGroup_SDT_TimeConstrainItem() ;
			}
		}
	}
	#endregion
}