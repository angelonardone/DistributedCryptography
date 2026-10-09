/*
				   File: type_SdtLegacyAddressResult
			Description: LegacyAddressResult
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
	[XmlRoot(ElementName="LegacyAddressResult")]
	[XmlType(TypeName="LegacyAddressResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtLegacyAddressResult : GxUserType
	{
		public SdtLegacyAddressResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtLegacyAddressResult_Error = "";

			gxTv_SdtLegacyAddressResult_Address = "";

			gxTv_SdtLegacyAddressResult_P2shp2wsh = "";

			gxTv_SdtLegacyAddressResult_P2wsh = "";

			gxTv_SdtLegacyAddressResult_Witnessscript = "";


		}

		public SdtLegacyAddressResult(IGxContext context)
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


			AddObjectProperty("p2shP2wsh", gxTpr_P2shp2wsh, false);


			AddObjectProperty("p2wsh", gxTpr_P2wsh, false);


			AddObjectProperty("witnessScript", gxTpr_Witnessscript, false);


			AddObjectProperty("k", gxTpr_K, false);


			AddObjectProperty("n", gxTpr_N, false);


			AddObjectProperty("sequence", gxTpr_Sequence, false);

			return;
		}
		#endregion

		#region Properties

		[SoapElement(ElementName="success")]
		[XmlElement(ElementName="success")]
		public bool gxTpr_Success
		{
			get {
				return gxTv_SdtLegacyAddressResult_Success; 
			}
			set {
				gxTv_SdtLegacyAddressResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtLegacyAddressResult_Error; 
			}
			set {
				gxTv_SdtLegacyAddressResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="address")]
		[XmlElement(ElementName="address")]
		public string gxTpr_Address
		{
			get {
				return gxTv_SdtLegacyAddressResult_Address; 
			}
			set {
				gxTv_SdtLegacyAddressResult_Address = value;
				SetDirty("Address");
			}
		}




		[SoapElement(ElementName="p2shP2wsh")]
		[XmlElement(ElementName="p2shP2wsh")]
		public string gxTpr_P2shp2wsh
		{
			get {
				return gxTv_SdtLegacyAddressResult_P2shp2wsh; 
			}
			set {
				gxTv_SdtLegacyAddressResult_P2shp2wsh = value;
				SetDirty("P2shp2wsh");
			}
		}




		[SoapElement(ElementName="p2wsh")]
		[XmlElement(ElementName="p2wsh")]
		public string gxTpr_P2wsh
		{
			get {
				return gxTv_SdtLegacyAddressResult_P2wsh; 
			}
			set {
				gxTv_SdtLegacyAddressResult_P2wsh = value;
				SetDirty("P2wsh");
			}
		}




		[SoapElement(ElementName="witnessScript")]
		[XmlElement(ElementName="witnessScript")]
		public string gxTpr_Witnessscript
		{
			get {
				return gxTv_SdtLegacyAddressResult_Witnessscript; 
			}
			set {
				gxTv_SdtLegacyAddressResult_Witnessscript = value;
				SetDirty("Witnessscript");
			}
		}




		[SoapElement(ElementName="k")]
		[XmlElement(ElementName="k")]
		public short gxTpr_K
		{
			get {
				return gxTv_SdtLegacyAddressResult_K; 
			}
			set {
				gxTv_SdtLegacyAddressResult_K = value;
				SetDirty("K");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public short gxTpr_N
		{
			get {
				return gxTv_SdtLegacyAddressResult_N; 
			}
			set {
				gxTv_SdtLegacyAddressResult_N = value;
				SetDirty("N");
			}
		}




		[SoapElement(ElementName="sequence")]
		[XmlElement(ElementName="sequence")]
		public long gxTpr_Sequence
		{
			get {
				return gxTv_SdtLegacyAddressResult_Sequence; 
			}
			set {
				gxTv_SdtLegacyAddressResult_Sequence = value;
				SetDirty("Sequence");
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
			gxTv_SdtLegacyAddressResult_Error = "";
			gxTv_SdtLegacyAddressResult_Address = "";
			gxTv_SdtLegacyAddressResult_P2shp2wsh = "";
			gxTv_SdtLegacyAddressResult_P2wsh = "";
			gxTv_SdtLegacyAddressResult_Witnessscript = "";



			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtLegacyAddressResult_Success;
		 

		protected string gxTv_SdtLegacyAddressResult_Error;
		 

		protected string gxTv_SdtLegacyAddressResult_Address;
		 

		protected string gxTv_SdtLegacyAddressResult_P2shp2wsh;
		 

		protected string gxTv_SdtLegacyAddressResult_P2wsh;
		 

		protected string gxTv_SdtLegacyAddressResult_Witnessscript;
		 

		protected short gxTv_SdtLegacyAddressResult_K;
		 

		protected short gxTv_SdtLegacyAddressResult_N;
		 

		protected long gxTv_SdtLegacyAddressResult_Sequence;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"LegacyAddressResult", Namespace="distributedcryptography")]
	public class SdtLegacyAddressResult_RESTInterface : GxGenericCollectionItem<SdtLegacyAddressResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtLegacyAddressResult_RESTInterface( ) : base()
		{	
		}

		public SdtLegacyAddressResult_RESTInterface( SdtLegacyAddressResult psdt ) : base(psdt)
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

		[JsonPropertyName("p2shP2wsh")]
		[JsonPropertyOrder(3)]
		[DataMember(Name="p2shP2wsh", Order=3)]
		public  string gxTpr_P2shp2wsh
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_P2shp2wsh);

			}
			set { 
				 sdt.gxTpr_P2shp2wsh = value;
			}
		}

		[JsonPropertyName("p2wsh")]
		[JsonPropertyOrder(4)]
		[DataMember(Name="p2wsh", Order=4)]
		public  string gxTpr_P2wsh
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_P2wsh);

			}
			set { 
				 sdt.gxTpr_P2wsh = value;
			}
		}

		[JsonPropertyName("witnessScript")]
		[JsonPropertyOrder(5)]
		[DataMember(Name="witnessScript", Order=5)]
		public  string gxTpr_Witnessscript
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Witnessscript);

			}
			set { 
				 sdt.gxTpr_Witnessscript = value;
			}
		}

		[JsonPropertyName("k")]
		[JsonPropertyOrder(6)]
		[DataMember(Name="k", Order=6)]
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
		[JsonPropertyOrder(7)]
		[DataMember(Name="n", Order=7)]
		public short gxTpr_N
		{
			get { 
				return sdt.gxTpr_N;

			}
			set { 
				sdt.gxTpr_N = value;
			}
		}

		[JsonPropertyName("sequence")]
		[JsonPropertyOrder(8)]
		[DataMember(Name="sequence", Order=8)]
		public  string gxTpr_Sequence
		{
			get { 
				return StringUtil.LTrim( StringUtil.Str( (decimal) sdt.gxTpr_Sequence, 10, 0));

			}
			set { 
				sdt.gxTpr_Sequence = (long) NumberUtil.Val( value, ".");
			}
		}


		#endregion
		[JsonIgnore]
		public SdtLegacyAddressResult sdt
		{
			get { 
				return (SdtLegacyAddressResult)Sdt;
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
				sdt = new SdtLegacyAddressResult() ;
			}
		}
	}
	#endregion
}