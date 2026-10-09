/*
				   File: type_SdtDelegatedAddressResult
			Description: DelegatedAddressResult
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
	[XmlRoot(ElementName="DelegatedAddressResult")]
	[XmlType(TypeName="DelegatedAddressResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtDelegatedAddressResult : GxUserType
	{
		public SdtDelegatedAddressResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtDelegatedAddressResult_Error = "";

			gxTv_SdtDelegatedAddressResult_Address = "";


		}

		public SdtDelegatedAddressResult(IGxContext context)
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
			AddObjectProperty("success", gxTpr_Success, false);


			AddObjectProperty("error", gxTpr_Error, false);


			AddObjectProperty("address", gxTpr_Address, false);


			AddObjectProperty("k", gxTpr_K, false);


			AddObjectProperty("n", gxTpr_N, false);


			AddObjectProperty("scripts", gxTpr_Scripts, false);


			AddObjectProperty("sequence", gxTpr_Sequence, false);


			AddObjectProperty("isChange", gxTpr_Ischange, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtDelegatedAddressResult_Success; 
			}
			set {
				gxTv_SdtDelegatedAddressResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtDelegatedAddressResult_Error; 
			}
			set {
				gxTv_SdtDelegatedAddressResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtDelegatedAddressResult_Address; 
			}
			set {
				gxTv_SdtDelegatedAddressResult_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="k")]
		[XmlElement(ElementName="k")]
		public short gxTpr_K
		{
			get {
				return gxTv_SdtDelegatedAddressResult_K; 
			}
			set {
				gxTv_SdtDelegatedAddressResult_K = value;
				SetDirty("K");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public short gxTpr_N
		{
			get {
				return gxTv_SdtDelegatedAddressResult_N; 
			}
			set {
				gxTv_SdtDelegatedAddressResult_N = value;
				SetDirty("N");
			}
		}




		[SoapElement(ElementName="scripts")]
		[XmlElement(ElementName="scripts")]
		public long gxTpr_Scripts
		{
			get {
				return gxTv_SdtDelegatedAddressResult_Scripts; 
			}
			set {
				gxTv_SdtDelegatedAddressResult_Scripts = value;
				SetDirty("Scripts");
			}
		}




		[SoapElement(ElementName="sequence")]
		[XmlElement(ElementName="sequence")]
		public long gxTpr_Sequence
		{
			get {
				return gxTv_SdtDelegatedAddressResult_Sequence; 
			}
			set {
				gxTv_SdtDelegatedAddressResult_Sequence = value;
				SetDirty("Sequence");
			}
		}




		[SoapElement(ElementName="isChange")]
		[XmlElement(ElementName="isChange")]
		public bool gxTpr_Ischange
		{
			get {
				return gxTv_SdtDelegatedAddressResult_Ischange; 
			}
			set {
				gxTv_SdtDelegatedAddressResult_Ischange = value;
				SetDirty("Ischange");
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
			gxTv_SdtDelegatedAddressResult_Error = "";
			gxTv_SdtDelegatedAddressResult_Address = "";





			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtDelegatedAddressResult_Success;
		 

		protected string gxTv_SdtDelegatedAddressResult_Error;
		 

		protected string gxTv_SdtDelegatedAddressResult_Address;
		 

		protected short gxTv_SdtDelegatedAddressResult_K;
		 

		protected short gxTv_SdtDelegatedAddressResult_N;
		 

		protected long gxTv_SdtDelegatedAddressResult_Scripts;
		 

		protected long gxTv_SdtDelegatedAddressResult_Sequence;
		 

		protected bool gxTv_SdtDelegatedAddressResult_Ischange;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"DelegatedAddressResult", Namespace="distributedcryptography")]
	public class SdtDelegatedAddressResult_RESTInterface : GxGenericCollectionItem<SdtDelegatedAddressResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtDelegatedAddressResult_RESTInterface( ) : base()
		{	
		}

		public SdtDelegatedAddressResult_RESTInterface( SdtDelegatedAddressResult psdt ) : base(psdt)
		{	
		}

		#region Rest Properties
		[JsonPropertyName("success")]
		[JsonPropertyOrder(0)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="success", Order=0)]
		public bool gxTpr_Success
		{
			get { 
				return sdt.gxTpr_Success;

			}
			set { 
				sdt.gxTpr_Success = value;
			}
		}

		[JsonPropertyName("error")]
		[JsonPropertyOrder(1)]
		[DataMember(Name="error", Order=1)]
		public  string gxTpr_Error
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Error);

			}
			set { 
				 sdt.gxTpr_Error = value;
			}
		}

		[JsonPropertyName("address")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="address", Order=2)]
		public  string gxTpr_Address
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Address);

			}
			set { 
				 sdt.gxTpr_Address = value;
			}
		}

		[JsonPropertyName("k")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="k", Order=3)]
		public short gxTpr_K
		{
			get { 
				return sdt.gxTpr_K;

			}
			set { 
				sdt.gxTpr_K = value;
			}
		}

		[JsonPropertyName("n")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="n", Order=4)]
		public short gxTpr_N
		{
			get { 
				return sdt.gxTpr_N;

			}
			set { 
				sdt.gxTpr_N = value;
			}
		}

		[JsonPropertyName("scripts")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="scripts", Order=5)]
		public  string gxTpr_Scripts
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Scripts, 10, 0));

			}
			set { 
				sdt.gxTpr_Scripts = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("sequence")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="sequence", Order=6)]
		public  string gxTpr_Sequence
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Sequence, 10, 0));

			}
			set { 
				sdt.gxTpr_Sequence = (long) NumberUtil.Val( value, ".");
			}
		}

		[JsonPropertyName("isChange")]
		[JsonPropertyOrder(7)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isChange", Order=7)]
		public bool gxTpr_Ischange
		{
			get { 
				return sdt.gxTpr_Ischange;

			}
			set { 
				sdt.gxTpr_Ischange = value;
			}
		}


		#endregion
		[JsonIgnore]
		public SdtDelegatedAddressResult sdt
		{
			get { 
				return (SdtDelegatedAddressResult)Sdt;
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
				sdt = new SdtDelegatedAddressResult() ;
			}
		}
	}
	#endregion
}