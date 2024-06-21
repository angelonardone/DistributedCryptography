/*
				   File: type_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used
			Description: GxExplorer_services_TxoutFromAddresses_Transaction_Used
				 Author: Nemo 🐠 for C# (.NET) version 18.0.8.180599
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="GxExplorer_services_TxoutFromAddresses_Transaction_Used")]
	[XmlType(TypeName="GxExplorer_services_TxoutFromAddresses_Transaction_Used" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used : GxUserType
	{
		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used( )
		{
			/* Constructor for serialization */
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedid = "";

			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Useddatetime = (DateTime)(DateTime.MinValue);

		}

		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used(IGxContext context)
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
			AddObjectProperty("UsedId", gxTpr_Usedid, false);


			AddObjectProperty("UsedN", StringUtil.LTrim( StringUtil.Str( (decimal)gxTpr_Usedn, 18, 0)), false);


			datetime_STZ = gxTpr_Useddatetime;
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
			AddObjectProperty("UsedDateTime", sDateCnv, false);


			if (gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto != null)
			{
				AddObjectProperty("UsedTo", gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="UsedId")]
		[XmlElement(ElementName="UsedId")]
		public string gxTpr_Usedid
		{
			get {
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedid; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedid = value;
				SetDirty("Usedid");
			}
		}




		[SoapElement(ElementName="UsedN")]
		[XmlElement(ElementName="UsedN")]
		public long gxTpr_Usedn
		{
			get {
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedn; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedn = value;
				SetDirty("Usedn");
			}
		}



		[SoapElement(ElementName="UsedDateTime")]
		[XmlElement(ElementName="UsedDateTime" , IsNullable=true)]
		public string gxTpr_Useddatetime_Nullable
		{
			get {
				if ( gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Useddatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Useddatetime).value ;
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Useddatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Useddatetime
		{
			get {
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Useddatetime; 
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Useddatetime = value;
				SetDirty("Useddatetime");
			}
		}



		[SoapElement(ElementName="UsedTo" )]
		[XmlArray(ElementName="UsedTo"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item> gxTpr_Usedto_GXBaseCollection
		{
			get {
				if ( gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto == null )
				{
					gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto = new GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item>( context, "GxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item", "");
				}
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto;
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto_N = false;
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item> gxTpr_Usedto
		{
			get {
				if ( gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto == null )
				{
					gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto = new GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item>( context, "GxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item", "");
				}
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto_N = false;
				return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto ;
			}
			set {
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto_N = false;
				gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto = value;
				SetDirty("Usedto");
			}
		}

		public void gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto_SetNull()
		{
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto_N = true;
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto = null;
		}

		public bool gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto_IsNull()
		{
			return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto == null;
		}
		public bool ShouldSerializegxTpr_Usedto_GXBaseCollection_Json()
		{
			return gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto != null && gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto.Count > 0;

		}

		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedid = "";

			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Useddatetime = (DateTime)(DateTime.MinValue);

			gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto_N = true;

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

		protected string gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedid;
		 

		protected long gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedn;
		 

		protected DateTime gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Useddatetime;
		 
		protected bool gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto_N;
		protected GXBaseCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item> gxTv_SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_Usedto = null;  


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"GxExplorer_services_TxoutFromAddresses_Transaction_Used", Namespace="distributedcryptography")]
	public class SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_RESTInterface : GxGenericCollectionItem<SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_RESTInterface( ) : base()
		{	
		}

		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_RESTInterface( SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="UsedId", Order=0)]
		public  string gxTpr_Usedid
		{
			get { 
				return sdt.gxTpr_Usedid;

			}
			set { 
				 sdt.gxTpr_Usedid = value;
			}
		}

		[DataMember(Name="UsedN", Order=1)]
		public  string gxTpr_Usedn
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Usedn, 18, 0));

			}
			set { 
				sdt.gxTpr_Usedn = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="UsedDateTime", Order=2)]
		public  string gxTpr_Useddatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Useddatetime,context);

			}
			set { 
				sdt.gxTpr_Useddatetime = DateTimeUtil.CToT2(value,context);
			}
		}

		[DataMember(Name="UsedTo", Order=3, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item_RESTInterface> gxTpr_Usedto
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Usedto_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used_UsedTo_Item_RESTInterface>(sdt.gxTpr_Usedto);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Usedto);
			}
		}


		#endregion

		public SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used sdt
		{
			get { 
				return (SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used)Sdt;
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
				sdt = new SdtGxExplorer_services_TxoutFromAddresses_Transaction_Used() ;
			}
		}
	}
	#endregion
}