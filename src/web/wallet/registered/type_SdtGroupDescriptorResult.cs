/*
				   File: type_SdtGroupDescriptorResult
			Description: GroupDescriptorResult
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
	[XmlRoot(ElementName="GroupDescriptorResult")]
	[XmlType(TypeName="GroupDescriptorResult" , Namespace="distributedcryptography" )]
	[Serializable]
	public class SdtGroupDescriptorResult : GxUserType
	{
		public SdtGroupDescriptorResult( )
		{
			/* Constructor for serialization */
			gxTv_SdtGroupDescriptorResult_Error = "";

			gxTv_SdtGroupDescriptorResult_Descriptor = "";


		}

		public SdtGroupDescriptorResult(IGxContext context)
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


			AddObjectProperty("descriptor", gxTpr_Descriptor, false);


			AddObjectProperty("k", gxTpr_K, false);


			AddObjectProperty("n", gxTpr_N, false);


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
				return gxTv_SdtGroupDescriptorResult_Success; 
			}
			set {
				gxTv_SdtGroupDescriptorResult_Success = value;
				SetDirty("Success");
			}
		}




		[SoapElement(ElementName="error")]
		[XmlElement(ElementName="error")]
		public string gxTpr_Error
		{
			get {
				return gxTv_SdtGroupDescriptorResult_Error; 
			}
			set {
				gxTv_SdtGroupDescriptorResult_Error = value;
				SetDirty("Error");
			}
		}




		[SoapElement(ElementName="descriptor")]
		[XmlElement(ElementName="descriptor")]
		public string gxTpr_Descriptor
		{
			get {
				return gxTv_SdtGroupDescriptorResult_Descriptor; 
			}
			set {
				gxTv_SdtGroupDescriptorResult_Descriptor = value;
				SetDirty("Descriptor");
			}
		}




		[SoapElement(ElementName="k")]
		[XmlElement(ElementName="k")]
		public short gxTpr_K
		{
			get {
				return gxTv_SdtGroupDescriptorResult_K; 
			}
			set {
				gxTv_SdtGroupDescriptorResult_K = value;
				SetDirty("K");
			}
		}




		[SoapElement(ElementName="n")]
		[XmlElement(ElementName="n")]
		public short gxTpr_N
		{
			get {
				return gxTv_SdtGroupDescriptorResult_N; 
			}
			set {
				gxTv_SdtGroupDescriptorResult_N = value;
				SetDirty("N");
			}
		}




		[SoapElement(ElementName="isChange")]
		[XmlElement(ElementName="isChange")]
		public bool gxTpr_Ischange
		{
			get {
				return gxTv_SdtGroupDescriptorResult_Ischange; 
			}
			set {
				gxTv_SdtGroupDescriptorResult_Ischange = value;
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
			gxTv_SdtGroupDescriptorResult_Error = "";
			gxTv_SdtGroupDescriptorResult_Descriptor = "";



			return  ;
		}



		#endregion

		#region Declaration

		protected bool gxTv_SdtGroupDescriptorResult_Success;
		 

		protected string gxTv_SdtGroupDescriptorResult_Error;
		 

		protected string gxTv_SdtGroupDescriptorResult_Descriptor;
		 

		protected short gxTv_SdtGroupDescriptorResult_K;
		 

		protected short gxTv_SdtGroupDescriptorResult_N;
		 

		protected bool gxTv_SdtGroupDescriptorResult_Ischange;
		 


		#endregion
	}
	#region Rest interface
	[GxJsonSerialization("default")]
	[DataContract(Name=@"GroupDescriptorResult", Namespace="distributedcryptography")]
	public class SdtGroupDescriptorResult_RESTInterface : GxGenericCollectionItem<SdtGroupDescriptorResult>, System.Web.SessionState.IRequiresSessionState
	{
		public SdtGroupDescriptorResult_RESTInterface( ) : base()
		{	
		}

		public SdtGroupDescriptorResult_RESTInterface( SdtGroupDescriptorResult psdt ) : base(psdt)
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

		[JsonPropertyName("descriptor")]
		[JsonPropertyOrder(2)]
		[DataMember(Name="descriptor", Order=2)]
		public  string gxTpr_Descriptor
		{
			get { 
				return StringUtil.RTrim( sdt.gxTpr_Descriptor);

			}
			set { 
				 sdt.gxTpr_Descriptor = value;
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

		[JsonPropertyName("isChange")]
		[JsonPropertyOrder(5)]
		[JsonConverter(typeof(BoolStringJsonConverter))]
		[DataMember(Name="isChange", Order=5)]
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
		public SdtGroupDescriptorResult sdt
		{
			get { 
				return (SdtGroupDescriptorResult)Sdt;
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
				sdt = new SdtGroupDescriptorResult() ;
			}
		}
	}
	#endregion
}