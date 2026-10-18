/*
				   File: type_Sdtlogin__postOutput
			Description: login__postOutput
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


namespace GeneXus.Programs
{
	[XmlRoot(ElementName="login__postOutput")]
	[XmlType(TypeName="login__postOutput" , Namespace="distributedcryptography" )]
	[Serializable]
	public class Sdtlogin__postOutput : GxUserType
	{
		public Sdtlogin__postOutput( )
		{
			/* Constructor for serialization */
			gxTv_Sdtlogin__postOutput_Token = "";

			gxTv_Sdtlogin__postOutput_Tokenexpires = (DateTime)(DateTime.MinValue);

			gxTv_Sdtlogin__postOutput_Userplan = "";

			gxTv_Sdtlogin__postOutput_Error = "";

		}

		public Sdtlogin__postOutput(IGxContext context)
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
			AddObjectProperty("Token", gxTpr_Token, false);


			datetime_STZ = gxTpr_Tokenexpires;
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
			AddObjectProperty("TokenExpires", sDateCnv, false);



			AddObjectProperty("UserPlan", gxTpr_Userplan, false);


			AddObjectProperty("error", gxTpr_Error, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="Token")]
		[XmlElement(ElementName="Token")]
		public string gxTpr_Token
		{
			get {
				return gxTv_Sdtlogin__postOutput_Token; 
			}
			set {
				gxTv_Sdtlogin__postOutput_Token = value;
				SetDirty("Token");
			}
		}



		[SoapElement(ElementName="TokenExpires")]
		[XmlElement(ElementName="TokenExpires" , IsNullable=true)]
		public string gxTpr_Tokenexpires_Nullable
		{
			get {
				if ( gxTv_Sdtlogin__postOutput_Tokenexpires == DateTime.MinValue)
					return null;
				return new GxDatetimeString(gxTv_Sdtlogin__postOutput_Tokenexpires).value ;
			}
			set {
				gxTv_Sdtlogin__postOutput_Tokenexpires = DateTimeUtil.CToD2(value);
			}
		}

		[XmlIgnore]
		public DateTime gxTpr_Tokenexpires
		{
			get {
				return gxTv_Sdtlogin__postOutput_Tokenexpires; 
			}
			set {
				gxTv_Sdtlogin__postOutput_Tokenexpires = value;
				SetDirty("Tokenexpires");
			}
		}



		[SoapElement(ElementName="UserPlan")]
		[XmlElement(ElementName="UserPlan")]
		public string gxTpr_Userplan
		{
			get {
				return gxTv_Sdtlogin__postOutput_Userplan; 
			}
			set {
				gxTv_Sdtlogin__postOutput_Userplan = value;
				SetDirty("Userplan");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_Sdtlogin__postOutput_Error; 
			}
			set {
				gxTv_Sdtlogin__postOutput_Error = value;
				SetDirty("Error");
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
			gxTv_Sdtlogin__postOutput_Token = "";
			gxTv_Sdtlogin__postOutput_Tokenexpires = (DateTime)(DateTime.MinValue);
			gxTv_Sdtlogin__postOutput_Userplan = "";
			gxTv_Sdtlogin__postOutput_Error = "";
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

		protected string gxTv_Sdtlogin__postOutput_Token;
		 

		protected DateTime gxTv_Sdtlogin__postOutput_Tokenexpires;
		 

		protected string gxTv_Sdtlogin__postOutput_Userplan;
		 

		protected string gxTv_Sdtlogin__postOutput_Error;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"login__postOutput", Namespace="distributedcryptography")]
	public class Sdtlogin__postOutput_RESTInterface : GxGenericCollectionItem<Sdtlogin__postOutput>, System.Web.SessionState.IRequiresSessionState
	{
		public Sdtlogin__postOutput_RESTInterface( ) : base()
		{	
		}

		public Sdtlogin__postOutput_RESTInterface( Sdtlogin__postOutput psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("Token")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="Token", Order=0)]
		public  string gxTpr_Token
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Token);

			}
			set { 
				 sdt.gxTpr_Token = value;
			}
		}

		[JsonPropertyName("TokenExpires")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="TokenExpires", Order=1)]
		public  string gxTpr_Tokenexpires
		{
			get { 
				return DateTimeUtil.TToC2( sdt.gxTpr_Tokenexpires,context);

			}
			set { 
				sdt.gxTpr_Tokenexpires = DateTimeUtil.CToT2(value,context);
			}
		}

		[JsonPropertyName("UserPlan")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="UserPlan", Order=2)]
		public  string gxTpr_Userplan
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Userplan);

			}
			set { 
				 sdt.gxTpr_Userplan = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="error", Order=3)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}


		#endregion
		[JsonIgnore]
		public Sdtlogin__postOutput sdt
		{
			get { 
				return (Sdtlogin__postOutput)Sdt;
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
				sdt = new Sdtlogin__postOutput() ;
			}
		}
	}
	#endregion
}