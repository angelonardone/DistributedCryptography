/*
				   File: type_SdtGroupRecordResponse
			Description: GroupRecordResponse
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
	[XmlRoot(ElementName="GroupRecordResponse")]
	[XmlType(TypeName="GroupRecordResponse" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupRecordResponse : GxUserType
	{
		public SdtGroupRecordResponse( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroupRecordResponse_Error = "";


		}

		public SdtGroupRecordResponse(IGxContext context)
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
			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("Version", gxTpr_Version, false);


			AddObjectProperty("NewVersion", gxTpr_Newversion, false);

			if (gxTv_SdtGroupRecordResponse_Records != null)
			{
				AddObjectProperty("records", gxTv_SdtGroupRecordResponse_Records, false);
			}
			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtGroupRecordResponse_Error; 
			}
			set {
				gxTv_SdtGroupRecordResponse_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="Version")]
		[XmlElement(ElementName="Version")]
		public long gxTpr_Version
		{
			get {
				return gxTv_SdtGroupRecordResponse_Version; 
			}
			set {
				gxTv_SdtGroupRecordResponse_Version = value;
				SetDirty("Version");
			}
		}




		[SoapElement(ElementName="NewVersion")]
		[XmlElement(ElementName="NewVersion")]
		public long gxTpr_Newversion
		{
			get {
				return gxTv_SdtGroupRecordResponse_Newversion; 
			}
			set {
				gxTv_SdtGroupRecordResponse_Newversion = value;
				SetDirty("Newversion");
			}
		}



		[SoapElement(ElementName="records" )]
		[XmlElement(ElementName="records" )]
		public SdtGroupRecordResponse_records gxTpr_Records
		{
			get {
				if ( gxTv_SdtGroupRecordResponse_Records == null )
				{
					gxTv_SdtGroupRecordResponse_Records = new SdtGroupRecordResponse_records(context);
				}
				gxTv_SdtGroupRecordResponse_Records_N = false;
				SetDirty("Records");
				return gxTv_SdtGroupRecordResponse_Records;
			}
			set {
				gxTv_SdtGroupRecordResponse_Records_N = false;
				gxTv_SdtGroupRecordResponse_Records = value;
				SetDirty("Records");
			}

		}

		public void gxTv_SdtGroupRecordResponse_Records_SetNull()
		{
			gxTv_SdtGroupRecordResponse_Records_N = true;
			gxTv_SdtGroupRecordResponse_Records = null;
		}

		public bool gxTv_SdtGroupRecordResponse_Records_IsNull()
		{
			return gxTv_SdtGroupRecordResponse_Records == null;
		}
		public bool ShouldSerializegxTpr_Records_Json()
		{
				return (gxTv_SdtGroupRecordResponse_Records != null && gxTv_SdtGroupRecordResponse_Records.ShouldSerializeSdtJson());

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
			gxTv_SdtGroupRecordResponse_Error = "";



			gxTv_SdtGroupRecordResponse_Records_N = true;

			return  ;
		}



		#endregion

		#region Declaration

		protected string gxTv_SdtGroupRecordResponse_Error;
		 

		protected long gxTv_SdtGroupRecordResponse_Version;
		 

		protected long gxTv_SdtGroupRecordResponse_Newversion;
		 
		protected bool gxTv_SdtGroupRecordResponse_Records_N;
		protected SdtGroupRecordResponse_records gxTv_SdtGroupRecordResponse_Records = null; 



		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GroupRecordResponse", Namespace="distributedcryptography")]
	public class SdtGroupRecordResponse_RESTInterface : GxGenericCollectionItem<SdtGroupRecordResponse>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupRecordResponse_RESTInterface( ) : base()
		{	
		}

		public SdtGroupRecordResponse_RESTInterface( SdtGroupRecordResponse psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("error")]
		[JsonPropertyOrder(0)]
		[DataMember(Name="error", Order=0)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[JsonPropertyName("Version")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="Version", Order=1)]
		public  string gxTpr_Version
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Version, 10, 0));

			}
			set { 
				sdt.gxTpr_Version = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("NewVersion")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="NewVersion", Order=2)]
		public  string gxTpr_Newversion
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Newversion, 10, 0));

			}
			set { 
				sdt.gxTpr_Newversion = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("records")]
		[JsonPropertyOrder(3)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DataMember(Name="records", Order=3, EmitDefaultValue=false)]
		public SdtGroupRecordResponse_records_RESTInterface gxTpr_Records
		{
			get {
				if (sdt.ShouldSerializegxTpr_Records_Json())
					return new SdtGroupRecordResponse_records_RESTInterface(sdt.gxTpr_Records);
				else
					return null;

			}

			set {
				sdt.gxTpr_Records = value.sdt;
			}

		}


		#endregion
		[JsonIgnore]
		public SdtGroupRecordResponse sdt
		{
			get { 
				return (SdtGroupRecordResponse)Sdt;
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
				sdt = new SdtGroupRecordResponse() ;
			}
		}
	}
	#endregion
}