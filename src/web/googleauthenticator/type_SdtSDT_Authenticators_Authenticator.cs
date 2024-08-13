/*
				   File: type_SdtSDT_Authenticators_Authenticator
			Description: SDT_Authenticators
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
namespace GeneXus.Programs.googleauthenticator
{
	[XmlRoot(ElementName="Authenticator")]
	[XmlType(TypeName="Authenticator" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtSDT_Authenticators_Authenticator : GxUserType
	{
		public SdtSDT_Authenticators_Authenticator( )
		{
			/* Constructor for serialization */
			gxTv_SdtSDT_Authenticators_Authenticator_Issuer = "";

			gxTv_SdtSDT_Authenticators_Authenticator_Accounttitle = "";

			gxTv_SdtSDT_Authenticators_Authenticator_Based32key = "";

			gxTv_SdtSDT_Authenticators_Authenticator_Createddatetime = (DateTime)(DateTime.MinValue);

		}

		public SdtSDT_Authenticators_Authenticator(IGxContext context)
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
			AddObjectProperty("issuer", gxTpr_Issuer, false);


			AddObjectProperty("accountTitle", gxTpr_Accounttitle, false);


			AddObjectProperty("based32Key", gxTpr_Based32key, false);


			datetime_STZ = gxTpr_Createddatetime;
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
			AddObjectProperty("createdDateTime", sDateCnv, false);


			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="issuer")]
		[XmlElement(ElementName="issuer")]
		public string gxTpr_Issuer
		{
			get {
				return gxTv_SdtSDT_Authenticators_Authenticator_Issuer; 
			}
			set {
				gxTv_SdtSDT_Authenticators_Authenticator_Issuer = value;
				SetDirty("Issuer");
			}
		}




		[SoapElement(ElementName="accountTitle")]
		[XmlElement(ElementName="accountTitle")]
		public string gxTpr_Accounttitle
		{
			get {
				return gxTv_SdtSDT_Authenticators_Authenticator_Accounttitle; 
			}
			set {
				gxTv_SdtSDT_Authenticators_Authenticator_Accounttitle = value;
				SetDirty("Accounttitle");
			}
		}




		[SoapElement(ElementName="based32Key")]
		[XmlElement(ElementName="based32Key")]
		public string gxTpr_Based32key
		{
			get {
				return gxTv_SdtSDT_Authenticators_Authenticator_Based32key; 
			}
			set {
				gxTv_SdtSDT_Authenticators_Authenticator_Based32key = value;
				SetDirty("Based32key");
			}
		}



		[SoapElement(ElementName="createdDateTime")]
		[XmlElement(ElementName="createdDateTime" , IsNullable=true)]
		public string gxTpr_Createddatetime_Nullable
		{
			get {
				if ( gxTv_SdtSDT_Authenticators_Authenticator_Createddatetime == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_SdtSDT_Authenticators_Authenticator_Createddatetime).value ;
			}
			set {
				gxTv_SdtSDT_Authenticators_Authenticator_Createddatetime = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Createddatetime
		{
			get {
				return gxTv_SdtSDT_Authenticators_Authenticator_Createddatetime; 
			}
			set {
				gxTv_SdtSDT_Authenticators_Authenticator_Createddatetime = value;
				SetDirty("Createddatetime");
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
			gxTv_SdtSDT_Authenticators_Authenticator_Issuer = "";
			gxTv_SdtSDT_Authenticators_Authenticator_Accounttitle = "";
			gxTv_SdtSDT_Authenticators_Authenticator_Based32key = "";
			gxTv_SdtSDT_Authenticators_Authenticator_Createddatetime = (DateTime)(DateTime.MinValue);
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

		protected string gxTv_SdtSDT_Authenticators_Authenticator_Issuer;
		 

		protected string gxTv_SdtSDT_Authenticators_Authenticator_Accounttitle;
		 

		protected string gxTv_SdtSDT_Authenticators_Authenticator_Based32key;
		 

		protected DateTime gxTv_SdtSDT_Authenticators_Authenticator_Createddatetime;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("wrapped")]
	[DataContract(Name=@"Authenticator", Namespace="distributedcryptography")]
	public class SdtSDT_Authenticators_Authenticator_RESTInterface : GxGenericCollectionItem<SdtSDT_Authenticators_Authenticator>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtSDT_Authenticators_Authenticator_RESTInterface( ) : base()
		{	
		}

		public SdtSDT_Authenticators_Authenticator_RESTInterface( SdtSDT_Authenticators_Authenticator psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[DataMember(Name="issuer", Order=0)]
		public  string gxTpr_Issuer
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Issuer);

			}
			set { 
				 sdt.gxTpr_Issuer = value;
			}
		}

		[DataMember(Name="accountTitle", Order=1)]
		public  string gxTpr_Accounttitle
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Accounttitle);

			}
			set { 
				 sdt.gxTpr_Accounttitle = value;
			}
		}

		[DataMember(Name="based32Key", Order=2)]
		public  string gxTpr_Based32key
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Based32key);

			}
			set { 
				 sdt.gxTpr_Based32key = value;
			}
		}

		[DataMember(Name="createdDateTime", Order=3)]
		public  string gxTpr_Createddatetime
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Createddatetime,context);

			}
			set { 
				sdt.gxTpr_Createddatetime = DateTimeUtil.CToT2(value,context);
			}
		}


		#endregion

		public SdtSDT_Authenticators_Authenticator sdt
		{
			get { 
				return (SdtSDT_Authenticators_Authenticator)Sdt;
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
				sdt = new SdtSDT_Authenticators_Authenticator() ;
			}
		}
	}
	#endregion
}