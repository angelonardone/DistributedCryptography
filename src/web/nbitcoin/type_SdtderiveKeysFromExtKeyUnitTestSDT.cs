/*
				   File: type_SdtderiveKeysFromExtKeyUnitTestSDT
			Description: deriveKeysFromExtKeyUnitTestSDT
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

using GeneXus.Programs;
namespace GeneXus.Programs.nbitcoin
{
	[XmlRoot(ElementName="deriveKeysFromExtKeyUnitTestSDT")]
	[XmlType(TypeName="deriveKeysFromExtKeyUnitTestSDT" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtderiveKeysFromExtKeyUnitTestSDT : GxUserType
	{
		public SdtderiveKeysFromExtKeyUnitTestSDT( )
		{
			/* Constructor for serialization */
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Testcaseid = "";

			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Serializedrootextkey = "";

			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgallkeyinfo = "";

			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Error = "";

			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectederror = "";

			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgerror = "";

		}

		public SdtderiveKeysFromExtKeyUnitTestSDT(IGxContext context)
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
			AddObjectProperty("TestCaseId", gxTpr_Testcaseid, false);


			AddObjectProperty("serializedRootExtKey", gxTpr_Serializedrootextkey, false);


			AddObjectProperty("base", gxTpr_Base, false);


			AddObjectProperty("begin", gxTpr_Begin, false);


			AddObjectProperty("end", gxTpr_End, false);

			if (gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo != null)
			{
				AddObjectProperty("allKeyInfo", gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo, false);
			}
			if (gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo != null)
			{
				AddObjectProperty("ExpectedallKeyInfo", gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo, false);
			}

			AddObjectProperty("MsgallKeyInfo", gxTpr_Msgallkeyinfo, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("Expectederror", gxTpr_Expectederror, false);


			AddObjectProperty("Msgerror", gxTpr_Msgerror, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="TestCaseId")]
		[XmlElement(ElementName="TestCaseId")]
		public string gxTpr_Testcaseid
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Testcaseid; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Testcaseid = value;
				SetDirty("Testcaseid");
			}
		}




		[SoapElement(ElementName="serializedRootExtKey")]
		[XmlElement(ElementName="serializedRootExtKey")]
		public string gxTpr_Serializedrootextkey
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Serializedrootextkey; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Serializedrootextkey = value;
				SetDirty("Serializedrootextkey");
			}
		}




		[SoapElement(ElementName="base")]
		[XmlElement(ElementName="base")]
		public long gxTpr_Base
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Base; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Base = value;
				SetDirty("Base");
			}
		}




		[SoapElement(ElementName="begin")]
		[XmlElement(ElementName="begin")]
		public long gxTpr_Begin
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Begin; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Begin = value;
				SetDirty("Begin");
			}
		}




		[SoapElement(ElementName="end")]
		[XmlElement(ElementName="end")]
		public long gxTpr_End
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_End; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_End = value;
				SetDirty("End");
			}
		}




		[SoapElement(ElementName="allKeyInfo" )]
		[XmlArray(ElementName="allKeyInfo"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> gxTpr_Allkeyinfo_GXBaseCollection
		{
			get {
				if ( gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo == null )
				{
					gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "");
				}
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo;
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo_N = false;
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> gxTpr_Allkeyinfo
		{
			get {
				if ( gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo == null )
				{
					gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "");
				}
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo_N = false;
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo ;
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo_N = false;
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo = value;
				SetDirty("Allkeyinfo");
			}
		}

		public void gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo_SetNull()
		{
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo_N = true;
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo = null;
		}

		public bool gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo_IsNull()
		{
			return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Allkeyinfo_GXBaseCollection_Json()
		{
			return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo != null && gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo.Count > 0;

		}


		[SoapElement(ElementName="ExpectedallKeyInfo" )]
		[XmlArray(ElementName="ExpectedallKeyInfo"  )]
		[XmlArrayItemAttribute(ElementName="Item" , IsNullable=false )]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> gxTpr_Expectedallkeyinfo_GXBaseCollection
		{
			get {
				if ( gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo == null )
				{
					gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "");
				}
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo;
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo_N = false;
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo = value;
			}
		}

		[XmlIgnore]
		public GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> gxTpr_Expectedallkeyinfo
		{
			get {
				if ( gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo == null )
				{
					gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo = new GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo>( context, "KeyInfo", "");
				}
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo_N = false;
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo ;
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo_N = false;
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo = value;
				SetDirty("Expectedallkeyinfo");
			}
		}

		public void gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo_SetNull()
		{
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo_N = true;
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo = null;
		}

		public bool gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo_IsNull()
		{
			return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo == null;
		}
		public bool ShouldSerializegxTpr_Expectedallkeyinfo_GXBaseCollection_Json()
		{
			return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo != null && gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo.Count > 0;

		}


		[SoapElement(ElementName="MsgallKeyInfo")]
		[XmlElement(ElementName="MsgallKeyInfo")]
		public string gxTpr_Msgallkeyinfo
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgallkeyinfo; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgallkeyinfo = value;
				SetDirty("Msgallkeyinfo");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Error; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Expectederror")]
		[XmlElement(ElementName="Expectederror")]
		public string gxTpr_Expectederror
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectederror; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectederror = value;
				SetDirty("Expectederror");
			}
		}




		[SoapElement(ElementName="Msgerror")]
		[XmlElement(ElementName="Msgerror")]
		public string gxTpr_Msgerror
		{
			get {
				return gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgerror; 
			}
			set {
				gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgerror = value;
				SetDirty("Msgerror");
			}
		}



		public override bool ShouldSerializeSdtJson()
		{
			return true;
		}



		#endregion

		#region Initialization

		public void initialize( )
		{
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Testcaseid = "";
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Serializedrootextkey = "";




			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo_N = true;


			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo_N = true;

			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgallkeyinfo = "";
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Error = "";
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectederror = "";
			gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgerror = "";
			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Testcaseid;
		 

		protected string gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Serializedrootextkey;
		 

		protected long gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Base;
		 

		protected long gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Begin;
		 

		protected long gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_End;
		 
		protected bool gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo_N;
		protected GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Allkeyinfo = null;  
		protected bool gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo_N;
		protected GXBaseCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo> gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectedallkeyinfo = null;  

		protected string gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgallkeyinfo;
		 

		protected string gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Error;
		 

		protected string gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Expectederror;
		 

		protected string gxTv_SdtderiveKeysFromExtKeyUnitTestSDT_Msgerror;
		 


		#endregion
	}
	#region Rest interface
	[GxUnWrappedJson()]
	[DataContract(Name=@"deriveKeysFromExtKeyUnitTestSDT", Namespace="distributedcryptography")]
	public class SdtderiveKeysFromExtKeyUnitTestSDT_RESTInterface : GxGenericCollectionItem<SdtderiveKeysFromExtKeyUnitTestSDT>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtderiveKeysFromExtKeyUnitTestSDT_RESTInterface( ) : base()
		{	
		}

		public SdtderiveKeysFromExtKeyUnitTestSDT_RESTInterface( SdtderiveKeysFromExtKeyUnitTestSDT psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="TestCaseId", Order=0)]
		public  string gxTpr_Testcaseid
		{
			get { 
				return sdt.gxTpr_Testcaseid;

			}
			set { 
				 sdt.gxTpr_Testcaseid = value;
			}
		}

		[DataMember(Name="serializedRootExtKey", Order=1)]
		public  string gxTpr_Serializedrootextkey
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Serializedrootextkey);

			}
			set { 
				 sdt.gxTpr_Serializedrootextkey = value;
			}
		}

		[DataMember(Name="base", Order=2)]
		public  string gxTpr_Base
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Base, 10, 0));

			}
			set { 
				sdt.gxTpr_Base = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="begin", Order=3)]
		public  string gxTpr_Begin
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Begin, 10, 0));

			}
			set { 
				sdt.gxTpr_Begin = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="end", Order=4)]
		public  string gxTpr_End
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_End, 10, 0));

			}
			set { 
				sdt.gxTpr_End = (long) NumberUtil.Val( value, ".");
			}
		}

		[DataMember(Name="allKeyInfo", Order=5, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface> gxTpr_Allkeyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Allkeyinfo_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface>(sdt.gxTpr_Allkeyinfo);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Allkeyinfo);
			}
		}

		[DataMember(Name="ExpectedallKeyInfo", Order=6, EmitDefaultValue=false)]
		public  GxGenericCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface> gxTpr_Expectedallkeyinfo
		{
			get { 
				if (sdt.ShouldSerializegxTpr_Expectedallkeyinfo_GXBaseCollection_Json())
					return new GxGenericCollection<GeneXus.Programs.nbitcoin.SdtKeyInfo_RESTInterface>(sdt.gxTpr_Expectedallkeyinfo);
				else
					return null;

			}
			set { 
				value.LoadCollection(sdt.gxTpr_Expectedallkeyinfo);
			}
		}

		[DataMember(Name="MsgallKeyInfo", Order=7)]
		public  string gxTpr_Msgallkeyinfo
		{
			get { 
				return sdt.gxTpr_Msgallkeyinfo;

			}
			set { 
				 sdt.gxTpr_Msgallkeyinfo = value;
			}
		}

		[DataMember(Name="error", Order=8)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[DataMember(Name="Expectederror", Order=9)]
		public  string gxTpr_Expectederror
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Expectederror);

			}
			set { 
				 sdt.gxTpr_Expectederror = value;
			}
		}

		[DataMember(Name="Msgerror", Order=10)]
		public  string gxTpr_Msgerror
		{
			get { 
				return sdt.gxTpr_Msgerror;

			}
			set { 
				 sdt.gxTpr_Msgerror = value;
			}
		}


		#endregion

		public SdtderiveKeysFromExtKeyUnitTestSDT sdt
		{
			get { 
				return (SdtderiveKeysFromExtKeyUnitTestSDT)Sdt;
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
				sdt = new SdtderiveKeysFromExtKeyUnitTestSDT() ;
			}
		}
	}
	#endregion
}